﻿using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Tbl18Superclass = ATIS.WinUi.Models.Tbl18Superclass;


//    Tbl51InfrafamiliesDocumentDataSourceSkriptdatum:  16.04.2023  10:32    

namespace ATIS.WinUi.Views.Report.Pdf.Tbl51Infrafamilies;

public abstract class Tbl51InfrafamiliesDocumentDataSource : ObservableObject
{
    #region [Private Data Members]   

    private static Tbl03Regnum _regnumSingle = new();
    private static Tbl06Phylum _phylumSingle = new();
    private static Tbl09Division _divisionSingle = new();
    private static Tbl12Subphylum _subphylumSingle = new();
    private static Tbl15Subdivision _subdivisionSingle = new();
    private static Tbl18Superclass _superclassSingle = new();
    private static Tbl21Class _classSingle = new();
    private static Tbl24Subclass _subclassSingle = new();
    private static Tbl27Infraclass _infraclassSingle = new();
    private static Tbl30Legio _legioSingle = new();
    private static Tbl33Ordo _ordoSingle = new();
    private static Tbl36Subordo _subordoSingle = new();
    private static Tbl39Infraordo _infraordoSingle = new();
    private static Tbl42Superfamily _superfamilySingle = new();
    private static Tbl45Family _familySingle = new();
    private static Tbl48Subfamily _subfamilySingle = new();
    private static Tbl51Infrafamily _infrafamilySingle = new();


    #endregion [Private Data Members]      


    //    Part 1    


    public static Tbl51InfrafamiliesPdfModel GetInfrafamilyDetails(int infrafamilyId)

    {
        #region [Private Data Members]

        var dataService = new DataService();
        var context = new AtisDbContext();
        var fishId = 0;
        var plantId = 0;

        #endregion [Private Data Members]

        ClearAllCollections();

        if (context.Tbl15Subdivisions != null)
        {
            var plantaeRegnum = context.Tbl15Subdivisions.FirstOrDefault(e => e.SubdivisionName == "Plantae#Regnum#");
            if (plantaeRegnum != null)
            {
                plantId = plantaeRegnum.SubdivisionId;
            }
        }

        if (context.Tbl12Subphylums != null)
        {
            var animaliaRegnum = context.Tbl12Subphylums.FirstOrDefault(e => e.SubphylumName == "Animalia#Regnum#");

            if (animaliaRegnum != null)
            {
                fishId = animaliaRegnum.SubphylumId;
            }
        }

        InfrafamiliesCollection = dataService.CollInfrafamiliesByInfrafamilyIdAndHash(infrafamilyId);
        var subfamilyId = dataService.SubfamilyIdFromInfrafamiliesCollectionSelect(infrafamilyId);
        SubfamiliesCollection = dataService.CollSubfamiliesBySubfamilyIdAndHash(subfamilyId);
        var familyId = dataService.FamilyIdFromSubfamiliesCollectionSelect(subfamilyId);
        FamiliesCollection = dataService.CollFamiliesByFamilyIdAndHash(familyId);
        var superfamilyId = dataService.SuperfamilyIdFromFamiliesCollectionSelect(familyId);
        SuperfamiliesCollection = dataService.CollSuperfamiliesBySuperfamilyIdAndHash(superfamilyId);
        var infraordoId = dataService.InfraordoIdFromSuperfamiliesCollectionSelect(superfamilyId);
        InfraordosCollection = dataService.CollInfraordosByInfraordoIdAndHash(infraordoId);
        var subordoId = dataService.SubordoIdFromInfraordosCollectionSelect(infraordoId);
        SubordosCollection = dataService.CollSubordosBySubordoIdAndHash(subordoId);
        var ordoId = dataService.OrdoIdFromSubordosCollectionSelect(subordoId);
        OrdosCollection = dataService.CollOrdosByOrdoIdAndHash(ordoId);
        var legioId = dataService.LegioIdFromOrdosCollectionSelect(ordoId);
        LegiosCollection = dataService.CollLegiosByLegioIdAndHash(legioId);
        var infraclassId = dataService.InfraclassIdFromLegiosCollectionSelect(legioId);
        InfraclassesCollection = dataService.CollInfraclassesByInfraclassIdAndHash(infraclassId);
        var subclassId = dataService.SubclassIdFromInfraclassesCollectionSelect(infraclassId);
        SubclassesCollection = dataService.CollSubclassesBySubclassIdAndHash(subclassId);
        var classId = dataService.ClassIdFromSubclassesCollectionSelect(subclassId);
        ClassesCollection = dataService.CollClassesByClassIdAndHash(classId);
        var superclassId = dataService.SuperclassIdFromClassesCollectionSelect(classId);
        SuperclassesCollection = dataService.CollSuperclassesBySuperclassIdAndHash(superclassId);
        var subphylumId = dataService.SubphylumIdFromSuperclassesCollectionSelect(superclassId);
        var subdivisionId = dataService.SubdivisionIdFromSuperclassesCollectionSelect(superclassId);

        if (subphylumId == fishId)  //Basis #Subphylum#
        {
            SubdivisionsCollection = dataService.CollSubdivisionsBySubdivisionIdAndHash(subdivisionId);
            var divisionId = dataService.DivisionIdFromSubdivisionsCollectionSelect(subdivisionId);
            DivisionsCollection = dataService.CollDivisionsByDivisionIdAndHash(divisionId);
            var regnumId = dataService.RegnumIdFromDivisionsCollectionSelect(divisionId);
            RegnumsCollection = dataService.CollRegnumsByRegnumIdAndHash(regnumId);

            _regnumSingle = dataService.GetRegnumSingleByRegnumIdAndHash(regnumId);
            _divisionSingle = dataService.GetDivisionSingleByDivisionIdAndHash(divisionId);
            _subdivisionSingle = dataService.GetSubdivisionSingleBySubdivisionIdAndHash(subdivisionId);

        }
        if (subdivisionId == plantId)  //Basis #Subdivision#
        {
            SubphylumsCollection = dataService.CollSubphylumsBySubphylumIdAndHash(subphylumId);
            var phylumId = dataService.PhylumIdFromSubphylumsCollectionSelect(subphylumId);
            PhylumsCollection = dataService.CollPhylumsByPhylumIdAndHash(phylumId);
            var regnumId = dataService.RegnumIdFromPhylumsCollectionSelect(phylumId);
            RegnumsCollection = dataService.CollRegnumsByRegnumIdAndHash(regnumId);

            _regnumSingle = dataService.GetRegnumSingleByRegnumIdAndHash(regnumId);
            _phylumSingle = dataService.GetPhylumSingleByPhylumIdAndHash(phylumId);
            _subphylumSingle = dataService.GetSubphylumSingleBySubphylumIdAndHash(subphylumId);
        }

        //direct children
        SupertribussesCollection = dataService.CollSupertribussesByInfrafamilyIdAndHash(infrafamilyId);

        //-----------------------------------------------------------------------------
        ExpertsCollection = dataService.CollExpertsByInfrafamilyId(infrafamilyId);
        SourcesCollection = dataService.CollSourcesByInfrafamilyId(infrafamilyId);
        AuthorsCollection = dataService.CollAuthorsByInfrafamilyId(infrafamilyId);
        //------------------------------------------------------------------------------
        CommentsCollection = dataService.CollCommentsByInfrafamilyId(infrafamilyId);

        _infrafamilySingle = dataService.GetInfrafamilySingleByInfrafamilyIdAndHash(infrafamilyId);
        _subfamilySingle = dataService.GetSubfamilySingleBySubfamilyIdAndHash(subfamilyId);
        _familySingle = dataService.GetFamilySingleByFamilyIdAndHash(familyId);
        _superfamilySingle = dataService.GetSuperfamilySingleBySuperfamilyIdAndHash(superfamilyId);
        _infraordoSingle = dataService.GetInfraordoSingleByInfraordoIdAndHash(infraordoId);
        _subordoSingle = dataService.GetSubordoSingleBySubordoIdAndHash(subordoId);
        _ordoSingle = dataService.GetOrdoSingleByOrdoIdAndHash(ordoId);
        _legioSingle = dataService.GetLegioSingleByLegioIdAndHash(legioId);
        _infraclassSingle = dataService.GetInfraclassSingleByInfraclassIdAndHash(infraclassId);
        _subclassSingle = dataService.GetSubclassSingleBySubclassIdAndHash(subclassId);
        _classSingle = dataService.GetClassSingleByClassIdAndHash(classId);
        _superclassSingle = dataService.GetSuperclassSingleBySuperclassIdAndHash(superclassId);

        return new Tbl51InfrafamiliesPdfModel
        {
            InfrafamilyName = _infrafamilySingle.InfrafamilyName,
            CountId = _infrafamilySingle.CountId,
            Synonym = _infrafamilySingle.Synonym,
            Author = _infrafamilySingle.Author,
            AuthorYear = _infrafamilySingle.AuthorYear,
            Info = _infrafamilySingle.Info,
            Valid = _infrafamilySingle.Valid,
            ValidYear = _infrafamilySingle.ValidYear,
            EngName = _infrafamilySingle.EngName,
            GerName = _infrafamilySingle.GerName,
            FraName = _infrafamilySingle.FraName,
            PorName = _infrafamilySingle.PorName,
            Memo = _infrafamilySingle.Memo,
            WriterDate = _infrafamilySingle.WriterDate,
            UpdaterDate = _infrafamilySingle.UpdaterDate,
            //------------------------------- 

            RegnumTree = GenrateModels.GenRegnums.GenerateRegnums(_regnumSingle),
            PhylumTree = GenrateModels.GenPhylums.GeneratePhylums(_phylumSingle),
            SubphylumTree = GenrateModels.GenSubphylums.GenerateSubphylums(_subphylumSingle),
            DivisionTree = GenrateModels.GenDivisions.GenerateDivisions(_divisionSingle),
            SubdivisionTree = GenrateModels.GenSubdivisions.GenerateSubdivisions(_subdivisionSingle),
            SuperclassTree = GenrateModels.GenSuperclasses.GenerateSuperclasses(_superclassSingle),
            ClassTree = GenrateModels.GenClasses.GenerateClasses(_classSingle),
            SubclassTree = GenrateModels.GenSubclasses.GenerateSubclasses(_subclassSingle),
            InfraclassTree = GenrateModels.GenInfraclasses.GenerateInfraclasses(_infraclassSingle),
            LegioTree = GenrateModels.GenLegios.GenerateLegios(_legioSingle),
            OrdoTree = GenrateModels.GenOrdos.GenerateOrdos(_ordoSingle),
            SubordoTree = GenrateModels.GenSubordos.GenerateSubordos(_subordoSingle),
            InfraordoTree = GenrateModels.GenInfraordos.GenerateInfraordos(_infraordoSingle),
            SuperfamilyTree = GenrateModels.GenSuperfamilies.GenerateSuperfamilies(_superfamilySingle),
            FamilyTree = GenrateModels.GenFamilies.GenerateFamilies(_familySingle),
            SubfamilyTree = GenrateModels.GenSubfamilies.GenerateSubfamilies(_subfamilySingle),
            InfrafamilyTree = GenrateModels.GenInfrafamilies.GenerateInfrafamilies(_infrafamilySingle)
        };
    }

    #region "Collections"

    public static ObservableCollection<Tbl03Regnum> RegnumsCollection { get; set; } = null!;

    public static ObservableCollection<Tbl06Phylum> PhylumsCollection { get; set; } = null!;

    public static ObservableCollection<Tbl12Subphylum> SubphylumsCollection { get; set; } = null!;

    public static ObservableCollection<Tbl09Division> DivisionsCollection { get; set; } = null!;

    public static ObservableCollection<Tbl15Subdivision> SubdivisionsCollection { get; set; } = null!;

    public static ObservableCollection<Tbl18Superclass> SuperclassesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl21Class> ClassesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl24Subclass> SubclassesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl27Infraclass> InfraclassesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl30Legio> LegiosCollection { get; set; } = null!;

    public static ObservableCollection<Tbl33Ordo> OrdosCollection { get; set; } = null!;

    public static ObservableCollection<Tbl36Subordo> SubordosCollection { get; set; } = null!;

    public static ObservableCollection<Tbl39Infraordo> InfraordosCollection { get; set; } = null!;

    public static ObservableCollection<Tbl42Superfamily> SuperfamiliesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl45Family> FamiliesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl48Subfamily> SubfamiliesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl51Infrafamily> InfrafamiliesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl54Supertribus> SupertribussesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl57Tribus> TribussesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl60Subtribus> SubtribussesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl90Reference> AuthorsCollection { get; set; } = null!;

    public static ObservableCollection<Tbl90Reference> SourcesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl90Reference> ExpertsCollection { get; set; } = null!;

    public static ObservableCollection<Tbl93Comment> CommentsCollection { get; set; } = null!;
    #endregion "Collections"          

    public static void ClearAllCollections()
    {
        _infrafamilySingle = new Tbl51Infrafamily();
        _subfamilySingle = new Tbl48Subfamily();
        _familySingle = new Tbl45Family();
        _superfamilySingle = new Tbl42Superfamily();
        _infraordoSingle = new Tbl39Infraordo();
        _subordoSingle = new Tbl36Subordo();
        _ordoSingle = new Tbl33Ordo();
        _legioSingle = new Tbl30Legio();
        _infraclassSingle = new Tbl27Infraclass();
        _subclassSingle = new Tbl24Subclass();
        _classSingle = new Tbl21Class();
        _superclassSingle = new Tbl18Superclass();
        _subdivisionSingle = new Tbl15Subdivision();
        _divisionSingle = new Tbl09Division();
        _subphylumSingle = new Tbl12Subphylum();
        _phylumSingle = new Tbl06Phylum();
        _regnumSingle = new Tbl03Regnum();

        SubtribussesCollection?.Clear();
        TribussesCollection?.Clear();
        SupertribussesCollection?.Clear();
        InfrafamiliesCollection?.Clear();
        SubfamiliesCollection?.Clear();
        FamiliesCollection?.Clear();
        SuperfamiliesCollection?.Clear();
        InfraordosCollection?.Clear();
        SubordosCollection?.Clear();
        OrdosCollection?.Clear();
        LegiosCollection?.Clear();
        InfraclassesCollection?.Clear();
        SubclassesCollection?.Clear();
        ClassesCollection?.Clear();
        SuperclassesCollection?.Clear();
        SubdivisionsCollection?.Clear();
        SubphylumsCollection?.Clear();
        DivisionsCollection?.Clear();
        PhylumsCollection?.Clear();
        RegnumsCollection?.Clear();

        CommentsCollection?.Clear();
        ExpertsCollection?.Clear();
        SourcesCollection?.Clear();
        AuthorsCollection?.Clear();
    }

}
