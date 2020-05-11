using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace KanbanProject
{
    public class Task
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private String title;
        private String text;
        private DateTime CREATION_TIME;
        private int status;
        private Guid taskid;
        private DateTime Due_Date;

        public object Log { get; private set; }

        public Task(String title, String text, DateTime Due_Date)
        {
            this.title = title;
            this.text = text;
            this.status = 0;
            this.taskid = Guid.NewGuid();
            CREATION_TIME = DateTime.Now;
            this.Due_Date = Due_Date;
        }
            public Task(String title, String text, DateTime Due_Date,Guid taskid, DateTime CREATION_TIME)
        {
            this.title = title;
            this.text = text;
            this.taskid = taskid;
            this.CREATION_TIME = CREATION_TIME;
            this.Due_Date = Due_Date;
        }

        public Guid getTaskid()
        {
            return taskid;
        }
        public String getTitle()
        {
            return title;
        }
        public String getText()
        {
            return text;
        }
        /*
        public int getStatus()
        {
            return status;
        }
        */
        public DateTime getCreationTime()
        {
            return CREATION_TIME;
        }
        /*
        public void advanceStatus(int status)
        { 
            this.status = status;
        }
        */
        public bool SetTitle(String title)
        {
                this.title = title;
                return true;
           
        }
        public void SetText(String text)
        {
                this.text = text;
        }
        public DateTime getDueDate()
        {
            return Due_Date;
        }
        public void setDueDate(DateTime Due_Date)
        {
                this.Due_Date = Due_Date;
         
        }
         

        /*
        public String ToString()
        {
            String S = title + ";" + text + ";" + Due_Date.ToString() + ";" + taskid + ";" + status + ";" + CREATION_TIME;                                                                               ;
            return S;
        }
        */
    }
}
