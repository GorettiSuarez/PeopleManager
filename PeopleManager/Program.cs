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
        static void Main (string[] args)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IPeopleModel, PeopleModel>();

            IPeopleModel peopleModel = container.Resolve<PeopleModel>();

            Console.WriteLine("What do you want to do? (Write down the number of the desired option): \n");
            Console.WriteLine("1.- Show all persons");
            Console.WriteLine("2.- Find one person.");          
            Console.WriteLine("3.- Show person details.");          
            Console.WriteLine("\n");            

            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            Console.WriteLine("\n");

            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.D1:
                    string allPeopleAsString = peopleModel.GetAllPeople();
                    People allPeople = JsonConvert.DeserializeObject<People>(allPeopleAsString);
                    Console.WriteLine(allPeople);
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Write down the field which you want to use to filter the people\n");
                    string filter = Console.ReadLine();

                    Console.WriteLine("Write down the desired value for that field to filter the people\n");
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
                    
                    break;
                case ConsoleKey.D3:
                    Console.WriteLine("Write down the full name of the person (e.g. Ronald Mundy): \n");
                    string[] fullName = Console.ReadLine().Split(' ');

                    string personAsString = peopleModel.GetSinglePerson(fullName);

                    People peopleWithOnePerson = JsonConvert.DeserializeObject<People>(personAsString);
                    Console.WriteLine(peopleWithOnePerson);
                    break;
                default:
                    Console.WriteLine($"The key {consoleKeyInfo.Key} does not belong to a valid option.");
                    break;
            }

            Console.ReadLine();
        }
    }
}
