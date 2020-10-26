using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  3.1.2012  12:32      -->  

namespace Atis.Domain.Repositories      {  
    public class TblCountersRepository : ITblCountersRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<TblCounter> TblCounters     {         
            get { return Entities.TblCounters; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<TblCounter> FindAll()    {         
            return Entities.TblCounters; 
        }   

            public IQueryable<TblCounter> FindAllSort()    {
            return from d in Entities.TblCounters
                   orderby d.CounterName
                   select d;
        }                                                                                                                                                                   
  
          public TblCounter Get(int id)        {
            return Entities.TblCounters.FirstOrDefault(d => d.CounterID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(TblCounter tblCounter)    {
            Entities.TblCounters.AddObject(tblCounter);           
        }

        public void Delete(TblCounter tblCounter)    {
            Entities.TblCounters.DeleteObject(tblCounter);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
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

