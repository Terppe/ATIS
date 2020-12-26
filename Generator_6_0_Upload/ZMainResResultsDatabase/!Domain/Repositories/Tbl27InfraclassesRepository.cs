using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  30.12.2011  18:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl27InfraclassesRepository : ITbl27InfraclassesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl27Infraclass> Tbl27Infraclasses     {         
            get { return Entities.Tbl27Infraclasses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl27Infraclass> FindAll()    {         
            return Entities.Tbl27Infraclasses; 
        }   

            public IQueryable<Tbl27Infraclass> FindAllSort()    {
            return from d in Entities.Tbl27Infraclasses
                   orderby d.InfraclassName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl27Infraclass Get(int id)        {
            return Entities.Tbl27Infraclasses.FirstOrDefault(d => d.InfraclassID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl27Infraclass tbl27Infraclass)    {
            Entities.Tbl27Infraclasses.AddObject(tbl27Infraclass);           
        }

        public void Delete(Tbl27Infraclass tbl27Infraclass)    {
            Entities.Tbl27Infraclasses.DeleteObject(tbl27Infraclass);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

