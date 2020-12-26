using System;
using System.Data.Linq.Mapping;
using System.Web.Mvc;
using Atis.Domain.Helpers;      
using System.ComponentModel.DataAnnotations;    

// <!-- Entities Skriptdatum:  3.1.2012  12:32      -->  

namespace Atis.Domain.Models     {  
    [MetadataType(typeof(TblCounterValidation))]
    public partial class TblCounter    {   
   

    }     

    public class TblCounterValidation    {               
  
        [HiddenInput(DisplayValue = false)]   
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]   
        public int CounterID { get; set; }   
  
        [Column]
        public string CounterName { get; set; }   
 
    }
}   


