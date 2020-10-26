using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  29.12.2011  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl90RefAuthorsRepository    {
        IQueryable<Tbl90RefAuthor> Tbl90RefAuthors { get; }                   
        IQueryable<Tbl90RefAuthor> FindAll();
        IQueryable<Tbl90RefAuthor> FindAllSort();          
    
Tbl90RefAuthor Get(int id);   
        

        void Add(Tbl90RefAuthor tbl90RefAuthor);
        void Delete(Tbl90RefAuthor tbl90RefAuthor);
        void Save( );               
     }
}   

