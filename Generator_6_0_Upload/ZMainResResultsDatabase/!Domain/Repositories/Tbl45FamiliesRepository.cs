using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class Tbl45FamiliesRepository : ITbl45FamiliesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<Tbl45Family> Tbl45Families     {         
            get { return Entities.Tbl45Families; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<Tbl45Family> FindAll()    {         
            return Entities.Tbl45Families; 
        }   

            public IQueryable<Tbl45Family> FindAllSort()    {
            return from d in Entities.Tbl45Families
                   orderby d.FamilyName
                   select d;
        }                                                                                                                                                                   
  
          public Tbl45Family Get(int id)        {
            return Entities.Tbl45Families.FirstOrDefault(d => d.FamilyID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(Tbl45Family tbl45Family)    {
            Entities.Tbl45Families.AddObject(tbl45Family);           
        }

        public void Delete(Tbl45Family tbl45Family)    {
            Entities.Tbl45Families.DeleteObject(tbl45Family);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

