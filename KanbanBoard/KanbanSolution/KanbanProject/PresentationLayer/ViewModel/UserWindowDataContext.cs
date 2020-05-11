using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KanbanProject.BusinessLogicLayer;
using KanbanProject.BusinessLogicLayer.Managers;
using KanbanProject.InterfaceLayer;

namespace KanbanProject.PresentationLayer.ViewModel
{
    class UserWindowDataContext : INotifyPropertyChanged
    {
        string userName = "";
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("UserName"));
            }
        }
        string pwd = "";
        public string PWD
        {
            get
            {
                return pwd;
            }
            set
            {
                pwd = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PWD"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
  
        public UserWindowDataContext()
        {
        }

    }
}

