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
using System.Linq;
using System.Text;

using Google.Apis.Adsense.v2;
using Google.Apis.Adsense.v2.Data;
using Google.Apis.Util;

using GenerateRequest = Google.Apis.Adsense.v2.AccountsResource.ReportsResource.GenerateRequest;

namespace AdSense.Driver {
  /// <summary>
  /// Collection of utilities to display and modify reports
  /// </summary>
  public static class ReportUtils {

    public const string DATEPATTERN = "yyyy-MM-dd";
    public const string MONTHPATTERN = "yyyy-MM";

    /// <summary>
    /// Escape special characters for a parameter being used in a filter.
    /// </summary>
    /// <param name="parameter">The parameter to be escaped.</param>
    /// <returns>The escaped parameter.</returns>
    public static string EscapeFilterParameter(string parameter) {
      return parameter.Split("/").Last().Replace("\\", "\\\\").Replace(",", "\\,");
    }

    public static IList<T> NullToEmpty<T>(this IList<T> list) {
      return list ?? new List<T>();
    }

    public static bool IsNullOrEmpty<T>(this IList<T> list) {
      return list == null || list.Count == 0;
    }

    public static void AddDimension(this GenerateRequest request,
                                    GenerateRequest.DimensionsEnum dimension) {
      string dimensionText = Utilities.ConvertToString(dimension);

      request.ModifyRequest += message => {
        var uriBuilder = new UriBuilder(message.RequestUri);
        string separator = uriBuilder.Query == "" ? "" : "&";
        uriBuilder.Query += $"{separator}dimensions={dimensionText}";
        message.RequestUri = uriBuilder.Uri;
      };
    }

    public static void AddMetric(this GenerateRequest request, GenerateRequest.MetricsEnum metric) {
      string metricText = Utilities.ConvertToString(metric);

      request.ModifyRequest += message => {
        var uriBuilder = new UriBuilder(message.RequestUri);
        string separator = uriBuilder.Query == "" ? "" : "&";
        uriBuilder.Query += $"{separator}metrics={metricText}";
        message.RequestUri = uriBuilder.Uri;
      };
    }
  }
}
