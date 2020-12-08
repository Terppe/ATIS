using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  17.03.2014  12:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl12SubphylumsRepository : ITbl12SubphylumsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl12Subphylum> Tbl12Subphylums     {         
            get { return Entities.Tbl12Subphylums; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl12Subphylum> FindAll()    {         
            return Entities.Tbl12Subphylums; 
        }   

            public IQueryable<Tbl12Subphylum> FindAllSort()    {
            return from d in Entities.Tbl12Subphylums
                   orderby d.SubphylumName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl12Subphylum Get(int id)        {
            return Entities.Tbl12Subphylums.FirstOrDefault(d => d.SubphylumID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl12Subphylum tbl12Subphylum)    {
            Entities.Tbl12Subphylums.AddObject(tbl12Subphylum);           
        }

        public void Delete(Tbl12Subphylum tbl12Subphylum)    {
            Entities.Tbl12Subphylums.DeleteObject(tbl12Subphylum);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

