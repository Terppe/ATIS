using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Globalization;  
using Atis.Domain.Models;  
using Atis.Domain.Repositories;  
using Atis.Domain.ViewModels.aspnet_Users;         
    
// <!-- Controller Skriptdatum:  01.02.2012  10:32    -->  

namespace Atis.WebUI.Controllers     {  
    [HandleError]
    public class aspnet_UsersController : LanguageBaseController    { 
        readonly aspnet_UsersRepository _aspnet_usersRepository = new aspnet_UsersRepository();   
    
        readonly  aspnet_ApplicationsRepository _aspnet_applicationsRepository = new aspnet_ApplicationsRepository();   
    
         readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /aspnet_User/  
        public ActionResult Index(string sortBy = "UserName", bool ascending = true, int page = 1, int pageSize = 12, int? applicationId = null, bool? valid = null, string userName = null, int? userId = null)    {                                    
       
            var model = new ListViewModel    {
                // Sorting-related properties
                SortBy = sortBy,
                SortAscending = ascending,

                // Paging-related properties
                CurrentPageIndex = page,
                PageSize = pageSize,    


               // 
                ApplicationId = applicationId,
                aspnet_ApplicationList = _aspnet_applicationsRepository.aspnet_Applications.Select(a =>
                                new     {
                                    a.ApplicationName,
                                    a.ApplicationId
                                }
                            )
                            .OrderBy(a => a.ApplicationName)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.ApplicationName,
                                    Value = a.ApplicationId.ToString(CultureInfo.InvariantCulture),
                                }).ToList(),
                Valid = valid.HasValue && valid.Value  
            };                                        
       
            var filteredResults = _aspnet_usersRepository.aspnet_Users.AsQueryable();   
 
            // Filter on UserName
            if (!string.IsNullOrEmpty(userName))
                filteredResults = filteredResults.Where(a => a.UserName.StartsWith(userName));      
 
            // Filter on UserId
            if (userId.HasValue)
                filteredResults = filteredResults.Where(a => a.UserId == userId);   

           // Filter on ApplicationId 
            if (applicationId != null)
                filteredResults = filteredResults.Where(p => p.ApplicationId == applicationId.Value);                                        
      
            //Filter on Valid
            if (valid != null && valid.Value)
                filteredResults = filteredResults.Where(p => p.Valid == true);   
 

            // Determine the total number of FILTERED products being paged through (needed to compute PageCount)
            model.TotalRecordCount = filteredResults.Count();

            // Get the current page of sorted, filtered products
            model.aspnet_Users = filteredResults
                                        .OrderBy(model.SortExpression)
                                        .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                                        .Take(model.PageSize);

            return View(model);

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

  
        //
        // GET: /aspnet_Users/Details/5     
        public ActionResult Details(int id)    {  
             var aspnet_user = _aspnet_usersRepository.Get(id);   
 
             return aspnet_user == null ? View("NotFound") : View("Details", aspnet_user);
        }      
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
 
          <!-- _Form.cshtml  Skriptdatum: 01.02.2012  10:32    -->     
  
        //
        // GET: /aspnet_Users/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int applicationId)              {   
             var aspnet_user = new aspnet_User {ApplicationId = applicationId};           
             ViewData["ApplicationNameDDL"] = new SelectList(_aspnet_applicationsRepository.FindAllSort().ToList(), "ApplicationId", "ApplicationName");    
     
             return View(aspnet_user);
        }     
  
        //       
        // POST: /aspnet_Users/Create
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Create([Bind(Exclude = "UserId")]FormCollection collection)    {               
  
            var aspnet_user = new aspnet_User       {
                CountID =_tblCountersRepository.Counter(),                  
                Writer = User.Identity.Name,
                WriterDate = DateTime.Now,
                Updater = User.Identity.Name,
                UpdaterDate = DateTime.Now  
            };                           
            ViewData["ApplicationNameDDL"] = new SelectList(_aspnet_applicationsRepository.FindAllSort().ToList(), "ApplicationId", "ApplicationName");         
      
            if (!TryUpdateModel(aspnet_user ))
                return View(aspnet_user);                                    

            //Add
            _aspnet_usersRepository.Add(aspnet_user);   
            _aspnet_usersRepository.Save();   

            TempData["message"] = aspnet_user.UserName + " " + SharedRes.StringsRes.TempDataSavedMessage;   
 
   
            return View(aspnet_user);    //redisplay same view                     
        }    
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------  
 
        //
        // GET: /aspnet_Users/Edit/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]           
        public ActionResult Edit(int id)    {   
            var aspnet_user = _aspnet_usersRepository.Get(id);    
         ViewData["ApplicationNameDDL"] = new SelectList(_aspnet_applicationsRepository.FindAllSort().ToList(), "ApplicationId", "ApplicationName");         
      
            return View(aspnet_user);
        }  
 
        //
        // POST: /aspnet_Users/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
        public ActionResult Edit(int id,  FormCollection collection)  {  
            var aspnet_user = _aspnet_usersRepository.Get(id);   
            ViewData["ApplicationNameDDL"] = new SelectList(_aspnet_applicationsRepository.FindAllSort().ToList(), "ApplicationId", "ApplicationName");         
      
                            
            if(!TryUpdateModel(aspnet_user))
                return View(aspnet_user);    

            //Fill
            aspnet_user.Updater = User.Identity.Name;
            aspnet_user.UpdaterDate = DateTime.Now;

            _aspnet_usersRepository.Save();      

           TempData["message"] = aspnet_user.UserName + " " + SharedRes.StringsRes.TempDataSavedMessage;   

           return RedirectToAction("Index", aspnet_user);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
  
        //
        // GET: /aspnet_Users/Delete/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id)  {  
            var aspnet_user = _aspnet_usersRepository.Get(id); 
  
            return aspnet_user == null ? View("NotFound") : View(aspnet_user);                      
        }   
  
        //
        // POST: /aspnet_Users/Delete/5
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id, string confirmButton)    {  
            var aspnet_user = _aspnet_usersRepository.Get(id);   

            if (aspnet_user == null)
                return RedirectToAction("NotFound");
            
            _aspnet_usersRepository.Delete(aspnet_user);   

            TempData["message"] = aspnet_user.UserName + " " + SharedRes.StringsRes.TempDataDeletedMessage;   
            _aspnet_usersRepository.Save();  
 
            return RedirectToAction("Index");
        }    
 
        public ActionResult NotFound()    {
            throw new NotImplementedException();
        }
   }
}   

