using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Globalization;  
using Atis.Domain.Models;  
using Atis.Domain.Repositories;  
using Atis.Domain.ViewModels.Tbl81Images;         
  
using System.Web.Helpers;      
using System.Web;     
    
// <!-- Controller Skriptdatum:  28.12.2011  10:32    -->  

namespace Atis.WebUI.Controllers     {  
    [HandleError]
    public class Tbl81ImagesController : LanguageBaseController    { 
        readonly Tbl81ImagesRepository _tbl81ImagesRepository = new Tbl81ImagesRepository();   
    
        readonly  Tbl69FiSpeciessesRepository _tbl69FiSpeciessesRepository = new Tbl69FiSpeciessesRepository();   
    
        readonly  Tbl72PlSpeciessesRepository _tbl72PlSpeciessesRepository = new Tbl72PlSpeciessesRepository();  
    
         readonly TblCountersRepository _tblCountersRepository = new TblCountersRepository();   
        //---------------------------------------------------------------------------------------------------------------------------------------------------------

        //
        // GET: /Tbl81Image/  
        public ActionResult Index(string sortBy = "ImageID", bool ascending = true, int page = 1, int pageSize = 5, int? fispeciesId = null, int? plspeciesId = null, bool? valid = null, string imageName = null, int? imageId = null)  {                                  
       
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
                                new     {
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
                                new SelectListItem     {
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
       
            var filteredResults = _tbl81ImagesRepository.Tbl81Images.AsQueryable();   
 
            // Filter on ImageID
            if (imageId.HasValue)
                filteredResults = filteredResults.Where(a => a.ImageID == imageId);   

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
            model.Tbl81Images = filteredResults
                                        .OrderBy(model.SortExpression)
                                        .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                                        .Take(model.PageSize);

            return View(model);

        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

  
        //
        //Image or Video from Filestream to show
        public FileContentResult GetFilestream(int id)
        {
            var imageVideo = _tbl81ImagesRepository.Tbl81Images.First(x => x.ImageID == id);
            if (imageVideo == null) throw new ArgumentNullException(SharedRes.StringsRes.ErrorNullException);
            //imageVideo darf nicht NULL sein passiert wenn Bild zu groß bei upload oder bei video
            return File(imageVideo.Filestream, imageVideo.ImageMimeType);  
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void ViewDataMimeTypesGetValue(Tbl81Image tbl81Image)  {
            var mimeTypes = new[]  {
                                    "jpg",
                                    "JPG",
                                    "png",
                                    "bmp",
                                    "tiff",
                                    "gif",
                                    "icon",
                                    "jpeg",
                                    "wmf",
                                    "wmv",
                                    "mpg",
                                    "mp4",
                                    "avi",
                                    "mov",
                                    "swf",
                                    "flv"
                                };

            ViewData["mimeTypes"] = new SelectList(mimeTypes, tbl81Image.ImageMimeType);
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
        // GET: /Tbl81Images/Details/5     
        public ActionResult Details(int id)    {  
             var tbl81Image = _tbl81ImagesRepository.Get(id);   
 
             return tbl81Image == null ? View("NotFound") : View("Details", tbl81Image);
        }      
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
 
          <!-- _Form.cshtml  Skriptdatum: 28.12.2011  10:32    -->     
  
        //
        // GET: /Tbl81Images/Create
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]
        public ActionResult Create(int fispeciesId, int plspeciesId)              {   
             var tbl81Image = new Tbl81Image    {
                   FiSpeciesID = fispeciesId,         
                   PlSpeciesID = plspeciesId
             };         
             ViewDataSpeciesGetValue();  
             ViewDataMimeTypesGetValue(tbl81Image);  

             return View(tbl81Image);
        }     
  
        //       
        // POST: /Tbl81Images/Create
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]     
        public ActionResult Create([Bind(Exclude = "ImageID")]HttpPostedFileBase image)    {       
 
            var tbl81Image = new Tbl81Image       {
                CountID =_tblCountersRepository.Counter(),     
                FilestreamID = Guid.NewGuid(),                                  
                Writer = User.Identity.Name,
                WriterDate = DateTime.Now,
                Updater = User.Identity.Name,
                UpdaterDate = DateTime.Now  
            };                            
  
            var imageVideoToDatabase = new WebImage(image.InputStream);  

             ViewDataSpeciesGetValue();  
             ViewDataMimeTypesGetValue(tbl81Image);  
  
            if (!TryUpdateModel(tbl81Image ))
                return View(tbl81Image);                                    
    
            //Fill
            tbl81Image.Filestream = imageVideoToDatabase.GetBytes();  //Image    

            _tbl81ImagesRepository.Add(tbl81Image);   
            _tbl81ImagesRepository.Save();               
    
            if (tbl81Image.PlSpeciesID == 2)  
               TempData["message"] = tbl81Image.ImageID + " / " + tbl81Image.Tbl69FiSpeciesses.Tbl66Genusses.GenusName + " " + tbl81Image.Tbl69FiSpeciesses.FiSpeciesName + " " + tbl81Image.Tbl69FiSpeciesses.Subspecies + " " + tbl81Image.Tbl69FiSpeciesses.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    
            else                  
               TempData["message"] = tbl81Image.ImageID + " / " + tbl81Image.Tbl72PlSpeciesses.Tbl66Genusses.GenusName + " " + tbl81Image.Tbl72PlSpeciesses.PlSpeciesName + " " + tbl81Image.Tbl72PlSpeciesses.Subspecies + " " + tbl81Image.Tbl72PlSpeciesses.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;       
    
   
            return View(tbl81Image);    //redisplay same view not possible Filestream new one expected                     
        }  
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------  
 
        //
        // GET: /Tbl81Images/Edit/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]           
        public ActionResult Edit(int id)    {   
            var tbl81Image = _tbl81ImagesRepository.Get(id);    
  
             ViewDataSpeciesGetValue();  
             ViewDataMimeTypesGetValue(tbl81Image);  
  
            return View(tbl81Image);
        }  
   
        //
        // POST: /Tbl81Images/Edit/5  
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]   
         public ActionResult Edit(int id,  HttpPostedFileBase image)   {  
            var tbl81Image = _tbl81ImagesRepository.Get(id);      
  
            ViewDataSpeciesGetValue();  
            ViewDataMimeTypesGetValue(tbl81Image);  
     
            if (image != null)  {
                var imageVideoToDatabase = new WebImage(image.InputStream);

                if(!TryUpdateModel(tbl81Image))
                    return View(tbl81Image);

                //Fill   
                if (tbl81Image != null)     {  
                    tbl81Image.Filestream = imageVideoToDatabase.GetBytes();  //Image   

                    tbl81Image.Updater = User.Identity.Name;
                    tbl81Image.UpdaterDate = DateTime.Now;
                }
                _tbl81ImagesRepository.Save();                  
            }
            else  {
            if(!TryUpdateModel(tbl81Image))
                return View(tbl81Image);

                //Fill   
                tbl81Image.Updater = User.Identity.Name;
                tbl81Image.UpdaterDate = DateTime.Now;

                _tbl81ImagesRepository.Save();   
            }      
    
           if (tbl81Image != null && tbl81Image.PlSpeciesID == 2)  
              TempData["message"] = tbl81Image.ImageID + " / " + tbl81Image.Tbl69FiSpeciesses.Tbl66Genusses.GenusName + " " + tbl81Image.Tbl69FiSpeciesses.FiSpeciesName + " " + tbl81Image.Tbl69FiSpeciesses.Subspecies + " " + tbl81Image.Tbl69FiSpeciesses.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;    
            else                  
              TempData["message"] = tbl81Image.ImageID + " / " + tbl81Image.Tbl72PlSpeciesses.Tbl66Genusses.GenusName + " " + tbl81Image.Tbl72PlSpeciesses.PlSpeciesName + " " + tbl81Image.Tbl72PlSpeciesses.Subspecies + " " + tbl81Image.Tbl72PlSpeciesses.Divers + " " + SharedRes.StringsRes.TempDataSavedMessage;       

           return RedirectToAction("Index", tbl81Image);         
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
  
        //
        // GET: /Tbl81Images/Delete/5
        [Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id)  {  
            var tbl81Image = _tbl81ImagesRepository.Get(id); 
  
            return tbl81Image == null ? View("NotFound") : View(tbl81Image);                      
        }   
  
        //
        // POST: /Tbl81Images/Delete/5
        [HttpPost, Authorize(Roles = "Administrator, Developer, Zoologist, Biologist")]    
        public ActionResult Delete(int id, string confirmButton)    {  
            var tbl81Image = _tbl81ImagesRepository.Get(id);   

            if (tbl81Image == null)
                return RedirectToAction("NotFound");
            
            _tbl81ImagesRepository.Delete(tbl81Image);   
    
           TempData["message"] = tbl81Image.ImageID + " " + SharedRes.StringsRes.TempDataDeletedMessage;    
            _tbl81ImagesRepository.Save();  
 
            return RedirectToAction("Index");
        }    
 
        public ActionResult NotFound()    {
            throw new NotImplementedException();
        }
   }
}   

