using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  23.12.2017  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl48SubfamiliesRepository    {
        IQueryable<Tbl48Subfamily> Tbl48Subfamilies { get; }                   
        IQueryable<Tbl48Subfamily> FindAll();
        IQueryable<Tbl48Subfamily> FindAllSort();          
    
Tbl48Subfamily Get(int id);   
        

        void Add(Tbl48Subfamily tbl48Subfamily);
        void Delete(Tbl48Subfamily tbl48Subfamily);
        void Save( );               
     }
}   

