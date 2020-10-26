using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Atis.Domain.Models;

// <!-- ListViewModel Skriptdatum:  06.02.2012  10:32    -->  

namespace Atis.Domain.ViewModels.aspnet_Memberships
{    

    public class ListViewModel                     
    {   
        // Constructor
        public ListViewModel()
        {
            // Define any default values here...
            PageSize = 10;
            NumericPageCount = 10;
            Valid = false; 
        } 

        // Data properties  
         public IEnumerable<aspnet_Membership> aspnet_Memberships { get; set; }    
 
      
        // Sorting-related properties
        public string SortBy { get; set; }
        public bool SortAscending { get; set; }
        public string SortExpression
        {
            get
            {
                return SortAscending ? SortBy + " asc" : SortBy + " desc";
            }
        }

        // Paging-related properties
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecordCount { get; set; }
        public int PageCount
        {
            get
            {
                return Math.Max(TotalRecordCount / PageSize, 1);
            }
        }
        public int NumericPageCount { get; set; }

        // Filtering-related properties    

  
        public bool Valid { get; set; }                       

        public int? ApplicationId { get; set; }
        public string ApplicationName { get; set; }   
       
        public IEnumerable<SelectListItem>aspnet_ApplicationList { get; set; }   
  
        public int? UserId { get; set; }
        public string UserName { get; set; }    
       
        public IEnumerable<SelectListItem>aspnet_UserList { get; set; }                  
    
        
        public int? UserId { get; set; }
        public string UserName { get; set; }  

    }
}   

