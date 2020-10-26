using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl39InfraordosRepository : ITbl39InfraordosRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl39Infraordo> Tbl39Infraordos     {         
            get { return _entities.Tbl39Infraordos; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl39Infraordo> FindAll()    {         
            return _entities.Tbl39Infraordos; 
        }   

            public IQueryable<Tbl39Infraordo> FindAllSort()    {
            return from d in _entities.Tbl39Infraordos
                   orderby d.InfraordoName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl39Infraordo Get(int id)        {
            return _entities.Tbl39Infraordos.FirstOrDefault(d => d.InfraordoID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl39Infraordo tbl39Infraordo)    {
            _entities.Tbl39Infraordos.Add(tbl39Infraordo);           
        }

        public void Delete(Tbl39Infraordo tbl39Infraordo)    {
            _entities.Tbl39Infraordos.Remove(tbl39Infraordo);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

