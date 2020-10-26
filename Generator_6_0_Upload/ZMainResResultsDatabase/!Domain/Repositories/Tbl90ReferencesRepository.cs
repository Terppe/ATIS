using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl90ReferencesRepository : ITbl90ReferencesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl90Reference> Tbl90References     {         
            get { return Entities.Tbl90References; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl90Reference> FindAll()    {         
            return Entities.Tbl90References; 
        }   

         public IQueryable<Tbl90Reference> FindAllSort()    {
            return from d in Entities.Tbl90References
                   orderby d.FiSpeciesID
                   select d;
        }                                                                                                                                                                   
  
          public Tbl90Reference Get(int id)        {
            return Entities.Tbl90References.FirstOrDefault(d => d.ReferenceID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl90Reference tbl90Reference)    {
            Entities.Tbl90References.AddObject(tbl90Reference);           
        }

        public void Delete(Tbl90Reference tbl90Reference)    {
            Entities.Tbl90References.DeleteObject(tbl90Reference);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

