using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  15.03.2012  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
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

