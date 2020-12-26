using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  20.12.2011  18:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl24SubclassesRepository : ITbl24SubclassesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl24Subclass> Tbl24Subclasses     {         
            get { return Entities.Tbl24Subclasses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl24Subclass> FindAll()    {         
            return Entities.Tbl24Subclasses; 
        }   

            public IQueryable<Tbl24Subclass> FindAllSort()    {
            return from d in Entities.Tbl24Subclasses
                   orderby d.SubclassName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl24Subclass Get(int id)        {
            return Entities.Tbl24Subclasses.FirstOrDefault(d => d.SubclassID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl24Subclass tbl24Subclass)    {
            Entities.Tbl24Subclasses.AddObject(tbl24Subclass);           
        }

        public void Delete(Tbl24Subclass tbl24Subclass)    {
            Entities.Tbl24Subclasses.DeleteObject(tbl24Subclass);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

