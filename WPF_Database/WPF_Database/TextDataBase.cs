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
            throw new NotImplementedException();

            using (var file = new StreamWriter(DATABASE_NAME))
            {
                file.WriteLine(person.GetFirstName());
                file.WriteLine(person.GetLastName());
            }
        }

        public PersonInfo FindMostRecentPerson()
        {
            if (!((IDataBase)this).DataBaseFound(DATABASE_NAME))
                return null;
            
            throw new NotImplementedException();

            string firstName, lastName;
            using (var file = new StreamReader(DATABASE_NAME))
            {
                firstName = file.ReadLine();
                lastName = file.ReadLine();
            }

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

        public static IEnumerable<string> ReadLastLines(string path, int count)
        {
            if (count < 1)
                return Enumerable.Empty<string>();

            var queue = new Queue<string>(count);

            foreach (var line in File.ReadLines(path))
            {
                if (queue.Count == count)
                    queue.Dequeue();

                queue.Enqueue(line);
            }

            return queue;
        }
    }
}
