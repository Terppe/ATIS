using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  30.03.2019  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl90RefAuthorsRepository : ITbl90RefAuthorsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl90RefAuthor> Tbl90RefAuthors     {         
            get { return _entities.Tbl90RefAuthors; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl90RefAuthor> FindAll()    {         
            return _entities.Tbl90RefAuthors; 
        }   

            public IQueryable<Tbl90RefAuthor> FindAllSort()    {
            return from d in _entities.Tbl90RefAuthors
                   orderby d.RefAuthorName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl90RefAuthor Get(int id)        {
            return _entities.Tbl90RefAuthors.FirstOrDefault(d => d.RefAuthorID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl90RefAuthor tbl90RefAuthor)    {
            _entities.Tbl90RefAuthors.Add(tbl90RefAuthor);           
        }

        public void Delete(Tbl90RefAuthor tbl90RefAuthor)    {
            _entities.Tbl90RefAuthors.Remove(tbl90RefAuthor);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

