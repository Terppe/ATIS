using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl69FiSpeciessesRepository : ITbl69FiSpeciessesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl69FiSpecies> Tbl69FiSpeciesses     {         
            get { return Entities.Tbl69FiSpeciesses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl69FiSpecies> FindAll()    {         
            return Entities.Tbl69FiSpeciesses; 
        }   

         public IQueryable<Tbl69FiSpecies> FindAllSort()    {
            return from d in Entities.Tbl69FiSpeciesses
                   orderby d.FiSpeciesName, d.Subspecies, d.Divers
                   select d;
        }                                                                                                                                                                   
  
          public Tbl69FiSpecies Get(int id)        {
            return Entities.Tbl69FiSpeciesses.FirstOrDefault(d => d.FiSpeciesID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl69FiSpecies tbl69FiSpecies)    {
            Entities.Tbl69FiSpeciesses.AddObject(tbl69FiSpecies);           
        }

        public void Delete(Tbl69FiSpecies tbl69FiSpecies)    {
            Entities.Tbl69FiSpeciesses.DeleteObject(tbl69FiSpecies);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

