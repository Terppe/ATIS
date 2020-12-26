using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl54SupertribussesRepository : ITbl54SupertribussesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl54Supertribus> Tbl54Supertribusses     {         
            get { return Entities.Tbl54Supertribusses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl54Supertribus> FindAll()    {         
            return Entities.Tbl54Supertribusses; 
        }   

            public IQueryable<Tbl54Supertribus> FindAllSort()    {
            return from d in Entities.Tbl54Supertribusses
                   orderby d.SupertribusName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl54Supertribus Get(int id)        {
            return Entities.Tbl54Supertribusses.FirstOrDefault(d => d.SupertribusID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl54Supertribus tbl54Supertribus)    {
            Entities.Tbl54Supertribusses.AddObject(tbl54Supertribus);           
        }

        public void Delete(Tbl54Supertribus tbl54Supertribus)    {
            Entities.Tbl54Supertribusses.DeleteObject(tbl54Supertribus);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

