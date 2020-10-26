using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:   29.11.2018  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl90RefSourcesRepository    {
        IQueryable<Tbl90RefSource> Tbl90RefSources { get; }                   
        IQueryable<Tbl90RefSource> FindAll();
        IQueryable<Tbl90RefSource> FindAllSort();          
    
Tbl90RefSource Get(int id);   
        

        void Add(Tbl90RefSource tbl90RefSource);
        void Delete(Tbl90RefSource tbl90RefSource);
        void Save( );               
     }
}   

