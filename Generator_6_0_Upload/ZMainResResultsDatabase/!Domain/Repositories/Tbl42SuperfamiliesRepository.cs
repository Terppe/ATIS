using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl42SuperfamiliesRepository : ITbl42SuperfamiliesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl42Superfamily> Tbl42Superfamilies     {         
            get { return Entities.Tbl42Superfamilies; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl42Superfamily> FindAll()    {         
            return Entities.Tbl42Superfamilies; 
        }   

            public IQueryable<Tbl42Superfamily> FindAllSort()    {
            return from d in Entities.Tbl42Superfamilies
                   orderby d.SuperfamilyName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl42Superfamily Get(int id)        {
            return Entities.Tbl42Superfamilies.FirstOrDefault(d => d.SuperfamilyID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl42Superfamily tbl42Superfamily)    {
            Entities.Tbl42Superfamilies.AddObject(tbl42Superfamily);           
        }

        public void Delete(Tbl42Superfamily tbl42Superfamily)    {
            Entities.Tbl42Superfamilies.DeleteObject(tbl42Superfamily);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

