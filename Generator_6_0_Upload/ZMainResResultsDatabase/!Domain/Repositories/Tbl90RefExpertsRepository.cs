using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  29.12.2011  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl90RefExpertsRepository : ITbl90RefExpertsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl90RefExpert> Tbl90RefExperts     {         
            get { return Entities.Tbl90RefExperts; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl90RefExpert> FindAll()    {         
            return Entities.Tbl90RefExperts; 
        }   

            public IQueryable<Tbl90RefExpert> FindAllSort()    {
            return from d in Entities.Tbl90RefExperts
                   orderby d.RefExpertName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl90RefExpert Get(int id)        {
            return Entities.Tbl90RefExperts.FirstOrDefault(d => d.RefExpertID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl90RefExpert tbl90RefExpert)    {
            Entities.Tbl90RefExperts.AddObject(tbl90RefExpert);           
        }

        public void Delete(Tbl90RefExpert tbl90RefExpert)    {
            Entities.Tbl90RefExperts.DeleteObject(tbl90RefExpert);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

