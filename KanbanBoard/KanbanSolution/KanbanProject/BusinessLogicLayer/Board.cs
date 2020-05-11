using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.BusinessLogicLayer
{
    using KanbanProject.DataAccessLayer;
    using Managers;


    public class Board : Iidentifiable
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private List<Column> columnlist;
        private String name;
        private String BoardID;
        private BoardsManager manager;


        public Board(String BoardID, String name)
        {
            this.name = name;
            this.BoardID = BoardID;
            this.manager = new BoardsManager();
            columnlist = new List<Column>();
            addColumn("BackLog");
            addColumn("In Progress");
            addColumn("Done");
        }
        public Board(String BoardID)
        {
            this.BoardID = BoardID;
            this.name = "Main Board";
            this.manager = new BoardsManager();
            columnlist = new List<Column>();
            addColumn("BackLog");
            addColumn("In Progress");
            addColumn("Done");
        }

        public Board(String BoardID,String name, List<Column> columnlist)
        {
            this.name = name;
            this.BoardID = BoardID;
            this.manager = new BoardsManager();
            this.columnlist = columnlist;
        }

        public void setManager(BoardsManager manager)
        {
            this.manager = manager;
        }

        public Guid createTask(string title, string text, DateTime time)
        {

            Task task = columnlist[0].createTask(title, text, time);
            Guid id = Guid.Empty;
            if (task!=null)
            {   id= task.getTaskid();
                String creation_time = task.getCreationTime().ToString();
                String due_date = time.ToString();
                manager.saveTask(title, text, creation_time, id, due_date, columnlist[0].ColID);
            }
            return id;

        }

        public String getID()
        {
            return BoardID;
        }
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public void saveBoard()
        {
            manager.saveBoard(BoardID,name);
        }
        public void deleteBoard()
        {
            manager.removeBoard(BoardID, name);
        }

        public void moveTask(Guid taskid, String src)
        {
            if (src == null || src.Length < 1)
            {
                log.Error("Invalid column name input on column creation attempt ");

            }
            else
            {
                int index = columnlist.FindIndex(c => c.getName().Equals(src));
                if (index < columnlist.Count - 1 && index>-1)
                {
                    Column nxtCol = columnlist[index + 1];
                    if ((nxtCol.getLimit() >nxtCol.getTasks().Count)|| nxtCol.getLimit() ==-1)
                    {
                        Task t = columnlist[index].removeTask(taskid);
                        if (t != null)
                        {
                            nxtCol.addTask(t);
                            log.Info("The task [" + t.getTitle() + "] has been moved from " + src + "to " + nxtCol.getName());
                            manager.saveTask(t.getTitle(),t.getText(),t.getCreationTime().ToString(),
                                    t.getTaskid(),t.getDueDate().ToString(),nxtCol.ColID);
                        }
                        else
                        {
                            log.Error("Unable to find the task to move on the column [" + columnlist[index].getName() + "]");
                        }
                    }
                    else
                    {
                        log.Error("Unable to move task to next column [" + nxtCol.getName() + "] due to the task amount limitation [" + nxtCol.getLimit() + "]");
                    }

                }
                else
                {
                    log.Error("Illegal task movement");
                }
                
            }

        }
        public Boolean ChangeLimit(int limit, String status)
        {
            if (status == null|status.Length<1)
            {
                log.Error("Attempted to change limit on null or empty column name");
                return false;
            }   
            Column col = columnlist.Find(c => c.getName().Equals(status));
            if (col != null)
            {
                if (col.limTasks(limit))
                {
                    manager.saveColumn(col.ColID,status,limit,this.BoardID,this.Name);
                    return true;
                }
            }
            return false;
        }
        public bool editTask(Guid taskid, String status, string title, string text,DateTime Due_Date)
        {
            if(status==null)
            {
                log.Error("Attempted to insert null column name");
                return false;
            }
            int index = columnlist.FindIndex(c => c.getName().Equals(status));
            
            Boolean check= (index!=-1&&index+1<columnlist.Count());
            if (!check)
            {
                log.Error("It is not possible to edit tasks on the last column");
            }
            else
            {
                Task toEdit = columnlist[index].editTask(taskid, title, text, Due_Date);
                check = toEdit != null;
                if (check)
                    manager.saveTask(title, text,toEdit.getCreationTime().ToString()
                                     , taskid, Due_Date.ToString(), columnlist[index].ColID);
            }
            return check;
        }

        public  KeyValuePair<String,List<List<String>>> GetPair()
        {
            List<List<String>> output = new List<List<String>>();
            foreach (Column c in columnlist)
            {
                if (c != null)
                    output.Add(c.ToString());
                else
                    output.Add(new List<String>());
            }
            KeyValuePair<String, List<List<String>>> pair = 
                    new KeyValuePair<String, List<List<String>>>(BoardID, output);
            return pair;
        }

        public NumbBoard GetNumb()
        {
            List<NumbColumn> numbCols = new List<NumbColumn>();
            foreach (Column col in columnlist)
            {
                List<NumbTask> numbTasks = new List<NumbTask>();
                foreach (Task t in col.getTasks())
                {
                    numbTasks.Add(new NumbTask(t.getTitle(), t.getText(), t.getCreationTime() + "", t.getTaskid() + "", t.getDueDate() + ""));
                }
                numbCols.Add(new NumbColumn(col.ColID.ToString(),col.getName(), col.getLimit(), numbTasks));
            }
            return new NumbBoard(BoardID,name, numbCols);
        }



        public List<Column> getColumns()
        {
            return columnlist;
        }


        //---------------------------manipulate columns---------------------------------------------------//

        public Boolean addColumn(String name)
        {
            if (name == null || name.Length < 1)
            {
                log.Error("Invalid column name input on column creation attempt ");
                return false;
            }
            foreach (Column c in columnlist)
            {
                if(c.getName().Equals(name))
                {
                    log.Error("Attempted to add column with existing name");
                    return false;
                }
            }
            Column col = new Column(name);
            columnlist.Add(col);
            log.Info("Column [" + name + "] has been added");
            manager.saveColumn(col.ColID,name,col.getLimit(),this.BoardID, this.Name);
            return true;
        }
        public Boolean removeColumn(String name)
        {
            if(name== null || name.Length < 1 )
            {
                log.Error("Invalid column name input on column removal attempt ");
                return false;
            }
            foreach(Column c in columnlist)
            {
                if(c.getName().Equals(name))
                {
                    columnlist.Remove(c);
                    log.Info("Column [" + name + "] has been removed");
                    manager.removeColumn(c.ColID);
                    return true;
                }
            }
            return false;
        }/*
        public Boolean swapColumns(String name1, String name2)
        {
            try
            {
                Column col1 = columnlist.Find(c => c.getName().Equals(name1));
                Column col2 = columnlist.Find(c => c.getName().Equals(name2));
                String tempName = col1.getName();
                List<Task> tempTasks = col1.getTasks();
                col1.setName(col2.getName());
                col1.setTasks(col2.getTasks());
                col2.setName(tempName);
                col2.setTasks(tempTasks);
                log.Info("Columns [" + name1 + "] and ["+ name2 +"] have been swapped");
                //manager.saveBoard();
                return true;
            }
            catch (ArgumentNullException)
            {
                log.Error("Invalid column name input on column swap attempt ");
                return false;
            }
            catch (NullReferenceException)
            {
                log.Error("Invalid column name input on column swap attempt ");
                return false;
            }
        }*/
        public Boolean columnSwap(String status , bool direction)
        {
            //direction = true >> move column towards the end of the list
            if (status == null)
            {
                log.Error("Invalid column name input [null] on column swap attempt");
                return false;
            }
            int dir = 1;
            if (!direction)
                dir = -1;
            int colindex = columnlist.FindIndex(c => c.getName().Equals(status));
            if ((colindex < 0))
            {
                log.Error("Invalid column name input on column swap attempt ");
                return false;
            }
            else if (colindex+dir >= columnlist.Count)
            {
                return false; //cant move the column it ths direcrion because its already the last one 
            }
            else if (colindex+dir < 0)
            {
                return false; //cant move the column it ths direcrion because its already the first one 
            }
            else
            {
                Column col1 = columnlist[colindex];
                Column col2 = columnlist[colindex+dir];
                columnlist.Remove(col2);
                columnlist.Insert(colindex,col2);

                /*String tempName = col1.getName();
                List<Task> tempTasks = col1.getTasks();
                col1.setName(col2.getName());
                col1.setTasks(col2.getTasks());
                col2.setName(tempName);
                col2.setTasks(tempTasks);*/

                log.Info("Columns [" + status + "] and [" + col1.getName() + "] have been swapped");
                
                return true;
            }
        }

        public Boolean renameColumn(String curr, String name)
        {
            if (name == null || name.Length < 1)
            {
                log.Error("Invalid column name input on column rename attempt ");
                return false;
            }
            Column col = columnlist.Find(c => c.getName().Equals(curr));
            if (col != null)
            {
                col.setName(name);
                log.Info("Column [" + curr + "] name was changed to [" + name + "]");
                manager.saveColumn(col.ColID,name,col.getLimit(),this.BoardID, this.Name);
                return true;
            }
            return false;
        }
    }
}
