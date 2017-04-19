using Data;
using ExpansesManager.Core;
using Models.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ExpansesManager
{
    /// <summary>
    /// Interaction logic for MainGroups.xaml
    /// </summary>
    public partial class MainGroups : Window
    {
        public MainGroups()
        {
            InitializeComponent();
            var vm = new MainAppViewModel();
            User currentUser = AuthenticationManager.GetCurrentUser();
            using (var contex = new ExpansesManagerContext())
            {


                List<Group> items = new List<Group>();
                items.Add(new Group());
                //{ Name = "John Doe", Age = 42, Mail = "john@doe-family.com" });
                //items.Add(new Group()
                //{ Name = "Jane Doe", Age = 39, Mail = "jane@doe-family.com" });
                //items.Add(new Group()
                //{ Name = "Sammy Doe", Age = 7, Mail = "sammy.doe@gmail.com" });
                //lvUsers.ItemsSource = items;
            }

        }

        private void EditGroup_Click(object sender, RoutedEventArgs e)
        {
          
        
            EditModeMainApp ed = new EditModeMainApp();
            this.Close();
            ed.ShowDialog(); 
        }
    

        private void DeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            //RegisterWindow register = new RegisterWindow();
            //register.ShowDialog();

            //App.Current.MainWindow.Close();
        }
        private void Statisyic_Clik(object sender, RoutedEventArgs e)
        {
            MainGroups edit = new MainGroups();
            this.Close();
            edit.ShowDialog();
        }

             private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainApp edit = new MainApp();
            this.Close();
            edit.ShowDialog();
        }
    }
}




