using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  14.03.2014  12:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl06PhylumsRepository    {
        IQueryable<Tbl06Phylum> Tbl06Phylums { get; }                   
        IQueryable<Tbl06Phylum> FindAll();
        IQueryable<Tbl06Phylum> FindAllSort();          
    
Tbl06Phylum Get(int id);   
        

        void Add(Tbl06Phylum tbl06Phylum);
        void Delete(Tbl06Phylum tbl06Phylum);
        void Save( );               
     }
}   

