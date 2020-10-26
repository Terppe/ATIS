using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  22.12.2017  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class Tbl45FamiliesRepository : ITbl45FamiliesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<Tbl45Family> Tbl45Families     {         
            get { return _entities.Tbl45Families; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl45Family> FindAll()    {         
            return _entities.Tbl45Families; 
        }   

            public IQueryable<Tbl45Family> FindAllSort()    {
            return from d in _entities.Tbl45Families
                   orderby d.FamilyName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl45Family Get(int id)        {
            return _entities.Tbl45Families.FirstOrDefault(d => d.FamilyID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl45Family tbl45Family)    {
            _entities.Tbl45Families.Add(tbl45Family);           
        }

        public void Delete(Tbl45Family tbl45Family)    {
            _entities.Tbl45Families.Remove(tbl45Family);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

