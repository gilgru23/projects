using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace KanbanProject.DataAccessLayer
{
    class BoardHandler
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SQLiteConnection connection;
        private SQLiteCommand command;
        public BoardHandler()
        {
            string database_name = "KanbanDataBase.db";
            String connetion_string = $"Data Source={database_name};Version=3;";
            connection = new SQLiteConnection(connetion_string);
        }


        public Boolean WriteTask(NumbTask task,string column_id)
        {
            String sqlQuery=
                        "REPLACE INTO Tasks (id,title,text,creation_time,due_date,column_id) " +
                        "VALUES (@taskid, @title, @text, @creation_time, @due_date, @column_id)";

            String[] parameters = { "taskid", "title", "text", "creation_time", "due_date", "column_id" };
            String[] args = { task.taskid, task.title, task.text, task.CREATION_TIME, task.Due_Date, column_id};
            return WriteElement(sqlQuery, parameters, args);
        }



        public Boolean WriteColumn(NumbColumn col,string boardID,string boardName)
        {
            String sqlQuery=
                "REPLACE INTO Columns (id,name,lim,board_id,board_name) " +
                "VALUES (@col_id, @name, @lim, @board_id, @board_name)";

            String[] parameters = { "col_id", "name", "lim", "board_id", "board_name" };
            String[] args = { col.id, col.name ,col.limit.ToString() ,boardID, boardName };
            return WriteElement(sqlQuery, parameters, args);
        }

        public Boolean WriteBoard(NumbBoard board)
        {
            String sqlQuery =
                "REPLACE INTO Boards (email_id,name) " +
                "VALUES (@email_id, @name)";

            String[] parameters = { "email_id", "name"};
            String[] args = { board.boardID , board.name};
            return WriteElement(sqlQuery, parameters, args);
        }

        private Boolean WriteElement(String sqlQuery, String[] parameters, String[] args)
        {
            Boolean isWritten = false;
            try
            {
                connection.Open();
                command = new SQLiteCommand(sqlQuery, connection);
                int i = 0;
                foreach (String param in parameters)
                {
                    command.Parameters.Add(new SQLiteParameter(@parameters[i],args[i]));
                    i++;
                }
                command.Connection = connection;
                command.Prepare();
                command.ExecuteReader();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            finally
            {
                isWritten = connection.Changes>0;
                connection.Close();
            }
            return isWritten;
        }
        

        public List<NumbBoard> ReadBoards()
        {
            List<NumbBoard> boards = null;

            try
            {
                connection.Open();
                command = new SQLiteCommand(null, connection);


                //-------------select board command-------------//
                boards= getEmptyNumbBoardsList(); //get boards
                
                // fill boards
                //-------------select column command-------------//    
                foreach (NumbBoard board in boards)
                {
                    //get cols
                    //System.Windows.MessageBox.Show("check");
                    board.cols = getEmptyNumbColumnsList(board);

                    //fill columns
                    //-------------select task command-------------//
                    foreach (NumbColumn column in board.cols)
                    {
                       
                        column.tasks = getNumbTasksList(column);
                    }
                }
            }
            catch (Exception ex)
            {
                
                System.Windows.MessageBox.Show(ex.ToString());

            }
            finally
            {
                connection.Close();
            }
            return boards;
        }

        private List<NumbBoard> getEmptyNumbBoardsList()
        {
            List<NumbBoard> boards = new List<NumbBoard>();
            string sql = "select * from Boards";
            SQLiteCommand getBoardsComm = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = getBoardsComm.ExecuteReader();
            while (reader.Read())
            {
                boards.Add(new NumbBoard((String)reader.GetValue(0), (String)reader.GetValue(1), new List<NumbColumn>()));
            }
            return boards;
        }





        internal bool removeTask(string taskID)
        {
            return removeElement("DELETE FROM Tasks WHERE id", taskID);
        }

        public bool removeColumn(string colID)
        {
            return removeElement("DELETE FROM Columns WHERE id", colID);
        }

        internal bool removeBoard(string boardID, string name)
        {
            Boolean isRemoved = false;
            try
            {
                connection.Open();
                command = new SQLiteCommand(null, connection);
                command.CommandText =
                            "DELETE FROM Boards " +
                            "WHERE email_id = @board_id AND name = @board_name ";

                SQLiteParameter board_id_param = new SQLiteParameter(@"board_id", boardID);
                command.Parameters.Add(board_id_param);
                SQLiteParameter board_name_param = new SQLiteParameter(@"board_name", name);
                command.Parameters.Add(board_name_param);
                command.Connection = connection;
                command.ExecuteReader();

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            finally
            {
                System.Windows.MessageBox.Show("changes made (by rows): " + connection.Changes);
                isRemoved = connection.Changes > 0;
                if (isRemoved)
                {
                    isRemoved = removeColumnsInBoardDeletion(boardID, name);
                }
                connection.Close();
                }
            return isRemoved;
        }
        private Boolean removeElement(string query,string id)
        {
            Boolean isRemoved = false;
            try
            {
                connection.Open();
                command = new SQLiteCommand(null, connection);
                command.CommandText =
                            query + " = @element_id ";
                
                SQLiteParameter element_id_param = new SQLiteParameter(@"element_id", id);
                command.Parameters.Add(element_id_param);
                command.Connection = connection;
                command.ExecuteReader();
                
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            finally
            {
                System.Windows.MessageBox.Show("changes made (by rows): "+connection.Changes);
                isRemoved = connection.Changes>0;
                connection.Close();
               
            }
            return isRemoved;
        }
        internal bool removeColumnsInBoardDeletion(string boardID, string name)
        {
            Boolean isRemoved = false;
            try
            {
                command = new SQLiteCommand(null, connection);
                command.CommandText =
                            "SELECT Columns.id FROM Columns " +
                            "WHERE board_id = @board_id AND board_name = @name ";

                SQLiteParameter board_id_param = new SQLiteParameter(@"board_id", boardID);
                command.Parameters.Add(board_id_param);
                SQLiteParameter board_name_param = new SQLiteParameter(@"name", name);
                command.Parameters.Add(board_name_param);
                command.Connection = connection;
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    removeTasksInColumnDeletion(reader.GetValue(0).ToString());
                }
                reader.Close();
                ColumnRemoving(boardID, name);
               }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            finally
            {
                isRemoved = connection.Changes > 0;
            }
            return isRemoved;
        }
        public void ColumnRemoving(string boardID, string name)
        {
            command = new SQLiteCommand(null, connection);
            command.CommandText =
                           "DELETE FROM Columns " +
                           "WHERE board_id = @board_id AND board_name = @name ";

            SQLiteParameter Column_id_param = new SQLiteParameter(@"board_id", boardID);
            command.Parameters.Add(Column_id_param);
            SQLiteParameter Column_name_param = new SQLiteParameter(@"name", name);
            command.Parameters.Add(Column_name_param);
            command.Connection = connection;
            command.ExecuteReader();
        }
        private bool removeTasksInColumnDeletion(String colID)
        {
            Boolean isRemoved = false;
            try
            {
                command = new SQLiteCommand(null, connection);
            
                command.CommandText =
                            "DELETE FROM Tasks WHERE column_id = @col_id ";

                SQLiteParameter element_id_param = new SQLiteParameter(@"col_id", colID);
                command.Parameters.Add(element_id_param);
                command.Connection = connection;
                command.ExecuteReader();
                
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            finally
            {
              
                isRemoved = connection.Changes > 0;
            }
            return isRemoved;
        }


        private List<NumbColumn> getEmptyNumbColumnsList(NumbBoard board)
        {
            List<NumbColumn> columns = new List<NumbColumn>();
            string sql = "SELECT * FROM Columns " +
                         "WHERE Columns.board_id = @param_board_id AND Columns.board_name = @param_board_name ";

            SQLiteCommand getColumnsComm = new SQLiteCommand(sql, connection);
            SQLiteParameter param_board_id = new SQLiteParameter(@"param_board_id", board.boardID);
            getColumnsComm.Parameters.Add(param_board_id);
            SQLiteParameter param_board_name = new SQLiteParameter(@"param_board_name", board.name);
            getColumnsComm.Parameters.Add(param_board_name);
            getColumnsComm.Connection = connection;
            getColumnsComm.Prepare();
            SQLiteDataReader reader = getColumnsComm.ExecuteReader();
            while (reader.Read())
            {
                columns.Add(new NumbColumn(reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), (int)reader.GetInt64(2), new List<NumbTask>()));
            }
            return columns;
        }

        private List<NumbTask> getNumbTasksList(NumbColumn col)
        {
            List<NumbTask> tasks = new List<NumbTask>();
            string sql = "SELECT * FROM tasks "+ /*Tasks.id,Tasks.title,Tasks.text,Tasks.creation_time,Tasks.due_date " +
                         "FROM (Tasks INNER JOIN Columns ON Tasks.column_id=Columns.id) " +*/
                         "WHERE column_id = @param_col_id ";               
            SQLiteCommand getTasksComm = new SQLiteCommand(sql, connection);
            SQLiteParameter param_col_id = new SQLiteParameter(@"param_col_id", col.id);
            getTasksComm.Parameters.Add(param_col_id);
            getTasksComm.Connection = connection;
            getTasksComm.Prepare();
            SQLiteDataReader reader = getTasksComm.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add(new NumbTask((String)reader.GetValue(1), (String)reader.GetValue(2), (String)reader.GetValue(3), (String)reader.GetValue(0), (String)reader.GetValue(4)));
            }
            return tasks;
        }

        /*
        private NumbBoard board;

        private List<NumbBoard> boards;

        public BoardHandler(List<KeyValuePair<String, List<List<String>>>> input)
        {
            boards = new List<NumbBoard>();
            foreach (KeyValuePair<String, List<List<String>>> pair in input)
            {
                boards.Add(new NumbBoard(pair.Value, pair.Key));    
            }
        }
        public BoardHandler()
        {
            this.boards = null;
        }

        // missing another constructor that creates an instance with a field holding a list of NumbBoards

        public void  writeBoards()
        {
            Stream myFileStream = File.Create("boardData.bin");
            BinaryFormatter serializes = new BinaryFormatter();

                serializes.Serialize(myFileStream, boards);

            myFileStream.Close();
        }

        public List<NumbBoard> ReadBoards()
        {
            if (File.Exists("boardData.bin"))
            {
                Stream myOtherFileStream = File.OpenRead("boardData.bin");
                BinaryFormatter deserializer = new BinaryFormatter();
                try
                {
                    boards = (List<NumbBoard>)deserializer.Deserialize(myOtherFileStream);
                }
               catch (System.Runtime.Serialization.SerializationException)
               {
                   boards = new List<NumbBoard>();
                    log.Error("An error has occurred while reading board-files from disk");
                }
                myOtherFileStream.Close();
            }
            return boards;
        }
        */
    }
}
