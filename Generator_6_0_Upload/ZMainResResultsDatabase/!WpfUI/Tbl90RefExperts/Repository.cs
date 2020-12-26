using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  29.12.2011  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl90RefExpertsRepository : ITbl90RefExpertsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl90RefExpert> Tbl90RefExperts     {         
            get { return _entities.Tbl90RefExperts; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl90RefExpert> FindAll()    {         
            return _entities.Tbl90RefExperts; 
        }   

            public IQueryable<Tbl90RefExpert> FindAllSort()    {
            return from d in _entities.Tbl90RefExperts
                   orderby d.RefExpertName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl90RefExpert Get(int id)        {
            return _entities.Tbl90RefExperts.FirstOrDefault(d => d.RefExpertID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl90RefExpert tbl90RefExpert)    {
            _entities.Tbl90RefExperts.Add(tbl90RefExpert);           
        }

        public void Delete(Tbl90RefExpert tbl90RefExpert)    {
            _entities.Tbl90RefExperts.Remove(tbl90RefExpert);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

