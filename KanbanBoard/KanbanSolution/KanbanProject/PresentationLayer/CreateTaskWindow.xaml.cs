using KanbanProject.InterfaceLayer;
using KanbanProject.PresentationLayer.ViewModel;
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

namespace KanbanProject.PresentationLayer
{
    /// <summary>
    /// Interaction logic for CreateTaskWindow.xaml
    /// </summary>
    public partial class CreateTaskWindow : Window
    {
        BoardInter myBoard;
        BoardWindowDataContext VM;
        TaskWindowDataContext TC;
        public CreateTaskWindow(BoardInter myBoard, BoardWindowDataContext VM)
        {
            InitializeComponent();
            this.myBoard = myBoard;
            this.VM = VM;
            this.TC = new TaskWindowDataContext();
            this.DataContext = this.TC;
        }

        private void createTask()
        {
            if (myBoard.createTask(TC.Title, TC.Text, TC.Due_date))
            {
                MessageBox.Show("Task was created");
                VM.ShowTheard();
                this.Close();
            }
            else
            {
                MessageBox.Show("Unable to create Task");
            }
        }

        //----------------------------------------Buttons--------------------------------------------------------//
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            createTask();
        }
    }
}
