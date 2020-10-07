using System;
using System.Data.SqlServerCe;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WPF_Database
{
    class CompactDatabase : IDataBase
    {
        readonly string DATABASE_NAME;
        readonly string CONNECTION_STR;

        //List of SQL commands
        const string WRITE_CMD = "INSERT INTO Person (FirstName, LastName) VALUES (@FirstName,@LastName)";
        const string READALL_CMD = "SELECT FirstName, LastName from Person";
        const string READ_NEWEST_CMD = "SELECT TOP 1 FirstName, LastName from Person ORDER BY PersonID DESC";



        public CompactDatabase(string database_name)
        {
            DATABASE_NAME = database_name;
            CONNECTION_STR = "Data Source = " + DATABASE_NAME + ";";
        }

        public bool Write(PersonInfo person)
        {
            if (!DataBaseFound(DATABASE_NAME))
                return false;


            using (SqlCeConnection conn = new SqlCeConnection(CONNECTION_STR))
            {
                conn.Open();
                using (SqlCeCommand cmd = new SqlCeCommand(WRITE_CMD, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", person.GetFirstName());
                    cmd.Parameters.AddWithValue("@LastName", person.GetLastName());

                    int result = cmd.ExecuteNonQuery();

                    if (result < 0)
                    {
                        Console.WriteLine("Error inserting data into database");
                        return false;
                    }
                }
            }

            return true;
        }

        public PersonInfo ReadMostRecent()
        {
            if (!DataBaseFound(DATABASE_NAME))
                return null;

            string firstName = null, lastName = null;

            using (SqlCeConnection conn = new SqlCeConnection(CONNECTION_STR))
            {
                conn.Open();
                using (SqlCeCommand cmd = new SqlCeCommand(READ_NEWEST_CMD, conn))
                {
                    using (SqlCeDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            firstName = rdr[0].ToString();
                            lastName = rdr[1].ToString();
                        }
                    }
                }
            }

            return new PersonInfo(firstName, lastName);
        }

        public bool PrintAll()
        {
            Console.WriteLine("Printing all names from database");

            if (!DataBaseFound(DATABASE_NAME))
                return false;

            string firstName = null, lastName = null;

            using (SqlCeConnection conn = new SqlCeConnection(CONNECTION_STR))
            {
                conn.Open();
                using (SqlCeCommand cmd = new SqlCeCommand(READALL_CMD, conn))
                {
                    using (SqlCeDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            firstName = rdr[0].ToString();
                            lastName = rdr[1].ToString();
                            Console.WriteLine(firstName + " " + lastName);
                        }
                    }
                }
            }

            return true;
        }

        bool DataBaseFound(string databaseName)
        {
            if (File.Exists(databaseName))
                return true;

            Console.WriteLine("Database could not be found");

            return false;
        }
    }
}
