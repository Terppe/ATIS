using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl36SubordosRepository : ITbl36SubordosRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl36Subordo> Tbl36Subordos     {         
            get { return Entities.Tbl36Subordos; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl36Subordo> FindAll()    {         
            return Entities.Tbl36Subordos; 
        }   

            public IQueryable<Tbl36Subordo> FindAllSort()    {
            return from d in Entities.Tbl36Subordos
                   orderby d.SubordoName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl36Subordo Get(int id)        {
            return Entities.Tbl36Subordos.FirstOrDefault(d => d.SubordoID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl36Subordo tbl36Subordo)    {
            Entities.Tbl36Subordos.AddObject(tbl36Subordo);           
        }

        public void Delete(Tbl36Subordo tbl36Subordo)    {
            Entities.Tbl36Subordos.DeleteObject(tbl36Subordo);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

