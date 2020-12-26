using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  15.03.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
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

