using System.Linq;
using Atis.Domain.Models;      
   
// <!-- Interface Skriptdatum:  16.03.2012  10:32     -->  

namespace Atis.Domain.Interfaces     {         
    public interface ITblUserProfilesRepository    {
        IQueryable<TblUserProfile> TblUserProfiles { get; }                   
        IQueryable<TblUserProfile> FindAll();
        IQueryable<TblUserProfile> FindAllSort();          
    
TblUserProfile Get(int id);   
        

        void Add(TblUserProfile tbluserprofile);
        void Delete(TblUserProfile tbluserprofile);
        void Save( );               
     }
}   

