using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WPF_Database
{
    interface IDataBase
    {
        bool Write(PersonInfo person);

        PersonInfo FindMostRecentPerson();

        List<string> FindAllFullNames();

        public bool DataBaseFound(string databaseName)
        {
            if (File.Exists(databaseName))
                return true;

            Trace.WriteLine("Database could not be found");

            return false;
        }
    }
}
