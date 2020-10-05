/*******************************
*   Author: Carl Lowther
*   Date: 9/28/2020
*   Purpose: A simple console app that reads in a first and last name
*               and creates a PersonInfo instance and then appends and
*               reads the first and last name from an excel file
********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Read_Write
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            PersonInfo person = new PersonInfo(firstName, lastName);

            Console.WriteLine("Your name is " + person.GetFullName());

            DatabaseRW database = new DatabaseRW();
            database.Write(person);
            PersonInfo filePerson = database.Read();

            if (person.GetFullName() == filePerson.GetFullName())
            {
                Console.WriteLine("Your name is " + filePerson.GetFullName());
            }
            else
            {
                Console.WriteLine("Error! Name from database11 does not equal inputed name.");
            }


            database.PrintAll();


            Console.WriteLine("Hit Enter to exit");
            Console.ReadLine();

        }
    }
}
