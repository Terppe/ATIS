using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl84SynonymsRepository : ITbl84SynonymsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl84Synonym> Tbl84Synonyms     {         
            get { return _entities.Tbl84Synonyms; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl84Synonym> FindAll()    {         
            return _entities.Tbl84Synonyms; 
        }   

            public IQueryable<Tbl84Synonym> FindAllSort()    {
            return from d in _entities.Tbl84Synonyms
                   orderby d.SynonymName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl84Synonym Get(int id)        {
            return _entities.Tbl84Synonyms.FirstOrDefault(d => d.SynonymID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl84Synonym tbl84Synonym)    {
            _entities.Tbl84Synonyms.Add(tbl84Synonym);           
        }

        public void Delete(Tbl84Synonym tbl84Synonym)    {
            _entities.Tbl84Synonyms.Remove(tbl84Synonym);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

