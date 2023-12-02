﻿using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using ATIS.WinUi.Models;
using Microsoft.UI.Xaml.Media;
using WinRT.Interop;

//    Tbl69FiSpeciessesViewModel Skriptdatum:  04.04.2023  10:32    

namespace ATIS.WinUi.ViewModels.Database;
public class Tbl69FiSpeciessesViewModel : ObservableObject
{

    #region [Private Data Members]
    private readonly IDataService _dataService;
    public ObservableCollection<Tbl69FiSpecies?> FiSpeciesItems { get; } = new();
    public ObservableCollection<Tbl78Name> NameItems { get; } = new();

    public ObservableCollection<Tbl68Speciesgroup> SpeciesgroupItems { get; } = new();
    public ObservableCollection<Tbl66Genus> GenusItems { get; } = new();
    public ObservableCollection<Tbl81Image> ImageItems { get; } = new();
    public ObservableCollection<Tbl84Synonym> SynonymItems { get; } = new();
    public ObservableCollection<Tbl87Geographic> GeographicItems { get; } = new();

    public ObservableCollection<Tbl90Reference> ReferenceExpertItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceSourceItems { get; } = new();
    public ObservableCollection<Tbl90Reference> ReferenceAuthorItems { get; } = new();
    public ObservableCollection<Tbl93Comment> CommentItems { get; } = new();
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
    public Tbl69FiSpeciessesViewModel(IDataService dataService)
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
        Tbl63InfratribussesAllList ??= new ObservableCollection<Tbl63Infratribus>();
        Tbl63InfratribussesAllList = _dataService.GetTbl63InfratribussesCollectionOrderByInfratribusName();

        TblCountriesAllList ??= new ObservableCollection<TblCountry>();
        TblCountriesAllList = _dataService.GetTblCountriesCollectionOrderByName();

        Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();
        Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
        Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();
        Languages = new List<Language>();
        GetValueLanguage();
        GetValueContinent();
        GetValueMimeType();
    }

    #endregion [Constructor]  


    //    Part 1    



    #region [Commands FiSpecies]

    public ICommand GetFiSpeciessesByNameOrIdCommand => new RelayCommand(execute: delegate
    { var task = GetFiSpeciessesByNameOrId_Executed(SearchFiSpeciesName); });

    public ICommand AddFiSpeciesCommand => new RelayCommand<string>(AddFiSpecies_Executed);
    public ICommand CopyFiSpeciesCommand => new RelayCommand<string>(CopyFiSpecies_Executed);

    public ICommand DeleteFiSpeciesCommand => new RelayCommand(execute: delegate { DeleteFiSpecies_Executed(SearchFiSpeciesName); });

    public ICommand SaveFiSpeciesCommand => new RelayCommand(execute: delegate { var task = SaveFiSpecies_Executed(SearchFiSpeciesName); });
    public ICommand RefreshFiSpeciesServerCommand => new RelayCommand(execute: delegate { RefreshFiSpeciesServer_Executed(SearchFiSpeciesName); });

    #endregion [Commands FiSpecies]

    #region [Methods FiSpecies]

    private async Task GetFiSpeciessesByNameOrId_Executed(string searchName)
    {
        FiSpeciesStartModify();
        Tbl66GenussesList?.Clear();
        Tbl68SpeciesgroupsList?.Clear();
        Tbl69FiSpeciessesList?.Clear();
        Tbl78NamesList?.Clear();
        Tbl81ImagesList?.Clear();
        Tbl84SynonymsList?.Clear();
        Tbl87GeographicsList?.Clear();

        Tbl90ReferenceExpertsList?.Clear();
        Tbl90ReferenceSourcesList?.Clear();
        Tbl90ReferenceAuthorsList?.Clear();
        Tbl93CommentsList?.Clear();

        GenusItems.Clear();
        SpeciesgroupItems.Clear();
        FiSpeciesItems.Clear();
        NameItems.Clear();
        ImageItems.Clear();
        SynonymItems.Clear();
        GeographicItems.Clear();

        ReferenceAuthorItems.Clear();
        ReferenceSourceItems.Clear();
        ReferenceExpertItems.Clear();
        CommentItems.Clear();

        Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesList = await _dataService.GetTbl69FiSpeciessesCollectionOrderByFiSpeciesNameFromSearchNameOrId(searchName);

        if (Tbl69FiSpeciessesList.Count == 0)
        {
            await _allDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }
        FiSpeciesDataSetCount = Tbl69FiSpeciessesList.Count;
        RefreshFiSpeciesItems();

        SelectedMainDetailTabIndex = 2;
    }

    private async void AddFiSpecies_Executed(string? parm)
    {
        FiSpeciesStartEdit();
        FiSpeciesStartNew();
        //Id search for first Dataset of Tbl66GenussesList
        var id = 0;
        var single = await _dataService.GetGenusSingleFirstDataset();
        if (single != null)
        {
            id = single.GenusId;
        }

        Tbl69FiSpeciessesList ??= new ObservableCollection<Tbl69FiSpecies>();
        Tbl69FiSpeciessesList.Insert(0, new Tbl69FiSpecies { FiSpeciesName = "New", GenusId = id });

        RefreshFiSpeciesItems();
    }

    private async void CopyFiSpecies_Executed(string? parm)
    {
        FiSpeciesStartEdit();
        FiSpeciesStartNew();

        Tbl69FiSpeciessesList = await _dataService.CopyFiSpecies(FiSpeciesSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshFiSpeciesItems();
    }

    private async void DeleteFiSpecies_Executed(string? parm)
    {

        if (await _allDialogs.DeleteDatasetQuestionConfirmationDialogAsync(FiSpeciesSelected!.FiSpeciesName!))
        {
            //necessary to delete before
            await _dataService.DeleteConnectedNames(FiSpeciesSelected);
            await _dataService.DeleteConnectedImages(FiSpeciesSelected);
            await _dataService.DeleteConnectedSynonyms(FiSpeciesSelected);
            await _dataService.DeleteConnectedGeographics(FiSpeciesSelected);
            await _dataService.DeleteConnectedFiSpeciesReferences(FiSpeciesSelected);
            await _dataService.DeleteConnectedFiSpeciesComments(FiSpeciesSelected);

            var ret = _dataService.DeleteFiSpecies(FiSpeciesSelected);
            if (!await ret)
            {
                return;
            }

            Tbl69FiSpeciessesList = await _dataService.GetTbl69FiSpeciessesCollectionOrderByFiSpeciesNameFromSearchNameOrId(parm!);

            FiSpeciesDataSetCount = Tbl69FiSpeciessesList.Count;
            RefreshFiSpeciesItems();
        }
    }

    private async Task SaveFiSpecies_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(FiSpeciesSelected?.FiSpeciesName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl69FiSpeciessesList != null)
        {

            var iNdx = Tbl69FiSpeciessesList.IndexOf(Tbl69FiSpeciessesList.First(t =>
                 t.FiSpeciesName == FiSpeciesSelected.FiSpeciesName));

            var ret = _dataService.SaveFiSpecies(FiSpeciesSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl69FiSpeciessesList = await _dataService.GetLastDatasetInTbl69FiSpeciesses();
                RefreshFiSpeciesItems();
            }
            else
            {
                if (FiSpeciesSelected.FiSpeciesId == 0) //new
                {
                    Tbl69FiSpeciessesList = await _dataService.GetLastDatasetInTbl69FiSpeciesses();
                    RefreshFiSpeciesItems();
                }
                else
                {
                    Tbl69FiSpeciessesList = await _dataService.GetTbl69FiSpeciessesCollectionOrderByFiSpeciesNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl69FiSpeciessesList!.Count)
                    {
                        FiSpeciesItems.Clear();
                        foreach (var item in Tbl69FiSpeciessesList)
                        {
                            FiSpeciesItems.Add(item);
                        }

                        FiSpeciesSelected = Tbl69FiSpeciessesList[iNdx];
                    }
                }
            }
        }
        FiSpeciesDataSetCount = Tbl69FiSpeciessesList!.Count;
        FiSpeciesCancelEditsAsync();
    }

    private async void RefreshFiSpeciesServer_Executed(string? parm)
    {
        Tbl69FiSpeciessesList = await _dataService.GetTbl69FiSpeciessesCollectionOrderByFiSpeciesNameFromSearchNameOrId(parm!);

        FiSpeciesDataSetCount = Tbl69FiSpeciessesList.Count;
        RefreshFiSpeciesItems();
    }

    public void FiSpeciesStartEdit() => IsInEdit = true;
    public void FiSpeciesStartModify() => IsModified = true;
    public void FiSpeciesStartNew() => IsNewFiSpecies = true;
    public event EventHandler AddNewFiSpeciesCanceled = null!;
    public void FiSpeciesCancelEditsAsync()
    {
        if (IsNewFiSpecies)
        {
            IsInEdit = false;
            AddNewFiSpeciesCanceled?.Invoke(this, EventArgs.Empty);
            IsNewFiSpecies = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Methods Tbl69FiSpecies]    




    //    Part 2    


    #region "Public Commands Connect <== Tbl66Genus"

    public ICommand SaveGenusCommand => new RelayCommand<string>(SaveGenus_Executed);
    public ICommand RefreshGenusServerCommand => new RelayCommand(execute: delegate { RefreshGenusServer_Executed(SearchFiSpeciesName); });

    private async void SaveGenus_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(GenusSelected?.GenusName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl66GenussesList != null)
        {
            var iNdx = Tbl66GenussesList.IndexOf(Tbl66GenussesList.First(t =>
               t.GenusName == GenusSelected.GenusName));

            var ret = _dataService.SaveGenus(GenusSelected);
            if (!await ret)
            {
                return;
            }

            if (string.IsNullOrEmpty(parm))
            {
                Tbl66GenussesList = await _dataService.GetLastDatasetInTbl66Genusses();
                RefreshGenusItems();
            }
            else
            {
                if (GenusSelected.GenusId == 0) //new
                {
                    Tbl66GenussesList = await _dataService.GetLastDatasetInTbl66Genusses();
                    RefreshGenusItems();
                }
                else
                {
                    Tbl66GenussesList = await _dataService.GetTbl66GenussesCollectionOrderByGenusNameFromSearchNameOrId(parm);
                    //   Index Position ?
                    if (iNdx < Tbl66GenussesList!.Count)
                    {
                        GenusItems.Clear();
                        foreach (var item in Tbl66GenussesList)
                        {
                            GenusItems.Add(item);
                        }

                        GenusSelected = Tbl66GenussesList[iNdx];
                    }
                }
            }
        }

        GenusDataSetCount = Tbl66GenussesList!.Count;
        GenusCancelEditsAsync();
    }
    private async void RefreshGenusServer_Executed(string? parm)
    {
        Tbl66GenussesList = await _dataService.GetTbl66GenussesCollectionOrderByGenusNameFromSearchNameOrId(parm!);

        GenusDataSetCount = Tbl66GenussesList.Count;
        RefreshGenusItems();
    }

    public void GenusStartEdit() => IsInEdit = true;
    public void GenusStartModify() => IsModified = true;
    public void GenusCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }

    #endregion "Public Commands"                  




    //    Part 3    


    #region "Public Commands Connect <== Tbl68Speciesgroup"

    public ICommand SaveSpeciesgroupCommand => new RelayCommand<string>(SaveSpeciesgroup_Executed);
    public ICommand RefreshSpeciesgroupServerCommand => new RelayCommand(execute: delegate { RefreshSpeciesgroupServer_Executed(); });


    private async void SaveSpeciesgroup_Executed(string? parm)
    {
        if (string.IsNullOrEmpty(SpeciesgroupSelected.SpeciesgroupName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl68SpeciesgroupsList.Count > 0)
        {
            if (SpeciesgroupSelected == null)
            {
                await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
                return;
            }
        }
        var ret = _dataService.SaveSpeciesgroup(SpeciesgroupSelected);

        if (!await ret)
        {
            return;
        }

        Tbl68SpeciesgroupsList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroupFromSpeciesgroupId(SpeciesgroupSelected.SpeciesgroupId);
        SpeciesgroupDataSetCount = Tbl68SpeciesgroupsList.Count;
        RefreshSpeciesgroupItems();
        SpeciesgroupCancelEditsAsync();
    }
    private Task RefreshSpeciesgroupServer_Executed()
    {
        Tbl68SpeciesgroupsList ??= new ObservableCollection<Tbl68Speciesgroup>();
        Tbl68SpeciesgroupsList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroupFromSpeciesgroupId(FiSpeciesSelected.SpeciesgroupId);

        SpeciesgroupDataSetCount = Tbl68SpeciesgroupsList.Count;

        RefreshSpeciesgroupItems();
        return Task.CompletedTask;
    }

    public void SpeciesgroupStartEdit() => IsInEdit = true;
    public void SpeciesgroupStartModify() => IsModified = true;
    public void SpeciesgroupCancelEditsAsync()
    {
        IsInEdit = false;
        IsModified = false;
    }
    #endregion "Public Commands"       






    //    Part 4    


    #region [Public Commands Connect ==> Tbl78Name]
    public ICommand AddNameCommand => new RelayCommand<string>(AddName_Executed);
    public ICommand CopyNameCommand => new RelayCommand<string>(CopyName_Executed);
    public ICommand DeleteNameCommand => new RelayCommand<string>(DeleteName_Executed);
    public ICommand SaveNameCommand => new RelayCommand<string>(SaveName_Executed);
    public ICommand RefreshNameServerCommand => new RelayCommand<string>(RefreshNameServer_Executed);
    #endregion [Public Commands Connect ==> Tbl78Name]

    #region [Public Methods Connect ==> Tbl78Name]
    private void AddName_Executed(string? parm)
    {
        NameStartEdit();
        NameStartNew();
        Tbl78NamesList ??= new ObservableCollection<Tbl78Name>();
        Tbl78NamesList.Insert(index: 0, item: new Tbl78Name { NameName = FiSpeciesSelected.FiSpeciesFullName });

        RefreshNameItems();
    }
    private async void CopyName_Executed(string? parm)
    {
        if (NameSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        NameStartEdit();
        NameStartNew();
        Tbl78NamesList ??= new ObservableCollection<Tbl78Name>();
        Tbl78NamesList = await _dataService.CopyName(NameSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshNameItems();
    }
    private async void DeleteName_Executed(string? parm)
    {
        if (NameSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteName(NameSelected);
        if (!await ret)
        {
            return;
        }

        Tbl78NamesList = _dataService.GetTbl78NamesCollectionOrderByNameNameFromFiSpeciesId(NameSelected.FiSpeciesId);
      
        NameDataSetCount = Tbl78NamesList.Count;

        RefreshNameItems();
    }

    private async void SaveName_Executed(string? parm)
    {
        if (NameSelected != null && string.IsNullOrEmpty(NameSelected.NameName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl78NamesList != null)
        {
            var indx = Tbl78NamesList.IndexOf(Tbl78NamesList.First(t =>
                NameSelected != null && t.NameName == NameSelected.NameName));

            if (NameSelected != null)
            {
                if (FiSpeciesSelected != null)
                {

                    NameSelected.FiSpeciesId = FiSpeciesSelected.FiSpeciesId;

                    var plantaeRegnum = _dataService.GetPlSpeciesSingleByPlSpeciesName("Plantae#Regnum#");

                    NameSelected.PlSpeciesId = plantaeRegnum.PlSpeciesId;


                    var ret = _dataService.SaveName(NameSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (NameSelected.NameId == 0) //new
                    {
                        Tbl78NamesList = await _dataService.GetLastDatasetInTbl78Names();
                        RefreshNameItems();
                    }
                    else
                    {
                        Tbl78NamesList = _dataService.GetTbl78NamesCollectionOrderByNameNameFromFiSpeciesId(NameSelected.FiSpeciesId);
                        //   Index Position ?
                        if (indx < Tbl78NamesList.Count)
                        {
                            NameItems.Clear();
                            foreach (var item in Tbl78NamesList)
                            {
                                NameItems.Add(item);
                            }

                            NameSelected = Tbl78NamesList[indx]; //Index
                        }
                    }
                }
            }
        }
        NameDataSetCount = Tbl78NamesList!.Count;
        NameCancelEditsAsync();

    }

    private void RefreshNameServer_Executed(string? parm)
    {
        Tbl78NamesList ??= new ObservableCollection<Tbl78Name>();
        Tbl78NamesList = _dataService.GetTbl78NamesCollectionOrderByNameNameFromFiSpeciesId(FiSpeciesSelected.FiSpeciesId);

        NameDataSetCount = Tbl78NamesList.Count;

        RefreshNameItems();
    }
    public void NameStartEdit() => IsInEdit = true;
    public void NameStartModify() => IsModified = true;
    public void NameStartNew() => IsNewName = true;
    public event EventHandler AddNewNameCanceled = null!;
    public void NameCancelEditsAsync()
    {
        if (IsNewName)
        {
            AddNewNameCanceled?.Invoke(this, EventArgs.Empty);
            IsInEdit = false;
            IsNewName = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Public Methods Connect ==> Tbl78Name]



    //    Part 5    


    #region [Public Commands Connect ==> Tbl81Image]
    public ICommand AddImageCommand => new RelayCommand<string>(AddImage_Executed);
    public ICommand CopyImageCommand => new RelayCommand<string>(CopyImage_Executed);
    public ICommand DeleteImageCommand => new RelayCommand<string>(DeleteImage_Executed);
    public ICommand SaveImageCommand => new RelayCommand<string>(SaveImage_Executed);
    public ICommand ChangeImageCommand => new RelayCommand<string>(OpenFileDialog);
    public ICommand RefreshImageServerCommand => new RelayCommand<string>(RefreshImageServer_Executed);

   #endregion [Public Commands Connect ==> Tbl81Image]

    #region [Public Methods Connect ==> Tbl81Image]
    private void AddImage_Executed(string? parm)
    {
        ImageStartEdit();
        ImageStartNew();
        Tbl81ImagesList ??= new ObservableCollection<Tbl81Image>();
        Tbl81ImagesList.Insert(0, new Tbl81Image { Info = FiSpeciesSelected.FiSpeciesFullName });

        //New Image search
        OpenFileDialog(parm);
        RefreshImageItems();
    }
    private async void CopyImage_Executed(string? s)
    {
        //New Image search
         OpenFileDialog(s);
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
    private async void DeleteImage_Executed(string? s)
    {
        if (ImageSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        var ret = _dataService.DeleteImage(ImageSelected);
        if (!await ret)
        {
            return;
        }

        Tbl81ImagesList = _dataService.GetTbl81ImagesCollectionOrderByInfoFromFiSpeciesId(ImageSelected.FiSpeciesId);

        RefreshImageItems();
    }
    private async void SaveImage_Executed(string? s)
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
        ImageSelected ??= Tbl81ImagesList[0];
        var indx = Tbl81ImagesList.IndexOf(Tbl81ImagesList.First(t =>
            t.Info == ImageSelected.Info));

        ImageSelected.FiSpeciesId = FiSpeciesSelected.FiSpeciesId;

        var plantaeRegnum = _dataService.GetPlSpeciesSingleByPlSpeciesName("Plantae#Regnum#");

        ImageSelected.PlSpeciesId = plantaeRegnum.PlSpeciesId;


        var ret = _dataService.SaveImage(ImageSelected, SelectedPath, SelectedFilestream);
        if (!await ret)
        {
            return;
        }

        if (ImageSelected.ImageId == 0) //new
        {
            Tbl81ImagesList = await _dataService.GetLastDatasetInTbl81Images();
            RefreshImageItems();
        }
        else
        {
            Tbl81ImagesList = _dataService.GetTbl81ImagesCollectionOrderByInfoFromFiSpeciesId(ImageSelected.FiSpeciesId);
            //   Index Position ?
            if (indx < Tbl81ImagesList.Count)
            {
                ImageItems.Clear();
                foreach (var item in Tbl81ImagesList)
                {
                    ImageItems.Add(item);
                }

                ImageSelected = Tbl81ImagesList[indx];  //Index
            }
        }
        ImageCancelEditsAsync();
    }
    private void RefreshImageServer_Executed(string? s)
    {
        Tbl81ImagesList ??= new ObservableCollection<Tbl81Image>();
        Tbl81ImagesList = _dataService.GetTbl81ImagesCollectionOrderByInfoFromFiSpeciesId(FiSpeciesSelected.FiSpeciesId);

        ImageDataSetCount = Tbl81ImagesList.Count;

        RefreshImageItems();
    }
    private async void OpenFileDialog(string? s)
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

    #endregion [Public Methods  Connect ==> Tbl81Image]                                                                                               


    //  Part 6    


    #region [Public Commands Connect ==> Tbl84Synonym]
    public ICommand AddSynonymCommand => new RelayCommand<string>(AddSynonym_Executed);
    public ICommand CopySynonymCommand => new RelayCommand<string>(CopySynonym_Executed);
    public ICommand DeleteSynonymCommand => new RelayCommand<string>(DeleteSynonym_Executed);
    public ICommand SaveSynonymCommand => new RelayCommand<string>(SaveSynonym_Executed);
    public ICommand RefreshSynonymServerCommand => new RelayCommand<string>(RefreshSynonymServer_Executed);

    #endregion [Public Commands Connect ==> Tbl84Synonym]

    #region [Public Methods Connect ==> Tbl84Synonym]
    private void AddSynonym_Executed(string? s)
    {
        SynonymStartEdit();
        SynonymStartNew();
        Tbl84SynonymsList ??= new ObservableCollection<Tbl84Synonym>();

        Tbl84SynonymsList.Insert(0, new Tbl84Synonym { SynonymName = FiSpeciesSelected.FiSpeciesFullName });

        RefreshSynonymItems();

    }
    private async void CopySynonym_Executed(string? s)
    {
        if (SynonymSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        SynonymStartEdit();
        Tbl84SynonymsList ??= new ObservableCollection<Tbl84Synonym>();
        Tbl84SynonymsList = await _dataService.CopySynonym(SynonymSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

        RefreshSynonymItems();
    }
    private async void DeleteSynonym_Executed(string? s)
    {
        if (SynonymSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        var ret = _dataService.DeleteSynonym(SynonymSelected);
        if (!await ret)
        {
            return;
        }

        Tbl84SynonymsList = _dataService.GetTbl84SynonymsCollectionOrderBySynonymNameFromFiSpeciesId(SynonymSelected.FiSpeciesId);

        RefreshSynonymItems();
    }
    private async void SaveSynonym_Executed(string? s)
    {
        if (string.IsNullOrEmpty(SynonymSelected.SynonymName))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (SynonymSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        SynonymSelected ??= Tbl84SynonymsList[0];
        var indx = Tbl84SynonymsList.IndexOf(Tbl84SynonymsList.First(t =>
            t.SynonymName == SynonymSelected.SynonymName));

        SynonymSelected.FiSpeciesId = FiSpeciesSelected.FiSpeciesId;

        var plantaeRegnum = _dataService.GetPlSpeciesSingleByPlSpeciesName("Plantae#Regnum#");

        SynonymSelected.PlSpeciesId = plantaeRegnum.PlSpeciesId;

        var ret = _dataService.SaveSynonym(SynonymSelected);
        if (!await ret)
        {
            return;
        }

        if (SynonymSelected.SynonymId == 0) //new
        {
            Tbl84SynonymsList = await _dataService.GetLastDatasetInTbl84Synonyms();
            RefreshSynonymItems();
        }
        else
        {
            Tbl84SynonymsList = _dataService.GetTbl84SynonymsCollectionOrderBySynonymNameFromFiSpeciesId(SynonymSelected.FiSpeciesId);
            //   Index Position ?
            if (indx < Tbl84SynonymsList.Count)
            {
                SynonymItems.Clear();
                foreach (var item in Tbl84SynonymsList)
                {
                    SynonymItems.Add(item);
                }

                SynonymSelected = Tbl84SynonymsList[indx];  //Index
            }
        }
        SynonymCancelEditsAsync();
    }
    private void RefreshSynonymServer_Executed(string? s)
    {
        Tbl84SynonymsList ??= new ObservableCollection<Tbl84Synonym>();
        Tbl84SynonymsList = _dataService.GetTbl84SynonymsCollectionOrderBySynonymNameFromFiSpeciesId(FiSpeciesSelected.FiSpeciesId);

        SynonymDataSetCount = Tbl84SynonymsList.Count;

        RefreshSynonymItems();
    }
    public void SynonymStartEdit() => IsInEdit = true;
    public void SynonymStartModify() => IsModified = true;
    public void SynonymStartNew() => IsNewSynonym = true;
    public event EventHandler AddNewSynonymCanceled = null!;
    public void SynonymCancelEditsAsync()
    {
        if (IsNewSynonym)
        {
            AddNewSynonymCanceled?.Invoke(this, EventArgs.Empty);
            IsInEdit = false;
            IsNewSynonym = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }

    #endregion [Public Methods Connect ==> Tbl84Synonym]



    //    Part 7    


    #region [Public Commands Connect ==> Tbl87Geographic]
    public ICommand AddGeographicCommand => new RelayCommand<string>(AddGeographic_Executed);
    public ICommand CopyGeographicCommand => new RelayCommand<string>(CopyGeographic_Executed);
    public ICommand DeleteGeographicCommand => new RelayCommand<string>(DeleteGeographic_Executed);
    public ICommand SaveGeographicCommand => new RelayCommand<string>(SaveGeographic_Executed);
    public ICommand RefreshGeographicServerCommand => new RelayCommand<string>(RefreshGeographicServer_Executed);
    #endregion [Public Commands Connect ==> Tbl87Geographic]

    #region [Public Methods Connect ==> Tbl87Geographic]
    private void AddGeographic_Executed(string? s)
    {
        GeographicStartEdit();
        GeographicStartNew();
        Tbl87GeographicsList ??= new ObservableCollection<Tbl87Geographic>();
        Tbl87GeographicsList.Insert(0, new Tbl87Geographic { Info = FiSpeciesSelected.FiSpeciesFullName });

        RefreshGeographicItems();
    }
    private async void CopyGeographic_Executed(string? s)
    {
        if (GeographicSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        GeographicStartEdit();
        GeographicStartNew();
        Tbl87GeographicsList ??= new ObservableCollection<Tbl87Geographic>();
        Tbl87GeographicsList = await _dataService.CopyGeographic(GeographicSelected);
        // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment
        RefreshGeographicItems();
    }
    private async void DeleteGeographic_Executed(string? s)
    {
        if (GeographicSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        var ret = _dataService.DeleteGeographic(GeographicSelected);
        if (!await ret)
        {
            return;
        }

        Tbl87GeographicsList = _dataService.GetTbl87GeographicsCollectionOrderByInfoFromFiSpeciesId(GeographicSelected.FiSpeciesId);

        RefreshGeographicItems();
    }
    private async void SaveGeographic_Executed(string? s)
    {
        if (GeographicSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        GeographicSelected ??= Tbl87GeographicsList[0];
        var indx = Tbl87GeographicsList.IndexOf(Tbl87GeographicsList.First(t =>
            t.Info == GeographicSelected.Info));

        GeographicSelected.FiSpeciesId = FiSpeciesSelected.FiSpeciesId;

        var plantaeRegnum = _dataService.GetPlSpeciesSingleByPlSpeciesName("Plantae#Regnum#");

        GeographicSelected.PlSpeciesId = plantaeRegnum.PlSpeciesId;


        var ret = _dataService.SaveGeographic(GeographicSelected);
        if (!await ret)
        {
            return;
        }

        if (GeographicSelected.GeographicId == 0) //new
        {
            Tbl87GeographicsList = await _dataService.GetLastDatasetInTbl87Geographics();
            RefreshGeographicItems();
        }
        else
        {
            Tbl87GeographicsList = _dataService.GetTbl87GeographicsCollectionOrderByInfoFromFiSpeciesId(GeographicSelected.FiSpeciesId);
            //   Index Position ?
            if (indx < Tbl87GeographicsList.Count)
            {
                GeographicItems.Clear();
                foreach (var item in Tbl87GeographicsList)
                {
                    GeographicItems.Add(item);
                }

                GeographicSelected = Tbl87GeographicsList[indx];  //Index
            }
        }
        GeographicCancelEditsAsync();
    }
    private void RefreshGeographicServer_Executed(string? s)
    {
        Tbl87GeographicsList ??= new ObservableCollection<Tbl87Geographic>();
        Tbl87GeographicsList = _dataService.GetTbl87GeographicsCollectionOrderByInfoFromFiSpeciesId(FiSpeciesSelected.FiSpeciesId);

        GeographicDataSetCount = Tbl87GeographicsList.Count;

        RefreshGeographicItems();
    }
    public void GeographicStartEdit() => IsInEdit = true;
    public void GeographicStartModify() => IsModified = true;
    public void GeographicStartNew() => IsNewGeographic = true;
    public event EventHandler AddNewGeographicCanceled = null!;
    public void GeographicCancelEditsAsync()
    {
        if (IsNewGeographic)
        {
            AddNewGeographicCanceled?.Invoke(this, EventArgs.Empty);
            IsInEdit = false;
            IsNewGeographic = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Public Methods  Connect ==> Tbl87Geographics]



    //    Part 8    


    #region [Commands FiSpecies ==> Tbl90Reference Author]
    public ICommand AddReferenceAuthorCommand => new RelayCommand<string>(AddReferenceAuthor_Executed);
    public ICommand CopyReferenceAuthorCommand => new RelayCommand<string>(CopyReferenceAuthor_Executed);
    public ICommand DeleteReferenceAuthorCommand => new RelayCommand<string>(DeleteReferenceAuthor_Executed);
    public ICommand SaveReferenceAuthorCommand => new RelayCommand<string>(SaveReferenceAuthor_Executed);
    public ICommand RefreshReferenceAuthorServerCommand => new RelayCommand<string>(RefreshReferenceAuthorServer_Executed);
    #endregion [Commands FiSpecies ==> Tbl90Reference Author]                

    #region [Methods FiSpecies ==> Tbl90Reference Author]

    private void AddReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", FiSpeciesId = FiSpeciesSelected!.FiSpeciesId });

        ReferenceAuthorDataSetCount = Tbl90ReferenceAuthorsList.Count;

        ReferenceAuthorItems.Clear();
        foreach (var item in Tbl90ReferenceAuthorsList)
        {
            ReferenceAuthorItems.Add(item);
        }
        ReferenceAuthorSelected = ReferenceAuthorItems.First();
    }

    private async void CopyReferenceAuthor_Executed(string? parm)
    {
        ReferenceAuthorStartEdit();
        ReferenceAuthorStartNew();
        ReferenceAuthorSelected.FiSpeciesId = FiSpeciesSelected.FiSpeciesId; //combo vorbelegen

        Tbl90ReferenceAuthorsList = await _dataService.CopyReferenceFiSpecies(ReferenceAuthorSelected, "Author");

        ReferenceAuthorDataSetCount = Tbl90ReferenceAuthorsList.Count;
        RefreshReferenceAuthorItems();
    }

    private async void DeleteReferenceAuthor_Executed(string? parm)
    {
        if (ReferenceAuthorSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteReference(ReferenceAuthorSelected);
        if (!await ret)
        {
            return;
        }

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromFiSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.FiSpeciesId);
        ReferenceAuthorDataSetCount = Tbl90ReferenceAuthorsList.Count;
        RefreshReferenceAuthorItems();
    }

    private async void SaveReferenceAuthor_Executed(string? parm)
    {
        if (ReferenceAuthorSelected != null && string.IsNullOrEmpty(ReferenceAuthorSelected.Info))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl90ReferenceAuthorsList != null)
        {
            ReferenceAuthorSelected ??= Tbl90ReferenceAuthorsList[0];
            var indx = Tbl90ReferenceAuthorsList.IndexOf(Tbl90ReferenceAuthorsList.First(t =>
                t.Info == ReferenceAuthorSelected.Info));

            if (FiSpeciesSelected != null)
            {
                ReferenceAuthorSelected.FiSpeciesId = FiSpeciesSelected.FiSpeciesId;
            }

            var ret = _dataService.SaveReference(ReferenceAuthorSelected, "Author");
            if (!await ret)
            {
                return;
            }

            if (ReferenceAuthorSelected.ReferenceId == 0) //new
            {
                Tbl90ReferenceAuthorsList = await _dataService.GetLastDatasetInTbl90References();
                RefreshReferenceAuthorItems();
            }
            else
            {
                Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromFiSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNull(ReferenceAuthorSelected.FiSpeciesId);
                //   Index Position ?
                if (indx < Tbl90ReferenceAuthorsList.Count)
                {
                    ReferenceAuthorItems.Clear();
                    foreach (var item in Tbl90ReferenceAuthorsList)
                    {
                        ReferenceAuthorItems.Add(item);
                    }
                    ReferenceAuthorSelected = Tbl90ReferenceAuthorsList[indx];  //Index
                }
            }
        }
        ReferenceAuthorCancelEditsAsync();
    }

    private void RefreshReferenceAuthorServer_Executed(string? parm)
    {
        Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

        Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromFiSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNull(FiSpeciesSelected.FiSpeciesId);

        ReferenceAuthorDataSetCount = Tbl90ReferenceAuthorsList.Count;
        RefreshReferenceAuthorItems();
    }

    public void ReferenceAuthorStartEdit() => IsInEdit = true;
    public void ReferenceAuthorStartModify() => IsModified = true;
    public void ReferenceAuthorStartNew() => IsNewReferenceAuthor = true;
    public event EventHandler AddNewReferenceAuthorCanceled = null!;
    public void ReferenceAuthorCancelEditsAsync()
    {
        if (IsNewReferenceAuthor)
        {
            IsInEdit = false;
            AddNewReferenceAuthorCanceled?.Invoke(this, EventArgs.Empty);
            IsNewReferenceAuthor = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Methods FiSpecies ==> Tbl90Reference Author]                   

    #region [Commands FiSpecies ==> Tbl90Reference Source]  
    public ICommand AddReferenceSourceCommand => new RelayCommand<string>(AddReferenceSource_Executed);
    public ICommand CopyReferenceSourceCommand => new RelayCommand<string>(CopyReferenceSource_Executed);
    public ICommand DeleteReferenceSourceCommand => new RelayCommand<string>(DeleteReferenceSource_Executed);
    public ICommand SaveReferenceSourceCommand => new RelayCommand<string>(SaveReferenceSource_Executed);
    public ICommand RefreshReferenceSourceServerCommand => new RelayCommand<string>(RefreshReferenceSourceServer_Executed);
    #endregion [Commands FiSpecies ==> Tbl90Reference Source]         

    #region [Methods FiSpecies ==> Tbl90Reference Source]      

    private void AddReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList.Insert(index: 0, item: new Tbl90Reference { Info = "New", FiSpeciesId = FiSpeciesSelected!.FiSpeciesId });
        ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
        ReferenceSourceItems.Clear();
        foreach (var item in Tbl90ReferenceSourcesList)
        {
            ReferenceSourceItems.Add(item);
        }
        ReferenceSourceSelected = ReferenceSourceItems.First();
    }

    private async void CopyReferenceSource_Executed(string? parm)
    {
        ReferenceSourceStartEdit();
        ReferenceSourceStartNew();
        if (FiSpeciesSelected != null)
        {
            ReferenceSourceSelected.FiSpeciesId = FiSpeciesSelected.FiSpeciesId; //combo vorbelegen
        }
        Tbl90ReferenceSourcesList = await _dataService.CopyReferenceFiSpecies(ReferenceSourceSelected, "Source");
        ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
        RefreshReferenceSourceItems();
    }

    private async void DeleteReferenceSource_Executed(string? parm)
    {
        if (ReferenceSourceSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }

        var ret = _dataService.DeleteReference(ReferenceSourceSelected);
        if (!await ret)
        {
            return;
        }

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.FiSpeciesId);
        ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
        RefreshReferenceSourceItems();
    }

    private async void SaveReferenceSource_Executed(string? parm)
    {
        if (ReferenceSourceSelected != null && string.IsNullOrEmpty(ReferenceSourceSelected.Info))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl90ReferenceSourcesList != null)
        {
            ReferenceSourceSelected ??= Tbl90ReferenceSourcesList[0];
            var indx = Tbl90ReferenceSourcesList.IndexOf(Tbl90ReferenceSourcesList.First(t =>
                t.Info == ReferenceSourceSelected.Info));

            if (FiSpeciesSelected != null)
            {
                ReferenceSourceSelected.FiSpeciesId = FiSpeciesSelected.FiSpeciesId;
            }
            var ret = _dataService.SaveReference(ReferenceSourceSelected, "Source");
            if (!await ret)
            {
                return;
            }

            if (ReferenceSourceSelected.ReferenceId == 0) //new
            {
                Tbl90ReferenceSourcesList = await _dataService.GetLastDatasetInTbl90References();
                RefreshReferenceSourceItems();
            }
            else
            {
                Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(ReferenceSourceSelected.FiSpeciesId);
                //   Index Position ?
                if (indx < Tbl90ReferenceSourcesList.Count)
                {
                    ReferenceSourceItems.Clear();
                    foreach (var item in Tbl90ReferenceSourcesList)
                    {
                        ReferenceSourceItems.Add(item);
                    }
                    ReferenceSourceSelected = Tbl90ReferenceSourcesList[indx];  //Index
                }
            }
        }
        ReferenceSourceCancelEditsAsync();
    }

    private void RefreshReferenceSourceServer_Executed(string? parm)
    {
        Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

        Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(FiSpeciesSelected.FiSpeciesId);

        ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
        RefreshReferenceSourceItems();
    }

    public void ReferenceSourceStartEdit() => IsInEdit = true;
    public void ReferenceSourceStartModify() => IsModified = true;
    public void ReferenceSourceStartNew() => IsNewReferenceSource = true;
    public event EventHandler AddNewReferenceSourceCanceled = null!;
    public void ReferenceSourceCancelEditsAsync()
    {
        if (IsNewReferenceSource)
        {
            IsInEdit = false;
            AddNewReferenceSourceCanceled?.Invoke(this, EventArgs.Empty);
            IsNewReferenceSource = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Methods FiSpecies ==> Tbl90Reference Source]           

    #region [Commands FiSpecies ==> Tbl90Reference Expert]       
    public ICommand AddReferenceExpertCommand => new RelayCommand<string>(AddReferenceExpert_Executed);
    public ICommand CopyReferenceExpertCommand => new RelayCommand<string>(CopyReferenceExpert_Executed);
    public ICommand DeleteReferenceExpertCommand => new RelayCommand<string>(DeleteReferenceExpert_Executed);
    public ICommand SaveReferenceExpertCommand => new RelayCommand<string>(SaveReferenceExpert_Executed);
    public ICommand RefreshReferenceExpertServerCommand => new RelayCommand<string>(RefreshReferenceExpertServer_Executed);
    #endregion [Commands FiSpecies ==> Tbl90Reference Expert]                    

    #region [Methods FiSpecies ==> Tbl90Reference Expert]                 

    private void AddReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList.Insert(index: 0, item: new Tbl90Reference { Info = "New", FiSpeciesId = FiSpeciesSelected!.FiSpeciesId });
        ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
        ReferenceExpertItems.Clear();
        foreach (var item in Tbl90ReferenceExpertsList)
        {
            ReferenceExpertItems.Add(item);
        }
        ReferenceExpertSelected = ReferenceExpertItems.First();
    }

    private async void CopyReferenceExpert_Executed(string? parm)
    {
        ReferenceExpertStartEdit();
        ReferenceExpertStartNew();
        if (FiSpeciesSelected != null)
        {
            ReferenceExpertSelected.FiSpeciesId = FiSpeciesSelected.FiSpeciesId; //combo vorbelegen
        }
        Tbl90ReferenceExpertsList = await _dataService.CopyReferenceFiSpecies(ReferenceExpertSelected, "Expert");
        ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
        RefreshReferenceExpertItems();
    }

    private async void DeleteReferenceExpert_Executed(string? parm)
    {
        if (ReferenceExpertSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteReference(ReferenceExpertSelected);
        if (!await ret)
        {
            return;
        }

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.FiSpeciesId);
        ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
        RefreshReferenceExpertItems();
    }

    private async void SaveReferenceExpert_Executed(string? parm)
    {
        if (ReferenceExpertSelected != null && string.IsNullOrEmpty(ReferenceExpertSelected.Info))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }

        if (Tbl90ReferenceExpertsList != null)
        {
            ReferenceExpertSelected ??= Tbl90ReferenceExpertsList[0];

            var indx = Tbl90ReferenceExpertsList.IndexOf(Tbl90ReferenceExpertsList.First(t =>
                t.Info == ReferenceExpertSelected.Info));
            if (FiSpeciesSelected != null)
            {
                ReferenceExpertSelected.FiSpeciesId = FiSpeciesSelected.FiSpeciesId;
            }
            var ret = _dataService.SaveReference(ReferenceExpertSelected, "Expert");
            if (!await ret)
            {
                return;
            }

            if (ReferenceExpertSelected.ReferenceId == 0) //new
            {
                Tbl90ReferenceExpertsList = await _dataService.GetLastDatasetInTbl90References();
                RefreshReferenceExpertItems();
            }
            else
            {
                Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(ReferenceExpertSelected.FiSpeciesId);
                //   Index Position ?
                if (indx < Tbl90ReferenceExpertsList.Count)
                {
                    ReferenceExpertItems.Clear();
                    foreach (var item in Tbl90ReferenceExpertsList)
                    {
                        ReferenceExpertItems.Add(item);
                    }
                    ReferenceExpertSelected = Tbl90ReferenceExpertsList[indx];  //Index
                }
            }
        }
        ReferenceExpertCancelEditsAsync();
    }

    private void RefreshReferenceExpertServer_Executed(string? parm)
    {
        Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

        Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(FiSpeciesSelected.FiSpeciesId);

        ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
        RefreshReferenceExpertItems();
    }

    public void ReferenceExpertStartEdit() => IsInEdit = true;
    public void ReferenceExpertStartModify() => IsModified = true;
    public void ReferenceExpertStartNew() => IsNewReferenceExpert = true;
    public event EventHandler AddNewReferenceExpertCanceled = null!;
    public void ReferenceExpertCancelEditsAsync()
    {
        if (IsNewReferenceExpert)
        {
            IsInEdit = false;
            AddNewReferenceExpertCanceled?.Invoke(this, EventArgs.Empty);
            IsNewReferenceExpert = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Methods FiSpecies ==> Tbl90Reference Expert]                                 

    #region [Commands FiSpecies ==> Tbl93Comments]         
    public ICommand AddCommentCommand => new RelayCommand<string>(AddComment_Executed);
    public ICommand CopyCommentCommand => new RelayCommand<string>(CopyComment_Executed);
    public ICommand DeleteCommentCommand => new RelayCommand<string>(DeleteComment_Executed);
    public ICommand SaveCommentCommand => new RelayCommand<string>(SaveComment_Executed);
    public ICommand RefreshCommentServerCommand => new RelayCommand<string>(RefreshCommentServer_Executed);
    #endregion [Commands FiSpecies ==> Tbl93Comments]           

    #region [Methods FiSpecies ==> Tbl93Comments]        

    private void AddComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = "New", FiSpeciesId = FiSpeciesSelected!.FiSpeciesId });

        CommentDataSetCount = Tbl93CommentsList.Count;
        CommentItems.Clear();
        foreach (var item in Tbl93CommentsList)
        {
            CommentItems.Add(item);
        }
        CommentSelected = CommentItems.First();
    }

    private async void CopyComment_Executed(string? parm)
    {
        CommentStartEdit();
        CommentStartNew();
        if (FiSpeciesSelected != null)
        {
            CommentSelected.FiSpeciesId = FiSpeciesSelected.FiSpeciesId;  //combo vorbelegen
        }
        Tbl93CommentsList = await _dataService.CopyComment(CommentSelected);

        CommentDataSetCount = Tbl93CommentsList.Count;
        RefreshCommentItems();
    }

    private async void DeleteComment_Executed(string? parm)
    {
        if (CommentSelected == null)
        {
            await _allDialogs.NoDatasetSelectedWarnMessageDialogAsync();
            return;
        }
        var ret = _dataService.DeleteComment(CommentSelected);
        if (!await ret)
        {
            return;
        }

        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromFiSpeciesId(CommentSelected.FiSpeciesId);
        CommentDataSetCount = Tbl93CommentsList.Count;
        RefreshCommentItems();
    }

    private async void SaveComment_Executed(string? parm)
    {
        if (CommentSelected != null && string.IsNullOrEmpty(CommentSelected.Info))
        {
            await _allDialogs.NameRequiredWarnMessageDialogAsync();
            return;
        }
        if (Tbl93CommentsList != null)
        {
            var indx = Tbl93CommentsList.IndexOf(Tbl93CommentsList.First(t =>
             CommentSelected != null && t.Info == CommentSelected.Info));
            if (CommentSelected != null)
            {
                if (FiSpeciesSelected != null)
                {
                    CommentSelected.FiSpeciesId = FiSpeciesSelected.FiSpeciesId;

                    var ret = _dataService.SaveComment(CommentSelected);
                    if (!await ret)
                    {
                        return;
                    }

                    if (CommentSelected.CommentId == 0) //new
                    {
                        Tbl93CommentsList = await _dataService.GetLastDatasetInTbl93Comments();
                        RefreshCommentItems();
                    }
                    else
                    {
                        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromFiSpeciesId(FiSpeciesSelected.FiSpeciesId);
                        //   Index Position ?
                        if (indx < Tbl93CommentsList.Count)
                        {
                            CommentItems.Clear();
                            foreach (var item in Tbl93CommentsList)
                            {
                                CommentItems.Add(item);
                            }

                            CommentSelected = Tbl93CommentsList[indx];  //Index
                        }
                    }
                }
            }
        }
        CommentCancelEditsAsync();
    }

    private void RefreshCommentServer_Executed(string? parm)
    {
        Tbl93CommentsList = _dataService.GetTbl93CommentsCollectionOrderByInfoFromFiSpeciesId(FiSpeciesSelected.FiSpeciesId);

        CommentDataSetCount = Tbl93CommentsList.Count;
        RefreshCommentItems();
    }

    public void CommentStartEdit() => IsInEdit = true;
    public void CommentStartModify() => IsModified = true;
    public void CommentStartNew() => IsNewComment = true;
    public event EventHandler AddNewCommentCanceled = null!;
    public void CommentCancelEditsAsync()
    {
        if (IsNewComment)
        {
            IsInEdit = false;
            AddNewCommentCanceled?.Invoke(this, EventArgs.Empty);
            IsNewComment = false;
        }
        else
        {
            IsInEdit = false;
            IsModified = false;
        }
    }
    #endregion [Methods FiSpecies ==> Tbl93Comments]                             


    //    Part 9    



    //    Part 10    


    #region "Public Commands to open Main and Detail TabItems"

    private int _selectedMainDetailTabIndex;
    private int _selectedMainDetailRefTabIndex;


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
                if (FiSpeciesSelected != null)
                {
                    IsLoading = true;
                    GenusStartModify();
                    Tbl63InfratribussesAllList ??= new ObservableCollection<Tbl63Infratribus>();
                    Tbl63InfratribussesAllList = _dataService.GetTbl63InfratribussesCollectionOrderByInfratribusName();
                    Tbl66GenussesList ??= new ObservableCollection<Tbl66Genus>();
                    Tbl66GenussesList = _dataService.GetTbl66GenussesCollectionOrderByGenusNameFromGenusId(FiSpeciesSelected.GenusId);

                    GenusDataSetCount = Tbl66GenussesList.Count;
                    RefreshGenusItems();
                    IsLoading = false;
                }
            }
            if (_selectedMainDetailTabIndex == 1)
            {
                if (FiSpeciesSelected != null)
                {
                    IsLoading = true;
                    SpeciesgroupStartModify();
                    Tbl68SpeciesgroupsList ??= new ObservableCollection<Tbl68Speciesgroup>();
                    Tbl68SpeciesgroupsList = _dataService.GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroupFromSpeciesgroupId(FiSpeciesSelected.SpeciesgroupId);

                    SpeciesgroupDataSetCount = Tbl68SpeciesgroupsList.Count;
                    RefreshSpeciesgroupItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 2)
            {
            }

            if (_selectedMainDetailTabIndex == 3)
            {
                if (FiSpeciesSelected != null)
                {
                    IsLoading = true;
                    NameStartModify();
                    Tbl78NamesList ??= new ObservableCollection<Tbl78Name>();
                    Tbl78NamesList = _dataService.GetTbl78NamesCollectionOrderByNameNameFromFiSpeciesId(FiSpeciesSelected.FiSpeciesId);
                    NameDataSetCount = Tbl78NamesList.Count;
                    RefreshNameItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 4)
            {
                if (FiSpeciesSelected != null)
                {
                    IsLoading = true;
                    ImageStartModify();
                    Tbl81ImagesList ??= new ObservableCollection<Tbl81Image>();
                    Tbl81ImagesList = _dataService.GetTbl81ImagesCollectionOrderByInfoFromFiSpeciesId(FiSpeciesSelected.FiSpeciesId);
                    ImageDataSetCount = Tbl81ImagesList.Count;
                    RefreshImageItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailTabIndex == 5)
            {
                if (FiSpeciesSelected != null)
                {
                    IsLoading = true;
                    SynonymStartModify();
                    Tbl84SynonymsList ??= new ObservableCollection<Tbl84Synonym>();
                    Tbl84SynonymsList = _dataService.GetTbl84SynonymsCollectionOrderBySynonymNameFromFiSpeciesId(FiSpeciesSelected.FiSpeciesId);
                    SynonymDataSetCount = Tbl84SynonymsList.Count;
                    RefreshSynonymItems();
                    IsLoading = false;
                }
            }
            if (_selectedMainDetailTabIndex == 6)
            {
                if (FiSpeciesSelected != null)
                {
                    IsLoading = true;
                    GeographicStartModify();
                    TblCountriesAllList = _dataService.GetTblCountriesCollectionOrderByName();
                    Tbl87GeographicsList ??= new ObservableCollection<Tbl87Geographic>();
                    Tbl87GeographicsList = _dataService.GetTbl87GeographicsCollectionOrderByInfoFromFiSpeciesId(FiSpeciesSelected.FiSpeciesId);
                    GeographicDataSetCount = Tbl87GeographicsList.Count;
                    RefreshGeographicItems();
                    IsLoading = false;
                }
            }
            if (_selectedMainDetailTabIndex == 7)
            {
                if (FiSpeciesSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();
                    Tbl90ReferenceExpertsList =
                        _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(FiSpeciesSelected.FiSpeciesId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }
            if (_selectedMainDetailTabIndex == 8)
            {
                if (FiSpeciesSelected != null)
                {
                    IsLoading = true;
                    CommentStartModify();
                    Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();
                    Tbl93CommentsList =
                        _dataService.GetTbl93CommentsCollectionOrderByInfoFromFiSpeciesId(FiSpeciesSelected.FiSpeciesId);
                    CommentDataSetCount = Tbl93CommentsList.Count;
                    RefreshCommentItems();
                    IsLoading = false;
                }
            }
        }
    }
    public int SelectedMainDetailRefTabIndex
    {
        get => _selectedMainDetailRefTabIndex;
        set
        {
            if (value == _selectedMainDetailRefTabIndex)
            {
                return;
            }

            _selectedMainDetailRefTabIndex = value; OnPropertyChanged();

            if (_selectedMainDetailRefTabIndex == 0)
            {
                if (FiSpeciesSelected != null)
                {
                    IsLoading = true;
                    ReferenceExpertStartModify();
                    Tbl90ExpertsAllList ??= new ObservableCollection<Tbl90RefExpert>();
                    Tbl90ExpertsAllList = _dataService.GetTbl90RefExpertsCollectionOrderByRefExpertName();

                    Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();
                    Tbl90ReferenceExpertsList = _dataService.GetTbl90ReferenceExpertsCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(FiSpeciesSelected.FiSpeciesId);
                    ReferenceExpertDataSetCount = Tbl90ReferenceExpertsList.Count;
                    RefreshReferenceExpertItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 1)
            {
                if (FiSpeciesSelected != null)
                {
                    IsLoading = true;
                    ReferenceSourceStartModify();
                    Tbl90SourcesAllList ??= new ObservableCollection<Tbl90RefSource>();
                    Tbl90SourcesAllList = _dataService.GetTbl90RefSourcesCollectionOrderByRefSourceName();

                    Tbl90ReferenceSourcesList ??= new ObservableCollection<Tbl90Reference>();
                    Tbl90ReferenceSourcesList = _dataService.GetTbl90ReferenceSourcesCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(FiSpeciesSelected.FiSpeciesId);

                    ReferenceSourceDataSetCount = Tbl90ReferenceSourcesList.Count;
                    RefreshReferenceSourceItems();
                    IsLoading = false;
                }
            }

            if (_selectedMainDetailRefTabIndex == 2)
            {
                if (FiSpeciesSelected != null)
                {
                    IsLoading = true;
                    ReferenceAuthorStartModify();
                    Tbl90AuthorsAllList ??= new ObservableCollection<Tbl90RefAuthor>();
                    Tbl90AuthorsAllList = _dataService.GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();

                    Tbl90ReferenceAuthorsList ??= new ObservableCollection<Tbl90Reference>();
                    Tbl90ReferenceAuthorsList = _dataService.GetTbl90ReferenceAuthorsCollectionOrderByInfoFromFiSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNull(FiSpeciesSelected.FiSpeciesId);

                    ReferenceAuthorDataSetCount = Tbl90ReferenceAuthorsList.Count;
                    RefreshReferenceAuthorItems();
                    IsLoading = false;
                }
            }
        }
    }

    #endregion "Public Commands to open Main und Ref TabItems"


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

    //   public new event PropertyChangedEventHandler PropertyChanged;

    private byte[] _imageBuffer = null!;
    public byte[] ImageBuffer
    {
        get => _imageBuffer;
        set
        {
            _imageBuffer = value;
            using var loadImageTask = LoadImageAsync();
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
    private Tbl68Speciesgroup _speciesgroupSelected = null!;

    public Tbl68Speciesgroup SpeciesgroupSelected
    {
        get => _speciesgroupSelected;
        set => SetProperty(ref _speciesgroupSelected, value);
    }

    private Tbl69FiSpecies _fispeciesSelected = null!;

    public Tbl69FiSpecies FiSpeciesSelected
    {
        get => _fispeciesSelected;
        set => SetProperty(ref _fispeciesSelected, value);
    }


    private Tbl78Name _nameSelected = null!;

    public Tbl78Name NameSelected
    {
        get => _nameSelected;
        set => SetProperty(ref _nameSelected, value);
    }

    private Tbl81Image _imageSelected = null!;


    public Tbl81Image ImageSelected
    {
        get => _imageSelected;
        set
        {
            SetProperty(ref _imageSelected, value);
            // ImageSelected loose ImageFileStream
            if (_imageSelected == null || (_imageSelected == null && _imageSelected!.CountId == 0))
            {
                return;
            }

            _dataSet = _fileStreamList.Find(t => t.ImageCountId == _imageSelected.CountId);

            _imageSelected.Filestream = _dataSet.ImageFileStream;
            _selectedFilestream = _dataSet.ImageFileStream;
        }
    }

    private Tbl84Synonym _synonymSelected = null!;

    public Tbl84Synonym SynonymSelected
    {
        get => _synonymSelected;
        set => SetProperty(ref _synonymSelected, value);
    }

    private Tbl87Geographic _geographicSelected = null!;

    public Tbl87Geographic GeographicSelected
    {
        get => _geographicSelected;
        set => SetProperty(ref _geographicSelected, value);
    }

    private int _fiSpeciesDataSetCount;
    public int FiSpeciesDataSetCount
    {
        get => _fiSpeciesDataSetCount;
        set
        {
            _fiSpeciesDataSetCount = value; OnPropertyChanged(nameof(FiSpeciesDataSetCount));
        }
    }

    private int _genusDataSetCount;
    public int GenusDataSetCount
    {
        get => _genusDataSetCount;
        set
        {
            _genusDataSetCount = value; OnPropertyChanged(nameof(GenusDataSetCount));
        }
    }

    private int _nameDataSetCount;
    public int NameDataSetCount
    {
        get => _nameDataSetCount;
        set
        {
            _nameDataSetCount = value; OnPropertyChanged(nameof(NameDataSetCount));
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

    private int _synonymDataSetCount;
    public int SynonymDataSetCount
    {
        get => _synonymDataSetCount;
        set
        {
            _synonymDataSetCount = value; OnPropertyChanged(nameof(SynonymDataSetCount));
        }
    }

    private int _geographicDataSetCount;
    public int GeographicDataSetCount
    {
        get => _geographicDataSetCount;
        set
        {
            _geographicDataSetCount = value; OnPropertyChanged(nameof(GeographicDataSetCount));
        }
    }

    private Tbl90Reference _referenceExpertSelected = null!;

    public Tbl90Reference ReferenceExpertSelected
    {
        get => _referenceExpertSelected;
        set => SetProperty(ref _referenceExpertSelected, value);
    }

    private Tbl90Reference _referenceSourceSelected = null!;

    public Tbl90Reference ReferenceSourceSelected
    {
        get => _referenceSourceSelected;
        set => SetProperty(ref _referenceSourceSelected, value);
    }

    private Tbl90Reference _referenceAuthorSelected = null!;

    public Tbl90Reference ReferenceAuthorSelected
    {
        get => _referenceAuthorSelected;
        set => SetProperty(ref _referenceAuthorSelected, value);
    }

    private Tbl93Comment _commentSelected = null!;

    public Tbl93Comment CommentSelected
    {
        get => _commentSelected;
        set => SetProperty(ref _commentSelected, value);
    }


    private int _referenceExpertDataSetCount;
    public int ReferenceExpertDataSetCount
    {
        get => _referenceExpertDataSetCount;
        set
        {
            _referenceExpertDataSetCount = value; OnPropertyChanged(nameof(ReferenceExpertDataSetCount));
        }
    }
    private int _referenceSourceDataSetCount;
    public int ReferenceSourceDataSetCount
    {
        get => _referenceSourceDataSetCount;
        set
        {
            _referenceSourceDataSetCount = value; OnPropertyChanged(nameof(ReferenceSourceDataSetCount));
        }
    }
    private int _referenceAuthorDataSetCount;
    public int ReferenceAuthorDataSetCount
    {
        get => _referenceAuthorDataSetCount;
        set
        {
            _referenceAuthorDataSetCount = value; OnPropertyChanged(nameof(ReferenceAuthorDataSetCount));
        }
    }

    private int _commentDataSetCount;
    public int CommentDataSetCount
    {
        get => _commentDataSetCount;
        set
        {
            _commentDataSetCount = value; OnPropertyChanged(nameof(CommentDataSetCount));
        }
    }

    private int _speciesgroupDataSetCount;
    public int SpeciesgroupDataSetCount
    {
        get => _speciesgroupDataSetCount;
        set
        {
            _speciesgroupDataSetCount = value; OnPropertyChanged(nameof(SpeciesgroupDataSetCount));
        }
    }


    //------------------------------------

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

    private bool _isNewFiSpecies;
    public bool IsNewFiSpecies
    {
        get => _isNewFiSpecies;
        set => SetProperty(ref _isNewFiSpecies, value);
    }

    private bool _isNewPlSpecies;
    public bool IsNewPlSpecies
    {
        get => _isNewPlSpecies;
        set => SetProperty(ref _isNewPlSpecies, value);
    }

    private bool _isNewName;
    public bool IsNewName
    {
        get => _isNewName;
        set => SetProperty(ref _isNewName, value);
    }
    private bool _isNewImage;
    public bool IsNewImage
    {
        get => _isNewImage;
        set => SetProperty(ref _isNewImage, value);
    }
    private bool _isNewSynonym;
    public bool IsNewSynonym
    {
        get => _isNewSynonym;
        set => SetProperty(ref _isNewSynonym, value);
    }
    private bool _isNewGeographic;
    public bool IsNewGeographic
    {
        get => _isNewGeographic;
        set => SetProperty(ref _isNewGeographic, value);
    }
    private bool _isNewReferenceExpert;
    public bool IsNewReferenceExpert
    {
        get => _isNewReferenceExpert;
        set => SetProperty(ref _isNewReferenceExpert, value);
    }

    private bool _isNewReferenceSource;
    public bool IsNewReferenceSource
    {
        get => _isNewReferenceSource;
        set => SetProperty(ref _isNewReferenceSource, value);
    }

    private bool _isNewReferenceAuthor;
    public bool IsNewReferenceAuthor
    {
        get => _isNewReferenceAuthor;
        set => SetProperty(ref _isNewReferenceAuthor, value);
    }
    private bool _isNewComment;
    public bool IsNewComment
    {
        get => _isNewComment;
        set => SetProperty(ref _isNewComment, value);
    }

    #endregion

    #region Refresh Properties

    private void RefreshGenusItems()
    {
        GenusItems.Clear();
        foreach (var item in Tbl66GenussesList)
        {
            GenusItems.Add(item);
        }
        if (Tbl66GenussesList.Count == 0)
        {
            return;
        }

        if (GenusSelected == null && Tbl66GenussesList.Count != 0)
        {
            GenusSelected = GenusItems.First();
        }
    }
    private void RefreshSpeciesgroupItems()
    {
        SpeciesgroupItems.Clear();
        if (Tbl68SpeciesgroupsList != null)
        {
            foreach (var item in Tbl68SpeciesgroupsList)
            {
                SpeciesgroupItems.Add(item);
            }

            if (Tbl68SpeciesgroupsList.Count == 0)
            {
                return;
            }

            if (GenusSelected == null && Tbl68SpeciesgroupsList.Count != 0)
            {
                SpeciesgroupSelected = SpeciesgroupItems.First()!;
            }
        }
    }
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
                 FiSpeciesSelected = FiSpeciesItems.First()!;
             }
         }
     }
    private void RefreshNameItems()
    {
        NameItems.Clear();
        if (Tbl78NamesList != null)
        {
            foreach (var item in Tbl78NamesList)
            {
                NameItems.Add(item);
            }
            if (Tbl78NamesList.Count == 0)
            {
                return;
            }

            if (NameSelected == null && Tbl78NamesList.Count != 0)
            {
                NameSelected = NameItems.FirstOrDefault()!;
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
            ImageSelected = ImageItems.First();
        }
    }
    private void RefreshSynonymItems()
    {
        SynonymItems.Clear();
        if (Tbl84SynonymsList != null)
        {
            foreach (var item in Tbl84SynonymsList)
            {
                SynonymItems.Add(item);
            }
            if (Tbl84SynonymsList.Count == 0)
            {
                return;
            }

            if (SynonymSelected == null && Tbl84SynonymsList.Count != 0)
            {
                SynonymSelected = SynonymItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshGeographicItems()
    {
        GeographicItems.Clear();
        if (Tbl87GeographicsList != null)
        {
            foreach (var item in Tbl87GeographicsList)
            {
                GeographicItems.Add(item);
            }
            if (Tbl87GeographicsList.Count == 0)
            {
                return;
            }

            if (GeographicSelected == null && Tbl87GeographicsList.Count != 0)
            {
                GeographicSelected = GeographicItems.FirstOrDefault()!;
            }
        }
    }

    private void RefreshReferenceExpertItems()
    {
        ReferenceExpertItems.Clear();
        if (Tbl90ReferenceExpertsList != null)
        {
            foreach (var item in Tbl90ReferenceExpertsList)
            {
                ReferenceExpertItems.Add(item);
            }
            if (Tbl90ReferenceExpertsList.Count == 0)
            {
                return;
            }

            if (ReferenceExpertSelected == null && Tbl90ReferenceExpertsList.Count != 0)
            {
                ReferenceExpertSelected = ReferenceExpertItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshReferenceSourceItems()
    {
        ReferenceSourceItems.Clear();
        if (Tbl90ReferenceSourcesList != null)
        {
            foreach (var item in Tbl90ReferenceSourcesList)
            {
                ReferenceSourceItems.Add(item);
            }
            if (Tbl90ReferenceSourcesList.Count == 0)
            {
                return;
            }

            if (ReferenceSourceSelected == null && Tbl90ReferenceSourcesList.Count != 0)
            {
                ReferenceSourceSelected = ReferenceSourceItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshReferenceAuthorItems()
    {
        ReferenceAuthorItems.Clear();
        if (Tbl90ReferenceAuthorsList != null)
        {
            foreach (var item in Tbl90ReferenceAuthorsList)
            {
                ReferenceAuthorItems.Add(item);
            }
            if (Tbl90ReferenceAuthorsList.Count == 0)
            {
                return;
            }

            if (ReferenceAuthorSelected == null && Tbl90ReferenceAuthorsList.Count != 0)
            {
                ReferenceAuthorSelected = ReferenceAuthorItems.FirstOrDefault()!;
            }
        }
    }
    private void RefreshCommentItems()
    {
        CommentItems.Clear();
        if (Tbl93CommentsList != null)
        {
            foreach (var item in Tbl93CommentsList)
            {
                CommentItems.Add(item);
            }
            if (Tbl93CommentsList.Count == 0)
            {
                return;
            }

            if (CommentSelected == null && Tbl93CommentsList.Count != 0)
            {
                CommentSelected = CommentItems.FirstOrDefault()!;
            }
        }
    }
    #endregion Refresh Properties

    #region Public Properties  

    private Data _dataSet;

    public Data DataSet
    {
        get => _dataSet;
        set
        {
            _dataSet = value; OnPropertyChanged(nameof(DataSet));
        }
    }

    private string _searchFiSpeciesName = "";

    public string SearchFiSpeciesName
    {
        get => _searchFiSpeciesName;
        set
        {
            _searchFiSpeciesName = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesList = null!;

    public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
    {
        get => _tbl69FiSpeciessesList;
        set
        {
            _tbl69FiSpeciessesList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList = null!;

    public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
    {
        get => _tbl69FiSpeciessesAllList;
        set
        {
            _tbl69FiSpeciessesAllList = value;
            OnPropertyChanged(nameof(Tbl69FiSpeciessesAllList));
        }
    }



    private ObservableCollection<Tbl66Genus> _tbl66GenussesList = null!;

    public ObservableCollection<Tbl66Genus> Tbl66GenussesList
    {
        get => _tbl66GenussesList;
        set
        {
            _tbl66GenussesList = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList = null!;

    public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
    {
        get => _tbl66GenussesAllList;
        set
        {
            _tbl66GenussesAllList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsList = null!;

    public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
    {
        get => _tbl68SpeciesgroupsList;
        set
        {
            _tbl68SpeciesgroupsList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList = null!;

    public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
    {
        get => _tbl68SpeciesgroupsAllList;
        set
        {
            _tbl68SpeciesgroupsAllList = value;
            OnPropertyChanged();
        }
    }



    private ObservableCollection<Tbl78Name> _tbl78NamesList = null!;

    public ObservableCollection<Tbl78Name> Tbl78NamesList
    {
        get => _tbl78NamesList;
        set
        {
            _tbl78NamesList = value;
            OnPropertyChanged();
        }
    }


    private ObservableCollection<Tbl81Image> _tbl81ImagesList = null!;

    public ObservableCollection<Tbl81Image> Tbl81ImagesList
    {
        get => _tbl81ImagesList;
        set
        {
            _tbl81ImagesList = value;
            OnPropertyChanged();
        }
    }


    private string _searchSynonymName = string.Empty;

    public string SearchSynonymName
    {
        get => _searchSynonymName;
        set
        {
            _searchSynonymName = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl84Synonym> _tbl84SynonymsList = null!;

    public ObservableCollection<Tbl84Synonym> Tbl84SynonymsList
    {
        get => _tbl84SynonymsList;
        set
        {
            _tbl84SynonymsList = value; OnPropertyChanged();
        }
    }



    private ObservableCollection<Tbl87Geographic> _tbl87GeographicsList = null!;

    public ObservableCollection<Tbl87Geographic> Tbl87GeographicsList
    {
        get => _tbl87GeographicsList;
        set
        {
            _tbl87GeographicsList = value;
            OnPropertyChanged();
        }
    }


    #region "Public Properties Tbl63Infratribus"

    private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList = null!;

    public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
    {
        get => _tbl63InfratribussesAllList;
        set
        {
            _tbl63InfratribussesAllList = value;
            OnPropertyChanged();
        }
    }

    #endregion "Public Properties"



    private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList = null!;

    public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
    {
        get => _tbl90SourcesAllList;
        set
        {
            _tbl90SourcesAllList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList = null!;

    public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
    {
        get => _tbl90ExpertsAllList;
        set
        {
            _tbl90ExpertsAllList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList = null!;

    public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
    {
        get => _tbl90ReferenceAuthorsList;
        set
        {
            _tbl90ReferenceAuthorsList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList = null!;

    public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
    {
        get => _tbl90ReferenceSourcesList;
        set
        {
            _tbl90ReferenceSourcesList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList = null!;

    public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
    {
        get => _tbl90ReferenceExpertsList;
        set
        {
            _tbl90ReferenceExpertsList = value;
            OnPropertyChanged();
        }
    }



    private ObservableCollection<Tbl93Comment> _tbl93CommentsList = null!;

    public ObservableCollection<Tbl93Comment> Tbl93CommentsList
    {
        get => _tbl93CommentsList;
        set
        {
            _tbl93CommentsList = value;
            OnPropertyChanged();
        }
    }
    private ObservableCollection<TblCountry> _tblCountriesAllList = null!;

    public ObservableCollection<TblCountry> TblCountriesAllList
    {
        get => _tblCountriesAllList;
        set
        {
            _tblCountriesAllList = value;
            OnPropertyChanged();
        }
    }
    private void GetValueContinent()
    {
        _continents = new List<Continent>()
            {
                new Continent { Name = "Africa" },
                new Continent { Name = "Antarctica" },
                new Continent { Name = "Asia" },
                new Continent { Name = "Australia" },
                new Continent { Name = "Central/South America" },
                new Continent { Name = "Europe" },
                new Continent { Name = "North America/Caribbean" }
            };

        _selectedContinent = new Continent();
    }

    private List<Continent> _continents = null!;

    public List<Continent> Continents
    {
        get => _continents;
        set
        {
            _continents = value;
            OnPropertyChanged();
        }
    }

    private Continent _selectedContinent = null!;

    public Continent SelectedContinent
    {
        get => _selectedContinent;
        set
        {
            _selectedContinent = value;
            OnPropertyChanged();
        }
    }


    public class Continent
    {
        public string? Name
        {
            get; set;
        }
    }

    private ObservableCollection<TblCountry> _tblCountriesList = null!;

    public ObservableCollection<TblCountry> TblCountriesList
    {
        get => _tblCountriesList;
        set
        {
            _tblCountriesList = value;
            OnPropertyChanged(nameof(TblCountriesList));
        }
    }



    private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList = null!;

    public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
    {
        get => _tbl90AuthorsAllList;
        set
        {
            _tbl90AuthorsAllList = value;
            OnPropertyChanged();
        }
    }
    private void GetValueLanguage()
    {
        _languages = new List<Language>()
            {
                new Language { Name = "GER" },
                new Language { Name = "ENG" },
                new Language { Name = "FRE" },
                new Language { Name = "POR" }
            };

        _selectedLanguage = new Language();
    }

    private List<Language> _languages = null!;

    public List<Language> Languages
    {
        get => _languages;
        set
        {
            _languages = value;
            OnPropertyChanged(nameof(Languages));
        }
    }

    private Language _selectedLanguage = null!;

    public Language SelectedLanguage
    {
        get => _selectedLanguage;
        set
        {
            _selectedLanguage = value;
            OnPropertyChanged(nameof(SelectedLanguage));
        }
    }

    public class Language
    {
        public string? Name
        {
            get; set;
        }
    }
    private void GetValueMimeType()
    {
        _mimeTypes = new List<MimeType>()
            {
                new MimeType { Name = "jpg" },
                new MimeType { Name = "png" },
                new MimeType { Name = "bmp" },
                new MimeType { Name = "tiff" },
                new MimeType { Name = "gif" },
                new MimeType { Name = "icon" },
                new MimeType { Name = "jpeg" },
                new MimeType { Name = "wmf" },
                new MimeType { Name = "wmv" },
                new MimeType { Name = "mpg" },
                new MimeType { Name = "mp4" },
                new MimeType { Name = "avi" },
                new MimeType { Name = "mov" },
                new MimeType { Name = "swf" },
                new MimeType { Name = "flv" }
            };

        _selectedMimeType = new MimeType();
    }

    private List<MimeType> _mimeTypes = null!;

    public List<MimeType> MimeTypes
    {
        get => _mimeTypes;
        set
        {
            _mimeTypes = value;
            OnPropertyChanged(nameof(MimeTypes));
        }
    }

    private MimeType _selectedMimeType = null!;

    public MimeType SelectedMimeType
    {
        get => _selectedMimeType;
        set
        {
            _selectedMimeType = value;
            OnPropertyChanged(nameof(SelectedMimeType));
        }
    }

    public class MimeType
    {
        public string? Name
        {
            get; set;
        }
    }
    public static RelayCommand? OpenCommand
    {
        get; set;
    }
    private string _selectedPath = null!;

    public string SelectedPath
    {
        get => _selectedPath;
        set
        {
            _selectedPath = value; OnPropertyChanged(nameof(SelectedPath));
        }
    }



    public readonly string DefaultPath = null!;



    #endregion Public Properties

    #endregion All Properties

    //public Tbl69FiSpeciessesViewModel()
    //{

    //}
}
