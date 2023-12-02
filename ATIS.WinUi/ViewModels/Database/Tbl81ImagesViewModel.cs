
using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using WinRT.Interop;
using Windows.Storage.Streams;
using ATIS.WinUi.Models;

//    Tbl81ImagesViewModel Skriptdatum:  21.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;

public class Tbl81ImagesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService = null!;
    public ObservableCollection<Tbl81Image> ImageItems { get; } = new();

    public ObservableCollection<Tbl69FiSpecies> FiSpeciesItems { get; } = new();
    public ObservableCollection<Tbl72PlSpecies> PlSpeciesItems { get; } = new();

    private readonly AllDialogs _allDialogs = new();
    private readonly FileOpenPicker _filePicker = new();

    private readonly List<Data> _fileStreamList = new();
    public struct Data
    {
        public int ImageCountId;
        public byte[] ImageFileStream;
    };
    #endregion [Private Data Members]           

    #region [Constructor]
    public Tbl81ImagesViewModel(IDataService dataService)
    {
        _dataService = dataService;
        SelectedMainDetailTabIndex = 2; //Tab Datagrid
        GetAllCollections();
    }

    private void GetAllCollections()
    {
        Tbl68SpeciesgroupsAllList ??= new ObservableCollection<Tbl68Speciesgroup>();
        Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

        Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesAllList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers();
        Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesAllList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers();

    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands Image]

    public ICommand GetImagesByInfoOrIdCommand => new RelayCommand(delegate { var task = GetImagesByInfoOrId_Executed(SearchImageInfoOrId); });

    public ICommand AddImageCommand => new RelayCommand<string>(AddImage_Executed);
    public ICommand CopyImageCommand => new RelayCommand<string>(CopyImage_Executed);

    public ICommand DeleteImageCommand => new RelayCommand(execute: delegate { var task = DeleteImage(); });

    public ICommand SaveImageCommand => new RelayCommand(execute: delegate { var task = SaveImage(SearchImageInfoOrId, SelectedFilestream); });
    public ICommand ChangeImageCommand => new RelayCommand(execute: delegate { var task = OpenFileDialog(); });
    public ICommand RefreshImageServerCommand => new RelayCommand(execute: delegate { var task = RefreshImageServer(); });
    #endregion [Commands Image]         

    #region [Methods Image]

    private async Task GetImagesByInfoOrId_Executed(string searchImageInfoOrId)
    {
        ImageStartModify();
        Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesAllList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers();
        Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesAllList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers();

        Tbl81ImagesList ??= new ObservableCollection<Tbl81Image>();
        Tbl81ImagesList = await _dataService.GetTbl81ImagesCollectionOrderByInfoFromSearchInfoOrImageId(searchImageInfoOrId);

        if (Tbl81ImagesList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        ImageDataSetCount = Tbl81ImagesList.Count;
        RefreshImageItems();

        SelectedMainDetailTabIndex = 2;
    }

    private async void AddImage_Executed(string? parm)
    {
        ImageStartEdit();
        ImageStartNew();
        Tbl69FiSpeciessesAllList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesAllList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers();
        Tbl72PlSpeciessesAllList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesAllList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers();

        //Id search for first Dataset of Tbl69FiSpeciessesList + Tbl72PlSpeciessesList
        var id = 0;
        var single = await _dataService.GetFiSpeciesSingleFirstDataset();
        if (single != null)
        {
            id = single.FiSpeciesId;
        }

        var id1 = 0;
        var single1 = await _dataService.GetPlSpeciesSingleFirstDataset();
        if (single != null)
        {
            id1 = single1.PlSpeciesId;
        }

        Tbl81ImagesList ??= new ObservableCollection<Tbl81Image>();
        Tbl81ImagesList.Insert(0, new Tbl81Image { Info = "New", FiSpeciesId = id, PlSpeciesId = id1 });
        RefreshImageItems();

        //New Image search
        await OpenFileDialog();
    }

    private async void CopyImage_Executed(string? s)
    {
        //New Image search
        await OpenFileDialog();
        if (ImageSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        ImageStartEdit();
        ImageStartNew();
        Tbl81ImagesList ??= new ObservableCollection<Tbl81Image>();
        Tbl81ImagesList = await _dataService.CopyImage(ImageSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshImageItems();
    }

    private async Task DeleteImage()
    {
        if (ImageSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        //necessary to delete before
        var ret = _dataService.DeleteImage(ImageSelected);
        if (!await ret)
        {
            return;
        }

        Tbl81ImagesList = await _dataService.GetTbl81ImagesCollectionOrderByInfoFromSearchInfoOrImageId(SearchImageInfoOrId);

        ImageDataSetCount = Tbl81ImagesList.Count;
        RefreshImageItems();
    }

    private async Task SaveImage(string searchInfo, byte[] selectedFilestream)
    {
        if (string.IsNullOrEmpty(ImageSelected.Info))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (ImageSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        var iNdx = Tbl81ImagesList.IndexOf(Tbl81ImagesList.First(t =>
            t.Info == ImageSelected.Info));

        var ret = _dataService.SaveImage(ImageSelected, SelectedPath, selectedFilestream);
        if (!await ret)
        {
            return;
        }

        if (string.IsNullOrEmpty(searchInfo))
        {
            Tbl81ImagesList = await _dataService.GetLastDatasetInTbl81Images();
            RefreshImageItems();
        }
        else
        {
            if (ImageSelected.ImageId == 0) //new
            {
                Tbl81ImagesList = await _dataService.GetLastDatasetInTbl81Images();
                RefreshImageItems();
            }
            else
            {
                Tbl81ImagesList = await _dataService.GetTbl81ImagesCollectionOrderByInfoFromSearchImageId(searchInfo);
                //   Index Position ?
                if (iNdx < Tbl81ImagesList.Count)
                {
                    ImageItems.Clear();
                    foreach (var item in Tbl81ImagesList)
                    {
                        ImageItems.Add(item);
                    }
                    ImageSelected = Tbl81ImagesList[iNdx];
                }
            }
        }

        ImageDataSetCount = Tbl81ImagesList.Count;
        ImageCancelEditsAsync();
    }


    private Task RefreshImageServer()
    {
        Tbl81ImagesList ??= new ObservableCollection<Tbl81Image>();
        Tbl81ImagesList = _dataService.GetTbl81ImagesCollectionOrderByInfoFromImageId(ImageSelected.ImageId);

        ImageDataSetCount = Tbl81ImagesList.Count;

        RefreshImageItems();

        return Task.CompletedTask;
    }

    private async Task OpenFileDialog()
    {
        var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
        InitializeWithWindow.Initialize(_filePicker, hwnd);
        _filePicker.FileTypeFilter.Add("*");
        _filePicker.ViewMode = PickerViewMode.Thumbnail;
        //_filePicker.FileTypeFilter.Add("*");
        _filePicker.FileTypeFilter.Add(".jpg");
        _filePicker.FileTypeFilter.Add(".jpeg");
        _filePicker.FileTypeFilter.Add(".png");
        _filePicker.FileTypeFilter.Add(".gif");
        _filePicker.FileTypeFilter.Add(".jpe");
        _filePicker.FileTypeFilter.Add(".bmp");
        _filePicker.FileTypeFilter.Add(".ico");
        _filePicker.FileTypeFilter.Add(".tif");
        _filePicker.FileTypeFilter.Add(".tiff");
        _filePicker.FileTypeFilter.Add(".hpd");
        _filePicker.FileTypeFilter.Add(".jxr");
        _filePicker.FileTypeFilter.Add(".wdp");
        _filePicker.FileTypeFilter.Add(".hpd");
        var file = await _filePicker.PickSingleFileAsync();
        if (file != null && file.Path != null)
        {
            SelectedPath = file.Path;
            ImageSource = new BitmapImage(new Uri(file.Path));
        }
    }

    public void ImageStartEdit() => IsInEdit = true;
    public void ImageStartModify() => IsModified = true;
    public void ImageStartNew() => IsNewImage = true;
    public event EventHandler AddNewImageCanceled = null!;
    public void ImageCancelEditsAsync()
    {
        if (IsNewImage)
        {
            AddNewImageCanceled?.Invoke(this, EventArgs.Empty);
            IsInEdit = false;
            IsNewImage = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Methods Image]     




    //    Part 2    


    #region "Public Commands Connect <== Tbl69FiSpecies"                 

    public ICommand SaveFiSpeciesCommand => new RelayCommand<string>(SaveFiSpecies_Executed);
    public ICommand RefreshFiSpeciesServerCommand => new RelayCommand(execute: delegate { RefreshFiSpeciesServer_Executed(); });

    private async void SaveFiSpecies_Executed(string? s)
    {
        if (string.IsNullOrEmpty(FiSpeciesSelected.FiSpeciesName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (FiSpeciesSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.SaveFiSpecies(FiSpeciesSelected);

        if (!await ret)
        {
            return;
        }

        Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(ImageSelected.FiSpeciesId);
        RefreshFiSpeciesItems();
        FiSpeciesCancelEditsAsync();
    }
    private void RefreshFiSpeciesServer_Executed()
    {
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

        Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesList = _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(ImageSelected.FiSpeciesId);

        FiSpeciesDataSetCount = Tbl69FiSpeciessesList.Count;

        RefreshFiSpeciesItems();
    }
    public void FiSpeciesStartEdit() => IsInEdit = true;
    public void FiSpeciesStartModify() => IsModified = true;
    public void FiSpeciesCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                       



    //    Part 3    


    #region "Public Commands Connect <== Tbl72PlSpecies"                 

    public ICommand SavePlSpeciesCommand => new RelayCommand<string>(SavePlSpecies_Executed);
    public ICommand RefreshPlSpeciesServerCommand => new RelayCommand(execute: delegate { RefreshPlSpeciesServer_Executed(); });

    private async void SavePlSpecies_Executed(string? s)
    {
        if (string.IsNullOrEmpty(PlSpeciesSelected.PlSpeciesName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (PlSpeciesSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.SavePlSpecies(PlSpeciesSelected);

        if (!await ret)
        {
            return;
        }

        Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(ImageSelected.PlSpeciesId);
        RefreshPlSpeciesItems();
        PlSpeciesCancelEditsAsync();
    }
    private void RefreshPlSpeciesServer_Executed()
    {
        Tbl66GenussesAllList ??= new ObservableCollection<Tbl66Genus>();
        Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

        Tbl72PlSpeciessesList ??= new ObservableCollection<Tbl72PlSpecies>();
        Tbl72PlSpeciessesList = _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(ImageSelected.PlSpeciesId);

        PlSpeciesDataSetCount = Tbl72PlSpeciessesList.Count;

        RefreshPlSpeciesItems();
    }

    public void PlSpeciesStartEdit() => IsInEdit = true;
    public void PlSpeciesStartModify() => IsModified = true;
    public void PlSpeciesCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                       




    //    Part 4    




    //    Part 5    




    //    Part 6    




    //    Part 7    



    //    Part 8    



    //    Part 9    



    //    Part 10    


    #region "Public Commands to open Main and Detail TabItems"

    private int _selectedMainDetailTabIndex;


    public int SelectedMainDetailTabIndex
    {
        get => _selectedMainDetailTabIndex;
        set
        {
            if (value == _selectedMainDetailTabIndex)
            {
                return;
            }

            _selectedMainDetailTabIndex = value; OnPropertyChanged();

            if (_selectedMainDetailTabIndex == 0)
            {
                if (ImageSelected != null)
                {
                    IsLoading = true;
                    FiSpeciesStartModify();
                    Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
                    Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

                    Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
                    Tbl69FiSpeciessesList =
                        _dataService.GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(ImageSelected.FiSpeciesId);
                    FiSpeciesDataSetCount = Tbl69FiSpeciessesList.Count;
                    RefreshFiSpeciesItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 1)
            {
                if (ImageSelected != null)
                {
                    IsLoading = true;
                    PlSpeciesStartModify();
                    Tbl68SpeciesgroupsAllList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
                    Tbl66GenussesAllList = _dataService.GetTbl66GenussesCollectionOrderByGenusName();

                    Tbl72PlSpeciessesList =
                        _dataService.GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(ImageSelected.PlSpeciesId);
                    PlSpeciesDataSetCount = Tbl72PlSpeciessesList.Count;
                    RefreshPlSpeciesItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 2)
            {
            }

        }
    }


    #endregion "Public Commands to open Main and Detail TabItems"   



    //    Part 11 

    #region All Properties

    #region Image

    private byte[] _selectedFilestream = null!;
    public byte[] SelectedFilestream
    {
        get => _selectedFilestream;
        set
        {
            _selectedFilestream = value; OnPropertyChanged(nameof(SelectedFilestream));
        }
    }

    private byte[] _imageBuffer = null!;
    public byte[] ImageBuffer
    {
        get => _imageBuffer;
        set
        {
            _imageBuffer = value;
            _ = LoadImageAsync();
        }
    }

    private ImageSource _imageSource = null!;
    public ImageSource ImageSource
    {
        get => _imageSource;
        set
        {
            _imageSource = value; OnPropertyChanged(nameof(ImageSource));
        }
    }

    private async Task LoadImageAsync()
    {
        using var ms = new InMemoryRandomAccessStream();
        // Writes the image byte array in an InMemoryRandomAccessStream
        // that is needed to set the source of BitmapImage.
        using (var writer = new DataWriter(ms.GetOutputStreamAt(0)))
        {
            writer.WriteBytes(_imageBuffer);
            await writer.StoreAsync();
        }

        var image = new BitmapImage();
        await image.SetSourceAsync(ms);
        ImageSource = image;
    }

    #endregion

    #region Selected Properties
    private Tbl66Genus _genusSelected = null!;
    public Tbl66Genus GenusSelected
    {
        get => _genusSelected;
        set => SetProperty(ref _genusSelected, value);
    }

    private Tbl69FiSpecies _fispeciesSelected = null!;
    public Tbl69FiSpecies FiSpeciesSelected
    {
        get => _fispeciesSelected;
        set => SetProperty(ref _fispeciesSelected, value);
    }

    private Tbl72PlSpecies _plspeciesSelected = null!;

    public Tbl72PlSpecies PlSpeciesSelected
    {
        get => _plspeciesSelected;
        set => SetProperty(ref _plspeciesSelected, value);
    }

    private Tbl81Image _imageSelected = null!;
    public Tbl81Image ImageSelected
    {
        get => _imageSelected;
        set
        {
            SetProperty(ref _imageSelected, value);
            // ImageSelected loose ImageFileStream
       //     if (_imageSelected == null || (_imageSelected == null && _imageSelected.CountId == 0)) return;
            if (_imageSelected == null)
            {
                return;
            }

            _dataSet = _fileStreamList.Find(t => t.ImageCountId == _imageSelected.CountId);

            _imageSelected.Filestream = _dataSet.ImageFileStream;
            _selectedFilestream = _dataSet.ImageFileStream;
        }
    }

    #endregion

    #region Refresh Properties

    private void RefreshFiSpeciesItems()
    {
        FiSpeciesItems.Clear();
        if (Tbl69FiSpeciessesList != null)
        {
            foreach (var item in Tbl69FiSpeciessesList)
            {
                FiSpeciesItems.Add(item);
            }
            if (Tbl69FiSpeciessesList.Count == 0)
            {
                return;
            }

            if (FiSpeciesSelected == null && Tbl69FiSpeciessesList.Count != 0)
            {
                FiSpeciesSelected = FiSpeciesItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshPlSpeciesItems()
    {
        PlSpeciesItems.Clear();
        if (Tbl72PlSpeciessesList != null)
        {
            foreach (var item in Tbl72PlSpeciessesList)
            {
                PlSpeciesItems.Add(item);
            }
            if (Tbl72PlSpeciessesList.Count == 0)
            {
                return;
            }

            if (PlSpeciesSelected == null && Tbl72PlSpeciessesList.Count != 0)
            {
                PlSpeciesSelected = PlSpeciesItems.FirstOrDefault()!;
            }
        }
    }

    private void RefreshImageItems()
    {
        ImageItems.Clear();
        foreach (var item in Tbl81ImagesList)
        {
            ImageItems.Add(item);
            _fileStreamList.Add(new Data { ImageCountId = item.CountId, ImageFileStream = item.Filestream! });
        }

        if (Tbl81ImagesList.Count == 0)
        {
            return;
        }

        if (ImageSelected == null && Tbl81ImagesList.Count != 0)
        {
            ImageSelected = ImageItems.FirstOrDefault()!;
        }
    }

    #endregion Refresh Properties

    #region Public Properties  
    private int _fiSpeciesDataSetCount;
    public int FiSpeciesDataSetCount
    {
        get => _fiSpeciesDataSetCount;
        set
        {
            _fiSpeciesDataSetCount = value; OnPropertyChanged(nameof(FiSpeciesDataSetCount));
        }
    }

    private int _plSpeciesDataSetCount;
    public int PlSpeciesDataSetCount
    {
        get => _plSpeciesDataSetCount;
        set
        {
            _plSpeciesDataSetCount = value; OnPropertyChanged(nameof(PlSpeciesDataSetCount));
        }
    }
    private int _imageDataSetCount;
    public int ImageDataSetCount
    {
        get => _imageDataSetCount;
        set
        {
            _imageDataSetCount = value; OnPropertyChanged(nameof(ImageDataSetCount));
        }
    }

    //---------------------------------------------------


    public bool IsModified
    {
        get; set;
    }

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private bool _isInEdit = false;
    public bool IsInEdit
    {
        get => _isInEdit;
        set => SetProperty(ref _isInEdit, value);
    }

    private bool _isNewImage;
    public bool IsNewImage
    {
        get => _isNewImage;
        set => SetProperty(ref _isNewImage, value);
    }


    //--------------------------------------------------- 

    public int SearchImageId
    {
        get; set;
    }

    private string _searchImageInfoOrId = "";

    public string SearchImageInfoOrId
    {
        get => _searchImageInfoOrId;
        set
        {
            _searchImageInfoOrId = value; OnPropertyChanged(nameof(SearchImageInfoOrId));
        }
    }

    private Data _dataSet;

    public Data DataSet
    {
        get => _dataSet;
        set
        {
            _dataSet = value; OnPropertyChanged(nameof(DataSet));
        }
    }
    private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList = null!;
    public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
    {
        get => _tbl69FiSpeciessesList;
        set
        {
            _tbl69FiSpeciessesList = value; OnPropertyChanged(nameof(Tbl69FiSpeciessesList));
        }
    }

    private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesList = null!;

    public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
    {
        get => _tbl72PlSpeciessesList;
        set
        {
            _tbl72PlSpeciessesList = value; OnPropertyChanged(nameof(Tbl72PlSpeciessesList));
        }
    }

    private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList = null!;

    public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
    {
        get => _tbl69FiSpeciessesAllList;
        set
        {
            _tbl69FiSpeciessesAllList = value; OnPropertyChanged(nameof(Tbl69FiSpeciessesAllList));
        }
    }

    private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList = null!;

    public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
    {
        get => _tbl72PlSpeciessesAllList;
        set
        {
            _tbl72PlSpeciessesAllList = value; OnPropertyChanged(nameof(Tbl72PlSpeciessesAllList));
        }
    }

    private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList = null!;

    public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
    {
        get => _tbl66GenussesAllList;
        set
        {
            _tbl66GenussesAllList = value; OnPropertyChanged(nameof(Tbl66GenussesAllList));
        }
    }

    private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList = null!;

    public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
    {
        get => _tbl68SpeciesgroupsAllList;
        set
        {
            _tbl68SpeciesgroupsAllList = value; OnPropertyChanged(nameof(Tbl68SpeciesgroupsAllList));
        }
    }

    private ObservableCollection<Tbl81Image> _tbl81ImagesList = null!;
    public ObservableCollection<Tbl81Image> Tbl81ImagesList
    {
        get => _tbl81ImagesList;
        set
        {
            _tbl81ImagesList = value; OnPropertyChanged(nameof(Tbl81ImagesList));
        }
    }

    public static RelayCommand OpenCommand
    {
        get; set;
    } = null!;

    private string _selectedPath = null!;

    public string SelectedPath
    {
        get => _selectedPath;
        set
        {
            _selectedPath = value; OnPropertyChanged(nameof(SelectedPath));
        }
    }


    #endregion Public Properties

    #endregion All Properties


    public Tbl81ImagesViewModel()
    {

    }

}
