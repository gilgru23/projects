using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace KanbanProject
{

    public class Column
    {
        private static readonly log4net.ILog log =
    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Guid colid;
        private List<Task> tasklist;
        private int limit;
        private String name;

        public Column(String name)
        {
            limit = -1;
            tasklist = new List<Task>();
            this.name = name;
            colid =Guid.NewGuid();
        }
        public Column(Guid colid,String name,int limit, List<Task> tasklist)
        {
            this.limit= limit;
            this.tasklist = tasklist ;
            this.name = name;
            this.colid= colid;
        }

        public Guid ColID
        {
            get { return colid;}
            set { colid =value;}
        }

        public int getLimit()
        {
            return limit;
        }
        public String getName()
        {
            return name;
        }
        public void setName(String name)
        {
            this.name = name;
        }


        public Boolean limTasks(int lim)
        {
            if (lim > 0 | lim == -1)
            {
                limit = lim;
                return true;
            }
            else
            {
                log.Error("Illegal Task limit argument");
                return false;
            }
        }

        public Task createTask(String title, String text,DateTime time)
        {
            if ((limit == -1 | tasklist.Count() < limit)&(title.Length>0&title.Length<=50)&(text.Length<=300))
            {
               Task task= new Task(title, text,time);
                
                tasklist.Add(task);
                log.Info("Task ["+title+"] was created");
                return task;
                
            }
            else if(limit<=tasklist.Count())
            {
                log.Error("Not allowed to create new tasks-maximum amount for tasks on the column has been reached");
                
                
                return null;
            }
            else
            {
                log.Error("The length of the title or the text is invalid");
                return null;
            }
        }

        //// wether we should move tasks if the next column is full??????
        public void addTask(Task t)
        {
            tasklist.Add(t);
        }

        public Task removeTask(Guid taskid)         /// should it recive an object or a Guid???meanwhile in int
        {
            bool isFound = false;
            Task t = null;
            IEnumerator<Task> iter = tasklist.GetEnumerator();
            bool hasNext = iter.MoveNext();
            while (hasNext & !isFound)
            {
                t = iter.Current;
                if (t.getTaskid().Equals(taskid))
                {
                    isFound = true;
                }
                hasNext = iter.MoveNext();
            }
            if (isFound)
                tasklist.Remove(t);
            return t;   //// needs testing!!!
        }

        public Task editTask(Guid taskid, String title, String text,DateTime time)
        {
            int index = tasklist.FindIndex(ta => ta.getTaskid().Equals(taskid));
            
            if (index != -1)
            {
                Task t = tasklist[index];
                if (t.SetTitle(title))
                { 
                    t.SetText(text);
                    t.setDueDate(time);
                    return t;
                }
            }
            return null;
        }
     
            public List<String> ToString()
            {
            List<String> output = new List<String>();
            output.Add(this.name);
            foreach(Task t in tasklist)
            {
                
                output.Add(t.ToString());
            }
            return output;
            }

        public List<Task> getTasks()
        {
            return tasklist;
        }
        public void setTasks(List<Task> tasks)
        {
            this.tasklist = tasks;
        }
        
    }
}
