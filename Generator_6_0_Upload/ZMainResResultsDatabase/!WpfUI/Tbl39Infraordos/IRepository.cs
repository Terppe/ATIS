using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  7.1.2012  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl39InfraordosRepository    {
        IQueryable<Tbl39Infraordo> Tbl39Infraordos { get; }                   
        IQueryable<Tbl39Infraordo> FindAll();
        IQueryable<Tbl39Infraordo> FindAllSort();          
    
Tbl39Infraordo Get(int id);   
        

        void Add(Tbl39Infraordo tbl39Infraordo);
        void Delete(Tbl39Infraordo tbl39Infraordo);
        void Save( );               
     }
}   

