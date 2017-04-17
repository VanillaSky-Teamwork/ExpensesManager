using Data;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }



        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.ShowDialog();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new ExpansesManagerContext())
            {
                if(PasswordTextBox.Text == RepeatedPasswordTextBox.Text)
                {
                    User user = new User()
                    {
                        Username = UsernameTextBox.Text,
                        Password = PasswordTextBox.Text,
                        Email = EmailtextBox.Text,
                    };

                    context.Users.Add(user);
                    context.SaveChanges();
                    MessageBox.Show("Successfully registered!");
                }
            }
        }
    }
}
