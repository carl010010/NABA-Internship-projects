/*******************************
*   Author: Carl Lowther
*   Date: 9/28/2020
*   Purpose: A simple console app that reads in a first and last name
*               and creates a PersonInfo instance and then appends and
*               reads the first and last name from an excel file
********************************/
using System;

namespace Excel_File_Read_Write
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

            ExcelDataBase excelDataBase = new ExcelDataBase();
            excelDataBase.Write(person);
            PersonInfo filePerson = excelDataBase.Read();

            if (person.GetFullName() == filePerson.GetFullName())
            {
                Console.WriteLine("Your name is " + filePerson.GetFullName());
            }
            else
            {
                Console.WriteLine("Error! Name from file does not equal inputed name.");
            }




            excelDataBase.PrintAll();


            Console.WriteLine("Hit Enter to exit");
            Console.ReadLine();

        }
    }
}
