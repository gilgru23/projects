using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.BusinessLogicLayer
{
   public class User : Managers.Iidentifiable
    {
        private String email;
        private String password;
        private BoardsCollection myboards;

        public User(String email, String password)
        {
            this.email = email;
            this.password = password;
            this.myboards = new BoardsCollection(email);
        }
        public User(String email, String password, BoardsCollection myboards)
        {
            this.email = email;
            this.password = password;
            this.myboards = myboards;
        }

        public String getID()
        {
            return email;
        }

        public String getPassword()
        {
            return password;
        }
        public BoardsCollection getBoards()
        {
            return myboards;
        }
    }
}
