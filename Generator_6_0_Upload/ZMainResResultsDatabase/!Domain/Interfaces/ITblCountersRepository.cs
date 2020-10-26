using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  3.1.2012  12:32       -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITblCountersRepository    {
        IQueryable<TblCounter> TblCounters { get; }                   
        IQueryable<TblCounter> FindAll();
        IQueryable<TblCounter> FindAllSort();          
    
TblCounter Get(int id);   
        

        void Add(TblCounter tblCounter);
        void Delete(TblCounter tblCounter);
        void Save( );               
 
        int Counter();       
     }
}   

