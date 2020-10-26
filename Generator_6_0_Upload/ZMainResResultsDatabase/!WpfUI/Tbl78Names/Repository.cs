using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  28.12.2011  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl78NamesRepository : ITbl78NamesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl78Name> Tbl78Names     {         
            get { return _entities.Tbl78Names; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl78Name> FindAll()    {         
            return _entities.Tbl78Names; 
        }   

            public IQueryable<Tbl78Name> FindAllSort()    {
            return from d in _entities.Tbl78Names
                   orderby d.NameName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl78Name Get(int id)        {
            return _entities.Tbl78Names.FirstOrDefault(d => d.NameID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl78Name tbl78Name)    {
            _entities.Tbl78Names.Add(tbl78Name);           
        }

        public void Delete(Tbl78Name tbl78Name)    {
            _entities.Tbl78Names.Remove(tbl78Name);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

