﻿using System.Collections.ObjectModel;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Tbl18Superclass = ATIS.WinUi.Models.Tbl18Superclass;


//    Tbl24SubclassesDocumentDataSourceSkriptdatum:  16.06.2023  18:32    

namespace ATIS.WinUi.Views.Report.Pdf.Tbl24Subclasses;

public abstract class Tbl24SubclassesDocumentDataSource : ObservableObject
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


    #endregion [Private Data Members]      

    //    Part 1    


    public static Tbl24SubclassesPdfModel GetSubclassDetails(int subclassId)
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
        InfraclassesCollection = dataService.CollInfraclassesBySubclassIdAndHash(subclassId);

        //-----------------------------------------------------------------------------
        ExpertsCollection = dataService.CollExpertsBySubclassId(subclassId);
        SourcesCollection = dataService.CollSourcesBySubclassId(subclassId);
        AuthorsCollection = dataService.CollAuthorsBySubclassId(subclassId);
        //------------------------------------------------------------------------------
        CommentsCollection = dataService.CollCommentsBySubclassId(subclassId);

        _subclassSingle = dataService.GetSubclassSingleBySubclassIdAndHash(subclassId);
        _classSingle = dataService.GetClassSingleByClassIdAndHash(classId);
        _superclassSingle = dataService.GetSuperclassSingleBySuperclassIdAndHash(superclassId);

        return new Tbl24SubclassesPdfModel
        {
            SubclassName = _subclassSingle.SubclassName,
            CountId = _subclassSingle.CountId,
            Synonym = _subclassSingle.Synonym,
            Author = _subclassSingle.Author,
            AuthorYear = _subclassSingle.AuthorYear,
            Info = _subclassSingle.Info,
            Valid = _subclassSingle.Valid,
            ValidYear = _subclassSingle.ValidYear,
            EngName = _subclassSingle.EngName,
            GerName = _subclassSingle.GerName,
            FraName = _subclassSingle.FraName,
            PorName = _subclassSingle.PorName,
            Memo = _subclassSingle.Memo,
            WriterDate = _subclassSingle.WriterDate,
            UpdaterDate = _subclassSingle.UpdaterDate,
            //------------------------------- 

            RegnumTree = GenrateModels.GenRegnums.GenerateRegnums(_regnumSingle),
            PhylumTree = GenrateModels.GenPhylums.GeneratePhylums(_phylumSingle),
            SubphylumTree = GenrateModels.GenSubphylums.GenerateSubphylums(_subphylumSingle),
            DivisionTree = GenrateModels.GenDivisions.GenerateDivisions(_divisionSingle),
            SubdivisionTree = GenrateModels.GenSubdivisions.GenerateSubdivisions(_subdivisionSingle),
            SuperclassTree = GenrateModels.GenSuperclasses.GenerateSuperclasses(_superclassSingle),
            ClassTree = GenrateModels.GenClasses.GenerateClasses(_classSingle),
            SubclassTree = GenrateModels.GenSubclasses.GenerateSubclasses(_subclassSingle)
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

    public static ObservableCollection<Tbl90Reference> AuthorsCollection { get; set; } = null!;

    public static ObservableCollection<Tbl90Reference> SourcesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl90Reference> ExpertsCollection { get; set; } = null!;

    public static ObservableCollection<Tbl93Comment> CommentsCollection { get; set; } = null!;
    #endregion "Collections"          

    public static void ClearAllCollections()
    {
        _subclassSingle = new Tbl24Subclass();
        _classSingle = new Tbl21Class();
        _superclassSingle = new Tbl18Superclass();
        _subdivisionSingle = new Tbl15Subdivision();
        _divisionSingle = new Tbl09Division();
        _subphylumSingle = new Tbl12Subphylum();
        _phylumSingle = new Tbl06Phylum();
        _regnumSingle = new Tbl03Regnum();

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
