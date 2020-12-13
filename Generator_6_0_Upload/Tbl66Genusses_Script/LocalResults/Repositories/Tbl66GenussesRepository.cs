using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  13.12.2020  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl66GenussesRepository : ITbl66GenussesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl66Genus> Tbl66Genusses     {         
            get { return _entities.Tbl66Genusses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl66Genus> FindAll()    {         
            return _entities.Tbl66Genusses; 
        }   

            public IQueryable<Tbl66Genus> FindAllSort()    {
            return from d in _entities.Tbl66Genusses
                   orderby d.GenusName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl66Genus Get(int id)        {
            return _entities.Tbl66Genusses.FirstOrDefault(d => d.GenusID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl66Genus genus)    {
            _entities.Tbl66Genusses.Add(tbl66Genus);           
        }

        public void Delete(Tbl66Genus genus)    {
            _entities.Tbl66Genusses.Remove(tbl66Genus);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

