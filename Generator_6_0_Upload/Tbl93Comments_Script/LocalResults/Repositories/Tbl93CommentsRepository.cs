using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  29.11.2018  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl93CommentsRepository : ITbl93CommentsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl93Comment> Tbl93Comments     {         
            get { return _entities.Tbl93Comments; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl93Comment> FindAll()    {         
            return _entities.Tbl93Comments; 
        }   

         public IQueryable<Tbl93Comment> FindAllSort()    {
            return from d in _entities.Tbl93Comments
                   orderby d.FiSpeciesID
                   select d;
        }                                                                                                                                                                   
  
          public Tbl93Comment Get(int id)        {
            return _entities.Tbl93Comments.FirstOrDefault(d => d.CommentID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl93Comment tbl93Comment)    {
            _entities.Tbl93Comments.Add(tbl93Comment);           
        }

        public void Delete(Tbl93Comment tbl93Comment)    {
            _entities.Tbl93Comments.Remove(tbl93Comment);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

