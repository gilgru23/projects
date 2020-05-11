using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.PresentationLayer.ViewModel
{
    class ColumnLimitDataContext : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        int limit;
        public int Limit
        {
            get
            {
                return limit;
            }
            set
            {
                limit = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("title"));
            }
        }
        Boolean isLimited;
        public Boolean IsLimited
        {
            get
            {
                return isLimited;
            }
            set
            {
                isLimited= value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("title"));
            }
        }

        public ColumnLimitDataContext( Boolean isLimited, int limit)
        {
            this.limit = limit;
            this.isLimited = isLimited;
        }
    }
}
