using KanbanProject.BusinessLogicLayer;
using KanbanProject.InterfaceLayer;
using KanbanProject.InterfaceLayer.ModelObjects;
using KanbanProject.PresentationLayer.ViewModel;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Collections.Generic;



namespace KanbanProject.PresentationLayer
{
    public partial class BoardWindow : Window
    {
        BoardWindowDataContext VM;
        private BoardInter myBoard;
        private UserInter ui;

        public BoardWindow( UserInter ui)
        {
            InitializeComponent();
            this.myBoard = ui.getMyBoard();
            VM = new BoardWindowDataContext(myBoard);
            this.DataContext = VM;
             this.ui = ui;
        }



        private void EditTask()
        {
            EditTaskWin editWin = new EditTaskWin(myBoard, VM);
            editWin.Show();
        }
        private void LogOut()
        {
            ui.LogOut();
            Login win2 = new Login();
            win2.Show();
            this.Close();
        }
        private void CreateTask()
        {
            CreateTaskWindow createWin = new CreateTaskWindow(myBoard, VM);
            createWin.Show();
        }
        private void MoveTask()
        {
            myBoard.moveTask(VM.Selected.taskid, VM.Selected.status);
            VM.ShowTheard();
        }
        private void UpColumn()
        {
            if (myBoard.swapColumns(VM.Selected.status, false))
                VM.ShowTheard();
        }
        private void DownColumn()
        {
            if (myBoard.swapColumns(VM.Selected.status, true))
                VM.ShowTheard();
        }
        private void AddColumn()
        {
            EditColumnWindow editCol = new EditColumnWindow(myBoard, VM, true);
            editCol.Show();
        }
        private void RenameColumn()
        {
            EditColumnWindow editCol = new EditColumnWindow(myBoard, VM, false);
            editCol.Show();
        }
        private void RemoveColumn()
        {
            if (myBoard.removeColumn(VM.Selected.status))
                VM.ShowTheard();
        }
        private void LimitColumn()
        {
            ColumnLimitWindow limWin = new ColumnLimitWindow(myBoard, VM);
            limWin.Show();
        }

        //---------------------------------------Buttons---------------------------------------------//

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            EditTask();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogOut();
        }
        
        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            CreateTask(); 
        }
        
        private void MoveTask_Click(object sender, RoutedEventArgs e)
        {
            MoveTask();
        }
        
        private void UpCol_Click(object sender, RoutedEventArgs e)
        {
            UpColumn();
        }

        private void DownCol_Click(object sender, RoutedEventArgs e)
        {
            DownColumn();
        }
        
        private void AddCol_Click(object sender, RoutedEventArgs e)
        {
            AddColumn();
        }

        private void RenameCol_Click(object sender, RoutedEventArgs e)
        {
            RenameColumn();
        }

        private void RemoveCol_Click(object sender, RoutedEventArgs e)
        {
            RemoveColumn();
        }
        
        private void LimitCol_Click(object sender, RoutedEventArgs e)
        {
            LimitColumn();
        }

        private void changeBoard()
        {
            myBoard.changeBoard(VM.SelectedBoard.ToString());
            VM.ShowTheard();
            //VM.Board=myBoard;
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            changeBoard();
        }
        
    }
}
