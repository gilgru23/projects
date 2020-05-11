using KanbanProject.BusinessLogicLayer;
using KanbanProject.BusinessLogicLayer.Managers;
using KanbanProject.DataAccessLayer;
using KanbanProject.InterfaceLayer;
using KanbanProject.InterfaceLayer.ModelObjects;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KanbanProject.PresentationLayer
{
    public partial class Login : Window
    {
        UserWindowDataContext VM;
        private UserInter ui;
        public Login()
        {
            InitializeComponent();
            UsersManager<User> um = App.um;
            ui = new UserInter(um);
            this.VM = new UserWindowDataContext();
            this.DataContext = this.VM;
        }

        private void login()
        {
            if (ui.Login(VM.UserName, VM.PWD))
            {
                BoardSelectionWindow bsw = new BoardSelectionWindow(ui);
                bsw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid user name or password");
            }
        }

        private void signup()
        {
            if (ui.SignUp(VM.UserName, VM.PWD))
                MessageBox.Show("Registration succeeded");
            else
            {
                MessageBox.Show("Invalid input -" + Environment.NewLine +
                    "this email is already used" + Environment.NewLine +
                    "or Illegal email or password");
            }
        }

        //--------------------------------------------------Buttons--------------------------------------------//

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //--------------------------------------tests--------------------------------//
            testing();

            //------------------------tests ends here, real logic starts-------------------//

            login();
        }

        private void testing()
        {
            int con = 0;
            if (con == 1)// testing thruogh interface layer
            {
                ui.Login(VM.UserName, VM.PWD);
                BoardInter bi = ui.getMyBoard();
                bi.createTask("title1", "text", DateTime.Parse("12/3/2018"));//creates tasks for the current user
                Guid taskid = bi.getBoard().columns[0].tasks[0].taskid;
                bi.moveTask(taskid, "Backlog");
                bi.moveTask(taskid, "In Progress");
                bi.createTask("title2", "text", DateTime.Parse("12/3/2018"));
                taskid = bi.getBoard().columns[0].tasks[0].taskid;
                bi.moveTask(taskid, "Backlog");
                bi.createTask("title3", "text", DateTime.Parse("12/3/2018"));
            }
            else if (con == 2)// testing business layer
            {
                /* 
                 Board board = App.um.login(VM.UserName, VM.PWD).getBoardAt(0);
                 MessageBox.Show("add Backlog= " + board.addColumn("Backlog").ToString());
                 MessageBox.Show("rename inprogress= " + board.renameColumn("InProgress", "In Progress").ToString());
                 MessageBox.Show("swap Backlog& In progress= " + board.swapColumns("BackLog", "In Progress"));
                 MessageBox.Show("add sharonnnn= " + board.addColumn("sharonnnn"));
                 MessageBox.Show("add gilz= " + board.addColumn("gilz"));
                 MessageBox.Show("remove gilz= " + board.removeColumn("gilz"));
                 */
            }
            else if (con == 3)//test Data Access layer
            {
                BoardHandler boardHandler = new BoardHandler();
                List<NumbBoard> nboards = boardHandler.ReadBoards();

                UserHandler userHandler = new UserHandler();
                List<NumbUser> nusers = userHandler.readUsers();
                foreach (NumbUser nuser in nusers)
                {
                    MessageBox.Show("user = " + nuser.email + " , password = " + nuser.password);
                }
            }
        }

        private void TxtUser_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            signup();
        }
    }
}
