using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  13.12.2019  18:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl24SubclassesRepository : ITbl24SubclassesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl24Subclass> Tbl24Subclasses     {         
            get { return _entities.Tbl24Subclasses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl24Subclass> FindAll()    {         
            return _entities.Tbl24Subclasses; 
        }   

            public IQueryable<Tbl24Subclass> FindAllSort()    {
            return from d in _entities.Tbl24Subclasses
                   orderby d.SubclassName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl24Subclass Get(int id)        {
            return _entities.Tbl24Subclasses.FirstOrDefault(d => d.SubclassID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl24Subclass tbl24Subclass)    {
            _entities.Tbl24Subclasses.Add(tbl24Subclass);           
        }

        public void Delete(Tbl24Subclass tbl24Subclass)    {
            _entities.Tbl24Subclasses.Remove(tbl24Subclass);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

