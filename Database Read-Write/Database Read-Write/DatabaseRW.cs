/*******************************
*   Author: Carl Lowther
*   Date: 9/29/2020
*   Purpose: A simple class that can read and write first and last
*               names to an SQL Compact database
*   
********************************/

using System;
using System.Data.SqlServerCe;
using System.IO;

namespace Database_Read_Write
{
    class DatabaseRW
    {
        const string DATABASE_NAME = "dbNABA.sdf";
        const string CONNECTION_STR = "Data Source = " + DATABASE_NAME + ";";

        //List of SQL commands
        const string WRITE_CMD = "INSERT INTO Person (FirstName, LastName) VALUES (@FirstName,@LastName)";
        const string READALL_CMD = "SELECT FirstName, LastName from Person";
        const string READ_NEWEST_CMD = "SELECT TOP 1 FirstName, LastName from Person ORDER BY PersonID DESC";


        //Insert a single person to the Person table in the database 
        public void Write(PersonInfo person)
        {
            if (!DataBaseFound(DATABASE_NAME))
                return;


            using (SqlCeConnection conn = new SqlCeConnection(CONNECTION_STR))
            {
                conn.Open();
                using (SqlCeCommand cmd = new SqlCeCommand(WRITE_CMD, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", person.GetFirstName());
                    cmd.Parameters.AddWithValue("@LastName", person.GetLastName());

                    int result = cmd.ExecuteNonQuery();

                    if(result < 0)
                        Console.WriteLine("Error inserting data into database");
                }
            } 
        }

        //Read the most recent name from the Person table in the database and return a PersonInfo object
        public PersonInfo Read()
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
                        if(rdr.Read())
                        {
                            firstName = rdr[0].ToString();
                            lastName = rdr[1].ToString();
                        }
                    }
                }
            }

            return new PersonInfo(firstName, lastName);
        }


        //Print all names from the Person table in the databas
        public void PrintAll()
        {
            Console.WriteLine("Printing all names from database");
            
            if (!DataBaseFound(DATABASE_NAME))
                return;

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
