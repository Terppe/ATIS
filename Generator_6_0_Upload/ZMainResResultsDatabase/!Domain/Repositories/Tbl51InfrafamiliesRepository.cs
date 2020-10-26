using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl51InfrafamiliesRepository : ITbl51InfrafamiliesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl51Infrafamily> Tbl51Infrafamilies     {         
            get { return Entities.Tbl51Infrafamilies; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl51Infrafamily> FindAll()    {         
            return Entities.Tbl51Infrafamilies; 
        }   

            public IQueryable<Tbl51Infrafamily> FindAllSort()    {
            return from d in Entities.Tbl51Infrafamilies
                   orderby d.InfrafamilyName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl51Infrafamily Get(int id)        {
            return Entities.Tbl51Infrafamilies.FirstOrDefault(d => d.InfrafamilyID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl51Infrafamily tbl51Infrafamily)    {
            Entities.Tbl51Infrafamilies.AddObject(tbl51Infrafamily);           
        }

        public void Delete(Tbl51Infrafamily tbl51Infrafamily)    {
            Entities.Tbl51Infrafamilies.DeleteObject(tbl51Infrafamily);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

