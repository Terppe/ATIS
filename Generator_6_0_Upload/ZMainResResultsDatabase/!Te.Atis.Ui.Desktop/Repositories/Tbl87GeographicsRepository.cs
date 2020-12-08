using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  22.01.2019  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl87GeographicsRepository : ITbl87GeographicsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl87Geographic> Tbl87Geographics     {         
            get { return _entities.Tbl87Geographics; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl87Geographic> FindAll()    {         
            return _entities.Tbl87Geographics; 
        }   

         public IQueryable<Tbl87Geographic> FindAllSort()    {
            return from d in _entities.Tbl87Geographics
                   orderby d.FiSpeciesID
                   select d;
        }                                                                                                                                                                   
  
          public Tbl87Geographic Get(int id)        {
            return _entities.Tbl87Geographics.FirstOrDefault(d => d.GeographicID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl87Geographic tbl87Geographic)    {
            _entities.Tbl87Geographics.Add(tbl87Geographic);           
        }

        public void Delete(Tbl87Geographic tbl87Geographic)    {
            _entities.Tbl87Geographics.Remove(tbl87Geographic);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

