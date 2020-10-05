/*******************************
*   Author: Carl Lowther
*   Date: 9/28/2020
*   Purpose: A simple console app that reads in a first and last name
*               and creates a PersonInfo instance and then writes and
*               reads the first and last name from a text file
********************************/
using System;

namespace IntroductionConsoleApplication
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Write("Entert First Name: ");

            string firstName = Console.ReadLine();

            Console.Write("Entert Last Name: ");

            string lastName = Console.ReadLine();

            PersonInfo person = new PersonInfo(firstName, lastName);

            Console.WriteLine("Your name is " + person.GetFullName());

            FileDataBase fileDataBase = new FileDataBase();

            fileDataBase.Write(person);

            PersonInfo filePerson = fileDataBase.Read();

            if(person.GetFullName() == filePerson.GetFullName())
            {
                Console.WriteLine("Your name is " + filePerson.GetFullName());
            }
            else
            {
                Console.WriteLine("Error! Name from file does not equal inputed name.");
            }


            Console.WriteLine("Hit Enter to exit");
            Console.ReadLine();

        }
    }
}
