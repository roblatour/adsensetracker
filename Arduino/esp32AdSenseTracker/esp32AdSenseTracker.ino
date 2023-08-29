// ESP32 AdSense Tracker v1.2
//
// Copyright Rob Latour, 2023
// License: MIT
//
// https://rlatour.com/adsensetracker
//
// Compile and upload using Arduino IDE (2.1.1 or greater)
//
// Physical board:                 LILYGO T-Display S3
// Board in Arduino board manager: ESP32S3 Dev Module
//
// Arduino Tools settings:
// ----------------------------------------------------------------
// USB CDC On Boot:                Enabled
// CPU Frequency:                  240MHz
// Core Debug Level:               None
// USB DFU On Boot:                Enabled (Requires USB-OTG Mode)
// Erase All Flash Before Upload:  Disabled
// Events Run On:                  Core 1
// Flash Mode:                     QIO 80Mhz
// Flash Size:                     16MB (128MB)
// JTAG Adapter:                   Integrated USB JTAG
// Arduino Runs On:                Core 1
// USB Firmware MSC On Boot:       Disabled
// Partition Scheme:               16M Flash (3MB APP/9.9MB FATFS)
// PSRAM:                          OPI PSRAM
// Upload Mode:                    UART0 / Hardware CDC
// Upload Speed:                   921600
// USB Mode:                       Hardware CDC and JTAG
// ----------------------------------------------------------------
// Programmer                      ESPTool
// ----------------------------------------------------------------

#include "secrets.h"  // contains the Google, MQTT, Wi-Fi and Over The Air (OTA) update credentials used in this sketch

#include <TFT_eSPI.h>       // for the LilyGo T-Display S3 use the TFT_eSPI library found here: https://github.com/Xinyuan-LilyGO/T-Display-S3/tree/main/lib
#include <EspMQTTClient.h>  // https://github.com/plapointe6/EspMQTTClient
#include <string.h>

#define ProgramName "ESP32AdSenseTracker"

// ESP32 Lilyo T-Display Green LED
#define GREEN_LED 38

// ESP32 LilyGo T-Display S3 Buttons
#define TOP_BUTTON 14    // Button marked 'Key' on LILYGO T-Display S3
#define BOTTOM_BUTTON 0  // Button marked 'BOT' on LILYGO T-Display S3; not used in this sketch

// ESP32 LilyGo T-Display S3 Power On Pin (used to ensure the display is on when the device is running on battery)
#define PIN_POWER_ON 15

EspMQTTClient client(
  WiFiSSID,
  WIFIPassword,
  MQTTBroker,
  MQTTUser,
  MQTTPassword,
  MQTTClientName,
  MQTTPort);

String totalPageViews = "0";
String totalClicks = "0";
String totalEarnings = "$0.00";

TFT_eSPI tft = TFT_eSPI();

#define TFT_TURQUOISE 0x2F5C  // http://www.barth-dev.de/online/rgb565-color-picker/

int displayWidth = 320;
int displayHeight = 170;
int currentTextSize;

void setupSerialMonitor() {

  Serial.begin(115200);
  Serial.println("Starting " + String(ProgramName));
}

void setupButtons() {

  Serial.println("Setting up buttons");
  pinMode(TOP_BUTTON, INPUT_PULLUP);
  pinMode(BOTTOM_BUTTON, INPUT_PULLUP);
}

bool checkButton(int pinNumber) {

  bool returnValue = false;

  if (digitalRead(pinNumber) == 0) {

    delay(10);  // weed out false positives caused by debounce

    if (digitalRead(pinNumber) == 0) {

      // hold here until the button is released
      while (digitalRead(pinNumber) == 0)
        delay(10);

      returnValue = true;
    };
  };

  return returnValue;
}

void setupDisplay() {

  Serial.println("Setting up display");

  // turn on the T-Display screen when in battery mode
  pinMode(PIN_POWER_ON, OUTPUT);
  digitalWrite(PIN_POWER_ON, HIGH);

  // turn off the green LED
  pinMode(GREEN_LED, OUTPUT);
  digitalWrite(GREEN_LED, LOW);

  tft.init();
  tft.setRotation(1);
  tft.fillScreen(TFT_BLACK);
  tft.setTextColor(TFT_WHITE);
  
  currentTextSize = 2;
  tft.setTextSize(currentTextSize);
  int tw = (displayWidth - (int)tft.textWidth(ProgramName)) / 2;
  int th = (displayHeight - (int)tft.fontHeight()) / 2;
  tft.setCursor(tw, th);
  tft.println(String(ProgramName));

  currentTextSize = 3;
  tft.setTextSize(currentTextSize);
}

void updateDisplay() {

  int margin = 15;

  int tw;
  int fh = (int)tft.fontHeight();

  int leftMargin = margin;
  int rightMargin = displayWidth - margin;
  int topMargin = 2 * margin;
  int bottomMargin = displayHeight - fh - margin * 2;
  int midHeight = (displayHeight - fh) / 2;

  // clear the screen
  tft.fillScreen(TFT_BLACK);

  // display the Views line
  tft.setTextColor(TFT_BLUE);
  tft.setCursor(leftMargin, topMargin);
  tft.println("Views");

  tw = tft.textWidth(totalPageViews);
  tft.setCursor(rightMargin - tw, topMargin);
  tft.println(totalPageViews);

  // display the Clicks line
  tft.setTextColor(TFT_TURQUOISE);
  tft.setCursor(leftMargin, midHeight);
  tft.println("Clicks");

  tw = tft.textWidth(totalClicks);
  tft.setCursor(rightMargin - tw, midHeight);
  tft.println(totalClicks);

  // dispaly the Revenue line
  tft.setTextColor(TFT_GREEN);
  tft.setCursor(leftMargin, bottomMargin);
  tft.println("Earnings");

  tw = tft.textWidth(totalEarnings);
  tft.setCursor(rightMargin - tw, bottomMargin);
  tft.println(totalEarnings);
};

void DisplayTheIPAddress() {

  int margin = 15;

  int tw;
  int fh = (int)tft.fontHeight();

  int leftMargin = margin;
  int midHeight = (displayHeight - fh) / 2;

  String devicesIPAddress = WiFi.localIP().toString().c_str();

  // clear the screen
  tft.fillScreen(TFT_BLACK);

  // display the IP Address
  tft.setTextColor(TFT_WHITE);

  tft.setCursor(leftMargin, midHeight - fh);
  tft.println("IP address:");

  tft.setCursor(leftMargin, midHeight + fh);
  tft.println(devicesIPAddress);
}

void onConnectionEstablished() {

  client.subscribe("adsense/TotalPageViews", [](const String &payload) {
    totalPageViews = String(payload);
    updateDisplay();
  });

  client.subscribe("adsense/TotalClicks", [](const String &payload) {
    totalClicks = String(payload);
    updateDisplay();
  });

  client.subscribe("adsense/TotalEarnings", [](const String &payload) {
    totalEarnings = "$" + String(payload);
    updateDisplay();
  });

  client.publish("adsense/refreshrequest", "get last known values");
}

void setupWifi() {

  Serial.println("Setting up WiFi");

  // Enable debugging messages sent to serial output
  client.enableDebuggingMessages();

  // Enable Over The Air (OTA) updates
  client.enableOTA(OTAPassword, OTAPort);

  // once connected, subscribe for updates and request an update
  onConnectionEstablished();
}

void setup() {

  setupSerialMonitor();
  setupDisplay();
  setupButtons();
  setupWifi();
}

void loop() {

  client.loop();

  if (checkButton(TOP_BUTTON))
    client.publish("adsense/refreshrequest", "get current values from Google");

  if (checkButton(BOTTOM_BUTTON)) {
    DisplayTheIPAddress();
    delay(5000);
    updateDisplay();
  };
}
