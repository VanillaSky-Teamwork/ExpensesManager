using System;
using System.Linq;
using System.Windows;
using Data;
using ExpansesManager.Core;
using Models.Models;

namespace ExpansesManager
{
	/// <summary>
	/// Interaction logic for MainApp.xaml
	/// </summary>
	public partial class MainApp : Window
	{
		public MainApp()
		{
			InitializeComponent();

			User user = AuthenticationManager.GetCurrentUser();
			using (var contex = new ExpansesManagerContext())
			{
				textBox.Text = "Welcome " +" "+  user.Username;
			}

		}
		protected void Page_Load(object sender, EventArgs e)
		{


		}

        private void textBox1_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello!");
        }
    }
}
