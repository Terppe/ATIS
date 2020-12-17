using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  17.12.2020  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl69FiSpeciessesRepository    {
        IQueryable<Tbl69FiSpecies> Tbl69FiSpeciesses { get; }                   
        IQueryable<Tbl69FiSpecies> FindAll();
        IQueryable<Tbl69FiSpecies> FindAllSort();          
    
Tbl69FiSpecies Get(int id);   
        

        void Add(Tbl69FiSpecies fiSpecies);
        void Delete(Tbl69FiSpecies fiSpecies);
        void Save( );               
     }
}   

