using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  15.12.2019  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl69FiSpeciessesRepository : ITbl69FiSpeciessesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl69FiSpecies> Tbl69FiSpeciesses     {         
            get { return _entities.Tbl69FiSpeciesses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl69FiSpecies> FindAll()    {         
            return _entities.Tbl69FiSpeciesses; 
        }   

         public IQueryable<Tbl69FiSpecies> FindAllSort()    {
            return from d in _entities.Tbl69FiSpeciesses
                   orderby d.FiSpeciesName, d.Subspecies, d.Divers
                   select d;
        }                                                                                                                                                                   
  
          public Tbl69FiSpecies Get(int id)        {
            return _entities.Tbl69FiSpeciesses.FirstOrDefault(d => d.FiSpeciesID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl69FiSpecies tbl69FiSpecies)    {
            _entities.Tbl69FiSpeciesses.Add(tbl69FiSpecies);           
        }

        public void Delete(Tbl69FiSpecies tbl69FiSpecies)    {
            _entities.Tbl69FiSpeciesses.Remove(tbl69FiSpecies);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

