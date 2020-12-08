using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Globalization;  
using Atis.Domain.Models;  
using Atis.Domain.Repositories;  
using Atis.Domain.ViewModels.Tbl33Ordos;         
    
// <!-- Controller Skriptdatum:  7.1.2012  10:32    -->  

namespace Atis.WebUI.Controllers     {  
    [HandleError]
    public class Tbl33OrdosController : LanguageBaseController    { 
        readonly Tbl33OrdosRepository _tbl33OrdosRepository = new Tbl33OrdosRepository();   
    
        readonly  Tbl30LegiosRepository _tbl30LegiosRepository = new Tbl30LegiosRepository();   
    
         readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /Tbl33Ordo/  
        public ActionResult Index(string sortBy = "OrdoName", bool ascending = true, int page = 1, int pageSize = 12, int? legioId = null, bool? valid = null, string ordoName = null, int? ordoId = null)    {                                    
       
            var model = new ListViewModel    {
                // Sorting-related properties
                SortBy = sortBy,
                SortAscending = ascending,

                // Paging-related properties
                CurrentPageIndex = page,
                PageSize = pageSize,    


               // 
                LegioID = legioId,
                LegioList = _tbl30LegiosRepository.Tbl30Legios.Select(a =>
                                new     {
                                    a.LegioName,
                                    a.LegioID
                                }
                            )
                            .OrderBy(a => a.LegioName)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.LegioName,
                                    Value = a.LegioID.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };                                        
       
            var filteredResults = _tbl33OrdosRepository.Tbl33Ordos.AsQueryable();   
 
            // Filter on OrdoName
            if (!string.IsNullOrEmpty(ordoName))
                filteredResults = filteredResults.Where(a => a.OrdoName.StartsWith(ordoName));      
 
            // Filter on OrdoID
            if (ordoId.HasValue)
                filteredResults = filteredResults.Where(a => a.OrdoID == ordoId);   

           // Filter on LegioID 
            if (legioId != null)
                filteredResults = filteredResults.Where(p => p.LegioID == legioId.Value);                                        
      
            //Filter on Valid
            if (valid != null && valid.Value)
                filteredResults = filteredResults.Where(p => p.Valid == true);   
 

            // Determine the total number of FILTERED products being paged through (needed to compute PageCount)
            model.TotalRecordCount = filteredResults.Count();

            // Get the current page of sorted, filtered products
            model.Tbl33Ordos = filteredResults
                                        .OrderBy(model.SortExpression)
                                        .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                                        .Take(model.PageSize);

            return View(model);

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

  
        //
        // GET: /Tbl33Ordos/Details/5     
        public ActionResult Details(int id)    {  
             var tbl33Ordo = _tbl33OrdosRepository.Get(id);   
 
             return tbl33Ordo == null ? View("NotFound") : View("Details", tbl33Ordo);
        }      
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
 
          <!-- _Form.cshtml  Skriptdatum: 7.1.2012  10:32    -->     
  
        //
        // GET: /Tbl33Ordos/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int legioId)              {   
             var tbl33Ordo = new Tbl33Ordo {LegioID = legioId};           
             ViewData["LegioNameDDL"] = new SelectList(_tbl30LegiosRepository.FindAllSort().ToList(), "LegioID", "LegioName");    
     
             return View(tbl33Ordo);
        }     
  
        //       
        // POST: /Tbl33Ordos/Create
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Create([Bind(Exclude = "OrdoID")]FormCollection collection)    {               
  
            var tbl33Ordo = new Tbl33Ordo       {
                CountID =_tblCountersRepository.Counter(),                  
                Writer = User.Identity.Name,
                WriterDate = DateTime.Now,
                Updater = User.Identity.Name,
                UpdaterDate = DateTime.Now  
            };                           
            ViewData["LegioNameDDL"] = new SelectList(_tbl30LegiosRepository.FindAllSort().ToList(), "LegioID", "LegioName");         
      
            if (!TryUpdateModel(tbl33Ordo ))
                return View(tbl33Ordo);                                    

            //Add
            _tbl33OrdosRepository.Add(tbl33Ordo);   
            _tbl33OrdosRepository.Save();   

            TempData["message"] = tbl33Ordo.OrdoName + " " + SharedRes.StringsRes.TempDataSavedMessage;   
 
   
            return View(tbl33Ordo);    //redisplay same view                     
        }    
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------  
 
        //
        // GET: /Tbl33Ordos/Edit/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]           
        public ActionResult Edit(int id)    {   
            var tbl33Ordo = _tbl33OrdosRepository.Get(id);    
         ViewData["LegioNameDDL"] = new SelectList(_tbl30LegiosRepository.FindAllSort().ToList(), "LegioID", "LegioName");         
      
            return View(tbl33Ordo);
        }  
 
        //
        // POST: /Tbl33Ordos/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Edit(int id,  FormCollection collection)  {  
            var tbl33Ordo = _tbl33OrdosRepository.Get(id);   
            ViewData["LegioNameDDL"] = new SelectList(_tbl30LegiosRepository.FindAllSort().ToList(), "LegioID", "LegioName");         
      
                            
            if(!TryUpdateModel(tbl33Ordo))
                return View(tbl33Ordo);    

            //Fill
            tbl33Ordo.Updater = User.Identity.Name;
            tbl33Ordo.UpdaterDate = DateTime.Now;

            _tbl33OrdosRepository.Save();      

           TempData["message"] = tbl33Ordo.OrdoName + " " + SharedRes.StringsRes.TempDataSavedMessage;   

           return RedirectToAction("Index", tbl33Ordo);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
  
        //
        // GET: /Tbl33Ordos/Delete/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id)  {  
            var tbl33Ordo = _tbl33OrdosRepository.Get(id); 
  
            return tbl33Ordo == null ? View("NotFound") : View(tbl33Ordo);                      
        }   
  
        //
        // POST: /Tbl33Ordos/Delete/5
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id, string confirmButton)    {  
            var tbl33Ordo = _tbl33OrdosRepository.Get(id);   

            if (tbl33Ordo == null)
                return RedirectToAction("NotFound");
            
            _tbl33OrdosRepository.Delete(tbl33Ordo);   

            TempData["message"] = tbl33Ordo.OrdoName + " " + SharedRes.StringsRes.TempDataDeletedMessage;   
            _tbl33OrdosRepository.Save();  
 
            return RedirectToAction("Index");
        }    
 
        public ActionResult NotFound()    {
            throw new NotImplementedException();
        }
   }
}   

