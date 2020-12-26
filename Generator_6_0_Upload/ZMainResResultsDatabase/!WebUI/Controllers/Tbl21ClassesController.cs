using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Globalization;  
using Atis.Domain.Models;  
using Atis.Domain.Repositories;  
using Atis.Domain.ViewModels.Tbl21Classes;         
    
// <!-- Controller Skriptdatum:  15.03.2012  18:32    -->  

namespace Atis.WebUI.Controllers     {  
    [HandleError]
    public class Tbl21ClassesController : LanguageBaseController    { 
        readonly Tbl21ClassesRepository _tbl21ClassesRepository = new Tbl21ClassesRepository();   
    
        readonly  Tbl18SuperclassesRepository _tbl18SuperclassesRepository = new Tbl18SuperclassesRepository();   
    
         readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /Tbl21Class/  
        public ActionResult Index(string sortBy = "ClassName", bool ascending = true, int page = 1, int pageSize = 12, int? superclassId = null, bool? valid = null, string className = null, int? classId = null)    {                                    
       
            var model = new ListViewModel    {
                // Sorting-related properties
                SortBy = sortBy,
                SortAscending = ascending,

                // Paging-related properties
                CurrentPageIndex = page,
                PageSize = pageSize,    


               // 
                SuperclassID = superclassId,
                SuperclassList = _tbl18SuperclassesRepository.Tbl18Superclasses.Select(a =>
                                new     {
                                    a.SuperclassName,
                                    a.SuperclassID
                                }
                            )
                            .OrderBy(a => a.SuperclassName)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.SuperclassName,
                                    Value = a.SuperclassID.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };                                        
       
            var filteredResults = _tbl21ClassesRepository.Tbl21Classes.AsQueryable();   
 
            // Filter on ClassName
            if (!string.IsNullOrEmpty(className))
                filteredResults = filteredResults.Where(a => a.ClassName.StartsWith(className));      
 
            // Filter on ClassID
            if (classId.HasValue)
                filteredResults = filteredResults.Where(a => a.ClassID == classId);   

           // Filter on SuperclassID 
            if (superclassId != null)
                filteredResults = filteredResults.Where(p => p.SuperclassID == superclassId.Value);                                        
      
            //Filter on Valid
            if (valid != null && valid.Value)
                filteredResults = filteredResults.Where(p => p.Valid == true);   
 

            // Determine the total number of FILTERED products being paged through (needed to compute PageCount)
            model.TotalRecordCount = filteredResults.Count();

            // Get the current page of sorted, filtered products
            model.Tbl21Classes = filteredResults
                                        .OrderBy(model.SortExpression)
                                        .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                                        .Take(model.PageSize);

            return View(model);

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

  
        //
        // GET: /Tbl21Classes/Details/5     
        public ActionResult Details(int id)    {  
             var tbl21Class = _tbl21ClassesRepository.Get(id);   
 
             return tbl21Class == null ? View("NotFound") : View("Details", tbl21Class);
        }      
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
 
          <!-- _Form.cshtml  Skriptdatum: 15.03.2012  18:32    -->     
  
        //
        // GET: /Tbl21Classes/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int superclassId)              {   
             var tbl21Class = new Tbl21Class {SuperclassID = superclassId};           
             ViewData["SuperclassNameDDL"] = new SelectList(_tbl18SuperclassesRepository.FindAllSort().ToList(), "SuperclassID", "SuperclassName");    
     
             return View(tbl21Class);
        }     
  
        //       
        // POST: /Tbl21Classes/Create
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Create([Bind(Exclude = "ClassID")]FormCollection collection)    {               
  
            var tbl21Class = new Tbl21Class       {
                CountID =_tblCountersRepository.Counter(),                  
                Writer = User.Identity.Name,
                WriterDate = DateTime.Now,
                Updater = User.Identity.Name,
                UpdaterDate = DateTime.Now  
            };                           
            ViewData["SuperclassNameDDL"] = new SelectList(_tbl18SuperclassesRepository.FindAllSort().ToList(), "SuperclassID", "SuperclassName");         
      
            if (!TryUpdateModel(tbl21Class ))
                return View(tbl21Class);                                    

            //Add
            _tbl21ClassesRepository.Add(tbl21Class);   
            _tbl21ClassesRepository.Save();   

            TempData["message"] = tbl21Class.ClassName + " " + SharedRes.StringsRes.TempDataSavedMessage;   
 
   
            return View(tbl21Class);    //redisplay same view                     
        }    
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------  
 
        //
        // GET: /Tbl21Classes/Edit/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]           
        public ActionResult Edit(int id)    {   
            var tbl21Class = _tbl21ClassesRepository.Get(id);    
         ViewData["SuperclassNameDDL"] = new SelectList(_tbl18SuperclassesRepository.FindAllSort().ToList(), "SuperclassID", "SuperclassName");         
      
            return View(tbl21Class);
        }  
 
        //
        // POST: /Tbl21Classes/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Edit(int id,  FormCollection collection)  {  
            var tbl21Class = _tbl21ClassesRepository.Get(id);   
            ViewData["SuperclassNameDDL"] = new SelectList(_tbl18SuperclassesRepository.FindAllSort().ToList(), "SuperclassID", "SuperclassName");         
      
                            
            if(!TryUpdateModel(tbl21Class))
                return View(tbl21Class);    

            //Fill
            tbl21Class.Updater = User.Identity.Name;
            tbl21Class.UpdaterDate = DateTime.Now;

            _tbl21ClassesRepository.Save();      

           TempData["message"] = tbl21Class.ClassName + " " + SharedRes.StringsRes.TempDataSavedMessage;   

           return RedirectToAction("Index", tbl21Class);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
  
        //
        // GET: /Tbl21Classes/Delete/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id)  {  
            var tbl21Class = _tbl21ClassesRepository.Get(id); 
  
            return tbl21Class == null ? View("NotFound") : View(tbl21Class);                      
        }   
  
        //
        // POST: /Tbl21Classes/Delete/5
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id, string confirmButton)    {  
            var tbl21Class = _tbl21ClassesRepository.Get(id);   

            if (tbl21Class == null)
                return RedirectToAction("NotFound");
            
            _tbl21ClassesRepository.Delete(tbl21Class);   

            TempData["message"] = tbl21Class.ClassName + " " + SharedRes.StringsRes.TempDataDeletedMessage;   
            _tbl21ClassesRepository.Save();  
 
            return RedirectToAction("Index");
        }    
 
        public ActionResult NotFound()    {
            throw new NotImplementedException();
        }
   }
}   

