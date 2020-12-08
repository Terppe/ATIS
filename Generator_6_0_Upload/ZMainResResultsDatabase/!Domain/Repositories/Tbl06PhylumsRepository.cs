using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  14.03.2014  12:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl06PhylumsRepository : ITbl06PhylumsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl06Phylum> Tbl06Phylums     {         
            get { return Entities.Tbl06Phylums; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl06Phylum> FindAll()    {         
            return Entities.Tbl06Phylums; 
        }   

            public IQueryable<Tbl06Phylum> FindAllSort()    {
            return from d in Entities.Tbl06Phylums
                   orderby d.PhylumName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl06Phylum Get(int id)        {
            return Entities.Tbl06Phylums.FirstOrDefault(d => d.PhylumID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl06Phylum tbl06Phylum)    {
            Entities.Tbl06Phylums.AddObject(tbl06Phylum);           
        }

        public void Delete(Tbl06Phylum tbl06Phylum)    {
            Entities.Tbl06Phylums.DeleteObject(tbl06Phylum);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

