using System;
using System.Collections.ObjectModel;
using System.ComponentModel;  

    
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.DatabaseHelper;
using log4net;
using Microsoft.EntityFrameworkCore;          
    
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.Command;
using Tyrrrz.Extensions;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.ClosedCaptions;
using YoutubeExplode.Models.MediaStreams;
using RelayCommand = Te.Atis.Ui.Desktop.Domain.RelayCommand; 
    
         //    ImagesViewModel Skriptdatum:  22.01.2019  10:32    

namespace ATIS.Ui.Views.Database.ListDetails
{     
    
    public class ImagesViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   
         
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(ImagesViewModel));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly CrudFunctions _extCrud = new CrudFunctions();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<Tbl81Image> _genImageMessageBoxes = new GenericMessageBoxes<Tbl81Image>();
        private readonly GenericMessageBoxes<Tbl69FiSpecies> _genFiSpeciesMessageBoxes = new GenericMessageBoxes<Tbl69FiSpecies>();
        private readonly GenericMessageBoxes<Tbl68Speciesgroup> _genNameMessageBoxes = new GenericMessageBoxes<Tbl68Speciesgroup>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private int _position;   
       

        //YouTube
        private readonly YoutubeClient _client;
        private bool _isBusy;
        private string _query;
        private Video _video;
  //      private readonly Channel _channel;
   //     private readonly MediaStreamInfoSet _mediaStreamInfos;
    //    private readonly IReadOnlyList<ClosedCaptionTrackInfo> _closedCaptionTrackInfos;
        private double _progress;        
        private bool _isProgressIndeterminate;   
         
        #endregion [Private Data Members]               
      
        #region [Constructor]

        public ImagesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          
      
                //Image;
                GetValueMimeType();
                RegisterCommands();

                //YouTube
                _client = new YoutubeClient();

                // Commands
                GetVideoCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(GetVideo,
                    () => !IsBusy && Query.IsNotBlank());
                DownloadMediaStreamCommand = new RelayCommand<MediaStreamInfo>(DownloadMediaStream,
                    _ => !IsBusy);
                DownloadClosedCaptionTrackCommand = new RelayCommand<ClosedCaptionTrackInfo>(
                    DownloadClosedCaptionTrack, _ => !IsBusy);

            }
        }   
        public bool IsInDesignMode { get; set; }

        #endregion "Constructor"                        
 

 //    Part 1    

         

        #region [Commands Image]

        private RelayCommand _getImagesByNameOrIdCommand;
        public ICommand GetImagesByNameOrIdCommand => _getImagesByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGetImagesByNameOrId(SearchImageName); });    
             
        private RelayCommand _addImageCommand;
        public ICommand AddImageCommand => _addImageCommand ??= new RelayCommand(delegate { ExecuteAddImage(null); });

        private RelayCommand _copyImageCommand;
        public ICommand CopyImageCommand => _copyImageCommand ??= new RelayCommand(delegate { ExecuteCopyImage(null); });      
             
        private RelayCommand _deleteImageCommand;
        public ICommand DeleteImageCommand => _deleteImageCommand ??= new RelayCommand(delegate { ExecuteDeleteImage(SearchImageName); });    
             
        private RelayCommand _saveImageCommand;
        public ICommand SaveImageCommand => _saveImageCommand ??= new RelayCommand(delegate { ExecuteSaveImage(SearchImageName); });    

        #endregion [Commands Image]       

ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void ExecuteAddImage(object o)
        {
            Tbl81ImagesList.Insert(0, new Tbl81Image   {   Info = CultRes.StringsRes.DatasetNew  }  );

            Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("fispecies");
            Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("plspecies");

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
     
        private void ExecuteCopyImage(object o)
        {
            if (_genImageMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl81Image)) return;

            Tbl81ImagesList = _extCrud.CopyImage(CurrentTbl81Image);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();
        }                         
     
        private void ExecuteDeleteImage(string searchName)
        {
            if (_genImageMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl81Image)) return;               
 
    
            //check if in NULL connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            NULLList = _extCrud.SearchForConnectedDatasetsWithImageIdInTableName(CurrentTbl81Image);     
     
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(NULLList.Count, "Name")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithImageIdInTableReference(CurrentTbl81Image);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + 
                                              CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extCrud.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithImageIdInTableComment(CurrentTbl81Image);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extCrud.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var image= _uow.Tbl81Images.GetById(CurrentTbl81Image.ImageId);
                if (image!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + 
                                          CurrentTbl81Image.ImageName)) return;

                    _extCrud.DeleteImage(image);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, 
                                         CurrentTbl81Image.ImageName);
                }
                else 
                        _allMessageBoxes.InfoMessageBox("Not To Delete", 
                                         CultRes.StringsRes.DeleteCan + " " + CurrentTbl81Image.ImageName + " " + 
                                         CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetImagesByNameOrId(searchName);

            ImagesView.MoveCurrentToFirst();
        }                
     
        private void ExecuteSaveImage(string searchName)
        {
            if (_genImageMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl81Image)) return;      
       
            //Combobox select FiSpeciesID  may be not 0
            if (CurrentTbl81Image.FiSpeciesId == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }     
       
        private void SaveImage(object o)
        {
        }
        private static byte[] LoadImageData(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fs);
            var imageBytes = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            return imageBytes;
         }
        #endregion "Public Commands"                   
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl69FiSpecies"                 
        

        private RelayCommand _saveFiSpeciesCommand;

        public ICommand SaveFiSpeciesCommand => 
                                      _saveFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteSaveFiSpecies(null); });        
           
        private void ExecuteSaveFiSpecies(string searchName)
        {
            if (_genFiSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            try
            {
                var fispecies = _uow.Tbl69FiSpeciesses.GetById(CurrentTbl69FiSpecies.FiSpeciesId);

                if (CurrentTbl69FiSpecies.FiSpeciesId == 0)
                    fispecies = _extCrud.FiSpeciesAdd(CurrentTbl69FiSpecies);
                else
                    fispecies = _extCrud.FiSpeciesUpdate(fispecies, CurrentTbl69FiSpecies);

                _position = ImagesView.CurrentPosition;   
       
                var cap = CurrentTbl69FiSpecies.FiSpeciesName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))        return;               
       
                try
                {
                    _extCrud.FiSpeciesSave(fispecies, CurrentTbl69FiSpecies);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave); 
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }            
      
                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl69FiSpecies.FiSpeciesId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl69FiSpecies.FiSpeciesName);
            }       
     
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGetImagesByNameOrId(searchName);
            ImagesView.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                  
       
        private void ExecuteSaveFiSpecies(string searchName)
        {
            if (_genFiSpeciesMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            try
            {
                var fispecies = _uow.Tbl69FiSpeciesses.GetById(CurrentTbl69FiSpecies.FiSpeciesId);

                if (CurrentTbl69FiSpecies.FiSpeciesId == 0)
                    fispecies = _extCrud.FiSpeciesAdd(CurrentTbl69FiSpecies);
                else
                    fispecies = _extCrud.FiSpeciesUpdate(fispecies, CurrentTbl69FiSpecies);

                _position = ImagesView.CurrentPosition;

                var cap = CurrentTbl69FiSpecies.FiSpeciesName;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))                return;

                try
                {
                    _extCrud.FiSpeciesSave(fispecies, CurrentTbl69FiSpecies);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave); 
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl69FiSpecies.FiSpeciesId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl69FiSpecies.FiSpeciesName;
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGetImagesByNameOrId(searchName);
            ImagesView.MoveCurrentToPosition(_position);
        }
        #endregion "Public Commands"                        
                                                          

 //    Part 3    

           
        #region "Public Commands Connect <== Tbl72PlSpecies"                 
       
        private RelayCommand _savePlSpeciesCommand;

        public ICommand SavePlSpeciesCommand => 
                            _savePlSpeciesCommand ??= new RelayCommand(delegate { ExecuteSavePlSpecies(null); });
       
       
        private void SavePlSpecies(object o)
        {           
 
        }
        #endregion "Public Commands"                  
                                                          



 //    Part 4    

                                                          


 //    Part 5    

                                                          
                      
 //    Part 6    

 
            

 //    Part 7    

 

 //    Part 8    

 
             
 //    Part 9    


     
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {           
        
            Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68Speciesgroups());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());

            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(
                _businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl81Image.FiSpeciesID));

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();     
     
        }

        #endregion "Public Method Connected Tables by DoubleClick"     
 


 //    Part 10    

     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubRefTabIndex;

        public  int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex; 
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; RaisePropertyChanged("");        
     
                if (_selectedMainTabIndex == 0)             
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl81Image.FiSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("");

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }         
     
                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl81Image != null)
                    {
                        NULLList = _extCrud.GetNULLCollectionFromImageIdOrderBy<Tbl68Speciesgroup>(CurrentTbl81Image.ImageId);

                        Tbl81ImagesAllList = _extCrud.GetCollectionAllOrderBy<Tbl81Image>("image");

                        NULLView = CollectionViewSource.GetDefaultView(NULLList);
                        NULLView.Refresh();
                    }
                    SelectedDetailTabIndex = 2;   
               }      
     
                if (_selectedMainTabIndex == 2)
                {
                        SelectedDetailTabIndex = 3;
                        SelectedMainSubRefTabIndex = 0;                  
                }           
     
                if (_selectedMainTabIndex == 3)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromImageIdOrderBy<Tbl93Comment>(CurrentTbl81Image.ImageId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedDetailTabIndex = 6;
                }        
     
            }
        }

        public  int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex; 
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value;    RaisePropertyChanged("");       
     
                if (_selectedDetailTabIndex == 0)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl81Image.FiSpeciesId);

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;  
               }     
     
                if (_selectedDetailTabIndex == 1)                
                {
                    SelectedMainTabIndex = 0;
                }    
     
                if (_selectedDetailTabIndex == 2)                
                {
                    if (CurrentTbl81Image != null)
                    {
                        NULLList = _extCrud.GetNULLCollectionFromImageIdOrderBy<Tbl68Speciesgroup>(CurrentTbl81Image.ImageId);

                        Tbl81ImagesAllList = _extCrud.GetCollectionAllOrderBy<Tbl81Image>("image");

                        NULLView = CollectionViewSource.GetDefaultView(NULLList);
                        NULLView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }    
     
                if (_selectedDetailTabIndex == 3)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromImageIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl81Image.ImageId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }        
     
                if (_selectedDetailTabIndex == 4)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromImageIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl81Image.ImageId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }        
     
                if (_selectedDetailTabIndex == 5)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromImageIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl81Image.ImageId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }       
     
                if (_selectedDetailTabIndex == 6)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromImageIdOrderBy<Tbl93Comment>(CurrentTbl81Image.ImageId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }       
     
                if (_selectedDetailTabIndex == 7)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl93CommentsList = _extCrud.GetCommentsCollectionFromImageIdOrderBy<Tbl93Comment>(CurrentTbl81Image.ImageId);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 4;
                }       
     
            }
        }

        public int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex;
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value;  RaisePropertyChanged("");     
     
                if (_selectedMainSubRefTabIndex == 0)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromImageIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl81Image.ImageId);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }        
     
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromImageIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl81Image.ImageId);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }      
     
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromImageIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl81Image.ImageId);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedDetailTabIndex = 5;
                    SelectedMainTabIndex = 2;
                }      
                     
            }
        }    
        #endregion "Public Commands to open Detail TabItems"          
 

 //    Part 11    

      
        #region "Public Properties Tbl81Image"

        private int _searchImageId = 0;
        public int SearchImageId
        {
            get => _searchImageId;
            set { _searchImageId = value; RaisePropertyChanged(""); }
        }

        public  ICollectionView ImagesView;
        private   Tbl81Image CurrentTbl81Image => ImagesView?.CurrentItem as Tbl81Image;

        private ObservableCollection<Tbl81Image> _tbl81ImagesList;
        public  ObservableCollection<Tbl81Image> Tbl81ImagesList
        {
            get => _tbl81ImagesList; 
            set {  _tbl81ImagesList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<Tbl81Image> _tbl81ImagesAllList;
        public  ObservableCollection<Tbl81Image> Tbl81ImagesAllList
        {
            get => _tbl81ImagesAllList; 
            set { _tbl81ImagesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl69FiSpecies"

        public  ICollectionView FiSpeciessesView;
        private Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;           

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciessesList; 
            set { _tbl69FiSpeciessesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList; 
            set { _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"   
  
       
        #region "Public Properties Tbl72PlSpecies"

        public  ICollectionView PlSpeciessesView;
        private  Tbl72PlSpecies CurrentTbl72PlSpecies => PlSpeciessesView?.CurrentItem as Tbl72PlSpecies;           

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList;
        public   ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get => _tbl72PlSpeciessesList; 
            set { _tbl72PlSpeciessesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public  ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList; 
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"   
         
        #region Video
        public bool IsBusy
        {
            get => _isBusy;
            private set
            {
                Set(ref _isBusy, value);
                GetVideoCommand.RaiseCanExecuteChanged();
                DownloadMediaStreamCommand.RaiseCanExecuteChanged();
            }
        }

        public string Query
        {
            get => _query;
            set
            {
                Set(ref _query, value);
                GetVideoCommand.RaiseCanExecuteChanged();
            }
        }

        public Video Video
        {
            get => _video;
            private set
            {
                Set(ref _video, value);
                RaisePropertyChanged(() => IsVideoAvailable);
            }
        }

        public bool IsVideoAvailable => Video != null;

        public double Progress
        {
            get => _progress;
            private set => Set(ref _progress, value);
        }

        public bool IsProgressIndeterminate
        {
            get => _isProgressIndeterminate;
            private set => Set(ref _isProgressIndeterminate, value);
        }


        // Commands
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand GetVideoCommand { get; }
        public RelayCommand<MediaStreamInfo> DownloadMediaStreamCommand { get; }
        public RelayCommand<ClosedCaptionTrackInfo> DownloadClosedCaptionTrackCommand { get; }

        private async void GetVideo()
        {
            IsBusy = true;
            IsProgressIndeterminate = true;

            // Reset data
            Video = null;

            // Parse URL if necessary
            if (!YoutubeClient.TryParseVideoId(Query, out string videoId))
                videoId = Query;

            // Perform the request
            Video = await _client.GetVideoAsync(videoId);

            IsBusy = false;
            IsProgressIndeterminate = false;

        }

        private  void DownloadMediaStream(MediaStreamInfo info)
        {
            // Create dialog
            var fileExt = info.Container.GetFileExtension();
            var defaultFileName = $"{Video.Title}.{fileExt}"
                .Replace(Path.GetInvalidFileNameChars(), '_');
            var sfd = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = fileExt,
                FileName = defaultFileName,
                Filter = $"{info.Container} Files|*.{fileExt}|All Files|*.*"
            };

            // Select file path
            if (sfd.ShowDialog() != true)
                return;

            var filePath = sfd.FileName;

            // Download to file
            IsBusy = true;
            Progress = 0;

            var progressHandler = new Progress<double>(p => Progress = p);
             _client.DownloadMediaStreamAsync(info, filePath, progressHandler);

            IsBusy = false;
            Progress = 0;
        }

        private async void DownloadClosedCaptionTrack(ClosedCaptionTrackInfo info)
        {
            // Create dialog
            var fileExt = $"{Video.Title}.{info.Language.Name}.srt"
                .Replace(Path.GetInvalidFileNameChars(), '_');
            var filter = "SRT Files|*.srt|All Files|*.*";
            var sfd = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "srt",
                FileName = fileExt,
                Filter = filter
            };

            // Select file path
            if (sfd.ShowDialog() != true)
                return;

            var filePath = sfd.FileName;

            // Download to file
            IsBusy = true;
            Progress = 0;

            var progressHandler = new Progress<double>(p => Progress = p);
            await _client.DownloadClosedCaptionTrackAsync(info, filePath, progressHandler);

            IsBusy = false;
            Progress = 0;
        } 
        #endregion 
       
        #region "Public Properties Tbl68Speciesgroup"

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList;
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties Tbl68Speciesgroup"

        #region "Public Properties Tbl66Genus"

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties Tbl66Genus" 

        #region Mimetype

        private void GetValueMimeType()
        {
            _mimeTypes = new List<MimeType>()
            {
                new MimeType {Name = "jpg"},
                new MimeType {Name = "png"},
                new MimeType {Name = "bmp"},
                new MimeType {Name = "tiff"},
                new MimeType {Name = "gif"},
                new MimeType {Name = "icon"},
                new MimeType {Name = "jpeg"},
                new MimeType {Name = "wmf"},
                new MimeType {Name = "wmv"},
                new MimeType {Name = "mpg"},
                new MimeType {Name = "mp4"},
                new MimeType {Name = "avi"},
                new MimeType {Name = "mov"},
                new MimeType {Name = "swf"},
                new MimeType {Name = "flv"}
            };

            _selectedMimeType = new MimeType();
        }

        private List<MimeType> _mimeTypes;

        public List<MimeType> MimeTypes
        {
            get => _mimeTypes;
            set { _mimeTypes = value; RaisePropertyChanged(""); }
        }

        private MimeType _selectedMimeType;
        public MimeType SelectedMimeType
        {
            get => _selectedMimeType;
            set { _selectedMimeType = value; RaisePropertyChanged(""); }
        }

        public class MimeType
        {
            public string Name { get; set; }
        }

        #endregion

        #region OpenfileDialog

        public static RelayCommand OpenCommand { get; set; }
        private string _selectedPath;

        public string SelectedPath
        {
            get => _selectedPath;
            set { _selectedPath = value; RaisePropertyChanged(""); }
        }


        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get => _imageSource;
            set { _imageSource = value; RaisePropertyChanged(""); }
        }

        public readonly string DefaultPath;

        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
        }

        private void ExecuteOpenFileDialog(object o)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select A File",
                InitialDirectory = DefaultPath,
                Filter = "All images|*.jpg;*.jpeg;*.jpe;*.bmp;*.gif;*.ico;*.png;*.tif;*.tiff;*.hpd;*.jxr;*.wdp|" +
                         "JPEG image|*.jpg;*.jpeg;*.jpe|Windows BMP image|*.bmp|GIF image|*.gif|Microsoft Windows icon|*.ico|" +
                         "PNG image|*.png|TIFF image|*.tif;*.tiff|JPEG XR|*.hpd;*.jxr;*.wdp",

                FilterIndex = 1
            };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
            ImageSource = new BitmapImage(new Uri(dialog.FileName));
        }

        #endregion 
 

   }
}   
