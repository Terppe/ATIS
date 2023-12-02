// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ATIS.WinUi.ViewModels.Search;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using System.Diagnostics;
using QuestPDF.Fluent;
using System.Configuration;
using QuestPDF.Infrastructure;
using ATIS.WinUi.Views.Report.Pdf.Tbl03Regnums;
using ATIS.WinUi.Views.Report.Pdf.Tbl21Classes;
using ATIS.WinUi.Views.Report.Pdf.Tbl24Subclasses;
using ATIS.WinUi.Views.Report.Pdf.Tbl33Ordos;
using ATIS.WinUi.Views.Report.Pdf.Tbl36Subordos;
using ATIS.WinUi.Views.Report.Pdf.Tbl39Infraordos;
using ATIS.WinUi.Views.Report.Pdf.Tbl42Superfamilies;
using ATIS.WinUi.Views.Report.Pdf.Tbl45Families;
using ATIS.WinUi.Views.Report.Pdf.Tbl54Supertribusses;
using ATIS.WinUi.Views.Report.Pdf.Tbl57Tribusses;
using ATIS.WinUi.Views.Report.Pdf.Tbl60Subtribusses;
using ATIS.WinUi.Views.Report.Pdf.Tbl63Infratribusses;
using ATIS.WinUi.Views.Report.Pdf.Tbl66Genusses;
using ATIS.WinUi.Views.Report.Pdf.Tbl51Infrafamilies;
using ATIS.WinUi.Views.Report.Pdf.Tbl48Subfamilies;
using ATIS.WinUi.Views.Report.Pdf.Tbl30Legios;
using ATIS.WinUi.Views.Report.Pdf.Tbl27Infraclasses;
using ATIS.WinUi.Views.Report.Pdf.Tbl18Superclasses;
using ATIS.WinUi.Views.Report.Pdf.Tbl15Subdivisions;
using ATIS.WinUi.Views.Report.Pdf.Tbl12Subphylums;
using ATIS.WinUi.Views.Report.Pdf.Tbl09Divisions;
using ATIS.WinUi.Views.Report.Pdf.Tbl06Phylums;
using ATIS.WinUi.Views.Report.Pdf.Tbl69FiSpeciesses;
using ATIS.WinUi.Views.Search.Pdf.Tbl03Regnums;
using ATIS.WinUi.Views.Search.Pdf.Tbl06Phylums;
using ATIS.WinUi.Views.Search.Pdf.Tbl09Divisions;
using ATIS.WinUi.Views.Search.Pdf.Tbl12Subphylums;
using ATIS.WinUi.Views.Search.Pdf.Tbl15Subdivisions;
using ATIS.WinUi.Views.Report.Pdf.Tbl69FiSpeciesSubs;
using ATIS.WinUi.Views.Report.Pdf.Tbl72PlSpeciesses;
using ATIS.WinUi.Views.Report.Pdf.Tbl72PlSpeciesSubs;
using ATIS.WinUi.Views.Search.Pdf.Tbl18Superclasses;
using ATIS.WinUi.Views.Search.Pdf.Tbl21Classes;
using ATIS.WinUi.Views.Search.Pdf.Tbl24Subclasses;
using ATIS.WinUi.Views.Search.Pdf.Tbl27Infraclasses;
using ATIS.WinUi.Views.Search.Pdf.Tbl30Legios;
using ATIS.WinUi.Views.Search.Pdf.Tbl33Ordos;
using ATIS.WinUi.Views.Search.Pdf.Tbl36Subordos;
using ATIS.WinUi.Views.Search.Pdf.Tbl39Infraordos;
using ATIS.WinUi.Views.Search.Pdf.Tbl42Superfamilies;
using ATIS.WinUi.Views.Search.Pdf.Tbl45Families;
using ATIS.WinUi.Views.Search.Pdf.Tbl48Subfamilies;
using ATIS.WinUi.Views.Search.Pdf.Tbl51Infrafamilies;
using ATIS.WinUi.Views.Search.Pdf.Tbl54Supertribusses;
using ATIS.WinUi.Views.Search.Pdf.Tbl57Tribusses;
using ATIS.WinUi.Views.Search.Pdf.Tbl60Subtribusses;
using ATIS.WinUi.Views.Search.Pdf.Tbl63Infratribusses;
using ATIS.WinUi.Views.Search.Pdf.Tbl66Genusses;
using ATIS.WinUi.Views.Search.Pdf.Tbl69FiSpeciesses;
using ATIS.WinUi.Views.Search.Pdf.Tbl72PlSpeciesses;
using ATIS.WinUi.Helpers;
using CommunityToolkit.WinUI.UI.Triggers;
using Microsoft.UI.Xaml.Controls.Primitives;
using static ATIS.WinUi.Views.Report.PdfModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Views.Search;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SearchQuickPage : Page
{
    public SearchViewModel ViewModel
    {
        get;
    }

    private static readonly AllDialogs AllDialogs = new();

    public SearchQuickPage()
    {
        ViewModel = App.GetService<SearchViewModel>();
        InitializeComponent();
        QuestPDF.Settings.DocumentLayoutExceptionThreshold = 1000;
        QuestPDF.Settings.EnableCaching = true;
        QuestPDF.Settings.License = LicenseType.Community;
    }

    private void SearchAutoSuggestBox_OnLoaded(object sender, RoutedEventArgs e)
    {
        SearchAutoSuggestBox.Visibility = Visibility.Visible;
        SearchAutoSuggestBox.Focus(FocusState.Programmatic);
    }

    public async void Tbl66GenussesListViewDoubleTapped()
    {
        if (ViewModel.GenusSelected.GenusId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.GenusSelected.GenusId, "Tbl66Genusses");

        GenusDialog.Title = ViewModel.GenusSelected.GenusName;
        await GenusDialog.ShowAsync();
    }

    public async void Tbl69FiSpeciessesListViewDoubleTapped()
    {
        if (ViewModel.FiSpeciesSelected.FiSpeciesId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.FiSpeciesSelected.FiSpeciesId, "Tbl69FiSpeciesses");

        //FiSpeciesDialog.Title = ViewModel.FiSpeciesSelected.Tbl66Genusses.GenusName + " " +
        //                        ViewModel.FiSpeciesSelected.FiSpeciesName + " " +
        //                        ViewModel.FiSpeciesSelected.Subspecies + " " +
        //                        ViewModel.FiSpeciesSelected.Divers;
        if (string.IsNullOrEmpty(ViewModel.FiSpeciesSelected.Subspecies))
        {
            await FiSpeciesDialog.ShowAsync();
        }
        else
        {
            await FiSpeciesSubDialog.ShowAsync();
        }
    }

    public async void Tbl72PlSpeciessesListViewDoubleTapped()
    {
        if (ViewModel.PlSpeciesSelected.PlSpeciesId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.PlSpeciesSelected.PlSpeciesId, "Tbl72PlSpeciesses");

        //PlSpeciesDialog.Title = ViewModel.PlSpeciesSelected.Tbl66Genusses.GenusName + " " +
        //                        ViewModel.PlSpeciesSelected.PlSpeciesName + " " +
        //                        ViewModel.PlSpeciesSelected.Subspecies + " " +
        //                        ViewModel.FiSpeciesSelected.Divers;
        if (string.IsNullOrEmpty(ViewModel.PlSpeciesSelected.Subspecies))
        {
            await PlSpeciesDialog.ShowAsync();
        }
        else
        {
            await PlSpeciesSubDialog.ShowAsync();
        }
    }

    public async void Tbl63InfratribussesListViewDoubleTapped()
    {
        if (ViewModel.InfratribusSelected.InfratribusId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.InfratribusSelected.InfratribusId, "Tbl63Infratribusses");

        InfratribusDialog.Title = ViewModel.InfratribusSelected.InfratribusName;
        await InfratribusDialog.ShowAsync();
    }

    public async void Tbl60SubtribussesListViewDoubleTapped()
    {
        if (ViewModel.SubtribusSelected.SubtribusId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.SubtribusSelected.SubtribusId, "Tbl60Subtribusses");

        SubtribusDialog.Title = ViewModel.SubtribusSelected.SubtribusName;
        await SubtribusDialog.ShowAsync();
    }

    public async void Tbl57TribussesListViewViewDoubleTapped()
    {
        if (ViewModel.TribusSelected.TribusId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.TribusSelected.TribusId, "Tbl57Tribusses");

        TribusDialog.Title = ViewModel.TribusSelected.TribusName;
        await TribusDialog.ShowAsync();
    }

    public async void Tbl54SupertribussesListViewDoubleTapped()
    {
        if (ViewModel.SupertribusSelected.SupertribusId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.SupertribusSelected.SupertribusId, "Tbl54Supertribusses");

        SupertribusDialog.Title = ViewModel.SupertribusSelected.SupertribusName;
        await SupertribusDialog.ShowAsync();
    }

    public async void Tbl51InfrafamiliesListViewDoubleTapped()
    {
        if (ViewModel.InfrafamilySelected.InfrafamilyId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.InfrafamilySelected.InfrafamilyId, "Tbl51Infrafamilies");

        InfrafamilyDialog.Title = ViewModel.InfrafamilySelected.InfrafamilyName;
        await InfrafamilyDialog.ShowAsync();
    }

    public async void Tbl48SubfamiliesListViewDoubleTapped()
    {
        if (ViewModel.SubfamilySelected.SubfamilyId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.SubfamilySelected.SubfamilyId, "Tbl48Subfamilies");

        SubfamilyDialog.Title = ViewModel.SubfamilySelected.SubfamilyName;
        await SubfamilyDialog.ShowAsync();
    }

    public async void Tbl45FamiliesListViewDoubleTapped()
    {
        if (ViewModel.FamilySelected.FamilyId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.FamilySelected.FamilyId, "Tbl45Families");

        FamilyDialog.Title = ViewModel.FamilySelected.FamilyName;
        await FamilyDialog.ShowAsync();
    }

    public async void Tbl42SuperfamiliesListViewDoubleTapped()
    {
        if (ViewModel.SuperfamilySelected.SuperfamilyId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.SuperfamilySelected.SuperfamilyId, "Tbl42Superfamilies");

        SuperfamilyDialog.Title = ViewModel.SuperfamilySelected.SuperfamilyName;
        await SuperfamilyDialog.ShowAsync();
    }

    public async void Tbl39InfraordosListViewDoubleTapped()
    {
        if (ViewModel.InfraordoSelected.InfraordoId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.InfraordoSelected.InfraordoId, "Tbl39Infraordos");

        InfraordoDialog.Title = ViewModel.InfraordoSelected.InfraordoName;
        await InfraordoDialog.ShowAsync();
    }

    public async void Tbl36SubordosListViewDoubleTapped()
    {
        if (ViewModel.SubordoSelected.SubordoId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.SubordoSelected.SubordoId, "Tbl36Subordos");

        SubordoDialog.Title = ViewModel.SubordoSelected.SubordoName;
        await SubordoDialog.ShowAsync();
    }

    public async void Tbl33OrdosListViewDoubleTapped()
    {
        if (ViewModel.OrdoSelected.OrdoId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.OrdoSelected.OrdoId, "Tbl33Ordos");

        OrdoDialog.Title = ViewModel.OrdoSelected.OrdoName;
        await OrdoDialog.ShowAsync();
    }

    public async void Tbl30LegiosListViewDoubleTapped()
    {
        if (ViewModel.LegioSelected.LegioId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.LegioSelected.LegioId, "Tbl30Legios");

        LegioDialog.Title = ViewModel.LegioSelected.LegioName;
        await LegioDialog.ShowAsync();
    }

    public async void Tbl27InfraclassesListViewDoubleTapped()
    {
        if (ViewModel.InfraclassSelected.InfraclassId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.InfraclassSelected.InfraclassId, "Tbl27Infraclasses");

        InfraclassDialog.Title = ViewModel.InfraclassSelected.InfraclassName;
        await InfraclassDialog.ShowAsync();
    }

    public async void Tbl24SubclassesListViewViewDoubleTapped()
    {
        if (ViewModel.SubclassSelected.SubclassId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.SubclassSelected.SubclassId, "Tbl24Subclasses");

        SubclassDialog.Title = ViewModel.SubclassSelected.SubclassName;
        await SubclassDialog.ShowAsync();
    }

    public async void Tbl21ClassesListViewDoubleTapped()
    {
        if (ViewModel.ClassSelected.ClassId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.ClassSelected.ClassId, "Tbl21Classes");

        ClassDialog.Title = ViewModel.ClassSelected.ClassName;
        await ClassDialog.ShowAsync();
    }

    public async void Tbl18SuperclassesListViewDoubleTapped()
    {
        if (ViewModel.SuperclassSelected.SuperclassId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.SuperclassSelected.SuperclassId, "Tbl18Superclasses");

        SuperclassDialog.Title = ViewModel.SuperclassSelected.SuperclassName;
        await SuperclassDialog.ShowAsync();
    }

    public async void Tbl15SubdivisionsListViewDoubleTapped()
    {
        if (ViewModel.SubdivisionSelected.SubdivisionId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.SubdivisionSelected.SubdivisionId, "Tbl15Subdivisions");

        SubdivisionDialog.Title = ViewModel.SubdivisionSelected.SubdivisionName;
        await SubdivisionDialog.ShowAsync();
    }

    public async void Tbl09DivisionsListViewDoubleTapped()
    {
        if (ViewModel.DivisionSelected.DivisionId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.DivisionSelected.DivisionId, "Tbl09Divisions");

        DivisionDialog.Title = ViewModel.DivisionSelected.DivisionName;
        await DivisionDialog.ShowAsync();
    }

    public async void Tbl12SubphylumsListViewDoubleTapped()
    {
        if (ViewModel.SubphylumSelected.SubphylumId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.SubphylumSelected.SubphylumId, "Tbl12Subphylums");

        SubphylumDialog.Title = ViewModel.SubphylumSelected.SubphylumName;
        await SubphylumDialog.ShowAsync();
    }

    public async void Tbl06PhylumsListViewDoubleTapped()
    {
        if (ViewModel.PhylumSelected.PhylumId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.PhylumSelected.PhylumId, "Tbl06Phylums");

        PhylumDialog.Title = ViewModel.PhylumSelected.PhylumName;
        await PhylumDialog.ShowAsync();
    }

    public async void Tbl03RegnumsListViewDoubleTapped()
    {
        if (ViewModel.RegnumSelected.RegnumId == 0)
        {
            return;
        }

        ViewModel.ReportSearchAll(ViewModel.RegnumSelected.RegnumId, "Tbl03Regnums");

        RegnumDialog.Title = ViewModel.RegnumSelected.RegnumFullName;
        await RegnumDialog.ShowAsync();
    }

    //----------------------Command PDF---------------------------------------------------
    public ICommand PdfRegnumCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfRegnum(ViewModel.RegnumSelected.RegnumId);
    });

    public ICommand PdfPhylumCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfPhylum(ViewModel.PhylumSelected.PhylumId);
    });

    public ICommand PdfDivisionCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfDivision(ViewModel.DivisionSelected.DivisionId);
    });

    public ICommand PdfSubphylumCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfSubphylum(ViewModel.SubphylumSelected.SubphylumId);
    });

    public ICommand PdfSubdivisionCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfSubdivision(ViewModel.SubdivisionSelected.SubdivisionId);
    });

    public ICommand PdfSuperclassCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfSuperclass(ViewModel.SuperclassSelected.SuperclassId);
    });

    public ICommand PdfClassCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfClass(ViewModel.ClassSelected.ClassId);
    });

    public ICommand PdfSubclassCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfSubclass(ViewModel.SubclassSelected.SubclassId);
    });

    public ICommand PdfInfraclassCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfInfraclass(ViewModel.InfraclassSelected.InfraclassId);
    });

    public ICommand PdfLegioCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfLegio(ViewModel.LegioSelected.LegioId);
    });

    public ICommand PdfOrdoCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfOrdo(ViewModel.OrdoSelected.OrdoId);
    });

    public ICommand PdfSubordoCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfSubordo(ViewModel.SubordoSelected.SubordoId);
    });

    public ICommand PdfInfraordoCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfInfraordo(ViewModel.InfraordoSelected.InfraordoId);
    });

    public ICommand PdfSuperfamilyCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfSuperfamily(ViewModel.SuperfamilySelected.SuperfamilyId);
    });

    public ICommand PdfFamilyCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfFamily(ViewModel.FamilySelected.FamilyId);
    });

    public ICommand PdfSubfamilyCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfSubfamily(ViewModel.SubfamilySelected.SubfamilyId);
    });

    public ICommand PdfInfrafamilyCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfInfrafamily(ViewModel.InfrafamilySelected.InfrafamilyId);
    });

    public ICommand PdfSupertribusCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfSupertribus(ViewModel.SupertribusSelected.SupertribusId);
    });

    public ICommand PdfTribusCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfTribus(ViewModel.TribusSelected.TribusId);
    });

    public ICommand PdfSubtribusCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfSubtribus(ViewModel.SubtribusSelected.SubtribusId);
    });

    public ICommand PdfInfratribusCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfInfratribus(ViewModel.InfratribusSelected.InfratribusId);
    });

    public ICommand PdfGenusCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfGenus(ViewModel.GenusSelected.GenusId);
    });

    public ICommand PdfFiSpeciesCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfFiSpecies(ViewModel.FiSpeciesSelected.FiSpeciesId);
    });

    public ICommand PdfFiSpeciesSubCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfFiSpeciesSub(ViewModel.FiSpeciesSubSelected.FiSpeciesId);
    });

    public ICommand PdfPlSpeciesCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfPlSpecies(ViewModel.PlSpeciesSelected.PlSpeciesId);
    });

    public ICommand PdfPlSpeciesSubCommand => new RelayCommand(execute: delegate
    {
        GeneratePdfPlSpeciesSub(ViewModel.PlSpeciesSubSelected.PlSpeciesId);
    });

    //public object PdfListPrintCommand { get; } = new();

    //--------------------Method PDF------------------------------------------------------------
    private static void GeneratePdfRegnum(int regnumId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportRegnum.pdf";

        //var filePath = "D:\\Temp\\PdfReportRegnum.pdf";

        var model = Tbl03RegnumsDocumentDataSource.GetRegnumDetails(regnumId);
        var document = new Tbl03RegnumsDocument(model);


        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfPhylum(int phylumId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportPhylum.pdf";

        //var filePath = "D:\\Temp\\PdfReportPhylum.pdf";

        var model = Tbl06PhylumsDocumentDataSource.GetPhylumDetails(phylumId);
        var document = new Tbl06PhylumsDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfDivision(int divisionId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportDivision.pdf";

        //var filePath = "D:\\Temp\\PdfReportDivision.pdf";

        var model = Tbl09DivisionsDocumentDataSource.GetDivisionDetails(divisionId);
        var document = new Tbl09DivisionsDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfSubphylum(int subphylumId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportSubphylum.pdf";

        //var filePath = "D:\\Temp\\PdfReportSubphylum.pdf";

        var model = Tbl12SubphylumsDocumentDataSource.GetSubphylumDetails(subphylumId);
        var document = new Tbl12SubphylumsDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfSubdivision(int subdivisionId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportSubdivision.pdf";

        //var filePath = "D:\\Temp\\PdfReportSubdivision.pdf";

        var model = Tbl15SubdivisionsDocumentDataSource.GetSubdivisionDetails(subdivisionId);
        var document = new Tbl15SubdivisionsDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfSuperclass(int superclassId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportSuperclass.pdf";

        //var filePath = "D:\\Temp\\PdfReportSuperclass.pdf";

        var model = Tbl18SuperclassesDocumentDataSource.GetSuperclassDetails(superclassId);
        var document = new Tbl18SuperclassesDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfClass(int classId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportClass.pdf";

        //var filePath = "D:\\Temp\\PdfReportClass.pdf";

        var model = Tbl21ClassesDocumentDataSource.GetClassDetails(classId);
        var document = new Tbl21ClassesDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfSubclass(int subclassId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportSubclass.pdf";

        //var filePath = "D:\\Temp\\PdfReportSubclass.pdf";

        var model = Tbl24SubclassesDocumentDataSource.GetSubclassDetails(subclassId);
        var document = new Tbl24SubclassesDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfInfraclass(int infraclassId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportInfraclass.pdf";

        //var filePath = "D:\\Temp\\PdfReportInfraclass.pdf";

        var model = Tbl27InfraclassesDocumentDataSource.GetInfraclassDetails(infraclassId);
        var document = new Tbl27InfraclassesDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfLegio(int legioId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportLegio.pdf";

        //var filePath = "D:\\Temp\\PdfReportLegio.pdf";

        var model = Tbl30LegiosDocumentDataSource.GetLegioDetails(legioId);
        var document = new Tbl30LegiosDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfOrdo(int ordoId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportOrdo.pdf";

        //var filePath = "D:\\Temp\\PdfReportOrdo.pdf";

        var model = Tbl33OrdosDocumentDataSource.GetOrdoDetails(ordoId);
        var document = new Tbl33OrdosDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfSubordo(int subordoId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportSubordo.pdf";

        //var filePath = "D:\\Temp\\PdfReportSubordo.pdf";

        var model = Tbl36SubordosDocumentDataSource.GetSubordoDetails(subordoId);
        var document = new Tbl36SubordosDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfInfraordo(int infraordoId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportInfraordo.pdf";

        //var filePath = "D:\\Temp\\PdfReportInfraordo.pdf";

        var model = Tbl39InfraordosDocumentDataSource.GetInfraordoDetails(infraordoId);
        var document = new Tbl39InfraordosDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfSuperfamily(int superfamilyId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportSuperfamily.pdf";

        //var filePath = "D:\\Temp\\PdfReportSuperfamily.pdf";

        var model = Tbl42SuperfamiliesDocumentDataSource.GetSuperfamilyDetails(superfamilyId);
        var document = new Tbl42SuperfamiliesDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfFamily(int familyId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportFamily.pdf";

        //var filePath = "D:\\Temp\\PdfReportFamily.pdf";

        var model = Tbl45FamiliesDocumentDataSource.GetFamilyDetails(familyId);
        var document = new Tbl45FamiliesDocument(model);

        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfSubfamily(int subfamilyId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportSubfamily.pdf";

        //var filePath = "D:\\Temp\\PdfReportSubfamily.pdf";

        var model = Tbl48SubfamiliesDocumentDataSource.GetSubfamilyDetails(subfamilyId);
        var document = new Tbl48SubfamiliesDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfInfrafamily(int infrafamilyId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportInfrafamily.pdf";

        //var filePath = "D:\\Temp\\PdfReportInfrafamily.pdf";

        var model = Tbl51InfrafamiliesDocumentDataSource.GetInfrafamilyDetails(infrafamilyId);
        var document = new Tbl51InfrafamiliesDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfSupertribus(int supertribusId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportSupertribus.pdf";

        //var filePath = "D:\\Temp\\PdfReportSupertribus.pdf";

        var model = Tbl54SupertribussesDocumentDataSource.GetSupertribusDetails(supertribusId);
        var document = new Tbl54SupertribussesDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfTribus(int tribusId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportTribus.pdf";

        //var filePath = "D:\\Temp\\PdfReportTribus.pdf";

        var model = Tbl57TribussesDocumentDataSource.GetTribusDetails(tribusId);
        var document = new Tbl57TribussesDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfSubtribus(int subtribusId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportInfratribus.pdf";

        //var filePath = "D:\\Temp\\PdfReportInfratribus.pdf";

        var model = Tbl60SubtribussesDocumentDataSource.GetSubtribusDetails(subtribusId);
        var document = new Tbl60SubtribussesDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfInfratribus(int infratribusId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportInfratribus.pdf";

        //var filePath = "D:\\Temp\\PdfReportInfratribus.pdf";

        var model = Tbl63InfratribussesDocumentDataSource.GetInfratribusDetails(infratribusId);
        var document = new Tbl63InfratribussesDocument(model);
        document.GeneratePdf(filePath);

        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfGenus(int genusId)
    {
        //Version QuestPDF
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportGenus.pdf";

        //var filePath = "D:\\Temp\\PdfReportGenus.pdf";

        var model = Tbl66GenussesDocumentDataSource.GetGenusDetails(genusId);
        var document = new Tbl66GenussesDocument(model);

        document.GeneratePdf(filePath);
        PrintPdfMethod(filePath);

        //--------------------------------------
        //// Please make sure that you are eligible to use the Community license.
        //// To learn more about the QuestPDF licensing, please visit:
        //// https://www.questpdf.com/pricing.html
        //Settings.License = LicenseType.Community;

        //// For documentation and implementation details, please visit:
        //// https://www.questpdf.com/documentation/getting-started.html
        //var model = InvoiceDocumentDataSource.GetInvoiceDetails();
        //var document = new InvoiceDocument(model);

        //// Generate PDF file and show it in the default viewer
        //document.GeneratePdfAndShow();

        //// Or open the QuestPDF Previewer and experiment with the document's design
        //// in real-time without recompilation after each code change
        ////document.ShowInPreviewer();

    }

    private static void GeneratePdfFiSpecies(int fiSpeciesId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportFiSpecies.pdf";

        //var filePath = "D:\\Temp\\PdfReportFiSpecies.pdf";

        var model = Tbl69FiSpeciessesDocumentDataSource.GetFiSpeciesDetails(fiSpeciesId);
        var document = new Tbl69FiSpeciessesDocument(model);
        document.GeneratePdf(filePath);
        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfFiSpeciesSub(int fiSpeciesId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportFiSpeciesSub.pdf";

        //var filePath = "D:\\Temp\\PdfReportFiSpeciesSub.pdf";

        var model = Tbl69FiSpeciesSubsDocumentDataSource.GetFiSpeciesSubDetails(fiSpeciesId);
        var document = new Tbl69FiSpeciesSubsDocument(model);
        document.GeneratePdf(filePath);
        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfPlSpecies(int plSpeciesId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportPlSpecies.pdf";

        //var filePath = "D:\\Temp\\PdfReportPlSpecies.pdf";

        var model = Tbl72PlSpeciessesDocumentDataSource.GetPlSpeciesDetails(plSpeciesId);
        var document = new Tbl72PlSpeciessesDocument(model);
        document.GeneratePdf(filePath);
        PrintPdfMethod(filePath);
    }

    private static void GeneratePdfPlSpeciesSub(int plSpeciesId)
    {
        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");
        filePath = $"{filePath}PdfReportPlSpeciesSub.pdf";

        //var filePath = "D:\\Temp\\PdfReportPlSpeciesSub.pdf";

        var model = Tbl72PlSpeciesSubsDocumentDataSource.GetPlSpeciesSubDetails(plSpeciesId);
        var document = new Tbl72PlSpeciesSubsDocument(model);
        document.GeneratePdf(filePath);
        PrintPdfMethod(filePath);
    }

    //-------------------------------------------Print List PDF-----

    public async void PdfPrintListAll(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(ViewModel.FilterText))
        {
            await AllDialogs.NoDatasetFoundInfoMessageDialogAsync();
            return;
        }

        var filePath = ConfigurationManager.AppSettings.Get("PdfPath");

        switch (ViewModel.SelectedMainTabIndex)
        {
            case 9:
            {
                filePath = $"{filePath}PdfListRegnum.pdf";
                {
                    if (ViewModel.RegnumsCollection.Count != 0)
                    {
                        var model =
                            Tbl03RegnumsListDocumentDataSource.GetRegnumListDetails(ViewModel.RegnumsCollection);
                        var document = new Tbl03RegnumsListDocument(model, ViewModel.RegnumsCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 8 when ViewModel.SelectedPhylumTabIndex == 0:
            {
                filePath = $"{filePath}PdfListSubphylum.pdf";
                {
                    if (ViewModel.SubphylumsCollection.Count != 0)
                    {
                        var model =
                            Tbl12SubphylumsListDocumentDataSource.GetSubphylumListDetails(
                                ViewModel.SubphylumsCollection);
                        var document = new Tbl12SubphylumsListDocument(model, ViewModel.SubphylumsCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 8 when ViewModel.SelectedPhylumTabIndex == 1:
            {
                filePath = $"{filePath}PdfListPhylum.pdf";
                {
                    if (ViewModel.PhylumsCollection.Count != 0)
                    {
                        var model =
                            Tbl06PhylumsListDocumentDataSource.GetPhylumListDetails(ViewModel.PhylumsCollection);
                        var document = new Tbl06PhylumsListDocument(model, ViewModel.PhylumsCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 7 when ViewModel.SelectedDivisionTabIndex == 0:
            {
                filePath = $"{filePath}PdfListSubdivision.pdf";
                {
                    if (ViewModel.SubdivisionsCollection.Count != 0)
                    {
                        var model =
                            Tbl15SubdivisionsListDocumentDataSource.GetSubdivisionListDetails(ViewModel
                                .SubdivisionsCollection);
                        var document = new Tbl15SubdivisionsListDocument(model, ViewModel.SubdivisionsCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 7 when ViewModel.SelectedDivisionTabIndex == 1:
            {
                filePath = $"{filePath}PdfListDivision.pdf";
                {
                    if (ViewModel.DivisionsCollection.Count != 0)
                    {
                        var model =
                            Tbl09DivisionsListDocumentDataSource.GetDivisionListDetails(ViewModel.DivisionsCollection);
                        var document = new Tbl09DivisionsListDocument(model, ViewModel.DivisionsCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 6 when ViewModel.SelectedClassTabIndex == 0:
            {
                filePath = $"{filePath}PdfListInfraclass.pdf";
                {
                    if (ViewModel.InfraclassesCollection.Count != 0)
                    {
                        var model =
                            Tbl27InfraclassesListDocumentDataSource.GetInfraclassListDetails(ViewModel
                                .InfraclassesCollection);
                        var document = new Tbl27InfraclassesListDocument(model, ViewModel.InfraclassesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 6 when ViewModel.SelectedClassTabIndex == 1:
            {
                filePath = $"{filePath}PdfListSubclass.pdf";
                {
                    if (ViewModel.SubclassesCollection.Count != 0)
                    {
                        var model =
                            Tbl24SubclassesListDocumentDataSource
                                .GetSubclassListDetails(ViewModel.SubclassesCollection);
                        var document = new Tbl24SubclassesListDocument(model, ViewModel.SubclassesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 6 when ViewModel.SelectedClassTabIndex == 2:
            {
                filePath = $"{filePath}PdfListClass.pdf";
                {
                    if (ViewModel.ClassesCollection.Count != 0)
                    {
                        var model = Tbl21ClassesListDocumentDataSource.GetClassListDetails(ViewModel.ClassesCollection);
                        var document = new Tbl21ClassesListDocument(model, ViewModel.ClassesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 6 when ViewModel.SelectedClassTabIndex == 3:
            {
                filePath = $"{filePath}PdfListSuperclass.pdf";
                {
                    if (ViewModel.SuperclassesCollection.Count != 0)
                    {
                        var model =
                            Tbl18SuperclassesListDocumentDataSource.GetSuperclassListDetails(ViewModel
                                .SuperclassesCollection);
                        var document = new Tbl18SuperclassesListDocument(model, ViewModel.SuperclassesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 5 when ViewModel.SelectedOrdoTabIndex == 0:
            {
                filePath = $"{filePath}PdfListInfraordo.pdf";
                {
                    if (ViewModel.InfraordosCollection.Count != 0)
                    {
                        var model =
                            Tbl39InfraordosListDocumentDataSource.GetInfraordoListDetails(
                                ViewModel.InfraordosCollection);
                        var document = new Tbl39InfraordosListDocument(model, ViewModel.InfraordosCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 5 when ViewModel.SelectedOrdoTabIndex == 1:
            {
                filePath = $"{filePath}PdfListSubordo.pdf";
                {
                    if (ViewModel.SubordosCollection.Count != 0)
                    {
                        var model =
                            Tbl36SubordosListDocumentDataSource.GetSubordoListDetails(ViewModel.SubordosCollection);
                        var document = new Tbl36SubordosListDocument(model, ViewModel.SubordosCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 5 when ViewModel.SelectedOrdoTabIndex == 2:
            {
                filePath = $"{filePath}PdfListOrdo.pdf";
                {
                    if (ViewModel.OrdosCollection.Count != 0)
                    {
                        var model = Tbl33OrdosListDocumentDataSource.GetOrdoListDetails(ViewModel.OrdosCollection);
                        var document = new Tbl33OrdosListDocument(model, ViewModel.OrdosCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 5 when ViewModel.SelectedOrdoTabIndex == 3:
            {
                filePath = $"{filePath}PdfListLegio.pdf";
                {
                    if (ViewModel.LegiosCollection.Count != 0)
                    {
                        var model = Tbl30LegiosListDocumentDataSource.GetLegioListDetails(ViewModel.LegiosCollection);
                        var document = new Tbl30LegiosListDocument(model, ViewModel.LegiosCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 4 when ViewModel.SelectedFamilyTabIndex == 0:
            {
                filePath = $"{filePath}PdfListInfrafamily.pdf";
                {
                    if (ViewModel.InfrafamiliesCollection.Count != 0)
                    {
                        var model =
                            Tbl51InfrafamiliesListDocumentDataSource.GetInfrafamilyListDetails(ViewModel
                                .InfrafamiliesCollection);
                        var document = new Tbl51InfrafamiliesListDocument(model, ViewModel.InfrafamiliesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 4 when ViewModel.SelectedFamilyTabIndex == 1:
            {
                filePath = $"{filePath}PdfListSubfamily.pdf";
                {
                    if (ViewModel.SubfamiliesCollection.Count != 0)
                    {
                        var model =
                            Tbl48SubfamiliesListDocumentDataSource.GetSubfamilyListDetails(ViewModel
                                .SubfamiliesCollection);
                        var document = new Tbl48SubfamiliesListDocument(model, ViewModel.SubfamiliesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 4 when ViewModel.SelectedFamilyTabIndex == 2:
            {
                filePath = $"{filePath}PdfListFamily.pdf";
                {
                    if (ViewModel.FamiliesCollection.Count != 0)
                    {
                        var model =
                            Tbl45FamiliesListDocumentDataSource.GetFamilyListDetails(ViewModel.FamiliesCollection);
                        var document = new Tbl45FamiliesListDocument(model, ViewModel.FamiliesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 4 when ViewModel.SelectedFamilyTabIndex == 3:
            {
                filePath = $"{filePath}PdfListSuperfamily.pdf";
                {
                    if (ViewModel.SuperfamiliesCollection.Count != 0)
                    {
                        var model =
                            Tbl42SuperfamiliesListDocumentDataSource.GetSuperfamilyListDetails(ViewModel
                                .SuperfamiliesCollection);
                        var document = new Tbl42SuperfamiliesListDocument(model, ViewModel.SuperfamiliesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 3 when ViewModel.SelectedTribusTabIndex == 0:
            {
                filePath = $"{filePath}PdfListInfratribus.pdf";
                {
                    if (ViewModel.InfratribussesCollection.Count != 0)
                    {
                        var model =
                            Tbl63InfratribussesListDocumentDataSource.GetInfratribusListDetails(ViewModel
                                .InfratribussesCollection);
                        var document = new Tbl63InfratribussesListDocument(model, ViewModel.InfratribussesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 3 when ViewModel.SelectedTribusTabIndex == 1:
            {
                filePath = $"{filePath}PdfListSubtribus.pdf";
                {
                    if (ViewModel.SubtribussesCollection.Count != 0)
                    {
                        var model =
                            Tbl60SubtribussesListDocumentDataSource.GetSubtribusListDetails(ViewModel
                                .SubtribussesCollection);
                        var document = new Tbl60SubtribussesListDocument(model, ViewModel.SubtribussesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 3 when ViewModel.SelectedTribusTabIndex == 2:
            {
                filePath = $"{filePath}PdfListTribus.pdf";
                {
                    if (ViewModel.TribussesCollection.Count != 0)
                    {
                        var model =
                            Tbl57TribussesListDocumentDataSource.GetTribusListDetails(ViewModel.TribussesCollection);
                        var document = new Tbl57TribussesListDocument(model, ViewModel.TribussesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 3 when ViewModel.SelectedTribusTabIndex == 3:
            {
                filePath = $"{filePath}PdfListSupertribus.pdf";
                {
                    if (ViewModel.SupertribussesCollection.Count != 0)
                    {
                        var model =
                            Tbl54SupertribussesListDocumentDataSource.GetSupertribusListDetails(ViewModel
                                .SupertribussesCollection);
                        var document = new Tbl54SupertribussesListDocument(model, ViewModel.SupertribussesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 2:
            {
                filePath = $"{filePath}PdfListPLSpecies.pdf";
                {
                    if (ViewModel.PlSpeciessesCollection.Count != 0)
                    {
                        var model =
                            Tbl72PlSpeciessesListDocumentDataSource.GetPlSpeciesListDetails(ViewModel
                                .PlSpeciessesCollection);
                        var document = new Tbl72PlSpeciessesListDocument(model, ViewModel.PlSpeciessesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 1:
            {
                filePath = $"{filePath}PdfListFiSpecies.pdf";
                {
                    if (ViewModel.FiSpeciessesCollection.Count != 0)
                    {
                        var model =
                            Tbl69FiSpeciessesListDocumentDataSource.GetFiSpeciesListDetails(ViewModel
                                .FiSpeciessesCollection);
                        var document = new Tbl69FiSpeciessesListDocument(model, ViewModel.FiSpeciessesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
            case 0:
            {
                filePath = $"{filePath}PdfListGenus.pdf";
                {
                    if (ViewModel.GenussesCollection.Count != 0)
                    {
                        var model =
                            Tbl66GenussesListDocumentDataSource.GetGenusListDetails(ViewModel.GenussesCollection);
                        var document = new Tbl66GenussesListDocument(model, ViewModel.GenussesCollection);
                        document.GeneratePdf(filePath);
                    }
                }
                break;
            }
        }

        PrintPdfMethod(filePath);
    }

    private static async void PrintPdfMethod(string? filePath)
    {
        // Process.Start("explorer.exe", filePath);

        {
            try
            {
                await File.ReadAllTextAsync(filePath!);
            }
            catch (DirectoryNotFoundException)
            {
                AllDialogs.NoFileFoundInfoWarnDialogAsync();
                return;
            }
            catch (FileNotFoundException)
            {
                AllDialogs.NoFileFoundInfoWarnDialogAsync();
                return;
            }

            var process = new Process
            {
                StartInfo = new ProcessStartInfo(filePath!)
                {
                    UseShellExecute = true
                }
            };
            process.Start();
        }
    }

    public async void HyperlinkRegnumDoubleTapped()
    {
        //var tag = ((ListView)sender).Tag;
        //if (tag != null)
        //{
        //    ViewModel.RegnumSelected.RegnumId = (int)tag;
        //}

        if (ViewModel.RegnumSelected.RegnumId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.RegnumSelected.RegnumId, "Tbl03Regnums");
        if (RegnumDialog != null)
        {
            RegnumDialog.Title = ViewModel.RegnumSelected.RegnumFullName;
            await RegnumDialog.ShowAsync();
        }
    }

    public async void HyperlinkPhylumDoubleTapped()
    {

        if (ViewModel.PhylumSelected.PhylumId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.PhylumSelected.PhylumId, "Tbl06Phylums");
        if (PhylumDialog != null)
        {
            PhylumDialog.Title = ViewModel.PhylumSelected.PhylumName;
            await PhylumDialog.ShowAsync();
        }
    }
    private async void Grid_PhylumDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.PhylumSelected.PhylumId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.PhylumSelected.PhylumId, "Tbl06Phylums");
        if (PhylumDialog != null)
        {
            PhylumDialog.Title = ViewModel.PhylumSelected.PhylumName;
            await PhylumDialog.ShowAsync();
        }
    }

    public async void HyperlinkDivisionDoubleTapped()
    {

        if (ViewModel.DivisionSelected.DivisionId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.DivisionSelected.DivisionId, "Tbl09Divisions");
        if (DivisionDialog != null)
        {
            DivisionDialog.Title = ViewModel.DivisionSelected.DivisionName;
            await DivisionDialog.ShowAsync();
        }
    }
    private async void Grid_DivisionDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.DivisionSelected.DivisionId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.DivisionSelected.DivisionId, "Tbl09Divisions");
        if (DivisionDialog != null)
        {
            DivisionDialog.Title = ViewModel.DivisionSelected.DivisionName;
            await DivisionDialog.ShowAsync();
        }
    }

    public async void HyperlinkSubphylumDoubleTapped()
    {

        if (ViewModel.SubphylumSelected.SubphylumId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SubphylumSelected.SubphylumId, "Tbl12Subphylums");
        if (SubphylumDialog != null)
        {
            SubphylumDialog.Title = ViewModel.SubphylumSelected.SubphylumName;
            await SubphylumDialog.ShowAsync();
        }
    }
    private async void Grid_SubphylumDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.SubphylumSelected.SubphylumId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SubphylumSelected.SubphylumId, "Tbl12Subphylums");
        if (SubphylumDialog != null)
        {
            SubphylumDialog.Title = ViewModel.SubphylumSelected.SubphylumName;
            await SubphylumDialog.ShowAsync();
        }
    }

    public async void HyperlinkSubdivisionDoubleTapped()
    {

        if (ViewModel.SubdivisionSelected.SubdivisionId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SubdivisionSelected.SubdivisionId, "Tbl15Subdivisions");
        if (SubdivisionDialog != null)
        {
            SubdivisionDialog.Title = ViewModel.SubdivisionSelected.SubdivisionName;
            await SubdivisionDialog.ShowAsync();
        }
    }
    private async void Grid_SubdivisionDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.SubdivisionSelected.SubdivisionId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SubdivisionSelected.SubdivisionId, "Tbl15Subdivisions");
        if (SubdivisionDialog != null)
        {
            SubdivisionDialog.Title = ViewModel.SubdivisionSelected.SubdivisionName;
            await SubdivisionDialog.ShowAsync();
        }
    }

    public async void HyperlinkSuperclassDoubleTapped()
    {

        if (ViewModel.SuperclassSelected.SuperclassId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SuperclassSelected.SuperclassId, "Tbl18Superclasses");
        if (SuperclassDialog != null)
        {
            SuperclassDialog.Title = ViewModel.SuperclassSelected.SuperclassName;
            await SuperclassDialog.ShowAsync();
        }
    }
    private async void Grid_SuperclassDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    { 
        if (ViewModel.SuperclassSelected.SuperclassId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SuperclassSelected.SuperclassId, "Tbl18Superclasses");
        if (SuperclassDialog != null)
        {
            SuperclassDialog.Title = ViewModel.SuperclassSelected.SuperclassName;
            await SuperclassDialog.ShowAsync();
        }
    }

    public async void HyperlinkClassDoubleTapped()
    {

        if (ViewModel.ClassSelected.ClassId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.ClassSelected.ClassId, "Tbl21Classes");
        if (ClassDialog != null)
        {
            ClassDialog.Title = ViewModel.ClassSelected.ClassName;
            await ClassDialog.ShowAsync();
        }
    }
    private async void Grid_ClassDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.ClassSelected.ClassId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.ClassSelected.ClassId, "Tbl21Classes");
        if (ClassDialog != null)
        {
            ClassDialog.Title = ViewModel.ClassSelected.ClassName;
            await ClassDialog.ShowAsync();
        }
    }

    public async void HyperlinkSubclassDoubleTapped()
    {
        if (ViewModel.SubclassSelected.SubclassId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SubclassSelected.SubclassId, "Tbl24Subclasses");
        if (SubclassDialog != null)
        {
            SubclassDialog.Title = ViewModel.SubclassSelected.SubclassName;
            await SubclassDialog.ShowAsync();
        }
    }
    private async void Grid_SubclassDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.SubclassSelected.SubclassId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SubclassSelected.SubclassId, "Tbl24Subclasses");
        if (SubclassDialog != null)
        {
            SubclassDialog.Title = ViewModel.SubclassSelected.SubclassName;
            await SubclassDialog.ShowAsync();
        }
    }

    public async void HyperlinkInfraclassDoubleTapped()
    {

        if (ViewModel.InfraclassSelected.InfraclassId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.InfraclassSelected.InfraclassId, "Tbl27Infraclasses");
        if (InfraclassDialog != null)
        {
            InfraclassDialog.Title = ViewModel.InfraclassSelected.InfraclassName;
            await InfraclassDialog.ShowAsync();
        }
    }
    private async void Grid_InfraclassDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.InfraclassSelected.InfraclassId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.InfraclassSelected.InfraclassId, "Tbl27Infraclasses");
        if (InfraclassDialog != null)
        {
            InfraclassDialog.Title = ViewModel.InfraclassSelected.InfraclassName;
            await InfraclassDialog.ShowAsync();
        }
    }

    public async void HyperlinkLegioDoubleTapped()
    {

        if (ViewModel.LegioSelected.LegioId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.LegioSelected.LegioId, "Tbl30Legios");
        if (LegioDialog != null)
        {
            LegioDialog.Title = ViewModel.LegioSelected.LegioName;
            await LegioDialog.ShowAsync();
        }
    }
    private async void Grid_LegioDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.LegioSelected.LegioId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.LegioSelected.LegioId, "Tbl30Legios");
        if (LegioDialog != null)
        {
            LegioDialog.Title = ViewModel.LegioSelected.LegioName;
            await LegioDialog.ShowAsync();
        }
    }

    public async void HyperlinkOrdoDoubleTapped()
    {

        if (ViewModel.OrdoSelected.OrdoId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.OrdoSelected.OrdoId, "Tbl33Ordos");
        if (OrdoDialog != null)
        {
            OrdoDialog.Title = ViewModel.OrdoSelected.OrdoName;
            await OrdoDialog.ShowAsync();
        }
    }
    private async void Grid_OrdoDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.OrdoSelected.OrdoId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.OrdoSelected.OrdoId, "Tbl33Ordos");
        if (OrdoDialog != null)
        {
            OrdoDialog.Title = ViewModel.OrdoSelected.OrdoName;
            await OrdoDialog.ShowAsync();
        }
    }

    public async void HyperlinkSubordoDoubleTapped()
    {

        if (ViewModel.SubordoSelected.SubordoId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SubordoSelected.SubordoId, "Tbl36Subordos");
        if (SubordoDialog != null)
        {
            SubordoDialog.Title = ViewModel.SubordoSelected.SubordoName;
            await SubordoDialog.ShowAsync();
        }
    }
    private async void Grid_SubordoDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.SubordoSelected.SubordoId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SubordoSelected.SubordoId, "Tbl36Subordos");
        if (SubordoDialog != null)
        {
            SubordoDialog.Title = ViewModel.SubordoSelected.SubordoName;
            await SubordoDialog.ShowAsync();
        }
    }

    public async void HyperlinkInfraordoDoubleTapped()
    {

        if (ViewModel.InfraordoSelected.InfraordoId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.InfraordoSelected.InfraordoId, "Tbl39Infraordos");
        if (InfraordoDialog != null)
        {
            InfraordoDialog.Title = ViewModel.InfraordoSelected.InfraordoName;
            await InfraordoDialog.ShowAsync();
        }
    }
    private async void Grid_InfraordoDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.InfraordoSelected.InfraordoId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.InfraordoSelected.InfraordoId, "Tbl39Infraordos");
        if (InfraordoDialog != null)
        {
            InfraordoDialog.Title = ViewModel.InfraordoSelected.InfraordoName;
            await InfraordoDialog.ShowAsync();
        }
    }

    public async void HyperlinkSuperfamilyDoubleTapped()
    {

        if (ViewModel.SuperfamilySelected.SuperfamilyId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SuperfamilySelected.SuperfamilyId, "Tbl42Superfamilies");
        if (SuperfamilyDialog != null)
        {
            SuperfamilyDialog.Title = ViewModel.SuperfamilySelected.SuperfamilyName;
            await SuperfamilyDialog.ShowAsync();
        }
    }
    private async void Grid_SuperfamilyDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.SuperfamilySelected.SuperfamilyId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SuperfamilySelected.SuperfamilyId, "Tbl42Superfamilies");
        if (SuperfamilyDialog != null)
        {
            SuperfamilyDialog.Title = ViewModel.SuperfamilySelected.SuperfamilyName;
            await SuperfamilyDialog.ShowAsync();
        }
    }

    public async void HyperlinkFamilyDoubleTapped()
    {

        if (ViewModel.FamilySelected.FamilyId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.FamilySelected.FamilyId, "Tbl45Families");
        if (FamilyDialog != null)
        {
            FamilyDialog.Title = ViewModel.FamilySelected.FamilyName;
            await FamilyDialog.ShowAsync();
        }
    }
    private async void Grid_FamilyDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.FamilySelected.FamilyId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.FamilySelected.FamilyId, "Tbl45Families");
        if (FamilyDialog != null)
        {
            FamilyDialog.Title = ViewModel.FamilySelected.FamilyName;
            await FamilyDialog.ShowAsync();
        }
    }

    public async void HyperlinkSubfamilyDoubleTapped()
    {

        if (ViewModel.SubfamilySelected.SubfamilyId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SubfamilySelected.SubfamilyId, "Tbl48Subfamilies");
        if (SubfamilyDialog != null)
        {
            SubfamilyDialog.Title = ViewModel.SubfamilySelected.SubfamilyName;
            await SubfamilyDialog.ShowAsync();
        }
    }
    private async void Grid_SubfamilyDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.SubfamilySelected.SubfamilyId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SubfamilySelected.SubfamilyId, "Tbl48Subfamilies");
        if (SubfamilyDialog != null)
        {
            SubfamilyDialog.Title = ViewModel.SubfamilySelected.SubfamilyName;
            await SubfamilyDialog.ShowAsync();
        }
    }

    public async void HyperlinkInfrafamilyDoubleTapped()
    {

        if (ViewModel.InfrafamilySelected.InfrafamilyId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.InfrafamilySelected.InfrafamilyId, "Tbl51Infrafamilies");
        if (InfrafamilyDialog != null)
        {
            InfrafamilyDialog.Title = ViewModel.InfrafamilySelected.InfrafamilyName;
            await InfrafamilyDialog.ShowAsync();
        }
    }
    private async void Grid_InfrafamilyDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.InfrafamilySelected.InfrafamilyId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.InfrafamilySelected.InfrafamilyId, "Tbl51Infrafamilies");
        if (InfrafamilyDialog != null)
        {
            InfrafamilyDialog.Title = ViewModel.InfrafamilySelected.InfrafamilyName;
            await InfrafamilyDialog.ShowAsync();
        }
    }

    public async void HyperlinkSupertribusDoubleTapped()
    {
        if (ViewModel.SupertribusSelected.SupertribusId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SupertribusSelected.SupertribusId, "Tbl54Supertribusses");
        if (SupertribusDialog != null)
        {
            SupertribusDialog.Title = ViewModel.SupertribusSelected.SupertribusName;
            await SupertribusDialog.ShowAsync();
        }
    }
    private async void Grid_SupertribusDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.SupertribusSelected.SupertribusId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SupertribusSelected.SupertribusId, "Tbl54Supertribusses");
        if (SupertribusDialog != null)
        {
            SupertribusDialog.Title = ViewModel.SupertribusSelected.SupertribusName;
            await SupertribusDialog.ShowAsync();
        }
    }

    public async void HyperlinkTribusDoubleTapped()
    {

        if (ViewModel.TribusSelected.TribusId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.TribusSelected.TribusId, "Tbl57Tribusses");
        if (TribusDialog != null)
        {
            TribusDialog.Title = ViewModel.TribusSelected.TribusName;
            await TribusDialog.ShowAsync();
        }
    }
    private async void Grid_TribusDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.TribusSelected.TribusId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.TribusSelected.TribusId, "Tbl57Tribusses");
        if (TribusDialog != null)
        {
            TribusDialog.Title = ViewModel.TribusSelected.TribusName;
            await TribusDialog.ShowAsync();
        }
    }

    public async void HyperlinkSubtribusDoubleTapped()
    {

        if (ViewModel.SubtribusSelected.SubtribusId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SubtribusSelected.SubtribusId, "Tbl60Subtribusses");
        if (SubtribusDialog != null)
        {
            SubtribusDialog.Title = ViewModel.SubtribusSelected.SubtribusName;
            await SubtribusDialog.ShowAsync();
        }
    }
    private async void Grid_SubtribusDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.SubtribusSelected.SubtribusId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.SubtribusSelected.SubtribusId, "Tbl60Subtribusses");
        if (SubtribusDialog != null)
        {
            SubtribusDialog.Title = ViewModel.SubtribusSelected.SubtribusName;
            await SubtribusDialog.ShowAsync();
        }
    }

    public async void HyperlinkInfratribusDoubleTapped()
    {

        if (ViewModel.InfratribusSelected.InfratribusId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.InfratribusSelected.InfratribusId, "Tbl63Infratribusses");
        if (InfratribusDialog != null)
        {
            InfratribusDialog.Title = ViewModel.InfratribusSelected.InfratribusName;
            await InfratribusDialog.ShowAsync();
        }
    }
    private async void Grid_InfratribusDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.InfratribusSelected.InfratribusId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.InfratribusSelected.InfratribusId, "Tbl63Infratribusses");
        if (InfratribusDialog != null)
        {
            InfratribusDialog.Title = ViewModel.InfratribusSelected.InfratribusName;
            await InfratribusDialog.ShowAsync();
        }
    }

    public async void HyperlinkGenusDoubleTapped()
    {

        if (ViewModel.GenusSelected.GenusId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.GenusSelected.GenusId, "Tbl66Genusses");


        if (GenusDialog != null)
        {
            GenusDialog.Title = ViewModel.GenusSelected.GenusName;
            await GenusDialog.ShowAsync();
        }
    }

    public async void HyperlinkFiSpeciesDoubleTapped()
        {
            if (ViewModel.FiSpeciesSelected.FiSpeciesId == 0)
            {
                return;
            }
            HideAllActiveDialogs();

            ViewModel.ReportSearchAll(ViewModel.FiSpeciesSelected.FiSpeciesId, "Tbl69FiSpeciesses");
            if (FiSpeciesDialog != null)
            {
                FiSpeciesDialog.Title = ViewModel.FiSpeciesSelected.FiSpeciesFullName;
                await FiSpeciesDialog.ShowAsync();
            }
        }

        public async void HyperlinkPlSpeciesDoubleTapped()
        {

            if (ViewModel.PlSpeciesSelected.PlSpeciesId == 0)
            {
                return;
            }

            HideAllActiveDialogs();

            ViewModel.ReportSearchAll(ViewModel.PlSpeciesSelected.PlSpeciesId, "Tbl72PlSpeciesses");
            if (PlSpeciesDialog != null)
            {
                PlSpeciesDialog.Title = ViewModel.PlSpeciesSelected.PlSpeciesFullName;
                await PlSpeciesDialog.ShowAsync();
            }
        }

        public async void HyperlinkFiSpeciesSubDoubleTapped()
        {

            if (ViewModel.FiSpeciesSubSelected.FiSpeciesId == 0)
            {
                return;
            }

            HideAllActiveDialogs();

            ViewModel.ReportSearchAll(ViewModel.FiSpeciesSubSelected.FiSpeciesId, "Tbl69FiSpeciessesSub");
            if (FiSpeciesSubDialog != null)
            {
                FiSpeciesSubDialog.Title = ViewModel.FiSpeciesSubSelected.FiSpeciesFullName;
                await FiSpeciesSubDialog.ShowAsync();
            }
        }

        public async void HyperlinkPlSpeciesSubDoubleTapped()
        {

            if (ViewModel.PlSpeciesSubSelected.PlSpeciesId == 0)
            {
                return;
            }

            HideAllActiveDialogs();

            ViewModel.ReportSearchAll(ViewModel.PlSpeciesSubSelected.PlSpeciesId, "Tbl72PlSpeciessesSub");
            if (PlSpeciesSubDialog != null)
            {
                PlSpeciesSubDialog.Title = ViewModel.PlSpeciesSubSelected.PlSpeciesFullName;
                await PlSpeciesSubDialog.ShowAsync();
            }
        }
        private async void Grid_GenusDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (ViewModel.GenusSelected.GenusId == 0)
            {
                return;
            }

            HideAllActiveDialogs();

            ViewModel.ReportSearchAll(ViewModel.GenusSelected.GenusId, "Tbl66Genusses");


            if (GenusDialog != null)
            {
                GenusDialog.Title = ViewModel.GenusSelected.GenusName;
                await GenusDialog.ShowAsync();
            }
        }


    private async  void Grid_FiSpeciesOrSubDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (ViewModel.FiSpeciesSelected.FiSpeciesId == 0)
            {
                return;
            }

            if (ViewModel.FiSpeciesSelected.Subspecies != null)  //SubDialog
            {
                HideAllActiveDialogs();

                ViewModel.ReportSearchAll(ViewModel.FiSpeciesSelected.FiSpeciesId, "Tbl69FiSpeciessesSub");
                if (FiSpeciesSubDialog != null)
                {
                    FiSpeciesSubDialog.Title = ViewModel.FiSpeciesSelected.FiSpeciesFullName;
                  await   FiSpeciesSubDialog.ShowAsync();
                }
            }
            else
            {
                HideAllActiveDialogs();

                ViewModel.ReportSearchAll(ViewModel.FiSpeciesSelected.FiSpeciesId, "Tbl69FiSpeciesses");
                if (FiSpeciesDialog != null)
                {
                    FiSpeciesDialog.Title = ViewModel.FiSpeciesSelected.FiSpeciesFullName;
                  await   FiSpeciesDialog.ShowAsync();
                }
            }
        }
    private async void Grid_FiSpeciesSubDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.FiSpeciesSubSelected.FiSpeciesId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.FiSpeciesSubSelected.FiSpeciesId, "Tbl69FiSpeciessesSub");


        if (FiSpeciesSubDialog != null)
        {
            FiSpeciesSubDialog.Title = ViewModel.FiSpeciesSubSelected.FiSpeciesFullName;
            await FiSpeciesSubDialog.ShowAsync();
        }
    }

    private async void Grid_PlSpeciesOrSubDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (ViewModel.PlSpeciesSelected.PlSpeciesId == 0)
            {
                return;
            }

            if (ViewModel.PlSpeciesSelected.Subspecies != null)  //SubDialog
            {
                HideAllActiveDialogs();

                ViewModel.ReportSearchAll(ViewModel.PlSpeciesSelected.PlSpeciesId, "Tbl72PlSpeciessesSub");
                if (PlSpeciesSubDialog != null)
                {
                    PlSpeciesSubDialog.Title = ViewModel.PlSpeciesSelected.PlSpeciesFullName;
                    await PlSpeciesSubDialog.ShowAsync();
                }
            }
            else
            {
                HideAllActiveDialogs();

                ViewModel.ReportSearchAll(ViewModel.PlSpeciesSelected.PlSpeciesId, "Tbl72PlSpeciesses");
                if (PlSpeciesDialog != null)
                {
                    PlSpeciesDialog.Title = ViewModel.PlSpeciesSelected.PlSpeciesFullName;
                    await PlSpeciesDialog.ShowAsync();
                }
            }
        }
    private async void Grid_PlSpeciesSubDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        if (ViewModel.PlSpeciesSubSelected.PlSpeciesId == 0)
        {
            return;
        }

        HideAllActiveDialogs();

        ViewModel.ReportSearchAll(ViewModel.PlSpeciesSubSelected.PlSpeciesId, "Tbl72PlSpeciessesSub");


        if (PlSpeciesSubDialog != null)
        {
            PlSpeciesSubDialog.Title = ViewModel.PlSpeciesSubSelected.PlSpeciesFullName;
            await PlSpeciesSubDialog.ShowAsync();
        }
    }

    private void HideAllActiveDialogs()
        {
            PlSpeciesSubDialog?.Hide();
            PlSpeciesDialog?.Hide();
            FiSpeciesSubDialog?.Hide();
            FiSpeciesDialog?.Hide();
            GenusDialog?.Hide();
            InfratribusDialog?.Hide();
            SubtribusDialog?.Hide();
            TribusDialog?.Hide();
            SupertribusDialog?.Hide();
            InfrafamilyDialog?.Hide();
            SubfamilyDialog?.Hide();
            FamilyDialog?.Hide();
            SuperfamilyDialog?.Hide();
            InfraordoDialog?.Hide();
            SubordoDialog?.Hide();
            OrdoDialog?.Hide();
            LegioDialog?.Hide();
            InfraclassDialog?.Hide();
            SubclassDialog?.Hide();
            ClassDialog?.Hide();
            SuperclassDialog?.Hide();
            SubdivisionDialog?.Hide();
            DivisionDialog?.Hide();
            SubphylumDialog?.Hide();
            PhylumDialog?.Hide();
            RegnumDialog?.Hide();

        }

}

