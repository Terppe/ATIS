using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  15.12.2020  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl68SpeciesgroupsRepository : ITbl68SpeciesgroupsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl68Speciesgroup> Tbl68Speciesgroups     {         
            get { return _entities.Tbl68Speciesgroups; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl68Speciesgroup> FindAll()    {         
            return _entities.Tbl68Speciesgroups; 
        }   

         public IQueryable<Tbl68Speciesgroup> FindAllSort()    {
            return from d in _entities.Tbl68Speciesgroups
                   orderby d.SpeciesgroupName, d.Subspeciesgroup
                   select d;
        }                                                                                                                                                                   
  
          public Tbl68Speciesgroup Get(int id)        {
            return _entities.Tbl68Speciesgroups.FirstOrDefault(d => d.SpeciesgroupID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl68Speciesgroup speciesgroup)    {
            _entities.Tbl68Speciesgroups.Add(tbl68Speciesgroup);           
        }

        public void Delete(Tbl68Speciesgroup speciesgroup)    {
            _entities.Tbl68Speciesgroups.Remove(tbl68Speciesgroup);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

