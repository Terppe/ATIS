using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  28.12.2011  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl78NamesRepository : ITbl78NamesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl78Name> Tbl78Names     {         
            get { return Entities.Tbl78Names; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl78Name> FindAll()    {         
            return Entities.Tbl78Names; 
        }   

            public IQueryable<Tbl78Name> FindAllSort()    {
            return from d in Entities.Tbl78Names
                   orderby d.NameName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl78Name Get(int id)        {
            return Entities.Tbl78Names.FirstOrDefault(d => d.NameID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl78Name tbl78Name)    {
            Entities.Tbl78Names.AddObject(tbl78Name);           
        }

        public void Delete(Tbl78Name tbl78Name)    {
            Entities.Tbl78Names.DeleteObject(tbl78Name);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

