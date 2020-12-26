using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl48SubfamiliesRepository : ITbl48SubfamiliesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl48Subfamily> Tbl48Subfamilies     {         
            get { return Entities.Tbl48Subfamilies; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl48Subfamily> FindAll()    {         
            return Entities.Tbl48Subfamilies; 
        }   

            public IQueryable<Tbl48Subfamily> FindAllSort()    {
            return from d in Entities.Tbl48Subfamilies
                   orderby d.SubfamilyName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl48Subfamily Get(int id)        {
            return Entities.Tbl48Subfamilies.FirstOrDefault(d => d.SubfamilyID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl48Subfamily tbl48Subfamily)    {
            Entities.Tbl48Subfamilies.AddObject(tbl48Subfamily);           
        }

        public void Delete(Tbl48Subfamily tbl48Subfamily)    {
            Entities.Tbl48Subfamilies.DeleteObject(tbl48Subfamily);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

