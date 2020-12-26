using System;
using System.Data.Linq.Mapping;
using System.Web.Mvc;
using Atis.Domain.Helpers;      
using System.ComponentModel.DataAnnotations;    

// <!-- Entities Skriptdatum:  15.03.2012  10:32    -->  

namespace Atis.Domain.Models     {  
    [MetadataType(typeof(Tbl69FiSpeciesValidation))]
    public partial class Tbl69FiSpecies    {   
   

    }     

    public class Tbl69FiSpeciesValidation    {               
  
        [HiddenInput(DisplayValue = false)]   
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]   
        public int FiSpeciesID { get; set; }   
      
        [Required(ErrorMessageResourceName = "RequiredFiSpeciesName", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [LocalizedDisplayName("NameTbl69FiSpecies", NameResourceType = typeof(SharedRes.StringsRes))]
        [StringLength(200, ErrorMessageResourceName = "StringLength_200", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string FiSpeciesName { get; set; }   
  
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
        public bool TypeSpecies { get; set; }    

        [StringLength(10, ErrorMessageResourceName = "StringLength_10", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string LNumber { get; set; }    
 
        [StringLength(50, ErrorMessageResourceName = "StringLength_50", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string LOrigin { get; set; }     

        [StringLength(10, ErrorMessageResourceName = "StringLength_10", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string LDANumber { get; set; }    
 
        [StringLength(50, ErrorMessageResourceName = "StringLength_50", ErrorMessageResourceType = typeof(SharedRes.StringsRes))]
        [Column]
        public string LDAOrigin { get; set; }     

        [Column]
        public int BasinLength { get; set; }   

        [Column]
        public decimal FishLength { get; set; }   

        [Column]
        public bool Karnivore { get; set; }    

        [Column]
        public bool Herbivore { get; set; }    

        [Column]
        public bool Limnivore { get; set; }    

        [Column]
        public bool Omnivore { get; set; }    

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoFoods { get; set; }                

        [Column]
        public bool Difficult1 { get; set; }    

        [Column]
        public bool Difficult2 { get; set; }    

        [Column]
        public bool Difficult3 { get; set; }    

        [Column]
        public bool Difficult4 { get; set; }    

        [Column]
        public bool RegionTop { get; set; }    

        [Column]
        public bool RegionMiddle { get; set; }    

        [Column]
        public bool RegionBottom { get; set; }    

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoRegion { get; set; }                

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
        public string MemoHusbandry { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoBreeding { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoBuilt { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoColor { get; set; }     
           
        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoSozial { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoDomorphism { get; set; }                

        [DataType(DataType.MultilineText)]
        [Column]
        public string MemoSpecial { get; set; }                                                                                                                                                                                                                                                                                                         
 
    }
}   


