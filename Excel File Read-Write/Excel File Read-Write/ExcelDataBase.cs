/*******************************
*   Author: Carl Lowther
*   Date: 9/29/2020
*   Purpose: A simple PersonInfo writer and reader class.
*               Writes and reads to/from a Names.xlsx exel file
*   
********************************/


using ClosedXML.Excel;
using System;
using System.IO;

namespace Excel_File_Read_Write
{
    class ExcelDataBase
    {
        //Append a single person to the Names.xlsx exel sheet 
        public void Write(PersonInfo person)
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
        }

        //Read the most recent name from Names.xlsx and return a PersonInfo object
        public PersonInfo Read()
        {
            string firstName, lastName;

            var workBook = new XLWorkbook("Names.xlsx");
            var workSheet = workBook.Worksheet("Name");

            var row = workSheet.LastRowUsed();

            firstName = row.Cell(1).GetString();
            lastName = row.Cell(2).GetString();

            return new PersonInfo(firstName, lastName);
        }


        //Print all names in Names.xlsx to the console
        public void PrintAll ()
        {
            Console.WriteLine("Printing all names from excel sheet");

            XLWorkbook workBook;
            IXLWorksheet workSheet;

            if (File.Exists("Names.xlsx"))
            {
                workBook = new XLWorkbook("Names.xlsx");
                workSheet = workBook.Worksheet("Name");

                var row = workSheet.FirstRow();

                while(!row.Cell(1).IsEmpty())
                {
                    string firstName = row.Cell(1).Value.ToString(), lastName = row.Cell(2).Value.ToString();

                    Console.WriteLine(firstName + " " + lastName);
                    row = row.RowBelow();
                }
            }
        }
    }
}
