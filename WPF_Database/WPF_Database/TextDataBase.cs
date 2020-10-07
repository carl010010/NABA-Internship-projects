using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WPF_Database
{
    class TextDataBase : IDataBase
    {
        readonly string DATABASE_NAME;

        public TextDataBase(string database_name)
        {
            DATABASE_NAME = database_name;
        }

        public bool Write(PersonInfo person)
        {
            throw new NotImplementedException(); 

            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Files");

            using (var file = new StreamWriter(Directory.GetCurrentDirectory() + "/Files/Name.txt"))
            {
                file.WriteLine(person.GetFirstName());
                file.WriteLine(person.GetLastName());
            }
        }

        public PersonInfo ReadMostRecent()
        {
            throw new NotImplementedException();

            string firstName, lastName;
            using (var file = new StreamReader(Directory.GetCurrentDirectory() + "/Files/Name.txt"))
            {
                firstName = file.ReadLine();
                lastName = file.ReadLine();
            }

            return new PersonInfo(firstName, lastName);
        }

        public bool PrintAll()
        {
            throw new NotImplementedException();
        }
    }
}
