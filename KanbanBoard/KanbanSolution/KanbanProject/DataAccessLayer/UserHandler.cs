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

    class UserHandler
    {
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SQLiteConnection connection;
        private SQLiteCommand command;
        private String sql_query;
        public UserHandler()
        {
            string database_name = "KanbanDataBase.db";
            String connetion_string = $"Data Source={database_name};Version=3;";
            connection = new SQLiteConnection(connetion_string);
            command = new SQLiteCommand(null, connection);
        }


        public List<NumbUser> readUsers()
        {
            List<NumbUser> users = new List<NumbUser>();
            try
            {
                connection.Open();
                string sql = "SELECT * FROM Users";
                SQLiteCommand c = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = c.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new NumbUser((String)reader.GetValue(0), (String)reader.GetValue(1)));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());

            }
            finally
            {
                connection.Close();
            }
            return users;
        }

        public void writeUser(NumbUser user)
        {
            try
            {
                connection.Open();
                command = new SQLiteCommand(null, connection);

                command.CommandText =
                            "INSERT INTO Users (email,password) " +
                            "VALUES (@email, @password)";
                SQLiteParameter email_param = new SQLiteParameter(@"email", user.email);
                SQLiteParameter password_param = new SQLiteParameter(@"password", user.password);
                command.Parameters.Add(email_param);
                command.Parameters.Add(password_param);
                command.Connection = connection;
                command.Prepare();
                command.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error");
                Console.WriteLine(ex.ToString());

            }
            finally
            {
                connection.Close();
            }
            
        }
    }
}

        /*
        private List<NumbUser> users;

        public UserHandler()
        {
            users = null;
        }
        public UserHandler(List<KeyValuePair<String,String>> list)
        {
            users = new List<NumbUser>();
            if (list != null)
            {
                foreach (KeyValuePair<String,String> pair in list)
                {

                    users.Add(new NumbUser(pair.Key, pair.Value));
                }
            }
            else
            {
                log.Error("UserHandler initialized without any data");
            }
        }
        public UserHandler(List<NumbUser> users)
        {
            this.users = users;
        }

        public void writeUsers()
        {
            if( users != null)
            {
                
                Stream myFileStream = File.Create("userData.bin");
                BinaryFormatter serializes = new BinaryFormatter();
                try
                {
                    serializes.Serialize(myFileStream, users);
                }
                catch (System.Runtime.Serialization.SerializationException)
                {
                    log.Error("Unable to write users to disk");
                }

                myFileStream.Close();
            }

        }

        public List<NumbUser> readUsers()
        {
            if (File.Exists("userData.bin"))
            {
                Stream myOtherFileStream = File.OpenRead("userData.bin");
                BinaryFormatter deserializer = new BinaryFormatter();
                try
                {
                    users = (List<NumbUser>)deserializer.Deserialize(myOtherFileStream);
                }
                catch (System.Runtime.Serialization.SerializationException)
                {
                    log.Error("Unable to load users to disk");
                    
                }
                myOtherFileStream.Close();
            }
            return users;
        }

    */
 
