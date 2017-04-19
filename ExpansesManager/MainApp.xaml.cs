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

                foreach (var group in contex.Groups.Where(g => g.IsActive == true))
                {
                    TreeViewGroups.Items.Add(group.Name);
                }
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            AuthenticationManager.Logout();
            this.Close();
            main.ShowDialog();
        }

        private void AddGroupButton_Click(object sender, EventArgs e)
        {
            using (var context = new ExpansesManagerContext())
            {
                //if(textBox1.Text == string.Empty)
                //{
                //    MessageBox.Show("Please enter group name!");
                //    return;
                //}

                //if(context.Groups.Any(g => g.Name == textBox1.Text))
                //{
                //    MessageBox.Show("Group already exists!");
                //    return;
                //}

                AddWindow addWindow = new AddWindow();
                addWindow.ShowDialog();


                Group group = new Group();
                group.Name = addWindow.textBox.Text;
                User currentUser = AuthenticationManager.GetCurrentUser();
                var user = context.Users.Find(currentUser.Id);
                group.UserId = currentUser.Id;

                context.Groups.Add(group);
                context.SaveChanges();

                TreeViewItem newGroup = new TreeViewItem();
                newGroup.Header = addWindow.textBox.Text;
                TreeViewGroups.Items.Add(newGroup);
            }
        }

        private void RemoveGroupButton_Click(object sender, RoutedEventArgs e)
        { 
            using (var context = new ExpansesManagerContext())
            {
                if(TreeView1.SelectedItem == null)
                {
                    MessageBox.Show("Please select first.");
                    return;
                }
                context.Groups.Where(g => g.IsActive == true).FirstOrDefault(n => n.Name == TreeView1.SelectedItem.ToString()).IsActive = false;
                context.SaveChanges();
            }
                        
            TreeViewItem newGroup = new TreeViewItem();
            newGroup.Name = TreeView1.SelectedItem.ToString();

            MainApp mApp = new MainApp();
            this.Close();
            mApp.ShowDialog();
        }
    }
}


