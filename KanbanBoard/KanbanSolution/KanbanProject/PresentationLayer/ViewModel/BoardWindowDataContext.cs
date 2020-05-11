using KanbanProject.BusinessLogicLayer;
using KanbanProject.InterfaceLayer;
using KanbanProject.InterfaceLayer.ModelObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using KanbanProject.PresentationLayer;
using System.Windows;

namespace KanbanProject.PresentationLayer.ViewModel
{
     public class BoardWindowDataContext : INotifyPropertyChanged
    {
        string searchTerm = "";
        public string SearchTerm
        {
            get
            {
                return searchTerm;
            }
            set
            {
                searchTerm = value;
                UpdateFilter();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SearchTerm"));
            }
        }
        private List<String> boardNames;
        public List<String> BoardNames
        {
            get { return boardNames; }
            set { boardNames = value; }
        }
        private String selectedBoard;
        public String SelectedBoard
        {
            get { return selectedBoard; }
            set
            {
                selectedBoard = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedBoard"));
            }
        }

        private BoardWindowColumn selected;
        public BoardWindowColumn Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Selected"));
            }
        }

        private ICollectionView gridView;
        public ICollectionView GridView
        {
            get
            {
                return gridView;
            }
            set
            {
                gridView = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("GridView"));
            }
        }

        private ObservableCollection<BoardWindowColumn> tasks;
        public ObservableCollection<BoardWindowColumn> Tasks
        {
            get
            {
                return tasks;
            }
            set
            {
                tasks = value;
                UpdateFilter();
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Tasks"));
            }
        }

        private void UpdateFilter()
        {
            CollectionViewSource cvs = new CollectionViewSource() { Source = tasks };
            ICollectionView cv = cvs.View;
            cv.Filter = o =>
            {
                BoardWindowColumn p = o as BoardWindowColumn;
                return p.text.ToUpper().Contains(SearchTerm.ToUpper())
                        || p.title.ToUpper().Contains(SearchTerm.ToUpper());
            };
            GridView = cv;
        }

        BoardInter board;
        public BoardInter Board
        {
            set
            {
                board = value;
                ShowTheard();
            }
        }

        public BoardWindowDataContext(BoardInter board)
        {
            this.board = board;
            BoardNames = board.getBoardNames();
            ShowTheard();
        }

        public void ShowTheard()
        {
            ObservableCollection<BoardWindowColumn> taskRows = new ObservableCollection<BoardWindowColumn>();
            foreach (ModelColumn c in board.getBoard().columns)
            {
                foreach (ModelTask m in c.tasks)
                {
                    taskRows.Add(new BoardWindowColumn(m, c.limit));
                }
                if (c.tasks.Count==0)
                {
                    taskRows.Add(new BoardWindowColumn(c.name,c.limit));
                }
            }
            Tasks = taskRows;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
    }
}

