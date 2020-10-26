using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  24.03.2012  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class AspnetMembershipsRepository : IAspnetMembershipsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<AspnetMembership> AspnetMemberships     {         
            get { return _entities.AspnetMemberships; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<AspnetMembership> FindAll()    {         
            return _entities.AspnetMemberships; 
        }   

         public IQueryable<AspnetMembership> FindAllSort()    {
            return from d in _entities.AspnetMemberships 
                   orderby d.Password
                   select d;
        }                                                                                                                                                                   
      
          public AspnetMembership Get(Guid id)        {
            return _entities.AspnetMemberships.FirstOrDefault(d => d.UserId == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(AspnetMembership aspnetMembership)    {
            _entities.AspnetMemberships.Add(aspnetMembership);           
        }

        public void Delete(AspnetMembership aspnetMembership)    {
            _entities.AspnetMemberships.Remove(aspnetMembership);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

