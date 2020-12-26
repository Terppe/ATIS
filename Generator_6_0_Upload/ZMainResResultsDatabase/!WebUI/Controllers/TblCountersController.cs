using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Globalization;  
using Atis.Domain.Models;  
using Atis.Domain.Repositories;  
using Atis.Domain.ViewModels.TblCounters;         
    
// <!-- Controller Skriptdatum:  3.1.2012  12:32      -->  

namespace Atis.WebUI.Controllers     {  
    [HandleError]
    public class TblCountersController : LanguageBaseController    { 
        readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
    
           //-----------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /TblCounter/             
        public ActionResult Index(string sortBy = "CounterName", bool ascending = true, int page = 1, int pageSize = 12, bool? valid = null, string counterName = null, int? counterId = null)  {                                    
       
            var model = new ListViewModel    {
                // Sorting-related properties
                SortBy = sortBy,
                SortAscending = ascending,

                // Paging-related properties
                CurrentPageIndex = page,
                PageSize = pageSize,    


                 
            }; 
       
            var filteredResults = _tblCountersRepository.TblCounters.AsQueryable();   
 
            // Filter on CounterName
            if (!string.IsNullOrEmpty(counterName))
                filteredResults = filteredResults.Where(a => a.CounterName.StartsWith(counterName));      
 
            // Filter on CounterID
            if (counterId.HasValue)
                filteredResults = filteredResults.Where(a => a.CounterID == counterId);   
 

            // Determine the total number of FILTERED products being paged through (needed to compute PageCount)
            model.TotalRecordCount = filteredResults.Count();

            // Get the current page of sorted, filtered products
            model.TblCounters = filteredResults
                                        .OrderBy(model.SortExpression)
                                        .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                                        .Take(model.PageSize);

            return View(model);

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

  
        //
        // GET: /TblCounters/Details/5     
        public ActionResult Details(int id)    {  
             var tblCounter = _tblCountersRepository.Get(id);   
 
             return tblCounter == null ? View("NotFound") : View("Details", tblCounter);
        }      
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
 
          <!-- _Form.cshtml  Skriptdatum: 3.1.2012  12:32      -->     
  
        //
        // GET: /TblCounters/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int NULLId)              {   
             var tblCounter = new TblCounter {NULL = NULLId};           
     
             return View(tblCounter);
        }     
  
        //       
        // POST: /TblCounters/Create
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Create([Bind(Exclude = "CounterID")]FormCollection collection)    {               
  
            if (!TryUpdateModel(tblCounter ))
                return View(tblCounter);                                    

            //Add
            _tblCountersRepository.Add(tblCounter);   
            _tblCountersRepository.Save();   

            TempData["message"] = tblCounter.CounterName + " " + SharedRes.StringsRes.TempDataSavedMessage;   
 
   
            return View(tblCounter);    //redisplay same view                     
        }    
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------  
 
        //
        // GET: /TblCounters/Edit/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]           
        public ActionResult Edit(int id)    {   
            var tblCounter = _tblCountersRepository.Get(id);    
  
            return View(tblCounter);
        }  
 
        //
        // POST: /TblCounters/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Edit(int id,  FormCollection collection)  {  
            var tblCounter = _tblCountersRepository.Get(id);   
  
                            
            if(!TryUpdateModel(tblCounter))
                return View(tblCounter);    
_tblCountersRepository.Save();      

           TempData["message"] = tblCounter.CounterName + " " + SharedRes.StringsRes.TempDataSavedMessage;   

           return RedirectToAction("Index", tblCounter);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
  
        //
        // GET: /TblCounters/Delete/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id)  {  
            var tblCounter = _tblCountersRepository.Get(id); 
  
            return tblCounter == null ? View("NotFound") : View(tblCounter);                      
        }   
  
        //
        // POST: /TblCounters/Delete/5
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id, string confirmButton)    {  
            var tblCounter = _tblCountersRepository.Get(id);   

            if (tblCounter == null)
                return RedirectToAction("NotFound");
            
            _tblCountersRepository.Delete(tblCounter);   

            TempData["message"] = tblCounter.CounterName + " " + SharedRes.StringsRes.TempDataDeletedMessage;   
            _tblCountersRepository.Save();  
 
            return RedirectToAction("Index");
        }    
 
        public ActionResult NotFound()    {
            throw new NotImplementedException();
        }
   }
}   

