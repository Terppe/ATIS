using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using log4net;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

//using GalaSoft.MvvmLight.Command;
//using Tyrrrz.Extensions;
//using YoutubeExplode;
//using YoutubeExplode.Models;
//using YoutubeExplode.Models.ClosedCaptions;
//using YoutubeExplode.Models.MediaStreams;
//using RelayCommand = Te.Atis.Ui.Desktop.Domain.RelayCommand;

//    ImagesViewModel Skriptdatum:  02.02.2021  10:32    

namespace ATIS.Ui.Views.Database.D81Image
{

    public class ImagesViewModel : ViewModelBase
    {
        // Version with Generic Unit Of Work and AtisDbContext for general use   

        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(typeof(ImagesViewModel));
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        private readonly SaveFunctions _extSave = new SaveFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;


        //YouTube
        //private readonly YoutubeClient _client;
        //private bool _isBusy;
        //private string _query;
        //private Video _video;
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
                //_client = new YoutubeClient();

                // Commands
                //GetVideoCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(GetVideo,
                //    () => !IsBusy && Query.IsNotBlank());
                //DownloadMediaStreamCommand = new RelayCommand<MediaStreamInfo>(DownloadMediaStream,
                //    _ => !IsBusy);
                //DownloadClosedCaptionTrackCommand = new RelayCommand<ClosedCaptionTrackInfo>(
                //    DownloadClosedCaptionTrack, _ => !IsBusy);

            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]          


        //    Part 1    



        #region [Commands Image]

        private RelayCommand _getImagesByIdCommand;
        public ICommand GetImagesByIdCommand => _getImagesByIdCommand ??= new RelayCommand(delegate { ExecuteGetImagesById(SearchImageId); });

        private RelayCommand _addImageCommand;
        public ICommand AddImageCommand => _addImageCommand ??= new RelayCommand(delegate { ExecuteAddImage(null); });

        private RelayCommand _copyImageCommand;
        public ICommand CopyImageCommand => _copyImageCommand ??= new RelayCommand(delegate { ExecuteCopyImage(null); });

        private RelayCommand _deleteImageCommand;
        public ICommand DeleteImageCommand => _deleteImageCommand ??= new RelayCommand(delegate { ExecuteDeleteImage(SearchImageId); });

        private RelayCommand _saveImageCommand;
        public ICommand SaveImageCommand => _saveImageCommand ??= new RelayCommand(delegate { ExecuteSaveImage(SearchImageId); });

        #endregion [Commands Image]       


        #region [Methods Image]

        private void ExecuteGetImagesById(int searchId)
        {
            if (Tbl69FiSpeciessesAllList == null)
                Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
            else
                Tbl69FiSpeciessesAllList.Clear();

            if (Tbl72PlSpeciessesAllList == null)
                Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
            else
                Tbl72PlSpeciessesAllList.Clear();

            if (Tbl81ImagesList == null)
                Tbl81ImagesList ??= new ObservableCollection<Tbl81Image>();
            else
                Tbl81ImagesList.Clear();

            Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("FiSpecies");
            Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("PlSpecies");

            Tbl81ImagesList = _extCrud.GetImagesCollectionFromSearchIdOrderBy<Tbl81Image>(searchId);

            if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl81ImagesList.Count)) return;

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 2;

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.Refresh();
        }

        private void ExecuteAddImage(object o)
        {
            if (Tbl81ImagesList == null)
                Tbl81ImagesList ??= new ObservableCollection<Tbl81Image>();
            else
                Tbl81ImagesList.Clear();

            if (Tbl69FiSpeciessesAllList == null)
                Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
            else
                Tbl69FiSpeciessesAllList.Clear();

            if (Tbl72PlSpeciessesAllList == null)
                Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
            else
                Tbl72PlSpeciessesAllList.Clear();

            Tbl81ImagesList.Insert(0, new Tbl81Image { Info = CultRes.StringsRes.DatasetNew });

            Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("FiSpecies");
            Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("PlSpecies");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 2;

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();
        }

        private void ExecuteCopyImage(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl81Image)) return;

            Tbl81ImagesList = _extCrud.CopyImage(CurrentTbl81Image);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteImage(int searchId)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl81Image)) return;

            _extDelete.DeleteImage(CurrentTbl81Image);

            Tbl81ImagesList = _extCrud.GetImagesCollectionFromSearchIdOrderBy<Tbl81Image>(searchId);
            ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
            ImagesView.MoveCurrentToLast();
        }

        private void ExecuteSaveImage(int searchId)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl81Image)) return;

            _position = ImagesView.CurrentPosition;

            var ret = _extSave.SaveImage(CurrentTbl81Image);

            if (ret != true)
            {
                ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
                ImagesView.Refresh();
                return;
            }

            if (CurrentTbl81Image.ImageId == 0) //new
            {
                Tbl81ImagesList = _extCrud.GetLastImagesDatasetOrderById();
                ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
                ImagesView.MoveCurrentToFirst();
            }
            else
            {
                Tbl81ImagesList = _extCrud.GetImagesCollectionFromSearchIdOrderBy<Tbl81Image>(searchId);
                ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
                ImagesView.MoveCurrentToPosition(_position);
            }
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

        public ICommand SaveFiSpeciesCommand => _saveFiSpeciesCommand ??= new RelayCommand(delegate { ExecuteSaveFiSpecies(null); });

        private void ExecuteSaveFiSpecies(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl69FiSpecies)) return;

            _extSave.SaveFiSpecies(CurrentTbl69FiSpecies);

            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl81Image.FiSpeciesId);
            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();
        }

        #endregion "Public Commands"                  


        //    Part 3    


        #region "Public Commands Connect <== Tbl72PlSpecies"                 

        private RelayCommand _savePlSpeciesCommand;

        public ICommand SavePlSpeciesCommand => _savePlSpeciesCommand ??= new RelayCommand(delegate { ExecuteSavePlSpecies(null); });


        private void ExecuteSavePlSpecies(object o)
        {
            if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl72PlSpecies)) return;

            _extSave.SavePlSpecies(CurrentTbl72PlSpecies);

            Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromPlSpeciesIdOrderBy<Tbl72PlSpecies>(CurrentTbl81Image.PlSpeciesId);
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
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {

            Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
            Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

            Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl81Image.FiSpeciesId);

            FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
            FiSpeciessesView.Refresh();

        }

        #endregion "Public Method Connected Tables by DoubleClick"     



        //    Part 10    


        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedDetailTabIndex;


        public int SelectedMainTabIndex
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

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }

                if (_selectedMainTabIndex == 1)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromPlSpeciesIdOrderBy<Tbl72PlSpecies>(CurrentTbl81Image.PlSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                        PlSpeciessesView.Refresh();
                    }
                    SelectedDetailTabIndex = 1;
                }

            }
        }

        public int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex;
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value; RaisePropertyChanged("");

                if (_selectedDetailTabIndex == 0)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl69FiSpeciessesList = _extCrud.GetFiSpeciessesCollectionFromFiSpeciesIdOrderBy<Tbl69FiSpecies>(CurrentTbl81Image.FiSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        FiSpeciessesView = CollectionViewSource.GetDefaultView(Tbl69FiSpeciessesList);
                        FiSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)
                {
                    if (CurrentTbl81Image != null)
                    {
                        Tbl72PlSpeciessesList = _extCrud.GetPlSpeciessesCollectionFromPlSpeciesIdOrderBy<Tbl72PlSpecies>(CurrentTbl81Image.PlSpeciesId);

                        Tbl66GenussesAllList = _extCrud.GetCollectionAllOrderBy<Tbl66Genus>("Genus");
                        Tbl68SpeciesgroupsAllList = _extCrud.GetCollectionAllOrderBy<Tbl68Speciesgroup>("Speciesgroup");

                        PlSpeciessesView = CollectionViewSource.GetDefaultView(Tbl72PlSpeciessesList);
                        PlSpeciessesView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 2)
                {
                    if (CurrentTbl81Image != null)
                    {
                        //  var plantaeRegnum = _extCrud.GetPlSpeciesSingleByPlSpeciesName<Tbl72PlSpecies>("Plantae#Regnum#");
                        // CurrentTbl81Image.PlSpeciesId = plantaeRegnum.PlSpeciesId;
                        //    CurrentTbl81Image.PlSpeciesId = 1;
                        //  var animaliaRegnum = _extCrud.GetFiSpeciesSingleByFiSpeciesName<Tbl69FiSpecies>("Animalia#Regnum#");
                        // CurrentTbl81Image.FiSpeciesId = animaliaRegnum.FiSpeciesId;
                        //  CurrentTbl81Image.FiSpeciesId = 2;

                        if (CurrentTbl81Image.FiSpeciesId == 2)
                        {
                            Tbl81ImagesList = _extCrud.GetNamesCollectionFromPlSpeciesIdOrderBy<Tbl81Image>(CurrentTbl81Image.PlSpeciesId);
                        }
                        if (CurrentTbl81Image.PlSpeciesId == 1)
                        {
                            Tbl81ImagesList = _extCrud.GetNamesCollectionFromFiSpeciesIdOrderBy<Tbl81Image>(CurrentTbl81Image.FiSpeciesId);
                        }

                        Tbl69FiSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl69FiSpecies>("FiSpecies");
                        Tbl72PlSpeciessesAllList = _extCrud.GetCollectionAllOrderBy<Tbl72PlSpecies>("PlSpecies");

                        ImagesView = CollectionViewSource.GetDefaultView(Tbl81ImagesList);
                        ImagesView.Refresh();
                    }
                    SelectedMainTabIndex = 0;
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

        public ICollectionView ImagesView;
        private Tbl81Image CurrentTbl81Image => ImagesView?.CurrentItem as Tbl81Image;

        private ObservableCollection<Tbl81Image> _tbl81ImagesList;
        public ObservableCollection<Tbl81Image> Tbl81ImagesList
        {
            get => _tbl81ImagesList;
            set { _tbl81ImagesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl81Image> _tbl81ImagesAllList;
        public ObservableCollection<Tbl81Image> Tbl81ImagesAllList
        {
            get => _tbl81ImagesAllList;
            set { _tbl81ImagesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl69FiSpecies"

        public ICollectionView FiSpeciessesView;
        private Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciessesList;
            set { _tbl69FiSpeciessesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList;
            set { _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   


        #region "Public Properties Tbl72PlSpecies"

        public ICollectionView PlSpeciessesView;
        private Tbl72PlSpecies CurrentTbl72PlSpecies => PlSpeciessesView?.CurrentItem as Tbl72PlSpecies;

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get => _tbl72PlSpeciessesList;
            set { _tbl72PlSpeciessesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList;
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        //#region Video
        //public bool IsBusy
        //{
        //    get => _isBusy;
        //    private set
        //    {
        //        Set(ref _isBusy, value);
        //        GetVideoCommand.RaiseCanExecuteChanged();
        //        DownloadMediaStreamCommand.RaiseCanExecuteChanged();
        //    }
        //}

        //public string Query
        //{
        //    get => _query;
        //    set
        //    {
        //        Set(ref _query, value);
        //        GetVideoCommand.RaiseCanExecuteChanged();
        //    }
        //}

        //public Video Video
        //{
        //    get => _video;
        //    private set
        //    {
        //        Set(ref _video, value);
        //        RaisePropertyChanged(() => IsVideoAvailable);
        //    }
        //}

        //public bool IsVideoAvailable => Video != null;

        //public double Progress
        //{
        //    get => _progress;
        //    private set => Set(ref _progress, value);
        //}

        //public bool IsProgressIndeterminate
        //{
        //    get => _isProgressIndeterminate;
        //    private set => Set(ref _isProgressIndeterminate, value);
        //}


        //// Commands
        //public GalaSoft.MvvmLight.CommandWpf.RelayCommand GetVideoCommand { get; }
        //public RelayCommand<MediaStreamInfo> DownloadMediaStreamCommand { get; }
        //public RelayCommand<ClosedCaptionTrackInfo> DownloadClosedCaptionTrackCommand { get; }

        //private async void GetVideo()
        //{
        //    IsBusy = true;
        //    IsProgressIndeterminate = true;

        //    // Reset data
        //    Video = null;

        //    // Parse URL if necessary
        //    if (!YoutubeClient.TryParseVideoId(Query, out string videoId))
        //        videoId = Query;

        //    // Perform the request
        //    Video = await _client.GetVideoAsync(videoId);

        //    IsBusy = false;
        //    IsProgressIndeterminate = false;

        //}

        //private void DownloadMediaStream(MediaStreamInfo info)
        //{
        //    // Create dialog
        //    var fileExt = info.Container.GetFileExtension();
        //    var defaultFileName = $"{Video.Title}.{fileExt}"
        //        .Replace(Path.GetInvalidFileNameChars(), '_');
        //    var sfd = new SaveFileDialog
        //    {
        //        AddExtension = true,
        //        DefaultExt = fileExt,
        //        FileName = defaultFileName,
        //        Filter = $"{info.Container} Files|*.{fileExt}|All Files|*.*"
        //    };

        //    // Select file path
        //    if (sfd.ShowDialog() != true)
        //        return;

        //    var filePath = sfd.FileName;

        //    // Download to file
        //    IsBusy = true;
        //    Progress = 0;

        //    var progressHandler = new Progress<double>(p => Progress = p);
        //    _client.DownloadMediaStreamAsync(info, filePath, progressHandler);

        //    IsBusy = false;
        //    Progress = 0;
        //}

        //private async void DownloadClosedCaptionTrack(ClosedCaptionTrackInfo info)
        //{
        //    // Create dialog
        //    var fileExt = $"{Video.Title}.{info.Language.Name}.srt"
        //        .Replace(Path.GetInvalidFileNameChars(), '_');
        //    var filter = "SRT Files|*.srt|All Files|*.*";
        //    var sfd = new SaveFileDialog
        //    {
        //        AddExtension = true,
        //        DefaultExt = "srt",
        //        FileName = fileExt,
        //        Filter = filter
        //    };

        //    // Select file path
        //    if (sfd.ShowDialog() != true)
        //        return;

        //    var filePath = sfd.FileName;

        //    // Download to file
        //    IsBusy = true;
        //    Progress = 0;

        //    var progressHandler = new Progress<double>(p => Progress = p);
        //    await _client.DownloadClosedCaptionTrackAsync(info, filePath, progressHandler);

        //    IsBusy = false;
        //    Progress = 0;
        //}
        //#endregion

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
            // OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
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
