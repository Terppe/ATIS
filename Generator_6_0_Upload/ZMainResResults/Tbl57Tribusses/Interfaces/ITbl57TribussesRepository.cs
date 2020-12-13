using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  13.12.2019  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl57TribussesRepository    {
        IQueryable<Tbl57Tribus> Tbl57Tribusses { get; }                   
        IQueryable<Tbl57Tribus> FindAll();
        IQueryable<Tbl57Tribus> FindAllSort();          
    
Tbl57Tribus Get(int id);   
        

        void Add(Tbl57Tribus tribus);
        void Delete(Tbl57Tribus tribus);
        void Save( );               
     }
}   

