using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  22.01.2019  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl81ImagesRepository    {
        IQueryable<Tbl81Image> Tbl81Images { get; }                   
        IQueryable<Tbl81Image> FindAll();
        IQueryable<Tbl81Image> FindAllSort();          
    
Tbl81Image Get(int id);   
        

        void Add(Tbl81Image tbl81Image);
        void Delete(Tbl81Image tbl81Image);
        void Save( );               
     }
}   

