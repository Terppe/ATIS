using System.Collections.ObjectModel;
using ATIS.WinUi.Models;
using Tbl18Superclass = ATIS.WinUi.Models.Tbl18Superclass;

namespace ATIS.WinUi.Contracts.Services;
public interface IDataService
{
    #region CRUD

    #region Regnum

    #region Get Regnum
    ObservableCollection<Tbl03Regnum>? GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum();
    Task<ObservableCollection<Tbl03Regnum>> GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromSearchNameOrId(
        string? searchName);
    ObservableCollection<Tbl03Regnum> GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromRegnumId(int id);
    Tbl03Regnum GetRegnumSingleByRegnumId(int id);
    Task<Tbl03Regnum> GetRegnumSingleFirstDataset();
    Task<ObservableCollection<Tbl03Regnum>> GetLastDatasetInTbl03Regnums();
    #endregion

    #region Copy Regnum
    Task<ObservableCollection<Tbl03Regnum>> CopyRegnum(Tbl03Regnum selected);
    #endregion

    #region Delete Regnum
    Task<bool> DeleteRegnum(Tbl03Regnum selected);
    Task DeleteRegnumDataset(Tbl03Regnum selected);
    #endregion

    #region Save Regnum
    Task<bool> SaveRegnum(Tbl03Regnum selected);
    Task<Tbl03Regnum> RegnumUpdate(Tbl03Regnum home, Tbl03Regnum selected);
    Task<Tbl03Regnum> RegnumAdd(Tbl03Regnum selected);
    Task RegnumSave(Tbl03Regnum home, Tbl03Regnum selected);
    #endregion

    #endregion Regnum

    #region Phylum

    #region Get Phylum
    ObservableCollection<Tbl06Phylum> GetTbl06PhylumsCollectionOrderByPhylumName();
    Task<ObservableCollection<Tbl06Phylum>> GetTbl06PhylumsCollectionOrderByPhylumNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl06Phylum> GetTbl06PhylumsCollectionOrderByPhylumNameFromRegnumId(int regnumId);
    ObservableCollection<Tbl06Phylum> GetTbl06PhylumsCollectionOrderByPhylumNameFromPhylumId(int phylumId);
    Task<ObservableCollection<Tbl06Phylum>> GetTbl06PhylumsCollectionFromRegnumId(int regnumId);


    Tbl06Phylum GetPhylumSingleByPhylumId(int id);
    Task<Tbl06Phylum> GetPhylumSingleFirstDataset();
    Task<ObservableCollection<Tbl06Phylum>> GetLastDatasetInTbl06Phylums();
    #endregion

    #region Copy Phylum
    Task<ObservableCollection<Tbl06Phylum>> CopyPhylum(Tbl06Phylum selected);
    #endregion

    #region Delete Phylum
    Task<bool> DeletePhylum(Tbl06Phylum selected);
    Task DeletePhylumDataset(Tbl06Phylum selected);
    Task DeleteDatasetsInTbl06Phylums(ObservableCollection<Tbl06Phylum> tbl06PhylumsList);
    Task DeleteConnectedPhylums(Tbl03Regnum selected);
    #endregion

    #region Save Phylum
    Task<bool> SavePhylum(Tbl06Phylum selected);
    Task<Tbl06Phylum> PhylumUpdate(Tbl06Phylum home, Tbl06Phylum selected);
    Task<Tbl06Phylum> PhylumAdd(Tbl06Phylum selected);
    Task PhylumSave(Tbl06Phylum home, Tbl06Phylum selected);
    #endregion

    #endregion Phylum

    #region Division

    #region Get Division
    ObservableCollection<Tbl09Division> GetTbl09DivisionsCollectionOrderByDivisionName();
    Task<ObservableCollection<Tbl09Division>> GetTbl09DivisionsCollectionOrderByDivisionNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl09Division> GetTbl09DivisionsCollectionOrderByDivisionNameFromRegnumId(int id);
    ObservableCollection<Tbl09Division> GetTbl09DivisionsCollectionOrderByDivisionNameFromDivisionId(int id);
    Tbl09Division GetDivisionSingleByDivisionId(int id);
    Task<Tbl09Division> GetDivisionSingleFirstDataset();
    Task<ObservableCollection<Tbl09Division>> GetLastDatasetInTbl09Divisions();
    #endregion

    #region Copy Division
    Task<ObservableCollection<Tbl09Division>> CopyDivision(Tbl09Division selected);
    #endregion

    #region Delete Division
    Task<bool> DeleteDivision(Tbl09Division selected);
    Task DeleteDivisionDataset(Tbl09Division selected);
    Task DeleteDatasetsInTbl09Divisions(ObservableCollection<Tbl09Division> tbl09DivisionsList);
    Task DeleteConnectedDivisions(Tbl03Regnum selected);
    #endregion

    #region Save Division
    Task<bool> SaveDivision(Tbl09Division selected);
    Task<Tbl09Division> DivisionUpdate(Tbl09Division home, Tbl09Division selected);
    Task<Tbl09Division> DivisionAdd(Tbl09Division selected);
    Task DivisionSave(Tbl09Division home, Tbl09Division selected);
    #endregion

    #endregion Division

    #region Subphylum

    #region Get Subphylum
    ObservableCollection<Tbl12Subphylum> GetTbl12SubphylumsCollectionOrderBySubphylumName();
    ObservableCollection<Tbl12Subphylum> GetTbl12SubphylumsCollectionOrderBySubphylumNameFromPhylumId(int phylumid);
    ObservableCollection<Tbl12Subphylum> GetTbl12SubphylumsCollectionOrderBySubphylumNameFromSubphylumId(int subphylum);
    Task<ObservableCollection<Tbl12Subphylum>> GetTbl12SubphylumsCollectionOrderBySubphylumNameFromSearchNameOrId(string? searchName);
    Tbl12Subphylum GetSubphylumSingleBySubphylumId(int id);
    Task<Tbl12Subphylum> GetSubphylumSingleFirstDataset();
    Task<ObservableCollection<Tbl12Subphylum>> GetLastDatasetInTbl12Subphylums();
    // Task<Tbl12Subphylum> GetSubphylumSingleBySubphylumName(string plantaeRegnum);//for "Plantae#Regnum#"
    // Tbl12Subphylum GetSubphylumSingleBySubphylumName(string name);   //for "Plantae#Regnum#"
    #endregion

    #region Copy Subphylum
    Task<ObservableCollection<Tbl12Subphylum>> CopySubphylum(Tbl12Subphylum selected);
    #endregion

    #region Delete Subphylum
    Task<bool> DeleteSubphylum(Tbl12Subphylum selected);
    Task DeleteSubphylumDataset(Tbl12Subphylum selected);
    Task DeleteConnectedSubphylums(Tbl06Phylum selected);
    #endregion

    #region Save Subphylum
    Task<bool> SaveSubphylum(Tbl12Subphylum selected);
    Task<Tbl12Subphylum> SubphylumUpdate(Tbl12Subphylum home, Tbl12Subphylum selected);
    Task<Tbl12Subphylum> SubphylumAdd(Tbl12Subphylum selected);
    Task SubphylumSave(Tbl12Subphylum home, Tbl12Subphylum selected);
    #endregion

    #endregion Subphylum

    #region Subdivision

    #region Get Subdivision
    ObservableCollection<Tbl15Subdivision> GetTbl15SubdivisionsCollectionOrderBySubdivisionName();
    Task<ObservableCollection<Tbl15Subdivision>> GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl15Subdivision> GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromDivisionId(int divisionid);
    ObservableCollection<Tbl15Subdivision> GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromSubdivisionId(int subdivisionid);

    Tbl15Subdivision GetSubdivisionSingleBySubdivisionId(int id);
    Task<ObservableCollection<Tbl15Subdivision>> GetLastDatasetInTbl15Subdivisions();
    Task<Tbl15Subdivision> GetSubdivisionSingleFirstDataset();
    //Task<Tbl15Subdivision> GetSubdivisionSingleBySubdivisionName(string name);   //for "Plantae#Regnum#"
    // Tbl15Subdivision GetSubdivisionSingleBySubdivisionName(string name);   //for "Plantae#Regnum#"

    #endregion

    #region Copy Subdivision
    Task<ObservableCollection<Tbl15Subdivision>> CopySubdivision(Tbl15Subdivision selected);
    #endregion

    #region Delete Subdivision
    Task<bool> DeleteSubdivision(Tbl15Subdivision selected);
    Task DeleteSubdivisionDataset(Tbl15Subdivision selected);
    Task DeleteConnectedSubdivisions(Tbl09Division selected);
    #endregion

    #region Save Subdivision
    Task<bool> SaveSubdivision(Tbl15Subdivision selected);
    Task<Tbl15Subdivision> SubdivisionUpdate(Tbl15Subdivision home, Tbl15Subdivision selected);
    Task<Tbl15Subdivision> SubdivisionAdd(Tbl15Subdivision selected);
    Task SubdivisionSave(Tbl15Subdivision home, Tbl15Subdivision selected);
    #endregion

    #endregion Subdivision

    #region Superclass

    #region Get Superclass
    ObservableCollection<Tbl18Superclass> GetTbl18SuperclassesCollectionOrderBySuperclassName();
    Task<ObservableCollection<Tbl18Superclass>> GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl18Superclass> GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSuperclassId(int superclassId);
    ObservableCollection<Tbl18Superclass> GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSubphylumId(int subphylumId);
    ObservableCollection<Tbl18Superclass> GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSubdivisionId(int subdivisionId);
    Task<ObservableCollection<Tbl18Superclass>> GetLastDatasetInTbl18Superclasses();
    Tbl18Superclass GetSuperclassSingleBySuperclassId(int superclassId);
    Task<Tbl18Superclass> GetSuperclassSingleFirstDataset();
    #endregion

    #region Copy Superclass
    Task<ObservableCollection<Tbl18Superclass>> CopySuperclass(Tbl18Superclass selected);
    #endregion

    #region Delete Superclass
    Task<bool> DeleteSuperclass(Tbl18Superclass selected);
    Task DeleteSuperclassDataset(Tbl18Superclass selected);
    Task DeleteDatasetsInTbl18Superclasses(ObservableCollection<Tbl18Superclass> tbl18SuperclassesList);
    Task DeleteConnectedSuperclassesWithSubphylum(Tbl12Subphylum selected);
    Task DeleteConnectedSuperclassesWithSubdivision(Tbl15Subdivision selected);
    #endregion

    #region Save Superclass
    Task<bool> SaveSuperclass(Tbl18Superclass selected);
    Task<Tbl18Superclass> SuperclassUpdate(Tbl18Superclass home, Tbl18Superclass selected);
    Task<Tbl18Superclass> SuperclassAdd(Tbl18Superclass selected);
    Task SuperclassSave(Tbl18Superclass home, Tbl18Superclass selected);
    #endregion

    #endregion Superclass

    #region Class

    #region Get Class

    ObservableCollection<Tbl21Class> GetTbl21ClassesCollectionOrderByClassNameFromSuperclassId(int superclassId);
    Task<ObservableCollection<Tbl21Class>> GetLastDatasetInTbl21Classes();
    ObservableCollection<Tbl21Class> GetTbl21ClassesCollectionOrderByClassName();
    Task<ObservableCollection<Tbl21Class>> GetTbl21ClassesCollectionOrderByClassNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl21Class> GetTbl21ClassesCollectionOrderByClassNameFromClassId(int classId);
    Tbl21Class GetClassSingleByClassId(int classId);
    Task<Tbl21Class> GetClassSingleFirstDataset();

    #endregion

    #region Copy Class
    Task<ObservableCollection<Tbl21Class>> CopyClass(Tbl21Class selected);
    #endregion

    #region Delete Class
    Task DeleteConnectedClasses(Tbl18Superclass selected);
    Task<bool> DeleteClass(Tbl21Class selected);
    Task DeleteClassDataset(Tbl21Class selected);
    Task DeleteDatasetsInTbl21Classes(ObservableCollection<Tbl21Class> tbl21ClassesList);
    #endregion

    #region Save Class
    Task<bool> SaveClass(Tbl21Class selected);
    Task<Tbl21Class> ClassUpdate(Tbl21Class home, Tbl21Class selected);
    Task<Tbl21Class> ClassAdd(Tbl21Class selected);
    Task ClassSave(Tbl21Class home, Tbl21Class selected);
    #endregion

    #endregion Class

    #region Subclass

    #region Get Subclass
    ObservableCollection<Tbl24Subclass> GetTbl24SubclassesCollectionOrderBySubclassNameFromClassId(int classId);
    Task<ObservableCollection<Tbl24Subclass>> GetLastDatasetInTbl24Subclasses();
    ObservableCollection<Tbl24Subclass> GetTbl24SubclassesCollectionOrderBySubclassName();
    Task<ObservableCollection<Tbl24Subclass>> GetTbl24SubclassesCollectionOrderBySubclassNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl24Subclass> GetTbl24SubclassesCollectionOrderBySubclassNameFromSubclassId(int subclassId);
    Tbl24Subclass GetSubclassSingleBySubclassId(int subclassId);
    Task<Tbl24Subclass> GetSubclassSingleFirstDataset();

    #endregion

    #region Copy Subclass
    Task<ObservableCollection<Tbl24Subclass>> CopySubclass(Tbl24Subclass selected);
    #endregion

    #region Delete Subclass
    Task DeleteConnectedSubclasses(Tbl21Class selected);
    Task<bool> DeleteSubclass(Tbl24Subclass selected);
    Task DeleteSubclassDataset(Tbl24Subclass selected);
    Task DeleteDatasetsInTbl24Subclasses(ObservableCollection<Tbl24Subclass> tbl24SubclassesList);

    #endregion

    #region Save Subclass
    Task<bool> SaveSubclass(Tbl24Subclass selected);
    Task<Tbl24Subclass> SubclassUpdate(Tbl24Subclass home, Tbl24Subclass selected);
    Task<Tbl24Subclass> SubclassAdd(Tbl24Subclass selected);
    Task SubclassSave(Tbl24Subclass home, Tbl24Subclass selected);

    #endregion

    #endregion Subclass

    #region Infraclass

    #region Get Infraclass
    ObservableCollection<Tbl27Infraclass> GetTbl27InfraclassesCollectionOrderByInfraclassNameFromSubclassId(int subclassId);
    Task<ObservableCollection<Tbl27Infraclass>> GetLastDatasetInTbl27Infraclasses();
    Tbl27Infraclass GetInfraclassSingleByInfraclassId(int infraclassId);
    Task<Tbl27Infraclass> GetInfraclassSingleFirstDataset();
    ObservableCollection<Tbl27Infraclass> GetTbl27InfraclassesCollectionOrderByInfraclassName();
    Task<ObservableCollection<Tbl27Infraclass>> GetTbl27InfraclassesCollectionOrderByInfraclassNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl27Infraclass> GetTbl27InfraclassesCollectionOrderByInfraclassNameFromInfraclassId(int infraclassId);

    #endregion

    #region Copy Infraclass
    Task<ObservableCollection<Tbl27Infraclass>> CopyInfraclass(Tbl27Infraclass selected);

    #endregion

    #region Delete Infraclass
    Task DeleteConnectedInfraclasses(Tbl24Subclass selected);
    Task<bool> DeleteInfraclass(Tbl27Infraclass selected);
    Task DeleteInfraclassDataset(Tbl27Infraclass selected);
    Task DeleteDatasetsInTbl27Infraclasses(ObservableCollection<Tbl27Infraclass> tbl27InfraclassesList);

    #endregion

    #region Save Infraclass
    Task<bool> SaveInfraclass(Tbl27Infraclass selected);
    Task<Tbl27Infraclass> InfraclassUpdate(Tbl27Infraclass home, Tbl27Infraclass selected);
    Task<Tbl27Infraclass> InfraclassAdd(Tbl27Infraclass selected);
    Task InfraclassSave(Tbl27Infraclass home, Tbl27Infraclass selected);
    #endregion

    #endregion Infraclass

    #region Legio

    #region Get Legio

    ObservableCollection<Tbl30Legio> GetTbl30LegiosCollectionOrderByLegioNameFromInfraclassId(int infraclassId);
    Task<ObservableCollection<Tbl30Legio>> GetLastDatasetInTbl30Legios();
    Tbl30Legio GetLegioSingleByLegioId(int legioId);
    Task<Tbl30Legio> GetLegioSingleFirstDataset();
    ObservableCollection<Tbl30Legio> GetTbl30LegiosCollectionOrderByLegioName();
    Task<ObservableCollection<Tbl30Legio>> GetTbl30LegiosCollectionOrderByLegioNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl30Legio> GetTbl30LegiosCollectionOrderByLegioNameFromLegioId(int legioId);

    #endregion

    #region Copy Legio
    Task<ObservableCollection<Tbl30Legio>> CopyLegio(Tbl30Legio selected);

    #endregion

    #region Delete Legio

    Task DeleteConnectedLegios(Tbl27Infraclass selected);
    Task<bool> DeleteLegio(Tbl30Legio selected);
    Task DeleteLegioDataset(Tbl30Legio selected);
    Task DeleteDatasetsInTbl30Legios(ObservableCollection<Tbl30Legio> tbl30LegiosList);

    #endregion

    #region Save Legio   

    Task<bool> SaveLegio(Tbl30Legio selected);
    Task<Tbl30Legio> LegioUpdate(Tbl30Legio home, Tbl30Legio selected);
    Task<Tbl30Legio> LegioAdd(Tbl30Legio selected);
    Task LegioSave(Tbl30Legio home, Tbl30Legio selected);

    #endregion

    #endregion Legio

    #region Ordo

    #region Get Ordo

    ObservableCollection<Tbl33Ordo> GetTbl33OrdosCollectionOrderByOrdoNameFromLegioId(int legioId);
    Task<ObservableCollection<Tbl33Ordo>> GetLastDatasetInTbl33Ordos();
    Task<Tbl33Ordo> GetOrdoSingleByOrdoId(int ordoId);
    Task<Tbl33Ordo> GetOrdoSingleFirstDataset();
    ObservableCollection<Tbl33Ordo> GetTbl33OrdosCollectionOrderByOrdoName();
    Task<ObservableCollection<Tbl33Ordo>> GetTbl33OrdosCollectionOrderByOrdoNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl33Ordo> GetTbl33OrdosCollectionOrderByOrdoNameFromOrdoId(int ordoId);

    #endregion

    #region Copy Ordo
    Task<ObservableCollection<Tbl33Ordo>> CopyOrdo(Tbl33Ordo selected);

    #endregion

    #region Delete Ordo

    Task DeleteConnectedOrdos(Tbl30Legio selected);
    Task<bool> DeleteOrdo(Tbl33Ordo selected);
    Task DeleteOrdoDataset(Tbl33Ordo selected);

    #endregion

    #region Save Ordo   

    Task<bool> SaveOrdo(Tbl33Ordo selected);
    Task<Tbl33Ordo> OrdoUpdate(Tbl33Ordo home, Tbl33Ordo selected);
    Task<Tbl33Ordo> OrdoAdd(Tbl33Ordo selected);
    Task OrdoSave(Tbl33Ordo home, Tbl33Ordo selected);

    #endregion

    #endregion Ordo

    #region Subordo

    #region Get Subordo

    ObservableCollection<Tbl36Subordo> GetTbl36SubordosCollectionOrderBySubordoNameFromOrdoId(int ordoId);
    Task<ObservableCollection<Tbl36Subordo>> GetLastDatasetInTbl36Subordos();
    Task<Tbl36Subordo> GetSubordoSingleBySubordoId(int subordoId);
    Task<Tbl36Subordo> GetSubordoSingleFirstDataset();
    ObservableCollection<Tbl36Subordo> GetTbl36SubordosCollectionOrderBySubordoName();
    Task<ObservableCollection<Tbl36Subordo>> GetTbl36SubordosCollectionOrderBySubordoNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl36Subordo> GetTbl36SubordosCollectionOrderBySubordoNameFromSubordoId(int subordoId);

    #endregion

    #region Copy Subordo
    Task<ObservableCollection<Tbl36Subordo>> CopySubordo(Tbl36Subordo selected);

    #endregion

    #region Delete Subordo

    Task DeleteConnectedSubordos(Tbl33Ordo selected);
    Task<bool> DeleteSubordo(Tbl36Subordo selected);
    Task DeleteSubordoDataset(Tbl36Subordo selected);
    Task DeleteDatasetsInTbl33Ordos(ObservableCollection<Tbl33Ordo> tbl33OrdosList);
    Task DeleteDatasetsInTbl36Subordos(ObservableCollection<Tbl36Subordo> tbl36SubordosList);
    #endregion

    #region Save Subordo   

    Task<bool> SaveSubordo(Tbl36Subordo selected);
    Task<Tbl36Subordo> SubordoUpdate(Tbl36Subordo home, Tbl36Subordo selected);
    Task<Tbl36Subordo> SubordoAdd(Tbl36Subordo selected);
    Task SubordoSave(Tbl36Subordo home, Tbl36Subordo selected);

    #endregion

    #endregion Subordo

    #region Infraordo

    #region Get Infraordo

    ObservableCollection<Tbl39Infraordo> GetTbl39InfraordosCollectionOrderByInfraordoNameFromSubordoId(int subordoId);
    Task<ObservableCollection<Tbl39Infraordo>> GetLastDatasetInTbl39Infraordos();
    Task<Tbl39Infraordo> GetInfraordoSingleByInfraordoId(int infraordoId);
    Task<Tbl39Infraordo> GetInfraordoSingleFirstDataset();
    ObservableCollection<Tbl39Infraordo> GetTbl39InfraordosCollectionOrderByInfraordoName();
    Task<ObservableCollection<Tbl39Infraordo>> GetTbl39InfraordosCollectionOrderByInfraordoNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl39Infraordo> GetTbl39InfraordosCollectionOrderByInfraordoNameFromInfraordoId(int infraordoId);
    #endregion

    #region Copy Infraordo
    Task<ObservableCollection<Tbl39Infraordo>> CopyInfraordo(Tbl39Infraordo selected);

    #endregion

    #region Delete Infraordo

    Task DeleteConnectedInfraordos(Tbl36Subordo selected);
    Task<bool> DeleteInfraordo(Tbl39Infraordo selected);
    Task DeleteInfraordoDataset(Tbl39Infraordo selected);
    Task DeleteDatasetsInTbl39Infraordos(ObservableCollection<Tbl39Infraordo> tbl39InfraordosList);

    #endregion

    #region Save Infraordo   

    Task<bool> SaveInfraordo(Tbl39Infraordo selected);
    Task<Tbl39Infraordo> InfraordoUpdate(Tbl39Infraordo home, Tbl39Infraordo selected);
    Task<Tbl39Infraordo> InfraordoAdd(Tbl39Infraordo selected);
    Task InfraordoSave(Tbl39Infraordo home, Tbl39Infraordo selected);

    #endregion

    #endregion Infraordo

    #region Superfamily

    #region Get Superfamily

    ObservableCollection<Tbl42Superfamily> GetTbl42SuperfamiliesCollectionOrderBySuperfamilyNameFromInfraordoId(int infraordoId);
    Task<ObservableCollection<Tbl42Superfamily>> GetLastDatasetInTbl42Superfamilies();
    Task<Tbl42Superfamily> GetSuperfamilySingleBySuperfamilyId(int superfamilyId);
    Task<Tbl42Superfamily> GetSuperfamilySingleFirstDataset();
    ObservableCollection<Tbl42Superfamily> GetTbl42SuperfamiliesCollectionOrderBySuperfamilyName();
    Task<ObservableCollection<Tbl42Superfamily>> GetTbl42SuperfamiliesCollectionOrderBySuperfamilyNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl42Superfamily> GetTbl42SuperfamiliesCollectionOrderBySuperfamilyNameFromSuperfamilyId(int superfamilyId);
    #endregion

    #region Copy Superfamily
    Task<ObservableCollection<Tbl42Superfamily>> CopySuperfamily(Tbl42Superfamily selected);

    #endregion

    #region Delete Superfamily

    Task DeleteConnectedSuperfamilies(Tbl39Infraordo selected);
    Task<bool> DeleteSuperfamily(Tbl42Superfamily selected);
    Task DeleteSuperfamilyDataset(Tbl42Superfamily selected);
    Task DeleteDatasetsInTbl42Superfamilies(ObservableCollection<Tbl42Superfamily> tbl42SuperfamiliesList);

    #endregion

    #region Save Superfamily   

    Task<bool> SaveSuperfamily(Tbl42Superfamily selected);
    Task<Tbl42Superfamily> SuperfamilyUpdate(Tbl42Superfamily home, Tbl42Superfamily selected);
    Task<Tbl42Superfamily> SuperfamilyAdd(Tbl42Superfamily selected);
    Task SuperfamilySave(Tbl42Superfamily home, Tbl42Superfamily selected);

    #endregion

    #endregion Superfamily

    #region Family

    #region Get Family

    ObservableCollection<Tbl45Family> GetTbl45FamiliesCollectionOrderByFamilyNameFromSuperfamilyId(int superfamilyId);
    Task<ObservableCollection<Tbl45Family>> GetLastDatasetInTbl45Families();
    Task<Tbl45Family> GetFamilySingleByFamilyId(int familyId);
    Task<Tbl45Family> GetFamilySingleFirstDataset();
    ObservableCollection<Tbl45Family> GetTbl45FamiliesCollectionOrderByFamilyName();
    Task<ObservableCollection<Tbl45Family>> GetTbl45FamiliesCollectionOrderByFamilyNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl45Family> GetTbl45FamiliesCollectionOrderByFamilyNameFromFamilyId(int familyId);

    #endregion

    #region Copy Family
    Task<ObservableCollection<Tbl45Family>> CopyFamily(Tbl45Family selected);

    #endregion

    #region Delete Family

    Task DeleteConnectedFamilies(Tbl42Superfamily selected);
    Task<bool> DeleteFamily(Tbl45Family selected);
    Task DeleteFamilyDataset(Tbl45Family selected);
    Task DeleteDatasetsInTbl45Families(ObservableCollection<Tbl45Family> tbl45FamiliesList);

    #endregion

    #region Save Family   

    Task<bool> SaveFamily(Tbl45Family selected);
    Task<Tbl45Family> FamilyUpdate(Tbl45Family home, Tbl45Family selected);
    Task<Tbl45Family> FamilyAdd(Tbl45Family selected);
    Task FamilySave(Tbl45Family home, Tbl45Family selected);

    #endregion

    #endregion Family

    #region Subfamily

    #region Get Subfamily

    ObservableCollection<Tbl48Subfamily> GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromFamilyId(int familyId);
    Task<ObservableCollection<Tbl48Subfamily>> GetLastDatasetInTbl48Subfamilies();
    Task<Tbl48Subfamily> GetSubfamilySingleBySubfamilyId(int subfamilyId);
    Task<Tbl48Subfamily> GetSubfamilySingleFirstDataset();
    ObservableCollection<Tbl48Subfamily> GetTbl48SubfamiliesCollectionOrderBySubfamilyName();
    Task<ObservableCollection<Tbl48Subfamily>> GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl48Subfamily> GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromSubfamilyId(int subfamilyId);

    #endregion

    #region Copy Subfamily
    Task<ObservableCollection<Tbl48Subfamily>> CopySubfamily(Tbl48Subfamily selected);

    #endregion

    #region Delete Subfamily

    Task DeleteConnectedSubfamilies(Tbl45Family selected);
    Task<bool> DeleteSubfamily(Tbl48Subfamily selected);
    Task DeleteSubfamilyDataset(Tbl48Subfamily selected);
    Task DeleteDatasetsInTbl48Subfamilies(ObservableCollection<Tbl48Subfamily> tbl48SubfamiliesList);

    #endregion

    #region Save Subfamily   

    Task<bool> SaveSubfamily(Tbl48Subfamily selected);
    Task<Tbl48Subfamily> SubfamilyUpdate(Tbl48Subfamily home, Tbl48Subfamily selected);
    Task<Tbl48Subfamily> SubfamilyAdd(Tbl48Subfamily selected);
    Task SubfamilySave(Tbl48Subfamily home, Tbl48Subfamily selected);

    #endregion

    #endregion Subfamily

    #region Infrafamily

    #region Get Infrafamily

    ObservableCollection<Tbl51Infrafamily> GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromSubfamilyId(int subfamilyId);
    Task<ObservableCollection<Tbl51Infrafamily>> GetLastDatasetInTbl51Infrafamilies();
    Task<Tbl51Infrafamily> GetInfrafamilySingleByInfrafamilyId(int infrafamilyId);
    Task<Tbl51Infrafamily> GetInfrafamilySingleFirstDataset();
    ObservableCollection<Tbl51Infrafamily> GetTbl51InfrafamiliesCollectionOrderByInfrafamilyName();
    Task<ObservableCollection<Tbl51Infrafamily>> GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl51Infrafamily> GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromInfrafamilyId(int infrafamilyId);

    #endregion

    #region Copy Infrafamily
    Task<ObservableCollection<Tbl51Infrafamily>> CopyInfrafamily(Tbl51Infrafamily selected);

    #endregion

    #region Delete Infrafamily

    Task DeleteConnectedInfrafamilies(Tbl48Subfamily selected);
    Task<bool> DeleteInfrafamily(Tbl51Infrafamily selected);
    Task DeleteInfrafamilyDataset(Tbl51Infrafamily selected);
    Task DeleteDatasetsInTbl51Infrafamilies(ObservableCollection<Tbl51Infrafamily> tbl51InfrafamiliesList);

    #endregion

    #region Save Infrafamily   

    Task<bool> SaveInfrafamily(Tbl51Infrafamily selected);
    Task<Tbl51Infrafamily> InfrafamilyUpdate(Tbl51Infrafamily home, Tbl51Infrafamily selected);
    Task<Tbl51Infrafamily> InfrafamilyAdd(Tbl51Infrafamily selected);
    Task InfrafamilySave(Tbl51Infrafamily home, Tbl51Infrafamily selected);

    #endregion

    #endregion Infrafamily

    #region Supertribus

    #region Get Supertribus
    ObservableCollection<Tbl54Supertribus> GetTbl54SupertribussesCollectionOrderBySupertribusNameFromInfrafamilyId(int infrafamilyId);
    Task<ObservableCollection<Tbl54Supertribus>> GetLastDatasetInTbl54Supertribusses();
    Task<Tbl54Supertribus> GetSupertribusSingleBySupertribusId(int supertribusId);
    Task<Tbl54Supertribus> GetSupertribusSingleFirstDataset();
    ObservableCollection<Tbl54Supertribus> GetTbl54SupertribussesCollectionOrderBySupertribusName();
    Task<ObservableCollection<Tbl54Supertribus>> GetTbl54SupertribussesCollectionOrderBySupertribusNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl54Supertribus> GetTbl54SupertribussesCollectionOrderBySupertribusNameFromSupertribusId(int supertribusId);

    #endregion

    #region Copy Supertribus
    Task<ObservableCollection<Tbl54Supertribus>> CopySupertribus(Tbl54Supertribus selected);

    #endregion

    #region Delete Supertribus

    Task DeleteConnectedSupertribusses(Tbl51Infrafamily selected);
    Task<bool> DeleteSupertribus(Tbl54Supertribus selected);
    Task DeleteSupertribusDataset(Tbl54Supertribus selected);
    Task DeleteDatasetsInTbl54Supertribusses(ObservableCollection<Tbl54Supertribus> tbl54SupertribussesList);

    #endregion

    #region Save Supertribus   

    Task<bool> SaveSupertribus(Tbl54Supertribus selected);
    Task<Tbl54Supertribus> SupertribusUpdate(Tbl54Supertribus home, Tbl54Supertribus selected);
    Task<Tbl54Supertribus> SupertribusAdd(Tbl54Supertribus selected);
    Task SupertribusSave(Tbl54Supertribus home, Tbl54Supertribus selected);

    #endregion

    #endregion Supertribus

    #region Tribus

    #region Get Tribus

    ObservableCollection<Tbl57Tribus> GetTbl57TribussesCollectionOrderByTribusNameFromSupertribusId(int supertribusId);
    Task<ObservableCollection<Tbl57Tribus>> GetLastDatasetInTbl57Tribusses();
    Task<Tbl57Tribus> GetTribusSingleByTribusId(int tribusId);
    Task<Tbl57Tribus> GetTribusSingleFirstDataset();
    ObservableCollection<Tbl57Tribus> GetTbl57TribussesCollectionOrderByTribusName();
    Task<ObservableCollection<Tbl57Tribus>> GetTbl57TribussesCollectionOrderByTribusNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl57Tribus> GetTbl57TribussesCollectionOrderByTribusNameFromTribusId(int tribusId);

    #endregion

    #region Copy Tribus
    Task<ObservableCollection<Tbl57Tribus>> CopyTribus(Tbl57Tribus selected);

    #endregion

    #region Delete Tribus

    Task DeleteConnectedTribusses(Tbl54Supertribus selected);
    Task<bool> DeleteTribus(Tbl57Tribus selected);
    Task DeleteTribusDataset(Tbl57Tribus selected);
    Task DeleteDatasetsInTbl57Tribusses(ObservableCollection<Tbl57Tribus> tbl57TribussesList);

    #endregion

    #region Save Tribus   

    Task<bool> SaveTribus(Tbl57Tribus selected);
    Task<Tbl57Tribus> TribusUpdate(Tbl57Tribus home, Tbl57Tribus selected);
    Task<Tbl57Tribus> TribusAdd(Tbl57Tribus selected);
    Task TribusSave(Tbl57Tribus home, Tbl57Tribus selected);

    #endregion

    #endregion Tribus

    #region Subtribus

    #region Get Subtribus

    ObservableCollection<Tbl60Subtribus> GetTbl60SubtribussesCollectionOrderBySubtribusNameFromTribusId(int tribusId);
    Task<ObservableCollection<Tbl60Subtribus>> GetLastDatasetInTbl60Subtribusses();
    Task<Tbl60Subtribus> GetSubtribusSingleBySubtribusId(int subtribusId);
    Task<Tbl60Subtribus> GetSubtribusSingleFirstDataset();
    ObservableCollection<Tbl60Subtribus> GetTbl60SubtribussesCollectionOrderBySubtribusName();
    Task<ObservableCollection<Tbl60Subtribus>> GetTbl60SubtribussesCollectionOrderBySubtribusNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl60Subtribus> GetTbl60SubtribussesCollectionOrderBySubtribusNameFromSubtribusId(int subtribusId);

    #endregion

    #region Copy Subtribus
    Task<ObservableCollection<Tbl60Subtribus>> CopySubtribus(Tbl60Subtribus selected);

    #endregion

    #region Delete Subtribus

    Task DeleteConnectedSubtribusses(Tbl57Tribus selected);
    Task<bool> DeleteSubtribus(Tbl60Subtribus selected);
    Task DeleteSubtribusDataset(Tbl60Subtribus selected);
    Task DeleteDatasetsInTbl60Subtribusses(ObservableCollection<Tbl60Subtribus> tbl60SubtribussesList);

    #endregion

    #region Save Subtribus   

    Task<bool> SaveSubtribus(Tbl60Subtribus selected);
    Task<Tbl60Subtribus> SubtribusUpdate(Tbl60Subtribus home, Tbl60Subtribus selected);
    Task<Tbl60Subtribus> SubtribusAdd(Tbl60Subtribus selected);
    Task SubtribusSave(Tbl60Subtribus home, Tbl60Subtribus selected);

    #endregion

    #endregion Subtribus

    #region Infratribus

    #region Get Infratribus

    ObservableCollection<Tbl63Infratribus> GetTbl63InfratribussesCollectionOrderByInfratribusNameFromSubtribusId(int subtribusId);
    Task<ObservableCollection<Tbl63Infratribus>> GetLastDatasetInTbl63Infratribusses();
    Tbl63Infratribus GetInfratribusSingleByInfratribusId(int infratribusId);

    // Tbl63Infratribus GetInfratribusPdfSingleByInfratribusId(int infratribusId);

    Task<Tbl63Infratribus> GetInfratribusSingleFirstDataset();
    ObservableCollection<Tbl63Infratribus> GetTbl63InfratribussesCollectionOrderByInfratribusName();
    Task<ObservableCollection<Tbl63Infratribus>> GetTbl63InfratribussesCollectionOrderByInfratribusNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl63Infratribus> GetTbl63InfratribussesCollectionOrderByInfratribusNameFromInfratribusId(int infratribusId);

    #endregion

    #region Copy Infratribus
    Task<ObservableCollection<Tbl63Infratribus>> CopyInfratribus(Tbl63Infratribus selected);

    #endregion

    #region Delete Infratribus

    Task DeleteConnectedInfratribusses(Tbl60Subtribus selected);
    Task<bool> DeleteInfratribus(Tbl63Infratribus selected);
    Task DeleteInfratribusDataset(Tbl63Infratribus selected);
    Task DeleteDatasetsInTbl63Infratribusses(ObservableCollection<Tbl63Infratribus> tbl63InfratribussesList);

    #endregion

    #region Save Infratribus   

    Task<bool> SaveInfratribus(Tbl63Infratribus selected);
    Task<Tbl63Infratribus> InfratribusUpdate(Tbl63Infratribus home, Tbl63Infratribus selected);
    Task<Tbl63Infratribus> InfratribusAdd(Tbl63Infratribus selected);
    Task InfratribusSave(Tbl63Infratribus home, Tbl63Infratribus selected);

    #endregion

    #endregion Infratribus

    #region Genus

    #region Get Genus

    ObservableCollection<Tbl66Genus> GetTbl66GenussesCollectionOrderByGenusNameFromInfratribusId(int infratribusId);
    Task<ObservableCollection<Tbl66Genus>> GetLastDatasetInTbl66Genusses();
    Tbl66Genus GetGenusSingleByGenusId(int genusId);
    //  Tbl66Genus GetGenusPdfSingleByGenusId(int genusId);
    Task<Tbl66Genus> GetGenusSingleByGenusName(string genusName);
    Task<Tbl66Genus> GetGenusSingleFirstDataset();
    ObservableCollection<Tbl66Genus> GetTbl66GenussesCollectionOrderByGenusName();
    Task<ObservableCollection<Tbl66Genus>> GetTbl66GenussesCollectionOrderByGenusNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl66Genus> GetTbl66GenussesCollectionOrderByGenusNameFromGenusId(int genusId);

    #endregion

    #region Copy Genus
    Task<ObservableCollection<Tbl66Genus>> CopyGenus(Tbl66Genus selected);

    #endregion

    #region Delete Genus

    Task DeleteConnectedGenusses(Tbl63Infratribus selected);
    Task<bool> DeleteGenus(Tbl66Genus selected);
    Task DeleteGenusDataset(Tbl66Genus selected);
    Task DeleteDatasetsInTbl66Genusses(ObservableCollection<Tbl66Genus> tbl66GenussesList);

    #endregion

    #region Save Genus   

    Task<bool> SaveGenus(Tbl66Genus selected);
    Task<Tbl66Genus> GenusUpdate(Tbl66Genus home, Tbl66Genus selected);
    Task<Tbl66Genus> GenusAdd(Tbl66Genus selected);
    Task GenusSave(Tbl66Genus home, Tbl66Genus selected);

    #endregion

    #endregion Genus

    #region Speciesgroup

    #region Get Speciesgroup
    ObservableCollection<Tbl68Speciesgroup> GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup();
    Task<Tbl68Speciesgroup> GetSpeciesgroupSingleBySpeciesgroupId(int speciesgroupId);
    ObservableCollection<Tbl68Speciesgroup> GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroupFromSpeciesgroupId(int? speciesgroupId);

    Task<ObservableCollection<Tbl68Speciesgroup>> GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroupFromSearchNameOrId(string searchName);
    Task<ObservableCollection<Tbl68Speciesgroup>> GetLastDatasetInTbl68Speciesgroups();

    #endregion

    #region Copy Speciesgroup

    Task<ObservableCollection<Tbl68Speciesgroup>> CopySpeciesgroup(Tbl68Speciesgroup selected);


    #endregion

    #region Delete Speciesgroup

    Task<bool> DeleteSpeciesgroup(Tbl68Speciesgroup selected);
    Task DeleteSpeciesgroupDataset(Tbl68Speciesgroup selected);

    #endregion

    #region Save Speciesgroup
    Task<bool> SaveSpeciesgroup(Tbl68Speciesgroup selected);
    Task<Tbl68Speciesgroup> SpeciesgroupUpdate(Tbl68Speciesgroup home, Tbl68Speciesgroup selected);
    Task<Tbl68Speciesgroup> SpeciesgroupAdd(Tbl68Speciesgroup selected);
    Task SpeciesgroupSave(Tbl68Speciesgroup home, Tbl68Speciesgroup selected);

    #endregion

    #endregion

    #region FiSpecies

    #region Get FiSpecies

    ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromGenusId(int genusId);
    ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromSpeciesgroupId(int speciesgroupId);
    ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(int fispeciesId);
    Task<ObservableCollection<Tbl69FiSpecies>> GetLastDatasetInTbl69FiSpeciesses();
    Tbl69FiSpecies GetFiSpeciesSingleModelByFiSpeciesId(int fispeciesId);
    Tbl69FiSpecies GetFiSpeciesSingleByFiSpeciesId(int fispeciesId);
    Task<Tbl69FiSpecies> GetFiSpeciesSingleByFiSpeciesNameAndSubspeciesAndDivers(string fiSpeciesName, string subspecies, string divers);
    Task<Tbl69FiSpecies> GetFiSpeciesSingleFirstDataset();
    ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByFiSpeciesName();
    ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers();
    Task<ObservableCollection<Tbl69FiSpecies>> GetTbl69FiSpeciessesCollectionOrderByFiSpeciesNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByFiSpeciesNameFromFiSpeciesId(int fispeciesId);
    Tbl69FiSpecies GetFiSpeciesSingleByFiSpeciesName(string name);   //for "Animalia#Regnum#"
    #endregion

    #region Copy FiSpecies
    Task<ObservableCollection<Tbl69FiSpecies>> CopyFiSpecies(Tbl69FiSpecies selected);

    #endregion

    #region Delete FiSpecies

    Task DeleteConnectedFiSpeciesses(Tbl66Genus selected);
    Task<bool> DeleteFiSpecies(Tbl69FiSpecies selected);
    Task DeleteFiSpeciesDataset(Tbl69FiSpecies selected);
    Task DeleteDatasetsInTbl69FiSpeciesses(ObservableCollection<Tbl69FiSpecies> tbl69FiSpeciessesList);

    #endregion

    #region Save FiSpecies   

    Task<bool> SaveFiSpecies(Tbl69FiSpecies selected);
    Task<Tbl69FiSpecies> FiSpeciesUpdate(Tbl69FiSpecies home, Tbl69FiSpecies selected);
    Task<Tbl69FiSpecies> FiSpeciesAdd(Tbl69FiSpecies selected);
    Task FiSpeciesSave(Tbl69FiSpecies home, Tbl69FiSpecies selected);

    #endregion

    #endregion FiSpecies

    #region PlSpecies

    #region Get PlSpecies

    ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromGenusId(int genusId);
    ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromSpeciesgroupId(int speciesgroupId);
    ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(int plspeciesId);
    ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByPlSpeciesName();
    ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers();
    Task<ObservableCollection<Tbl72PlSpecies>> GetTbl72PlSpeciessesCollectionOrderByPlSpeciesNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByPlSpeciesNameFromPlSpeciesId(int plspeciesId);
    Task<ObservableCollection<Tbl72PlSpecies>> GetLastDatasetInTbl72PlSpeciesses();
    Tbl72PlSpecies GetPlSpeciesSingleModelByPlSpeciesId(int plspeciesId);
    Task<Tbl72PlSpecies> GetPlSpeciesSingleByPlSpeciesId(int plspeciesId);
    Task<Tbl72PlSpecies> GetPlSpeciesSingleByPlSpeciesNameAndSubspeciesAndDivers(string plSpeciesName, string subspecies, string divers);
    Task<Tbl72PlSpecies> GetPlSpeciesSingleFirstDataset();

    Tbl72PlSpecies GetPlSpeciesSingleByPlSpeciesName(string name);   //for "Plantae#Regnum#"


    #endregion

    #region Copy PlSpecies
    Task<ObservableCollection<Tbl72PlSpecies>> CopyPlSpecies(Tbl72PlSpecies selected);

    #endregion

    #region Delete PlSpecies

    Task DeleteDatasetsInTbl72PlSpeciesses(ObservableCollection<Tbl72PlSpecies> tbl72PlSpeciessesList);
    Task DeleteConnectedPlSpeciesses(Tbl66Genus selected);
    Task<bool> DeletePlSpecies(Tbl72PlSpecies selected);
    Task DeletePlSpeciesDataset(Tbl72PlSpecies selected);

    #endregion

    #region Save PlSpecies   

    Task<bool> SavePlSpecies(Tbl72PlSpecies selected);
    Task<Tbl72PlSpecies> PlSpeciesUpdate(Tbl72PlSpecies home, Tbl72PlSpecies selected);
    Task<Tbl72PlSpecies> PlSpeciesAdd(Tbl72PlSpecies selected);
    Task PlSpeciesSave(Tbl72PlSpecies home, Tbl72PlSpecies selected);

    #endregion

    #endregion PlSpecies



    #region Name

    #region Get Name
    Task<ObservableCollection<Tbl78Name>> GetTbl78NamesCollectionOrderByNameNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl78Name> GetTbl78NamesCollectionOrderByNameNameFromFiSpeciesId(int fispeciesId);
    ObservableCollection<Tbl78Name> GetTbl78NamesCollectionOrderByNameNameFromPlSpeciesId(int plspeciesId);
    ObservableCollection<Tbl78Name> GetTbl78NamesCollectionOrderByNameNameFromNameId(int nameId);
    ObservableCollection<Tbl78Name> GetTbl78NamesCollectionOrderByNameName();

    Task<ObservableCollection<Tbl78Name>> GetLastDatasetInTbl78Names();
    Task<Tbl78Name> GetNameSingleByNameId(int nameId);
    Task<Tbl78Name> GetNameSingleByNameNameAndLanguage(string name, string language);
    Task<Tbl78Name> GetNameSingleFirstDataset();

    #endregion

    #region Copy Name
    Task<ObservableCollection<Tbl78Name>> CopyName(Tbl78Name selected);

    #endregion

    #region Delete Name

    Task DeleteConnectedNames(Tbl69FiSpecies selected);
    Task DeleteConnectedNames(Tbl72PlSpecies selected);
    Task<bool> DeleteName(Tbl78Name selected);
    Task DeleteNameDataset(Tbl78Name selected);

    #endregion

    #region Save Name   

    Task<bool> SaveName(Tbl78Name selected);
    Task<Tbl78Name> NameUpdate(Tbl78Name home, Tbl78Name selected);
    Task<Tbl78Name> NameAdd(Tbl78Name selected);
    Task NameSave(Tbl78Name home, Tbl78Name selected);

    #endregion

    #endregion Name

    #region Image

    #region Get Image
    Task<ObservableCollection<Tbl81Image>> GetTbl81ImagesCollectionOrderByInfoFromSearchImageId(string searchInfo);
    Task<ObservableCollection<Tbl81Image>> GetTbl81ImagesCollectionOrderByInfoFromSearchInfoOrImageId(string searchInfoOrId);


    ObservableCollection<Tbl81Image> GetTbl81ImagesCollectionOrderByInfoFromFiSpeciesId(int fispeciesId);
    ObservableCollection<Tbl81Image> GetTbl81ImagesCollectionOrderByInfoFromPlSpeciesId(int plspeciesId);
    ObservableCollection<Tbl81Image> GetTbl81ImagesCollectionOrderByInfoFromImageId(int imageId);
    Task<Tbl81Image> GetImageSingleByImageId(int imageId);
    Task<ObservableCollection<Tbl81Image>> GetLastDatasetInTbl81Images();

    #endregion

    #region Copy Image

    Task<ObservableCollection<Tbl81Image>> CopyImage(Tbl81Image selected);

    #endregion

    #region Delete Image
    Task DeleteConnectedImages(Tbl69FiSpecies selected);
    Task DeleteConnectedImages(Tbl72PlSpecies selected);
    Task<bool> DeleteImage(Tbl81Image selected);
    Task DeleteImageDataset(Tbl81Image selected);


    #endregion

    #region Save Image
    //   Task<bool> SaveImage(Tbl81Image selected, string selectedPath);
    Task<bool> SaveImage(Tbl81Image selected, string selectedPath, byte[] selectedFilestream);

    //    Task<Tbl81Image> ImageUpdate(Tbl81Image home, Tbl81Image selected, string selectedPath);
    Task<Tbl81Image> ImageUpdate(Tbl81Image home, Tbl81Image selected, string selectedPath, byte[] selectedFilestream);

    Task<Tbl81Image> ImageAdd(Tbl81Image selected, string selectedPath);
    Task ImageSave(Tbl81Image home, Tbl81Image selected);


    #endregion

    #endregion

    #region Synonym

    #region Get Synonym
    Task<ObservableCollection<Tbl84Synonym>> GetTbl84SynonymsCollectionOrderBySynonymNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl84Synonym> GetTbl84SynonymsCollectionOrderBySynonymNameFromFiSpeciesId(int fispeciesId);
    ObservableCollection<Tbl84Synonym> GetTbl84SynonymsCollectionOrderBySynonymNameFromPlSpeciesId(int plspeciesId);
    Task<ObservableCollection<Tbl84Synonym>> GetLastDatasetInTbl84Synonyms();
    Task<Tbl84Synonym> GetSynonymSingleBySynonymId(int synonymId);
    Task<Tbl84Synonym> GetNameSingleBySynonymNameAndAuthorAndAuthorYear(string synonymName, string author, string authorYear);

    #endregion Get Synonym

    #region Copy Synonym
    Task<ObservableCollection<Tbl84Synonym>> CopySynonym(Tbl84Synonym selected);


    #endregion Copy Synonym

    #region Delete Synonym
    Task DeleteConnectedSynonyms(Tbl69FiSpecies selected);
    Task DeleteConnectedSynonyms(Tbl72PlSpecies selected);
    Task<bool> DeleteSynonym(Tbl84Synonym selected);
    Task DeleteSynonymDataset(Tbl84Synonym selected);


    #endregion Delete Synonym

    #region Save Synonym
    Task<bool> SaveSynonym(Tbl84Synonym selected);
    Task<Tbl84Synonym> SynonymUpdate(Tbl84Synonym home, Tbl84Synonym selected);
    Task<Tbl84Synonym> SynonymAdd(Tbl84Synonym selected);
    Task SynonymSave(Tbl84Synonym home, Tbl84Synonym selected);


    #endregion Save Synonym


    #endregion Synonym

    #region Geographic

    #region Get Geographic
    Task<ObservableCollection<Tbl87Geographic>> GetTbl87GeographicsCollectionOrderByInfoFromSearchGeographicId(string searchInfo);
    Task<ObservableCollection<Tbl87Geographic>> GetTbl87GeographicsCollectionOrderByInfoFromSearchInfoOrGeographicId(string searchInfoOrId);


    ObservableCollection<Tbl87Geographic> GetTbl87GeographicsCollectionOrderByInfoFromFiSpeciesId(int fispeciesId);
    ObservableCollection<Tbl87Geographic> GetTbl87GeographicsCollectionOrderByInfoFromPlSpeciesId(int plspeciesId);
    ObservableCollection<Tbl87Geographic> GetTbl87GeographicsCollectionOrderByInfoFromGeographicId(int geographicId);
    Task<Tbl87Geographic> GetGeographicSingleByGeographicId(int geographicId);
    Task<ObservableCollection<Tbl87Geographic>> GetLastDatasetInTbl87Geographics();

    #endregion

    #region Copy Geographic

    Task<ObservableCollection<Tbl87Geographic>> CopyGeographic(Tbl87Geographic selected);


    #endregion

    #region Delete Geographic

    Task DeleteConnectedGeographics(Tbl69FiSpecies selected);
    Task DeleteConnectedGeographics(Tbl72PlSpecies selected);
    Task<bool> DeleteGeographic(Tbl87Geographic selected);
    Task DeleteGeographicDataset(Tbl87Geographic selected);


    #endregion

    #region Save Geographic
    Task<bool> SaveGeographic(Tbl87Geographic selected);
    Task<Tbl87Geographic> GeographicUpdate(Tbl87Geographic home, Tbl87Geographic selected);
    Task<Tbl87Geographic> GeographicAdd(Tbl87Geographic selected);
    Task GeographicSave(Tbl87Geographic home, Tbl87Geographic selected);

    #endregion

    #endregion

    #region Country

    #region Get Country
    ObservableCollection<TblCountry> GetTblCountriesCollectionOrderByName();
    Task<ObservableCollection<TblCountry>> GetTblCountriesCollectionOrderByNameFromSearchNameOrId(string searchName);
    Task<ObservableCollection<TblCountry>> GetLastDatasetInTblCountries();
    Task<TblCountry> GetCountrySingleByCountryId(int countryId);
    Task<TblCountry> GetCountrySingleByName(string name);


    #endregion Country

    #region Copy Country
    Task<ObservableCollection<TblCountry>> CopyCountry(TblCountry selected);

    #endregion Country

    #region Delete Country
    Task<bool> DeleteCountry(TblCountry selected);
    Task DeleteCountryDataset(TblCountry selected);


    #endregion Country

    #region Save Country
    Task<bool> SaveCountry(TblCountry selected);
    Task<TblCountry> CountryUpdate(TblCountry home, TblCountry selected);
    Task<TblCountry> CountryAdd(TblCountry selected);
    Task CountrySave(TblCountry home, TblCountry selected);

    #endregion Country

    #endregion Country

    #region UserProfile

    #region Get UserProfile

    Task<ObservableCollection<TblUserProfile>> GetTblUserProfilesCollectionOrderByNameFromSearchNameOrId(string searchName);
    Task<ObservableCollection<TblUserProfile>> GetTblUserProfilesCollectionOrderByEmailFromSearchEmailOrId(string searchEmail);

    Task<ObservableCollection<TblUserProfile>> GetLastDatasetInTblUserProfiles();
    Task<TblUserProfile> GetUserProfileSingleByUserProfileId(int userprofileId);
    Task<TblUserProfile> GetUserProfileSingleByEMail(string email);

    #endregion

    #region Copy UserProfile

    Task<ObservableCollection<TblUserProfile>> CopyUserProfile(TblUserProfile selected);



    #endregion

    #region Delete UserProfile

    Task<bool> DeleteUserProfile(TblUserProfile selected);
    Task DeleteUserProfileDataset(TblUserProfile selected);



    #endregion

    #region Save UserProfile
    Task<bool> SaveUserProfile(TblUserProfile selected);
    Task<TblUserProfile> UserProfileAdd(TblUserProfile selected);

    Task<TblUserProfile> UserProfileUpdate(TblUserProfile home, TblUserProfile selected);

    Task UserProfileSave(TblUserProfile home, TblUserProfile selected);


    #endregion

    #endregion


    #region References Expert, Source, Author from Regnum-Userprofile

    #region References

    #region Get Reference

    ObservableCollection<Tbl90Reference> GetTbl90ReferencesCollectionOrderByInfoFromSearchInfoOrId(string searchInfo);

    ObservableCollection<Tbl90Reference> GetTbl90ReferencesExpertsCollectionOrderByInfoFromSearchInfoOrId(string searchInfo);
    ObservableCollection<Tbl90Reference> GetTbl90ReferencesSourcesCollectionOrderByInfoFromSearchInfoOrId(string searchInfo);
    ObservableCollection<Tbl90Reference> GetTbl90ReferencesAuthorsCollectionOrderByInfoFromSearchInfoOrId(string searchInfo);

    ObservableCollection<Tbl90Reference> GetTbl90ReferencesCollectionOrderByInfoFromReferenceId(int referenceId);


    Task<ObservableCollection<Tbl90Reference>> GetTbl90ReferencesCollectionOrderByFromRegnumId(int regnumId);
    //----------------------------------------------------------
    ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceExpertsCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(
            int? regnumId);
    ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceSourcesCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(
            int? regnumId);
    ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceAuthorsCollectionOrderByInfoFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(
            int? regnumId);
    //----------------------------------------------------------
    ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceExpertsCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(
            int? phylumId);
    ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceSourcesCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(
            int? phylumId);
    ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceAuthorsCollectionOrderByInfoFromPhylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(
            int? phylumId);
    //----------------------------------------------------------

    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromDivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? divisionId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromDivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? divisionId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromDivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? divisionId);
    //----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubphylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? subphylumId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? subphylumId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? subphylumId);
    //----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? subdivisionId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? subdivisionId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? subdivisionId);
    //----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? superclassId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? superclassId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? superclassId);
    //----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromClassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? classId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? classId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? classId);
    //----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? subclassId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? subclassId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? subclassId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfraclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? infraclassId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? infraclassId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? infraclassId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromLegioIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? legioId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? legioId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? legioId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromOrdoIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? ordoId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromOrdoIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? ordoId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromOrdoIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? ordoId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubordoIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? subordoId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubordoIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? subordoId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubordoIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? subordoId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfraordoIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? infraordoId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfraordoIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? infraordoId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfraordoIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? infraordoId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSuperfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? superfamilyId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSuperfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? superfamilyId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSuperfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? superfamilyId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromFamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? familyId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? familyId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? familyId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? subfamilyId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? subfamilyId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? subfamilyId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfrafamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? infrafamilyId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfrafamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? infrafamilyId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfrafamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? infrafamilyId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSupertribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? supertribusId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? supertribusId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? supertribusId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromTribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? tribusId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromTribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? tribusId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromTribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? tribusId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubtribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? subtribusId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubtribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? subtribusId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubtribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? subtribusId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfratribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? infratribusId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? infratribusId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? infratribusId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromGenusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? genusId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? genusId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? genusId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromFiSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? fispeciesId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? fispeciesId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? fispeciesId);
    //-----------------------------------------------------------
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromPlSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? plspeciesId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromPlSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? plspeciesId);
    ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromPlSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? plspeciesId);

    //-----------------------------------------------------------
    //-----------------------------------------------------------
    //-----------------------------------------------------------

    Task<Tbl90Reference> GetReferenceSingleByReferenceId(int referenceId);

    Task<ObservableCollection<Tbl90Reference>> GetLastDatasetInTbl90References();
    #endregion

    #region Copy Reference
    Task<ObservableCollection<Tbl90Reference>> CopyReference(Tbl90Reference selected);

    Task<ObservableCollection<Tbl90Reference>> CopyReferenceRegnum(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferencePhylum(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceDivision(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceSubphylum(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceSubdivision(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceSuperclass(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceClass(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceSubclass(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceInfraclass(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceLegio(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceOrdo(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceSubordo(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceInfraordo(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceSuperfamily(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceFamily(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceSubfamily(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceInfrafamily(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceSupertribus(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceTribus(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceSubtribus(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceInfratribus(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceGenus(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferenceFiSpecies(Tbl90Reference selected, string reference);
    Task<ObservableCollection<Tbl90Reference>> CopyReferencePlSpecies(Tbl90Reference selected, string reference);

    Task<ObservableCollection<Tbl90Reference>> CopyReferenceSource(Tbl90Reference selected);
    #endregion

    #region Delete Reference
    Task<bool> DeleteReference(Tbl90Reference selected);
    Task DeleteReferenceDataset(Tbl90Reference selected);


    Task DeleteTbl90ReferenceDatasetsFromPhylumId(int phylumId);


    Task DeleteRegnumReferences(int regnumId);
    Task DeletePhylumReferences(int phylumId);
    Task DeleteDivisionReferences(int divisionId);
    Task DeleteSubphylumReferences(int subphylumId);
    Task DeleteSubdivisionReferences(int subdivisionId);
    Task DeleteSuperclassReferences(int superclassId);

    Task DeleteReferences(ObservableCollection<Tbl90Reference> coll);

    Task DeleteDatasetsInTbl90References(ObservableCollection<Tbl90Reference> tbl90ReferencesList);
    Task DeleteConnectedRegnumReferences(Tbl03Regnum selected);
    Task DeleteConnectedPhylumReferences(Tbl06Phylum selected);
    Task DeleteConnectedDivisionReferences(Tbl09Division selected);
    Task DeleteConnectedSubphylumReferences(Tbl12Subphylum selected);
    Task DeleteConnectedSubdivisionReferences(Tbl15Subdivision selected);
    Task DeleteConnectedSuperclassReferences(Tbl18Superclass selected);
    Task DeleteConnectedClassReferences(Tbl21Class selected);
    Task DeleteConnectedSubclassReferences(Tbl24Subclass selected);
    Task DeleteConnectedInfraclassReferences(Tbl27Infraclass selected);
    Task DeleteConnectedLegioReferences(Tbl30Legio selected);
    Task DeleteConnectedOrdoReferences(Tbl33Ordo selected);
    Task DeleteConnectedSubordoReferences(Tbl36Subordo selected);
    Task DeleteConnectedInfraordoReferences(Tbl39Infraordo selected);
    Task DeleteConnectedSuperfamilyReferences(Tbl42Superfamily selected);
    Task DeleteConnectedFamilyReferences(Tbl45Family selected);
    Task DeleteConnectedSubfamilyReferences(Tbl48Subfamily selected);
    Task DeleteConnectedInfrafamilyReferences(Tbl51Infrafamily selected);
    Task DeleteConnectedSupertribusReferences(Tbl54Supertribus selected);
    Task DeleteConnectedTribusReferences(Tbl57Tribus selected);
    Task DeleteConnectedSubtribusReferences(Tbl60Subtribus selected);
    Task DeleteConnectedInfratribusReferences(Tbl63Infratribus selected);
    Task DeleteConnectedGenusReferences(Tbl66Genus selected);
    Task DeleteConnectedFiSpeciesReferences(Tbl69FiSpecies selected);
    Task DeleteConnectedPlSpeciesReferences(Tbl72PlSpecies selected);

    #endregion

    #region Save Reference
    Task<bool> SaveReference(Tbl90Reference selected, string reference);
    Task<Tbl90Reference> ReferenceUpdate(Tbl90Reference home, Tbl90Reference selected, string reference);
    Task<Tbl90Reference> ReferenceAdd(Tbl90Reference selected, string reference);
    Task ReferenceSave(Tbl90Reference home, Tbl90Reference selected);
    #endregion

    #endregion References

    #region RefAuthor

    #region Get RefAuthor  
    ObservableCollection<Tbl90RefAuthor> GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1();
    Task<ObservableCollection<Tbl90RefAuthor>> GetTbl90RefAuthorsCollectionOrderByRefAuthorNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl90RefAuthor> GetTbl90RefAuthorsCollectionOrderByRefAuthorNameFromRefAuthorId(int? refAuthorId);
    Task<Tbl90RefAuthor> GetRefAuthorSingleByRefAuthorId(int refAuthorId);
    Task<Tbl90RefAuthor> GetRefAuthorSingleByRefAuthorNameAndArticelTitleAndBookNameAndPage(string refAuthorName, string articelTitle, string bookName, string page);
    Task<ObservableCollection<Tbl90RefAuthor>> GetLastDatasetInTbl90RefAuthors();

    #endregion Get RefAuthor

    #region Copy RefAuthor
    Task<ObservableCollection<Tbl90RefAuthor>> CopyRefAuthor(Tbl90RefAuthor selected);

    #endregion Copy RefAuthor

    #region Delete RefAuthor

    Task<bool> DeleteRefAuthor(Tbl90RefAuthor selected);
    Task DeleteRefAuthorDataset(Tbl90RefAuthor selected);

    #endregion Delete RefAuthor

    #region Save RefAuthor
    Task<bool> SaveRefAuthor(Tbl90RefAuthor selected);
    Task<Tbl90RefAuthor> RefAuthorUpdate(Tbl90RefAuthor home, Tbl90RefAuthor selected);
    Task<Tbl90RefAuthor> RefAuthorAdd(Tbl90RefAuthor selected);
    Task RefAuthorSave(Tbl90RefAuthor home, Tbl90RefAuthor selected);

    #endregion Save RefAuthor

    #endregion RefAuthor

    #region RefSource

    #region Get RefSource
    ObservableCollection<Tbl90RefSource> GetTbl90RefSourcesCollectionOrderByRefSourceName();
    Task<ObservableCollection<Tbl90RefSource>> GetTbl90RefSourcesCollectionOrderByRefSourceNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl90RefSource> GetTbl90RefSourcesCollectionOrderByRefSourceNameFromRefSourceId(int? refSourceId);

    Task<Tbl90RefSource> GetRefSourceSingleByRefSourceId(int refSourceId);
    Task<Tbl90RefSource> GetRefSourceSingleByRefSourceNameAndAuthor(string refSourceName, string author);

    Task<ObservableCollection<Tbl90RefSource>> GetLastDatasetInTbl90RefSources();

    #endregion Get RefSource

    #region Copy RefSource
    Task<ObservableCollection<Tbl90RefSource>> CopyRefSource(Tbl90RefSource selected);

    #endregion Copy RefSource

    #region Delete RefSource
    Task<bool> DeleteRefSource(Tbl90RefSource selected);
    Task DeleteRefSourceDataset(Tbl90RefSource selected);

    #endregion Delete RefSource

    #region Save RefSource
    Task<bool> SaveRefSource(Tbl90RefSource selected);
    Task<Tbl90RefSource> RefSourceUpdate(Tbl90RefSource home, Tbl90RefSource selected);
    Task<Tbl90RefSource> RefSourceAdd(Tbl90RefSource selected);
    Task RefSourceSave(Tbl90RefSource home, Tbl90RefSource selected);

    #endregion Save RefSource

    #endregion RefAuthor

    #region RefExpert

    #region Get RefExpert
    ObservableCollection<Tbl90RefExpert> GetTbl90RefExpertsCollectionOrderByRefExpertName();
    Task<ObservableCollection<Tbl90RefExpert>> GetTbl90RefExpertsCollectionOrderByRefExpertNameFromSearchNameOrId(string searchName);
    ObservableCollection<Tbl90RefExpert> GetTbl90RefExpertsCollectionOrderByRefExpertNameFromRefExpertId(int? refExpertId);

    Task<Tbl90RefExpert> GetRefExpertSingleByRefExpertId(int refExpertId);
    Task<Tbl90RefExpert> GetRefExpertSingleByRefExpertName(string refExpertName);


    Task<ObservableCollection<Tbl90RefExpert>> GetLastDatasetInTbl90RefExperts();

    #endregion Get RefExpert

    #region Copy RefExpert
    Task<ObservableCollection<Tbl90RefExpert>> CopyRefExpert(Tbl90RefExpert selected);

    #endregion Copy RefExpert

    #region Delete RefExpert
    Task<bool> DeleteRefExpert(Tbl90RefExpert selected);
    Task DeleteRefExpertDataset(Tbl90RefExpert selected);

    #endregion Delete RefExpert

    #region Save RefExpert
    Task<bool> SaveRefExpert(Tbl90RefExpert selected);
    Task<Tbl90RefExpert> RefExpertUpdate(Tbl90RefExpert home, Tbl90RefExpert selected);
    Task<Tbl90RefExpert> RefExpertAdd(Tbl90RefExpert selected);
    Task RefExpertSave(Tbl90RefExpert home, Tbl90RefExpert selected);

    #endregion Save RefExpert

    #endregion RefExpert


    #endregion References Expert, Source, Author from Regnum-Userprofile

    #region Comments  from Regnum-Userprofile

    #region Get Comment
    Task<ObservableCollection<Tbl93Comment>> GetTbl93CommentsCollectionOrderByInfoFromSearchInfoOrId(string searchInfo);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromRegnumId(int? regnumId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromPhylumId(int? phylumId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromDivisionId(int? divisionId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSubphylumId(int? subphylumId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSubdivisionId(int? subdivisionId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSuperclassId(int? superclassId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromClassId(int? classId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSubclassId(int? subclassId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromInfraclassId(int? infraclassId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromLegioId(int? legioId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromOrdoId(int? ordoId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSubordoId(int? subordoId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromInfraordoId(int? infraordoId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSuperfamilyId(int? superfamilyId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromFamilyId(int? familyId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSubfamilyId(int? subfamilyId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromInfrafamilyId(int? infrafamilyId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSupertribusId(int? supertribusId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromTribusId(int? tribusId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSubtribusId(int? subtribusId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromInfratribusId(int? infratribusId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromGenusId(int? genusId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromFiSpeciesId(int? fispeciesId);
    ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromPlSpeciesId(int? plspeciesId);
    //--------------------------------------------------------
    Task<ObservableCollection<Tbl93Comment>> GetLastDatasetInTbl93Comments();

    Task<Tbl93Comment> GetCommentSingleByCommentId(int id);
    #endregion

    #region Copy Comment
    Task<ObservableCollection<Tbl93Comment>> CopyComment(Tbl93Comment selected);
    #endregion

    #region Delete Comment
    Task DeleteRegnumComments(int regnumId);
    Task DeletePhylumComments(int phylumId);
    Task DeleteDivisionComments(int divisionId);
    Task DeleteSubphylumComments(int subphylumId);
    Task DeleteSubdivisionComments(int subdivisionId);
    Task DeleteSuperclassComments(int superclassId);

    Task DeleteTbl93CommentDatasetsFromPhylumId(int phylumId);


    Task<bool> DeleteComment(Tbl93Comment selected);
    Task DeleteCommentDataset(Tbl93Comment selected);
    Task DeleteComments(ObservableCollection<Tbl93Comment> coll);
    Task DeleteDatasetsInTbl93Comments(ObservableCollection<Tbl93Comment> tbl93CommentsList);
    Task DeleteConnectedRegnumComments(Tbl03Regnum selected);
    Task DeleteConnectedPhylumComments(Tbl06Phylum selected);
    Task DeleteConnectedDivisionComments(Tbl09Division selected);
    Task DeleteConnectedSubphylumComments(Tbl12Subphylum selected);
    Task DeleteConnectedSubdivisionComments(Tbl15Subdivision selected);
    Task DeleteConnectedSuperclassComments(Tbl18Superclass selected);
    Task DeleteConnectedClassComments(Tbl21Class selected);
    Task DeleteConnectedSubclassComments(Tbl24Subclass selected);
    Task DeleteConnectedInfraclassComments(Tbl27Infraclass selected);
    Task DeleteConnectedLegioComments(Tbl30Legio selected);
    Task DeleteConnectedOrdoComments(Tbl33Ordo selected);
    Task DeleteConnectedSubordoComments(Tbl36Subordo selected);
    Task DeleteConnectedInfraordoComments(Tbl39Infraordo selected);
    Task DeleteConnectedSuperfamilyComments(Tbl42Superfamily selected);
    Task DeleteConnectedFamilyComments(Tbl45Family selected);
    Task DeleteConnectedSubfamilyComments(Tbl48Subfamily selected);
    Task DeleteConnectedInfrafamilyComments(Tbl51Infrafamily selected);
    Task DeleteConnectedSupertribusComments(Tbl54Supertribus selected);
    Task DeleteConnectedTribusComments(Tbl57Tribus selected);
    Task DeleteConnectedSubtribusComments(Tbl60Subtribus selected);
    Task DeleteConnectedInfratribusComments(Tbl63Infratribus selected);
    Task DeleteConnectedGenusComments(Tbl66Genus selected);
    Task DeleteConnectedFiSpeciesComments(Tbl69FiSpecies selected);
    Task DeleteConnectedPlSpeciesComments(Tbl72PlSpecies selected);

    #endregion

    #region Save Comment
    Task<bool> SaveComment(Tbl93Comment selected);
    Task<Tbl93Comment> CommentUpdate(Tbl93Comment home, Tbl93Comment selected);
    Task<Tbl93Comment> CommentAdd(Tbl93Comment selected);
    Task CommentSave(Tbl93Comment home, Tbl93Comment selected);
    #endregion

    #endregion Comments  from Regnum-Userprofile

    #endregion CRUD

    #region Search_Gets

    ObservableCollection<Tbl03Regnum> GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromFilterText(string filterText);
    ObservableCollection<Tbl06Phylum> GetTbl06PhylumsCollectionOrderByPhylumNameFromFilterText(string filterText);
    ObservableCollection<Tbl09Division> GetTbl09DivisionsCollectionOrderByDivisionNameFromFilterText(string filterText);
    ObservableCollection<Tbl12Subphylum> GetTbl12SubphylumsCollectionOrderBySubphylumNameFromFilterText(string filterText);
    ObservableCollection<Tbl15Subdivision> GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromFilterText(string filterText);
    ObservableCollection<Tbl18Superclass> GetTbl18SuperclassesCollectionOrderBySuperclassNameFromFilterText(string filterText);
    ObservableCollection<Tbl21Class> GetTbl21ClassesCollectionOrderByClassNameFromFilterText(string filterText);
    ObservableCollection<Tbl24Subclass> GetTbl24SubclassesCollectionOrderBySubclassNameFromFilterText(string filterText);
    ObservableCollection<Tbl27Infraclass> GetTbl27InfraclassesCollectionOrderByInfraclassNameFromFilterText(string filterText);
    ObservableCollection<Tbl30Legio> GetTbl30LegiosCollectionOrderByLegioNameFromFilterText(string filterText);
    ObservableCollection<Tbl33Ordo> GetTbl33OrdosCollectionOrderByOrdoNameFromFilterText(string filterText);
    ObservableCollection<Tbl36Subordo> GetTbl36SubordosCollectionOrderBySubordoNameFromFilterText(string filterText);
    ObservableCollection<Tbl39Infraordo> GetTbl39InfraordosCollectionOrderByInfraordoNameFromFilterText(string filterText);
    ObservableCollection<Tbl42Superfamily> GetTbl42SuperfamiliesCollectionOrderBySuperfamilyNameFromFilterText(string filterText);
    ObservableCollection<Tbl45Family> GetTbl45FamiliesCollectionOrderByFamilyNameFromFilterText(string filterText);
    ObservableCollection<Tbl48Subfamily> GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromFilterText(string filterText);
    ObservableCollection<Tbl51Infrafamily> GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromFilterText(string filterText);
    ObservableCollection<Tbl54Supertribus> GetTbl54SupertribussesCollectionOrderBySupertribusNameFromFilterText(string filterText);
    ObservableCollection<Tbl57Tribus> GetTbl57TribussesCollectionOrderByTribusNameFromFilterText(string filterText);
    ObservableCollection<Tbl60Subtribus> GetTbl60SubtribussesCollectionOrderBySubtribusNameFromFilterText(string filterText);
    ObservableCollection<Tbl63Infratribus> GetTbl63InfratribussesCollectionOrderByInfratribusNameFromFilterText(string filterText);
    ObservableCollection<Tbl66Genus> GetTbl66GenussesCollectionOrderByGenusNameFromFilterText(string filterText);
    ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFilterText(string filterText);
    ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromFilterText(string filterText);
    ObservableCollection<Tbl78Name> GetTbl78NamesCollectionOrderByNameNameFromFilterText(string filterText);
    ObservableCollection<Tbl84Synonym> GetTbl84SynonymsCollectionOrderBySynonymNameFromFilterText(string filterText);


    #endregion Search_Gets


    #region Report_Gets

    #region Regnum
    Tbl03Regnum GetRegnumSingleByRegnumIdAndHash(int id);
    ObservableCollection<Tbl03Regnum> CollRegnumsByRegnumId(int id);
    //direct children
    ObservableCollection<Tbl06Phylum> CollPhylumsByRegnumIdAndHash(int id);
    ObservableCollection<Tbl09Division> CollDivisionsByRegnumIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByRegnumId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByRegnumId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByRegnumId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByRegnumId(int id);
    #endregion Regnum

    #region Phylum
    Tbl06Phylum GetPhylumSingleByPhylumIdAndHash(int id);

    ObservableCollection<Tbl06Phylum> CollPhylumsByPhylumId(int id);
    //direct children
    ObservableCollection<Tbl12Subphylum> CollSubphylumsByPhylumIdAndHash(int id);
    //Function
    int RegnumIdFromPhylumsCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl03Regnum> CollRegnumsByRegnumIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByPhylumId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByPhylumId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByPhylumId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByPhylumId(int id);
    #endregion Phylum

    #region Division
    Tbl09Division GetDivisionSingleByDivisionIdAndHash(int id);

    ObservableCollection<Tbl09Division> CollDivisionsByDivisionId(int id);
    //direct children
    ObservableCollection<Tbl15Subdivision> CollSubdivisionsByDivisionIdAndHash(int id);
    //Function
    int RegnumIdFromDivisionsCollectionSelect(int id);
    //ForeignKey
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByDivisionId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByDivisionId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByDivisionId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByDivisionId(int id);
    #endregion Division

    #region Subphylum
    Tbl12Subphylum GetSubphylumSingleBySubphylumIdAndHash(int id);

    ObservableCollection<Tbl12Subphylum> CollSubphylumsBySubphylumId(int id);
    //direct children
    ObservableCollection<Tbl18Superclass> CollSuperclassesBySubphylumIdAndHash(int id);
    //Function
    int PhylumIdFromSubphylumsCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl06Phylum> CollPhylumsByPhylumIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsBySubphylumId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesBySubphylumId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsBySubphylumId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsBySubphylumId(int id);
    #endregion Subphylum

    #region Subdivision
    Tbl15Subdivision GetSubdivisionSingleBySubdivisionIdAndHash(int id);

    ObservableCollection<Tbl15Subdivision> CollSubdivisionsBySubdivisionId(int id);
    //direct children
    ObservableCollection<Tbl18Superclass> CollSuperclassesBySubdivisionIdAndHash(int id);
    //Function
    int DivisionIdFromSubdivisionsCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl09Division> CollDivisionsByDivisionIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsBySubdivisionId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesBySubdivisionId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsBySubdivisionId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsBySubdivisionId(int id);
    #endregion Subdivision

    #region Superclass
    Tbl18Superclass GetSuperclassSingleBySuperclassIdAndHash(int id);

    ObservableCollection<Tbl18Superclass> CollSuperclassesBySuperclassId(int id);
    //direct children
    ObservableCollection<Tbl21Class> CollClassesBySuperclassIdAndHash(int id);
    //Function
    int SubdivisionIdFromSuperclassesCollectionSelect(int id);
    int SubphylumIdFromSuperclassesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl12Subphylum> CollSubphylumsBySubphylumIdAndHash(int id);
    ObservableCollection<Tbl15Subdivision> CollSubdivisionsBySubdivisionIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsBySuperclassId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesBySuperclassId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsBySuperclassId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsBySuperclassId(int id);
    #endregion Superclass

    #region Class
    Tbl21Class GetClassSingleByClassIdAndHash(int id);

    ObservableCollection<Tbl21Class> CollClassesByClassId(int id);
    //direct children
    ObservableCollection<Tbl24Subclass> CollSubclassesByClassIdAndHash(int id);
    //Function
    int SuperclassIdFromClassesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl18Superclass> CollSuperclassesBySuperclassIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByClassId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByClassId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByClassId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByClassId(int id);
    #endregion Class

    #region Subclass
    Tbl24Subclass GetSubclassSingleBySubclassIdAndHash(int id);

    ObservableCollection<Tbl24Subclass> CollSubclassesBySubclassId(int id);
    //direct children
    ObservableCollection<Tbl27Infraclass> CollInfraclassesBySubclassIdAndHash(int id);
    //Function
    int ClassIdFromSubclassesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl21Class> CollClassesByClassIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsBySubclassId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesBySubclassId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsBySubclassId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsBySubclassId(int id);
    #endregion Subclass

    #region Infraclass
    Tbl27Infraclass GetInfraclassSingleByInfraclassIdAndHash(int id);

    ObservableCollection<Tbl27Infraclass> CollInfraclassesByInfraclassId(int id);
    //direct children
    ObservableCollection<Tbl30Legio> CollLegiosByInfraclassIdAndHash(int id);
    //Function
    int SubclassIdFromInfraclassesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl24Subclass> CollSubclassesBySubclassIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByInfraclassId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByInfraclassId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByInfraclassId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByInfraclassId(int id);
    #endregion Infraclass

    #region Legio
    Tbl30Legio GetLegioSingleByLegioIdAndHash(int id);

    ObservableCollection<Tbl30Legio> CollLegiosByLegioId(int id);
    //direct children
    ObservableCollection<Tbl33Ordo> CollOrdosByLegioIdAndHash(int id);
    //Function
    int InfraclassIdFromLegiosCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl27Infraclass> CollInfraclassesByInfraclassIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByLegioId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByLegioId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByLegioId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByLegioId(int id);
    #endregion Legio

    #region Ordo
    Tbl33Ordo GetOrdoSingleByOrdoIdAndHash(int id);

    ObservableCollection<Tbl33Ordo> CollOrdosByOrdoId(int id);
    //direct children
    ObservableCollection<Tbl36Subordo> CollSubordosByOrdoIdAndHash(int id);
    //Function
    int LegioIdFromOrdosCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl30Legio> CollLegiosByLegioIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByOrdoId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByOrdoId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByOrdoId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByOrdoId(int id);
    #endregion Ordo

    #region Subordo
    Tbl36Subordo GetSubordoSingleBySubordoIdAndHash(int id);

    ObservableCollection<Tbl36Subordo> CollSubordosBySubordoId(int id);
    //direct children
    ObservableCollection<Tbl39Infraordo> CollInfraordosBySubordoIdAndHash(int id);
    //Function
    int OrdoIdFromSubordosCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl33Ordo> CollOrdosByOrdoIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsBySubordoId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesBySubordoId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsBySubordoId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsBySubordoId(int id);
    #endregion Subordo

    #region Infraordo
    Tbl39Infraordo GetInfraordoSingleByInfraordoIdAndHash(int id);

    ObservableCollection<Tbl39Infraordo> CollInfraordosByInfraordoId(int id);
    //direct children
    ObservableCollection<Tbl42Superfamily> CollSuperfamiliesByInfraordoIdAndHash(int id);
    //Function
    int SubordoIdFromInfraordosCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl36Subordo> CollSubordosBySubordoIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByInfraordoId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByInfraordoId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByInfraordoId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByInfraordoId(int id);
    #endregion Infraordo

    #region Superfamily
    Tbl42Superfamily GetSuperfamilySingleBySuperfamilyIdAndHash(int id);

    ObservableCollection<Tbl42Superfamily> CollSuperfamiliesBySuperfamilyId(int id);
    //direct children
    ObservableCollection<Tbl45Family> CollFamiliesBySuperfamilyIdAndHash(int id);
    //Function
    int InfraordoIdFromSuperfamiliesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl39Infraordo> CollInfraordosByInfraordoIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsBySuperfamilyId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesBySuperfamilyId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsBySuperfamilyId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsBySuperfamilyId(int id);
    #endregion Superfamily

    #region Family
    Tbl45Family GetFamilySingleByFamilyIdAndHash(int id);

    ObservableCollection<Tbl45Family> CollFamiliesByFamilyId(int id);
    //direct children
    ObservableCollection<Tbl48Subfamily> CollSubfamiliesByFamilyIdAndHash(int id);
    //Function
    int SuperfamilyIdFromFamiliesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl42Superfamily> CollSuperfamiliesBySuperfamilyIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByFamilyId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByFamilyId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByFamilyId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByFamilyId(int id);
    #endregion Family

    #region Subfamily
    Tbl48Subfamily GetSubfamilySingleBySubfamilyIdAndHash(int id);

    ObservableCollection<Tbl48Subfamily> CollSubfamiliesBySubfamilyId(int id);
    //direct children
    ObservableCollection<Tbl51Infrafamily> CollInfrafamiliesBySubfamilyIdAndHash(int id);
    //Function
    int FamilyIdFromSubfamiliesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl45Family> CollFamiliesByFamilyIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsBySubfamilyId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesBySubfamilyId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsBySubfamilyId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsBySubfamilyId(int id);
    #endregion Subfamily

    #region Infrafamily
    Tbl51Infrafamily GetInfrafamilySingleByInfrafamilyIdAndHash(int id);

    ObservableCollection<Tbl51Infrafamily> CollInfrafamiliesByInfrafamilyId(int id);
    //direct children
    ObservableCollection<Tbl54Supertribus> CollSupertribussesByInfrafamilyIdAndHash(int id);
    //Function
    int SubfamilyIdFromInfrafamiliesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl48Subfamily> CollSubfamiliesBySubfamilyIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByInfrafamilyId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByInfrafamilyId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByInfrafamilyId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByInfrafamilyId(int id);
    #endregion Infrafamily

    #region Supertribus
    Tbl54Supertribus GetSupertribusSingleBySupertribusIdAndHash(int id);

    ObservableCollection<Tbl54Supertribus> CollSupertribussesBySupertribusId(int id);
    //direct children
    ObservableCollection<Tbl57Tribus> CollTribussesBySupertribusIdAndHash(int id);
    //Function
    int InfrafamilyIdFromSupertribussesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl51Infrafamily> CollInfrafamiliesByInfrafamilyIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsBySupertribusId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesBySupertribusId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsBySupertribusId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsBySupertribusId(int id);
    #endregion Supertribus

    #region Tribus
    Tbl57Tribus GetTribusSingleByTribusIdAndHash(int id);

    ObservableCollection<Tbl57Tribus> CollTribussesByTribusId(int id);
    //direct children
    ObservableCollection<Tbl60Subtribus> CollSubtribussesByTribusIdAndHash(int id);
    //Function
    int SupertribusIdFromTribussesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl54Supertribus> CollSupertribussesBySupertribusIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByTribusId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByTribusId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByTribusId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByTribusId(int id);
    #endregion Tribus

    #region Subtribus
    Tbl60Subtribus GetSubtribusSingleBySubtribusIdAndHash(int id);

    ObservableCollection<Tbl60Subtribus> CollSubtribussesBySubtribusId(int id);
    //direct children
    ObservableCollection<Tbl63Infratribus> CollInfratribussesBySubtribusIdAndHash(int id);
    //Function
    int TribusIdFromSubtribussesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl57Tribus> CollTribussesByTribusIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsBySubtribusId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesBySubtribusId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsBySubtribusId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsBySubtribusId(int id);
    #endregion Subtribus

    #region Infratribus
    Tbl63Infratribus GetInfratribusSingleByInfratribusIdAndHash(int id);

    ObservableCollection<Tbl63Infratribus> CollInfratribussesByInfratribusId(int id);
    //direct children
    ObservableCollection<Tbl66Genus> CollGenussesByInfratribusIdAndHash(int id);
    //Function
    int SubtribusIdFromInfratribussesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl60Subtribus> CollSubtribussesBySubtribusIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByInfratribusId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByInfratribusId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByInfratribusId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByInfratribusId(int id);
    #endregion Infratribus

    #region Genus
    Tbl66Genus GetGenusSingleByGenusIdAndHash(int id);

    ObservableCollection<Tbl66Genus> CollGenussesByGenusId(int id);
    //direct children
    ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByGenusIdAndHash(int id);
    ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByGenusIdAndSubspeciesIsNullAndHash(int id);
    ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByGenusIdAndHash(int id);
    ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByGenusIdAndSubspeciesIsNullAndHash(int id);
    //Function
    int InfratribusIdFromGenussesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl63Infratribus> CollInfratribussesByInfratribusIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByGenusId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByGenusId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByGenusId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByGenusId(int id);
    #endregion Genus

    #region FiSpecies
    Tbl69FiSpecies GetFiSpeciesSingleByFiSpeciesIdAndHash(int id);

    ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByFiSpeciesId(int id);
    ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByFiSpeciesNameAndNotEmptySubspecies(int id, string name);
    ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByGenusIdAndFiSpeciesNameAndEmptySubspecies(int id, string name);
    ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByFiSpeciesNameAndSubspeciesAndDivers(string fiSpeciesName, string subspecies,
        string divers);
    //direct children
    ObservableCollection<Tbl78Name> CollNamesByFiSpeciesIdAndHash(int id);
    ObservableCollection<Tbl84Synonym> CollSynonymsByFiSpeciesIdAndHash(int id);
    //Function
    int GenusIdFromFiSpeciessesCollectionSelect(int id);
    //ForeignKey
    ObservableCollection<Tbl66Genus> CollGenussesByGenusIdAndHash(int id);
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByFiSpeciesId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByFiSpeciesId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByFiSpeciesId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByFiSpeciesId(int id);
    //Images
    ObservableCollection<Tbl81Image> CollImagesByFiSpeciesId(int id);
    //Geographics
    ObservableCollection<Tbl87Geographic> CollGeographicsByFiSpeciesId(int id);
    #endregion FiSpecies

    #region PlSpecies
    Tbl72PlSpecies GetPlSpeciesSingleByPlSpeciesIdAndHash(int id);

    ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByPlSpeciesId(int id);
    ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByPlSpeciesNameAndNotEmptySubspecies(int id, string name);
    ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByGenusIdAndPlSpeciesNameAndEmptySubspecies(int id, string name);
    ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByPlSpeciesNameAndSubspeciesAndDivers(string plSpeciesName, string subspecies,
        string divers);

    //direct children
    ObservableCollection<Tbl78Name> CollNamesByPlSpeciesIdAndHash(int id);
    ObservableCollection<Tbl84Synonym> CollSynonymsByPlSpeciesIdAndHash(int id);
    //Function
    int GenusIdFromPlSpeciessesCollectionSelect(int id);
    //ForeignKey
    //References
    ObservableCollection<Tbl90Reference> CollExpertsByPlSpeciesId(int id);
    ObservableCollection<Tbl90Reference> CollSourcesByPlSpeciesId(int id);
    ObservableCollection<Tbl90Reference> CollAuthorsByPlSpeciesId(int id);
    //Comments
    ObservableCollection<Tbl93Comment> CollCommentsByPlSpeciesId(int id);
    //Images
    ObservableCollection<Tbl81Image> CollImagesByPlSpeciesId(int id);
    //Geographics
    ObservableCollection<Tbl87Geographic> CollGeographicsByPlSpeciesId(int id);
    #endregion PlSpecies

    #endregion Report_Gets


}
