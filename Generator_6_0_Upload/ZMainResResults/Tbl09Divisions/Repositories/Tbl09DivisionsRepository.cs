using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  03.11.2020  12:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl09DivisionsRepository : ITbl09DivisionsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl09Division> Tbl09Divisions     {         
            get { return _entities.Tbl09Divisions; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl09Division> FindAll()    {         
            return _entities.Tbl09Divisions; 
        }   

            public IQueryable<Tbl09Division> FindAllSort()    {
            return from d in _entities.Tbl09Divisions
                   orderby d.DivisionName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl09Division Get(int id)        {
            return _entities.Tbl09Divisions.FirstOrDefault(d => d.DivisionID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl09Division tbl09Division)    {
            _entities.Tbl09Divisions.Add(tbl09Division);           
        }

        public void Delete(Tbl09Division tbl09Division)    {
            _entities.Tbl09Divisions.Remove(tbl09Division);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

