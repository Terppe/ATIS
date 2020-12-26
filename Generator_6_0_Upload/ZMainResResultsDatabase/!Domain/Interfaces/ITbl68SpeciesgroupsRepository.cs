using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  15.03.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
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

