using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  17.03.2014  12:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITbl12SubphylumsRepository    {
        IQueryable<Tbl12Subphylum> Tbl12Subphylums { get; }                   
        IQueryable<Tbl12Subphylum> FindAll();
        IQueryable<Tbl12Subphylum> FindAllSort();          
    
Tbl12Subphylum Get(int id);   
        

        void Add(Tbl12Subphylum tbl12Subphylum);
        void Delete(Tbl12Subphylum tbl12Subphylum);
        void Save( );               
     }
}   

