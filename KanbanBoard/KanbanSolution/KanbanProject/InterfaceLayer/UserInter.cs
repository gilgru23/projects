using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KanbanProject.BusinessLogicLayer;
using KanbanProject.BusinessLogicLayer.Managers;
using KanbanProject.InterfaceLayer;

namespace KanbanProject.InterfaceLayer
{
    public class UserInter
    {
        private static readonly log4net.ILog log =
      log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private UsersManager<User> um;
        private User user;
        private BoardInter myBoard;
     
        public UserInter(UsersManager<User> um)
        {
            this.um = um;
        }
        public Boolean Login(String email, String password)
        {
            bool loged = false;
            user = um.login(email, password);
            
            if (user == null)
            {
                Console.WriteLine("error");
                Console.ReadLine();
                log.Info("Failed login attempt: email[" + email + "] is not registered or password input does not match");
            }
            else
            {
                if (user.getBoards() != null)
                {
                   myBoard = new BoardInter(user.getBoards());
                }
                else
                {
                   myBoard = new BoardInter(new BoardsCollection(user.getID()));
                }
                log.Info("User [" + user.getID() + "] has logged in");
                loged = true;
            }
            return loged;
        }

        public bool SignUp(String email, String password)
        {
            int hasAtSign = 0;
            Boolean hasDot = false;
            Boolean hasCap = false;
            Boolean hasLow = false;
            Boolean hasNum = false;
            if (password.Length >= 4 & password.Length <= 20)
            {

              
                for (int i = 0; i < email.Length; i++)
                {

                    if (email[i] == '@')
                    {
                        hasAtSign++;
                    }
                    if (hasAtSign==1 & email[i] == '.')
                        hasDot = true;
                }
                for (int j = 0; j < password.Length; j++)
                {



                    if (password[j] >= 'A' & password[j] <= 'Z')
                    {

                        hasCap = true;
                    }
                    if (password[j] >= 'a' & password[j] <= 'z')
                    {

                        hasLow = true;
                    }
                    if (password[j] >= '0' & password[j] <= '9')
                    {

                        hasNum = true;
                    }

                }

                
                if (!(hasAtSign==1 & hasDot))
                {
                   
                    log.Error("Illegal email adress");
                    return false;
                }
                if (!(hasCap & hasLow & hasNum))
                {
                    log.Error("The password must include at least one capital character, one small character and a number");
                    return false;
                }


            }

            else
            {
              
                log.Error("The password length should be between 4 to 20 letters");
                return false;
            }
            if (hasAtSign == 1 & hasCap & hasDot & hasLow & hasNum) { 
            user = new User(email, password);


                if (!um.registerUser(user))
                {
                    Console.WriteLine("the email adress [" + user.getID() + "] is already registered in the system ");
                    return false;
                }
            }
            return true;
        }
        public void LogOut()
        {
            log.Info("User [" + user.getID() + "] has logged out");
            user = null;
            myBoard = null;
            
        }
        public BoardInter getMyBoard()
        {
            return myBoard;
        }
        

    }
}