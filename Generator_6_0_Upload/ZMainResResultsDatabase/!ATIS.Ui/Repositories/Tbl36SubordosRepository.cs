using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  15.12.2019  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl36SubordosRepository : ITbl36SubordosRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl36Subordo> Tbl36Subordos     {         
            get { return _entities.Tbl36Subordos; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl36Subordo> FindAll()    {         
            return _entities.Tbl36Subordos; 
        }   

            public IQueryable<Tbl36Subordo> FindAllSort()    {
            return from d in _entities.Tbl36Subordos
                   orderby d.SubordoName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl36Subordo Get(int id)        {
            return _entities.Tbl36Subordos.FirstOrDefault(d => d.SubordoID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl36Subordo tbl36Subordo)    {
            _entities.Tbl36Subordos.Add(tbl36Subordo);           
        }

        public void Delete(Tbl36Subordo tbl36Subordo)    {
            _entities.Tbl36Subordos.Remove(tbl36Subordo);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

