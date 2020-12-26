using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:   15.11.2017 12:32      -->  

namespace Atis.WpfUi.Repositories      {  
    public class TblCountriesRepository : ITblCountriesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<TblCountry> TblCountries     {         
            get { return _entities.TblCountries; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<TblCountry> FindAll()    {         
            return _entities.TblCountries; 
        }   

            public IQueryable<TblCountry> FindAllSort()    {
            return from d in _entities.TblCountries
                   orderby d.CountryName
                   select d;
        }                                                                                                                                                                   
  
          public TblCountry Get(int id)        {
            return _entities.TblCountries.FirstOrDefault(d => d.CountryID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(TblCountry tblCountry)    {
            _entities.TblCountries.Add(tblCountry);           
        }

        public void Delete(TblCountry tblCountry)    {
            _entities.TblCountries.Remove(tblCountry);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

