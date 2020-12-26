using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Globalization;  
using Atis.Domain.Models;  
using Atis.Domain.Repositories;  
using Atis.Domain.ViewModels.Tbl03Regnums;         
    
// <!-- Controller Skriptdatum:  18.3.2012  12:32      -->  

namespace Atis.WebUI.Controllers     {  
    [HandleError]
    public class Tbl03RegnumsController : LanguageBaseController    { 
        readonly Tbl03RegnumsRepository _tbl03RegnumsRepository = new Tbl03RegnumsRepository();   
    
         readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /Tbl03Regnum/  
        public ActionResult Index(string sortBy = "RegnumName", bool ascending = true, int page = 1, int pageSize = 12, bool? valid = null, string regnumName = null, int? regnumId = null)  {                                    
       
            var model = new ListViewModel    {
                // Sorting-related properties
                SortBy = sortBy,
                SortAscending = ascending,

                // Paging-related properties
                CurrentPageIndex = page,
                PageSize = pageSize,    


                Valid = valid.HasValue && valid.Value  
            }; 
       
            var filteredResults = _tbl03RegnumsRepository.Tbl03Regnums.AsQueryable();   
 
            // Filter on RegnumName
            if (!string.IsNullOrEmpty(regnumName))
                filteredResults = filteredResults.Where(a => a.RegnumName.StartsWith(regnumName));      
 
            // Filter on RegnumID
            if (regnumId.HasValue)
                filteredResults = filteredResults.Where(a => a.RegnumID == regnumId);   
  
            //Filter on Valid
            if (valid != null && valid.Value)
                filteredResults = filteredResults.Where(p => p.Valid == true);   
 

            // Determine the total number of FILTERED products being paged through (needed to compute PageCount)
            model.TotalRecordCount = filteredResults.Count();

            // Get the current page of sorted, filtered products
            model.Tbl03Regnums = filteredResults
                                        .OrderBy(model.SortExpression)
                                        .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                                        .Take(model.PageSize);

            return View(model);

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

  
        //
        // GET: /Tbl03Regnums/Details/5     
        public ActionResult Details(int id)    {  
             var tbl03Regnum = _tbl03RegnumsRepository.Get(id);   
 
             return tbl03Regnum == null ? View("NotFound") : View("Details", tbl03Regnum);
        }      
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
 
          <!-- _Form.cshtml  Skriptdatum: 18.3.2012  12:32      -->     
   
        //
        // GET: /Tbl03Regnums/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create()              {   
             var tbl03Regnum = new Tbl03Regnum();   

             return View(tbl03Regnum);
        }     
  
        //       
        // POST: /Tbl03Regnums/Create
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Create([Bind(Exclude = "RegnumID")]FormCollection collection)    {               
  
            var tbl03Regnum = new Tbl03Regnum       {
                CountID =_tblCountersRepository.Counter(),                  
                Writer = User.Identity.Name,
                WriterDate = DateTime.Now,
                Updater = User.Identity.Name,
                UpdaterDate = DateTime.Now  
            };                           
  
            if (!TryUpdateModel(tbl03Regnum ))
                return View(tbl03Regnum);                                    
  
            if (tbl03Regnum.Subregnum == null)    {    
                tbl03Regnum.Subregnum = " ";    } //fill with "" to show in DDL for Phylum and Division aso.    
            _tbl03RegnumsRepository.Add(tbl03Regnum);   
            _tbl03RegnumsRepository.Save();   

            TempData["message"] = tbl03Regnum.RegnumName + " " + SharedRes.StringsRes.TempDataSavedMessage;   
 
   
            return View(tbl03Regnum);    //redisplay same view                     
        }    
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------  
 
        //
        // GET: /Tbl03Regnums/Edit/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]           
        public ActionResult Edit(int id)    {   
            var tbl03Regnum = _tbl03RegnumsRepository.Get(id);    
  
            return View(tbl03Regnum);
        }  
 
        //
        // POST: /Tbl03Regnums/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Edit(int id,  FormCollection collection)  {  
            var tbl03Regnum = _tbl03RegnumsRepository.Get(id);   
  
                            
            if(!TryUpdateModel(tbl03Regnum))
                return View(tbl03Regnum);    
  
            if (tbl03Regnum.Subregnum == null)    {    
                tbl03Regnum.Subregnum = " ";    } //fill with "" to show in DDL for Phylum and Division aso.   
            tbl03Regnum.Updater = User.Identity.Name;
            tbl03Regnum.UpdaterDate = DateTime.Now;

            _tbl03RegnumsRepository.Save();      

           TempData["message"] = tbl03Regnum.RegnumName + " " + SharedRes.StringsRes.TempDataSavedMessage;   

           return RedirectToAction("Index", tbl03Regnum);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
  
        //
        // GET: /Tbl03Regnums/Delete/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id)  {  
            var tbl03Regnum = _tbl03RegnumsRepository.Get(id); 
  
            return tbl03Regnum == null ? View("NotFound") : View(tbl03Regnum);                      
        }   
  
        //
        // POST: /Tbl03Regnums/Delete/5
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id, string confirmButton)    {  
            var tbl03Regnum = _tbl03RegnumsRepository.Get(id);   

            if (tbl03Regnum == null)
                return RedirectToAction("NotFound");
            
            _tbl03RegnumsRepository.Delete(tbl03Regnum);   

            TempData["message"] = tbl03Regnum.RegnumName + " " + SharedRes.StringsRes.TempDataDeletedMessage;   
            _tbl03RegnumsRepository.Save();  
 
            return RedirectToAction("Index");
        }    
 
        public ActionResult NotFound()    {
            throw new NotImplementedException();
        }
   }
}   

