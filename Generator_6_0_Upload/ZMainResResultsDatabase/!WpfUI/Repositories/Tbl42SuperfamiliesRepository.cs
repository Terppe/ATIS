using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  22.12.2017  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl42SuperfamiliesRepository : ITbl42SuperfamiliesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl42Superfamily> Tbl42Superfamilies     {         
            get { return _entities.Tbl42Superfamilies; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl42Superfamily> FindAll()    {         
            return _entities.Tbl42Superfamilies; 
        }   

            public IQueryable<Tbl42Superfamily> FindAllSort()    {
            return from d in _entities.Tbl42Superfamilies
                   orderby d.SuperfamilyName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl42Superfamily Get(int id)        {
            return _entities.Tbl42Superfamilies.FirstOrDefault(d => d.SuperfamilyID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl42Superfamily tbl42Superfamily)    {
            _entities.Tbl42Superfamilies.Add(tbl42Superfamily);           
        }

        public void Delete(Tbl42Superfamily tbl42Superfamily)    {
            _entities.Tbl42Superfamilies.Remove(tbl42Superfamily);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

