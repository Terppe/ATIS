using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  05.11.2020  18:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl21ClassesRepository    {
        IQueryable<Tbl21Class> Tbl21Classes { get; }                   
        IQueryable<Tbl21Class> FindAll();
        IQueryable<Tbl21Class> FindAllSort();          
    
Tbl21Class Get(int id);   
        

        void Add(Tbl21Class class);
        void Delete(Tbl21Class class);
        void Save( );               
     }
}   

