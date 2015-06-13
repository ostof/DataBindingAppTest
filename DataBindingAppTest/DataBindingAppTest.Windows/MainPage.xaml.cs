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
using Windows.Data.Json;

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
            //TODO: mettre ceci dans un try catch event et si le task reussi, afficher un message à l'utilisateur
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

                // Parsing weather data from Json
                try
                {                   
                    JsonObject jsonObject = JsonObject.Parse(response);
                    //Debug.WriteLine("JsonObject: -----------\n {0}", jsonObject);

                    //string City = jsonObject.GetNamedString("name");
                    //Debug.WriteLine("City from json:.................... \n {0}", City);
                }
                catch (Exception ex)
                {

                    throw ex;
                }


                c.Text = response.ToString();
                //Debug.WriteLine("tostring: -----------\n {0}", response.ToString());

                return response;
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

        private void parseButton_Click(object sender, RoutedEventArgs e)
        {
            //cityTextBlock.Text = "Current weather in " + Capitalize(cityTextBox.Text);
            string nameKey = "name";
            string mainKey = "main";
            string tempKey = "temp";

            string jsonData = jsonResultTextBox.Text;
            try
            {
                JsonObject jo = JsonObject.Parse(jsonData);
                string city = jo.GetNamedString(nameKey, "");
                JsonObject main = jo.GetNamedObject(mainKey, null);

                //TODO Ajouter la fonction des icones

                if (main!=null)
                {
                    double currentTemperatur = main.GetNamedNumber(tempKey, 0);
                    cityTextBlock.Text = "Current weather in " + city;

                    double currentTempCelcius = currentTemperatur - 272;

                    currentTemperatureTextBlock.Text = Convert.ToInt32(currentTempCelcius).ToString();
                    unitTextBlock.Text = "°C";

                    Debug.WriteLine("Current weather: {0}K", currentTemperatur);
                    Debug.WriteLine("Current weather in celcius: {0}°C", currentTemperatur-272.15);
                }
                
                //Debug.WriteLine("")
            }
            catch (Exception ex) 
            {

                throw ex;
            }

        }
    }
}
