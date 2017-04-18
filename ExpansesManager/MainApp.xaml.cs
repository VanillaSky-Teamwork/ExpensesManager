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
                var user = contex.Users.Find(currentUser.Id);
                vm.Groups = new ObservableCollection<GroupViewModel>(Mapper.Instance.Map<IEnumerable<Group>, ObservableCollection<GroupViewModel>>(user.Groups));
			    //this.GroupsGrid.ItemsSource = vm.Groups;
			}
		    //this.GroupsGrid.RowEditEnding += OnRowEditEnding;
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

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //using (var context = new ExpansesManagerContext())
            //{
            //    Group newGroups = new Group();


            //TreeViewItem newGroup = new TreeViewItem();
            //newGroup.Header = textBox1.Text;
            //h.Items.Add(newGroup);
            //context.Groups.Add(newGroup);
            var Button1 = new Button();

            using (var context = new ExpansesManagerContext())
            {
                Group group = new Group();
                group.Name = textBox1.Text;
                User currentUser = AuthenticationManager.GetCurrentUser();
                var user = context.Users.Find(currentUser.Id);
                group.UserId = currentUser.Id;
                context.Groups.Add(group);
                context.SaveChanges();
            }
                TreeViewItem newGroup = new TreeViewItem();
                newGroup.Header = textBox1.Text;
                h.Items.Add(newGroup);

           
        }
    }
}
