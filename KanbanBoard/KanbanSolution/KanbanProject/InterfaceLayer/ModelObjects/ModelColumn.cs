
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.InterfaceLayer.ModelObjects
{
    public class ModelColumn
    {
        public List<ModelTask> tasks;
        public String name;
        public int limit;
        public ModelColumn(String name,List<ModelTask> tasks,int limit)
        {
            this.limit = limit;
            this.tasks = tasks;
            this.name = name;
        }
    }
}
