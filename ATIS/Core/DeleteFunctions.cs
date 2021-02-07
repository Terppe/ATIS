using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ATIS.Dal.Models;
using ATIS.Ui.Helper;
using log4net;

namespace ATIS.Ui.Core
{
    public class DeleteFunctions : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DeleteFunctions));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();

        #region Delete from Regnum-Userprofile

        public void DeleteRegnum(Tbl03Regnum currentTbl03Regnum)
        {
            Tbl06PhylumsList = _extCrud.GetConnectedDatasetsWithRegnumIdInTablePhylum(currentTbl03Regnum.RegnumId);
            Tbl09DivisionsList = _extCrud.GetConnectedDatasetsWithRegnumIdInTableDivision(currentTbl03Regnum.RegnumId);

            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl06PhylumsList.Count, "Phylum")) return;
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl09DivisionsList.Count, "Division")) return;

            DeleteRegnumReferences(currentTbl03Regnum.RegnumId);

            DeleteRegnumComments(currentTbl03Regnum.RegnumId);
            try
            {
                var dataset = _uow.Tbl03Regnums.GetById(currentTbl03Regnum.RegnumId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl03Regnum.RegnumName)) return;

                    _extCrud.DeleteRegnum(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl03Regnum.RegnumName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl03Regnum.RegnumName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeletePhylum(Tbl06Phylum currentTbl06Phylum)
        {
            Tbl12SubphylumsList = _extCrud.GetConnectedDatasetsWithPhylumIdInTableSubphylum(currentTbl06Phylum.PhylumId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl12SubphylumsList.Count, "Subphylum")) return;

            DeletePhylumReferences(currentTbl06Phylum.PhylumId);
            DeletePhylumComments(currentTbl06Phylum.PhylumId);
            try
            {
                var dataset = _uow.Tbl06Phylums.GetById(currentTbl06Phylum.PhylumId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl06Phylum.PhylumName)) return;

                    _extCrud.DeletePhylum(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl06Phylum.PhylumName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl06Phylum.PhylumName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteDivision(Tbl09Division currentTbl09Division)
        {
            Tbl15SubdivisionsList = _extCrud.GetConnectedDatasetsWithDivisionIdInTableSubdivision(CurrentTbl09Division.DivisionId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl15SubdivisionsList.Count, "Subdivision")) return;

            DeleteDivisionReferences(currentTbl09Division.DivisionId);

            DeleteDivisionComments(currentTbl09Division.DivisionId);
            try
            {
                var dataset = _uow.Tbl09Divisions.GetById(CurrentTbl09Division.DivisionId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl09Division.DivisionName)) return;

                    _extCrud.DeleteDivision(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl09Division.DivisionName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + CurrentTbl09Division.DivisionName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteSubphylum(Tbl12Subphylum currentTbl12Subphylum)
        {
            Tbl18SuperclassesList = _extCrud.GetConnectedDatasetsWithSubphylumIdInTableSuperclass(currentTbl12Subphylum.SubphylumId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl18SuperclassesList.Count, "Superclass")) return;

            DeleteSubphylumReferences(currentTbl12Subphylum.SubphylumId);

            DeleteSubphylumComments(currentTbl12Subphylum.SubphylumId);
            try
            {
                var dataset = _uow.Tbl06Phylums.GetById(currentTbl12Subphylum.SubphylumId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl12Subphylum.SubphylumName)) return;

                    _extCrud.DeletePhylum(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl12Subphylum.SubphylumName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl12Subphylum.SubphylumName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteSubdivision(Tbl15Subdivision currentTbl15Subdivision)
        {
            Tbl18SuperclassesList = _extCrud.GetConnectedDatasetsWithSubdivisionIdInTableSuperclass(currentTbl15Subdivision.SubdivisionId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl18SuperclassesList.Count, "Superclass")) return;

            DeleteSubdivisionReferences(currentTbl15Subdivision.SubdivisionId);

            DeleteSubdivisionComments(currentTbl15Subdivision.SubdivisionId);
            try
            {
                var dataset = _uow.Tbl15Subdivisions.GetById(currentTbl15Subdivision.SubdivisionId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl15Subdivision.SubdivisionName)) return;

                    _extCrud.DeleteSubdivision(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl15Subdivision.SubdivisionName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl15Subdivision.SubdivisionName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

        }
        public void DeleteSuperclass(Tbl18Superclass currentTbl18Superclass)
        {
            Tbl21ClassesList = _extCrud.GetConnectedDatasetsWithSuperclassIdInTableClass(currentTbl18Superclass.SuperclassId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl21ClassesList.Count, "Class")) return;

            DeleteSuperclassReferences(currentTbl18Superclass.SuperclassId);

            DeleteSuperclassComments(currentTbl18Superclass.SuperclassId);
            try
            {
                var dataset = _uow.Tbl18Superclasses.GetById(currentTbl18Superclass.SuperclassId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl18Superclass.SuperclassName)) return;

                    _extCrud.DeleteSuperclass(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl18Superclass.SuperclassName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl18Superclass.SuperclassName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

        }
        public void DeleteClass(Tbl21Class currentTbl21Class)
        {
            Tbl24SubclassesList = _extCrud.GetConnectedDatasetsWithClassIdInTableSubclass(currentTbl21Class.ClassId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl24SubclassesList.Count, "Subclass")) return;

            DeleteClassReferences(currentTbl21Class.ClassId);

            DeleteClassComments(currentTbl21Class.ClassId);
            try
            {
                var dataset = _uow.Tbl21Classes.GetById(currentTbl21Class.ClassId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl21Class.ClassName)) return;

                    _extCrud.DeleteClass(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl21Class.ClassName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl21Class.ClassName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

        }
        public void DeleteSubclass(Tbl24Subclass currentTbl24Subclass)
        {
            Tbl27InfraclassesList = _extCrud.GetConnectedDatasetsWithSubclassIdInTableInfraclass(currentTbl24Subclass.SubclassId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl27InfraclassesList.Count, "Infraclass")) return;

            DeleteSubclassReferences(currentTbl24Subclass.SubclassId);

            DeleteSubclassComments(currentTbl24Subclass.SubclassId);
            try
            {
                var dataset = _uow.Tbl24Subclasses.GetById(currentTbl24Subclass.SubclassId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl24Subclass.SubclassName)) return;

                    _extCrud.DeleteSubclass(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl24Subclass.SubclassName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl24Subclass.SubclassName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

        }
        public void DeleteInfraclass(Tbl27Infraclass currentTbl27Infraclass)
        {
            Tbl30LegiosList = _extCrud.GetConnectedDatasetsWithInfraclassIdInTableLegio(currentTbl27Infraclass.InfraclassId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl30LegiosList.Count, "Legio")) return;

            DeleteInfraclassReferences(currentTbl27Infraclass.InfraclassId);

            DeleteInfraclassComments(currentTbl27Infraclass.InfraclassId);
            try
            {
                var dataset = _uow.Tbl27Infraclasses.GetById(currentTbl27Infraclass.InfraclassId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl27Infraclass.InfraclassName)) return;

                    _extCrud.DeleteInfraclass(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl27Infraclass.InfraclassName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl27Infraclass.InfraclassName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

        }
        public void DeleteLegio(Tbl30Legio currentTbl30Legio)
        {
            Tbl33OrdosList = _extCrud.GetConnectedDatasetsWithLegioIdInTableOrdo(currentTbl30Legio.LegioId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl33OrdosList.Count, "Ordo")) return;

            DeleteLegioReferences(currentTbl30Legio.LegioId);

            DeleteLegioComments(currentTbl30Legio.LegioId);
            try
            {
                var dataset = _uow.Tbl30Legios.GetById(currentTbl30Legio.LegioId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl30Legio.LegioName)) return;

                    _extCrud.DeleteLegio(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl30Legio.LegioName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl30Legio.LegioName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

        }
        public void DeleteOrdo(Tbl33Ordo currentTbl33Ordo)
        {
            Tbl36SubordosList = _extCrud.GetConnectedDatasetsWithOrdoIdInTableSubordo(currentTbl33Ordo.OrdoId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl36SubordosList.Count, "Subordo")) return;

            DeleteOrdoReferences(currentTbl33Ordo.OrdoId);

            DeleteOrdoComments(currentTbl33Ordo.OrdoId);
            try
            {
                var dataset = _uow.Tbl33Ordos.GetById(currentTbl33Ordo.OrdoId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl33Ordo.OrdoName)) return;

                    _extCrud.DeleteOrdo(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl33Ordo.OrdoName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl33Ordo.OrdoName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

        }
        public void DeleteSubordo(Tbl36Subordo currentTbl36Subordo)
        {
            Tbl39InfraordosList = _extCrud.GetConnectedDatasetsWithSubordoIdInTableInfraordo(currentTbl36Subordo.SubordoId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl39InfraordosList.Count, "Infraordo")) return;

            DeleteSubordoReferences(currentTbl36Subordo.SubordoId);

            DeleteSubordoComments(currentTbl36Subordo.SubordoId);
            try
            {
                var dataset = _uow.Tbl36Subordos.GetById(currentTbl36Subordo.SubordoId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl36Subordo.SubordoName)) return;

                    _extCrud.DeleteSubordo(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl36Subordo.SubordoName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl36Subordo.SubordoName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

        }
        public void DeleteInfraordo(Tbl39Infraordo currentTbl39Infraordo)
        {
            Tbl42SuperfamiliesList = _extCrud.GetConnectedDatasetsWithInfraordoIdInTableSuperfamily(currentTbl39Infraordo.InfraordoId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl42SuperfamiliesList.Count, "Superfamily")) return;

            DeleteInfraordoReferences(currentTbl39Infraordo.InfraordoId);

            DeleteInfraordoComments(currentTbl39Infraordo.InfraordoId);
            try
            {
                var dataset = _uow.Tbl39Infraordos.GetById(currentTbl39Infraordo.InfraordoId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl39Infraordo.InfraordoName)) return;

                    _extCrud.DeleteInfraordo(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl39Infraordo.InfraordoName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl39Infraordo.InfraordoName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteSuperfamily(Tbl42Superfamily currentTbl42Superfamily)
        {
            Tbl45FamiliesList = _extCrud.GetConnectedDatasetsWithSuperfamilyIdInTableFamily(currentTbl42Superfamily.SuperfamilyId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl45FamiliesList.Count, "Family")) return;

            DeleteSuperfamilyReferences(currentTbl42Superfamily.SuperfamilyId);

            DeleteSuperfamilyComments(currentTbl42Superfamily.SuperfamilyId);
            try
            {
                var dataset = _uow.Tbl42Superfamilies.GetById(currentTbl42Superfamily.SuperfamilyId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl42Superfamily.SuperfamilyName)) return;

                    _extCrud.DeleteSuperfamily(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl42Superfamily.SuperfamilyName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl42Superfamily.SuperfamilyName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteFamily(Tbl45Family currentTbl45Family)
        {
            Tbl48SubfamiliesList = _extCrud.GetConnectedDatasetsWithFamilyIdInTableSubfamily(currentTbl45Family.FamilyId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl48SubfamiliesList.Count, "Subfamily")) return;

            DeleteFamilyReferences(currentTbl45Family.FamilyId);

            DeleteFamilyComments(currentTbl45Family.FamilyId);
            try
            {
                var dataset = _uow.Tbl45Families.GetById(currentTbl45Family.FamilyId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl45Family.FamilyName)) return;

                    _extCrud.DeleteFamily(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl45Family.FamilyName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl45Family.FamilyName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteSubfamily(Tbl48Subfamily currentTbl48Subfamily)
        {
            Tbl51InfrafamiliesList = _extCrud.GetConnectedDatasetsWithSubfamilyIdInTableInfrafamily(currentTbl48Subfamily.SubfamilyId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl51InfrafamiliesList.Count, "Infrafamily")) return;

            DeleteSubfamilyReferences(currentTbl48Subfamily.SubfamilyId);

            DeleteSubfamilyComments(currentTbl48Subfamily.SubfamilyId);
            try
            {
                var dataset = _uow.Tbl48Subfamilies.GetById(currentTbl48Subfamily.SubfamilyId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl48Subfamily.SubfamilyName)) return;

                    _extCrud.DeleteSubfamily(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl48Subfamily.SubfamilyName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl48Subfamily.SubfamilyName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteInfrafamily(Tbl51Infrafamily currentTbl51Infrafamily)
        {
            Tbl54SupertribussesList = _extCrud.GetConnectedDatasetsWithInfrafamilyIdInTableSupertribus(currentTbl51Infrafamily.InfrafamilyId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl54SupertribussesList.Count, "Supertribus")) return;

            DeleteInfrafamilyReferences(currentTbl51Infrafamily.InfrafamilyId);

            DeleteInfrafamilyComments(currentTbl51Infrafamily.InfrafamilyId);
            try
            {
                var dataset = _uow.Tbl51Infrafamilies.GetById(currentTbl51Infrafamily.InfrafamilyId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl51Infrafamily.InfrafamilyName)) return;

                    _extCrud.DeleteInfrafamily(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl51Infrafamily.InfrafamilyName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl51Infrafamily.InfrafamilyName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteSupertribus(Tbl54Supertribus currentTbl54Supertribus)
        {
            Tbl57TribussesList = _extCrud.GetConnectedDatasetsWithSupertribusIdInTableTribus(currentTbl54Supertribus.SupertribusId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl57TribussesList.Count, "Tribus")) return;

            DeleteSupertribusReferences(currentTbl54Supertribus.SupertribusId);

            DeleteSupertribusComments(currentTbl54Supertribus.SupertribusId);
            try
            {
                var dataset = _uow.Tbl54Supertribusses.GetById(currentTbl54Supertribus.SupertribusId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl54Supertribus.SupertribusName)) return;

                    _extCrud.DeleteSupertribus(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl54Supertribus.SupertribusName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl54Supertribus.SupertribusName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteTribus(Tbl57Tribus currentTbl57Tribus)
        {
            Tbl60SubtribussesList = _extCrud.GetConnectedDatasetsWithTribusIdInTableSubtribus(currentTbl57Tribus.TribusId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl60SubtribussesList.Count, "Subtribus")) return;

            DeleteTribusReferences(currentTbl57Tribus.TribusId);

            DeleteTribusComments(currentTbl57Tribus.TribusId);
            try
            {
                var dataset = _uow.Tbl57Tribusses.GetById(currentTbl57Tribus.TribusId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl57Tribus.TribusName)) return;

                    _extCrud.DeleteTribus(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl57Tribus.TribusName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl57Tribus.TribusName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteSubtribus(Tbl60Subtribus currentTbl60Subtribus)
        {
            Tbl63InfratribussesList = _extCrud.GetConnectedDatasetsWithSubtribusIdInTableInfratribus(currentTbl60Subtribus.SubtribusId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl63InfratribussesList.Count, "Infratribus")) return;

            DeleteSubtribusReferences(currentTbl60Subtribus.SubtribusId);

            DeleteSubtribusComments(currentTbl60Subtribus.SubtribusId);
            try
            {
                var dataset = _uow.Tbl60Subtribusses.GetById(currentTbl60Subtribus.SubtribusId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl60Subtribus.SubtribusName)) return;

                    _extCrud.DeleteSubtribus(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl60Subtribus.SubtribusName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl60Subtribus.SubtribusName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteInfratribus(Tbl63Infratribus currentTbl63Infratribus)
        {
            Tbl66GenussesList = _extCrud.GetConnectedDatasetsWithInfratribusIdInTableGenus(currentTbl63Infratribus.InfratribusId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl66GenussesList.Count, "Genus")) return;

            DeleteInfratribusReferences(currentTbl63Infratribus.InfratribusId);

            DeleteInfratribusComments(currentTbl63Infratribus.InfratribusId);
            try
            {
                var dataset = _uow.Tbl63Infratribusses.GetById(currentTbl63Infratribus.InfratribusId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl63Infratribus.InfratribusName)) return;

                    _extCrud.DeleteInfratribus(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl63Infratribus.InfratribusName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl63Infratribus.InfratribusName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteGenus(Tbl66Genus currentTbl66Genus)
        {
            Tbl69FiSpeciessesList = _extCrud.GetConnectedDatasetsWithGenusIdInTableFiSpecies(currentTbl66Genus.GenusId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl69FiSpeciessesList.Count, "FiSpecies")) return;
            Tbl72PlSpeciessesList = _extCrud.GetConnectedDatasetsWithGenusIdInTablePlSpecies(currentTbl66Genus.GenusId);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl72PlSpeciessesList.Count, "PlSpecies")) return;

            DeleteGenusReferences(currentTbl66Genus.GenusId);

            DeleteGenusComments(currentTbl66Genus.GenusId);
            try
            {
                var dataset = _uow.Tbl66Genusses.GetById(currentTbl66Genus.GenusId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl66Genus.GenusName)) return;

                    _extCrud.DeleteGenus(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl66Genus.GenusName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl66Genus.GenusName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteSpeciesgroup(Tbl68Speciesgroup currentTbl68Speciesgroup)
        {
            try
            {
                var dataset = _uow.Tbl68Speciesgroups.GetById(currentTbl68Speciesgroup.SpeciesgroupId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl68Speciesgroup.SpeciesgroupName + " " + currentTbl68Speciesgroup.Subspeciesgroup)) return;

                    _extCrud.DeleteSpeciesgroup(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl68Speciesgroup.SpeciesgroupName + " " + currentTbl68Speciesgroup.Subspeciesgroup);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl68Speciesgroup.SpeciesgroupName + " " + currentTbl68Speciesgroup.Subspeciesgroup + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteFiSpecies(Tbl69FiSpecies currentTbl69FiSpecies)
        {

            DeleteFiSpeciesReferences(currentTbl69FiSpecies.FiSpeciesId);

            DeleteFiSpeciesComments(currentTbl69FiSpecies.FiSpeciesId);
            try
            {
                var dataset = _uow.Tbl69FiSpeciesses.GetById(currentTbl69FiSpecies.FiSpeciesId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl69FiSpecies.FiSpeciesName + " " + currentTbl69FiSpecies.Subspecies + " " + currentTbl69FiSpecies.Divers)) return;

                    _extCrud.DeleteFiSpecies(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl69FiSpecies.FiSpeciesName + " " + currentTbl69FiSpecies.Subspecies + " " + currentTbl69FiSpecies.Divers);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl69FiSpecies.FiSpeciesName + " " + currentTbl69FiSpecies.Subspecies + " " + currentTbl69FiSpecies.Divers + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeletePlSpecies(Tbl72PlSpecies currentTbl72PlSpecies)
        {

            DeletePlSpeciesReferences(currentTbl72PlSpecies.PlSpeciesId);

            DeletePlSpeciesComments(currentTbl72PlSpecies.PlSpeciesId);
            try
            {
                var dataset = _uow.Tbl72PlSpeciesses.GetById(currentTbl72PlSpecies.PlSpeciesId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl72PlSpecies.PlSpeciesName + " " + currentTbl72PlSpecies.Subspecies + " " + currentTbl72PlSpecies.Divers)) return;

                    _extCrud.DeletePlSpecies(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl72PlSpecies.PlSpeciesName + " " + currentTbl72PlSpecies.Subspecies + " " + currentTbl72PlSpecies.Divers);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl72PlSpecies.PlSpeciesName + " " + currentTbl72PlSpecies.Subspecies + " " + currentTbl72PlSpecies.Divers + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteName(Tbl78Name currentTbl78Name)
        {
            //Tbl69FiSpeciessesList = _extCrud.GetConnectedDatasetsWithGenusIdInTableFiSpecies(currentTbl66Genus.GenusId);
            //if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl69FiSpeciessesList.Count, "FiSpecies")) return;
            //Tbl72PlSpeciessesList = _extCrud.GetConnectedDatasetsWithGenusIdInTablePlSpecies(currentTbl66Genus.GenusId);
            //if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl72PlSpeciessesList.Count, "PlSpecies")) return;

            //DeleteNameReferences(currentTbl66Genus.GenusId);

            //DeleteGenusComments(currentTbl66Genus.GenusId);
            try
            {
                var dataset = _uow.Tbl78Names.GetById(currentTbl78Name.NameId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl78Name.NameName)) return;

                    _extCrud.DeleteName(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl78Name.NameName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl78Name.NameName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteImage(Tbl81Image currentTbl81Image)
        {
            //Tbl69FiSpeciessesList = _extCrud.GetConnectedDatasetsWithGenusIdInTableFiSpecies(currentTbl66Genus.GenusId);
            //if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl69FiSpeciessesList.Count, "FiSpecies")) return;
            //Tbl72PlSpeciessesList = _extCrud.GetConnectedDatasetsWithGenusIdInTablePlSpecies(currentTbl66Genus.GenusId);
            //if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl72PlSpeciessesList.Count, "PlSpecies")) return;

            //DeleteNameReferences(currentTbl66Genus.GenusId);

            //DeleteGenusComments(currentTbl66Genus.GenusId);
            try
            {
                var dataset = _uow.Tbl81Images.GetById(currentTbl81Image.ImageId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl81Image.Info)) return;

                    _extCrud.DeleteImage(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl81Image.Info);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl81Image.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteSynonym(Tbl84Synonym currentTbl84Synonym)
        {
            //Tbl69FiSpeciessesList = _extCrud.GetConnectedDatasetsWithGenusIdInTableFiSpecies(currentTbl66Genus.GenusId);
            //if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl69FiSpeciessesList.Count, "FiSpecies")) return;
            //Tbl72PlSpeciessesList = _extCrud.GetConnectedDatasetsWithGenusIdInTablePlSpecies(currentTbl66Genus.GenusId);
            //if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl72PlSpeciessesList.Count, "PlSpecies")) return;

            //DeleteNameReferences(currentTbl66Genus.GenusId);

            //DeleteGenusComments(currentTbl66Genus.GenusId);
            try
            {
                var dataset = _uow.Tbl84Synonyms.GetById(currentTbl84Synonym.SynonymId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl84Synonym.SynonymName)) return;

                    _extCrud.DeleteSynonym(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl84Synonym.SynonymName);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl84Synonym.SynonymName + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteGeographic(Tbl87Geographic currentTbl87Geographic)
        {
            //Tbl69FiSpeciessesList = _extCrud.GetConnectedDatasetsWithGenusIdInTableFiSpecies(currentTbl66Genus.GenusId);
            //if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl69FiSpeciessesList.Count, "FiSpecies")) return;
            //Tbl72PlSpeciessesList = _extCrud.GetConnectedDatasetsWithGenusIdInTablePlSpecies(currentTbl66Genus.GenusId);
            //if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(Tbl72PlSpeciessesList.Count, "PlSpecies")) return;

            //DeleteNameReferences(currentTbl66Genus.GenusId);

            //DeleteGenusComments(currentTbl66Genus.GenusId);
            try
            {
                var dataset = _uow.Tbl87Geographics.GetById(currentTbl87Geographic.GeographicId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl87Geographic.Info)) return;

                    _extCrud.DeleteGeographic(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl87Geographic.Info);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl87Geographic.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }

        public void DeleteReference(Tbl90Reference currentTbl90Reference)
        {
            try
            {
                var dataset = _uow.Tbl90References.GetById(currentTbl90Reference.ReferenceId);
                if (dataset != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl90Reference.Info)) return;

                    _extCrud.DeleteReference(dataset);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl90Reference.Info);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl90Reference.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }


        #endregion


        #region Delete References Expert, Source, Author from Regnum-Userprofile

        private void DeleteRegnumReferences(int regnumId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithRegnumIdInTableReference(regnumId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeletePhylumReferences(int phylumId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithPhylumIdInTableReference(phylumId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);

        }
        private void DeleteDivisionReferences(int divisionId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithDivisionIdInTableReference(divisionId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteSubphylumReferences(int subphylumId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithSubphylumIdInTableReference(subphylumId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteSubdivisionReferences(int subdivisionId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithSubdivisionIdInTableReference(subdivisionId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteSuperclassReferences(int superclassId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithSuperclassIdInTableReference(superclassId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteClassReferences(int classId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithClassIdInTableReference(classId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteSubclassReferences(int subclassId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithSubclassIdInTableReference(subclassId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteInfraclassReferences(int infraclassId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithSubclassIdInTableReference(infraclassId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteLegioReferences(int legioId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithLegioIdInTableReference(legioId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteOrdoReferences(int ordoId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithOrdoIdInTableReference(ordoId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteSubordoReferences(int subordoId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithSubordoIdInTableReference(subordoId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteInfraordoReferences(int infraordoId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithInfraordoIdInTableReference(infraordoId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteSuperfamilyReferences(int superfamilyId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithSuperfamilyIdInTableReference(superfamilyId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteFamilyReferences(int familyId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithFamilyIdInTableReference(familyId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteSubfamilyReferences(int subfamilyId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithSubfamilyIdInTableReference(subfamilyId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteInfrafamilyReferences(int infrafamilyId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithInfrafamilyIdInTableReference(infrafamilyId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteSupertribusReferences(int supertribusId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithSupertribusIdInTableReference(supertribusId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteTribusReferences(int tribusId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithTribusIdInTableReference(tribusId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteSubtribusReferences(int subtribusId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithSubtribusIdInTableReference(subtribusId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteInfratribusReferences(int infratribusId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithInfratribusIdInTableReference(infratribusId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteGenusReferences(int genusId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithGenusIdInTableReference(genusId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeleteFiSpeciesReferences(int fispeciesId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithFiSpeciesIdInTableReference(fispeciesId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }
        private void DeletePlSpeciesReferences(int plspeciesId)
        {
            Tbl90ReferencesList = _extCrud.DeleteDatasetsWithPlSpeciesIdInTableReference(plspeciesId);
            if (Tbl90ReferencesList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

            _extCrud.DeleteReferences(Tbl90ReferencesList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        }

        #endregion

        #region Delete Comments  from Regnum-Userprofile
        private void DeleteRegnumComments(int regnumId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithRegnumIdInTableComment(regnumId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeletePhylumComments(int phylumId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithPhylumIdInTableComment(phylumId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteDivisionComments(int divisionId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithDivisionIdInTableComment(divisionId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteSubphylumComments(int subphylumId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithSubphylumIdInTableComment(subphylumId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteSubdivisionComments(int subdivisionId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithSubdivisionIdInTableComment(subdivisionId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteSuperclassComments(int superclassId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithSuperclassIdInTableComment(superclassId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteClassComments(int classId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithClassIdInTableComment(classId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteSubclassComments(int subclassId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithSubclassIdInTableComment(subclassId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteInfraclassComments(int infraclassId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithInfraclassIdInTableComment(infraclassId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteLegioComments(int legioId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithLegioIdInTableComment(legioId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteOrdoComments(int ordoId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithOrdoIdInTableComment(ordoId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteSubordoComments(int subordoId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithSubordoIdInTableComment(subordoId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteInfraordoComments(int infraordoId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithInfraordoIdInTableComment(infraordoId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteSuperfamilyComments(int superfamilyId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithSuperfamilyIdInTableComment(superfamilyId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteFamilyComments(int familyId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithFamilyIdInTableComment(familyId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteSubfamilyComments(int subfamilyId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithSubfamilyIdInTableComment(subfamilyId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteInfrafamilyComments(int infrafamilyId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithInfrafamilyIdInTableComment(infrafamilyId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteSupertribusComments(int supertribusId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithSupertribusIdInTableComment(supertribusId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteTribusComments(int tribusId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithTribusIdInTableComment(tribusId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteSubtribusComments(int subtribusId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithSubtribusIdInTableComment(subtribusId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteInfratribusComments(int infratribusId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithInfratribusIdInTableComment(infratribusId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteGenusComments(int genusId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithGenusIdInTableComment(genusId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeleteFiSpeciesComments(int fispeciesId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithFiSpeciesIdInTableComment(fispeciesId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }
        private void DeletePlSpeciesComments(int plspeciesId)
        {
            Tbl93CommentsList = _extCrud.DeleteDatasetsWithPlSpeciesIdInTableComment(plspeciesId);
            if (Tbl93CommentsList.Count <= 0) return;
            if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

            _extCrud.DeleteComments(Tbl93CommentsList);

            _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
        }

        #endregion

        #region Delete Reference

        public void DeleteReferenceAuthor(Tbl90Reference currentTbl90ReferenceAuthor)
        {
            try
            {
                var reference = _uow.Tbl90References.GetById(currentTbl90ReferenceAuthor.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl90ReferenceAuthor.Info)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl90ReferenceAuthor.Info);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl90ReferenceAuthor.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void DeleteReferenceSource(Tbl90Reference currentTbl90ReferenceSource)
        {
            try
            {
                var reference = _uow.Tbl90References.GetById(currentTbl90ReferenceSource.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl90ReferenceSource.Info)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl90ReferenceSource.Info);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl90ReferenceSource.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

        }
        public void DeleteReferenceExpert(Tbl90Reference currentTbl90ReferenceExpert)
        {
            try
            {
                var reference = _uow.Tbl90References.GetById(currentTbl90ReferenceExpert.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl90ReferenceExpert.Info)) return;

                    _extCrud.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl90ReferenceExpert.Info);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl90ReferenceExpert.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        #endregion

        #region Delete Comment

        public void DeleteComment(Tbl93Comment currentTbl93Comment)
        {
            try
            {
                var comment = _uow.Tbl93Comments.GetById(currentTbl93Comment.CommentId);
                if (comment != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + currentTbl93Comment.Info)) return;

                    _extCrud.DeleteComment(comment);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, currentTbl93Comment.Info);
                }
                else _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteNot, CultRes.StringsRes.DeleteCan + " " + currentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }

        #endregion


        #region Properties

        #region "Public Properties Tbl03Regnum"

        private string _searchRegnumName = "";
        public string SearchRegnumName
        {
            get => _searchRegnumName;
            set { _searchRegnumName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView RegnumsView;
        private Tbl03Regnum CurrentTbl03Regnum => RegnumsView?.CurrentItem as Tbl03Regnum;

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        {
            get => _tbl03RegnumsList;
            set { _tbl03RegnumsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList;
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl06Phylum"

        public ICollectionView PhylumsView;
        private Tbl06Phylum CurrentTbl06Phylum => PhylumsView?.CurrentItem as Tbl06Phylum;

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
        {
            get => _tbl06PhylumsList;
            set { _tbl06PhylumsList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl09Division"

        public ICollectionView DivisionsView;
        private Tbl09Division CurrentTbl09Division => DivisionsView?.CurrentItem as Tbl09Division;

        private ObservableCollection<Tbl09Division> _tbl09DivisionsList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsList
        {
            get => _tbl09DivisionsList;
            set { _tbl09DivisionsList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"

        #region "Public Properties Tbl12Subphylum"

        public ICollectionView SubphylumsView;
        private Tbl12Subphylum CurrentTbl12Subphylum => SubphylumsView?.CurrentItem as Tbl12Subphylum;

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
        {
            get => _tbl12SubphylumsList;
            set { _tbl12SubphylumsList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl15Subdivision"

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
        {
            get => _tbl15SubdivisionsList;
            set { _tbl15SubdivisionsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl18Superclass"

        public ICollectionView SuperclassesView;
        private Tbl18Superclass CurrentTbl18Superclass => SuperclassesView?.CurrentItem as Tbl18Superclass;

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
        {
            get => _tbl18SuperclassesList;
            set { _tbl18SuperclassesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"

        #region "Public Properties Tbl21Class"

        public ICollectionView ClassesView;
        private Tbl21Class CurrentTbl21Class => ClassesView?.CurrentItem as Tbl21Class;

        private ObservableCollection<Tbl21Class> _tbl21ClassesList;
        public ObservableCollection<Tbl21Class> Tbl21ClassesList
        {
            get => _tbl21ClassesList;
            set { _tbl21ClassesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl24Subclass"

        public ICollectionView SubclassesView;
        private Tbl24Subclass CurrentTbl24Subclass => SubclassesView?.CurrentItem as Tbl24Subclass;

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesList
        {
            get => _tbl24SubclassesList;
            set { _tbl24SubclassesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl27Infraclass"

        public ICollectionView InfraclassesView;
        private Tbl27Infraclass CurrentTbl27Infraclass => InfraclassesView?.CurrentItem as Tbl27Infraclass;

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesList
        {
            get => _tbl27InfraclassesList;
            set { _tbl27InfraclassesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl30Legio"

        public ICollectionView LegiosView;
        private Tbl30Legio CurrentTbl30Legio => LegiosView?.CurrentItem as Tbl30Legio;

        private ObservableCollection<Tbl30Legio> _tbl30LegiosList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosList
        {
            get => _tbl30LegiosList;
            set { _tbl30LegiosList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl33Ordo"

        public ICollectionView OrdosView;
        private Tbl33Ordo CurrentTbl33Ordo => OrdosView?.CurrentItem as Tbl33Ordo;

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosList
        {
            get => _tbl33OrdosList;
            set { _tbl33OrdosList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl36Subordo"

        public ICollectionView SubordosView;
        private Tbl36Subordo CurrentTbl36Subordo => SubordosView?.CurrentItem as Tbl36Subordo;

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosList
        {
            get => _tbl36SubordosList;
            set { _tbl36SubordosList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl39Infraordo"

        public ICollectionView InfraordosView;
        private Tbl39Infraordo CurrentTbl39Infraordo => InfraordosView?.CurrentItem as Tbl39Infraordo;

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosList
        {
            get => _tbl39InfraordosList;
            set { _tbl39InfraordosList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl42Superfamily"

        public ICollectionView SuperfamiliesView;
        private Tbl42Superfamily CurrentTbl42Superfamily => SuperfamiliesView?.CurrentItem as Tbl42Superfamily;

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesList;
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesList
        {
            get => _tbl42SuperfamiliesList;
            set { _tbl42SuperfamiliesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl45Family"

        public ICollectionView FamiliesView;
        private Tbl45Family CurrentTbl45Family => FamiliesView?.CurrentItem as Tbl45Family;

        private ObservableCollection<Tbl45Family> _tbl45FamiliesList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesList
        {
            get => _tbl45FamiliesList;
            set { _tbl45FamiliesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl48Subfamily"

        public ICollectionView SubfamiliesView;
        private Tbl48Subfamily CurrentTbl48Subfamily => SubfamiliesView?.CurrentItem as Tbl48Subfamily;

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesList;
        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesList
        {
            get => _tbl48SubfamiliesList;
            set { _tbl48SubfamiliesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl51Infrafamily"

        public ICollectionView InfrafamiliesView;
        private Tbl51Infrafamily CurrentTbl51Infrafamily => InfrafamiliesView?.CurrentItem as Tbl51Infrafamily;

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesList
        {
            get => _tbl51InfrafamiliesList;
            set { _tbl51InfrafamiliesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl54Supertribus"

        public ICollectionView SupertribussesView;
        private Tbl54Supertribus CurrentTbl54Supertribus => SupertribussesView?.CurrentItem as Tbl54Supertribus;

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesList;
        public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesList
        {
            get => _tbl54SupertribussesList;
            set { _tbl54SupertribussesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl57Tribus"

        public ICollectionView TribussesView;
        private Tbl57Tribus CurrentTbl57Tribus => TribussesView?.CurrentItem as Tbl57Tribus;

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesList
        {
            get => _tbl57TribussesList;
            set { _tbl57TribussesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl60Subtribus"

        public ICollectionView SubtribussesView;
        private Tbl60Subtribus CurrentTbl60Subtribus => SubtribussesView?.CurrentItem as Tbl60Subtribus;

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesList;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesList
        {
            get => _tbl60SubtribussesList;
            set { _tbl60SubtribussesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl63Infratribus"

        public ICollectionView InfratribussesView;
        private Tbl63Infratribus CurrentTbl63Infratribus => InfratribussesView?.CurrentItem as Tbl63Infratribus;

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesList;
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesList
        {
            get => _tbl63InfratribussesList;
            set { _tbl63InfratribussesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl66Genus"

        public ICollectionView GenussesView;
        private Tbl66Genus CurrentTbl66Genus => GenussesView?.CurrentItem as Tbl66Genus;

        private ObservableCollection<Tbl66Genus> _tbl66GenussesList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesList
        {
            get => _tbl66GenussesList;
            set { _tbl66GenussesList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl68Speciesgroup"

        public ICollectionView SpeciesgroupsView;
        private Tbl68Speciesgroup CurrentTbl68Speciesgroup => SpeciesgroupsView?.CurrentItem as Tbl68Speciesgroup;

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
        {
            get => _tbl68SpeciesgroupsList;
            set { _tbl68SpeciesgroupsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList;
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(""); }
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
        #endregion "Public Properties"     

        #region "Public Properties Tbl72PlSpecies"

        private string _searchPlSpeciesName = string.Empty;
        public string SearchPlSpeciesName
        {
            get => _searchPlSpeciesName;
            set { _searchPlSpeciesName = value; RaisePropertyChanged(""); }
        }

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


        #region Public Properties Tbl90References

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesList;

        public ObservableCollection<Tbl90Reference> Tbl90ReferencesList
        {
            get => _tbl90ReferencesList;
            set { _tbl90ReferencesList = value; RaisePropertyChanged(""); }
        }

        #endregion

        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList;
            set { _tbl93CommentsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"     

        #endregion

    }
}
