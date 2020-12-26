using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:   29.11.2018 12:32       -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITblCountriesRepository    {
        IQueryable<TblCountry> TblCountries { get; }                   
        IQueryable<TblCountry> FindAll();
        IQueryable<TblCountry> FindAllSort();          
    
TblCountry Get(int id);   
        

        void Add(TblCountry tblCountry);
        void Delete(TblCountry tblCountry);
        void Save( );               
     }
}   

