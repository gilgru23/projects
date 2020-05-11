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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KanbanProject.BusinessLogicLayer;
using KanbanProject.BusinessLogicLayer.Managers;
namespace KanbanProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       public static UsersManager<User> um;
        public App()
        { 
            um = new UsersManager<User>();
            um.init();
            

            
        }

    }
}
