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
    /// Interaction logic for ColumnLimitWindow.xaml
    /// </summary>
    public partial class ColumnLimitWindow : Window
    {
        BoardWindowDataContext VM;
        ColumnLimitDataContext CLD;
        BoardInter BI;

        public ColumnLimitWindow(BoardInter BI, BoardWindowDataContext VM)
        {
            InitializeComponent();
            this.VM = VM;
            this.BI = BI;
            this.CLD = new ColumnLimitDataContext((VM.Selected.limit!=-1), VM.Selected.limit);
            this.DataContext = CLD;
        }
 
        private void changeLimit()
        {
            if (CLD.IsLimited)
            {
                if (BI.LimitColumn(CLD.Limit, VM.Selected.status))
                    VM.ShowTheard();
            }
            else
            {
                if (BI.RemoveLimit(VM.Selected.status))
                    VM.ShowTheard();
            }
            this.Close();
        }

        //--------------------------------------------buttons-----------------------------------------------//

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            changeLimit();
        }



    }
}
