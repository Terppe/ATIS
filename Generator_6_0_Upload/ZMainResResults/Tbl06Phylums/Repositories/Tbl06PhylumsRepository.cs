using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  24.10.2020  12:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl06PhylumsRepository : ITbl06PhylumsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl06Phylum> Tbl06Phylums     {         
            get { return _entities.Tbl06Phylums; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl06Phylum> FindAll()    {         
            return _entities.Tbl06Phylums; 
        }   

            public IQueryable<Tbl06Phylum> FindAllSort()    {
            return from d in _entities.Tbl06Phylums
                   orderby d.PhylumName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl06Phylum Get(int id)        {
            return _entities.Tbl06Phylums.FirstOrDefault(d => d.PhylumId == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl06Phylum tbl06Phylum)    {
            _entities.Tbl06Phylums.Add(tbl06Phylum);           
        }

        public void Delete(Tbl06Phylum tbl06Phylum)    {
            _entities.Tbl06Phylums.Remove(tbl06Phylum);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

