using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:  01.12.2020  12:32     -->  

namespace Atis.WpfUi.Interfaces     {         
    public interface ITbl12SubphylumsRepository    {
        IQueryable<Tbl12Subphylum> Tbl12Subphylums { get; }                   
        IQueryable<Tbl12Subphylum> FindAll();
        IQueryable<Tbl12Subphylum> FindAllSort();          
    
Tbl12Subphylum Get(int id);   
        

        void Add(Tbl12Subphylum subphylum);
        void Delete(Tbl12Subphylum subphylum);
        void Save( );               
     }
}   

