using Newtonsoft.Json;
using System.Collections.Generic;

namespace PeopleManager.Model.Data
{
    class People
    {
        [JsonProperty("@odata.context")]
        public string OdataContext
        {
            get; set;
        }

        [JsonProperty("value")]
        public List<Person> AllPeople
        {
            get; set;
        }

        public override string ToString ()
        {
            string result = "The list of people:\n";

            foreach (Person person in AllPeople)
            {
                result = result + person.ToString() + "\n";
            }
            return result;
        }
    }
}
