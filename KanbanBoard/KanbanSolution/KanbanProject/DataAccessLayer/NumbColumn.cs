using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.DataAccessLayer
{
    public class NumbColumn
    {
        public string id;
        public string name;
        public int limit;
        public List<NumbTask> tasks;

        public NumbColumn(string id, string name, int limit, List<NumbTask> tasks)
        {
            this.id = id;
            this.name = name;
            this.limit = limit;
            this.tasks = tasks;
        }
    }
}
