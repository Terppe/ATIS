using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  12.12.2019  12:32      -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl03RegnumsRepository : ITbl03RegnumsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl03Regnum> Tbl03Regnums     {         
            get { return _entities.Tbl03Regnums; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl03Regnum> FindAll()    {         
            return _entities.Tbl03Regnums; 
        }   

         public IQueryable<Tbl03Regnum> FindAllSort()    {
            return from d in _entities.Tbl03Regnums 
                   orderby d.RegnumName, d.Subregnum
                   select d;
        }                                                                                                                                                                   
  
          public Tbl03Regnum Get(int id)        {
            return _entities.Tbl03Regnums.FirstOrDefault(d => d.RegnumID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl03Regnum tbl03Regnum)    {
            _entities.Tbl03Regnums.Add(tbl03Regnum);           
        }

        public void Delete(Tbl03Regnum tbl03Regnum)    {
            _entities.Tbl03Regnums.Remove(tbl03Regnum);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

