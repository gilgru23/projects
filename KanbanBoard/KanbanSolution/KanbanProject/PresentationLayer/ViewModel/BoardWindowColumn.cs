using KanbanProject.InterfaceLayer;
using KanbanProject.InterfaceLayer.ModelObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.PresentationLayer.ViewModel
{
    public class BoardWindowColumn
    {
        
        public String status { get; set; }
        public String title { get; set; }
        public String text { get; set; }
        public DateTime CREATION_TIME { get; set; }
        public DateTime Due_Date { get; set; }
        public Guid taskid { get; set; }
        public int limit { get; set; }

        public BoardWindowColumn(ModelTask task,int limit)
        {
            this.taskid = task.taskid;
            this.title = task.title;
            this.text = task.text;
            this.status = task.status;
            this.CREATION_TIME = task.CREATION_TIME;
            this.Due_Date = task.Due_Date;
            this.limit = limit;
        }
        public BoardWindowColumn(String status, int limit)
        {
            this.taskid = Guid.Empty;
            this.title = "";
            this.text = "";
            this.status = status;
            this.CREATION_TIME = DateTime.MinValue;
            this.Due_Date = DateTime.MinValue;
            this.limit = limit;
        }

    }
}
