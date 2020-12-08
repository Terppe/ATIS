using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  22.12.2017  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl45FamiliesRepository    {
        IQueryable<Tbl45Family> Tbl45Families { get; }                   
        IQueryable<Tbl45Family> FindAll();
        IQueryable<Tbl45Family> FindAllSort();          
    
Tbl45Family Get(int id);   
        

        void Add(Tbl45Family tbl45Family);
        void Delete(Tbl45Family tbl45Family);
        void Save( );               
     }
}   

