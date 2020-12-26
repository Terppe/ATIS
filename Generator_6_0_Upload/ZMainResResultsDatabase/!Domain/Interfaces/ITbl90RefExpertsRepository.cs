using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  29.12.2011  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl90RefExpertsRepository    {
        IQueryable<Tbl90RefExpert> Tbl90RefExperts { get; }                   
        IQueryable<Tbl90RefExpert> FindAll();
        IQueryable<Tbl90RefExpert> FindAllSort();          
    
Tbl90RefExpert Get(int id);   
        

        void Add(Tbl90RefExpert tbl90RefExpert);
        void Delete(Tbl90RefExpert tbl90RefExpert);
        void Save( );               
     }
}   

