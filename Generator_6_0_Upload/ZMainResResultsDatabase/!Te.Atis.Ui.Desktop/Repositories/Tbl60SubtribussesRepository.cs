using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  08.11.2018  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl60SubtribussesRepository : ITbl60SubtribussesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl60Subtribus> Tbl60Subtribusses     {         
            get { return _entities.Tbl60Subtribusses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl60Subtribus> FindAll()    {         
            return _entities.Tbl60Subtribusses; 
        }   

            public IQueryable<Tbl60Subtribus> FindAllSort()    {
            return from d in _entities.Tbl60Subtribusses
                   orderby d.SubtribusName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl60Subtribus Get(int id)        {
            return _entities.Tbl60Subtribusses.FirstOrDefault(d => d.SubtribusID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl60Subtribus tbl60Subtribus)    {
            _entities.Tbl60Subtribusses.Add(tbl60Subtribus);           
        }

        public void Delete(Tbl60Subtribus tbl60Subtribus)    {
            _entities.Tbl60Subtribusses.Remove(tbl60Subtribus);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

