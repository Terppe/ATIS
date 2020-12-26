using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  15.03.2012  18:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl21ClassesRepository : ITbl21ClassesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl21Class> Tbl21Classes     {         
            get { return Entities.Tbl21Classes; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl21Class> FindAll()    {         
            return Entities.Tbl21Classes; 
        }   

            public IQueryable<Tbl21Class> FindAllSort()    {
            return from d in Entities.Tbl21Classes
                   orderby d.ClassName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl21Class Get(int id)        {
            return Entities.Tbl21Classes.FirstOrDefault(d => d.ClassID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl21Class tbl21Class)    {
            Entities.Tbl21Classes.AddObject(tbl21Class);           
        }

        public void Delete(Tbl21Class tbl21Class)    {
            Entities.Tbl21Classes.DeleteObject(tbl21Class);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

