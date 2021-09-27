using Newtonsoft.Json;
using System.Collections.Generic;

namespace PeopleManager.Model.Data
{
    public class Person
    {
        [JsonProperty("UserName")]
        public string UserName
        {
            get; set;
        }

        [JsonProperty("FirstName")]
        public string FirstName
        {
            get; set;
        }

        [JsonProperty("LastName")]
        public string LastName
        {
            get; set;
        }

        [JsonProperty("MiddleName")]
        public string MiddleName
        {
            get; set;
        }

        [JsonProperty("Gender")]
        public string Gender
        {
            get; set;
        }

        [JsonProperty("Age")]
        public int? Age
        {
            get; set;
        }

        [JsonProperty("Emails")]
        public List<string> Emails
        {
            get; set;
        }

        [JsonProperty("FavoriteFeature")]
        public string FavoriteFeature
        {
            get; set;
        }

        [JsonProperty("Features")]
        public List<string> Features
        {
            get; set;
        }

        [JsonProperty("AddressInfo")]
        public List<AddressInformation> AddressInfo
        {
            get; set;
        }

        [JsonProperty("HomeAddress")]
        public AddressInformation HomeAddress
        {
            get; set;
        }

        public override string ToString ()
        {
            string personAsString =
                "User name: " + UserName + "\n" +
                "First name: " + FirstName + "\n" +
                "Last name: " + LastName + "\n" +
                "Middle name: " + MiddleName + "\n" +
                "Gender: " + Gender + "\n" +
                "Age: " + Age + "\n" +
                "Emails:\n";

            foreach (string email in Emails)
            {
                personAsString += "    " + email + "\n";
            }

            personAsString +=
                "Favorite feature: " + FavoriteFeature + "\n" +
                "Features:\n";

            foreach (string feature in Features)
            {
                personAsString += "    " + feature + "\n";
            }

            personAsString += "Address information:\n";
            foreach (AddressInformation addressInfo in AddressInfo)
            {
                personAsString += "    " + addressInfo + "\n";
            }

            personAsString += "Home adress:\n" + HomeAddress;
            return personAsString;
        }
    }
}
