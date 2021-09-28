using Newtonsoft.Json;
using PeopleManager.Model;
using PeopleManager.Model.Data;
using System;
using System.Net.Http;
using Unity;

namespace PeopleManager
{
    class Program
    {
        /// <summary>
        /// Initializes the console application.
        /// </summary>
        /// <param name="args"></param>
        static void Main (string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IPeopleModel, PeopleModel>();

            IPeopleModel peopleModel = container.Resolve<PeopleModel>();

            while (true)
            {
                ShowMainMenuOptions();

                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
                Console.WriteLine("\n");

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.D1:
                        ShowAllPeople(peopleModel);
                        break;
                    case ConsoleKey.D2:
                        FilterPeople(peopleModel);
                        break;
                    case ConsoleKey.D3:
                        ShowOnePersonDetails(peopleModel);
                        break;
                    default:
                        Console.WriteLine($"The key {consoleKeyInfo.Key} does not belong to a valid option.");
                        break;
                }

                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Shows in the console the main options the user can select.
        /// </summary>
        private static void ShowMainMenuOptions()
        {
            Console.WriteLine("What do you want to do? (Write down the number of the desired option): \n");
            Console.WriteLine("1.- Show all persons");
            Console.WriteLine("2.- Filter people.");
            Console.WriteLine("3.- Show person details.");
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Shows the details of one person after the user provides its full name.
        /// </summary>
        /// <param name="peopleModel">The people model to get the person from the OData API.</param>
        private static void ShowOnePersonDetails(IPeopleModel peopleModel)
        {
            Console.WriteLine("Write down the full name of the person (e.g. Ronald Mundy): \n");
            string[] fullName = Console.ReadLine().Split(' ');

            string personAsString = peopleModel.GetSinglePerson(fullName);

            People peopleWithOnePerson = JsonConvert.DeserializeObject<People>(personAsString);
            Console.WriteLine(peopleWithOnePerson);
        }

        /// <summary>
        /// Shows the details of one person after the user provides its field to filter and its desired value.
        /// </summary>
        /// <param name="peopleModel">The people model to get the person from the OData API.</param>
        private static void FilterPeople(IPeopleModel peopleModel)
        {
            Console.WriteLine("Write down the field which you want to use to filter the people.\n" +
                "Available filters are: UserName, FirstName, LastName, MiddleName, Gender and Age.\n");
            string filter = Console.ReadLine();

            Console.WriteLine("\nWrite down the desired value for that field to filter the people\n");
            string desiredValue = Console.ReadLine();

            try
            {
                string filteredPeopleAsString = peopleModel.GetFilteredPeople(filter, desiredValue);
                People filteredPeople = JsonConvert.DeserializeObject<People>(filteredPeopleAsString);
                Console.WriteLine(filteredPeople);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Invalid request. Try again.");
            }
        }

        /// <summary>
        /// Shows the details of all the people that can be retrieved from the OData service.
        /// </summary>
        /// <param name="peopleModel">The people model to get the people from the OData API.</param>
        private static void ShowAllPeople(IPeopleModel peopleModel)
        {
            string allPeopleAsString = peopleModel.GetAllPeople();
            People allPeople = JsonConvert.DeserializeObject<People>(allPeopleAsString);
            Console.WriteLine(allPeople);
        }
    }
}
