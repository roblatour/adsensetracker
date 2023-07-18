/*
Copyright 2023 Rob Latour
License: MIT https://rlatour.com/adsensetracker/License.txt
*/

/*
 
NOTICE: the code below was derived from the original code found here: 
https://github.com/googleads/googleads-adsense-examples
with unneeded portions from the orignal code removed and some modifications made to fit the needs of this project.

Copyright 2021 Google Inc

Licensed under the Apache License, Version 2.0(the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using Google.Apis.Adsense.v2;
using Google.Apis.Adsense.v2.Data;
using DimensionsEnum = Google.Apis.Adsense.v2.AccountsResource.ReportsResource.GenerateRequest.DimensionsEnum;
using MetricsEnum = Google.Apis.Adsense.v2.AccountsResource.ReportsResource.GenerateRequest.MetricsEnum; 
using SavedDateRangeEnum = Google.Apis.Adsense.v2.AccountsResource.ReportsResource.SavedResource.GenerateRequest.DateRangeEnum;

namespace AdSense.Driver
{

    public class ManagementApiConsumer
    {
        public DateTime ReportingDate { get; private set; }
        public int TotalPageViews { get; private set; }
        public int TotalClicks { get; private set; }
        public decimal TotalEarnings { get; private set; }

        private AdsenseService service;
        private int maxListPageSize; 
        private Account adSenseAccount;

        private IList<AdClient> adClients;

        public ManagementApiConsumer(AdsenseService service, int maxListPageSize)
        {
            this.service = service;
            this.maxListPageSize = maxListPageSize;
        }
        internal void SetupCalls()
        {

            IList<Account> accounts = GetAllAccounts();

            // Get an example account, so we can run the following samples.
            adSenseAccount = accounts.NullToEmpty().FirstOrDefault();

            // var adClients = GetAllAdClients();
            adClients = GetAllAdClients();

            // Get an ad client, so we can run the rest of the samples.
            var exampleAdClient = adClients.NullToEmpty().FirstOrDefault();                     

        }

        internal void RunCalls()
        {

            var exampleAdClient = adClients.NullToEmpty().FirstOrDefault();
            if (exampleAdClient != null)
                GenerateReport(exampleAdClient.Name);

        }

        /// <summary>Gets and prints all accounts for the logged in user.</summary>
        /// <returns>The last page of retrieved accounts.</returns>
        private IList<Account> GetAllAccounts()
        {

            string pageToken = null;
            ListAccountsResponse accountResponse = null;

            do
            {
                var accountRequest = service.Accounts.List();
                accountRequest.PageSize = maxListPageSize;
                accountRequest.PageToken = pageToken;
                accountResponse = accountRequest.Execute();

                pageToken = accountResponse.NextPageToken;

            } while (pageToken != null);

            return accountResponse.Accounts;
        }

        private IList<AdClient> GetAllAdClients()
        {
                 
            string pageToken = null;
            ListAdClientsResponse adClientResponse = null;

            do
            {
                var adClientRequest = service.Accounts.Adclients.List(adSenseAccount.Name);
                adClientRequest.PageSize = maxListPageSize;
                adClientRequest.PageToken = pageToken;
                adClientResponse = adClientRequest.Execute();                           

                pageToken = adClientResponse.NextPageToken;

            } while (pageToken != null);
 
            // Return the last page of ad clients, so that the main sample has something to run.
            return adClientResponse.AdClients;
        }

   
        private void GenerateReport(string adClientId)
        {

            // Prepare report.
            var reportRequest = service.Accounts.Reports.Generate(adSenseAccount.Name);

            // Specify the reporting date as the current date 

            DateTime now = DateTime.Now;
            int year = now.Year;
            int month = now.Month;
            int day = now.Day;

            reportRequest.StartDateYear = year;
            reportRequest.StartDateMonth = month;
            reportRequest.StartDateDay = day;
            reportRequest.EndDateYear = year;
            reportRequest.EndDateMonth = month;
            reportRequest.EndDateDay = day;

            reportRequest.Filters = new List<string> { "AD_CLIENT_ID==" + ReportUtils.EscapeFilterParameter(adClientId) };

            reportRequest.AddMetric(MetricsEnum.PAGEVIEWS);
            reportRequest.AddMetric(MetricsEnum.CLICKS);
            reportRequest.AddMetric(MetricsEnum.ESTIMATEDEARNINGS);

            reportRequest.AddDimension(DimensionsEnum.DATE);
            // reportRequest.OrderBy = new List<string> { "+DATE" };

            // Run the report
            var reportResponse = reportRequest.Execute();

            if (reportResponse.Rows.IsNullOrEmpty())
            {
                ReportingDate = now;
                TotalPageViews = 0;
                TotalClicks = 0;
                TotalEarnings = 0;
            }
            else
            {
                ReportingDate = DateTime.Parse(reportResponse.Rows[0].Cells[0].Value);                               
                TotalPageViews = Int32.Parse(reportResponse.Rows[0].Cells[1].Value);
                TotalClicks = Int32.Parse(reportResponse.Rows[0].Cells[2].Value);
                TotalEarnings = Decimal.Parse(reportResponse.Rows[0].Cells[3].Value);
            };
     
        }           

    }
}
