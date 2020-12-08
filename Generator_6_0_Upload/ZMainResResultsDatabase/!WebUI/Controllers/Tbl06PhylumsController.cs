using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Globalization;  
using Atis.Domain.Models;  
using Atis.Domain.Repositories;  
using Atis.Domain.ViewModels.Tbl06Phylums;         
    
// <!-- Controller Skriptdatum:  14.03.2012  12:32    -->  

namespace Atis.WebUI.Controllers     {  
    [HandleError]
    public class Tbl06PhylumsController : LanguageBaseController    { 
        readonly Tbl06PhylumsRepository _tbl06PhylumsRepository = new Tbl06PhylumsRepository();   
    
        readonly  Tbl03RegnumsRepository _tbl03RegnumsRepository = new Tbl03RegnumsRepository();   
    
         readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /Tbl06Phylum/  
        public ActionResult Index(string sortBy = "PhylumName", bool ascending = true, int page = 1, int pageSize = 12, int? regnumId = null, bool? valid = null, string phylumName = null, int? phylumId = null)    {                                    
       
            var model = new ListViewModel    {
                // Sorting-related properties
                SortBy = sortBy,
                SortAscending = ascending,

                // Paging-related properties
                CurrentPageIndex = page,
                PageSize = pageSize,    


                //
                RegnumID = regnumId,
                RegnumList = _tbl03RegnumsRepository.Tbl03Regnums.Select(a =>
                                new    {
                                    a.RegnumName,
                                    a.Subregnum,
                                    a.RegnumID
                                }
                            )
                            .OrderBy(a => a.RegnumName + a.Subregnum)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = string.Format("{0} {1}", a.RegnumName, a.Subregnum),
                                    Value = a.RegnumID.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };                                        
       
            var filteredResults = _tbl06PhylumsRepository.Tbl06Phylums.AsQueryable();   
 
            // Filter on PhylumName
            if (!string.IsNullOrEmpty(phylumName))
                filteredResults = filteredResults.Where(a => a.PhylumName.StartsWith(phylumName));      
 
            // Filter on PhylumID
            if (phylumId.HasValue)
                filteredResults = filteredResults.Where(a => a.PhylumID == phylumId);   

           // Filter on RegnumID 
            if (regnumId != null)
                filteredResults = filteredResults.Where(p => p.RegnumID == regnumId.Value);                                        
      
            //Filter on Valid
            if (valid != null && valid.Value)
                filteredResults = filteredResults.Where(p => p.Valid == true);   
 

            // Determine the total number of FILTERED products being paged through (needed to compute PageCount)
            model.TotalRecordCount = filteredResults.Count();

            // Get the current page of sorted, filtered products
            model.Tbl06Phylums = filteredResults
                                        .OrderBy(model.SortExpression)
                                        .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                                        .Take(model.PageSize);

            return View(model);

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

   
           
        private void ViewDataTbl06PhylumsGetValue()  {
            var query = from d in _tbl03RegnumsRepository.Tbl03Regnums
                         orderby d.RegnumName, d.Subregnum
                         select new  {
                             d.RegnumID,
                             DDLName = d.RegnumName + " " + d.Subregnum
                         };
            ViewData["RegnumNameDDL"] = new SelectList(query.ToList(), "RegnumID", "DDLName");
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  
  
        //
        // GET: /Tbl06Phylums/Details/5     
        public ActionResult Details(int id)    {  
             var tbl06Phylum = _tbl06PhylumsRepository.Get(id);   
 
             return tbl06Phylum == null ? View("NotFound") : View("Details", tbl06Phylum);
        }      
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
 
          <!-- _Form.cshtml  Skriptdatum: 14.03.2012  12:32    -->     
   
        //
        // GET: /Tbl06Phylums/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int regnumId)              {   
             var tbl06Phylum = new Tbl06Phylum {RegnumID = regnumId};         
             ViewDataTbl06PhylumsGetValue();  

             return View(tbl06Phylum);
        }     
  
        //       
        // POST: /Tbl06Phylums/Create
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Create([Bind(Exclude = "PhylumID")]FormCollection collection)    {               
  
            var tbl06Phylum = new Tbl06Phylum       {
                CountID =_tblCountersRepository.Counter(),                  
                Writer = User.Identity.Name,
                WriterDate = DateTime.Now,
                Updater = User.Identity.Name,
                UpdaterDate = DateTime.Now  
            };                           
    
             ViewDataTbl06PhylumsGetValue();  
  
            if (!TryUpdateModel(tbl06Phylum ))
                return View(tbl06Phylum);                                    

            //Add
            _tbl06PhylumsRepository.Add(tbl06Phylum);   
            _tbl06PhylumsRepository.Save();   

            TempData["message"] = tbl06Phylum.PhylumName + " " + SharedRes.StringsRes.TempDataSavedMessage;   
 
   
            return View(tbl06Phylum);    //redisplay same view                     
        }    
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------  
 
        //
        // GET: /Tbl06Phylums/Edit/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]           
        public ActionResult Edit(int id)    {   
            var tbl06Phylum = _tbl06PhylumsRepository.Get(id);    
    
             ViewDataTbl06PhylumsGetValue();  
  
            return View(tbl06Phylum);
        }  
 
        //
        // POST: /Tbl06Phylums/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Edit(int id,  FormCollection collection)  {  
            var tbl06Phylum = _tbl06PhylumsRepository.Get(id);   
    
             ViewDataTbl06PhylumsGetValue();  
  
                            
            if(!TryUpdateModel(tbl06Phylum))
                return View(tbl06Phylum);    

            //Fill
            tbl06Phylum.Updater = User.Identity.Name;
            tbl06Phylum.UpdaterDate = DateTime.Now;

            _tbl06PhylumsRepository.Save();      

           TempData["message"] = tbl06Phylum.PhylumName + " " + SharedRes.StringsRes.TempDataSavedMessage;   

           return RedirectToAction("Index", tbl06Phylum);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
  
        //
        // GET: /Tbl06Phylums/Delete/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id)  {  
            var tbl06Phylum = _tbl06PhylumsRepository.Get(id); 
  
            return tbl06Phylum == null ? View("NotFound") : View(tbl06Phylum);                      
        }   
  
        //
        // POST: /Tbl06Phylums/Delete/5
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id, string confirmButton)    {  
            var tbl06Phylum = _tbl06PhylumsRepository.Get(id);   

            if (tbl06Phylum == null)
                return RedirectToAction("NotFound");
            
            _tbl06PhylumsRepository.Delete(tbl06Phylum);   

            TempData["message"] = tbl06Phylum.PhylumName + " " + SharedRes.StringsRes.TempDataDeletedMessage;   
            _tbl06PhylumsRepository.Save();  
 
            return RedirectToAction("Index");
        }    
 
        public ActionResult NotFound()    {
            throw new NotImplementedException();
        }
   }
}   

