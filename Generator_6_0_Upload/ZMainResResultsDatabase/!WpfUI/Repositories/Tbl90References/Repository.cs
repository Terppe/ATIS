using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl90ReferencesRepository : ITbl90ReferencesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl90Reference> Tbl90References     {         
            get { return _entities.Tbl90References; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl90Reference> FindAll()    {         
            return _entities.Tbl90References; 
        }   

         public IQueryable<Tbl90Reference> FindAllSort()    {
            return from d in _entities.Tbl90References
                   orderby d.FiSpeciesID
                   select d;
        }                                                                                                                                                                   
  
          public Tbl90Reference Get(int id)        {
            return _entities.Tbl90References.FirstOrDefault(d => d.ReferenceID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl90Reference tbl90Reference)    {
            _entities.Tbl90References.Add(tbl90Reference);           
        }

        public void Delete(Tbl90Reference tbl90Reference)    {
            _entities.Tbl90References.Remove(tbl90Reference);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

