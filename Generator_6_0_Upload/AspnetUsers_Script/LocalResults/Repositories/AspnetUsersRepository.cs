using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  16.03.2012  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class AspnetUsersRepository : IAspnetUsersRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<AspnetUser> AspnetUsers     {         
            get { return _entities.AspnetUsers; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<AspnetUser> FindAll()    {         
            return _entities.AspnetUsers; 
        }   

            public IQueryable<AspnetUser> FindAllSort()    {
            return from d in _entities.AspnetUsers
                   orderby d.UserName
                   select d;
        }                                                                                                                                                                   
      
          public AspnetUser Get(Guid id)        {
            return _entities.AspnetUsers.FirstOrDefault(d => d.UserId == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(AspnetUser aspnetUser)    {
            _entities.AspnetUsers.Add(aspnetUser);           
        }

        public void Delete(AspnetUser aspnetUser)    {
            _entities.AspnetUsers.Remove(aspnetUser);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

