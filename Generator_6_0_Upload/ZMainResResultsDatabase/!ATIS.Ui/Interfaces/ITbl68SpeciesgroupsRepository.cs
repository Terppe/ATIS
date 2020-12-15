using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  15.12.2020  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl68SpeciesgroupsRepository    {
        IQueryable<Tbl68Speciesgroup> Tbl68Speciesgroups { get; }                   
        IQueryable<Tbl68Speciesgroup> FindAll();
        IQueryable<Tbl68Speciesgroup> FindAllSort();          
    
Tbl68Speciesgroup Get(int id);   
        

        void Add(Tbl68Speciesgroup speciesgroup);
        void Delete(Tbl68Speciesgroup speciesgroup);
        void Save( );               
     }
}   

