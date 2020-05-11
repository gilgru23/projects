using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.DataAccessLayer
{
    public class NumbBoard
    {
        public string boardID;
        public string name;
        public List<NumbColumn> cols;

        public NumbBoard(String boardID, String name, List<NumbColumn> cols)
        {
            this.boardID = boardID;
            this.name = name;
            this.cols = cols;
        }
    }
}
