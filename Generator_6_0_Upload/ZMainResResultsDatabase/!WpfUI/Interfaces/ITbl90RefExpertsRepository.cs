using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  14.11.2017  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
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

