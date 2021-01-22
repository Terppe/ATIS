using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using ATIS.Dal.Models;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.D06Phylum;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Core
{
    public class SaveFunctions : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(SaveFunctions));
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        //private readonly AtisDbContext _context = new AtisDbContext();
        private readonly CrudFunctions _extCrud = new CrudFunctions();
        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();

        public void SaveRegnum(Tbl03Regnum currentTbl03Regnum)
        {
            try
            {
              //  var dataset = _extCrud.GetRegnumSingleByRegnumId<Tbl03Regnum>(currentTbl03Regnum.RegnumId);
                var dataset = _uow.Tbl03Regnums.GetById(currentTbl03Regnum.RegnumId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl03Regnum.RegnumName))
                    return;

                if (currentTbl03Regnum.RegnumId == 0)
                    dataset = _extCrud.RegnumAdd(currentTbl03Regnum);
                else
                    dataset = _extCrud.RegnumUpdate(dataset, currentTbl03Regnum);
                try
                {
                    _extCrud.RegnumSave(dataset, currentTbl03Regnum);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.DetailErrorMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl03Regnum.RegnumId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl03Regnum.RegnumName + " " + currentTbl03Regnum.Subregnum);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SavePhylum(Tbl06Phylum currentTbl06Phylum)
        {
            //Combobox select RegnumId  may not be 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl06Phylum.RegnumId)) return ;
            try
            {
          //    var dataset = _extCrud.GetPhylumSingleByPhylumId(currentTbl06Phylum.PhylumId);
              var dataset = _uow.Tbl06Phylums.GetById(currentTbl06Phylum.PhylumId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl06Phylum.PhylumName)) return ;

                if (currentTbl06Phylum.PhylumId == 0)
                    dataset = _extCrud.PhylumAdd(currentTbl06Phylum);
                else
                    dataset = _extCrud.PhylumUpdate(dataset, currentTbl06Phylum);

                try
                {
                    _extCrud.PhylumSave(dataset, currentTbl06Phylum);
                }

                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return ;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return ;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl06Phylum.PhylumId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl06Phylum.PhylumName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveDivision(Tbl09Division currentTbl09Division)
        {
            //Combobox select RegnumId  may not be 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl09Division.RegnumId)) return;
            try
            {
            //    var dataset = _extCrud.GetDivisionSingleByDivisionId<Tbl09Division>(currentTbl09Division.DivisionId);
                var dataset = _uow.Tbl09Divisions.GetById(currentTbl09Division.DivisionId);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl09Division.DivisionName)) return;

                if (currentTbl09Division.DivisionId == 0)
                    dataset = _extCrud.DivisionAdd(currentTbl09Division);
                else
                    dataset = _extCrud.DivisionUpdate(dataset, currentTbl09Division);

                try
                {
                    _extCrud.DivisionSave(dataset, currentTbl09Division);
                }

                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl09Division.DivisionId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl09Division.DivisionName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveSubphylum(Tbl12Subphylum currentTbl12Subphylum)
        {
            //Combobox select PhylumId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl12Subphylum.PhylumId)) return;

            try
            {
            //    var dataset = _extCrud.GetSubphylumSingleBySubphylumId<Tbl12Subphylum>(currentTbl12Subphylum.SubphylumId);
                var dataset = _uow.Tbl12Subphylums.GetById(currentTbl12Subphylum.SubphylumId);


                if (currentTbl12Subphylum.SubphylumId == 0)
                    dataset = _extCrud.SubphylumAdd(currentTbl12Subphylum);
                else
                    dataset = _extCrud.SubphylumUpdate(dataset, currentTbl12Subphylum);


                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl12Subphylum.SubphylumName)) return;

                try
                {
                    _extCrud.SubphylumSave(dataset, currentTbl12Subphylum);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl12Subphylum.SubphylumId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl12Subphylum.SubphylumName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

        }
        public void SaveSubdivision(Tbl15Subdivision currentTbl15Subdivision)
        {
            //Combobox select DivisionId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl15Subdivision.DivisionId)) return;

            try
            {
          //      var dataset = _extCrud.GetSubdivisionSingleBySubdivisionId<Tbl15Subdivision>(currentTbl15Subdivision.SubdivisionId);
                var dataset = _uow.Tbl15Subdivisions.GetById(currentTbl15Subdivision.SubdivisionId);

                if (currentTbl15Subdivision.SubdivisionId == 0)
                    dataset = _extCrud.SubdivisionAdd(currentTbl15Subdivision);
                else
                    dataset = _extCrud.SubdivisionUpdate(dataset, currentTbl15Subdivision);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl15Subdivision.SubdivisionName)) return;

                try
                {
                    _extCrud.SubdivisionSave(dataset, currentTbl15Subdivision);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl15Subdivision.SubdivisionId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl15Subdivision.SubdivisionName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveSuperclass(Tbl18Superclass currentTbl18Superclass)
        {
            //Combobox select SubdivisionId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl18Superclass.SubdivisionId)) return;
            //Combobox select SubphylumId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl18Superclass.SubphylumId)) return;

            try
            {
                var dataset = _uow.Tbl18Superclasses.GetById(currentTbl18Superclass.SuperclassId);
                //    var dataset = _extCrud.GetSuperclassSingleBySubdivisionId<Tbl18Superclass>(currentTbl18Superclass.SuperclassId);

                if (currentTbl18Superclass.SuperclassId == 0)
                    dataset = _extCrud.SuperclassAdd(currentTbl18Superclass);
                else
                    dataset = _extCrud.SuperclassUpdate(dataset, currentTbl18Superclass);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl18Superclass.SuperclassName)) return;

                try
                {
                    _extCrud.SuperclassSave(dataset, currentTbl18Superclass);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl18Superclass.SuperclassId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl18Superclass.SuperclassName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveClass(Tbl21Class currentTbl21Class)
        {
            //Combobox select SuperclassId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl21Class.SuperclassId)) return;

            try
            {
                var dataset = _uow.Tbl21Classes.GetById(currentTbl21Class.ClassId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl21Class.ClassId);

                if (currentTbl21Class.ClassId == 0)
                    dataset = _extCrud.ClassAdd(currentTbl21Class);
                else
                    dataset = _extCrud.ClassUpdate(dataset, currentTbl21Class);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl21Class.ClassName)) return;

                try
                {
                    _extCrud.ClassSave(dataset, currentTbl21Class);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl21Class.ClassId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl21Class.ClassName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveSubclass(Tbl24Subclass currentTbl24Subclass)
        {
            //Combobox select ClassId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl24Subclass.ClassId)) return;

            try
            {
                var dataset = _uow.Tbl24Subclasses.GetById(currentTbl24Subclass.SubclassId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl21Class.ClassId);

                if (currentTbl24Subclass.SubclassId == 0)
                    dataset = _extCrud.SubclassAdd(currentTbl24Subclass);
                else
                    dataset = _extCrud.SubclassUpdate(dataset, currentTbl24Subclass);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl24Subclass.SubclassName)) return;

                try
                {
                    _extCrud.SubclassSave(dataset, currentTbl24Subclass);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl24Subclass.SubclassId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl24Subclass.SubclassName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveInfraclass(Tbl27Infraclass currentTbl27Infraclass)
        {
            //Combobox select SubclassId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl27Infraclass.SubclassId)) return;

            try
            {
                var dataset = _uow.Tbl27Infraclasses.GetById(currentTbl27Infraclass.InfraclassId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl21Class.ClassId);

                if (currentTbl27Infraclass.InfraclassId == 0)
                    dataset = _extCrud.InfraclassAdd(currentTbl27Infraclass);
                else
                    dataset = _extCrud.InfraclassUpdate(dataset, currentTbl27Infraclass);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl27Infraclass.InfraclassName)) return;

                try
                {
                    _extCrud.InfraclassSave(dataset, currentTbl27Infraclass);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl27Infraclass.InfraclassId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl27Infraclass.InfraclassName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveLegio(Tbl30Legio currentTbl30Legio)
        {
            //Combobox select InfraclassId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl30Legio.InfraclassId)) return;

            try
            {
                var dataset = _uow.Tbl30Legios.GetById(currentTbl30Legio.LegioId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl21Class.ClassId);

                if (currentTbl30Legio.LegioId == 0)
                    dataset = _extCrud.LegioAdd(currentTbl30Legio);
                else
                    dataset = _extCrud.LegioUpdate(dataset, currentTbl30Legio);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl30Legio.LegioName)) return;

                try
                {
                    _extCrud.LegioSave(dataset, currentTbl30Legio);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl30Legio.LegioId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl30Legio.LegioName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveOrdo(Tbl33Ordo currentTbl33Ordo)
        {
            //Combobox select LegioId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl33Ordo.LegioId)) return;

            try
            {
                var dataset = _uow.Tbl33Ordos.GetById(currentTbl33Ordo.OrdoId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl33Ordo.OrdoId);

                if (currentTbl33Ordo.OrdoId == 0)
                    dataset = _extCrud.OrdoAdd(currentTbl33Ordo);
                else
                    dataset = _extCrud.OrdoUpdate(dataset, currentTbl33Ordo);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl33Ordo.OrdoName)) return;

                try
                {
                    _extCrud.OrdoSave(dataset, currentTbl33Ordo);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl33Ordo.OrdoId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl33Ordo.OrdoName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveSubordo(Tbl36Subordo currentTbl36Subordo)
        {
            //Combobox select OrdoId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl36Subordo.OrdoId)) return;

            try
            {
                var dataset = _uow.Tbl36Subordos.GetById(currentTbl36Subordo.SubordoId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl36Subordo.SubordoId);

                if (currentTbl36Subordo.SubordoId == 0)
                    dataset = _extCrud.SubordoAdd(currentTbl36Subordo);
                else
                    dataset = _extCrud.SubordoUpdate(dataset, currentTbl36Subordo);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl36Subordo.SubordoName)) return;

                try
                {
                    _extCrud.SubordoSave(dataset, currentTbl36Subordo);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl36Subordo.SubordoId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl36Subordo.SubordoName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveInfraordo(Tbl39Infraordo currentTbl39Infraordo)
        {
            //Combobox select SubordoId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl39Infraordo.SubordoId)) return;

            try
            {
                var dataset = _uow.Tbl39Infraordos.GetById(currentTbl39Infraordo.InfraordoId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl39Infraordo.InfraordoId);

                if (currentTbl39Infraordo.InfraordoId == 0)
                    dataset = _extCrud.InfraordoAdd(currentTbl39Infraordo);
                else
                    dataset = _extCrud.InfraordoUpdate(dataset, currentTbl39Infraordo);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl39Infraordo.InfraordoName)) return;

                try
                {
                    _extCrud.InfraordoSave(dataset, currentTbl39Infraordo);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl39Infraordo.InfraordoId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl39Infraordo.InfraordoName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveSuperfamily(Tbl42Superfamily currentTbl42Superfamily)
        {
            //Combobox select InfraordoId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl42Superfamily.InfraordoId)) return;

            try
            {
                var dataset = _uow.Tbl42Superfamilies.GetById(currentTbl42Superfamily.SuperfamilyId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl42Superfamily.SuperfamilyId);

                if (currentTbl42Superfamily.SuperfamilyId == 0)
                    dataset = _extCrud.SuperfamilyAdd(currentTbl42Superfamily);
                else
                    dataset = _extCrud.SuperfamilyUpdate(dataset, currentTbl42Superfamily);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl42Superfamily.SuperfamilyName)) return;

                try
                {
                    _extCrud.SuperfamilySave(dataset, currentTbl42Superfamily);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl42Superfamily.SuperfamilyId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl42Superfamily.SuperfamilyName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveFamily(Tbl45Family currentTbl45Family)
        {
            //Combobox select SuperfamilyId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl45Family.SuperfamilyId)) return;

            try
            {
                var dataset = _uow.Tbl45Families.GetById(currentTbl45Family.FamilyId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl45Family.FamilyId);

                if (currentTbl45Family.FamilyId == 0)
                    dataset = _extCrud.FamilyAdd(currentTbl45Family);
                else
                    dataset = _extCrud.FamilyUpdate(dataset, currentTbl45Family);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl45Family.FamilyName)) return;

                try
                {
                    _extCrud.FamilySave(dataset, currentTbl45Family);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl45Family.FamilyId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl45Family.FamilyName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveSubfamily(Tbl48Subfamily currentTbl48Subfamily)
        {
            //Combobox select FamilyId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl48Subfamily.FamilyId)) return;

            try
            {
                var dataset = _uow.Tbl48Subfamilies.GetById(currentTbl48Subfamily.SubfamilyId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl48Subfamily.SubfamilyId);

                if (currentTbl48Subfamily.SubfamilyId == 0)
                    dataset = _extCrud.SubfamilyAdd(currentTbl48Subfamily);
                else
                    dataset = _extCrud.SubfamilyUpdate(dataset, currentTbl48Subfamily);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl48Subfamily.SubfamilyName)) return;

                try
                {
                    _extCrud.SubfamilySave(dataset, currentTbl48Subfamily);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl48Subfamily.SubfamilyId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl48Subfamily.SubfamilyName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveInfrafamily(Tbl51Infrafamily currentTbl51Infrafamily)
        {
            //Combobox select SubfamilyId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl51Infrafamily.SubfamilyId)) return;

            try
            {
                var dataset = _uow.Tbl51Infrafamilies.GetById(currentTbl51Infrafamily.InfrafamilyId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl51Infrafamily.InfrafamilyId);

                if (currentTbl51Infrafamily.InfrafamilyId == 0)
                    dataset = _extCrud.InfrafamilyAdd(currentTbl51Infrafamily);
                else
                    dataset = _extCrud.InfrafamilyUpdate(dataset, currentTbl51Infrafamily);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl51Infrafamily.InfrafamilyName)) return;

                try
                {
                    _extCrud.InfrafamilySave(dataset, currentTbl51Infrafamily);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl51Infrafamily.InfrafamilyId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl51Infrafamily.InfrafamilyName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveSupertribus(Tbl54Supertribus currentTbl54Supertribus)
        {
            //Combobox select InfrafamilyId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl54Supertribus.InfrafamilyId)) return;

            try
            {
                var dataset = _uow.Tbl54Supertribusses.GetById(currentTbl54Supertribus.SupertribusId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl54Supertribus.SupertribusId);

                if (currentTbl54Supertribus.SupertribusId == 0)
                    dataset = _extCrud.SupertribusAdd(currentTbl54Supertribus);
                else
                    dataset = _extCrud.SupertribusUpdate(dataset, currentTbl54Supertribus);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl54Supertribus.SupertribusName)) return;

                try
                {
                    _extCrud.SupertribusSave(dataset, currentTbl54Supertribus);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl54Supertribus.SupertribusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl54Supertribus.SupertribusName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveTribus(Tbl57Tribus currentTbl57Tribus)
        {
            //Combobox select SupertribusId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl57Tribus.SupertribusId)) return;

            try
            {
                var dataset = _uow.Tbl57Tribusses.GetById(currentTbl57Tribus.TribusId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl57Tribus.TribusId);

                if (currentTbl57Tribus.TribusId == 0)
                    dataset = _extCrud.TribusAdd(currentTbl57Tribus);
                else
                    dataset = _extCrud.TribusUpdate(dataset, currentTbl57Tribus);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl57Tribus.TribusName)) return;

                try
                {
                    _extCrud.TribusSave(dataset, currentTbl57Tribus);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl57Tribus.TribusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl57Tribus.TribusName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveSubtribus(Tbl60Subtribus currentTbl60Subtribus)
        {
            //Combobox select TribusId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl60Subtribus.TribusId)) return;

            try
            {
                var dataset = _uow.Tbl60Subtribusses.GetById(currentTbl60Subtribus.SubtribusId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl60Subtribus.SubtribusId);

                if (currentTbl60Subtribus.SubtribusId == 0)
                    dataset = _extCrud.SubtribusAdd(currentTbl60Subtribus);
                else
                    dataset = _extCrud.SubtribusUpdate(dataset, currentTbl60Subtribus);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl60Subtribus.SubtribusName)) return;

                try
                {
                    _extCrud.SubtribusSave(dataset, currentTbl60Subtribus);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl60Subtribus.SubtribusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl60Subtribus.SubtribusName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveInfratribus(Tbl63Infratribus currentTbl63Infratribus)
        {
            //Combobox select SubtribusId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl63Infratribus.SubtribusId)) return;

            try
            {
                var dataset = _uow.Tbl63Infratribusses.GetById(currentTbl63Infratribus.InfratribusId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl63Infratribus.InfratribusId);

                if (currentTbl63Infratribus.InfratribusId == 0)
                    dataset = _extCrud.InfratribusAdd(currentTbl63Infratribus);
                else
                    dataset = _extCrud.InfratribusUpdate(dataset, currentTbl63Infratribus);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl63Infratribus.InfratribusName)) return;

                try
                {
                    _extCrud.InfratribusSave(dataset, currentTbl63Infratribus);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl63Infratribus.InfratribusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl63Infratribus.InfratribusName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveGenus(Tbl66Genus currentTbl66Genus)
        {
            //Combobox select InfratribusId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl66Genus.InfratribusId)) return;

            try
            {
                var dataset = _uow.Tbl66Genusses.GetById(currentTbl66Genus.GenusId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl66Genus.GenusId);

                if (currentTbl66Genus.GenusId == 0)
                    dataset = _extCrud.GenusAdd(currentTbl66Genus);
                else
                    dataset = _extCrud.GenusUpdate(dataset, currentTbl66Genus);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl66Genus.GenusName)) return;

                try
                {
                    _extCrud.GenusSave(dataset, currentTbl66Genus);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl66Genus.GenusId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl66Genus.GenusName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveSpeciesgroup(Tbl68Speciesgroup currentTbl68Speciesgroup)
        {
            ////Combobox select InfratribusId may be not 0
            //if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl66Genus.InfratribusId)) return;

            try
            {
                var dataset = _uow.Tbl68Speciesgroups.GetById(currentTbl68Speciesgroup.SpeciesgroupId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl66Genus.GenusId);

                if (currentTbl68Speciesgroup.SpeciesgroupId == 0)
                    dataset = _extCrud.SpeciesgroupAdd(currentTbl68Speciesgroup);
                else
                    dataset = _extCrud.SpeciesgroupUpdate(dataset, currentTbl68Speciesgroup);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl68Speciesgroup.SpeciesgroupName + " " + currentTbl68Speciesgroup.Subspeciesgroup)) return;

                try
                {
                    _extCrud.SpeciesgroupSave(dataset, currentTbl68Speciesgroup);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl68Speciesgroup.SpeciesgroupId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl68Speciesgroup.SpeciesgroupName + " " + currentTbl68Speciesgroup.Subspeciesgroup);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveFiSpecies(Tbl69FiSpecies currentTbl69FiSpecies)
        {
            //Combobox select GenusId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl69FiSpecies.GenusId)) return;

            try
            {
                var dataset = _uow.Tbl69FiSpeciesses.GetById(currentTbl69FiSpecies.FiSpeciesId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl69FiSpecies.FiSpeciesId);

                if (currentTbl69FiSpecies.FiSpeciesId == 0)
                    dataset = _extCrud.FiSpeciesAdd(currentTbl69FiSpecies);
                else
                    dataset = _extCrud.FiSpeciesUpdate(dataset, currentTbl69FiSpecies);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl69FiSpecies.FiSpeciesName + " " + currentTbl69FiSpecies.Subspecies + " " + currentTbl69FiSpecies.Divers)) return;

                try
                {
                    _extCrud.FiSpeciesSave(dataset, currentTbl69FiSpecies);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl69FiSpecies.FiSpeciesId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl69FiSpecies.FiSpeciesName + " " + currentTbl69FiSpecies.Subspecies + " " + currentTbl69FiSpecies.Divers);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SavePlSpecies(Tbl72PlSpecies currentTbl72PlSpecies)
        {
            //Combobox select GenusId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl72PlSpecies.GenusId)) return;

            try
            {
                var dataset = _uow.Tbl72PlSpeciesses.GetById(currentTbl72PlSpecies.PlSpeciesId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl69FiSpecies.FiSpeciesId);

                if (currentTbl72PlSpecies.PlSpeciesId == 0)
                    dataset = _extCrud.PlSpeciesAdd(currentTbl72PlSpecies);
                else
                    dataset = _extCrud.PlSpeciesUpdate(dataset, currentTbl72PlSpecies);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl72PlSpecies.PlSpeciesName + " " + currentTbl72PlSpecies.Subspecies + " " + currentTbl72PlSpecies.Divers)) return;

                try
                {
                    _extCrud.PlSpeciesSave(dataset, currentTbl72PlSpecies);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl72PlSpecies.PlSpeciesId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl72PlSpecies.PlSpeciesName + " " + currentTbl72PlSpecies.Subspecies + " " + currentTbl72PlSpecies.Divers);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }

        public void SaveName(Tbl78Name currentTbl78Name)
        {
            //Combobox select FiSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl78Name.FiSpeciesId)) return;
            //Combobox select PlSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl78Name.PlSpeciesId)) return;

            try
            {
                var dataset = _uow.Tbl78Names.GetById(currentTbl78Name.NameId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl66Genus.GenusId);

                if (currentTbl78Name.NameId == 0)
                    dataset = _extCrud.NameAdd(currentTbl78Name);
                else
                    dataset = _extCrud.NameUpdate(dataset, currentTbl78Name);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl78Name.NameName)) return;

                try
                {
                    _extCrud.NameSave(dataset, currentTbl78Name);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl78Name.NameId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl78Name.NameName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveImage(Tbl81Image currentTbl81Image)
        {
            //Combobox select FiSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl81Image.FiSpeciesId)) return;
            //Combobox select PlSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl81Image.PlSpeciesId)) return;

            try
            {
                var dataset = _uow.Tbl81Images.GetById(currentTbl81Image.ImageId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl81Image.ImageId);

                if (currentTbl81Image.ImageId == 0)
                    dataset = _extCrud.ImageAdd(currentTbl81Image);
                else
                    dataset = _extCrud.ImageUpdate(dataset, currentTbl81Image);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl81Image.Info)) return;

                try
                {
                    _extCrud.ImageSave(dataset, currentTbl81Image);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl81Image.ImageId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl81Image.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveSynonym(Tbl84Synonym currentTbl84Synonym)
        {
            //Combobox select FiSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl84Synonym.FiSpeciesId)) return;
            //Combobox select PlSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl84Synonym.PlSpeciesId)) return;

            try
            {
                var dataset = _uow.Tbl84Synonyms.GetById(currentTbl84Synonym.SynonymId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl66Genus.GenusId);

                if (currentTbl84Synonym.SynonymId == 0)
                    dataset = _extCrud.SynonymAdd(currentTbl84Synonym);
                else
                    dataset = _extCrud.SynonymUpdate(dataset, currentTbl84Synonym);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl84Synonym.SynonymName)) return;

                try
                {
                    _extCrud.SynonymSave(dataset, currentTbl84Synonym);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl84Synonym.SynonymId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl84Synonym.SynonymName);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }
        public void SaveGeographic(Tbl87Geographic currentTbl87Geographic)
        {
            //Combobox select FiSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl87Geographic.FiSpeciesId)) return;
            //Combobox select PlSpeciesId may be not 0
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl87Geographic.PlSpeciesId)) return;

            try
            {
                var dataset = _uow.Tbl87Geographics.GetById(currentTbl87Geographic.GeographicId);
                //    var dataset = _extCrud.GetClassSingleByClassId<Tbl21Class>(currentTbl87Geographic.GeographicId);

                if (currentTbl87Geographic.GeographicId == 0)
                    dataset = _extCrud.GeographicAdd(currentTbl87Geographic);
                else
                    dataset = _extCrud.GeographicUpdate(dataset, currentTbl87Geographic);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl87Geographic.Info)) return;

                try
                {
                    _extCrud.GeographicSave(dataset, currentTbl87Geographic);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl87Geographic.GeographicId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl87Geographic.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
        }


        public void SaveReferenceAuthor(Tbl90Reference currentTbl90ReferenceAuthor, string name)
        {
            //Combobox select RefAuthorId may be not null
            if (_allMessageBoxes.IdSelectInComboBoxNotBe0InfoMessageBox(currentTbl90ReferenceAuthor.RefAuthorId)) return;

            try
            {
                      var dataset = _uow.Tbl90References.GetById(currentTbl90ReferenceAuthor.ReferenceId);
               // var dataset = _extCrud.GetReferenceSingleByReferenceId<Tbl90Reference>(currentTbl90ReferenceAuthor.ReferenceId);


                dataset = AddUpdateReferenceAuthor(currentTbl90ReferenceAuthor, dataset, name);

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl90ReferenceAuthor.Info)) return;

                try
                {
                    _extCrud.ReferenceAuthorSave(dataset, currentTbl90ReferenceAuthor);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl90ReferenceAuthor.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl90ReferenceAuthor.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
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
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl90ReferenceSource.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl90ReferenceSource.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
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
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl90ReferenceExpert.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl90ReferenceExpert.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
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


        public void SaveComment(Tbl93Comment currentTbl93Comment, string name)
        {
            try
            {
                   var dataset = _uow.Tbl93Comments.GetById(currentTbl93Comment.CommentId);
              //  var dataset = _extCrud.GetCommentSingleByCommentId<Tbl93Comment>(currentTbl93Comment.CommentId);

                dataset = AddUpdateComment(currentTbl93Comment, dataset, name);


                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(currentTbl93Comment.Info))
                    return;

                try
                {
                    _extCrud.CommentSave(dataset, currentTbl93Comment);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.DetailErrorMessageBox(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, currentTbl93Comment.CommentId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : currentTbl93Comment.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.ErrorMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
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

    }
}
