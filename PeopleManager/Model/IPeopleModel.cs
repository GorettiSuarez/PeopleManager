namespace PeopleManager.Model
{
    /// <summary>
    /// Provides methods to make operations on people.
    /// </summary>
    public interface IPeopleModel
    {
        /// <summary>
        /// Gets all the people from the OData service.
        /// </summary>
        /// <returns>A string representing all the people.</returns>
        string GetAllPeople ();

        /// <summary>
        /// Gets all the people that has the desired value in desired filter field.
        /// </summary>
        /// <param name="filter">The filter to use to filter the people.</param>
        /// <param name="desiredValue">The desired value in the filter field.</param>
        /// <returns>A string representing the filtered people.</returns>
        string GetFilteredPeople (string filter, string desiredValue);

        /// <summary>
        /// Gets a single person with the desired name.
        /// </summary>
        /// <param name="fullName">The full name of the desired person.</param>
        /// <returns>An string representing a person.</returns>
        string GetSinglePerson (string[] fullName);

        /// <summary>
        /// Modifies the data of a single person.
        /// </summary>
        /// <param name="userName">The user name of the person to modify.</param>
        /// <param name="fields">The fields that the user want to modify.</param>
        /// <param name="newValues">The new desired values for the specified fields.</param>
        void ModifySinglePerson(string userName, string[] fields, string[] newValues);
    }
}
