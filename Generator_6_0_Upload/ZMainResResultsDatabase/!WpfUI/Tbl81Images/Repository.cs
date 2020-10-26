using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  28.12.2011  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl81ImagesRepository : ITbl81ImagesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl81Image> Tbl81Images     {         
            get { return _entities.Tbl81Images; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl81Image> FindAll()    {         
            return _entities.Tbl81Images; 
        }   

         public IQueryable<Tbl81Image> FindAllSort()    {
            return from d in _entities.Tbl81Images
                   orderby d.FiSpeciesID
                   select d;
        }                                                                                                                                                                   
  
          public Tbl81Image Get(int id)        {
            return _entities.Tbl81Images.FirstOrDefault(d => d.ImageID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl81Image tbl81Image)    {
            _entities.Tbl81Images.Add(tbl81Image);           
        }

        public void Delete(Tbl81Image tbl81Image)    {
            _entities.Tbl81Images.Remove(tbl81Image);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

