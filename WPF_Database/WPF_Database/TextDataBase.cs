using DocumentFormat.OpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            using (var file = new StreamWriter(DATABASE_NAME, true))
            {
                file.WriteLine(person.GetFirstName());
                file.WriteLine(person.GetLastName());
            }

            return true;
        }

        public PersonInfo FindMostRecentPerson()
        {
            if (!((IDataBase)this).DataBaseFound(DATABASE_NAME))
                return null;
            

            string fTemp, lTemp, firstName = null, lastName = null;
            using (var file = new StreamReader(DATABASE_NAME))
            {
                while ((fTemp = file.ReadLine()) != null)
                {
                    if ((lTemp = file.ReadLine()) != null)
                    {
                        firstName = fTemp;
                        lastName = lTemp;
                    }
                }
            }

            if (firstName == null || lastName == null)
                return null;

            return new PersonInfo(firstName, lastName);
        }

        public List<string> FindAllFullNames()
        {
            if (!((IDataBase)this).DataBaseFound(DATABASE_NAME))
                return null;

            string firstName, lastName;

            List<string> fullNames = new List<string>();

            using (var file = new StreamReader(DATABASE_NAME))
            {
                while((firstName = file.ReadLine()) != null)
                {
                    if((lastName = file.ReadLine()) != null)
                    {
                        fullNames.Add(firstName + " " + lastName);
                    }
                }
            }

            return fullNames;
        }
    }
}
