using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Globalization;  
using Atis.Domain.Models;  
using Atis.Domain.Repositories;  
using Atis.Domain.ViewModels.Tbl30Legios;         
    
// <!-- Controller Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.WebUI.Controllers     {  
    [HandleError]
    public class Tbl30LegiosController : LanguageBaseController    { 
        readonly Tbl30LegiosRepository _tbl30LegiosRepository = new Tbl30LegiosRepository();   
    
        readonly  Tbl27InfraclassesRepository _tbl27InfraclassesRepository = new Tbl27InfraclassesRepository();   
    
         readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /Tbl30Legio/  
        public ActionResult Index(string sortBy = "LegioName", bool ascending = true, int page = 1, int pageSize = 12, int? infraclassId = null, bool? valid = null, string legioName = null, int? legioId = null)    {                                    
       
            var model = new ListViewModel    {
                // Sorting-related properties
                SortBy = sortBy,
                SortAscending = ascending,

                // Paging-related properties
                CurrentPageIndex = page,
                PageSize = pageSize,    


               // 
                InfraclassID = infraclassId,
                InfraclassList = _tbl27InfraclassesRepository.Tbl27Infraclasses.Select(a =>
                                new     {
                                    a.InfraclassName,
                                    a.InfraclassID
                                }
                            )
                            .OrderBy(a => a.InfraclassName)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.InfraclassName,
                                    Value = a.InfraclassID.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };                                        
       
            var filteredResults = _tbl30LegiosRepository.Tbl30Legios.AsQueryable();   
 
            // Filter on LegioName
            if (!string.IsNullOrEmpty(legioName))
                filteredResults = filteredResults.Where(a => a.LegioName.StartsWith(legioName));      
 
            // Filter on LegioID
            if (legioId.HasValue)
                filteredResults = filteredResults.Where(a => a.LegioID == legioId);   

           // Filter on InfraclassID 
            if (infraclassId != null)
                filteredResults = filteredResults.Where(p => p.InfraclassID == infraclassId.Value);                                        
      
            //Filter on Valid
            if (valid != null && valid.Value)
                filteredResults = filteredResults.Where(p => p.Valid == true);   
 

            // Determine the total number of FILTERED products being paged through (needed to compute PageCount)
            model.TotalRecordCount = filteredResults.Count();

            // Get the current page of sorted, filtered products
            model.Tbl30Legios = filteredResults
                                        .OrderBy(model.SortExpression)
                                        .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                                        .Take(model.PageSize);

            return View(model);

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

  
        //
        // GET: /Tbl30Legios/Details/5     
        public ActionResult Details(int id)    {  
             var tbl30Legio = _tbl30LegiosRepository.Get(id);   
 
             return tbl30Legio == null ? View("NotFound") : View("Details", tbl30Legio);
        }      
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
 
          <!-- _Form.cshtml  Skriptdatum: 7.1.2012  10:32    -->     
  
        //
        // GET: /Tbl30Legios/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int infraclassId)              {   
             var tbl30Legio = new Tbl30Legio {InfraclassID = infraclassId};           
             ViewData["InfraclassNameDDL"] = new SelectList(_tbl27InfraclassesRepository.FindAllSort().ToList(), "InfraclassID", "InfraclassName");    
     
             return View(tbl30Legio);
        }     
  
        //       
        // POST: /Tbl30Legios/Create
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Create([Bind(Exclude = "LegioID")]FormCollection collection)    {               
  
            var tbl30Legio = new Tbl30Legio       {
                CountID =_tblCountersRepository.Counter(),                  
                Writer = User.Identity.Name,
                WriterDate = DateTime.Now,
                Updater = User.Identity.Name,
                UpdaterDate = DateTime.Now  
            };                           
            ViewData["InfraclassNameDDL"] = new SelectList(_tbl27InfraclassesRepository.FindAllSort().ToList(), "InfraclassID", "InfraclassName");         
      
            if (!TryUpdateModel(tbl30Legio ))
                return View(tbl30Legio);                                    

            //Add
            _tbl30LegiosRepository.Add(tbl30Legio);   
            _tbl30LegiosRepository.Save();   

            TempData["message"] = tbl30Legio.LegioName + " " + SharedRes.StringsRes.TempDataSavedMessage;   
 
   
            return View(tbl30Legio);    //redisplay same view                     
        }    
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------  
 
        //
        // GET: /Tbl30Legios/Edit/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]           
        public ActionResult Edit(int id)    {   
            var tbl30Legio = _tbl30LegiosRepository.Get(id);    
         ViewData["InfraclassNameDDL"] = new SelectList(_tbl27InfraclassesRepository.FindAllSort().ToList(), "InfraclassID", "InfraclassName");         
      
            return View(tbl30Legio);
        }  
 
        //
        // POST: /Tbl30Legios/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Edit(int id,  FormCollection collection)  {  
            var tbl30Legio = _tbl30LegiosRepository.Get(id);   
            ViewData["InfraclassNameDDL"] = new SelectList(_tbl27InfraclassesRepository.FindAllSort().ToList(), "InfraclassID", "InfraclassName");         
      
                            
            if(!TryUpdateModel(tbl30Legio))
                return View(tbl30Legio);    

            //Fill
            tbl30Legio.Updater = User.Identity.Name;
            tbl30Legio.UpdaterDate = DateTime.Now;

            _tbl30LegiosRepository.Save();      

           TempData["message"] = tbl30Legio.LegioName + " " + SharedRes.StringsRes.TempDataSavedMessage;   

           return RedirectToAction("Index", tbl30Legio);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
  
        //
        // GET: /Tbl30Legios/Delete/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id)  {  
            var tbl30Legio = _tbl30LegiosRepository.Get(id); 
  
            return tbl30Legio == null ? View("NotFound") : View(tbl30Legio);                      
        }   
  
        //
        // POST: /Tbl30Legios/Delete/5
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id, string confirmButton)    {  
            var tbl30Legio = _tbl30LegiosRepository.Get(id);   

            if (tbl30Legio == null)
                return RedirectToAction("NotFound");
            
            _tbl30LegiosRepository.Delete(tbl30Legio);   

            TempData["message"] = tbl30Legio.LegioName + " " + SharedRes.StringsRes.TempDataDeletedMessage;   
            _tbl30LegiosRepository.Save();  
 
            return RedirectToAction("Index");
        }    
 
        public ActionResult NotFound()    {
            throw new NotImplementedException();
        }
   }
}   

