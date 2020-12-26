using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  29.12.2011  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl90RefAuthorsRepository : ITbl90RefAuthorsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl90RefAuthor> Tbl90RefAuthors     {         
            get { return Entities.Tbl90RefAuthors; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl90RefAuthor> FindAll()    {         
            return Entities.Tbl90RefAuthors; 
        }   

            public IQueryable<Tbl90RefAuthor> FindAllSort()    {
            return from d in Entities.Tbl90RefAuthors
                   orderby d.RefAuthorName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl90RefAuthor Get(int id)        {
            return Entities.Tbl90RefAuthors.FirstOrDefault(d => d.RefAuthorID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl90RefAuthor tbl90RefAuthor)    {
            Entities.Tbl90RefAuthors.AddObject(tbl90RefAuthor);           
        }

        public void Delete(Tbl90RefAuthor tbl90RefAuthor)    {
            Entities.Tbl90RefAuthors.DeleteObject(tbl90RefAuthor);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

