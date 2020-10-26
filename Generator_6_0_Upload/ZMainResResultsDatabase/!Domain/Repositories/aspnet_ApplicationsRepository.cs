using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  16.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class AspnetApplicationsRepository : IAspnetApplicationsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<AspnetApplication> AspnetApplications     {         
            get { return Entities.AspnetApplications; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<AspnetApplication> FindAll()    {         
            return Entities.AspnetApplications; 
        }   

            public IQueryable<AspnetApplication> FindAllSort()    {
            return from d in Entities.AspnetApplications
                   orderby d.ApplicationName
                   select d;
        }                                                                                                                                                                   
      
          public AspnetApplication Get(Guid id)        {
            return Entities.AspnetApplications.FirstOrDefault(d => d.ApplicationId == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(AspnetApplication aspnetApplication)    {
            Entities.AspnetApplications.AddObject(aspnetApplication);           
        }

        public void Delete(AspnetApplication aspnetApplication)    {
            Entities.AspnetApplications.DeleteObject(aspnetApplication);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

