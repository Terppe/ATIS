using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Globalization;  
using Atis.Domain.Models;  
using Atis.Domain.Repositories;  
using Atis.Domain.ViewModels.Tbl78Names;         
    
// <!-- Controller Skriptdatum:  28.12.2011  10:32    -->  

namespace Atis.WebUI.Controllers     {  
    [HandleError]
    public class Tbl78NamesController : LanguageBaseController    { 
        readonly Tbl78NamesRepository _tbl78NamesRepository = new Tbl78NamesRepository();   
    
        readonly  Tbl69FiSpeciessesRepository _tbl69FiSpeciessesRepository = new Tbl69FiSpeciessesRepository();   
    
        readonly  Tbl72PlSpeciessesRepository _tbl72PlSpeciessesRepository = new Tbl72PlSpeciessesRepository();  
    
         readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /Tbl78Name/  
        public ActionResult Index(string sortBy = "NameID", bool ascending = true, int page = 1, int pageSize = 12, int? fispeciesId = null, int? plspeciesId = null, bool? valid = null, string nameName = null, int? nameId = null)  {                                    
       
            var model = new ListViewModel    {
                // Sorting-related properties
                SortBy = sortBy,
                SortAscending = ascending,

                // Paging-related properties
                CurrentPageIndex = page,
                PageSize = pageSize,    


               //
                FiSpeciesID = fispeciesId,
                FiSpeciesList = _tbl69FiSpeciessesRepository.Tbl69FiSpeciesses.Select(a =>
                                new    {
                                    a.Tbl66Genusses.GenusName,
                                    a.FiSpeciesName,
                                    a.Subspecies,
                                    a.Divers,
                                    a.FiSpeciesID
                                }
                            )
                            .OrderBy(a => a.GenusName + a.FiSpeciesName + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = string.Format("{0} {1} {2} {3}", a.GenusName, a.FiSpeciesName, a.Subspecies, a.Divers),
                                    Value = a.FiSpeciesID.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                PlSpeciesID = plspeciesId,
                PlSpeciesList = _tbl72PlSpeciessesRepository.Tbl72PlSpeciesses.Select(a =>
                                new     {
                                    a.Tbl66Genusses.GenusName,
                                    a.PlSpeciesName,
                                    a.Subspecies,
                                    a.Divers,
                                    a.PlSpeciesID
                                }
                            )
                            .OrderBy(a => a.GenusName + a.PlSpeciesName + a.Subspecies + a.Divers)                                     
                            .ToList()
                            .Select(a =>
                                new SelectListItem     {
                                    Text = string.Format("{0} {1} {2} {3}", a.GenusName, a.PlSpeciesName, a.Subspecies, a.Divers),
                                    Value = a.PlSpeciesID.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };                                                                                    
       
            var filteredResults = _tbl78NamesRepository.Tbl78Names.AsQueryable();   
 
            // Filter on NameName
            if (!string.IsNullOrEmpty(nameName))
                filteredResults = filteredResults.Where(a => a.NameName.StartsWith(nameName));      
 
            // Filter on NameID
            if (nameId.HasValue)
                filteredResults = filteredResults.Where(a => a.NameID == nameId);   

           // Filter on FiSpeciesID 
            if (fispeciesId != null)
                filteredResults = filteredResults.Where(p => p.FiSpeciesID == fispeciesId.Value);                                        
    
            // Filter on PlSpeciesID 
            if (plspeciesId != null)
                filteredResults = filteredResults.Where(p => p.PlSpeciesID == plspeciesId.Value);                                       
      
            //Filter on Valid
            if (valid != null && valid.Value)
                filteredResults = filteredResults.Where(p => p.Valid == true);   
 

            // Determine the total number of FILTERED products being paged through (needed to compute PageCount)
            model.TotalRecordCount = filteredResults.Count();

            // Get the current page of sorted, filtered products
            model.Tbl78Names = filteredResults
                                        .OrderBy(model.SortExpression)
                                        .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                                        .Take(model.PageSize);

            return View(model);

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

  
        
        private void ViewDataLanguagesGetValue(Tbl78Name tbl78Name)  {
            var languages = new[]  {
                                    "ENG",
                                    "GER",
                                    "FRA",
                                    "POR"
                                };

            ViewData["languages"] = new SelectList(languages, tbl78Name.Language);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  
    
        private void ViewDataSpeciesGetValue()  {

            var query1 = from d in _tbl69FiSpeciessesRepository.Tbl69FiSpeciesses
                         orderby d.Tbl66Genusses.GenusName, d.FiSpeciesName, d.Subspecies, d.Divers
                         select new  {
                             d.FiSpeciesID,
                             DDLFiName = d.Tbl66Genusses.GenusName + " " + d.FiSpeciesName + " " + d.Subspecies + " " + d.Divers
                         };

            var query2 = from d in _tbl72PlSpeciessesRepository.Tbl72PlSpeciesses
                         orderby d.Tbl66Genusses.GenusName, d.PlSpeciesName, d.Subspecies, d.Divers
                         select new  {
                             d.PlSpeciesID,
                             DDLPlName = d.Tbl66Genusses.GenusName + " " + d.PlSpeciesName + " " + d.Subspecies + " " + d.Divers
                         };
            ViewData["FiSpeciesNameDDL"] = new SelectList(query1.ToList(), "FiSpeciesID", "DDLFiName");
            ViewData["PlSpeciesNameDDL"] = new SelectList(query2.ToList(), "PlSpeciesID", "DDLPlName");
        }       
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  
  
        //
        // GET: /Tbl78Names/Details/5     
        public ActionResult Details(int id)    {  
             var tbl78Name = _tbl78NamesRepository.Get(id);   
 
             return tbl78Name == null ? View("NotFound") : View("Details", tbl78Name);
        }      
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
 
          <!-- _Form.cshtml  Skriptdatum: 28.12.2011  10:32    -->     
 
        //
        // GET: /Tbl78Names/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int fispeciesId, int plspeciesId)              {   
             var tbl78Name = new Tbl78Name    {
                   FiSpeciesID = fispeciesId,         
                   PlSpeciesID = plspeciesId
             };         
             ViewDataSpeciesGetValue();     
             ViewDataLanguagesGetValue(tbl78Name);  

             return View(tbl78Name);
        }     
  
        //       
        // POST: /Tbl78Names/Create
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Create([Bind(Exclude = "NameID")]FormCollection collection)    {               
  
            var tbl78Name = new Tbl78Name       {
                CountID =_tblCountersRepository.Counter(),                  
                Writer = User.Identity.Name,
                WriterDate = DateTime.Now,
                Updater = User.Identity.Name,
                UpdaterDate = DateTime.Now  
            };                           
    
             ViewDataSpeciesGetValue();  
             ViewDataLanguagesGetValue(tbl78Name);  
  
            if (!TryUpdateModel(tbl78Name ))
                return View(tbl78Name);                                    

            //Add
            _tbl78NamesRepository.Add(tbl78Name);   
            _tbl78NamesRepository.Save();   

            TempData["message"] = tbl78Name.NameName + " " + SharedRes.StringsRes.TempDataSavedMessage;   
 
   
            return View(tbl78Name);    //redisplay same view                     
        }    
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------  
 
        //
        // GET: /Tbl78Names/Edit/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]           
        public ActionResult Edit(int id)    {   
            var tbl78Name = _tbl78NamesRepository.Get(id);    
    
             ViewDataSpeciesGetValue();  
             ViewDataLanguagesGetValue(tbl78Name);  
  
            return View(tbl78Name);
        }  
 
        //
        // POST: /Tbl78Names/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Edit(int id,  FormCollection collection)  {  
            var tbl78Name = _tbl78NamesRepository.Get(id);   
    
             ViewDataSpeciesGetValue();  
             ViewDataLanguagesGetValue(tbl78Name);  
  
                            
            if(!TryUpdateModel(tbl78Name))
                return View(tbl78Name);    

            //Fill
            tbl78Name.Updater = User.Identity.Name;
            tbl78Name.UpdaterDate = DateTime.Now;

            _tbl78NamesRepository.Save();      

           TempData["message"] = tbl78Name.NameName + " " + SharedRes.StringsRes.TempDataSavedMessage;   

           return RedirectToAction("Index", tbl78Name);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
  
        //
        // GET: /Tbl78Names/Delete/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id)  {  
            var tbl78Name = _tbl78NamesRepository.Get(id); 
  
            return tbl78Name == null ? View("NotFound") : View(tbl78Name);                      
        }   
  
        //
        // POST: /Tbl78Names/Delete/5
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id, string confirmButton)    {  
            var tbl78Name = _tbl78NamesRepository.Get(id);   

            if (tbl78Name == null)
                return RedirectToAction("NotFound");
            
            _tbl78NamesRepository.Delete(tbl78Name);   

            TempData["message"] = tbl78Name.NameName + " " + SharedRes.StringsRes.TempDataDeletedMessage;   
            _tbl78NamesRepository.Save();  
 
            return RedirectToAction("Index");
        }    
 
        public ActionResult NotFound()    {
            throw new NotImplementedException();
        }
   }
}   

