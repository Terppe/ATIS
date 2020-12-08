using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  01.02.2012  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class aspnet_UsersRepository : Iaspnet_UsersRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<aspnet_User> aspnet_Users     {         
            get { return _entities.aspnet_Users; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<aspnet_User> FindAll()    {         
            return _entities.aspnet_Users; 
        }   

            public IQueryable<aspnet_User> FindAllSort()    {
            return from d in _entities.aspnet_Users
                   orderby d.UserName
                   select d;
        }                                                                                                                                                                   
  
          public aspnet_User Get(int id)        {
            return _entities.aspnet_Users.FirstOrDefault(d => d.UserId == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(aspnet_User aspnet_user)    {
            _entities.aspnet_Users.Add(aspnet_user);           
        }

        public void Delete(aspnet_User aspnet_user)    {
            _entities.aspnet_Users.Remove(aspnet_user);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

