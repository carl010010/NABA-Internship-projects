using System;
using ClosedXML;
using System.Collections.Generic;
using System.Text;
using ClosedXML.Excel;
using System.IO;
using System.Diagnostics;

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

            if (File.Exists(DATABASE_NAME))
            {
                workBook = new XLWorkbook(DATABASE_NAME);
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

            workBook.SaveAs(DATABASE_NAME);

            return true;
        }

        public PersonInfo FindMostRecentPerson()
        {
            string firstName, lastName;

            if (!((IDataBase)this).DataBaseFound(DATABASE_NAME))
                return null;

            var workBook = new XLWorkbook(DATABASE_NAME);
            var workSheet = workBook.Worksheet("Name");

            var row = workSheet.LastRowUsed();

            firstName = row.Cell(1).GetString();
            lastName = row.Cell(2).GetString();

            return new PersonInfo(firstName, lastName);
        }

        public List<string> FindAllFullNames()
        {
            Trace.WriteLine("Printing all names from excel sheet");

            XLWorkbook workBook;
            IXLWorksheet workSheet;

            if (!((IDataBase)this).DataBaseFound(DATABASE_NAME))
                return null;

            workBook = new XLWorkbook(DATABASE_NAME);
            workSheet = workBook.Worksheet("Name");

            var row = workSheet.FirstRow();

            List<string> fullNames = new List<string>();

            while (!row.Cell(1).IsEmpty())
            {
                string firstName = row.Cell(1).Value.ToString(), lastName = row.Cell(2).Value.ToString();

                //Trace.WriteLine(firstName + " " + lastName);
                fullNames.Add(firstName + " " + lastName);
                row = row.RowBelow();
            }
            return fullNames;
        }
    }
}
