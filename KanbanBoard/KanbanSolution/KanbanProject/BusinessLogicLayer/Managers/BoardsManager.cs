using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.BusinessLogicLayer.Managers
{
    using DataAccessLayer;
    public class BoardsManager //<T> : ObjectManager<Board> where T : Iidentifiable
    {
        //--------------------------fields-------------------------------------------------//
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //--------------------------constructors-------------------------------------------//

        private List<Board> dataset;
        private BoardHandler boardStorage;

        public BoardsManager()
        {
            this.dataset = new List<Board>();
            boardStorage = new BoardHandler();
        }
        
        //--------------------------abstract methods implemenation--------------------------//
        public void loadData()
        {
            dataset = new List<Board>();
            List<NumbBoard> list = new List<NumbBoard>();
            list = boardStorage.ReadBoards();
            if (list != null)
            {
                foreach (NumbBoard numbBoard in list)
                {
                    List<Column> colList = new List<Column>();
                    foreach (NumbColumn numbCol in numbBoard.cols)
                    {
                        List<Task> tasks = new List<Task>();
                        foreach (NumbTask numbTask in numbCol.tasks)
                        {
                            DateTime due_date;
                            if (!DateTime.TryParse(numbTask.Due_Date, out due_date))
                            {
                                due_date = DateTime.MinValue;
                            }
                            DateTime crt_date;
                            if (!DateTime.TryParse(numbTask.CREATION_TIME, out crt_date))
                            {
                                crt_date = DateTime.MinValue;
                            }

                            Guid taskid = Guid.Parse(numbTask.taskid);
                            Task t = new Task(numbTask.title, numbTask.text, due_date, taskid, crt_date);
                            tasks.Add(t);
                        }
                        Guid colid = Guid.Parse(numbCol.id);
                        Column col = new Column(colid, numbCol.name, numbCol.limit,tasks);

                        colList.Add(col);
                    }
                    //-------------------------add to dataset------------------
                        dataset.Add(new Board(numbBoard.boardID,numbBoard.name, colList));
                }
            }
            else
            {
                log.Error("Unable to load Boards from disk");
            }
        }
        /*
        public override void saveAllData() //might be better to return boolean
        {
            
            if (dataset!=null)
            {
                List<KeyValuePair<String,List<List<String>>>> boardList = new List<KeyValuePair<String, List<List<String>>>>();
                foreach (KeyValuePair<String,Board> pair in dataset)
                {
                    boardList.Add(pair.Value.GetPair());
                }
               
                BoardHandler bh = new BoardHandler(boardList);
                bh.writeBoards();
            }
            else
            {
                log.Error("Failed save attempt-No board data was found");
            }


        }
        */

        //------------------------------------save elements---------------------------------//
        public Boolean saveBoard(String boardID,String name)
        {
            NumbBoard nb= new NumbBoard(boardID,name,null);
            return boardStorage.WriteBoard(nb);
        }
        public Boolean saveColumn(Guid id, String name, int limit, String board_id,String board_name)
        {
            NumbColumn ncol = new NumbColumn(id.ToString(),name,limit,null);
            return boardStorage.WriteColumn(ncol,board_id,board_name);
        }
        public Boolean saveTask(String title, String text, String creation_time, Guid taskid, String due_date, Guid parent)
        {
            String colID = parent.ToString();
            NumbTask ntask = new NumbTask(title,text,creation_time,taskid.ToString(),due_date );
            return boardStorage.WriteTask(ntask,colID);
        }
        //------------------------------remove elements-------------------------------------//
        public Boolean removeBoard(String boardID, String name)
        {
            return boardStorage.removeBoard(boardID, name);
        }
        public Boolean removeColumn(Guid colID)
        {
            return boardStorage.removeColumn(colID.ToString());
        }
        public Boolean removeTask(Guid taskID)
        {
            return boardStorage.removeTask(taskID.ToString());
        }
        //---------------------------------additional methods-------------------------------//
        public BoardsCollection getBoardCollection(String email)
        {
            List<Board> myboards = dataset.FindAll(brd => email.Equals(brd.getID()));
            if (myboards.Count == 0)
            {
                Board curr = new Board(email,"Main Board");
                dataset.Add(curr);
                myboards.Add(curr);
                saveBoard(email, "Main Board");
            }
            BoardsCollection boardscoll = new BoardsCollection(email,myboards);
            return boardscoll;
        }

    }
}
