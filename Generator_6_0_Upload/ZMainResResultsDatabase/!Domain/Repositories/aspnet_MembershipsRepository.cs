using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  06.02.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class aspnet_MembershipsRepository : Iaspnet_MembershipsRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<aspnet_Membership> aspnet_Memberships     {         
            get { return Entities.aspnet_Memberships; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<aspnet_Membership> FindAll()    {         
            return Entities.aspnet_Memberships; 
        }   

            public IQueryable<aspnet_Membership> FindAllSort()    {
            return from d in Entities.aspnet_Memberships
                   orderby d.UserName
                   select d;
        }                                                                                                                                                                   
  
          public aspnet_Membership Get(int id)        {
            return Entities.aspnet_Memberships.FirstOrDefault(d => d.UserId == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(aspnet_Membership aspnet_membership)    {
            Entities.aspnet_Memberships.AddObject(aspnet_membership);           
        }

        public void Delete(aspnet_Membership aspnet_membership)    {
            Entities.aspnet_Memberships.DeleteObject(aspnet_membership);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

