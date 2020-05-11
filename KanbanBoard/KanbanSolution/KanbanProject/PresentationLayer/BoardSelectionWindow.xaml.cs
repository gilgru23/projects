using KanbanProject.BusinessLogicLayer;
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
    /// Interaction logic for BoardSelectionWindow.xaml
    /// </summary>
    public partial class BoardSelectionWindow : Window
    {
        private BoardSelectionDataContext VM;
        private BoardInter bi;
        private UserInter ui;
        public BoardSelectionWindow(UserInter ui)
        {
            InitializeComponent();
            bi = ui.getMyBoard();
            VM = new BoardSelectionDataContext(bi);
            this.DataContext = this.VM;
            this.ui = ui;
        }
        
       
        private void addBoard()
        {
            if (bi.addBoard(VM.NameText)==null)
            {
                System.Windows.MessageBox.Show("Board adittion failed");
            }
            else
            {
                VM.BoardNames=bi.getBoardNames();
            }
        }

        private void AddBoard_Click(object sender, RoutedEventArgs e)
        {
            addBoard();
        }
        private void removeBoard()
        {
            if (!bi.removeBoard(VM.SelectedBoard))
            {
                System.Windows.MessageBox.Show("Board deletion failed");
            }
            else
            {
                VM.BoardNames = bi.getBoardNames();
            }
        }
        private void RemoveBoard_Click(object sender, RoutedEventArgs e)
        {
            removeBoard();
        }
        private void changeBoard()
        {
            bi.changeBoard(VM.SelectedBoard);
            BoardWindow win2 = new BoardWindow(ui);
            win2.Show();
            this.Close();
        }
        private void ChooseBoard_Click(object sender, RoutedEventArgs e)
        {
            changeBoard();
        }

        private void List_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            changeBoard();
        }
    }
}
/*
         InitializeComponent();
         BoardsCollection collection = new BoardsCollection("gil's ID");
         collection.Add("this board");
         collection.Add("other board");
         collection.Add("third board");
         BoardInter BI= new BoardInter(collection);
         BI.createTask("first task", "ever", DateTime.Now);
         BI.changeBoard("other board");
         BI.createTask("second task", "ever", DateTime.Now);
         BI.changeBoard("First board");
         VM = new BoardWindowDataContext(BI);
         this.DataContext = VM;
         */
