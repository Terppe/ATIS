using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  04.11.2020  12:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl15SubdivisionsRepository : ITbl15SubdivisionsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl15Subdivision> Tbl15Subdivisions     {         
            get { return _entities.Tbl15Subdivisions; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl15Subdivision> FindAll()    {         
            return _entities.Tbl15Subdivisions; 
        }   

            public IQueryable<Tbl15Subdivision> FindAllSort()    {
            return from d in _entities.Tbl15Subdivisions
                   orderby d.SubdivisionName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl15Subdivision Get(int id)        {
            return _entities.Tbl15Subdivisions.FirstOrDefault(d => d.SubdivisionID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl15Subdivision tbl15Subdivision)    {
            _entities.Tbl15Subdivisions.Add(tbl15Subdivision);           
        }

        public void Delete(Tbl15Subdivision tbl15Subdivision)    {
            _entities.Tbl15Subdivisions.Remove(tbl15Subdivision);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

