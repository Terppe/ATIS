using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl72PlSpeciessesRepository : ITbl72PlSpeciessesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl72PlSpecies> Tbl72PlSpeciesses     {         
            get { return Entities.Tbl72PlSpeciesses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl72PlSpecies> FindAll()    {         
            return Entities.Tbl72PlSpeciesses; 
        }   

         public IQueryable<Tbl72PlSpecies> FindAllSort()    {
            return from d in Entities.Tbl72PlSpeciesses
                   orderby d.PlSpeciesName, d.Subspecies, d.Divers
                   select d;
        }                                                                                                                                                                   
  
          public Tbl72PlSpecies Get(int id)        {
            return Entities.Tbl72PlSpeciesses.FirstOrDefault(d => d.PlSpeciesID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl72PlSpecies tbl72PlSpecies)    {
            Entities.Tbl72PlSpeciesses.AddObject(tbl72PlSpecies);           
        }

        public void Delete(Tbl72PlSpecies tbl72PlSpecies)    {
            Entities.Tbl72PlSpeciesses.DeleteObject(tbl72PlSpecies);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

