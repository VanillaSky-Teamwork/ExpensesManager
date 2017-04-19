﻿using System;
using System.Collections.Generic;
using System.IO;
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
using Data;
using ExpansesManager.Core;
using Models.Models;
using Newtonsoft.Json;


namespace ExpansesManager
{
    /// <summary>
    /// Interaction logic for ExportGroupToJson.xaml
    /// </summary>
    public partial class ExportGroupToJson : Window
    {
        private ExpansesManagerContext _context = new ExpansesManagerContext();
        public ExportGroupToJson()
        {
            InitializeComponent();
            var currentUser = AuthenticationManager.GetCurrentUser();

            var grList = (from gr in _context.Groups.Where(u => u.UserId == currentUser.Id) select gr).ToList();
            comboBox.ItemsSource = grList;




        }

        private void ExportGroup(Group group)
        {
            using (var context = new ExpansesManagerContext())
            {
                var selectedGrop = context.Groups.Find(group.Id);
                string json = JsonConvert.SerializeObject(new
                {
                    Name = selectedGrop.Name,
                    IsActive = selectedGrop.IsActive,
                    SubGroups = selectedGrop.SubGroups.Count > 0
                        ? selectedGrop.SubGroups.Select(u => new
                        {
                            Name = u.Name,
                            IsActive = u.IsActive,
                            Elemnet = u.Elements.Count > 0
                                ? u.Elements.Select(e => new
                                {
                                    Name = e.Name,
                                    Price = e.Price,
                                    Date = e.DateBought
                                })
                                : null,
                        })
                        : null
                }, Formatting.Indented);

                string path = @"Groups.json";

                if (!File.Exists(path))
                {

                    File.WriteAllText(path, json);
                }
                else
                {
                    string appendGroup = json;
                    File.AppendAllText(path, appendGroup);
                }
            }
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            var selected = comboBox.SelectedItem as Group;
            var groupId = selected.Id;
            var currentGroup = AuthenticationManager.GetGroupById(groupId);

            ExportGroup(currentGroup);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainApp mApp = new MainApp();
            this.Close();
            mApp.ShowDialog();
        }
    }
}
