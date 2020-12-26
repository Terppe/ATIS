using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl57TribussesRepository : ITbl57TribussesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl57Tribus> Tbl57Tribusses     {         
            get { return _entities.Tbl57Tribusses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl57Tribus> FindAll()    {         
            return _entities.Tbl57Tribusses; 
        }   

            public IQueryable<Tbl57Tribus> FindAllSort()    {
            return from d in _entities.Tbl57Tribusses
                   orderby d.TribusName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl57Tribus Get(int id)        {
            return _entities.Tbl57Tribusses.FirstOrDefault(d => d.TribusID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl57Tribus tbl57Tribus)    {
            _entities.Tbl57Tribusses.Add(tbl57Tribus);           
        }

        public void Delete(Tbl57Tribus tbl57Tribus)    {
            _entities.Tbl57Tribusses.Remove(tbl57Tribus);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

