using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl84SynonymsRepository : ITbl84SynonymsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl84Synonym> Tbl84Synonyms     {         
            get { return Entities.Tbl84Synonyms; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl84Synonym> FindAll()    {         
            return Entities.Tbl84Synonyms; 
        }   

            public IQueryable<Tbl84Synonym> FindAllSort()    {
            return from d in Entities.Tbl84Synonyms
                   orderby d.SynonymName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl84Synonym Get(int id)        {
            return Entities.Tbl84Synonyms.FirstOrDefault(d => d.SynonymID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl84Synonym tbl84Synonym)    {
            Entities.Tbl84Synonyms.AddObject(tbl84Synonym);           
        }

        public void Delete(Tbl84Synonym tbl84Synonym)    {
            Entities.Tbl84Synonyms.DeleteObject(tbl84Synonym);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

