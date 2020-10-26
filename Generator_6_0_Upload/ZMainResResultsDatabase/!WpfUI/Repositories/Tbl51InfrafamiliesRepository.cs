using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  23.12.2017  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl51InfrafamiliesRepository : ITbl51InfrafamiliesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl51Infrafamily> Tbl51Infrafamilies     {         
            get { return _entities.Tbl51Infrafamilies; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl51Infrafamily> FindAll()    {         
            return _entities.Tbl51Infrafamilies; 
        }   

            public IQueryable<Tbl51Infrafamily> FindAllSort()    {
            return from d in _entities.Tbl51Infrafamilies
                   orderby d.InfrafamilyName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl51Infrafamily Get(int id)        {
            return _entities.Tbl51Infrafamilies.FirstOrDefault(d => d.InfrafamilyID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl51Infrafamily tbl51Infrafamily)    {
            _entities.Tbl51Infrafamilies.Add(tbl51Infrafamily);           
        }

        public void Delete(Tbl51Infrafamily tbl51Infrafamily)    {
            _entities.Tbl51Infrafamilies.Remove(tbl51Infrafamily);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

