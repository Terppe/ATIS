using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  17.03.2014  12:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl15SubdivisionsRepository : ITbl15SubdivisionsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl15Subdivision> Tbl15Subdivisions     {         
            get { return Entities.Tbl15Subdivisions; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl15Subdivision> FindAll()    {         
            return Entities.Tbl15Subdivisions; 
        }   

            public IQueryable<Tbl15Subdivision> FindAllSort()    {
            return from d in Entities.Tbl15Subdivisions
                   orderby d.SubdivisionName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl15Subdivision Get(int id)        {
            return Entities.Tbl15Subdivisions.FirstOrDefault(d => d.SubdivisionID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl15Subdivision tbl15Subdivision)    {
            Entities.Tbl15Subdivisions.AddObject(tbl15Subdivision);           
        }

        public void Delete(Tbl15Subdivision tbl15Subdivision)    {
            Entities.Tbl15Subdivisions.DeleteObject(tbl15Subdivision);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

