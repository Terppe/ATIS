using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using log4net;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.BusinessLayer;
using Te.Atis.Ui.Desktop.Domain;
using Te.Atis.Ui.Desktop.Domain.Helper;
using Te.Atis.Ui.Desktop.MessageBox;    

    
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
    
         //    Tbl81ImagesViewModel Skriptdatum:  22.01.2019  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl81ImagesViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;
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
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl81ImagesViewModel()
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

                _entityException = new DbEntityException();
            }
        }   
        #endregion "Constructor"                        
 

 //    Part 1    

             
        #region "Public Commands Basic Tbl81Image"
        //-------------------------------------------------------------------------
        private RelayCommand _clearImageCommand;

        public ICommand ClearImageCommand => _clearImageCommand ??
                                                  (_clearImageCommand = new RelayCommand(delegate { ClearImage(null); }));         
    
        private RelayCommand _getImagesByIdCommand;       

        public  ICommand GetImagesByIdCommand => _getImagesByIdCommand ??
                                                           (_getImagesByIdCommand = new RelayCommand(delegate { GetImagesById(null); }));        
             
        private RelayCommand _addImageCommand;

        public ICommand AddImageCommand => _addImageCommand ??
                                                (_addImageCommand = new RelayCommand(delegate { AddImage(null); }));

        private RelayCommand _copyImageCommand;

        public ICommand CopyImageCommand => _copyImageCommand ??
                                                 (_copyImageCommand = new RelayCommand(delegate { CopyImage(null); }));      
             
        private RelayCommand _deleteImageCommand;

        public ICommand DeleteImageCommand => _deleteImageCommand ??
                                                   (_deleteImageCommand = new RelayCommand(delegate { DeleteImage(null); }));    
             
        private RelayCommand _saveImageCommand;

        public ICommand SaveImageCommand => _saveImageCommand ??
                                                 (_saveImageCommand = new RelayCommand(delegate { SaveImage(null); }));
        //-------------------------------------------------------------------------          
        
        private void ClearImage(object o)
        {
            SearchImageId = 0;

            SelectedMainTabIndex = 0;  //change tab
            SelectedDetailTabIndex = 0;
            SelectedDetailSubTabIndex = 0;

            Tbl81ImagesList?.Clear();
            Tbl69FiSpeciessesList?.Clear();
            Tbl72PlSpeciessesList?.Clear();
        }
        //----------------------------------------------------------------------                  
        
        private void GetImagesById(object o)
        {
            if (SearchImageId != 0)
            {
                Tbl81ImagesList?.Clear();

                Tbl69FiSpeciessesList?.Clear();
                Tbl72PlSpeciessesList?.Clear();

                 if(int.TryParse(SearchImageId.ToString(CultureInfo.InvariantCulture), out var id))

                    _businessLayer = new BusinessLayer.BusinessLayer();
                    Tbl69FiSpeciessesAllList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciesses());
                    Tbl72PlSpeciessesAllList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciesses());
                    Tbl81ImagesList = new ObservableCollection<Tbl81Image>(_businessLayer.ListTbl81ImagesByImageId(id));
    
                if (Tbl81ImagesList.Count == 0)
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Tables, CultRes.StringsRes.DatasetNot,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            else
            {
                WpfMessageBox.Show(CultRes.StringsRes.SearchNameOrId, CultRes.StringsRes.InputRequested,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void AddImage(object o)
        {
            if (Tbl81ImagesList == null)
                Tbl81ImagesList =  new ObservableCollection<Tbl81Image>( );

            Tbl81ImagesList.Insert(0, new Tbl81Image   {   Info = CultRes.StringsRes.DatasetNew  }  );

                    _businessLayer = new BusinessLayer.BusinessLayer();
                Tbl69FiSpeciessesAllList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciesses());
                Tbl72PlSpeciessesAllList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciesses());

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyImage(object o)
        {
            if (CurrentTbl81Image == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var image = _businessLayer.SingleListTbl81ImagesByImageId(CurrentTbl81Image.ImageID);

            Tbl81ImagesList.Insert(0, new Tbl81Image
            {
                 FiSpeciesID =  image. FiSpeciesID,
                 PlSpeciesID =  image. PlSpeciesID,
                Valid = image.Valid,
                ValidYear = image.ValidYear,
                ShotDate = image.ShotDate,
                Info = image.Info,
                ImageData = image.ImageData,
                ImageMimeType = image.ImageMimeType,
                FilestreamID = Guid.NewGuid(),
                Memo = image.Memo                   
            });

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
        
        private void DeleteImage(object o)
        {
            if (CurrentTbl81Image == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
                var image = _businessLayer.SingleListTbl81ImagesByImageId(CurrentTbl81Image.ImageID);
                if (image != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl81Image.ImageID.ToString(),
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    image.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveImage(image);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl81Image.ImageID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl81Image.ImageID.ToString() + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
            }

            Tbl81ImagesList = new ObservableCollection<Tbl81Image>(_businessLayer.ListTbl81ImagesByImageId(SearchImageId));

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();            
        }
        //-------------------------------------------------------------------------------------------------                    
       
        private void SaveImage(object o)
        {
            if (CurrentTbl81Image == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
                var image = _businessLayer.SingleListTbl81ImagesByImageId(CurrentTbl81Image.ImageID);
                if (CurrentTbl81Image.ImageID != 0)
                {
                    if (image != null) //update
                    {
                            image.FiSpeciesID = CurrentTbl81Image.FiSpeciesID;
                            image.PlSpeciesID = CurrentTbl81Image.PlSpeciesID;
                            image.Valid = CurrentTbl81Image.Valid;
                            image.ValidYear = CurrentTbl81Image.ValidYear;
                            image.ShotDate = CurrentTbl81Image.ShotDate;
                            image.Info = CurrentTbl81Image.Info;
                            image.Memo = CurrentTbl81Image.Memo;
                            image.ImageData = CurrentTbl81Image.ImageData;
                            image.ImageMimeType = CurrentTbl81Image.ImageMimeType;
                            if (SelectedPath != null) image.Filestream = LoadImageData(SelectedPath);
                            image.FilestreamID = Guid.NewGuid();
                            image.Updater = Environment.UserName;
                            image.UpdaterDate = DateTime.Now; 
                        image.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    if (CurrentTbl81Image.ImageMimeType != "")
                        if (SelectedPath != null)
                            image = new Tbl81Image   //add new
                            {
                                FiSpeciesID = CurrentTbl81Image.FiSpeciesID,
                                PlSpeciesID = CurrentTbl81Image.PlSpeciesID,
                                    CountID = RandomHelper.Randomnumber(),
                                    Valid = CurrentTbl81Image.Valid,
                                    ValidYear = CurrentTbl81Image.ValidYear,
                                    ShotDate = CurrentTbl81Image.ShotDate,
                                    Info = CurrentTbl81Image.Info,
                                    Memo = CurrentTbl81Image.Memo,
                                    ImageData = CurrentTbl81Image.ImageData, //empty
                                    ImageMimeType = CurrentTbl81Image.ImageMimeType,
                                    Filestream = LoadImageData(SelectedPath),
                                    FilestreamID = Guid.NewGuid(),
                                    Writer = Environment.UserName,
                                    WriterDate = DateTime.Now,
                                    Updater = Environment.UserName,
                                    UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //FiSpeciesID && PlSpeciesID  may be not 0
                    if (CurrentTbl81Image.FiSpeciesID == 0 && CurrentTbl81Image.PlSpeciesID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    //check if dataset with ImageId and FiSpeciesId and PlSpeciesId  already exist       
                    var dataset = _businessLayer.ListTbl81ImagesByImageIdAndFiSpeciesIdAndPlSpeciesId(CurrentTbl81Image.ImageID, CurrentTbl81Image.FiSpeciesID, CurrentTbl81Image.PlSpeciesID);

                    if (dataset.Count != 0 && CurrentTbl81Image.ImageID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl81Image.ImageID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl81Image.ImageID == 0 ||
                        dataset.Count != 0 && CurrentTbl81Image.ImageID != 0 ||
                        dataset.Count == 0 && CurrentTbl81Image.ImageID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl81Image.ImageID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateImage(image);
                                _position = ImagesView.CurrentPosition;
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl81Image.ImageID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl81Image.ImageID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
                 return;
            }

            if (CurrentTbl81Image.ImageID != 0)
                Tbl81ImagesList = new ObservableCollection<Tbl81Image>(_businessLayer.ListTbl81ImagesByImageId(SearchImageId));
            else
                Tbl81ImagesList =  new ObservableCollection<Tbl81Image>(_businessLayer.ListTbl81ImagesByImageId(CurrentTbl81Image.ImageID));

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.Refresh();
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
        //-------------------------------------------------------------------------

        private RelayCommand _saveFiSpeciesCommand;

        public ICommand SaveFiSpeciesCommand => _saveFiSpeciesCommand ??
                                                 (_saveFiSpeciesCommand = new RelayCommand(delegate { SaveFiSpecies(null); }));

        //-------------------------------------------------------------------------          
       
        private void SaveFiSpecies(object o)
        {
            if (CurrentTbl69FiSpecies == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var fispecies = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl69FiSpecies.FiSpeciesID);
                if (CurrentTbl69FiSpecies.FiSpeciesID != 0)
                {
                    if (fispecies != null) //update
                    {
                        fispecies.FiSpeciesName = CurrentTbl69FiSpecies.FiSpeciesName;
                            fispecies.Subspecies = CurrentTbl69FiSpecies.Subspecies;
                            fispecies.Divers = CurrentTbl69FiSpecies.Divers;
                            fispecies.GenusID = CurrentTbl69FiSpecies.GenusID;
                            fispecies.SpeciesgroupID = CurrentTbl69FiSpecies.SpeciesgroupID;
                            fispecies.Valid = CurrentTbl69FiSpecies.Valid;
                            fispecies.ValidYear = CurrentTbl69FiSpecies.ValidYear;
                            fispecies.TradeName = CurrentTbl69FiSpecies.TradeName;
                            fispecies.Author = CurrentTbl69FiSpecies.Author;
                            fispecies.AuthorYear = CurrentTbl69FiSpecies.AuthorYear;
                            fispecies.Importer = CurrentTbl69FiSpecies.Importer;
                            fispecies.ImportingYear = CurrentTbl69FiSpecies.ImportingYear;
                            fispecies.TypeSpecies = CurrentTbl69FiSpecies.TypeSpecies;
                            fispecies.LNumber = CurrentTbl69FiSpecies.LNumber;
                            fispecies.LOrigin = CurrentTbl69FiSpecies.LOrigin;
                            fispecies.LDAOrigin = CurrentTbl69FiSpecies.LDAOrigin;
                            fispecies.LDANumber = CurrentTbl69FiSpecies.LDANumber;
                            fispecies.BasinLength = CurrentTbl69FiSpecies.BasinLength;
                            fispecies.FishLength = CurrentTbl69FiSpecies.FishLength;
                            fispecies.Karnivore = CurrentTbl69FiSpecies.Karnivore;
                            fispecies.Herbivore = CurrentTbl69FiSpecies.Herbivore;
                            fispecies.Limnivore = CurrentTbl69FiSpecies.Limnivore;
                            fispecies.Omnivore = CurrentTbl69FiSpecies.Omnivore;
                            fispecies.MemoFoods = CurrentTbl69FiSpecies.MemoFoods;
                            fispecies.Difficult1 = CurrentTbl69FiSpecies.Difficult1;
                            fispecies.Difficult2 = CurrentTbl69FiSpecies.Difficult2;
                            fispecies.Difficult3 = CurrentTbl69FiSpecies.Difficult3;
                            fispecies.Difficult4 = CurrentTbl69FiSpecies.Difficult4;
                            fispecies.RegionTop = CurrentTbl69FiSpecies.RegionTop;
                            fispecies.RegionMiddle = CurrentTbl69FiSpecies.RegionMiddle;
                            fispecies.RegionBottom = CurrentTbl69FiSpecies.RegionBottom;
                            fispecies.MemoRegion = CurrentTbl69FiSpecies.MemoRegion;
                            fispecies.MemoTech = CurrentTbl69FiSpecies.MemoTech;
                            fispecies.Ph1 = CurrentTbl69FiSpecies.Ph1;
                            fispecies.Ph2 = CurrentTbl69FiSpecies.Ph2;
                            fispecies.Temp1 = CurrentTbl69FiSpecies.Temp1;
                            fispecies.Temp2 = CurrentTbl69FiSpecies.Temp2;
                            fispecies.Hardness1 = CurrentTbl69FiSpecies.Hardness1;
                            fispecies.Hardness2 = CurrentTbl69FiSpecies.Hardness2;
                            fispecies.CarboHardness1 = CurrentTbl69FiSpecies.CarboHardness1;
                            fispecies.CarboHardness2 = CurrentTbl69FiSpecies.CarboHardness2;
                            fispecies.MemoHusbandry = CurrentTbl69FiSpecies.MemoHusbandry;
                            fispecies.MemoBuilt = CurrentTbl69FiSpecies.MemoBuilt;
                            fispecies.MemoColor = CurrentTbl69FiSpecies.MemoColor;
                            fispecies.MemoSozial = CurrentTbl69FiSpecies.MemoSozial;
                            fispecies.MemoDomorphism = CurrentTbl69FiSpecies.MemoDomorphism;
                            fispecies.MemoSpecial = CurrentTbl69FiSpecies.MemoSpecial;
                            fispecies.Updater = Environment.UserName;
                            fispecies.UpdaterDate = DateTime.Now;
                            fispecies.MemoBreeding = CurrentTbl69FiSpecies.MemoBreeding;   
                        fispecies.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    fispecies = new Tbl69FiSpecies   //add new
                    {
							GenusID = CurrentTbl69FiSpecies.GenusID,
							SpeciesgroupID = CurrentTbl69FiSpecies.SpeciesgroupID,
							FiSpeciesName = CurrentTbl69FiSpecies.FiSpeciesName,
							Subspecies = CurrentTbl69FiSpecies.Subspecies,
							Divers = CurrentTbl69FiSpecies.Divers,
							CountID = RandomHelper.Randomnumber(),
							Valid = CurrentTbl69FiSpecies.Valid,
							ValidYear = CurrentTbl69FiSpecies.ValidYear,
							MemoSpecies = CurrentTbl69FiSpecies.MemoSpecies,
							TradeName = CurrentTbl69FiSpecies.TradeName,
							Author = CurrentTbl69FiSpecies.Author,
							AuthorYear = CurrentTbl69FiSpecies.AuthorYear,
							Importer = CurrentTbl69FiSpecies.Importer,
							ImportingYear = CurrentTbl69FiSpecies.ImportingYear,
							TypeSpecies = CurrentTbl69FiSpecies.TypeSpecies,
							LNumber = CurrentTbl69FiSpecies.LNumber,
							LOrigin = CurrentTbl69FiSpecies.LOrigin,
							LDAOrigin = CurrentTbl69FiSpecies.LDAOrigin,
							LDANumber = CurrentTbl69FiSpecies.LDANumber,
							BasinLength = CurrentTbl69FiSpecies.BasinLength,
							FishLength = CurrentTbl69FiSpecies.FishLength,
							Karnivore = CurrentTbl69FiSpecies.Karnivore,
							Herbivore = CurrentTbl69FiSpecies.Herbivore,
							Limnivore = CurrentTbl69FiSpecies.Limnivore,
							Omnivore = CurrentTbl69FiSpecies.Omnivore,
							MemoFoods = CurrentTbl69FiSpecies.MemoFoods,
							Difficult1 = CurrentTbl69FiSpecies.Difficult1,
							Difficult2 = CurrentTbl69FiSpecies.Difficult2,
							Difficult3 = CurrentTbl69FiSpecies.Difficult3,
							Difficult4 = CurrentTbl69FiSpecies.Difficult4,
							RegionTop = CurrentTbl69FiSpecies.RegionTop,
							RegionMiddle = CurrentTbl69FiSpecies.RegionMiddle,
							RegionBottom = CurrentTbl69FiSpecies.RegionBottom,
							MemoRegion = CurrentTbl69FiSpecies.MemoRegion,
							MemoTech = CurrentTbl69FiSpecies.MemoTech,
							Ph1 = CurrentTbl69FiSpecies.Ph1,
							Ph2 = CurrentTbl69FiSpecies.Ph2,
							Temp1 = CurrentTbl69FiSpecies.Temp1,
							Temp2 = CurrentTbl69FiSpecies.Temp2,
							Hardness1 = CurrentTbl69FiSpecies.Hardness1,
							Hardness2 = CurrentTbl69FiSpecies.Hardness2,
							CarboHardness1 = CurrentTbl69FiSpecies.CarboHardness1,
							CarboHardness2 = CurrentTbl69FiSpecies.CarboHardness2,
							MemoHusbandry = CurrentTbl69FiSpecies.MemoHusbandry,
							MemoBuilt = CurrentTbl69FiSpecies.MemoBuilt,
							MemoColor = CurrentTbl69FiSpecies.MemoColor,
							MemoSozial = CurrentTbl69FiSpecies.MemoSozial,
							MemoDomorphism = CurrentTbl69FiSpecies.MemoDomorphism,
							MemoSpecial = CurrentTbl69FiSpecies.MemoSpecial,
							Writer = Environment.UserName,
							WriterDate = DateTime.Now,
							Updater = Environment.UserName,
							UpdaterDate = DateTime.Now,
							MemoBreeding = CurrentTbl69FiSpecies.MemoBreeding,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //GenusID and SpeciesgroupID may be not 0
                    if (CurrentTbl69FiSpecies.GenusID == 0 || CurrentTbl69FiSpecies.SpeciesgroupID == 0)

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and GenusId and SpeciesgroupId already exist       
                    var dataset = _businessLayer.ListTbl69FiSpeciessesByFiSpeciesNameAndSubspeciesAndDiversAndGenusIdAndSpeciesgroupId(CurrentTbl69FiSpecies.FiSpeciesName, CurrentTbl69FiSpecies.Subspecies, CurrentTbl69FiSpecies.Divers, CurrentTbl69FiSpecies.GenusID, CurrentTbl69FiSpecies.SpeciesgroupID);

                    if (dataset.Count != 0 && CurrentTbl69FiSpecies.FiSpeciesID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl69FiSpecies.FiSpeciesID == 0 ||
                        dataset.Count != 0 && CurrentTbl69FiSpecies.FiSpeciesID != 0 ||
                        dataset.Count == 0 && CurrentTbl69FiSpecies.FiSpeciesID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdateFiSpecies(fispecies);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl69FiSpecies.FiSpeciesID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl69FiSpecies.FiSpeciesName + " " + CurrentTbl69FiSpecies.Subspecies + " " + CurrentTbl69FiSpecies.Divers,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
                 return;
            }

                Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl81Image.FiSpeciesID));            

            SelectedMainTabIndex = 0;
            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }
        #endregion "Public Commands"                        
                                                          

 //    Part 3    

           
        #region "Public Commands Connect <== Tbl72PlSpecies"                 
        //-------------------------------------------------------------------------
        private RelayCommand _savePlSpeciesCommand;

        public ICommand SavePlSpeciesCommand => _savePlSpeciesCommand ??
                                                 (_savePlSpeciesCommand = new RelayCommand(delegate { SavePlSpecies(null); }));

        //-------------------------------------------------------------------------          
       
        private void SavePlSpecies(object o)
        {           
             if (CurrentTbl69FiSpecies == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var plspecies = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesId(CurrentTbl72PlSpecies.PlSpeciesID);
                if (CurrentTbl72PlSpecies.PlSpeciesID != 0)
                {
                    if (plspecies != null) //update
                    {
                            plspecies.GenusID = CurrentTbl72PlSpecies.GenusID;
                            plspecies.SpeciesgroupID = CurrentTbl72PlSpecies.SpeciesgroupID;
                            plspecies.PlSpeciesName = CurrentTbl72PlSpecies.PlSpeciesName;
                            plspecies.Subspecies = CurrentTbl72PlSpecies.Subspecies;
                            plspecies.Divers = CurrentTbl72PlSpecies.Divers;
                            plspecies.Valid = CurrentTbl72PlSpecies.Valid;
                            plspecies.ValidYear = CurrentTbl72PlSpecies.ValidYear;
                            plspecies.MemoSpecies = CurrentTbl72PlSpecies.MemoSpecies;
                            plspecies.TradeName = CurrentTbl72PlSpecies.TradeName;
                            plspecies.Author = CurrentTbl72PlSpecies.Author;
                            plspecies.AuthorYear = CurrentTbl72PlSpecies.AuthorYear;
                            plspecies.Importer = CurrentTbl72PlSpecies.Importer;
                            plspecies.ImportingYear = CurrentTbl72PlSpecies.ImportingYear;
                            plspecies.BasinHeight = CurrentTbl72PlSpecies.BasinHeight;
                            plspecies.PlantLength = CurrentTbl72PlSpecies.PlantLength;
                            plspecies.Difficult1 = CurrentTbl72PlSpecies.Difficult1;
                            plspecies.Difficult2 = CurrentTbl72PlSpecies.Difficult2;
                            plspecies.Difficult3 = CurrentTbl72PlSpecies.Difficult3;
                            plspecies.Difficult4 = CurrentTbl72PlSpecies.Difficult4;
                            plspecies.MemoTech = CurrentTbl72PlSpecies.MemoTech;
                            plspecies.Ph1 = CurrentTbl72PlSpecies.Ph1;
                            plspecies.Ph2 = CurrentTbl72PlSpecies.Ph2;
                            plspecies.Temp1 = CurrentTbl72PlSpecies.Temp1;
                            plspecies.Temp2 = CurrentTbl72PlSpecies.Temp2;
                            plspecies.Hardness1 = CurrentTbl72PlSpecies.Hardness1;
                            plspecies.Hardness2 = CurrentTbl72PlSpecies.Hardness2;
                            plspecies.CarboHardness1 = CurrentTbl72PlSpecies.CarboHardness1;
                            plspecies.CarboHardness2 = CurrentTbl72PlSpecies.CarboHardness2;
                            plspecies.MemoBuilt = CurrentTbl72PlSpecies.MemoBuilt;
                            plspecies.MemoColor = CurrentTbl72PlSpecies.MemoColor;
                            plspecies.MemoReproduction = CurrentTbl72PlSpecies.MemoReproduction;
                            plspecies.MemoCulture = CurrentTbl72PlSpecies.MemoCulture;
                            plspecies.MemoGlobal = CurrentTbl72PlSpecies.MemoGlobal;
                            plspecies.Updater = Environment.UserName;
                            plspecies.UpdaterDate = DateTime.Now;
                            plspecies.EntityState = EntityState.Modified;
                        }
                    }
                    else
                    {
                        plspecies = new Tbl72PlSpecies   //add new
                        {
                            GenusID = CurrentTbl72PlSpecies.GenusID,
                            SpeciesgroupID = CurrentTbl72PlSpecies.SpeciesgroupID,
                            PlSpeciesName = CurrentTbl72PlSpecies.PlSpeciesName,
                            Subspecies = CurrentTbl72PlSpecies.Subspecies,
                            Divers = CurrentTbl72PlSpecies.Divers,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl72PlSpecies.Valid,
                            ValidYear = CurrentTbl72PlSpecies.ValidYear,
                            MemoSpecies = CurrentTbl72PlSpecies.MemoSpecies,
                            TradeName = CurrentTbl72PlSpecies.TradeName,
                            Author = CurrentTbl72PlSpecies.Author,
                            AuthorYear = CurrentTbl72PlSpecies.AuthorYear,
                            Importer = CurrentTbl72PlSpecies.Importer,
                            ImportingYear = CurrentTbl72PlSpecies.ImportingYear,
                            BasinHeight = CurrentTbl72PlSpecies.BasinHeight,
                            PlantLength = CurrentTbl72PlSpecies.PlantLength,
                            Difficult1 = CurrentTbl72PlSpecies.Difficult1,
                            Difficult2 = CurrentTbl72PlSpecies.Difficult2,
                            Difficult3 = CurrentTbl72PlSpecies.Difficult3,
                            Difficult4 = CurrentTbl72PlSpecies.Difficult4,
                            MemoTech = CurrentTbl72PlSpecies.MemoTech,
                            Ph1 = CurrentTbl72PlSpecies.Ph1,
                            Ph2 = CurrentTbl72PlSpecies.Ph2,
                            Temp1 = CurrentTbl72PlSpecies.Temp1,
                            Temp2 = CurrentTbl72PlSpecies.Temp2,
                            Hardness1 = CurrentTbl72PlSpecies.Hardness1,
                            Hardness2 = CurrentTbl72PlSpecies.Hardness2,
                            CarboHardness1 = CurrentTbl72PlSpecies.CarboHardness1,
                            CarboHardness2 = CurrentTbl72PlSpecies.CarboHardness2,
                            MemoBuilt = CurrentTbl72PlSpecies.MemoBuilt,
                            MemoColor = CurrentTbl72PlSpecies.MemoColor,
                            MemoReproduction = CurrentTbl72PlSpecies.MemoReproduction,
                            MemoCulture = CurrentTbl72PlSpecies.MemoCulture,
                            MemoGlobal = CurrentTbl72PlSpecies.MemoGlobal,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            EntityState = EntityState.Added
                        };
                    }
                    {   
                    //GenusID and SpeciesgroupID may be not 0
                    if (CurrentTbl72PlSpecies.GenusID == 0 || CurrentTbl72PlSpecies.SpeciesgroupID == 0)

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and GenusId and SpeciesgroupId already exist       
                    var dataset = _businessLayer.ListTbl72PlSpeciessesByPlSpeciesNameAndSubspeciesAndDiversAndGenusIdAndSpeciesgroupId(CurrentTbl72PlSpecies.PlSpeciesName, CurrentTbl72PlSpecies.Subspecies, CurrentTbl72PlSpecies.Divers, CurrentTbl72PlSpecies.GenusID, CurrentTbl72PlSpecies.SpeciesgroupID);

                    if (dataset.Count != 0 && CurrentTbl72PlSpecies.PlSpeciesID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl72PlSpecies.PlSpeciesName + " " + CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl72PlSpecies.PlSpeciesID == 0 ||
                        dataset.Count != 0 && CurrentTbl72PlSpecies.PlSpeciesID != 0 ||
                        dataset.Count == 0 && CurrentTbl72PlSpecies.PlSpeciesID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl72PlSpecies.PlSpeciesName + " " + CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                                _businessLayer.UpdatePlSpecies(plspecies);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl72PlSpecies.PlSpeciesID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl72PlSpecies.PlSpeciesName + " " + CurrentTbl72PlSpecies.Subspecies + " " + CurrentTbl72PlSpecies.Divers,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                    Log.Error(ex);
                  return;
            }

                Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciessesByPlSpeciesId(CurrentTbl81Image.PlSpeciesID));            

            SelectedMainTabIndex = 1;
            PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
            PlSpeciessesView.Refresh();
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
        public  ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??
                                                         (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); }));

        private void GetConnectedTablesById(object o)
        {
            SelectedMainTabIndex = 0;  //change to Connect tab
            SelectedDetailTabIndex = 1;
            SelectedDetailSubTabIndex = 0;

            _businessLayer = new BusinessLayer.BusinessLayer();
            Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68Speciesgroups());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());

            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(
                _businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl81Image.FiSpeciesID));

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }

        #endregion "Public Commands Connected Tables by DoubleClick"     
 

 //    Part 10    

     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubTabIndex;
        private int _selectedDetailSubFiSpeciesTabIndex;
        private int _selectedDetailSubPlSpeciesTabIndex;

        public  int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex;
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value;
                RaisePropertyChanged();
                if (_selectedMainTabIndex == 0)
                {
                    SelectedDetailTabIndex = 2;
                    SelectedDetailSubTabIndex = 0;
                }
                if (_selectedMainTabIndex == 1)
                {
                    SelectedDetailTabIndex = 2;
                    SelectedDetailSubTabIndex = 1;
                }
            }
        }

        public  int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex;
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailTabIndex == 0)
                {
                    SelectedDetailSubTabIndex = 0;
                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailTabIndex == 1)
                    SelectedDetailSubTabIndex = 0;
                if (_selectedDetailTabIndex == 2)
                    SelectedDetailSubTabIndex = 0;
                if (_selectedDetailTabIndex == 3)
                    SelectedDetailSubTabIndex = 0;
            }
        }

        public  int SelectedDetailSubTabIndex
        {
            get => _selectedDetailSubTabIndex;
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                _selectedDetailSubTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailSubTabIndex == 0)
                {
                    Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>(
                        _businessLayer.ListTbl68Speciesgroups());

                    Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(
                        _businessLayer.ListTbl66Genusses());

                    Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(
                        _businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl81Image.FiSpeciesID));

                    FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                    FiSpeciessesView.Refresh();

                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailSubTabIndex == 1)
                {
                    Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>(
                        _businessLayer.ListTbl68Speciesgroups());

                    Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(
                        _businessLayer.ListTbl66Genusses());

                    Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>(
                        _businessLayer.ListTbl72PlSpeciessesByPlSpeciesId(CurrentTbl81Image.PlSpeciesID));

                    PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                    PlSpeciessesView.Refresh();

                   SelectedDetailTabIndex = 4;
                   SelectedMainTabIndex = 1;
                }
            }
        }

        public int SelectedDetailSubFiSpeciesTabIndex
        {
            get => _selectedDetailSubFiSpeciesTabIndex;
            set { _selectedDetailSubFiSpeciesTabIndex = value; RaisePropertyChanged(); }
        }

        public int SelectedDetailSubPlSpeciesTabIndex
        {
            get => _selectedDetailSubPlSpeciesTabIndex;
            set { _selectedDetailSubPlSpeciesTabIndex = value; RaisePropertyChanged(); }
        }

        #endregion "Public Commands to open Detail TabItems"      
 

 //    Part 11    

      
        #region "Public Properties Tbl81Image"

        private int _searchImageId = 0;
        public int SearchImageId
        {
            get => _searchImageId;
            set { _searchImageId = value; RaisePropertyChanged(); }
        }

        public  ICollectionView ImagesView;
        private   Tbl81Image CurrentTbl81Image => ImagesView?.CurrentItem as Tbl81Image;

        private ObservableCollection<Tbl81Image> _tbl81ImagesList;
        public  ObservableCollection<Tbl81Image> Tbl81ImagesList
        {
            get => _tbl81ImagesList; 
            set {  _tbl81ImagesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl81Image> _tbl81ImagesAllList;
        public  ObservableCollection<Tbl81Image> Tbl81ImagesAllList
        {
            get => _tbl81ImagesAllList; 
            set { _tbl81ImagesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl69FiSpecies"

        public  ICollectionView FiSpeciessesView;
        private Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;           

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciessesList; 
            set { _tbl69FiSpeciessesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public  ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList; 
            set { _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
  
       
        #region "Public Properties Tbl72PlSpecies"

        public  ICollectionView PlSpeciessesView;
        private  Tbl72PlSpecies CurrentTbl72PlSpecies => PlSpeciessesView?.CurrentItem as Tbl72PlSpecies;           

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList;
        public   ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get => _tbl72PlSpeciessesList; 
            set { _tbl72PlSpeciessesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public  ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList; 
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(); }       
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
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties Tbl68Speciesgroup"

        #region "Public Properties Tbl66Genus"

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(); }
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
            set { _mimeTypes = value; RaisePropertyChanged(); }
        }

        private MimeType _selectedMimeType;
        public MimeType SelectedMimeType
        {
            get => _selectedMimeType;
            set { _selectedMimeType = value; RaisePropertyChanged(); }
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
            set { _selectedPath = value; RaisePropertyChanged(); }
        }


        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get => _imageSource;
            set { _imageSource = value; RaisePropertyChanged(); }
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
