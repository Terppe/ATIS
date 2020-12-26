using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  24.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class AspnetMembershipsRepository : IAspnetMembershipsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<AspnetMembership> AspnetMemberships     {         
            get { return Entities.AspnetMemberships; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<AspnetMembership> FindAll()    {         
            return Entities.AspnetMemberships; 
        }   

         public IQueryable<AspnetMembership> FindAllSort()    {
            return from d in Entities.AspnetMemberships 
                   orderby d.Password
                   select d;
        }                                                                                                                                                                   
      
          public AspnetMembership Get(Guid id)        {
            return Entities.AspnetMemberships.FirstOrDefault(d => d.UserId == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(AspnetMembership aspnetMembership)    {
            Entities.AspnetMemberships.AddObject(aspnetMembership);           
        }

        public void Delete(AspnetMembership aspnetMembership)    {
            Entities.AspnetMemberships.DeleteObject(aspnetMembership);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

