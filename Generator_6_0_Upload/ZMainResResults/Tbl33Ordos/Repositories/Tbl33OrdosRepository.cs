using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  29.01.2019  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl33OrdosRepository : ITbl33OrdosRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl33Ordo> Tbl33Ordos     {         
            get { return _entities.Tbl33Ordos; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl33Ordo> FindAll()    {         
            return _entities.Tbl33Ordos; 
        }   

            public IQueryable<Tbl33Ordo> FindAllSort()    {
            return from d in _entities.Tbl33Ordos
                   orderby d.OrdoName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl33Ordo Get(int id)        {
            return _entities.Tbl33Ordos.FirstOrDefault(d => d.OrdoID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl33Ordo tbl33Ordo)    {
            _entities.Tbl33Ordos.Add(tbl33Ordo);           
        }

        public void Delete(Tbl33Ordo tbl33Ordo)    {
            _entities.Tbl33Ordos.Remove(tbl33Ordo);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

