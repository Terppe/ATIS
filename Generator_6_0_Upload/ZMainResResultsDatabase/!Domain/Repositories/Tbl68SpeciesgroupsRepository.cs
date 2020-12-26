using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl68SpeciesgroupsRepository : ITbl68SpeciesgroupsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl68Speciesgroup> Tbl68Speciesgroups     {         
            get { return Entities.Tbl68Speciesgroups; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl68Speciesgroup> FindAll()    {         
            return Entities.Tbl68Speciesgroups; 
        }   

         public IQueryable<Tbl68Speciesgroup> FindAllSort()    {
            return from d in Entities.Tbl68Speciesgroups
                   orderby d.SpeciesgroupName, d.Subspeciesgroup
                   select d;
        }                                                                                                                                                                   
  
          public Tbl68Speciesgroup Get(int id)        {
            return Entities.Tbl68Speciesgroups.FirstOrDefault(d => d.SpeciesgroupID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl68Speciesgroup tbl68Speciesgroup)    {
            Entities.Tbl68Speciesgroups.AddObject(tbl68Speciesgroup);           
        }

        public void Delete(Tbl68Speciesgroup tbl68Speciesgroup)    {
            Entities.Tbl68Speciesgroups.DeleteObject(tbl68Speciesgroup);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

