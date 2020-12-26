using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:  23.03.2012  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class AspnetApplicationsRepository : IAspnetApplicationsRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<AspnetApplication> AspnetApplications     {         
            get { return _entities.AspnetApplications; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<AspnetApplication> FindAll()    {         
            return _entities.AspnetApplications; 
        }   

            public IQueryable<AspnetApplication> FindAllSort()    {
            return from d in _entities.AspnetApplications
                   orderby d.ApplicationName
                   select d;
        }                                                                                                                                                                   
      
          public AspnetApplication Get(Guid id)        {
            return _entities.AspnetApplications.FirstOrDefault(d => d.ApplicationId == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(AspnetApplication aspnetApplication)    {
            _entities.AspnetApplications.Add(aspnetApplication);           
        }

        public void Delete(AspnetApplication aspnetApplication)    {
            _entities.AspnetApplications.Remove(aspnetApplication);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

