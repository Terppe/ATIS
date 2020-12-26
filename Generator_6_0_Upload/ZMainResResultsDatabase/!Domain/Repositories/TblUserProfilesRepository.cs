using System;
using System.Linq; 
using Atis.Domain.Models;
using Atis.Domain.Interfaces;    

// <!-- Repository Skriptdatum:  16.03.2012  10:32    -->  

namespace Atis.Domain.Repositories      {  
    public class TblUserProfilesRepository : ITblUserProfilesRepository    {
        public ATISEntities Entities = new ATISEntities();

        public IQueryable<TblUserProfile> TblUserProfiles     {         
            get { return Entities.TblUserProfiles; }
        }   
        //------------------------------------------------------------------------------------------     

        //  Query Methods  
        public IQueryable<TblUserProfile> FindAll()    {         
            return Entities.TblUserProfiles; 
        }   

            public IQueryable<TblUserProfile> FindAllSort()    {
            return from d in Entities.TblUserProfiles
                   orderby d.LastName
                   select d;
        }                                                                                                                                                                   
  
          public TblUserProfile Get(int id)        {
            return Entities.TblUserProfiles.FirstOrDefault(d => d.UserProfileID == id);
        }          
        

         //-------------------------------------------------------------------------------------------------------------

        //  Insert/Delete Methods
        public void Add(TblUserProfile tbluserprofile)    {
            Entities.TblUserProfiles.AddObject(tbluserprofile);           
        }

        public void Delete(TblUserProfile tbluserprofile)    {
            Entities.TblUserProfiles.DeleteObject(tbluserprofile);
        } 
        //-----------------------------------------------------------------------------------------------------------

        //  Persistance
        public void Save()     {           
            Entities.SaveChanges();
        }   
 
    }
}   

