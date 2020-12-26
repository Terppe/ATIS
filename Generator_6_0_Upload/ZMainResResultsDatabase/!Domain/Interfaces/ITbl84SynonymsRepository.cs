using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  15.03.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl84SynonymsRepository    {
        IQueryable<Tbl84Synonym> Tbl84Synonyms { get; }                   
        IQueryable<Tbl84Synonym> FindAll();
        IQueryable<Tbl84Synonym> FindAllSort();          
    
Tbl84Synonym Get(int id);   
        

        void Add(Tbl84Synonym tbl84Synonym);
        void Delete(Tbl84Synonym tbl84Synonym);
        void Save( );               
     }
}   

