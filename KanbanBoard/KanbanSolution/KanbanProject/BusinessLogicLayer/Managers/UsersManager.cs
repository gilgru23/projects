using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace KanbanProject.BusinessLogicLayer.Managers
{
 
    using KanbanProject.DataAccessLayer;
    
    public  class UsersManager<T> : ObjectManager<User> where T : Iidentifiable
    {

        //--------------------------fields-------------------------------------------------//

        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private BoardsManager boards;
        private UserHandler userStorage;

        //--------------------------constructors-------------------------------------------//

        public UsersManager(List<User> list) : base(list) { }
        public UsersManager()
        {
            dataset = null;
            userStorage = new UserHandler();
        }


        //--------------------------abstract methods implemenation--------------------------//
        public override void loadData()
        {
            dataset = new SortedDictionary<string, User>();
            List<NumbUser> list = new List<NumbUser>();  // might be better to use 'List<>' with strings instead
            list = userStorage.readUsers();
            if (list != null)
            {
                foreach (NumbUser numb in list)
                {
                    addOrReplace(new User(numb.email, numb.password));
                }
            }
            else
            {
                 log.Error("Unable to load Users from disk");
            }
        }
        /*
        public void saveAllData()
        {
            if (dataset != null)
            {
                List<KeyValuePair<String,String>> list = new List<KeyValuePair<String, String>>();
                foreach (KeyValuePair<String, User> pair in base.dataset)
                {
                    list.Add(new KeyValuePair<string, string>(pair.Key,pair.Value.getPassword()));
                } 
                userStorage.writeUsers();
            }
            else
            {
                log.Error("Attempted saving data before it was loaded");
            }
        }
        */

        public void saveUser(User user)
        {
            NumbUser nu = new NumbUser(user.getID(), user.getPassword());
            userStorage.writeUser(nu);
        }

        //---------------------------------additional methods-------------------------------//
        public void init()
        {
            loadData();
            boards = new BoardsManager();
            boards.loadData();
            log.Info("System was initialized");

        }

        // <summary>
        //  first checks for a match between the given email&password and the the user list on 'dataset'
        //  then, if succesful, goes through the boards data-set on 'BoardsManager' and looks-up
        //  the matching board.
        //  *note - the fuction 'init' must be called succesfully before calling this fuction.
        // </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        // <returns>
        //  if succesful,this function returns a User type object that represents ,and acts as, the current
        //  active user. otherwise it returns 'null'.
        // </returns>
        public User login(String email, String password)
        {
            bool isMatch = false;
            User currentUser = null;
            try
            {
                if (dataset[email].getPassword().Equals(password))
                {
                    isMatch = true;
                }
            }
            catch (NullReferenceException)
            {
                log.Error("Attempted login before data was retrieved");
            }
            catch (KeyNotFoundException)
            {
                log.Info("Failed login attempt: email ["+email+"] is not registered or password input does not match");
            }

            if (boards !=null && isMatch)
            {
                BoardsCollection tempBoard= boards.getBoardCollection(email);
                currentUser = new User(email, password, tempBoard);
            }
            else if (boards == null)
            {
                log.Error("Attempted login system has been initialized");
            }
            return currentUser;
        }
        public Boolean registerUser(User user)
        {
            Boolean exist = false;
           
            if (dataset != null)
            {
                
                if (dataset.ContainsKey(user.getID()))
                {
                    exist = true;
                    log.Error("the email adress [" + user.getID() + "] is already registered in the system");

                }
                else
                {
                    
                    exist = false;
                    addOrReplace(user);
                    log.Info("User [" + user.getID() + "] was registered to the system");
                    saveUser(user);
                }
            }
            return !exist;
        }
        


    }
}
