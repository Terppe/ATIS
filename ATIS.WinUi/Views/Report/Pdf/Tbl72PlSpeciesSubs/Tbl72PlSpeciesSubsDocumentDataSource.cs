﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATIS.WinUi.Models;
using ATIS.WinUi.Services;
using ATIS.WinUi.Views.Report.Pdf.Tbl72PlSpeciesses;
using CommunityToolkit.Mvvm.ComponentModel;
using Tbl18Superclass = ATIS.WinUi.Models.Tbl18Superclass;

namespace ATIS.WinUi.Views.Report.Pdf.Tbl72PlSpeciesSubs;
internal class Tbl72PlSpeciesSubsDocumentDataSource : ObservableObject
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
    private static Tbl54Supertribus _supertribusSingle = new();
    private static Tbl57Tribus _tribusSingle = new();
    private static Tbl60Subtribus _subtribusSingle = new();
    private static Tbl63Infratribus _infratribusSingle = new();
    private static Tbl66Genus _genusSingle = new();
    private static Tbl72PlSpecies _plspeciesSingle = new();
    private static readonly Tbl78Name _nameSingle = new();
    private static readonly Tbl84Synonym _synonymSingle = new();


    #endregion [Private Data Members]      

    //    Part 1    

    public static Tbl72PlSpeciesSubsPdfModel GetPlSpeciesSubDetails(int plspeciesId)
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

        PlSpeciessesCollection = dataService.CollPlSpeciessesByPlSpeciesId(plspeciesId);
        var genusId = dataService.GenusIdFromPlSpeciessesCollectionSelect(plspeciesId);
        GenussesCollection = dataService.CollGenussesByGenusIdAndHash(genusId);
        var infratribusId = dataService.InfratribusIdFromGenussesCollectionSelect(genusId);
        InfratribussesCollection = dataService.CollInfratribussesByInfratribusIdAndHash(infratribusId);
        var subtribusId = dataService.SubtribusIdFromInfratribussesCollectionSelect(infratribusId);
        SubtribussesCollection = dataService.CollSubtribussesBySubtribusIdAndHash(subtribusId);
        var tribusId = dataService.TribusIdFromSubtribussesCollectionSelect(subtribusId);
        TribussesCollection = dataService.CollTribussesByTribusIdAndHash(tribusId);
        var supertribusId = dataService.SupertribusIdFromTribussesCollectionSelect(tribusId);
        SupertribussesCollection = dataService.CollSupertribussesBySupertribusIdAndHash(supertribusId);
        var infrafamilyId = dataService.InfrafamilyIdFromSupertribussesCollectionSelect(supertribusId);
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

        if (subphylumId == fishId) //Basis #Subphylum#
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

        if (subdivisionId == plantId) //Basis #Subdivision#
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
        NamesCollection = dataService.CollNamesByFiSpeciesIdAndHash(plspeciesId);
        SynonymsCollection = dataService.CollSynonymsByFiSpeciesIdAndHash(plspeciesId);
        ImagesCollection = dataService.CollImagesByFiSpeciesId(plspeciesId);
        GeographicsCollection = dataService.CollGeographicsByFiSpeciesId(plspeciesId);

        //-----------------------------------------------------------------------------
        ExpertsCollection = dataService.CollExpertsByFiSpeciesId(plspeciesId);
        SourcesCollection = dataService.CollSourcesByFiSpeciesId(plspeciesId);
        AuthorsCollection = dataService.CollAuthorsByFiSpeciesId(plspeciesId);
        //------------------------------------------------------------------------------
        CommentsCollection = dataService.CollCommentsByFiSpeciesId(plspeciesId);


        _plspeciesSingle = dataService.GetPlSpeciesSingleByPlSpeciesIdAndHash(plspeciesId);
        _genusSingle = dataService.GetGenusSingleByGenusIdAndHash(genusId);
        _infratribusSingle = dataService.GetInfratribusSingleByInfratribusIdAndHash(infratribusId);
        _subtribusSingle = dataService.GetSubtribusSingleBySubtribusIdAndHash(subtribusId);
        _tribusSingle = dataService.GetTribusSingleByTribusIdAndHash(tribusId);
        _supertribusSingle = dataService.GetSupertribusSingleBySupertribusIdAndHash(supertribusId);
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

        return new Tbl72PlSpeciesSubsPdfModel
        {
            PlSpeciesName = _plspeciesSingle.PlSpeciesName,
            Subspecies = _plspeciesSingle.Subspecies,
            Divers = _plspeciesSingle.Divers,
            GenusId = _plspeciesSingle.GenusId,
            SpeciesgroupId = _plspeciesSingle.SpeciesgroupId,
            CountId = _plspeciesSingle.CountId,
            Valid = _plspeciesSingle.Valid,
            ValidYear = _plspeciesSingle.ValidYear,
            MemoSpecies = _plspeciesSingle.MemoSpecies,
            TradeName = _plspeciesSingle.TradeName,
            Author = _plspeciesSingle.Author,
            AuthorYear = _plspeciesSingle.AuthorYear,
            Importer = _plspeciesSingle.Importer,
            ImportingYear = _plspeciesSingle.ImportingYear,
            BasinHeight = _plspeciesSingle.BasinHeight,
            PlantLength = _plspeciesSingle.PlantLength,
            Difficult1 = _plspeciesSingle.Difficult1,
            Difficult2 = _plspeciesSingle.Difficult2,
            Difficult3 = _plspeciesSingle.Difficult3,
            Difficult4 = _plspeciesSingle.Difficult4,
            MemoTech = _plspeciesSingle.MemoTech,
            Ph1 = _plspeciesSingle.Ph1,
            Ph2 = _plspeciesSingle.Ph2,
            Temp1 = _plspeciesSingle.Temp1,
            Temp2 = _plspeciesSingle.Temp2,
            Hardness1 = _plspeciesSingle.Hardness1,
            Hardness2 = _plspeciesSingle.Hardness2,
            CarboHardness1 = _plspeciesSingle.CarboHardness1,
            CarboHardness2 = _plspeciesSingle.CarboHardness2,
            MemoBuilt = _plspeciesSingle.MemoBuilt,
            MemoColor = _plspeciesSingle.MemoColor,
            MemoReproduction = _plspeciesSingle.MemoReproduction,
            MemoCulture = _plspeciesSingle.MemoCulture,
            MemoGlobal = _plspeciesSingle.MemoGlobal,
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
            InfrafamilyTree = GenrateModels.GenInfrafamilies.GenerateInfrafamilies(_infrafamilySingle),
            SupertribusTree = GenrateModels.GenSupertribusses.GenerateSupertribusses(_supertribusSingle),
            TribusTree = GenrateModels.GenTribusses.GenerateTribusses(_tribusSingle),
            SubtribusTree = GenrateModels.GenSubtribusses.GenerateSubtribusses(_subtribusSingle),
            InfratribusTree = GenrateModels.GenInfratribusses.GenerateInfratribusses(_infratribusSingle),
            GenusTree = GenrateModels.GenGenusses.GenerateGenusses(_genusSingle),
            PlSpeciesTree = GenrateModels.GenPlSpeciesses.GeneratePlSpeciesses(_plspeciesSingle),
            SynonymsTree = GenrateModels.GenSynonyms.GenerateSynonyms(_synonymSingle),
            NamesTree = GenrateModels.GenNames.GenerateNames(_nameSingle)
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

    public static ObservableCollection<Tbl63Infratribus> InfratribussesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl66Genus> GenussesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl69FiSpecies> FiSpeciessesCollection { get; set; } = null!;

    public ObservableCollection<Tbl69FiSpecies> FiSpeciessesSubCollection { get; set; } = null!;

    public static ObservableCollection<Tbl72PlSpecies> PlSpeciessesCollection { get; set; } = null!;

    public ObservableCollection<Tbl69FiSpecies> FiSpeciessesFiSpeciesNameCollection { get; set; } = null!;

    public ObservableCollection<Tbl72PlSpecies> PlSpeciessesSubCollection { get; set; } = null!;

    public ObservableCollection<Tbl72PlSpecies> PlSpeciessesPlSpeciesNameCollection { get; set; } = null!;

    public static ObservableCollection<Tbl78Name> NamesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl81Image> ImagesCollection { get; set; } = null!;

    public static ObservableCollection<Tbl84Synonym> SynonymsCollection { get; set; } = null!;

    public static ObservableCollection<Tbl87Geographic> GeographicsCollection { get; set; } = null!;

    public static ObservableCollection<Tbl90Reference> AuthorsCollection { get; set; } = null!;

    public ObservableCollection<Tbl90Reference> Tbl90RefAuthorsList { get; set; } = null!;

    public static ObservableCollection<Tbl90Reference> SourcesCollection { get; set; } = null!;

    public ObservableCollection<Tbl90Reference> Tbl90RefSourcesList { get; set; } = null!;

    public static ObservableCollection<Tbl90Reference> ExpertsCollection { get; set; } = null!;

    public ObservableCollection<Tbl90Reference> Tbl90RefExpertsList { get; set; } = null!;

    public static ObservableCollection<Tbl93Comment> CommentsCollection { get; set; } = null!;
    #endregion "Collections"          

    public static void ClearAllCollections()
    {
        _plspeciesSingle = new Tbl72PlSpecies();
        _genusSingle = new Tbl66Genus();
        _infratribusSingle = new Tbl63Infratribus();
        _subtribusSingle = new Tbl60Subtribus();
        _tribusSingle = new Tbl57Tribus();
        _supertribusSingle = new Tbl54Supertribus();
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

        PlSpeciessesCollection?.Clear();
        GenussesCollection?.Clear();
        InfratribussesCollection?.Clear();
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

        GeographicsCollection?.Clear();
        SynonymsCollection?.Clear();
        ImagesCollection?.Clear();
        NamesCollection?.Clear();

        CommentsCollection?.Clear();
        ExpertsCollection?.Clear();
        SourcesCollection?.Clear();
        AuthorsCollection?.Clear();

    }

}
