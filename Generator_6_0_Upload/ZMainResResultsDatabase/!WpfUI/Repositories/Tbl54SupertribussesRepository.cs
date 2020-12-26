using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  23.12.2017  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl54SupertribussesRepository : ITbl54SupertribussesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl54Supertribus> Tbl54Supertribusses     {         
            get { return _entities.Tbl54Supertribusses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl54Supertribus> FindAll()    {         
            return _entities.Tbl54Supertribusses; 
        }   

            public IQueryable<Tbl54Supertribus> FindAllSort()    {
            return from d in _entities.Tbl54Supertribusses
                   orderby d.SupertribusName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl54Supertribus Get(int id)        {
            return _entities.Tbl54Supertribusses.FirstOrDefault(d => d.SupertribusID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl54Supertribus tbl54Supertribus)    {
            _entities.Tbl54Supertribusses.Add(tbl54Supertribus);           
        }

        public void Delete(Tbl54Supertribus tbl54Supertribus)    {
            _entities.Tbl54Supertribusses.Remove(tbl54Supertribus);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

