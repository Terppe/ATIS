using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  22.12.2017  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl36SubordosRepository    {
        IQueryable<Tbl36Subordo> Tbl36Subordos { get; }                   
        IQueryable<Tbl36Subordo> FindAll();
        IQueryable<Tbl36Subordo> FindAllSort();          
    
Tbl36Subordo Get(int id);   
        

        void Add(Tbl36Subordo tbl36Subordo);
        void Delete(Tbl36Subordo tbl36Subordo);
        void Save( );               
     }
}   

