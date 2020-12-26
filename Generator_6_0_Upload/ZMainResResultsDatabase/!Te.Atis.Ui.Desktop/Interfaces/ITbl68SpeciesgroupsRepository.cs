using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  09.11.2018  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl68SpeciesgroupsRepository    {
        IQueryable<Tbl68Speciesgroup> Tbl68Speciesgroups { get; }                   
        IQueryable<Tbl68Speciesgroup> FindAll();
        IQueryable<Tbl68Speciesgroup> FindAllSort();          
    
Tbl68Speciesgroup Get(int id);   
        

        void Add(Tbl68Speciesgroup tbl68Speciesgroup);
        void Delete(Tbl68Speciesgroup tbl68Speciesgroup);
        void Save( );               
     }
}   

