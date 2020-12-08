using System;
using System.Data.Linq.Mapping;
using System.Web.Mvc;
using Atis.Domain.Helpers;      
using System.ComponentModel.DataAnnotations;    

// <!-- Entities Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.Domain.Models     {  
    [MetadataType(typeof(Tbl72PlSpeciesValidation))]
    public partial class Tbl72PlSpecies    {   
   

    }     

    public class Tbl72PlSpeciesValidation    {               
  
        [HiddenInput(DisplayValue = false)]   
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]   
        public int PlSpeciesID { get; set; }   
      
        [Required(ErrorMessageResourceName = "RequiredPlSpeciesName", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [LocalizedDisplayName("NameTbl72PlSpecies", NameResourceType = typeof(SharedRes.StringsRes))]
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string PlSpeciesName { get; set; }   
  
        [HiddenInput(DisplayValue = false)]
        [Required(ErrorMessageResourceName = "RequiredCountID", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public int CountID { get; set; }   
      
        [Column]
        public bool? Valid { get; set; }    
      
        [StringLength(4, MinimumLength=4, ErrorMessageResourceName = "StringLengthYear_4", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string ValidYear { get; set; }    
      
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Author { get; set; }     
      
        [StringLength(4, MinimumLength=4, ErrorMessageResourceName = "StringLengthYear_4", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string AuthorYear { get; set; }     
      
        [HiddenInput(DisplayValue = false)]
        [Column]
        public string Writer { get; set; }     

        [HiddenInput(DisplayValue = false)]
        [Column]
        public DateTime WriterDate { get; set; }     

        [HiddenInput(DisplayValue = false)]
        [Column]
        public string Updater { get; set; }     

        [HiddenInput(DisplayValue = false)]
        [Column]
        public DateTime UpdaterDate { get; set; }       
            
        [Column]
        public int GenusID { get; set; }          
        
        [Column]
        public int SpeciesgroupID { get; set; }          
         
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Subspecies { get; set; }   
                                                                                                                                                                                                                                                                                      
        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Divers { get; set; } 

        [StringLength(60, ErrorMessageResourceName = "StringLength_60", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string Importer { get; set; }     

        [Column]
        public string ImportingYear { get; set; }     

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoSpecies { get; set; }                

        [StringLength(100, ErrorMessageResourceName = "StringLength_100", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string TradeName { get; set; } 

        [Column]
        public int BasinHeight { get; set; }   

        [Column]
        public decimal PlantLength { get; set; }   

        [Column]
        public bool Difficult1 { get; set; }    

        [Column]
        public bool Difficult2 { get; set; }    

        [Column]
        public bool Difficult3 { get; set; }    

        [Column]
        public bool Difficult4 { get; set; }    

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoTech { get; set; }                

        [Column]
        public decimal Ph1 { get; set; }   

        [Column]
        public decimal Ph2 { get; set; }   

        [Column]
        public int Temp1 { get; set; }   

        [Column]
        public int Temp2 { get; set; }   

        [Column]
        public int Hardness1 { get; set; }   

        [Column]
        public int Hardness2 { get; set; }   

        [Column]
        public int CarboHardness1 { get; set; }   

        [Column]
        public int CarboHardness2 { get; set; }   

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoBuilt { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoColor { get; set; }     
           
        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoReproduction { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoCulture { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoGlobal { get; set; }                                                                                                                                                                                                                                                                                                         
 
    }
}   


