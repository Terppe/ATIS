using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  21.12.2017  18:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl21ClassesRepository : ITbl21ClassesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl21Class> Tbl21Classes     {         
            get { return _entities.Tbl21Classes; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl21Class> FindAll()    {         
            return _entities.Tbl21Classes; 
        }   

            public IQueryable<Tbl21Class> FindAllSort()    {
            return from d in _entities.Tbl21Classes
                   orderby d.ClassName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl21Class Get(int id)        {
            return _entities.Tbl21Classes.FirstOrDefault(d => d.ClassID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl21Class tbl21Class)    {
            _entities.Tbl21Classes.Add(tbl21Class);           
        }

        public void Delete(Tbl21Class tbl21Class)    {
            _entities.Tbl21Classes.Remove(tbl21Class);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

