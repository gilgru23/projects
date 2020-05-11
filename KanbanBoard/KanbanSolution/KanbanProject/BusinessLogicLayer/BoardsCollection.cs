using KanbanProject.BusinessLogicLayer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.BusinessLogicLayer
{
    public class BoardsCollection
    {
        private List<Board> myboards;
        private String Id;

        public BoardsCollection(String Id)
        {
            this.myboards = new List<Board>();
            this.Id = Id;
        }
        public BoardsCollection(String Id, List<Board> myboards)
        {
            this.myboards = myboards;
            this.Id = Id;
        }


        internal Board Add(String boardName)
        {
            if (boardName != null && boardName.Length>0 && !myboards.Exists(brd => boardName.Equals(brd.Name)))
            {
                Board toAdd = new Board(Id, boardName);
                myboards.Add(toAdd);
                toAdd.saveBoard();
                return toAdd;
            }
            else
                return null;
        }

        internal int getSize()
        {
            return myboards.Count;
        }

        internal Board getBoardAt(int i)
        {
            if (i < myboards.Count & i >= 0)
                return myboards[i];
            else
                return null;
        }

        public Boolean removeBoard(String toRemove)
        {
            if (toRemove != null)//&& 1 == myboards.RemoveAll(brd => toRemove.Equals(brd.Name)))
            {
                int i = myboards.FindIndex(brd => toRemove.Equals(brd.Name));
                if (i >= 0)
                {
                    Board toDelete = myboards[i];
                    myboards.Remove(toDelete);
                    toDelete.deleteBoard();
                    return true;
                }
            }
            return false;
        }

        internal Board getByName(string board)
        {
            if (board != null)
            {
                int i = myboards.FindIndex(brd => board.Equals(brd.Name));
                if (i > -1)
                    return myboards[i];
            }
            return null;
        }

        internal List<String> getBoardNames()
        {
            List<String> names = new List<string>();
            foreach (Board brd in myboards)
            {
                names.Add(brd.Name);
            }
            return names;
        }

        public Boolean renameBoard(String currName, String newName)
        {
            if (currName != null && newName != null && newName.Length > 0)
            {
                int toEdit = myboards.FindIndex(brd => currName.Equals(brd.Name));
                if (toEdit > -1)
                {
                    myboards[toEdit].Name = newName;
                    return true;
                }
            }
            return false;
        }
        public void setManager(BoardsManager manager)
        {
            foreach (Board board in myboards)
            {
                board.setManager(manager);
            }
        }
    }
}

