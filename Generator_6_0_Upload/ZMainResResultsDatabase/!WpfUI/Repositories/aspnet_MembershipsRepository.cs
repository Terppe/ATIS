using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  06.02.2012  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class aspnet_MembershipsRepository : Iaspnet_MembershipsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<aspnet_Membership> aspnet_Memberships     {         
            get { return _entities.aspnet_Memberships; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<aspnet_Membership> FindAll()    {         
            return _entities.aspnet_Memberships; 
        }   

            public IQueryable<aspnet_Membership> FindAllSort()    {
            return from d in _entities.aspnet_Memberships
                   orderby d.UserName
                   select d;
        }                                                                                                                                                                   
  
          public aspnet_Membership Get(int id)        {
            return _entities.aspnet_Memberships.FirstOrDefault(d => d.UserId == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(aspnet_Membership aspnet_membership)    {
            _entities.aspnet_Memberships.Add(aspnet_membership);           
        }

        public void Delete(aspnet_Membership aspnet_membership)    {
            _entities.aspnet_Memberships.Remove(aspnet_membership);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

