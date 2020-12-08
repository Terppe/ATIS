using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  28.12.2011  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl81ImagesRepository : ITbl81ImagesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl81Image> Tbl81Images     {         
            get { return Entities.Tbl81Images; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl81Image> FindAll()    {         
            return Entities.Tbl81Images; 
        }   

         public IQueryable<Tbl81Image> FindAllSort()    {
            return from d in Entities.Tbl81Images
                   orderby d.FiSpeciesID
                   select d;
        }                                                                                                                                                                   
  
          public Tbl81Image Get(int id)        {
            return Entities.Tbl81Images.FirstOrDefault(d => d.ImageID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl81Image tbl81Image)    {
            Entities.Tbl81Images.AddObject(tbl81Image);           
        }

        public void Delete(Tbl81Image tbl81Image)    {
            Entities.Tbl81Images.DeleteObject(tbl81Image);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

