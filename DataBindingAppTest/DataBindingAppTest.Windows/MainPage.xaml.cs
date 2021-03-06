﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Net;
using Windows.Web.Http;
using System.Threading.Tasks;
using System.Text;
using System.Globalization;
using Windows.Data.Json;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace DataBindingAppTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region PersonModel
        private readonly ObservableCollection<PersonModel> personModelCollection;
        public static string result="";

        public ObservableCollection<PersonModel> PersonModelCollection
        {
            get { return this.personModelCollection; }
        }
        #endregion

        #region MainPage
        public MainPage()
        {

            #region PersonModel Region
            personModelCollection = new ObservableCollection<PersonModel>();

            personModelCollection.Add(new PersonModel("Max Mustermann", 30));
            personModelCollection.Add(new PersonModel("Yann", 31));
            personModelCollection.Add(new PersonModel("Loreen", 1));

            this.InitializeComponent();

            Debug.WriteLine("DataViewModel: " + PersonModelCollection);
            Debug.WriteLine("Count: " + "{0}", PersonModelCollection.Count());

            #endregion            
        }
        #endregion

        #region getWeatherButton_Click Event
        private void getWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Task<string> t = GetJsonWeatherData(jsonResultTextBox, cityTextBox.Text);
                string s = t.ToString();
            }
            catch (Exception ex)
            {

                throw ex; 
            }
           
        }

        #endregion

        #region resetButton_Click Event
        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            jsonResultTextBox.Text = "";
            cityMessageTextBlock.Text = "";
        }
        #endregion

        #region Async Method GetWeatherData

        public async Task<string> GetJsonWeatherData(TextBox c, string city)
        {
            HttpClient client = new HttpClient();

            // Form a valid uri with the entered city name
            StringBuilder sb = new StringBuilder("http://api.openweathermap.org/data/2.5/weather?q=");

            if (city == string.Empty)
            {
                c.Text = "";
                cityMessageTextBlock.Text = "Enter a city";
                return "";
            }
            else
            {
                Debug.WriteLine("City: {0}", city);

                sb.Append(city);               
                sb.Append(",de");
                
                string uriString = sb.ToString();
                Debug.WriteLine("URI: {0}", uriString);

                Uri uri = new Uri(uriString);

                string response = await client.GetStringAsync(uri);

                c.Text = response.ToString();

                return response;
            }
        }

        #endregion

        #region cityTextBox_TextChanged Event
        private void cityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Debug.WriteLine("Test changed");
        }
        #endregion

        #region Capitalize Method
        /// <summary>
        /// Capitalize the first letter of a given word
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string Capitalize(string input)
        {
            try
            {
                string firstLetter = input[0].ToString().ToUpper();
                input = input.Remove(0, 1);
                input = input.Insert(0, firstLetter);
                return input;
            }
            catch (Exception e)
            {

                Debug.WriteLine("Capitalize Exception: {0}", e);
            }

            return input;
            
        }
        #endregion

        #region parseButton_Click Event
        private void parseButton_Click(object sender, RoutedEventArgs e)
        {
            string nameKey = "name";
            string mainKey = "main";
            string tempKey = "temp";
            string weatherKey = "weather";
            string iconKey = "icon";

            string jsonData = jsonResultTextBox.Text;
            try
            {
                JsonObject jsonObject = JsonObject.Parse(jsonData);
                string city = jsonObject.GetNamedString(nameKey, "");
                JsonObject main = jsonObject.GetNamedObject(mainKey, null);
                JsonArray weather = jsonObject.GetNamedArray(weatherKey, new JsonArray());

                string icon = weather.GetObjectAt(0).GetNamedString(iconKey, "");

                if (main!=null)
                {
                    double currentTemperatur = main.GetNamedNumber(tempKey, 0);
                    cityTextBlock.Text = "Current weather in " + city;

                    double currentTempCelcius = currentTemperatur - 272;

                    currentTemperatureTextBlock.Text = Convert.ToInt32(currentTempCelcius).ToString();
                    unitTextBlock.Text = "°C";

                    // place the right weather icon
                    ChooseTheRightWeatherIcon(icon);

                    Debug.WriteLine("Current weather: {0}K", currentTemperatur);
                    Debug.WriteLine("Current weather in celcius: {0}°C", currentTemperatur-272.15);
                }
                
            }
            catch(FormatException formatException)
            {
                Debug.WriteLine("EXCEPTION:\n {0}", formatException.Message);
            }

            catch (Exception ex) 
            {
                throw ex;
            }

        }

        #endregion

        #region ChooseTheRightWeatherIcon Method
        // Choose the right weather icon for the current weather
        public void ChooseTheRightWeatherIcon(string jsonIconCode)
        {
            StringBuilder stringBuilder = new StringBuilder("/Assets/Weather Icons/");
            stringBuilder.Append(jsonIconCode);
            stringBuilder.Append(".png");
            string imageSource = stringBuilder.ToString();

            Debug.WriteLine("Image source: " + imageSource);
            //
            Image imageObject = new Image();
            currentWeatherStackPanel.Children.Add(imageObject);
            BitmapImage bitmapImage = null;
            if (imageObject != null)
            {
                bitmapImage = new BitmapImage();
                Uri uri = new Uri(BaseUri, imageSource);
                bitmapImage.UriSource = uri;
            }
            
            switch (jsonIconCode)
            {
                case "01d":
                case "01n":
                    imageObject.Source = bitmapImage;
                    break;

                case "02d":
                case "02n":
                    imageObject.Source = bitmapImage;
                    break;

                case "03d":
                case "03n":
                    imageObject.Source = bitmapImage;
                    break;

                case "04d":
                case "04n":
                    imageObject.Source = bitmapImage;
                    break;

                case "09d":
                case "09n":
                    imageObject.Source = bitmapImage;
                    break;

                case "10d":
                case "10n":
                    imageObject.Source = bitmapImage;
                    break;

                case "11d":
                case "11n":
                    imageObject.Source = bitmapImage;
                    break;

                case "13d":
                case "13n":
                    imageObject.Source = bitmapImage;
                    break;

                default:
                    bitmapImage.UriSource = new Uri("ms-appx:///Assets/Weather Icons/none.png");
                    imageObject.Source = bitmapImage;
                    break;
            }
        }
        #endregion
    }
}
