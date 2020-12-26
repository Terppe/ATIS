using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  15.03.2012  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
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

