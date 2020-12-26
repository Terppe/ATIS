using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  18.03.2014  12:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl18SuperclassesRepository : ITbl18SuperclassesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl18Superclass> Tbl18Superclasses     {         
            get { return Entities.Tbl18Superclasses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl18Superclass> FindAll()    {         
            return Entities.Tbl18Superclasses; 
        }   

            public IQueryable<Tbl18Superclass> FindAllSort()    {
            return from d in Entities.Tbl18Superclasses
                   orderby d.SuperclassName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl18Superclass Get(int id)        {
            return Entities.Tbl18Superclasses.FirstOrDefault(d => d.SuperclassID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl18Superclass tbl18Superclass)    {
            Entities.Tbl18Superclasses.AddObject(tbl18Superclass);           
        }

        public void Delete(Tbl18Superclass tbl18Superclass)    {
            Entities.Tbl18Superclasses.DeleteObject(tbl18Superclass);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

