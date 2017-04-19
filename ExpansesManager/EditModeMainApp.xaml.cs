using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for EditModeMainApp.xaml
    /// </summary>
    public partial class EditModeMainApp : Window
    {
        public EditModeMainApp()
        {
            InitializeComponent();

            var vm = new MainAppViewModel();
            User currentUser = AuthenticationManager.GetCurrentUser();
            using (var contex = new ExpansesManagerContext())
            {
                textBox.Text = "Welcome " + " " + currentUser.Username;
                var user = contex.Users.Find(currentUser.Id);
                vm.Groups = new ObservableCollection<GroupViewModel>(Mapper.Instance.Map<IEnumerable<Group>, ObservableCollection<GroupViewModel>>(user.Groups));
                this.GroupsGrid.DataContext = vm;
                this.GroupsGrid.ItemsSource = vm.Groups;
            }
        }
        private void ButtonSaveChanges_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = this.GroupsGrid.DataContext as MainAppViewModel;
            var gridGroups = Mapper.Instance.Map<ObservableCollection<GroupViewModel>, IEnumerable<Group>>(vm.Groups);

            using (var db = new ExpansesManagerContext())
            {
                foreach (var group in gridGroups)
                {
                    if (group.Id > 0)
                    {
                        var dbGroup = db.Groups.Find(group.Id);
                        if (dbGroup != null)
                        {
                            if (dbGroup.Name != group.Name)
                            {
                                dbGroup.Name = group.Name;
                                db.SaveChanges();
                            }
                            var existingSubGroups = group.SubGroups.Where(s => s.Id != 0).ToList();
                            existingSubGroups.ForEach(eg =>
                            {
                                var dbSubGroup = dbGroup.SubGroups.FirstOrDefault(g => g.Id == eg.Id);
                                if (dbSubGroup != null)
                                {
                                    if (dbSubGroup.Name != eg.Name)
                                    {
                                        dbSubGroup.Name = eg.Name;
                                        db.SaveChanges();
                                    }
                                    foreach (var element in eg.Elements)
                                    {
                                        var dbElement = dbSubGroup.Elements.FirstOrDefault(el => el.Id == element.Id);
                                        if (dbElement != null && element.Id > 0)
                                        {
                                            if (dbElement.Name != element.Name || dbElement.Price != element.Price || dbElement.DateBought != element.DateBought)
                                            {
                                                dbElement.Name = element.Name;
                                                dbElement.Price = element.Price;
                                                dbElement.DateBought = element.DateBought;
                                                db.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            });

                            var newSubGroups = group.SubGroups.Where(s => s.Id == 0).ToList();
                            if (newSubGroups.Count > 0)
                            {
                                newSubGroups.ForEach(sg =>
                                {
                                    dbGroup.SubGroups.Add(sg);
                                    db.SaveChanges();
                                });
                            }
                        }
                    }
                }

                var newGroups = gridGroups.Where(g => g.Id == 0).ToList();
                var currentUser = db.Users.Find(AuthenticationManager.GetCurrentUser().Id);
                newGroups.ForEach(g =>
                {
                    currentUser.Groups.Add(g);
                    db.SaveChanges();
                });

                vm.Groups = new ObservableCollection<GroupViewModel>(Mapper.Instance.Map<IEnumerable<Group>, ObservableCollection<GroupViewModel>>(currentUser.Groups));
            }
            this.GroupsGrid.UpdateLayout();
        }
    }
}
