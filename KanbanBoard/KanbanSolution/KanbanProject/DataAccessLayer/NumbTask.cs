using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.DataAccessLayer
{
    public class NumbTask
    {
        public string title;
        public string text;
        public string CREATION_TIME;
        public string taskid;
        public string Due_Date;

        public NumbTask(String title, String text, String CREATION_TIME, String taskid, String Due_Date)
        {
            this.title = title;
            this.text = text;
            this.CREATION_TIME = CREATION_TIME;
            this.taskid = taskid;
            this.Due_Date = Due_Date;
        }
    }
}
