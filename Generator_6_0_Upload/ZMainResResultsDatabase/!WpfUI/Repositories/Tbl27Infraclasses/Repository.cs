using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  30.12.2011  18:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl27InfraclassesRepository : ITbl27InfraclassesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl27Infraclass> Tbl27Infraclasses     {         
            get { return _entities.Tbl27Infraclasses; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl27Infraclass> FindAll()    {         
            return _entities.Tbl27Infraclasses; 
        }   

            public IQueryable<Tbl27Infraclass> FindAllSort()    {
            return from d in _entities.Tbl27Infraclasses
                   orderby d.InfraclassName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl27Infraclass Get(int id)        {
            return _entities.Tbl27Infraclasses.FirstOrDefault(d => d.InfraclassID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl27Infraclass tbl27Infraclass)    {
            _entities.Tbl27Infraclasses.Add(tbl27Infraclass);           
        }

        public void Delete(Tbl27Infraclass tbl27Infraclass)    {
            _entities.Tbl27Infraclasses.Remove(tbl27Infraclass);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

