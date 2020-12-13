using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  13.12.2020  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl63InfratribussesRepository : ITbl63InfratribussesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl63Infratribus> Tbl63Infratribusses     {         
            get { return _entities.Tbl63Infratribusses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl63Infratribus> FindAll()    {         
            return _entities.Tbl63Infratribusses; 
        }   

            public IQueryable<Tbl63Infratribus> FindAllSort()    {
            return from d in _entities.Tbl63Infratribusses
                   orderby d.InfratribusName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl63Infratribus Get(int id)        {
            return _entities.Tbl63Infratribusses.FirstOrDefault(d => d.InfratribusID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl63Infratribus infratribus)    {
            _entities.Tbl63Infratribusses.Add(tbl63Infratribus);           
        }

        public void Delete(Tbl63Infratribus infratribus)    {
            _entities.Tbl63Infratribusses.Remove(tbl63Infratribus);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

