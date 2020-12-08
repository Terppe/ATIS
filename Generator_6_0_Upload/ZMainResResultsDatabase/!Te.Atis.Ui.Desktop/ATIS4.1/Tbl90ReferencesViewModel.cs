using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Domain;
using Te.Atis.Ui.Desktop.Domain.Helper;
using Te.Atis.Ui.Desktop.MessageBox;    

    
         //    Tbl90ReferencesViewModel Skriptdatum:  21.07.2018  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl90ReferencesViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"

        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;   
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl90ReferencesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {    
        
                // Code runs "for real" 
                _businessLayer = new BusinessLayer.BusinessLayer();
                _entityException = new DbEntityException();
            }
        }     
        #endregion "Constructor"         
 

 //    Part 1    

             
        #region "Public Commands Basic Tbl90Reference"
        //-------------------------------------------------------------------------
        private RelayCommand _clearReferenceCommand;

        public ICommand ClearReferenceCommand => _clearReferenceCommand ??
                                                  (_clearReferenceCommand = new RelayCommand(delegate { ClearReference(null); }));         
             
        private RelayCommand _getReferencesByNameOrIdCommand;  

        public  ICommand GetReferencesByNameOrIdCommand => _getReferencesByNameOrIdCommand ??
                                                           (_getReferencesByNameOrIdCommand = new RelayCommand(delegate { GetReferencesByNameOrId(null); }));        
             
        private RelayCommand _addReferenceCommand;

        public ICommand AddReferenceCommand => _addReferenceCommand ??
                                                (_addReferenceCommand = new RelayCommand(delegate { AddReference(null); }));

        private RelayCommand _copyReferenceCommand;

        public ICommand CopyReferenceCommand => _copyReferenceCommand ??
                                                 (_copyReferenceCommand = new RelayCommand(delegate { CopyReference(null); }));      
             
        private RelayCommand _deleteReferenceCommand;

        public ICommand DeleteReferenceCommand => _deleteReferenceCommand ??
                                                   (_deleteReferenceCommand = new RelayCommand(delegate { DeleteReference(null); }));    
             
        private RelayCommand _saveReferenceCommand;

        public ICommand SaveReferenceCommand => _saveReferenceCommand ??
                                                 (_saveReferenceCommand = new RelayCommand(delegate { SaveReference(null); }));
        //-------------------------------------------------------------------------          
        
        private void ClearReference(object o)
        {
            SearchReferenceInfo = string.Empty;

            Tbl90ReferencesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
        }
        //----------------------------------------------------------------------                  
        
        private void GetReferencesByNameOrId(object o)
        {
            Tbl90ReferencesList = int.TryParse(SearchReferenceInfo, out var id) ?
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(id)) :
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceName(SearchReferenceInfo));

            Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03Regnums());
            Tbl06PhylumsAllList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06Phylums());
            Tbl09DivisionsAllList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09Divisions());
            Tbl12SubphylumsAllList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12Subphylums());
            Tbl15SubdivisionsAllList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15Subdivisions());
            Tbl18SuperclassesAllList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18Superclasses());
            Tbl21ClassesAllList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21Classes());
            Tbl24SubclassesAllList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24Subclasses());
            Tbl27InfraclassesAllList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27Infraclasses());
            Tbl30LegiosAllList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30Legios());
            Tbl33OrdosAllList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33Ordos());
            Tbl36SubordosAllList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36Subordos());
            Tbl39InfraordosAllList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39Infraordos());
            Tbl42SuperfamiliesAllList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42Superfamilies());
            Tbl45FamiliesAllList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45Families());
            Tbl48SubfamiliesAllList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48Subfamilies());
            Tbl51InfrafamiliesAllList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51Infrafamilies());
            Tbl54SupertribussesAllList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54Supertribusses());
            Tbl57TribussesAllList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57Tribusses());
            Tbl60SubtribussesAllList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60Subtribusses());
            Tbl63InfratribussesAllList = new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63Infratribusses());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());
            Tbl69FiSpeciessesAllList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciesses());
            Tbl72PlSpeciessesAllList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciesses());

            Tbl90RefExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExperts());
            Tbl90RefSourcesAllList = new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSources());
            Tbl90RefAuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthors());

            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
        
        private void AddReference(object o)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference> {new Tbl90Reference
            {    ReferenceID = 0     }  };

            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyReference(object o)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>();

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90Reference.ReferenceID);

            Tbl90ReferencesList.Add(new Tbl90Reference
            {
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = reference.Info,
                Memo = reference.Memo
            });

            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
        
        private void DeleteReference(object o)
        {
            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90Reference.ReferenceID);
                if (reference != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90Reference.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    reference.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveReference(reference);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90Reference.Info,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90Reference.Info + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceName(SearchReferenceInfo));

            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
       
        private void SaveReference(object o)
        {
            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90Reference.ReferenceID);
                if (CurrentTbl90Reference.ReferenceID != 0)
                {
                    if (reference != null) //update
                    {
                            reference.RefExpertID = CurrentTbl90Reference.RefExpertID;
                            reference.RefAuthorID = CurrentTbl90Reference.RefAuthorID;
                            reference.RefSourceID = CurrentTbl90Reference.RefSourceID;
                            reference.RegnumID = CurrentTbl90Reference.RegnumID;
                            reference.PhylumID = CurrentTbl90Reference.PhylumID;
                            reference.DivisionID = CurrentTbl90Reference.DivisionID;
                            reference.SubphylumID = CurrentTbl90Reference.SubphylumID;
                            reference.SubdivisionID = CurrentTbl90Reference.SubdivisionID;
                            reference.SuperclassID = CurrentTbl90Reference.SuperclassID;
                            reference.ClassID = CurrentTbl90Reference.ClassID;
                            reference.SubclassID = CurrentTbl90Reference.SubclassID;
                            reference.InfraclassID = CurrentTbl90Reference.InfraclassID;
                            reference.LegioID = CurrentTbl90Reference.LegioID;
                            reference.OrdoID = CurrentTbl90Reference.OrdoID;
                            reference.SubordoID = CurrentTbl90Reference.SubordoID;
                            reference.InfraordoID = CurrentTbl90Reference.InfraordoID;
                            reference.SuperfamilyID = CurrentTbl90Reference.SuperfamilyID;
                            reference.FamilyID = CurrentTbl90Reference.FamilyID;
                            reference.SubfamilyID = CurrentTbl90Reference.SubfamilyID;
                            reference.InfrafamilyID = CurrentTbl90Reference.InfrafamilyID;
                            reference.SupertribusID = CurrentTbl90Reference.SupertribusID;
                            reference.TribusID = CurrentTbl90Reference.TribusID;
                            reference.SubtribusID = CurrentTbl90Reference.SubtribusID;
                            reference.InfratribusID = CurrentTbl90Reference.InfratribusID;
                            reference.GenusID = CurrentTbl90Reference.GenusID;
                            reference.PlSpeciesID = CurrentTbl90Reference.PlSpeciesID;
                            reference.FiSpeciesID = CurrentTbl90Reference.FiSpeciesID;
                            reference.Valid = CurrentTbl90Reference.Valid;
                            reference.ValidYear = CurrentTbl90Reference.ValidYear;
                            reference.Info = CurrentTbl90Reference.Info;
                            reference.Updater = Environment.UserName;
                            reference.UpdaterDate = DateTime.Now;
                            reference.Memo = CurrentTbl90Reference.Memo;
                        reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                            reference = new Tbl90Reference   //add new
                            {
                        RefAuthorID = CurrentTbl90Reference.RefAuthorID,
                            RefSourceID = CurrentTbl90Reference.RefSourceID,
                            RefExpertID = CurrentTbl90Reference.RefExpertID,
                            RegnumID = CurrentTbl90Reference.RegnumID,
                            PhylumID = CurrentTbl90Reference.PhylumID,
                            DivisionID = CurrentTbl90Reference.DivisionID,
                            SubphylumID = CurrentTbl90Reference.SubphylumID,
                            SubdivisionID = CurrentTbl90Reference.SubdivisionID,
                            SuperclassID = CurrentTbl90Reference.SuperclassID,
                            ClassID = CurrentTbl90Reference.ClassID,
                            SubclassID = CurrentTbl90Reference.SubclassID,
                            InfraclassID = CurrentTbl90Reference.InfraclassID,
                            LegioID = CurrentTbl90Reference.LegioID,
                            OrdoID = CurrentTbl90Reference.OrdoID,
                            SubordoID = CurrentTbl90Reference.SubordoID,
                            InfraordoID = CurrentTbl90Reference.InfraordoID,
                            SuperfamilyID = CurrentTbl90Reference.SuperfamilyID,
                            FamilyID = CurrentTbl90Reference.FamilyID,
                            SubfamilyID = CurrentTbl90Reference.SubfamilyID,
                            InfrafamilyID = CurrentTbl90Reference.InfrafamilyID,
                            SupertribusID = CurrentTbl90Reference.SupertribusID,
                            TribusID = CurrentTbl90Reference.TribusID,
                            SubtribusID = CurrentTbl90Reference.SubtribusID,
                            InfratribusID = CurrentTbl90Reference.InfratribusID,
                            GenusID = CurrentTbl90Reference.GenusID,
                            PlSpeciesID = CurrentTbl90Reference.PlSpeciesID,
                            FiSpeciesID = CurrentTbl90Reference.FiSpeciesID,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90Reference.Valid,
                            ValidYear = CurrentTbl90Reference.ValidYear,
                            Info = CurrentTbl90Reference.Info,
                            Memo = CurrentTbl90Reference.Memo,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            EntityState = EntityState.Added
                    };
                }
                {
                    //check if dataset RefExpertId and RefSourceId and RefAuthorId and Info already exist       
                    var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90Reference);

                    if (dataset.Count != 0 && CurrentTbl90Reference.ReferenceID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90Reference.ReferenceID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl90Reference.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90Reference.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90Reference.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90Reference.ReferenceID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateReference(reference);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90Reference.ReferenceID.ToString(),
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl90Reference.ReferenceID == 0)  //new Dataset                        
                Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceName(CurrentTbl90Reference.Info));
            if (CurrentTbl90Reference.ReferenceID != 0)   //update 
                Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(CurrentTbl90Reference.ReferenceID));

            ReferencesView = CollectionViewSource.GetDefaultView(Tbl90ReferencesList);
            ReferencesView.Refresh();
        }
        #endregion "Public Commands"                   
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl90RefExpert"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearRefExpertCommand;

        public  ICommand ClearRefExpertCommand => _clearRefExpertCommand ??
                                                  (_clearRefExpertCommand = new RelayCommand(delegate { ClearRefExpert(null); }));

        private RelayCommand _getRefExpertsByNameOrIdCommand;  

        public  ICommand GetRefExpertsByNameOrIdCommand => _getRefExpertsByNameOrIdCommand ??
                                                           (_getRefExpertsByNameOrIdCommand = new RelayCommand(delegate { GetRefExpertsByNameOrId(null); }));

        private RelayCommand _addRefExpertCommand;

        public  ICommand AddRefExpertCommand => _addRefExpertCommand ??
                                                (_addRefExpertCommand = new RelayCommand(delegate { AddRefExpert(null); }));

        private RelayCommand _copyRefExpertCommand;

        public  ICommand CopyRefExpertCommand => _copyRefExpertCommand ??
                                                 (_copyRefExpertCommand = new RelayCommand(delegate { CopyRefExpert(null); }));

        private RelayCommand _saveRefExpertCommand;

        public  ICommand SaveRefExpertCommand => _saveRefExpertCommand ??
                                                 (_saveRefExpertCommand = new RelayCommand(delegate { SaveRefExpert(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearRefExpert(object o)
        {
            SearchRefExpertName = string.Empty;
            Tbl90RefExpertsList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        private void GetRefExpertsByNameOrId(object o)
        {
            Tbl90RefExpertsList = int.TryParse(SearchRefExpertName, out var id) ?
                new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertId(id)) :
                new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertName(SearchRefExpertName));

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        private void AddRefExpert(object o)      
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert> {new Tbl90RefExpert
            {   RefExpertName = CultRes.StringsRes.DatasetNew    }  };

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void CopyRefExpert(object o)
        {
            Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>();

            var refexpert = _businessLayer.SingleListTbl90RefExpertsByRefExpertId(CurrentTbl90RefExpert.RefExpertID);

            Tbl90RefExpertsList.Add(new Tbl90RefExpert
            {
                RefExpertName = CultRes.StringsRes.DatasetNew,
                Valid = refexpert.Valid,
                ValidYear = refexpert.ValidYear,
                Info = refexpert.Info,
                Notes = refexpert.Notes,
                Memo = refexpert.Memo
            });

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
          
        private void SaveRefExpert(object o)
        {
            try
            {
                var refexpert = _businessLayer.SingleListTbl90RefExpertsByRefExpertId(CurrentTbl90RefExpert.RefExpertID);
                if (CurrentTbl90RefExpert.RefExpertID != 0)
                {
                    if (refexpert != null) //update
                    {
                        refexpert.RefExpertName = CurrentTbl90RefExpert.RefExpertName;
                            refexpert.Valid = CurrentTbl90RefExpert.Valid;
                            refexpert.ValidYear = CurrentTbl90RefExpert.ValidYear;
                            refexpert.Info = CurrentTbl90RefExpert.Info;
                            refexpert.Notes = CurrentTbl90RefExpert.Notes;
                            refexpert.Updater = Environment.UserName;
                            refexpert.UpdaterDate = DateTime.Now;
                            refexpert.Memo = CurrentTbl90RefExpert.Memo;
                        refexpert.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    refexpert = new Tbl90RefExpert   //add new
                    {
                        RefExpertName = CurrentTbl90RefExpert.RefExpertName,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefExpert.Valid,
                            ValidYear = CurrentTbl90RefExpert.ValidYear,
                            Info = CurrentTbl90RefExpert.Info,
                            Notes = CurrentTbl90RefExpert.Notes,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefExpert.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //RefExpertID may be not 0
                    if (CurrentTbl90RefExpert.RefExpertID == 0)

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and RefExpertId already exist       
                    var dataset = _businessLayer.ListTbl90RefExpertsByRefExpertNameAndRefExpertId(CurrentTbl90RefExpert.RefExpertName, CurrentTbl90RefExpert.RefExpertID);

                    if (dataset.Count != 0 && CurrentTbl90RefExpert.RefExpertID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefExpert.RefExpertName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl90RefExpert.RefExpertID == 0 ||
                        dataset.Count != 0 && CurrentTbl90RefExpert.RefExpertID != 0 ||
                        dataset.Count == 0 && CurrentTbl90RefExpert.RefExpertID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefExpert.RefExpertName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateRefExpert(refexpert);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefExpert.RefExpertName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl90RefExpert.RefExpertID == 0)  //new Dataset                        
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertName(CurrentTbl90RefExpert.RefExpertName));
            if (CurrentTbl90RefExpert.RefExpertID != 0)   //update 
                Tbl90RefExpertsList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExpertsByRefExpertId(CurrentTbl90RefExpert.RefExpertID));

            SelectedMainTabIndex = 0;
            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();
        }
        #endregion "Public Commands"                        
 
            

 //    Part 3    

           
        #region "Public Commands Connect <== Tbl90RefSource"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearRefSourceCommand;

        public  ICommand ClearRefSourceCommand => _clearRefSourceCommand ??
                                                  (_clearRefSourceCommand = new RelayCommand(delegate { ClearRefSource(null); }));

        private RelayCommand _getRefSourcesByNameOrIdCommand;  

        public  ICommand GetRefSourcesByNameOrIdCommand => _getRefSourcesByNameOrIdCommand ??
                                                           (_getRefSourcesByNameOrIdCommand = new RelayCommand(delegate { GetRefSourcesByNameOrId(null); }));

        private RelayCommand _addRefSourceCommand;

        public ICommand AddRefSourceCommand => _addRefSourceCommand ??
                                                (_addRefSourceCommand = new RelayCommand(delegate { AddRefSource(null); }));

        private RelayCommand _copyRefSourceCommand;

        public ICommand CopyRefSourceCommand => _copyRefSourceCommand ??
                                                 (_copyRefSourceCommand = new RelayCommand(delegate { CopyRefSource(null); }));

        private RelayCommand _saveRefSourceCommand;

        public ICommand SaveRefSourceCommand => _saveRefSourceCommand ??
                                                 (_saveRefSourceCommand = new RelayCommand(delegate { SaveRefSource(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearRefSource(object o)
        {
            SearchRefSourceName = string.Empty;
            Tbl90RefSourcesList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        private void GetRefSourcesByNameOrId(object o)
        {
            Tbl90RefSourcesList = int.TryParse(SearchRefSourceName, out var id) ?
                new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSourcesByRefSourceId(id)) :
                new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSourcesByRefSourceName(SearchRefSourceName));

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        //----------------------------------------------------------------------            
       
        private void AddRefSource(object o)      
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource> {new Tbl90RefSource
            {         RefSourceName = CultRes.StringsRes.DatasetNew       }  };

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
       
        private void CopyRefSource(object o)
        {
            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>();

            var refsource = _businessLayer.SingleListTbl90RefSourcesByRefSourceId(CurrentTbl90RefSource.RefSourceID);

            Tbl90RefSourcesList.Add(new Tbl90RefSource
            {
                RefSourceName = CultRes.StringsRes.DatasetNew,
                Valid = refsource.Valid,
                ValidYear = refsource.ValidYear,
                Info = refsource.Info,
                Notes = refsource.Notes,
                Memo = refsource.Memo
            });

            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
        
        private void SaveRefSource(object o)
        {
            try
            {
                var refsource = _businessLayer.SingleListTbl90RefSourcesByRefSourceId(CurrentTbl90RefSource.RefSourceID);
                if (CurrentTbl90RefSource.RefSourceID != 0)
                {
                    if (refsource != null) //update
                    {
                            refsource.RefSourceName = CurrentTbl90RefSource.RefSourceName;
                            refsource.Valid = CurrentTbl90RefSource.Valid;
                            refsource.ValidYear = CurrentTbl90RefSource.ValidYear;
                            refsource.Info = CurrentTbl90RefSource.Info;
                            refsource.Notes = CurrentTbl90RefSource.Notes;
                            refsource.Updater = Environment.UserName;
                            refsource.UpdaterDate = DateTime.Now;
                            refsource.Memo = CurrentTbl90RefSource.Memo;
                            refsource.EntityState = EntityState.Modified;
                        }
                    }
                    else
                    {
                        refsource = new Tbl90RefSource   //add new
                        {
                        RefSourceName = CurrentTbl90RefSource.RefSourceName,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefSource.Valid,
                            ValidYear = CurrentTbl90RefSource.ValidYear,
                            Info = CurrentTbl90RefSource.Info,
                            Notes = CurrentTbl90RefSource.Notes,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefSource.Memo,
                            EntityState = EntityState.Added
                        };
                    }
                    {   
                    //RefSourceID may be not 0
                    if (CurrentTbl90RefSource.RefSourceID == 0)
                     {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                        //check if dataset with Name and RefSourceId already exist       
                    var dataset = _businessLayer.ListTbl90RefSourcesByRefSourceNameAndRefSourceId(CurrentTbl90RefSource.RefSourceName, CurrentTbl90RefSource.RefSourceID);

                    if (dataset.Count != 0 && CurrentTbl90RefSource.RefSourceID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefSource.RefSourceName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl90RefSource.RefSourceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90RefSource.RefSourceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90RefSource.RefSourceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefSource.RefSourceName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateRefSource(refsource);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefSource.RefSourceName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl90RefSource.RefSourceID == 0)  //new Dataset                        
                Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSourcesByRefSourceName(CurrentTbl90RefSource.RefSourceName));
            if (CurrentTbl90RefSource.RefSourceID != 0)   //update 
                Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSourcesByRefSourceId(CurrentTbl90RefSource.RefSourceID));

            SelectedMainTabIndex = 1;
            RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
            RefSourcesView.Refresh();
        }
        #endregion "Public Commands"                  
 


 //    Part 4    

           
        #region "Public Commands Connect <== Tbl90RefAuthor"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearRefAuthorCommand;

        public ICommand ClearRefAuthorCommand => _clearRefAuthorCommand ??
                                                  (_clearRefAuthorCommand = new RelayCommand(delegate { ClearRefAuthor(null); }));

        private RelayCommand _getRefAuthorsByNameOrIdCommand;  

        public  ICommand GetRefAuthorsByNameOrIdCommand => _getRefAuthorsByNameOrIdCommand ??
                                                           (_getRefAuthorsByNameOrIdCommand = new RelayCommand(delegate { GetRefAuthorsByNameOrId(null); }));

        private RelayCommand _addRefAuthorCommand;

        public ICommand AddRefAuthorCommand => _addRefAuthorCommand ??
                                                (_addRefAuthorCommand = new RelayCommand(delegate { AddRefAuthor(null); }));

        private RelayCommand _copyRefAuthorCommand;

        public ICommand CopyRefAuthorCommand => _copyRefAuthorCommand ??
                                                 (_copyRefAuthorCommand = new RelayCommand(delegate { CopyRefAuthor(null); }));

        private RelayCommand _saveRefAuthorCommand;

        public ICommand SaveRefAuthorCommand => _saveRefAuthorCommand ??
                                                 (_saveRefAuthorCommand = new RelayCommand(delegate { SaveRefAuthor(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearRefAuthor(object o)
        {
            SearchRefAuthorName = string.Empty;
            Tbl90RefAuthorsList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        private void GetRefAuthorsByNameOrId(object o)
        {
            Tbl90RefAuthorsList = int.TryParse(SearchRefAuthorName, out var id) ?
                new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorId(id)) :
                new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorName(SearchRefAuthorName));

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        //----------------------------------------------------------------------            
         
        private void AddRefAuthor(object o)      
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor> {new Tbl90RefAuthor
            {       RefAuthorName = CultRes.StringsRes.DatasetNew,          }    };

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------                
             
        private void CopyRefAuthor(object o)
        {
            Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>();

            var refauthor = _businessLayer.SingleListTbl90RefAuthorsByRefAuthorId(CurrentTbl90RefAuthor.RefAuthorID);

            Tbl90RefAuthorsList.Add(new Tbl90RefAuthor
            {
                RefAuthorName = CultRes.StringsRes.DatasetNew,
                Valid = refauthor.Valid,
                ValidYear = refauthor.ValidYear,
                PublicationYear = refauthor.PublicationYear,
                ArticelTitle = refauthor.ArticelTitle,
                BookName = refauthor.BookName,
                Page1 = refauthor.Page1,
                Publisher = refauthor.Publisher,
                PublicationPlace = refauthor.PublicationPlace,
                ISBN = refauthor.ISBN,
                Notes = refauthor.Notes,
                Info = refauthor.Info,
                Memo = refauthor.Memo
            });

            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
             
        private void SaveRefAuthor(object o)
        {
            try
            {
                var refauthor = _businessLayer.SingleListTbl90RefAuthorsByRefAuthorId(CurrentTbl90RefAuthor.RefAuthorID);
                if (CurrentTbl90RefAuthor.RefAuthorID != 0)
                {
                    if (refauthor != null) //update
                    {
                            refauthor.RefAuthorName = CurrentTbl90RefAuthor.RefAuthorName;
                            refauthor.Valid = CurrentTbl90RefAuthor.Valid;
                            refauthor.ValidYear = CurrentTbl90RefAuthor.ValidYear;
                            refauthor.PublicationYear = CurrentTbl90RefAuthor.PublicationYear;
                            refauthor.ArticelTitle = CurrentTbl90RefAuthor.ArticelTitle;
                            refauthor.BookName = CurrentTbl90RefAuthor.BookName;
                            refauthor.Info = CurrentTbl90RefAuthor.Info;
                            refauthor.Page1 = CurrentTbl90RefAuthor.Page1;
                            refauthor.Publisher = CurrentTbl90RefAuthor.Publisher;
                            refauthor.PublicationPlace = CurrentTbl90RefAuthor.PublicationPlace;
                            refauthor.ISBN = CurrentTbl90RefAuthor.ISBN;
                            refauthor.Notes = CurrentTbl90RefAuthor.Notes;
                            refauthor.Updater = Environment.UserName;
                            refauthor.UpdaterDate = DateTime.Now;
                            refauthor.Memo = CurrentTbl90RefAuthor.Memo;
                            refauthor.EntityState = EntityState.Modified;
                       }
                    }
                    else
                    {
                        refauthor = new Tbl90RefAuthor   //add new
                        {
                            RefAuthorName = CurrentTbl90RefAuthor.RefAuthorName,
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl90RefAuthor.Valid,
                            ValidYear = CurrentTbl90RefAuthor.ValidYear,
                            PublicationYear = CurrentTbl90RefAuthor.PublicationYear,
                            ArticelTitle = CurrentTbl90RefAuthor.ArticelTitle,
                            BookName = CurrentTbl90RefAuthor.BookName,
                            Info = CurrentTbl90RefAuthor.Info,
                            Page1 = CurrentTbl90RefAuthor.Page1,
                            Publisher = CurrentTbl90RefAuthor.Publisher,
                            PublicationPlace = CurrentTbl90RefAuthor.PublicationPlace,
                            ISBN = CurrentTbl90RefAuthor.ISBN,
                            Notes = CurrentTbl90RefAuthor.Notes,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl90RefAuthor.Memo,
                            EntityState = EntityState.Added
                    };
                }
                {
                    //RefAuthorID may be not 0
                    if (CurrentTbl90RefAuthor.RefAuthorID == 0)
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and ReferenceId already exist       
                    var dataset = _businessLayer.ListTbl90RefAuthorsByRefAuthorNameAndRefAuthorId(CurrentTbl90RefAuthor.RefAuthorName, CurrentTbl90RefAuthor.RefAuthorID);

                    if (dataset.Count != 0 && CurrentTbl90RefAuthor.RefAuthorID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90RefAuthor.RefAuthorName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl90RefAuthor.RefAuthorID == 0 ||
                        dataset.Count != 0 && CurrentTbl90RefAuthor.RefAuthorID != 0 ||
                        dataset.Count == 0 && CurrentTbl90RefAuthor.RefAuthorID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90RefAuthor.RefAuthorName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateRefAuthor(refauthor);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90RefAuthor.RefAuthorName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl90RefAuthor.RefAuthorID == 0)  //new Dataset                        
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorName(CurrentTbl90RefAuthor.RefAuthorName));
            if (CurrentTbl90RefAuthor.RefAuthorID != 0)   //update 
                Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthorsByRefAuthorId(CurrentTbl90RefAuthor.RefAuthorID));

            SelectedMainTabIndex = 1;
            RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
            RefAuthorsView.Refresh();
        }
        #endregion "Public Commands"                  
 
             

 //    Part 5    

 
                                       
 //    Part 6    

 
            

 //    Part 7    

 

 //    Part 8    

 
            
 //    Part 9    

     
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??
                                                         (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); }));

        public void GetConnectedTablesById(object o)
        {
            Tbl90RefExpertsList?.Clear();

            Tbl90RefExpertsList =  new ObservableCollection<Tbl90RefExpert>(
                    _businessLayer.ListTbl90RefExpertsByRefExpertId(CurrentTbl90Reference.RefExpertID));
 

            RefExpertsView = CollectionViewSource.GetDefaultView(Tbl90RefExpertsList);
            RefExpertsView.Refresh();

            SelectedMainTabIndex = 0;  //change to Connect tab
            SelectedMainSubRefTabIndex = 0;
            SelectedDetailTabIndex = 1;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;
        }

        #endregion "Public Commands Connected Tables by DoubleClick"     
 

 //    Part 10    

     
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubTabIndex;
        private int _selectedDetailSubRefTabIndex;

        public  int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex; 
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; 
                RaisePropertyChanged();
                if (_selectedMainTabIndex == 0)             
                    SelectedDetailSubTabIndex = 0;              
                if (_selectedMainTabIndex == 1)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 1;
                }
            }
        }

        public  int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex; 
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value; 
                 RaisePropertyChanged();
                if (_selectedMainSubRefTabIndex == 0)
                    SelectedDetailSubRefTabIndex = 0;
                if (_selectedMainSubRefTabIndex == 1)
                    SelectedDetailSubRefTabIndex = 1;
                if (_selectedMainSubRefTabIndex == 2)
                    SelectedDetailSubRefTabIndex = 2;
            }
        }

        public  int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex; 
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailTabIndex == 0)
                {
                    SelectedDetailSubTabIndex = 0;
                    SelectedMainTabIndex = 0;
                    SelectedDetailSubRefTabIndex = 0;
                }
                if (_selectedDetailTabIndex == 1)
                {
                    SelectedDetailSubTabIndex = 1;
                    SelectedMainTabIndex = 0;
                    SelectedDetailSubRefTabIndex = 0;
                }
                if (_selectedDetailTabIndex == 2)
                {
                    SelectedDetailSubTabIndex = 2;
                    SelectedMainTabIndex = 0;
                    SelectedDetailSubRefTabIndex = 0;
                }
            }
        }

        public  int SelectedDetailSubTabIndex
        {
            get => _selectedDetailSubTabIndex;
            set
            {
                if (value == _selectedDetailSubTabIndex) return;
                _selectedDetailSubTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailSubTabIndex == 0)
                {
                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailSubTabIndex == 1)
                {
                    Tbl90RefSourcesList?.Clear();

                    Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>(
                        _businessLayer.ListTbl90RefSourcesByRefSourceId(CurrentTbl90Reference.RefSourceID));

                    RefSourcesView = CollectionViewSource.GetDefaultView(Tbl90RefSourcesList);
                    RefSourcesView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl90RefAuthorsList?.Clear();

                    Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>(
                        _businessLayer.ListTbl90RefAuthorsByRefAuthorId(CurrentTbl90Reference.RefAuthorID));

                    RefAuthorsView = CollectionViewSource.GetDefaultView(Tbl90RefAuthorsList);
                    RefAuthorsView.Refresh();

                    SelectedMainTabIndex = 2;
                }
            }
        }

        public  int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex;
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailSubRefTabIndex == 0)
                {
                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }
        #endregion "Public Commands to open Detail TabItems"
 

 //    Part 11    

      
        #region "Public Properties Tbl90Reference"

        private string _searchReferenceInfo = string.Empty;
        public string SearchReferenceInfo
        {
            get => _searchReferenceInfo; 
            set { _searchReferenceInfo = value; RaisePropertyChanged();  }
        }

        public   ICollectionView ReferencesView;
        private  Tbl90Reference CurrentTbl90Reference => ReferencesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesList;
        public  ObservableCollection<Tbl90Reference> Tbl90ReferencesList
        {
            get => _tbl90ReferencesList; 
            set {  _tbl90ReferencesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesAllList;
        public  ObservableCollection<Tbl90Reference> Tbl90ReferencesAllList
        {
            get => _tbl90ReferencesAllList; 
            set { _tbl90ReferencesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsAllList;

        public ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsAllList
        {
            get => _tbl90RefExpertsAllList;
            set
            {
                _tbl90RefExpertsAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesAllList;

        public ObservableCollection<Tbl90RefSource> Tbl90RefSourcesAllList
        {
            get => _tbl90RefSourcesAllList;
            set
            {
                _tbl90RefSourcesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsAllList;

        public ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsAllList
        {
            get => _tbl90RefAuthorsAllList;
            set
            {
                _tbl90RefAuthorsAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;

        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set
            {
                _tbl03RegnumsAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;

        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList;
            set
            {
                _tbl06PhylumsAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;

        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList;
            set
            {
                _tbl09DivisionsAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;

        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList;
            set
            {
                _tbl12SubphylumsAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;

        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get => _tbl15SubdivisionsAllList;
            set
            {
                _tbl15SubdivisionsAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;

        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList;
            set
            {
                _tbl18SuperclassesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;

        public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList;
            set
            {
                _tbl21ClassesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;

        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList;
            set
            {
                _tbl24SubclassesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;

        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList;
            set
            {
                _tbl27InfraclassesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList;

        public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
        {
            get => _tbl30LegiosAllList;
            set
            {
                _tbl30LegiosAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList;

        public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
        {
            get => _tbl33OrdosAllList;
            set
            {
                _tbl33OrdosAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;

        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList;
            set
            {
                _tbl36SubordosAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList;

        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
        {
            get => _tbl39InfraordosAllList;
            set
            {
                _tbl39InfraordosAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesAllList;

        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesAllList
        {
            get => _tbl42SuperfamiliesAllList;
            set
            {
                _tbl42SuperfamiliesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;

        public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get => _tbl45FamiliesAllList;
            set
            {
                _tbl45FamiliesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList;

        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
        {
            get => _tbl48SubfamiliesAllList;
            set
            {
                _tbl48SubfamiliesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;

        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList;
            set
            {
                _tbl51InfrafamiliesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList;

        public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
        {
            get => _tbl54SupertribussesAllList;
            set
            {
                _tbl54SupertribussesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;

        public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList;
            set
            {
                _tbl57TribussesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;

        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList;
            set
            {
                _tbl60SubtribussesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList;

        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
        {
            get => _tbl63InfratribussesAllList;
            set
            {
                _tbl63InfratribussesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;

        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set
            {
                _tbl66GenussesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;

        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList;
            set
            {
                _tbl69FiSpeciessesAllList = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;

        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList;
            set
            {
                _tbl72PlSpeciessesAllList = value;
                RaisePropertyChanged();
            }
        }
        #endregion "Public Properties"   
       
        #region "Public Properties Tbl90RefExpert"

        private string _searchRefExpertName = string.Empty;
        public  string SearchRefExpertName
        {
            get  => _searchRefExpertName; 
            set { _searchRefExpertName = value; RaisePropertyChanged(); }
        }

        public  ICollectionView RefExpertsView;
        private Tbl90RefExpert CurrentTbl90RefExpert => RefExpertsView?.CurrentItem as Tbl90RefExpert;           

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsList;
        public ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsList
        {
            get => _tbl90RefExpertsList; 
            set { _tbl90RefExpertsList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"   
  
       
        #region "Public Properties Tbl90RefSource"

        private string _searchRefSourceName = string.Empty;
        public  string SearchRefSourceName
        {
            get => _searchRefSourceName; 
            set { _searchRefSourceName = value; RaisePropertyChanged(); }
        }

        public  ICollectionView RefSourcesView;
        private  Tbl90RefSource CurrentTbl90RefSource => RefSourcesView?.CurrentItem as Tbl90RefSource;           

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesList;
        public   ObservableCollection<Tbl90RefSource> Tbl90RefSourcesList
        {
            get => _tbl90RefSourcesList; 
            set { _tbl90RefSourcesList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"   
        
        #region "Public Properties Tbl90RefAuthor"

        private string _searchRefAuthorName = string.Empty;
        public string SearchRefAuthorName
        {
            get => _searchRefAuthorName; 
            set { _searchRefAuthorName = value; RaisePropertyChanged(); }
        }

        public ICollectionView RefAuthorsView;
        private Tbl90RefAuthor CurrentTbl90RefAuthor => RefAuthorsView?.CurrentItem as Tbl90RefAuthor;           

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsList
        {
            get => _tbl90RefAuthorsList; 
            set { _tbl90RefAuthorsList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"     
           
        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public new ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList; 
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public new ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList; 
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public new ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList; 
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90ReferenceAuthor"

        private string _searchReferenceAuthorName  = string.Empty;
        public new string SearchReferenceAuthorName
        {
            get => _searchReferenceAuthorName; 
            set { _searchReferenceAuthorName = value; RaisePropertyChanged(); }
        }
        public new ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public new ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList; 
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        private string _searchReferenceSourceName  = string.Empty;
        public new string SearchReferenceSourceName
        {
            get => _searchReferenceSourceName; 
            set { _searchReferenceSourceName = value; RaisePropertyChanged(); }
        }
        public new ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public new ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList; 
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        private string _searchReferenceExpertName  = string.Empty;
        public new string SearchReferenceExpertName
        {
            get => _searchReferenceExpertName; 
            set { _searchReferenceExpertName = value; RaisePropertyChanged(); }
        }
        public new ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public new ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList; 
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
 

 



   }
}   
