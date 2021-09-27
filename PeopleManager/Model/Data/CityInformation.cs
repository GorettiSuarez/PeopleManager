using Newtonsoft.Json;

namespace PeopleManager.Model.Data
{
    public class CityInformation
    {
        [JsonProperty("Name")]
        public string Name
        {
            get; set;
        }

        [JsonProperty("CountryRegion")]
        public string CountryRegion
        {
            get; set;
        }

        [JsonProperty("Region")]
        public string Region
        {
            get; set;
        }

        public override string ToString ()
        {
            string result = "-- Name: " + Name + "\n" +
                "-- Country region: " + CountryRegion + "\n" +
                "-- Region: " + Region + "\n";
            return result;
        }
    }
}