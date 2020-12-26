using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl63InfratribussesRepository : ITbl63InfratribussesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl63Infratribus> Tbl63Infratribusses     {         
            get { return Entities.Tbl63Infratribusses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl63Infratribus> FindAll()    {         
            return Entities.Tbl63Infratribusses; 
        }   

            public IQueryable<Tbl63Infratribus> FindAllSort()    {
            return from d in Entities.Tbl63Infratribusses
                   orderby d.InfratribusName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl63Infratribus Get(int id)        {
            return Entities.Tbl63Infratribusses.FirstOrDefault(d => d.InfratribusID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl63Infratribus tbl63Infratribus)    {
            Entities.Tbl63Infratribusses.AddObject(tbl63Infratribus);           
        }

        public void Delete(Tbl63Infratribus tbl63Infratribus)    {
            Entities.Tbl63Infratribusses.DeleteObject(tbl63Infratribus);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

