using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  13.12.2019  12:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl72PlSpeciessesRepository : ITbl72PlSpeciessesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl72PlSpecies> Tbl72PlSpeciesses     {         
            get { return _entities.Tbl72PlSpeciesses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl72PlSpecies> FindAll()    {         
            return _entities.Tbl72PlSpeciesses; 
        }   

         public IQueryable<Tbl72PlSpecies> FindAllSort()    {
            return from d in _entities.Tbl72PlSpeciesses
                   orderby d.PlSpeciesName, d.Subspecies, d.Divers
                   select d;
        }                                                                                                                                                                   
  
          public Tbl72PlSpecies Get(int id)        {
            return _entities.Tbl72PlSpeciesses.FirstOrDefault(d => d.PlSpeciesID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl72PlSpecies plSpecies)    {
            _entities.Tbl72PlSpeciesses.Add(tbl72PlSpecies);           
        }

        public void Delete(Tbl72PlSpecies plSpecies)    {
            _entities.Tbl72PlSpeciesses.Remove(tbl72PlSpecies);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

