using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.PresentationLayer.ViewModel
{
    class TaskWindowDataContext : INotifyPropertyChanged
    {
        public TaskWindowDataContext()
        {
            title = "";
            text = "";
            due_date = DateTime.Now;
        }
        public TaskWindowDataContext(String title,String text,DateTime due_date)
        {
            this.title = title;
            this.text = text;
            this.due_date = due_date;
        }
        private String title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("title"));
            }
        }
        private String text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("text"));
            }
        }
        private DateTime due_date;
        public DateTime Due_date
        {
            get
            {
                return due_date;
            }
            set
            {
                due_date = value;

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("due_date"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
    


