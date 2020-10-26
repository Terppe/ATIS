using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  12.12.2018  12:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl18SuperclassesRepository : ITbl18SuperclassesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl18Superclass> Tbl18Superclasses     {         
            get { return _entities.Tbl18Superclasses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl18Superclass> FindAll()    {         
            return _entities.Tbl18Superclasses; 
        }   

            public IQueryable<Tbl18Superclass> FindAllSort()    {
            return from d in _entities.Tbl18Superclasses
                   orderby d.SuperclassName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl18Superclass Get(int id)        {
            return _entities.Tbl18Superclasses.FirstOrDefault(d => d.SuperclassID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl18Superclasstbl18Superclass)    {
            _entities.Tbl18Superclasses.Add(tbl18Superclass);           
        }

        public void Delete(Tbl18Superclasstbl18Superclass)    {
            _entities.Tbl18Superclasses.Remove(tbl18Superclass);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

