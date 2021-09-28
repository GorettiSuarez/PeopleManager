using System.Net.Http;
using System.Text;

namespace PeopleManager.Model
{
    /// <summary>
    /// Provides methods to make operations on people.
    /// </summary>
    class PeopleModel : IPeopleModel
    {
        #region Constants

        /// <summary>
        /// The URI used to request all the people from the OData service.
        /// </summary>
        private const string REQUEST_PEOPLE_URI =
            "https://services.odata.org/TripPinRESTierService/(S(gle5si2wr2rlhhrv10x4yyu1))/People";


        #endregion

        #region Fields

        /// <summary>
        /// the HTTP client.
        /// </summary>
        HttpClient mHttpClient;

        #endregion

        #region Constructor

        public PeopleModel ()
        {
            mHttpClient = new HttpClient();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets all the people from the OData service.
        /// </summary>
        /// <returns>An object representing all the people.</returns>
        public string GetAllPeople ()
        {
            string allPeople = MakeHttpGetRequest(REQUEST_PEOPLE_URI);
            return allPeople;
        }

        /// <summary>
        /// Gets all the people that has the desired value in desired filter field.
        /// </summary>
        /// <param name="filter">The filter to use to filter the people.</param>
        /// <param name="desiredValue">The desired value in the filter field.</param>
        /// <returns></returns>
        public string GetFilteredPeople (string filter, string desiredValue)
        {
            string filterUri = REQUEST_PEOPLE_URI + $"?$filter={filter} eq '{desiredValue}'";
            string filteredPeople = MakeHttpGetRequest(filterUri);

            return filteredPeople;
        }

        /// <summary>
        /// Gets a single person with the desired name.
        /// </summary>
        /// <param name="fullName">The full name of the desired person.</param>
        /// <returns>An string representing a person.</returns>
        public string GetSinglePerson (string[] fullName)
        {
            string filteredPeople;

            string filterUri = REQUEST_PEOPLE_URI + $"?$filter=FirstName eq '{fullName[0]}' and LastName eq '{fullName[1]}'&$top=1";
            filteredPeople = MakeHttpGetRequest(filterUri);

            return filteredPeople;
        }

        /// <summary>
        /// Modifies the data of a single person.
        /// </summary>
        /// <param name="userName">The user name of the person to modify.</param>
        /// <param name="fields">The fields that the user want to modify.</param>
        /// <param name="newValues">The new desired values for the specified fields.</param>
        public void ModifySinglePerson(string userName, string[] fields, string[] newValues)
        {
            string jsonContent = BuildJson(fields, newValues);
            string patchUri = REQUEST_PEOPLE_URI + $"('{userName}')";
            MakeHttpPatchRequest(patchUri, jsonContent);
        }

        #endregion

        #region Private Methods



        /// <summary>
        /// Builds the JSON with all the desired fields and values.
        /// </summary>
        /// <param name="fields">The fields to modify.</param>
        /// <param name="newValues">The new values for those fields.</param>
        /// <returns></returns>
        private static string BuildJson(string[] fields, string[] newValues)
        {
            //string jsonContent = "header:\n{\nContent-Type: application/json\n}\nbody:\n{\n";
            string jsonContent = "{\n";

            for (int index = 0; index < fields.Length; index++)
            {
                jsonContent += $"    \"{fields[index]}\": \"{newValues[index]}\"\n";
            }

            jsonContent += "}";
            return jsonContent;
        }

        /// <summary>
        /// Makes HTTP GET request to the specify URI.
        /// </summary>
        /// <param name="uri">The desired URI.</param>
        /// <returns>The content of the response message as a string. It is empty if the request is not successfull.</returns>
        private string MakeHttpGetRequest (string uri)
        {
            string contentAsString;
            HttpResponseMessage responseMessage = mHttpClient.GetAsync(uri).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                contentAsString = responseMessage.Content.ReadAsStringAsync().Result;
            }
            else
            {
                string message = "Request operation not successful with error code: " + responseMessage.ReasonPhrase;
                throw new HttpRequestException(message);
            }

            return contentAsString;
        }

        /// <summary>
        /// Makes a HTTP PATCH request to the specify URI.
        /// </summary>
        /// <param name="uri">The desired URI.</param>
        /// <returns>The content of the response message as a string. It is empty if the request is not successfull.</returns>
        private void MakeHttpPatchRequest (string uri, string json)
        {
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri)
            { Content = httpContent };
            
            HttpResponseMessage responseMessage = mHttpClient.SendAsync(request).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                // Do nothing.
            }
            else
            {
                string message = "Request operation not successful with error code: " + responseMessage.ReasonPhrase;
                throw new HttpRequestException(message);
            }
        }

        #endregion
    }
}