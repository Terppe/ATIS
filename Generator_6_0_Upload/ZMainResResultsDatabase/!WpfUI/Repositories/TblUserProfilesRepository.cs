using System.Linq; 
using Atis.WpfUi.Interfaces;
using Atis.WpfUi.Model;    

// <!-- Repository Skriptdatum:   15.11.2017  10:32    -->  

namespace Atis.WpfUi.Repositories      {  
    public class TblUserProfilesRepository : ITblUserProfilesRepository    {
        private readonly ATISEntities _entities = new ATISEntities();

        public IQueryable<TblUserProfile> TblUserProfiles     {         
            get { return _entities.TblUserProfiles; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<TblUserProfile> FindAll()    {         
            return _entities.TblUserProfiles; 
        }   

            public IQueryable<TblUserProfile> FindAllSort()    {
            return from d in _entities.TblUserProfiles
                   orderby d.LastName
                   select d;
        }                                                                                                                                                                   
  
          public TblUserProfile Get(int id)        {
            return _entities.TblUserProfiles.FirstOrDefault(d => d.UserProfileID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(TblUserProfile tbluserprofile)    {
            _entities.TblUserProfiles.Add(tbluserprofile);           
        }

        public void Delete(TblUserProfile tbluserprofile)    {
            _entities.TblUserProfiles.Remove(tbluserprofile);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            _entities.SaveChanges();
        }   
 
    }
}   

