using Newtonsoft.Json;
using PeopleManager.Model.Data;
using System;
using System.Net.Http;

namespace PeopleManager
{
    class Program
    {
        static void Main (string[] args)
        {
            HttpClient httpClient = new HttpClient();
            const string REQUEST_PEOPLE_URI = "https://services.odata.org/TripPinRESTierService/People";

            HttpResponseMessage responseMessage = httpClient.GetAsync(REQUEST_PEOPLE_URI).Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                string responseAsString = responseMessage.Content.ReadAsStringAsync().Result;

                People allPeople = JsonConvert.DeserializeObject<People>(responseAsString);
                Console.WriteLine(allPeople);
            }
            else
            {
                Console.WriteLine("Request operation not successful with error code: " + responseMessage.ReasonPhrase);
            }

            Console.ReadLine();
        }
    }
}
