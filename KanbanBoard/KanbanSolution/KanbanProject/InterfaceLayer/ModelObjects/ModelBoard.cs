using KanbanProject.InterfaceLayer.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.InterfaceLayer.ModelObjects
{
   public class ModelBoard
    {
        public List<ModelColumn> columns;
        public ModelBoard(List<ModelColumn> columns)
        {
            this.columns = columns;
        }
    }
}
