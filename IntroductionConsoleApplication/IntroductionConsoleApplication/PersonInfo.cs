/*******************************
*   Author: Carl Lowther
*   Date: 9/28/2020
*   Purpose: Holds first and last name strings for one person. 
********************************/

namespace IntroductionConsoleApplication
{
    public class PersonInfo
    {
        string m_firstName;
        string m_lastName;

        public PersonInfo(string firstName, string lastName)
        {
            m_firstName = firstName;
            m_lastName = lastName;
        }

        public string GetFullName()
        {
            return m_firstName + " " + m_lastName;
        }

        public string GetFirstName()
        {
            return m_firstName;
        }

        public string GetLastName() 
        {
            return m_lastName;
        }
    }
}
