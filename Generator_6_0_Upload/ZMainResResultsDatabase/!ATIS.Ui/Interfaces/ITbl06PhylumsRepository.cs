using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  28.11.2020  12:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl06PhylumsRepository    {
        IQueryable<Tbl06Phylum> Tbl06Phylums { get; }                   
        IQueryable<Tbl06Phylum> FindAll();
        IQueryable<Tbl06Phylum> FindAllSort();          
    
Tbl06Phylum Get(int id);   
        

        void Add(Tbl06Phylum phylum);
        void Delete(Tbl06Phylum phylum);
        void Save( );               
     }
}   

