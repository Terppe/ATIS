using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl93CommentsRepository : ITbl93CommentsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl93Comment> Tbl93Comments     {         
            get { return Entities.Tbl93Comments; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl93Comment> FindAll()    {         
            return Entities.Tbl93Comments; 
        }   

         public IQueryable<Tbl93Comment> FindAllSort()    {
            return from d in Entities.Tbl93Comments
                   orderby d.FiSpeciesID
                   select d;
        }                                                                                                                                                                   
  
          public Tbl93Comment Get(int id)        {
            return Entities.Tbl93Comments.FirstOrDefault(d => d.CommentID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl93Comment tbl93Comment)    {
            Entities.Tbl93Comments.AddObject(tbl93Comment);           
        }

        public void Delete(Tbl93Comment tbl93Comment)    {
            Entities.Tbl93Comments.DeleteObject(tbl93Comment);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

