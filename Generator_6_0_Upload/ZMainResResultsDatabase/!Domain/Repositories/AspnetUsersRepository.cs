using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  16.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class AspnetUsersRepository : IAspnetUsersRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<AspnetUser> AspnetUsers     {         
            get { return Entities.AspnetUsers; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<AspnetUser> FindAll()    {         
            return Entities.AspnetUsers; 
        }   

            public IQueryable<AspnetUser> FindAllSort()    {
            return from d in Entities.AspnetUsers
                   orderby d.UserName
                   select d;
        }                                                                                                                                                                   
      
          public AspnetUser Get(Guid id)        {
            return Entities.AspnetUsers.FirstOrDefault(d => d.UserId == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(AspnetUser aspnetUser)    {
            Entities.AspnetUsers.AddObject(aspnetUser);           
        }

        public void Delete(AspnetUser aspnetUser)    {
            Entities.AspnetUsers.DeleteObject(aspnetUser);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

