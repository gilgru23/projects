using KanbanProject.InterfaceLayer;
using KanbanProject.PresentationLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for EditColumnWindow.xaml
    /// </summary>
    public partial class EditColumnWindow : Window , INotifyPropertyChanged
    {
        BoardInter myBoard;
        BoardWindowDataContext VM;
        private bool create;
        private String winTitle;
        public string WinTitle
        {
            get
            {
                return winTitle;
            }
            set
            {
                winTitle = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("title"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public EditColumnWindow(BoardInter myBoard, BoardWindowDataContext VM, bool create)
        {
            InitializeComponent();
            this.myBoard = myBoard;
            this.VM = VM;
            this.create = create;
            colName.Text = "";
            this.DataContext =WinTitle;
            if (create)
            {
                WinTitle = "Add Column";  
            }
            else
            {
                WinTitle = "Rename Column";
            }
        }

        private void setColumn()
        {
            if (create) // add new column
            {
                if (myBoard.addColumn(colName.Text))
                {
                    VM.ShowTheard();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid column name");
                }
            }
            else // rename Column
            {
                if (myBoard.renameColumn(VM.Selected.status, colName.Text))
                {
                    VM.ShowTheard();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid column name");
                }
            }
        } 

        //-----------------------------------------Buttons---------------------------------------------//

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            setColumn();
        }
    }
}
