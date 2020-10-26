using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl33OrdosRepository : ITbl33OrdosRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl33Ordo> Tbl33Ordos     {         
            get { return Entities.Tbl33Ordos; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl33Ordo> FindAll()    {         
            return Entities.Tbl33Ordos; 
        }   

            public IQueryable<Tbl33Ordo> FindAllSort()    {
            return from d in Entities.Tbl33Ordos
                   orderby d.OrdoName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl33Ordo Get(int id)        {
            return Entities.Tbl33Ordos.FirstOrDefault(d => d.OrdoID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl33Ordo tbl33Ordo)    {
            Entities.Tbl33Ordos.AddObject(tbl33Ordo);           
        }

        public void Delete(Tbl33Ordo tbl33Ordo)    {
            Entities.Tbl33Ordos.DeleteObject(tbl33Ordo);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

