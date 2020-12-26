using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl39InfraordosRepository : ITbl39InfraordosRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl39Infraordo> Tbl39Infraordos     {         
            get { return Entities.Tbl39Infraordos; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl39Infraordo> FindAll()    {         
            return Entities.Tbl39Infraordos; 
        }   

            public IQueryable<Tbl39Infraordo> FindAllSort()    {
            return from d in Entities.Tbl39Infraordos
                   orderby d.InfraordoName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl39Infraordo Get(int id)        {
            return Entities.Tbl39Infraordos.FirstOrDefault(d => d.InfraordoID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl39Infraordo tbl39Infraordo)    {
            Entities.Tbl39Infraordos.AddObject(tbl39Infraordo);           
        }

        public void Delete(Tbl39Infraordo tbl39Infraordo)    {
            Entities.Tbl39Infraordos.DeleteObject(tbl39Infraordo);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

