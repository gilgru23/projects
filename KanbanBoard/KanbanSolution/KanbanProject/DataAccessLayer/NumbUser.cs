using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.DataAccessLayer
{
    [Serializable]
    class NumbUser
    {
        public string email;
        public string password;

        public NumbUser(String email, String password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
