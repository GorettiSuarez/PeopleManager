using Newtonsoft.Json;

namespace PeopleManager.Model.Data
{
    public class AddressInformation
    {
        [JsonProperty("Address")]
        public string Address
        {
            get; set;
        }

        [JsonProperty("City")]
        public CityInformation City
        {
            get; set;
        }

        public override string ToString ()
        {
            string result = "- Addres: " + Address + "\n" +
                "- City: \n" + City + "\n";
            return result;
        }
    }
}