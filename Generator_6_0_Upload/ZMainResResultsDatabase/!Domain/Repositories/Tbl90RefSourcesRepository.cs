using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  29.12.2011  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl90RefSourcesRepository : ITbl90RefSourcesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl90RefSource> Tbl90RefSources     {         
            get { return Entities.Tbl90RefSources; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl90RefSource> FindAll()    {         
            return Entities.Tbl90RefSources; 
        }   

            public IQueryable<Tbl90RefSource> FindAllSort()    {
            return from d in Entities.Tbl90RefSources
                   orderby d.RefSourceName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl90RefSource Get(int id)        {
            return Entities.Tbl90RefSources.FirstOrDefault(d => d.RefSourceID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl90RefSource tbl90RefSource)    {
            Entities.Tbl90RefSources.AddObject(tbl90RefSource);           
        }

        public void Delete(Tbl90RefSource tbl90RefSource)    {
            Entities.Tbl90RefSources.DeleteObject(tbl90RefSource);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

