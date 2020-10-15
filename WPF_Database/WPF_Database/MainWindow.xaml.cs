using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Database
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    public enum DatabaseType { TEXTFILE, EXCELFILE, COMPACTDATABASE};
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Submit_Clicked(object sender, RoutedEventArgs e)
        {
            DatabaseType databaseType;

            if (!Enum.TryParse<DatabaseType>(DataBaseChoice.SelectedValue.ToString(), out databaseType))
                return;

            IDataBase dataBase = null;

            switch (databaseType)
            {
                case DatabaseType.TEXTFILE:
                    dataBase = new TextDataBase("Names.txt");
                    break;
                case DatabaseType.EXCELFILE:
                    dataBase = new ExcelDatabase("Names.xlsx");
                    break;
                case DatabaseType.COMPACTDATABASE:
                    dataBase = new CompactDatabase("dbNABA.sdf");
                    break;
                default:
                    break;
            }

            


            if(dataBase != null)
            {
                PersonInfo person = new PersonInfo(TextBoxFirstName.Text, TextBoxLastName.Text);


                dataBase.Write(person);

                var temp = dataBase.FindMostRecentPerson();

                Trace.WriteLine(temp.GetFullName());


                List<string> allFullNames = dataBase.FindAllFullNames();

                ObservableCollection<string> oList;
                oList = new ObservableCollection<string>(allFullNames);
                PersonList.DataContext = oList;

                Binding binding = new Binding();
                PersonList.SetBinding(ListBox.ItemsSourceProperty, binding);
            }


            Trace.WriteLine(TextBoxFirstName.Text);
            Trace.WriteLine(TextBoxLastName.Text);
            Trace.WriteLine(DataBaseChoice.SelectedValue);

        }
    }
}
