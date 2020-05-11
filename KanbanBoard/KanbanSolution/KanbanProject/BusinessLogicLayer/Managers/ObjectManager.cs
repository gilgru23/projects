using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanbanProject.BusinessLogicLayer.Managers
{

     public abstract class ObjectManager<T> where T : Iidentifiable
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        protected SortedDictionary<String, T> dataset; 

        public ObjectManager()
        {
            dataset = null;
        }
        public ObjectManager(IList<T> set)
        {
            foreach (T t in set)
            {   
                try
                {
                    dataset.Add(t.getID(), t);
                }
                catch (ArgumentException)
                {
                    log.Error("Encountered duplicate data while Loading data from storage");
                }
            }
        }
        
        public abstract void loadData();
       
        public Boolean addElement(T t)
        {
            bool added = true;
            try
            {
                dataset.Add(t.getID(), t);
            }

            catch (ArgumentException)
            {
                log.Error("Encountered duplicate data while Loading data from storage");
                added = false;
            }
            return added;
        }

        public Boolean replaceElement(T t)
        {
            bool rplcd = false;
            try
            {
               dataset[t.getID()]= t;
                
            }
            catch (ArgumentException)
            {
                log.Error("Could not find required elemnt");
                rplcd = false;
            }

            return rplcd;
        }

        //////
        public void addOrReplace(T t)
        {
            bool added = true;
            if (dataset != null)
            {
                try
                {
                    dataset.Add(t.getID(), t);
                }
                catch (ArgumentException)
                {
                    dataset[t.getID()] = t;
                }
            }
            else
            {
                added = false;
                log.Error("Attempted accessing data before it was loaded");
            }
        }
    }
}
