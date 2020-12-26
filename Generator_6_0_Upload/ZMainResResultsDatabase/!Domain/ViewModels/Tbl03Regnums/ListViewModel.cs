using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Atis.Domain.Models;

// <!-- ListViewModel Skriptdatum:  12.03.2014  12:32      -->  

namespace Atis.Domain.ViewModels.Tbl03Regnums
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
         public IEnumerable<Tbl03Regnum> Tbl03Regnums { get; set; }    
 
      
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
  
        
        public int? RegnumID { get; set; }
        public string RegnumName { get; set; }  

        public string Subregnum { get; set; }  
    
    }
}   

