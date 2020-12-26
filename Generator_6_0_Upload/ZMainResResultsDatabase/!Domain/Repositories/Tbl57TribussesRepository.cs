using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl57TribussesRepository : ITbl57TribussesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl57Tribus> Tbl57Tribusses     {         
            get { return Entities.Tbl57Tribusses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl57Tribus> FindAll()    {         
            return Entities.Tbl57Tribusses; 
        }   

            public IQueryable<Tbl57Tribus> FindAllSort()    {
            return from d in Entities.Tbl57Tribusses
                   orderby d.TribusName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl57Tribus Get(int id)        {
            return Entities.Tbl57Tribusses.FirstOrDefault(d => d.TribusID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl57Tribus tbl57Tribus)    {
            Entities.Tbl57Tribusses.AddObject(tbl57Tribus);           
        }

        public void Delete(Tbl57Tribus tbl57Tribus)    {
            Entities.Tbl57Tribusses.DeleteObject(tbl57Tribus);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

