using MvxStarter.Core.DataAccess;
using MvxStarter.Core.Models;
using MvxStarter.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CLITesting
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PersonModel> people;
            people = PersonDA.GetListOfPeople("");
            foreach(PersonModel person in people)
            {
                Console.WriteLine(person.Name);
            }
        Console.ReadLine();
        }
    }
}