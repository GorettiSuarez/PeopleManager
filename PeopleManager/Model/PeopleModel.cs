using System.Net.Http;

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
            "https://services.odata.org/TripPinRESTierService/People";


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
            string allPeople = MakeHttpRequest(REQUEST_PEOPLE_URI);
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
            string filteredPeople = MakeHttpRequest(filterUri);

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
            filteredPeople = MakeHttpRequest(filterUri);

            return filteredPeople;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Makes HTTP request to the specify URI.
        /// </summary>
        /// <param name="uri">The desired URI.</param>
        /// <returns>The content of the response message as a string. It is empty if the request is not successfull.</returns>
        private string MakeHttpRequest (string uri)
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

        #endregion
    }
}