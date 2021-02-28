using System;
using ATIS.Ui.Models;
using ATIS.Ui.Helper;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Core
{
    public class SaveFunctions : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        //private readonly AtisDbContext _context = new AtisDbContext();
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();

        public bool SaveRegnum(Tbl03Regnum currentTbl03Regnum)
        {
            var returnBool = false;

            try
            {
                //  var dataset = _extCrud.GetRegnumSingleByRegnumId<Tbl03Regnum>(currentTbl03Regnum.RegnumId);
                var dataset = _uow.Tbl03Regnums.GetById(currentTbl03Regnum.RegnumId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl03Regnum.RegnumName))
                    return false;

                if (currentTbl03Regnum.RegnumId == 0)
                    dataset = _extCrud.RegnumAdd(currentTbl03Regnum);
                else
                    dataset = _extCrud.RegnumUpdate(dataset, currentTbl03Regnum);
                try
                {
                    _extCrud.RegnumSave(dataset, currentTbl03Regnum);
                    returnBool = true;

                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl03Regnum.RegnumId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl03Regnum.RegnumName + " " + currentTbl03Regnum.Subregnum);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);

            }
            return returnBool;
        }
        public bool SavePhylum(Tbl06Phylum currentTbl06Phylum)
        {
            var returnBool = false;

            //Combobox select RegnumId  may not be 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl06Phylum.RegnumId)) return false;
            try
            {
                var dataset = _uow.Tbl06Phylums.GetById(currentTbl06Phylum.PhylumId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl06Phylum.PhylumName)) return false;

                if (currentTbl06Phylum.PhylumId == 0)
                    dataset = _extCrud.PhylumAdd(currentTbl06Phylum);
                else
                    dataset = _extCrud.PhylumUpdate(dataset, currentTbl06Phylum);

                try
                {
                    _extCrud.PhylumSave(dataset, currentTbl06Phylum);
                    returnBool = true;
                }

                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl06Phylum.PhylumId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl06Phylum.PhylumName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveDivision(Tbl09Division currentTbl09Division)
        {
            var returnBool = false;
            //Combobox select RegnumId  may not be 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl09Division.RegnumId)) return false;
            try
            {
                var dataset = _uow.Tbl09Divisions.GetById(currentTbl09Division.DivisionId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl09Division.DivisionName)) return false;

                if (currentTbl09Division.DivisionId == 0)
                    dataset = _extCrud.DivisionAdd(currentTbl09Division);
                else
                    dataset = _extCrud.DivisionUpdate(dataset, currentTbl09Division);

                try
                {
                    _extCrud.DivisionSave(dataset, currentTbl09Division);
                    returnBool = true;
                }

                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl09Division.DivisionId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl09Division.DivisionName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }

            return returnBool;
        }
        public bool SaveSubphylum(Tbl12Subphylum currentTbl12Subphylum)
        {
            var returnBool = false;

            //Combobox select PhylumId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl12Subphylum.PhylumId)) return false;

            try
            {
                var dataset = _uow.Tbl12Subphylums.GetById(currentTbl12Subphylum.SubphylumId);


                if (currentTbl12Subphylum.SubphylumId == 0)
                    dataset = _extCrud.SubphylumAdd(currentTbl12Subphylum);
                else
                    dataset = _extCrud.SubphylumUpdate(dataset, currentTbl12Subphylum);


                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl12Subphylum.SubphylumName)) return false;

                try
                {
                    _extCrud.SubphylumSave(dataset, currentTbl12Subphylum);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl12Subphylum.SubphylumId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl12Subphylum.SubphylumName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveSubdivision(Tbl15Subdivision currentTbl15Subdivision)
        {
            var returnBool = false;
            //Combobox select DivisionID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl15Subdivision.DivisionId)) return false;

            try
            {
                var dataset = _uow.Tbl15Subdivisions.GetById(currentTbl15Subdivision.SubdivisionId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl15Subdivision.SubdivisionName)) return false;

                if (currentTbl15Subdivision.SubdivisionId == 0)
                    dataset = _extCrud.SubdivisionAdd(currentTbl15Subdivision);
                else
                    dataset = _extCrud.SubdivisionUpdate(dataset, currentTbl15Subdivision);

                try
                {
                    _extCrud.SubdivisionSave(dataset, currentTbl15Subdivision);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl15Subdivision.SubdivisionId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl15Subdivision.SubdivisionName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveSuperclass(Tbl18Superclass currentTbl18Superclass)
        {
            var returnBool = false;
            //Combobox select SubphylumID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl18Superclass.SubphylumId)) return false;

            try
            {
                var dataset = _uow.Tbl18Superclasses.GetById(currentTbl18Superclass.SuperclassId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl18Superclass.SuperclassName)) return false;

                if (currentTbl18Superclass.SuperclassId == 0)
                    dataset = _extCrud.SuperclassAdd(currentTbl18Superclass);
                else
                    dataset = _extCrud.SuperclassUpdate(dataset, currentTbl18Superclass);

                try
                {
                    _extCrud.SuperclassSave(dataset, currentTbl18Superclass);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl18Superclass.SuperclassId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl18Superclass.SuperclassName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveClass(Tbl21Class currentTbl21Class)
        {
            var returnBool = false;
            //Combobox select SuperclassID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl21Class.SuperclassId)) return false;

            try
            {
                var dataset = _uow.Tbl21Classes.GetById(currentTbl21Class.ClassId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl21Class.ClassName)) return false;

                if (currentTbl21Class.ClassId == 0)
                    dataset = _extCrud.ClassAdd(currentTbl21Class);
                else
                    dataset = _extCrud.ClassUpdate(dataset, currentTbl21Class);

                try
                {
                    _extCrud.ClassSave(dataset, currentTbl21Class);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl21Class.ClassId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl21Class.ClassName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveSubclass(Tbl24Subclass currentTbl24Subclass)
        {
            var returnBool = false;
            //Combobox select ClassID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl24Subclass.ClassId)) return false;

            try
            {
                var dataset = _uow.Tbl24Subclasses.GetById(currentTbl24Subclass.SubclassId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl24Subclass.SubclassName)) return false;

                if (currentTbl24Subclass.SubclassId == 0)
                    dataset = _extCrud.SubclassAdd(currentTbl24Subclass);
                else
                    dataset = _extCrud.SubclassUpdate(dataset, currentTbl24Subclass);

                try
                {
                    _extCrud.SubclassSave(dataset, currentTbl24Subclass);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl24Subclass.SubclassId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl24Subclass.SubclassName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveInfraclass(Tbl27Infraclass currentTbl27Infraclass)
        {
            var returnBool = false;
            //Combobox select SubclassID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl27Infraclass.SubclassId)) return false;

            try
            {
                var dataset = _uow.Tbl27Infraclasses.GetById(currentTbl27Infraclass.InfraclassId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl27Infraclass.InfraclassName)) return false;

                if (currentTbl27Infraclass.InfraclassId == 0)
                    dataset = _extCrud.InfraclassAdd(currentTbl27Infraclass);
                else
                    dataset = _extCrud.InfraclassUpdate(dataset, currentTbl27Infraclass);

                try
                {
                    _extCrud.InfraclassSave(dataset, currentTbl27Infraclass);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl27Infraclass.InfraclassId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl27Infraclass.InfraclassName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveLegio(Tbl30Legio currentTbl30Legio)
        {
            var returnBool = false;
            //Combobox select InfraclassID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl30Legio.InfraclassId)) return false;

            try
            {
                var dataset = _uow.Tbl30Legios.GetById(currentTbl30Legio.LegioId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl30Legio.LegioName)) return false;

                if (currentTbl30Legio.LegioId == 0)
                    dataset = _extCrud.LegioAdd(currentTbl30Legio);
                else
                    dataset = _extCrud.LegioUpdate(dataset, currentTbl30Legio);

                try
                {
                    _extCrud.LegioSave(dataset, currentTbl30Legio);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl30Legio.LegioId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl30Legio.LegioName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveOrdo(Tbl33Ordo currentTbl33Ordo)
        {
            var returnBool = false;
            //Combobox select LegioID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl33Ordo.LegioId)) return false;

            try
            {
                var dataset = _uow.Tbl33Ordos.GetById(currentTbl33Ordo.OrdoId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl33Ordo.OrdoName)) return false;

                if (currentTbl33Ordo.OrdoId == 0)
                    dataset = _extCrud.OrdoAdd(currentTbl33Ordo);
                else
                    dataset = _extCrud.OrdoUpdate(dataset, currentTbl33Ordo);

                try
                {
                    _extCrud.OrdoSave(dataset, currentTbl33Ordo);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl33Ordo.OrdoId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl33Ordo.OrdoName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveSubordo(Tbl36Subordo currentTbl36Subordo)
        {
            var returnBool = false;
            //Combobox select OrdoID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl36Subordo.OrdoId)) return false;

            try
            {
                var dataset = _uow.Tbl36Subordos.GetById(currentTbl36Subordo.SubordoId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl36Subordo.SubordoName)) return false;

                if (currentTbl36Subordo.SubordoId == 0)
                    dataset = _extCrud.SubordoAdd(currentTbl36Subordo);
                else
                    dataset = _extCrud.SubordoUpdate(dataset, currentTbl36Subordo);

                try
                {
                    _extCrud.SubordoSave(dataset, currentTbl36Subordo);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl36Subordo.SubordoId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl36Subordo.SubordoName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveInfraordo(Tbl39Infraordo currentTbl39Infraordo)
        {
            var returnBool = false;
            //Combobox select SubordoID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl39Infraordo.SubordoId)) return false;

            try
            {
                var dataset = _uow.Tbl39Infraordos.GetById(currentTbl39Infraordo.InfraordoId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl39Infraordo.InfraordoName)) return false;

                if (currentTbl39Infraordo.InfraordoId == 0)
                    dataset = _extCrud.InfraordoAdd(currentTbl39Infraordo);
                else
                    dataset = _extCrud.InfraordoUpdate(dataset, currentTbl39Infraordo);

                try
                {
                    _extCrud.InfraordoSave(dataset, currentTbl39Infraordo);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl39Infraordo.InfraordoId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl39Infraordo.InfraordoName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveSuperfamily(Tbl42Superfamily currentTbl42Superfamily)
        {
            var returnBool = false;
            //Combobox select InfraordoID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl42Superfamily.InfraordoId)) return false;

            try
            {
                var dataset = _uow.Tbl42Superfamilies.GetById(currentTbl42Superfamily.SuperfamilyId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl42Superfamily.SuperfamilyName)) return false;

                if (currentTbl42Superfamily.SuperfamilyId == 0)
                    dataset = _extCrud.SuperfamilyAdd(currentTbl42Superfamily);
                else
                    dataset = _extCrud.SuperfamilyUpdate(dataset, currentTbl42Superfamily);

                try
                {
                    _extCrud.SuperfamilySave(dataset, currentTbl42Superfamily);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl42Superfamily.SuperfamilyId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl42Superfamily.SuperfamilyName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveFamily(Tbl45Family currentTbl45Family)
        {
            var returnBool = false;
            //Combobox select SuperfamilyID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl45Family.SuperfamilyId)) return false;

            try
            {
                var dataset = _uow.Tbl45Families.GetById(currentTbl45Family.FamilyId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl45Family.FamilyName)) return false;

                if (currentTbl45Family.FamilyId == 0)
                    dataset = _extCrud.FamilyAdd(currentTbl45Family);
                else
                    dataset = _extCrud.FamilyUpdate(dataset, currentTbl45Family);

                try
                {
                    _extCrud.FamilySave(dataset, currentTbl45Family);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl45Family.FamilyId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl45Family.FamilyName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveSubfamily(Tbl48Subfamily currentTbl48Subfamily)
        {
            var returnBool = false;
            //Combobox select FamilyID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl48Subfamily.FamilyId)) return false;

            try
            {
                var dataset = _uow.Tbl48Subfamilies.GetById(currentTbl48Subfamily.SubfamilyId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl48Subfamily.SubfamilyName)) return false;

                if (currentTbl48Subfamily.SubfamilyId == 0)
                    dataset = _extCrud.SubfamilyAdd(currentTbl48Subfamily);
                else
                    dataset = _extCrud.SubfamilyUpdate(dataset, currentTbl48Subfamily);

                try
                {
                    _extCrud.SubfamilySave(dataset, currentTbl48Subfamily);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl48Subfamily.SubfamilyId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl48Subfamily.SubfamilyName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveInfrafamily(Tbl51Infrafamily currentTbl51Infrafamily)
        {
            var returnBool = false;
            //Combobox select SubfamilyID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl51Infrafamily.SubfamilyId)) return false;

            try
            {
                var dataset = _uow.Tbl51Infrafamilies.GetById(currentTbl51Infrafamily.InfrafamilyId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl51Infrafamily.InfrafamilyName)) return false;

                if (currentTbl51Infrafamily.InfrafamilyId == 0)
                    dataset = _extCrud.InfrafamilyAdd(currentTbl51Infrafamily);
                else
                    dataset = _extCrud.InfrafamilyUpdate(dataset, currentTbl51Infrafamily);

                try
                {
                    _extCrud.InfrafamilySave(dataset, currentTbl51Infrafamily);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl51Infrafamily.InfrafamilyId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl51Infrafamily.InfrafamilyName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveSupertribus(Tbl54Supertribus currentTbl54Supertribus)
        {
            var returnBool = false;
            //Combobox select InfrafamilyID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl54Supertribus.InfrafamilyId)) return false;

            try
            {
                var dataset = _uow.Tbl54Supertribusses.GetById(currentTbl54Supertribus.SupertribusId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl54Supertribus.SupertribusName)) return false;

                if (currentTbl54Supertribus.SupertribusId == 0)
                    dataset = _extCrud.SupertribusAdd(currentTbl54Supertribus);
                else
                    dataset = _extCrud.SupertribusUpdate(dataset, currentTbl54Supertribus);

                try
                {
                    _extCrud.SupertribusSave(dataset, currentTbl54Supertribus);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl54Supertribus.SupertribusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl54Supertribus.SupertribusName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveTribus(Tbl57Tribus currentTbl57Tribus)
        {
            var returnBool = false;
            //Combobox select SupertribusID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl57Tribus.SupertribusId)) return false;

            try
            {
                var dataset = _uow.Tbl57Tribusses.GetById(currentTbl57Tribus.TribusId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl57Tribus.TribusName)) return false;

                if (currentTbl57Tribus.TribusId == 0)
                    dataset = _extCrud.TribusAdd(currentTbl57Tribus);
                else
                    dataset = _extCrud.TribusUpdate(dataset, currentTbl57Tribus);

                try
                {
                    _extCrud.TribusSave(dataset, currentTbl57Tribus);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl57Tribus.TribusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl57Tribus.TribusName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveSubtribus(Tbl60Subtribus currentTbl60Subtribus)
        {
            var returnBool = false;
            //Combobox select TribusID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl60Subtribus.TribusId)) return false;

            try
            {
                var dataset = _uow.Tbl60Subtribusses.GetById(currentTbl60Subtribus.SubtribusId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl60Subtribus.SubtribusName)) return false;

                if (currentTbl60Subtribus.SubtribusId == 0)
                    dataset = _extCrud.SubtribusAdd(currentTbl60Subtribus);
                else
                    dataset = _extCrud.SubtribusUpdate(dataset, currentTbl60Subtribus);

                try
                {
                    _extCrud.SubtribusSave(dataset, currentTbl60Subtribus);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl60Subtribus.SubtribusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl60Subtribus.SubtribusName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveInfratribus(Tbl63Infratribus currentTbl63Infratribus)
        {
            var returnBool = false;
            //Combobox select SubtribusID  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl63Infratribus.SubtribusId)) return false;

            try
            {
                var dataset = _uow.Tbl63Infratribusses.GetById(currentTbl63Infratribus.InfratribusId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl63Infratribus.InfratribusName)) return false;

                if (currentTbl63Infratribus.InfratribusId == 0)
                    dataset = _extCrud.InfratribusAdd(currentTbl63Infratribus);
                else
                    dataset = _extCrud.InfratribusUpdate(dataset, currentTbl63Infratribus);

                try
                {
                    _extCrud.InfratribusSave(dataset, currentTbl63Infratribus);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl63Infratribus.InfratribusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl63Infratribus.InfratribusName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveGenus(Tbl66Genus currentTbl66Genus)
        {
            var returnBool = false;

            //Combobox select InfratribusId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl66Genus.InfratribusId)) return false;

            try
            {
                var dataset = _uow.Tbl66Genusses.GetById(currentTbl66Genus.GenusId);
                //   var dataset = _extCrud.GetGenusSingleByGenusId<Tbl66Genus>(currentTbl66Genus.GenusId);

                if (currentTbl66Genus.GenusId == 0)
                    dataset = _extCrud.GenusAdd(currentTbl66Genus);
                else
                    dataset = _extCrud.GenusUpdate(dataset, currentTbl66Genus);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl66Genus.GenusName)) return false;

                try
                {
                    _extCrud.GenusSave(dataset, currentTbl66Genus);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, e.Message);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl66Genus.GenusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl66Genus.GenusName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveSpeciesgroup(Tbl68Speciesgroup currentTbl68Speciesgroup)
        {
            var returnBool = false;

            try
            {
                var dataset = _uow.Tbl68Speciesgroups.GetById(currentTbl68Speciesgroup.SpeciesgroupId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl68Speciesgroup.SpeciesgroupName)) return false;

                if (currentTbl68Speciesgroup.SpeciesgroupId == 0)
                    dataset = _extCrud.SpeciesgroupAdd(currentTbl68Speciesgroup);
                else
                    dataset = _extCrud.SpeciesgroupUpdate(dataset, currentTbl68Speciesgroup);

                try
                {
                    _extCrud.SpeciesgroupSave(dataset, currentTbl68Speciesgroup);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl68Speciesgroup.SpeciesgroupId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl68Speciesgroup.SpeciesgroupName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveFiSpecies(Tbl69FiSpecies currentTbl69FiSpecies)
        {
            var returnBool = false;

            //Combobox select GenusId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl69FiSpecies.GenusId)) return false;

            try
            {
                var dataset = _uow.Tbl69FiSpeciesses.GetById(currentTbl69FiSpecies.FiSpeciesId);
                //    var dataset = _extCrud.GetFiSpeciesSingleByFiSpeciesId<Tbl69FiSpecies>(currentTbl69FiSpecies.FiSpeciesId);

                if (currentTbl69FiSpecies.FiSpeciesId == 0)
                    dataset = _extCrud.FiSpeciesAdd(currentTbl69FiSpecies);
                else
                    dataset = _extCrud.FiSpeciesUpdate(dataset, currentTbl69FiSpecies);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl69FiSpecies.FiSpeciesName + " " + currentTbl69FiSpecies.Subspecies + " " + currentTbl69FiSpecies.Divers)) return false;

                try
                {
                    _extCrud.FiSpeciesSave(dataset, currentTbl69FiSpecies);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl69FiSpecies.FiSpeciesId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl69FiSpecies.FiSpeciesName + " " + currentTbl69FiSpecies.Subspecies + " " + currentTbl69FiSpecies.Divers);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SavePlSpecies(Tbl72PlSpecies currentTbl72PlSpecies)
        {
            var returnBool = false;

            //Combobox select GenusId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl72PlSpecies.GenusId)) return false;

            try
            {
                var dataset = _uow.Tbl72PlSpeciesses.GetById(currentTbl72PlSpecies.PlSpeciesId);

                if (currentTbl72PlSpecies.PlSpeciesId == 0)
                    dataset = _extCrud.PlSpeciesAdd(currentTbl72PlSpecies);
                else
                    dataset = _extCrud.PlSpeciesUpdate(dataset, currentTbl72PlSpecies);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl72PlSpecies.PlSpeciesName + " " + currentTbl72PlSpecies.Subspecies + " " + currentTbl72PlSpecies.Divers)) return false;

                try
                {
                    _extCrud.PlSpeciesSave(dataset, currentTbl72PlSpecies);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl72PlSpecies.PlSpeciesId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl72PlSpecies.PlSpeciesName + " " + currentTbl72PlSpecies.Subspecies + " " + currentTbl72PlSpecies.Divers);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }

            return returnBool;
        }
        public bool SaveName(Tbl78Name currentTbl78Name)
        {
            var returnBool = false;

            //Combobox select FiSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl78Name.FiSpeciesId)) return false;
            //Combobox select PlSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl78Name.PlSpeciesId)) return false;

            try
            {
                var dataset = _uow.Tbl78Names.GetById(currentTbl78Name.NameId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl78Name.NameName)) return false;

                if (currentTbl78Name.NameId == 0)
                    dataset = _extCrud.NameAdd(currentTbl78Name);
                else
                    dataset = _extCrud.NameUpdate(dataset, currentTbl78Name);

                try
                {
                    _extCrud.NameSave(dataset, currentTbl78Name);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl78Name.NameId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl78Name.NameName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveImage(Tbl81Image currentTbl81Image)
        {
            var returnBool = false;

            //Combobox select FiSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl81Image.FiSpeciesId)) return false;
            //Combobox select PlSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl81Image.PlSpeciesId)) return false;

            try
            {
                var dataset = _uow.Tbl81Images.GetById(currentTbl81Image.ImageId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl81Image.Info)) return false;

                if (currentTbl81Image.ImageId == 0)
                    dataset = _extCrud.ImageAdd(currentTbl81Image);
                else
                    dataset = _extCrud.ImageUpdate(dataset, currentTbl81Image);

                try
                {
                    _extCrud.ImageSave(dataset, currentTbl81Image);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl81Image.ImageId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl81Image.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveSynonym(Tbl84Synonym currentTbl84Synonym)
        {
            var returnBool = false;

            //Combobox select FiSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl84Synonym.FiSpeciesId)) return false;
            //Combobox select PlSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl84Synonym.PlSpeciesId)) return false;

            try
            {
                var dataset = _uow.Tbl84Synonyms.GetById(currentTbl84Synonym.SynonymId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl84Synonym.SynonymName)) return false;

                if (currentTbl84Synonym.SynonymId == 0)
                    dataset = _extCrud.SynonymAdd(currentTbl84Synonym);
                else
                    dataset = _extCrud.SynonymUpdate(dataset, currentTbl84Synonym);

                try
                {
                    _extCrud.SynonymSave(dataset, currentTbl84Synonym);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl84Synonym.SynonymId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl84Synonym.SynonymName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        public bool SaveGeographic(Tbl87Geographic currentTbl87Geographic)
        {
            var returnBool = false;

            //Combobox select FiSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl87Geographic.FiSpeciesId)) return false;
            //Combobox select PlSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl87Geographic.PlSpeciesId)) return false;

            try
            {
                var dataset = _uow.Tbl87Geographics.GetById(currentTbl87Geographic.GeographicId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl87Geographic.Info)) return false;

                if (currentTbl87Geographic.GeographicId == 0)
                    dataset = _extCrud.GeographicAdd(currentTbl87Geographic);
                else
                    dataset = _extCrud.GeographicUpdate(dataset, currentTbl87Geographic);

                try
                {
                    _extCrud.GeographicSave(dataset, currentTbl87Geographic);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl87Geographic.GeographicId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl87Geographic.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }

        public bool SaveReference(Tbl90Reference currentTbl90Reference)
        {
            var returnBool = false;
            // Regnum-PlSpecies
            var z = 0;
            if (currentTbl90Reference.RegnumId != null) z += 1;
            if (currentTbl90Reference.PhylumId != null) z += 1;
            if (currentTbl90Reference.SubphylumId != null) z += 1;
            if (currentTbl90Reference.DivisionId != null) z += 1;
            if (currentTbl90Reference.SubdivisionId != null) z += 1;
            if (currentTbl90Reference.SuperclassId != null) z += 1;
            if (currentTbl90Reference.ClassId != null) z += 1;
            if (currentTbl90Reference.SubclassId != null) z += 1;
            if (currentTbl90Reference.InfraclassId != null) z += 1;
            if (currentTbl90Reference.LegioId != null) z += 1;
            if (currentTbl90Reference.OrdoId != null) z += 1;
            if (currentTbl90Reference.SubordoId != null) z += 1;
            if (currentTbl90Reference.InfraordoId != null) z += 1;
            if (currentTbl90Reference.SuperfamilyId != null) z += 1;
            if (currentTbl90Reference.FamilyId != null) z += 1;
            if (currentTbl90Reference.SubfamilyId != null) z += 1;
            if (currentTbl90Reference.InfrafamilyId != null) z += 1;
            if (currentTbl90Reference.SupertribusId != null) z += 1;
            if (currentTbl90Reference.TribusId != null) z += 1;
            if (currentTbl90Reference.SubtribusId != null) z += 1;
            if (currentTbl90Reference.InfratribusId != null) z += 1;
            if (currentTbl90Reference.GenusId != null) z += 1;
            if (currentTbl90Reference.FiSpeciesId != null) z += 1;
            if (currentTbl90Reference.PlSpeciesId != null) z += 1;

            if (z == 0)
            {
                _allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(z);
                return false;
            }

            if (z > 1)
            {
                _allMessageBoxes.IdSelectInComboBoxMoreThenOneInfoMessageBox();
                return false;
            }

            // RefExpert-RefAuthor extra
            z = 0;
            if (currentTbl90Reference.RefExpertId != null) z += 1;
            if (currentTbl90Reference.RefSourceId != null) z += 1;
            if (currentTbl90Reference.RefAuthorId != null) z += 1;

            if (z == 0)
            {
                _allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(z);
                return false;
            }

            if (z > 1)
            {
                _allMessageBoxes.IdSelectInComboBoxMoreThenOneInfoMessageBox();
                return false;
            }

            try
            {
                var dataset = _uow.Tbl90References.GetById(currentTbl90Reference.ReferenceId);
                //    var dataset = _extCrud.GetClassSingleByReferenceId<Tbl90Reference>(currentTbl90Reference.ReferenceId);

                if (currentTbl90Reference.ReferenceId == 0)
                    dataset = _extCrud.ReferenceAdd(currentTbl90Reference);
                else
                    dataset = _extCrud.ReferenceUpdate(dataset, currentTbl90Reference);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl90Reference.Info)) return false;

                try
                {
                    _extCrud.ReferenceSave(dataset, currentTbl90Reference);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl90Reference.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl90Reference.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }

        public bool SaveReferenceAuthor(Tbl90Reference currentTbl90ReferenceAuthor, string name)
        {
            var returnBool = false;

            //Combobox select RefAuthorId may be not null
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl90ReferenceAuthor.RefAuthorId)) return false;

            try
            {
                var dataset = _uow.Tbl90References.GetById(currentTbl90ReferenceAuthor.ReferenceId);
                // var dataset = _extCrud.GetReferenceSingleByReferenceId<Tbl90Reference>(currentTbl90ReferenceAuthor.ReferenceId);


                dataset = AddUpdateReferenceAuthor(currentTbl90ReferenceAuthor, dataset, name);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl90ReferenceAuthor.Info)) return false;

                try
                {
                    _extCrud.ReferenceAuthorSave(dataset, currentTbl90ReferenceAuthor);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl90ReferenceAuthor.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl90ReferenceAuthor.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }

            return returnBool;
        }
        private Tbl90Reference AddUpdateReferenceAuthor(Tbl90Reference currentTbl90ReferenceAuthor, Tbl90Reference dataset, string name)
        {
            switch (name)
            {
                case "Regnum":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorRegnumAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorRegnumUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Phylum":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorPhylumAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorPhylumUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Division":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorDivisionAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorDivisionUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Subphylum":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorSubphylumAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorSubphylumUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Subdivision":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorSubdivisionAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorSubdivisionUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Superclass":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorSuperclassAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorSuperclassUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Class":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorClassAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorClassUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Subclass":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorSubclassAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorSubclassUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Infraclass":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorInfraclassAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorInfraclassUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Legio":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorLegioAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorLegioUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Ordo":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorOrdoAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorOrdoUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Subordo":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorSubordoAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorSubordoUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Infraordo":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorInfraordoAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorInfraordoUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Superfamily":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorSuperfamilyAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorSuperfamilyUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Family":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorFamilyAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorFamilyUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Subfamily":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorSubfamilyAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorSubfamilyUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Infrafamily":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorInfrafamilyAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorInfrafamilyUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Supertribus":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorSupertribusAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorSupertribusUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Tribus":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorTribusAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorTribusUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Subtribus":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorSubtribusAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorSubtribusUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Infratribus":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorInfratribusAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorInfratribusUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "Genus":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorGenusAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorGenusUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "FiSpecies":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorFiSpeciesAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorFiSpeciesUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }
                case "PlSpecies":
                    {
                        if (currentTbl90ReferenceAuthor.ReferenceId == 0)
                            dataset = _extCrud.ReferenceAuthorPlSpeciesAdd(currentTbl90ReferenceAuthor);

                        else
                            dataset = _extCrud.ReferenceAuthorPlSpeciesUpdate(dataset, currentTbl90ReferenceAuthor);
                        return dataset;
                    }

                default:
                    return dataset;
            }
        }
        public void SaveReferenceSource(Tbl90Reference currentTbl90ReferenceSource, string name)
        {
            //Combobox select RefSourceId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl90ReferenceSource.RefSourceId)) return;

            try
            {
                var dataset = _uow.Tbl90References.GetById(currentTbl90ReferenceSource.ReferenceId);
                //  var dataset = _extCrud.GetReferenceSingleByReferenceId<Tbl90Reference>(currentTbl90ReferenceSource.ReferenceId);

                dataset = AddUpdateReferenceSource(currentTbl90ReferenceSource, dataset, name);


                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl90ReferenceSource.Info)) return;

                try
                {
                    _extCrud.ReferenceSourceSave(dataset, currentTbl90ReferenceSource);

                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl90ReferenceSource.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl90ReferenceSource.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
        }
        private Tbl90Reference AddUpdateReferenceSource(Tbl90Reference currentTbl90ReferenceSource, Tbl90Reference dataset, string name)
        {
            switch (name)
            {
                case "Regnum":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceRegnumAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceRegnumUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Phylum":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourcePhylumAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourcePhylumUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Division":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceDivisionAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceDivisionUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Subphylum":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceSubphylumAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceSubphylumUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Subdivision":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceSubdivisionAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceSubdivisionUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Superclass":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceSuperclassAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceSuperclassUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Class":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceClassAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceClassUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Subclass":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceSubclassAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceSubclassUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Infraclass":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceInfraclassAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceInfraclassUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Legio":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceLegioAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceLegioUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Ordo":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceOrdoAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceOrdoUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Subordo":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceSubordoAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceSubordoUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Infraordo":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceInfraordoAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceInfraordoUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Superfamily":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceSuperfamilyAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceSuperfamilyUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Family":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceFamilyAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceFamilyUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Subfamily":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceSubfamilyAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceSubfamilyUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Infrafamily":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceInfrafamilyAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceInfrafamilyUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Supertribus":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceSupertribusAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceSupertribusUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Tribus":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceTribusAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceTribusUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Subtribus":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceSubtribusAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceSubtribusUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Infratribus":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceInfratribusAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceInfratribusUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "Genus":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceGenusAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceGenusUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "FiSpecies":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourceFiSpeciesAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourceFiSpeciesUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                case "PlSpecies":
                    {
                        if (currentTbl90ReferenceSource.ReferenceId == 0)
                            dataset = _extCrud.ReferenceSourcePlSpeciesAdd(currentTbl90ReferenceSource);
                        else
                            dataset = _extCrud.ReferenceSourcePlSpeciesUpdate(dataset, currentTbl90ReferenceSource);
                        return dataset;
                    }
                default:
                    return dataset;
            }
        }
        public void SaveReferenceExpert(Tbl90Reference currentTbl90ReferenceExpert, string name)
        {
            //Combobox select RefExpertId  may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl90ReferenceExpert.RefExpertId)) return;

            try
            {
                var dataset = _uow.Tbl90References.GetById(currentTbl90ReferenceExpert.ReferenceId);
                // var dataset = _extCrud.GetReferenceSingleByReferenceId<Tbl90Reference>(currentTbl90ReferenceExpert.ReferenceId);

                dataset = AddUpdateReferenceExpert(currentTbl90ReferenceExpert, dataset, name);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl90ReferenceExpert.Info)) return;

                try
                {
                    _extCrud.ReferenceExpertSave(dataset, currentTbl90ReferenceExpert);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl90ReferenceExpert.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl90ReferenceExpert.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
        }
        private Tbl90Reference AddUpdateReferenceExpert(Tbl90Reference currentTbl90ReferenceExpert, Tbl90Reference dataset, string name)
        {
            switch (name)
            {
                case "Regnum":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertRegnumAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertRegnumUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Phylum":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertPhylumAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertPhylumUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Division":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertDivisionAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertDivisionUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Subphylum":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertSubphylumAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertSubphylumUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Subdivision":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertSubdivisionAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertSubdivisionUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Superclass":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertSuperclassAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertSuperclassUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Class":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertClassAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertClassUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Subclass":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertSubclassAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertSubclassUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Infraclass":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertInfraclassAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertInfraclassUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Legio":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertLegioAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertLegioUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Ordo":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertOrdoAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertOrdoUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Subordo":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertSubordoAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertSubordoUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Infraordo":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertInfraordoAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertInfraordoUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Superfamily":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertSuperfamilyAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertSuperfamilyUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Family":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertFamilyAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertFamilyUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Subfamily":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertSubfamilyAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertSubfamilyUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Infrafamily":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertInfrafamilyAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertInfrafamilyUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Supertribus":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertSupertribusAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertSupertribusUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Tribus":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertTribusAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertTribusUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Subtribus":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertSubtribusAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertSubtribusUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Infratribus":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertInfratribusAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertInfratribusUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "Genus":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertGenusAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertGenusUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "FiSpecies":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertFiSpeciesAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertFiSpeciesUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                case "PlSpecies":
                    {
                        if (currentTbl90ReferenceExpert.ReferenceId == 0)
                            dataset = _extCrud.ReferenceExpertPlSpeciesAdd(currentTbl90ReferenceExpert);
                        else
                            dataset = _extCrud.ReferenceExpertPlSpeciesUpdate(dataset, currentTbl90ReferenceExpert);
                        return dataset;
                    }
                default:
                    return dataset;
            }
        }

        public bool SaveRefExpert(Tbl90RefExpert currentTbl90RefExpert)
        {
            var returnBool = false;
            try
            {
                var dataset = _uow.Tbl90RefExperts.GetById(currentTbl90RefExpert.RefExpertId);
                //    var dataset = _extCrud.GetRefExpertSingleByRefExpertId<Tbl90RefExpert>(currentTbl90RefExpert.RefExpertId);

                if (currentTbl90RefExpert.RefExpertId == 0)
                    dataset = _extCrud.RefExpertAdd(currentTbl90RefExpert);
                else
                    dataset = _extCrud.RefExpertUpdate(dataset, currentTbl90RefExpert);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl90RefExpert.RefExpertName)) return false;

                try
                {
                    _extCrud.RefExpertSave(dataset, currentTbl90RefExpert);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl90RefExpert.RefExpertId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl90RefExpert.RefExpertName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }

            return returnBool;
        }
        public bool SaveRefSource(Tbl90RefSource currentTbl90RefSource)
        {
            var returnBool = false;
            try
            {
                var dataset = _uow.Tbl90RefSources.GetById(currentTbl90RefSource.RefSourceId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl90RefExpert>(currentTbl90RefExpert.RefExpertId);

                if (currentTbl90RefSource.RefSourceId == 0)
                    dataset = _extCrud.RefSourceAdd(currentTbl90RefSource);
                else
                    dataset = _extCrud.RefSourceUpdate(dataset, currentTbl90RefSource);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl90RefSource.RefSourceName)) return false;

                try
                {
                    _extCrud.RefSourceSave(dataset, currentTbl90RefSource);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl90RefSource.RefSourceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl90RefSource.RefSourceName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }

            return returnBool;
        }
        public bool SaveRefAuthor(Tbl90RefAuthor currentTbl90RefAuthor)
        {
            var returnBool = false;

            try
            {
                var dataset = _uow.Tbl90RefAuthors.GetById(currentTbl90RefAuthor.RefAuthorId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl90RefExpert>(currentTbl90RefExpert.RefExpertId);

                if (currentTbl90RefAuthor.RefAuthorId == 0)
                    dataset = _extCrud.RefAuthorAdd(currentTbl90RefAuthor);
                else
                    dataset = _extCrud.RefAuthorUpdate(dataset, currentTbl90RefAuthor);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl90RefAuthor.RefAuthorName)) return false;

                try
                {
                    _extCrud.RefAuthorSave(dataset, currentTbl90RefAuthor);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl90RefAuthor.RefAuthorId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl90RefAuthor.RefAuthorName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }

        public bool SaveComment(Tbl93Comment currentTbl93Comment, string name)
        {
            var returnBool = false;

            try
            {
                var dataset = _uow.Tbl93Comments.GetById(currentTbl93Comment.CommentId);
                //  var dataset = _extCrud.GetCommentSingleByCommentId<Tbl93Comment>(currentTbl93Comment.CommentId);

                dataset = AddUpdateComment(currentTbl93Comment, dataset, name);

                //if (currentTbl93Comment.CommentId == 0)
                //    dataset = _extCrud.CommentRegnumAdd(currentTbl93Comment);
                //else
                //    dataset = _extCrud.CommentRegnumUpdate(dataset, currentTbl93Comment);


                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl93Comment.Info)) return false;

                try
                {
                    _extCrud.CommentSave(dataset, currentTbl93Comment);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl93Comment.CommentId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl93Comment.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }
        private Tbl93Comment AddUpdateComment(Tbl93Comment currentTbl93Comment, Tbl93Comment dataset, string name)
        {
            switch (name)
            {
                case "Regnum":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentRegnumAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentRegnumUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Phylum":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentPhylumAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentPhylumUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Division":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentDivisionAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentDivisionUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Subphylum":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentSubphylumAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentSubphylumUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Subdivision":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentSubdivisionAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentSubdivisionUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Superclass":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentSuperclassAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentSuperclassUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Class":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentClassAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentClassUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Subclass":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentSubclassAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentSubclassUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Infraclass":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentInfraclassAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentInfraclassUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Legio":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentLegioAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentLegioUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Ordo":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentOrdoAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentOrdoUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Subordo":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentSubordoAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentSubordoUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Infraordo":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentInfraordoAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentInfraordoUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Superfamily":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentSuperfamilyAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentSuperfamilyUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Family":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentFamilyAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentFamilyUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Subfamily":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentSubfamilyAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentSubfamilyUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Infrafamily":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentInfrafamilyAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentInfrafamilyUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Supertribus":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentSupertribusAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentSupertribusUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Tribus":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentTribusAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentTribusUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Subtribus":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentSubtribusAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentSubtribusUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Infratribus":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentInfratribusAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentInfratribusUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "Genus":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentGenusAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentGenusUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "FiSpecies":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentFiSpeciesAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentFiSpeciesUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }
                case "PlSpecies":
                    {
                        if (currentTbl93Comment.CommentId == 0)
                            dataset = _extCrud.CommentPlSpeciesAdd(currentTbl93Comment);
                        else
                            dataset = _extCrud.CommentPlSpeciesUpdate(dataset, currentTbl93Comment);
                        return dataset;
                    }

                default:
                    return dataset;
            }
        }

        public bool SaveComment(Tbl93Comment currentTbl93Comment)
        {
            var returnBool = false;
            // Regnum-PlSpecies
            var z = 0;
            if (currentTbl93Comment.RegnumId != null) z += 1;
            if (currentTbl93Comment.PhylumId != null) z += 1;
            if (currentTbl93Comment.SubphylumId != null) z += 1;
            if (currentTbl93Comment.DivisionId != null) z += 1;
            if (currentTbl93Comment.SubdivisionId != null) z += 1;
            if (currentTbl93Comment.SuperclassId != null) z += 1;
            if (currentTbl93Comment.ClassId != null) z += 1;
            if (currentTbl93Comment.SubclassId != null) z += 1;
            if (currentTbl93Comment.InfraclassId != null) z += 1;
            if (currentTbl93Comment.LegioId != null) z += 1;
            if (currentTbl93Comment.OrdoId != null) z += 1;
            if (currentTbl93Comment.SubordoId != null) z += 1;
            if (currentTbl93Comment.InfraordoId != null) z += 1;
            if (currentTbl93Comment.SuperfamilyId != null) z += 1;
            if (currentTbl93Comment.FamilyId != null) z += 1;
            if (currentTbl93Comment.SubfamilyId != null) z += 1;
            if (currentTbl93Comment.InfrafamilyId != null) z += 1;
            if (currentTbl93Comment.SupertribusId != null) z += 1;
            if (currentTbl93Comment.TribusId != null) z += 1;
            if (currentTbl93Comment.SubtribusId != null) z += 1;
            if (currentTbl93Comment.InfratribusId != null) z += 1;
            if (currentTbl93Comment.GenusId != null) z += 1;
            if (currentTbl93Comment.FiSpeciesId != null) z += 1;
            if (currentTbl93Comment.PlSpeciesId != null) z += 1;

            if (z == 0)
            {
                _allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(z);
                return false;
            }

            if (z > 1)
            {
                _allMessageBoxes.IdSelectInComboBoxMoreThenOneInfoMessageBox();
                return false;
            }

            try
            {
                var dataset = _uow.Tbl93Comments.GetById(currentTbl93Comment.CommentId);
                //  var dataset = _extCrud.GetCommentSingleByCommentId<Tbl93Comment>(currentTbl93Comment.CommentId);

                if (currentTbl93Comment.CommentId == 0)
                    dataset = _extCrud.CommentAdd(currentTbl93Comment);
                else
                    dataset = _extCrud.CommentUpdate(dataset, currentTbl93Comment);


                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl93Comment.Info)) return false;

                try
                {
                    _extCrud.CommentSave(dataset, currentTbl93Comment);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl93Comment.CommentId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl93Comment.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }

        public bool SaveCountry(TblCountry currentTblCountry)
        {
            var returnBool = false;

            try
            {
                var dataset = _uow.TblCountries.GetById(currentTblCountry.CountryId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTblCountry.Name)) return false;

                if (currentTblCountry.CountryId == 0)
                    dataset = _extCrud.CountryAdd(currentTblCountry);
                else
                    dataset = _extCrud.CountryUpdate(dataset, currentTblCountry);

                try
                {
                    _extCrud.CountrySave(dataset, currentTblCountry);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTblCountry.CountryId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTblCountry.Name);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }

        public bool SaveUserProfile(TblUserProfile currentTblUserProfile)
        {
            var returnBool = false;

            try
            {
                var dataset = _uow.TblUserProfiles.GetById(currentTblUserProfile.UserProfileId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTblUserProfile.Email)) return false;

                if (currentTblUserProfile.UserProfileId == 0)
                    dataset = _extCrud.UserProfileAdd(currentTblUserProfile);
                else
                    dataset = _extCrud.UserProfileUpdate(dataset, currentTblUserProfile);

                try
                {
                    _extCrud.UserProfileSave(dataset, currentTblUserProfile);
                    returnBool = true;
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.ErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    SimpleLog.Log(e);
                    return false;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    SimpleLog.Log(e);
                    return false;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTblUserProfile.UserProfileId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTblUserProfile.Email);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                SimpleLog.Log(e);
            }
            return returnBool;
        }

    }
}
