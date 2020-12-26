using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  01.02.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class aspnet_UsersRepository : Iaspnet_UsersRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<aspnet_User> aspnet_Users     {         
            get { return Entities.aspnet_Users; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<aspnet_User> FindAll()    {         
            return Entities.aspnet_Users; 
        }   

            public IQueryable<aspnet_User> FindAllSort()    {
            return from d in Entities.aspnet_Users
                   orderby d.UserName
                   select d;
        }                                                                                                                                                                   
  
          public aspnet_User Get(int id)        {
            return Entities.aspnet_Users.FirstOrDefault(d => d.UserId == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(aspnet_User aspnet_user)    {
            Entities.aspnet_Users.AddObject(aspnet_user);           
        }

        public void Delete(aspnet_User aspnet_user)    {
            Entities.aspnet_Users.DeleteObject(aspnet_user);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

