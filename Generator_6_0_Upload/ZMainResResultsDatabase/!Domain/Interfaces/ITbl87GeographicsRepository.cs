using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  15.03.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl87GeographicsRepository    {
        IQueryable<Tbl87Geographic> Tbl87Geographics { get; }                   
        IQueryable<Tbl87Geographic> FindAll();
        IQueryable<Tbl87Geographic> FindAllSort();          
    
Tbl87Geographic Get(int id);   
        

        void Add(Tbl87Geographic tbl87Geographic);
        void Delete(Tbl87Geographic tbl87Geographic);
        void Save( );               
     }
}   

