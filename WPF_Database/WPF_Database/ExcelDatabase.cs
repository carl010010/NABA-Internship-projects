using System;
using ClosedXML;
using System.Collections.Generic;
using System.Text;
using ClosedXML.Excel;
using System.IO;

namespace WPF_Database
{
    class ExcelDatabase : IDataBase
    {
        readonly string DATABASE_NAME;

        public ExcelDatabase(string database_name)
        {
            DATABASE_NAME = database_name;
        }

        public bool Write(PersonInfo person)
        {
            XLWorkbook workBook;
            IXLWorksheet workSheet;
            IXLRow row;

            if (File.Exists("Names.xlsx"))
            {
                workBook = new XLWorkbook("Names.xlsx");
                workSheet = workBook.Worksheet("Name");
                row = workSheet.LastRowUsed().RowBelow();
            }
            else
            {
                workBook = new XLWorkbook();
                workSheet = workBook.Worksheets.Add("Name");
                row = workSheet.FirstRow();
            }


            row.Cell(1).SetValue(person.GetFirstName());
            row.Cell(2).SetValue(person.GetLastName());

            workBook.SaveAs("Names.xlsx");

            return true;
        }

        public PersonInfo ReadMostRecent()
        {
            string firstName, lastName;

            var workBook = new XLWorkbook("Names.xlsx");
            var workSheet = workBook.Worksheet("Name");

            var row = workSheet.LastRowUsed();

            firstName = row.Cell(1).GetString();
            lastName = row.Cell(2).GetString();

            return new PersonInfo(firstName, lastName);
        }

        public bool PrintAll()
        {
            Console.WriteLine("Printing all names from excel sheet");

            XLWorkbook workBook;
            IXLWorksheet workSheet;

            if (!File.Exists("Names.xlsx"))
                return false;

            workBook = new XLWorkbook("Names.xlsx");
            workSheet = workBook.Worksheet("Name");

            var row = workSheet.FirstRow();

            while (!row.Cell(1).IsEmpty())
            {
                string firstName = row.Cell(1).Value.ToString(), lastName = row.Cell(2).Value.ToString();

                Console.WriteLine(firstName + " " + lastName);
                row = row.RowBelow();
            }
            return true;
        }
    }
}
