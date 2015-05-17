using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace DataBindingAppTest
{

    public class WeatherData
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }

        public WeatherData() { }
    }

    public class WeatherTemperatureData
    {
        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double pressure { get; set; }
        public double sea_level { get; set; }
        public double grnd_level { get; set; }
        public int humidity { get; set; }

        public WeatherTemperatureData() { }
    }


    #region JsonDataSource Class
    //class JsonDataSource
    //{
    //    private static JsonDataSource jsonDataSource = new JsonDataSource();
    //    public JsonDataSource()
    //    {
    //        await jsonDataSource.GetSampleDataAsync("ms-appx:///WeatherSampleData.json");
    //    }

    //    public async Task GetSampleDataAsync(string jsonFileUri)
    //    {
    //        Uri fileUri = new Uri(jsonFileUri);

            




    //        JsonValue jsonValue = JsonValue.Parse(jsonFileUri);
    //    }
    //}
    #endregion
}
