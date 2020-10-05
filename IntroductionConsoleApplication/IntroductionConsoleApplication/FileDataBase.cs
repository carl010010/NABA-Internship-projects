/*******************************
*   Author: Carl Lowther
*   Date: 9/28/2020
*   Purpose: A simple PersonInfo writer and reader class.
*               Writes and reads to/from a Name.txt file
********************************/
using System.IO;

namespace IntroductionConsoleApplication
{
    public class FileDataBase
    {

        public void Write(PersonInfo person)
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Files");

            using (var file = new StreamWriter(Directory.GetCurrentDirectory() + "/Files/Name.txt"))
            {
                file.WriteLine(person.GetFirstName());
                file.WriteLine(person.GetLastName());
            }
        }


        public PersonInfo Read()
        {
            string firstName, lastName;
            using (var file = new StreamReader(Directory.GetCurrentDirectory() + "/Files/Name.txt"))
            {
                firstName = file.ReadLine();
                lastName = file.ReadLine();
            }

            return new PersonInfo(firstName, lastName);
        }
    }
}
