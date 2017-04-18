using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using Data;
using ExpansesManager.Core;
using ExpansesManager.ViewModels;
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
            var vm = new MainAppViewModel();
			User currentUser = AuthenticationManager.GetCurrentUser();
			using (var contex = new ExpansesManagerContext())
			{
				textBox.Text = "Welcome " +" "+ currentUser.Username;
                var user = contex.Users.Find(currentUser.Id);
                vm.Groups = new ObservableCollection<GroupViewModel>(Mapper.Instance.Map<IEnumerable<Group>, ObservableCollection<GroupViewModel>>(user.Groups));
			    this.GroupsGrid.ItemsSource = vm.Groups;
			}
		    this.GroupsGrid.RowEditEnding += OnRowEditEnding;
		}

	    private void OnRowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
	    {
	        var s = sender as DataGrid;
	        var row = e.Row;
	        var group = e.Row.DataContext as GroupViewModel;

	        if (e.EditAction == DataGridEditAction.Commit)
	        {
                
                using (var db = new ExpansesManagerContext())
                {
                    User currentUser = AuthenticationManager.GetCurrentUser();
                    var user = db.Users.Find(currentUser.Id);
                    bool doesGroupAlreadyExist = user.Groups.Any(g => g.Name.Equals(group.Name));
                    if (doesGroupAlreadyExist)
                    {
                        
                    }
                    else
                    {
                        user.Groups.Add(Mapper.Instance.Map<GroupViewModel, Group>(group));
                        db.SaveChanges();
                    }
                }   
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
