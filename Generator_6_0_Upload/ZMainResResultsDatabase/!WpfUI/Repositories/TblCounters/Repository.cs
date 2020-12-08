using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  3.1.2012  12:32      -->  

namespace Atis.WpfUi.Repositories      {  
    public class TblCountersRepository : ITblCountersRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<TblCounter> TblCounters     {         
            get { return _entities.TblCounters; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<TblCounter> FindAll()    {         
            return _entities.TblCounters; 
        }   

            public IQueryable<TblCounter> FindAllSort()    {
            return from d in _entities.TblCounters
                   orderby d.CounterName
                   select d;
        }                                                                                                                                                                   
  
          public TblCounter Get(int id)        {
            return _entities.TblCounters.FirstOrDefault(d => d.CounterID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(TblCounter tblCounter)    {
            _entities.TblCounters.Add(tblCounter);           
        }

        public void Delete(TblCounter tblCounter)    {
            _entities.TblCounters.Remove(tblCounter);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   

        /// <summary>
        /// Counter für alle Tabellen
        /// </summary>
        /// <returns></returns>
        public int Counter()  {
            var count = (from p in Entities.TblCounters
                         select p).FirstOrDefault();
            count.Zahl = count.Zahl + 1;
            var iCount = Convert.ToInt32(count.Zahl);
            iCount = iCount + 1;
      //      Add(count);
            Save(); //save new number into tblCounters
            
            return iCount;
        }           
 
    }
}   

