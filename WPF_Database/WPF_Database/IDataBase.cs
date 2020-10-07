using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Database
{
    interface IDataBase
    {
        bool Write(PersonInfo person);

        PersonInfo ReadMostRecent();

        bool PrintAll();
    }
}
