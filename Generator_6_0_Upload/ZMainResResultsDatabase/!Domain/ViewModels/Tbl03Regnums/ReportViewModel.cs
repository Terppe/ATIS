using System;
using System.Collections.Generic;
using Atis.Domain.Models;

// <!-- ReportViewModel Skriptdatum:  12.03.2014  12:32      -->  

namespace Atis.Domain.ViewModels.Tbl03Regnums     {    

    public class ReportViewModel                         {   
        // Constructor
        public ReportViewModel()     {
            // Define any default values here...
            Valid = false; 
        } 

         // Data properties  
     
         public IEnumerable<Tbl03Regnum> Tbl03Regnums { get; set; }    
  
        // Properties        

        public string Author { get; set; }
        public DateTime AuthorYear { get; set; }
        public bool Valid { get; set; }
        public string EngName { get; set; }
        public string GerName { get; set; }
        public string FraName { get; set; }
        public string PorName { get; set; }

        public int? CountID { get; set; }
        public string Synonym { get; set; }  

        public int? PhylumID { get; set; }
        public string PhylumName { get; set; }   
                                    
        public IList<Tbl06Phylum> Tbl06PhylumsSearchResults { get; set; }  
  
        public int? DivisionID { get; set; }
        public string DivisionName { get; set; }    
                                    
        public IList<Tbl09Division> Tbl09DivisionsSearchResults { get; set; }                             
  
        public int? SubphylumID { get; set; }
        public string SubphylumName { get; set; }    
                                    
        public IList<Tbl12Subphylum> Tbl12SubphylumsSearchResults { get; set; }                             
  
        public int? SubdivisionID { get; set; }
        public string SubdivisionName { get; set; }    
                                    
        public IList<Tbl15Subdivision> Tbl15SubdivisionsSearchResults { get; set; }                             
  
        public int? ReferenceID { get; set; }
        public string ReferenceName { get; set; }    
                                    
        public IList<Tbl90Reference> Tbl90ReferencesSearchResults { get; set; }                             
  
        public int? RefSourceID { get; set; }
        public string RefSourceName { get; set; }    
                                    
        public IList<Tbl90RefSource> Tbl90RefSourcesSearchResults { get; set; }                             
  
        public int? RefExpertID { get; set; }
        public string RefExpertName { get; set; }    
                                    
        public IList<Tbl90RefExpert> Tbl90RefExpertsSearchResults { get; set; }                             
  
        public int? RefAuthorID { get; set; }
        public string RefAuthorName { get; set; }    
                                    
        public IList<Tbl90RefAuthor> Tbl90RefAuthorsSearchResults { get; set; }                             
  
        public int? CommentID { get; set; }
        public string CommentName { get; set; }    
                                    
        public IList<Tbl93Comment> Tbl93CommentsSearchResults { get; set; }                             
  
        
        public int? RegnumID { get; set; }
        public string RegnumName { get; set; }  
       
        public string Subregnum { get; set; }  
                                       
         public IList<Tbl03Regnum> Tbl03RegnumsSearchResults { get; set; }  
 
    }
}   

