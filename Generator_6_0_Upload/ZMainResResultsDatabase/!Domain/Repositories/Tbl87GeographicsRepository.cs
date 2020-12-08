using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl87GeographicsRepository : ITbl87GeographicsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl87Geographic> Tbl87Geographics     {         
            get { return Entities.Tbl87Geographics; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl87Geographic> FindAll()    {         
            return Entities.Tbl87Geographics; 
        }   

         public IQueryable<Tbl87Geographic> FindAllSort()    {
            return from d in Entities.Tbl87Geographics
                   orderby d.FiSpeciesID
                   select d;
        }                                                                                                                                                                   
  
          public Tbl87Geographic Get(int id)        {
            return Entities.Tbl87Geographics.FirstOrDefault(d => d.GeographicID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl87Geographic tbl87Geographic)    {
            Entities.Tbl87Geographics.AddObject(tbl87Geographic);           
        }

        public void Delete(Tbl87Geographic tbl87Geographic)    {
            Entities.Tbl87Geographics.DeleteObject(tbl87Geographic);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

