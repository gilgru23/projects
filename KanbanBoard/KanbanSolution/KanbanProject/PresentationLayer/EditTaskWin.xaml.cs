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
    /// Interaction logic for EditTaskWin.xaml
    /// </summary>
    public partial class EditTaskWin : Window
    {
        BoardInter myBoard;
        BoardWindowDataContext VM;
        TaskWindowDataContext TC;
        Guid taskid;
        String status;
        public EditTaskWin(BoardInter myBoard, BoardWindowDataContext VM)// ,Guid taskid ,String status, String title, String text, DateTime due_Date)
        {
            InitializeComponent();
            this.myBoard = myBoard;
            this.VM = VM;
            this.taskid = VM.Selected.taskid;
            this.status = VM.Selected.status;
            this.TC = new TaskWindowDataContext();
            TC.Title = VM.Selected.title;
            TC.Text = VM.Selected.text;
            TC.Due_date = VM.Selected.Due_Date;
            this.DataContext = this.TC;
        }


        private void editTask()
        {
            if (myBoard.EditTask(taskid, status, TC.Title, TC.Text, TC.Due_date))
            {
                MessageBox.Show("task edited Successfully");
                VM.ShowTheard();
                this.Close();
            }
            else
            {
                MessageBox.Show("unable to edit task");
            }
        }

        //--------------------------------------Buttons----------------------------------------------------------------//
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            editTask();
        }

    }
}
