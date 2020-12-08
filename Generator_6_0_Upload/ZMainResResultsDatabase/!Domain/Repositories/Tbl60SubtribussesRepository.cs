using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl60SubtribussesRepository : ITbl60SubtribussesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl60Subtribus> Tbl60Subtribusses     {         
            get { return Entities.Tbl60Subtribusses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl60Subtribus> FindAll()    {         
            return Entities.Tbl60Subtribusses; 
        }   

            public IQueryable<Tbl60Subtribus> FindAllSort()    {
            return from d in Entities.Tbl60Subtribusses
                   orderby d.SubtribusName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl60Subtribus Get(int id)        {
            return Entities.Tbl60Subtribusses.FirstOrDefault(d => d.SubtribusID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl60Subtribus tbl60Subtribus)    {
            Entities.Tbl60Subtribusses.AddObject(tbl60Subtribus);           
        }

        public void Delete(Tbl60Subtribus tbl60Subtribus)    {
            Entities.Tbl60Subtribusses.DeleteObject(tbl60Subtribus);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

