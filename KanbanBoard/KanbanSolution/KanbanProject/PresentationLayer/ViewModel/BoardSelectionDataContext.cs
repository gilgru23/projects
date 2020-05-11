using KanbanProject.InterfaceLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.PresentationLayer.ViewModel
{
    class BoardSelectionDataContext : INotifyPropertyChanged
    {
        
        public BoardSelectionDataContext(BoardInter bi)
        {
            BoardNames = bi.getBoardNames();
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

        private String nameText;
        public string NameText
        {
            get
            {
                return nameText;
            }
            set
            {
                nameText = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("nameText"));
            }
        }

        private List<String> boardNames;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<String> BoardNames
        {
            get { return boardNames; }
            set
            { boardNames = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("boardNames"));
            }
        }
   
    }
}
