using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  08.11.2018  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl48SubfamiliesRepository : ITbl48SubfamiliesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl48Subfamily> Tbl48Subfamilies     {         
            get { return _entities.Tbl48Subfamilies; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl48Subfamily> FindAll()    {         
            return _entities.Tbl48Subfamilies; 
        }   

            public IQueryable<Tbl48Subfamily> FindAllSort()    {
            return from d in _entities.Tbl48Subfamilies
                   orderby d.SubfamilyName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl48Subfamily Get(int id)        {
            return _entities.Tbl48Subfamilies.FirstOrDefault(d => d.SubfamilyID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl48Subfamily tbl48Subfamily)    {
            _entities.Tbl48Subfamilies.Add(tbl48Subfamily);           
        }

        public void Delete(Tbl48Subfamily tbl48Subfamily)    {
            _entities.Tbl48Subfamilies.Remove(tbl48Subfamily);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

