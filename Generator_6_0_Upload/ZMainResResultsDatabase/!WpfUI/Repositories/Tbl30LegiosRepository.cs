using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  21.12.2017  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl30LegiosRepository : ITbl30LegiosRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl30Legio> Tbl30Legios     {         
            get { return _entities.Tbl30Legios; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl30Legio> FindAll()    {         
            return _entities.Tbl30Legios; 
        }   

            public IQueryable<Tbl30Legio> FindAllSort()    {
            return from d in _entities.Tbl30Legios
                   orderby d.LegioName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl30Legio Get(int id)        {
            return _entities.Tbl30Legios.FirstOrDefault(d => d.LegioID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl30Legio tbl30Legio)    {
            _entities.Tbl30Legios.Add(tbl30Legio);           
        }

        public void Delete(Tbl30Legio tbl30Legio)    {
            _entities.Tbl30Legios.Remove(tbl30Legio);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

