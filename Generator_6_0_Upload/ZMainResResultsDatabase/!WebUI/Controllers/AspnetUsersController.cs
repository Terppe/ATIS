using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Globalization;  
using Atis.Domain.Models;  
using Atis.Domain.Repositories;  
using Atis.Domain.ViewModels.AspnetUsers;         
  
using System.Web.Security;     
    
// <!-- Controller Skriptdatum:  16.03.2012  10:32    -->  

namespace Atis.WebUI.Controllers     {  
    [HandleError]
    public class AspnetUsersController : LanguageBaseController    { 
        readonly AspnetUsersRepository _aspnetUsersRepository = new AspnetUsersRepository();   
    
        readonly  AspnetApplicationsRepository _aspnetApplicationsRepository = new AspnetApplicationsRepository();   
    
        //-----------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /AspnetUser/             
        public ActionResult Index(string sortBy = "UserName", bool ascending = true, int page = 1, int pageSize = 12, string applicationName = null, Guid? applicationId = null, string userName = null, Guid? userId = null)  {                                   
       
            var model = new ListViewModel    {
                // Sorting-related properties
                SortBy = sortBy,
                SortAscending = ascending,

                // Paging-related properties
                CurrentPageIndex = page,
                PageSize = pageSize,    


                //
                ApplicationId = applicationId,
                ApplicationList = _aspnetApplicationsRepository.AspnetApplications.Select(a =>
                                new    {
                                    a.ApplicationName,
                                    a.ApplicationId
                                }
                            )
                            .OrderBy(a => a.ApplicationName)
                            .ToList()
                            .Select(a =>
                                new SelectListItem    {
                                    Text = a.ApplicationName,
                                    Value = a.ApplicationId.ToString(CultureInfo.InvariantCulture.ToString()),
                                }).ToList()
            };                                        
       
            var filteredResults = _aspnetUsersRepository.AspnetUsers.AsQueryable();   
 
            // Filter on UserName
            if (!string.IsNullOrEmpty(userName))
                filteredResults = filteredResults.Where(a => a.UserName.StartsWith(userName));      
 
            // Filter on UserId
            if (userId.HasValue)
                filteredResults = filteredResults.Where(a => a.UserId == userId);   

           // Filter on ApplicationId 
            if (applicationId != null)
                filteredResults = filteredResults.Where(p => p.ApplicationId == applicationId.Value);                                        
     

            // Determine the total number of FILTERED products being paged through (needed to compute PageCount)
            model.TotalRecordCount = filteredResults.Count();

            // Get the current page of sorted, filtered products
            model.AspnetUsers = filteredResults
                                        .OrderBy(model.SortExpression)
                                        .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                                        .Take(model.PageSize);

            return View(model);

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

      
        //
        // GET: /AspnetUsers/Details/5     
        public ActionResult Details(Guid id)    { 
             var aspnetUser = _aspnetUsersRepository.Get(id);   
 
             return aspnetUser == null ? View("NotFound") : View("Details", aspnetUser);
        }      
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
   
        //
        // Get:/AspnetUsers/Role/Edit
        [Authorize(Roles = "Administrator")]
        public ActionResult Role(Guid id )    {
            var aspnetUser = _aspnetUsersRepository.Get(id);

            return View(aspnetUser);
        }

        //
        // Post:/AspnetUsers/Role/Edit
        [HttpPost, Authorize(Roles = "Administrator")]
        public ActionResult Role(Guid id, FormCollection fc)    {
            var aspnetUser = _aspnetUsersRepository.Get(id);
            //Change Role for Administrator, Developer, Zoologist, Biologist, User 

            var userName = aspnetUser.UserName;

            if (Roles.IsUserInRole(userName, "Administrator"))
                Roles.RemoveUserFromRole(userName, "Administrator");
            if (Roles.IsUserInRole(userName, "Developer"))
                Roles.RemoveUserFromRole(userName, "Developer");
            if (Roles.IsUserInRole(userName, "Zoologist"))
                Roles.RemoveUserFromRole(userName, "Zoologist");
            if (Roles.IsUserInRole(userName, "Biologist"))
                Roles.RemoveUserFromRole(userName, "Biologist");
            if (Roles.IsUserInRole(userName, "User"))
                Roles.RemoveUserFromRole(userName, "User");

            var cb1 = fc["Administrator"];
            var cb2 = fc["Developer"];
            var cb3 = fc["Zoologist"];
            var cb4 = fc["Biologist"];
            var cb5 = fc["User"];

            if (cb1 == "true,false")   {
                Roles.AddUserToRole(userName, "Administrator");
            }
            if (cb2 == "true,false")    {
                Roles.AddUserToRole(userName, "Developer");
            }
            if (cb3 == "true,false")    {
                Roles.AddUserToRole(userName, "Zoologist");
            }
            if (cb4 == "true,false")   {
                Roles.AddUserToRole(userName, "Biologist");
            }
            if (cb5 == "true,false")   {
                Roles.AddUserToRole(userName, "User");
            }
            return RedirectToAction("Index");
        }
        //-------------------------------------------------------------------------    
   
        //
        // GET: /AspnetUsers/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()              {   
             var aspnetUser = new AspnetUser();   
                 ViewData["ApplicationNameDDL"] = new SelectList(_aspnetApplicationsRepository.FindAllSort().ToList(), "ApplicationId", "ApplicationName");    
             return View(aspnetUser);
        }     
  
        //       
        // POST: /AspnetUsers/Create
        [HttpPost, Authorize(Roles = "Administrator")]   
        public ActionResult Create([Bind(Exclude = "UserId")]FormCollection collection)    {               
   
            var aspnetUser = new AspnetUser();   
            ViewData["ApplicationNameDDL"] = new SelectList(_aspnetApplicationsRepository.FindAllSort().ToList(), "ApplicationId", "ApplicationName");         
      
            if (!TryUpdateModel(aspnetUser ))
                return View(aspnetUser);                                    

            //Add
            _aspnetUsersRepository.Add(aspnetUser);   
            _aspnetUsersRepository.Save();   

            TempData["message"] = aspnetUser.UserName + " " + SharedRes.StringsRes.TempDataSavedMessage;   
 
   
            return View(aspnetUser);    //redisplay same view                     
        }    
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------  
   
        //
        // GET: /AspnetUsers/Edit/5
        [Authorize(Roles = "Administrator")]             
        public ActionResult Edit(Guid id)    {   
            var aspnetUser = _aspnetUsersRepository.Get(id);    
         ViewData["ApplicationNameDDL"] = new SelectList(_aspnetApplicationsRepository.FindAllSort().ToList(), "ApplicationId", "ApplicationName");         
      
            return View(aspnetUser);
        }  
  
        //
        // POST: /AspnetUsers/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator")]    
         public ActionResult Edit(Guid id,  FormCollection collection)   {  
            var aspnetUser = _aspnetUsersRepository.Get(id);       
            ViewData["ApplicationNameDDL"] = new SelectList(_aspnetApplicationsRepository.FindAllSort().ToList(), "ApplicationId", "ApplicationName");         
      
                            
            if(!TryUpdateModel(aspnetUser))
                return View(aspnetUser);    
       
            //Save
            _aspnetUsersRepository.Save();      

           TempData["message"] = aspnetUser.UserName + " " + SharedRes.StringsRes.TempDataSavedMessage;   

           return RedirectToAction("Index", aspnetUser);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
  
        //
        // GET: /AspnetUsers/Delete/5
        [Authorize(Roles = "Administrator")]    
        public ActionResult Delete(Guid id)   {     
            var aspnetUser = _aspnetUsersRepository.Get(id); 
  
            return aspnetUser == null ? View("NotFound") : View(aspnetUser);                      
        }   
 
        //
        // POST: /AspnetUsers/Delete/5
        [HttpPost, Authorize(Roles = "Administrator")]     
        public ActionResult Delete(Guid id, string confirmButton)    {       
            var aspnetUser = _aspnetUsersRepository.Get(id);   

            if (aspnetUser == null)
                return RedirectToAction("NotFound");
            
            _aspnetUsersRepository.Delete(aspnetUser);    

            TempData["message"] = aspnetUser.UserName + " " + SharedRes.StringsRes.TempDataDeletedMessage;   
            _aspnetUsersRepository.Save();  
 
            return RedirectToAction("Index");
        }    
 
        public ActionResult NotFound()    {
            throw new NotImplementedException();
        }
   }
}   

