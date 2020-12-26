using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  10.12.2020  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl45FamiliesRepository    {
        IQueryable<Tbl45Family> Tbl45Families { get; }                   
        IQueryable<Tbl45Family> FindAll();
        IQueryable<Tbl45Family> FindAllSort();          
    
Tbl45Family Get(int id);   
        

        void Add(Tbl45Family family);
        void Delete(Tbl45Family family);
        void Save( );               
     }
}   

