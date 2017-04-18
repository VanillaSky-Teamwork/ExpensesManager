using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpansesManager.ViewModels;
using Models.Models;

namespace ExpansesManager
{
    public class MainAppViewModel// : INotifyPropertyChanged
    {
        private ObservableCollection<GroupViewModel> group;
        public ObservableCollection<GroupViewModel> Groups
        {
            get
            {
                return this.group;
            }
            set
            {
                if (this.group != value)
                {
                    this.group = value;
                }
            }
        }

        public MainAppViewModel()
        {
                this.Groups = new ObservableCollection<GroupViewModel>();
        }
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    if (this.PropertyChanged != null)
        //    {
        //        this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        //    }
        //}
    }
}
