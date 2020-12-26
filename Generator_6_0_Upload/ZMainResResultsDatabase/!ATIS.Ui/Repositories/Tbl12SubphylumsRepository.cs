using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  01.12.2020  12:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl12SubphylumsRepository : ITbl12SubphylumsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl12Subphylum> Tbl12Subphylums     {         
            get { return _entities.Tbl12Subphylums; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl12Subphylum> FindAll()    {         
            return _entities.Tbl12Subphylums; 
        }   

            public IQueryable<Tbl12Subphylum> FindAllSort()    {
            return from d in _entities.Tbl12Subphylums
                   orderby d.SubphylumName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl12Subphylum Get(int id)        {
            return _entities.Tbl12Subphylums.FirstOrDefault(d => d.SubphylumId == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl12Subphylum subphylum)    {
            _entities.Tbl12Subphylums.Add(tbl12Subphylum);           
        }

        public void Delete(Tbl12Subphylum subphylum)    {
            _entities.Tbl12Subphylums.Remove(tbl12Subphylum);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

