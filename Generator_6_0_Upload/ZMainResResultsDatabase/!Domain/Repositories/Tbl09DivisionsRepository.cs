using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  16.03.2014  12:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl09DivisionsRepository : ITbl09DivisionsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl09Division> Tbl09Divisions     {         
            get { return Entities.Tbl09Divisions; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl09Division> FindAll()    {         
            return Entities.Tbl09Divisions; 
        }   

            public IQueryable<Tbl09Division> FindAllSort()    {
            return from d in Entities.Tbl09Divisions
                   orderby d.DivisionName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl09Division Get(int id)        {
            return Entities.Tbl09Divisions.FirstOrDefault(d => d.DivisionID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl09Division tbl09Division)    {
            Entities.Tbl09Divisions.AddObject(tbl09Division);           
        }

        public void Delete(Tbl09Division tbl09Division)    {
            Entities.Tbl09Divisions.DeleteObject(tbl09Division);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

