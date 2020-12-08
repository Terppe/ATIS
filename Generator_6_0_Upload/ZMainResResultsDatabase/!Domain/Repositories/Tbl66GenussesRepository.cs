using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  8.1.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl66GenussesRepository : ITbl66GenussesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl66Genus> Tbl66Genusses     {         
            get { return Entities.Tbl66Genusses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl66Genus> FindAll()    {         
            return Entities.Tbl66Genusses; 
        }   

            public IQueryable<Tbl66Genus> FindAllSort()    {
            return from d in Entities.Tbl66Genusses
                   orderby d.GenusName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl66Genus Get(int id)        {
            return Entities.Tbl66Genusses.FirstOrDefault(d => d.GenusID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl66Genus tbl66Genus)    {
            Entities.Tbl66Genusses.AddObject(tbl66Genus);           
        }

        public void Delete(Tbl66Genus tbl66Genus)    {
            Entities.Tbl66Genusses.DeleteObject(tbl66Genus);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

