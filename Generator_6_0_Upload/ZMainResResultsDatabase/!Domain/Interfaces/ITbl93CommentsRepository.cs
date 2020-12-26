using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  15.03.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl93CommentsRepository    {
        IQueryable<Tbl93Comment> Tbl93Comments { get; }                   
        IQueryable<Tbl93Comment> FindAll();
        IQueryable<Tbl93Comment> FindAllSort();          
    
Tbl93Comment Get(int id);   
        

        void Add(Tbl93Comment tbl93Comment);
        void Delete(Tbl93Comment tbl93Comment);
        void Save( );               
     }
}   

