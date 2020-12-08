using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  15.12.2019  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl69FiSpeciessesRepository    {
        IQueryable<Tbl69FiSpecies> Tbl69FiSpeciesses { get; }                   
        IQueryable<Tbl69FiSpecies> FindAll();
        IQueryable<Tbl69FiSpecies> FindAllSort();          
    
Tbl69FiSpecies Get(int id);   
        

        void Add(Tbl69FiSpecies tbl69FiSpecies);
        void Delete(Tbl69FiSpecies tbl69FiSpecies);
        void Save( );               
     }
}   

