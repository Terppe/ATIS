using System.Linq;
using Atis.WpfUi.Model;      
   
// <!-- Interface Skriptdatum:   15.11.2017  10:32     -->  

namespace Atis.WpfUi.Interfaces     {         
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

