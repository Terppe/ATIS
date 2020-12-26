using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Atis.Domain.Models;

// <!-- ListViewModel Skriptdatum:  24.03.2012  10:32    -->  

namespace Atis.Domain.ViewModels.AspnetMemberships
{    
     
    public class ListViewModel                     
    {   
        // Constructor
        public ListViewModel()
        {
            // Define any default values here...
            PageSize = 10;
            NumericPageCount = 10;
        }  

        // Data properties  
         public IEnumerable<AspnetMembership> AspnetMemberships { get; set; }    
 
      
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


        public Guid? ApplicationId { get; set; }
        public string ApplicationName { get; set; }   
        public IEnumerable<SelectListItem>ApplicationList { get; set; }   
  
        public Guid? UserId { get; set; }
        public string UserName { get; set; }    
        public IEnumerable<SelectListItem>UserList { get; set; }                  
        
        
        public Guid? UserId { get; set; }
        public string UserName { get; set; }  

    }
}   

