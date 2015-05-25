using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DataBindingAppTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ObservableCollection<PersonModel> personModelCollection;
        public static string result="";

        public ObservableCollection<PersonModel> PersonModelCollection
        {
            get { return this.personModelCollection; }
        }

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

            //Task<string> t = DownloadPageAsync();

            //Task<string> t2 = GetJsonWeatherData(jsonResultTextBox, cityTextBox.Text);

            Debug.WriteLine("Downloading page...");
        }

        #region Handle Controller Events
        private void getWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            Task<string> t = GetJsonWeatherData(jsonResultTextBox, cityTextBox.Text);
            cityTextBlock.Text = "Current weather in " + Capitalize(cityTextBox.Text);
            //t.Start();
        }

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

                var response = await client.GetStringAsync(uri);

                c.Text = response.ToString();

                return response.ToString();
            }

        }

        #endregion

        #region DownloadPageAsync function
        static async Task<string> DownloadPageAsync()
        {
            #region Example Json

            //Uri dataUri = new Uri("http://api.openweathermap.org/data/2.5/weather?q=raunheim,de");

            #endregion

            #region Example Httpclient Webpage
            // ... Target page.
            Uri page = new Uri("http://api.openweathermap.org/data/2.5/weather?q=raunheim,de");

            // ... Use HttpClient.
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(page);
            IHttpContent content = response.Content;
            
            try
            {
                // ... Read the string.
                string result = await content.ReadAsStringAsync();

                // ... Display the result.
                if (result != null)
                {
                    Debug.WriteLine("Result: \n {0}", result);
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine("Exception: " + e.Message);
            }

            return result;
            
            #endregion

            #region Example 2 HttpClient Webpage
            //Uri uri = new Uri("http://example.com/datalist.aspx");
            //HttpClient httpClient = new HttpClient();

            //// Always catch network exceptions for async methods
            //try
            //{
            //    string result = await httpClient.GetStringAsync(uri);
            //    Debug.WriteLine(result);
            //}
            //catch(Exception e)
            //{
            //    // Details in ex.Message and ex.HResult. 
            //    Debug.WriteLine("Debug Message: " + e.Message);
            //}

            //// Once your app is done using the HttpClient object call dispose to 
            //// free up system resources (the underlying socket and memory used for the object)
            //httpClient.Dispose();

            #endregion

        }


        #endregion

        private void cityTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Debug.WriteLine("Test changed");
        }

        #region Capitalize
        /// <summary>
        /// Capitalize the first letter of a given word
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string Capitalize(string input)
        {
            string firstLetter = input[0].ToString().ToUpper();
            input = input.Remove(0, 1);
            input = input.Insert(0, firstLetter);
            return input;
        }
        #endregion
    }
}
