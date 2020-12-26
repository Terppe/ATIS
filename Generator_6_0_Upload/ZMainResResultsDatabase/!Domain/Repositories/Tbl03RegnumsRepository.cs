using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  14.03.2014  12:32      -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl03RegnumsRepository : ITbl03RegnumsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl03Regnum> Tbl03Regnums     {         
            get { return Entities.Tbl03Regnums; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl03Regnum> FindAll()    {         
            return Entities.Tbl03Regnums; 
        }   

         public IQueryable<Tbl03Regnum> FindAllSort()    {
            return from d in Entities.Tbl03Regnums 
                   orderby d.RegnumName, d.Subregnum
                   select d;
        }                                                                                                                                                                   
  
          public Tbl03Regnum Get(int id)        {
            return Entities.Tbl03Regnums.FirstOrDefault(d => d.RegnumID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl03Regnum tbl03Regnum)    {
            Entities.Tbl03Regnums.AddObject(tbl03Regnum);           
        }

        public void Delete(Tbl03Regnum tbl03Regnum)    {
            Entities.Tbl03Regnums.DeleteObject(tbl03Regnum);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

