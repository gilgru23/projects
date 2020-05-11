using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanbanProject.BusinessLogicLayer.Managers;
using KanbanProject.BusinessLogicLayer;

namespace KanbanProject.InterfaceLayer
{
    using KanbanProject.InterfaceLayer.ModelObjects;
    public class BoardInter
    {
        private BoardsCollection myboards;
        private Board currBoard;
        public BoardInter(BoardsCollection myboards)
        {
            this.myboards = myboards;
            currBoard = myboards.getBoardAt(0); //assuming the boards manager makes sure
                                                //there is at least 1 board in collection 
        }
        //---------------------------manage boards----------------------------------//
        public Board addBoard(String boardName)
        {
            return myboards.Add(boardName);
        }
        public Boolean removeBoard(String boardName)
        {
            return myboards.removeBoard(boardName);
        }
        private Boolean renameBoard(String currName, String newName)
        {
            return myboards.renameBoard(currName,newName);
        }
        internal Boolean changeBoard(string board)
        {
            Console.WriteLine("change to board "+board);
            Board toChange= myboards.getByName(board);
            if (toChange != null)
            {
                currBoard = toChange;
                return true;
            }
            return false;
        }

        internal void removeBoard()
        {
            throw new NotImplementedException();
        }

        internal List<string> getBoardNames()
        {
            return myboards.getBoardNames();
        }
        //--------------------------------------------------------------------------//



        public Boolean createTask(String title, String text, DateTime due_date)
        {
            if (title.Contains(';') | text.Contains(';'))
                return false;
            else
                return (!currBoard.createTask(title, text, due_date).Equals(Guid.Empty));
        }
        public void moveTask(Guid taskid, String status)
        {
            currBoard.moveTask(taskid, status);
        }
        public Boolean LimitColumn(int limit, String status)
        {
            if (limit > 0 && currBoard.ChangeLimit(limit, status))
            {
                return true;
            }
            return false;
        }
        public Boolean RemoveLimit(string status)
        {
            if (currBoard.ChangeLimit(-1, status))
            {
                return true;
            }
            return false;
        }

        public Boolean EditTask(Guid taskid,String status, String title, String text, DateTime due_date)
        {
            return currBoard.editTask(taskid,status,title, text, due_date);
        }

        public ModelBoard getBoard()
        {
            List<Column> colList = currBoard.getColumns();
            List<ModelColumn> modColList = new List<ModelColumn>();
            foreach (Column col in colList)
            {
                List<Task> tasks = col.getTasks();
                List<ModelTask> modTaskList = new List<ModelTask>();
                foreach (Task task in tasks)
                {
                    modTaskList.Add(new ModelTask(task.getTaskid(), task.getTitle(), task.getText(), col.getName(), 
                                                    task.getCreationTime(), task.getDueDate()));
                }
                modColList.Add(new ModelColumn(col.getName(), modTaskList, col.getLimit()));
            }
            ModelBoard modboard = new ModelBoard(modColList);

            return modboard;
        }


        public void printBoard()
        {
            Console.WriteLine(currBoard.ToString());
        }
        public Boolean addColumn(String name)
        {
            return currBoard.addColumn(name);
        }
        public Boolean removeColumn(String name)
        {
            return currBoard.removeColumn(name);
        }
        public Boolean swapColumns(String name,bool dir)
        {
            return currBoard.columnSwap(name,dir);
        }
        public Boolean renameColumn(String curr, String name)
        {
            return currBoard.renameColumn(curr, name);
        }

    }
}
