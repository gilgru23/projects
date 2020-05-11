using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.InterfaceLayer.ModelObjects
{
   public class ModelTask
    {
        
        public Guid taskid;
        public String title;
        public String text;
        public String status;
        public DateTime CREATION_TIME;
        public DateTime Due_Date;

        public ModelTask(Guid taskid, String title, String text, String status, DateTime CREATION_TIME,DateTime Due_Date)
        {
            this.taskid = taskid;
            this.title = title;
            this.text = text;
            this.status = status;
            this.CREATION_TIME = CREATION_TIME;
            this.Due_Date = Due_Date;
        }
    }
}
