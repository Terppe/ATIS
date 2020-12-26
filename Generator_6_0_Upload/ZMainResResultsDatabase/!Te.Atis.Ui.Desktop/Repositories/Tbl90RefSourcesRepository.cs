using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:   29.11.2018  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl90RefSourcesRepository : ITbl90RefSourcesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl90RefSource> Tbl90RefSources     {         
            get { return _entities.Tbl90RefSources; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl90RefSource> FindAll()    {         
            return _entities.Tbl90RefSources; 
        }   

            public IQueryable<Tbl90RefSource> FindAllSort()    {
            return from d in _entities.Tbl90RefSources
                   orderby d.RefSourceName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl90RefSource Get(int id)        {
            return _entities.Tbl90RefSources.FirstOrDefault(d => d.RefSourceID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl90RefSource tbl90RefSource)    {
            _entities.Tbl90RefSources.Add(tbl90RefSource);           
        }

        public void Delete(Tbl90RefSource tbl90RefSource)    {
            _entities.Tbl90RefSources.Remove(tbl90RefSource);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

