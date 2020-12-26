using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl30LegiosRepository : ITbl30LegiosRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl30Legio> Tbl30Legios     {         
            get { return Entities.Tbl30Legios; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl30Legio> FindAll()    {         
            return Entities.Tbl30Legios; 
        }   

            public IQueryable<Tbl30Legio> FindAllSort()    {
            return from d in Entities.Tbl30Legios
                   orderby d.LegioName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl30Legio Get(int id)        {
            return Entities.Tbl30Legios.FirstOrDefault(d => d.LegioID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl30Legio tbl30Legio)    {
            Entities.Tbl30Legios.AddObject(tbl30Legio);           
        }

        public void Delete(Tbl30Legio tbl30Legio)    {
            Entities.Tbl30Legios.DeleteObject(tbl30Legio);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

