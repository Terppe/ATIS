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

    
         //    Tbl60SubtribussesViewModel Skriptdatum:  19.06.2018  10:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl60SubtribussesViewModel : Tbl03RegnumsViewModel
    {     
         
        #region "Private Data Members"

        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;   
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl60SubtribussesViewModel()
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

             
        #region "Public Commands Basic Tbl60Subtribus"
        //-------------------------------------------------------------------------
        private RelayCommand _clearSubtribusCommand;

        public ICommand ClearSubtribusCommand => _clearSubtribusCommand ??
                                                  (_clearSubtribusCommand = new RelayCommand(delegate { ClearSubtribus(null); }));         
             
        private RelayCommand _getSubtribussesByNameOrIdCommand;  

        public  ICommand GetSubtribussesByNameOrIdCommand => _getSubtribussesByNameOrIdCommand ??
                                                           (_getSubtribussesByNameOrIdCommand = new RelayCommand(delegate { GetSubtribussesByNameOrId(null); }));        
             
        private RelayCommand _addSubtribusCommand;

        public ICommand AddSubtribusCommand => _addSubtribusCommand ??
                                                (_addSubtribusCommand = new RelayCommand(delegate { AddSubtribus(null); }));

        private RelayCommand _copySubtribusCommand;

        public ICommand CopySubtribusCommand => _copySubtribusCommand ??
                                                 (_copySubtribusCommand = new RelayCommand(delegate { CopySubtribus(null); }));      
             
        private RelayCommand _deleteSubtribusCommand;

        public ICommand DeleteSubtribusCommand => _deleteSubtribusCommand ??
                                                   (_deleteSubtribusCommand = new RelayCommand(delegate { DeleteSubtribus(null); }));    
             
        private RelayCommand _saveSubtribusCommand;

        public ICommand SaveSubtribusCommand => _saveSubtribusCommand ??
                                                 (_saveSubtribusCommand = new RelayCommand(delegate { SaveSubtribus(null); }));
        //-------------------------------------------------------------------------          
     
        private void ClearSubtribus(object o)
        {
            SearchSubtribusName = string.Empty;

            Tbl57TribussesList?.Clear();
            Tbl60SubtribussesList?.Clear();
            Tbl63InfratribussesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();
        }
        //----------------------------------------------------------------------                  
     
        private void GetSubtribussesByNameOrId(object o)
        {
            Tbl57TribussesList?.Clear();
            Tbl63InfratribussesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();

            Tbl60SubtribussesList = int.TryParse(SearchSubtribusName, out var id) ?
                new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesBySubtribusId(id)) :
                new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesBySubtribusName(SearchSubtribusName));

            Tbl57TribussesAllList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57Tribusses());

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
     
        private void AddSubtribus(object o)
        {
            Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus> {new Tbl60Subtribus
            {
                SubtribusName = CultRes.StringsRes.DatasetNew,
                TribusID = CurrentTbl60Subtribus.TribusID
            }  };

            Tbl57TribussesAllList?.Clear();
            Tbl57TribussesAllList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57Tribusses());

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
     
        private void CopySubtribus(object o)
        {
            Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>();

            var subtribus = _businessLayer.SingleListTbl60SubtribussesBySubtribusId(CurrentTbl60Subtribus.SubtribusID);

            Tbl60SubtribussesList.Add(new Tbl60Subtribus
            {
                 TribusID =  subtribus. TribusID,
                 SubtribusName = CultRes.StringsRes.DatasetNew,
                Valid =  subtribus.Valid,
                ValidYear =  subtribus.ValidYear,
                Synonym =  subtribus.Synonym,
                Author =  subtribus.Author,
                AuthorYear =  subtribus.AuthorYear,
                Info =  subtribus.Info,
                EngName =  subtribus.EngName,
                GerName =  subtribus.GerName,
                FraName =  subtribus.FraName,
                PorName =  subtribus.PorName,
                Memo =  subtribus.Memo
            });

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
     
        private void DeleteSubtribus(object o)
        {
            try
            {
                var subtribus = _businessLayer.SingleListTbl60SubtribussesBySubtribusId(CurrentTbl60Subtribus.SubtribusID);
                if (subtribus != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl60Subtribus.SubtribusName,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    subtribus.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveSubtribus(subtribus);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl60Subtribus.SubtribusName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl60Subtribus.SubtribusName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesBySubtribusName(SearchSubtribusName));

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
     
        private void SaveSubtribus(object o)
        {
            try
            {
                var subtribus = _businessLayer.SingleListTbl60SubtribussesBySubtribusId(CurrentTbl60Subtribus.SubtribusID);
                if (CurrentTbl60Subtribus.SubtribusID != 0)
                {
                    if (subtribus != null) //update
                    {
                        subtribus.SubtribusName = CurrentTbl60Subtribus.SubtribusName;
                        subtribus.TribusID = CurrentTbl60Subtribus.TribusID;
                        subtribus.Valid = CurrentTbl60Subtribus.Valid;
                        subtribus.ValidYear = CurrentTbl60Subtribus.ValidYear;       
                        subtribus.Synonym = CurrentTbl60Subtribus.Synonym;
                        subtribus.Author = CurrentTbl60Subtribus.Author;
                        subtribus.AuthorYear = CurrentTbl60Subtribus.AuthorYear;
                        subtribus.Info = CurrentTbl60Subtribus.Info;
                        subtribus.EngName = CurrentTbl60Subtribus.EngName;
                        subtribus.GerName = CurrentTbl60Subtribus.GerName;
                        subtribus.FraName = CurrentTbl60Subtribus.FraName;
                        subtribus.PorName = CurrentTbl60Subtribus.PorName;
                        subtribus.Updater = Environment.UserName;
                        subtribus.UpdaterDate = DateTime.Now;
                        subtribus.Memo = CurrentTbl60Subtribus.Memo;
                        subtribus.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    subtribus = new Tbl60Subtribus   //add new
                    {
                        SubtribusName = CurrentTbl60Subtribus.SubtribusName,
                        TribusID = CurrentTbl60Subtribus.TribusID,

                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl60Subtribus.Valid,
                        ValidYear = CurrentTbl60Subtribus.ValidYear,
                        Synonym = CurrentTbl60Subtribus.Synonym,
                        Author = CurrentTbl60Subtribus.Author,
                        AuthorYear = CurrentTbl60Subtribus.AuthorYear,
                        Info = CurrentTbl60Subtribus.Info,
                        EngName = CurrentTbl60Subtribus.EngName,
                        GerName = CurrentTbl60Subtribus.GerName,
                        FraName = CurrentTbl60Subtribus.FraName,
                        PorName = CurrentTbl60Subtribus.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl60Subtribus.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //TribusID may be not 0
                    if (CurrentTbl60Subtribus.TribusID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and TribusId already exist       
                    var dataset = _businessLayer.ListTbl60SubtribussesBySubtribusNameAndTribusId(CurrentTbl60Subtribus.SubtribusName, CurrentTbl60Subtribus.TribusID);

                    if (dataset.Count != 0 && CurrentTbl60Subtribus.SubtribusID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl60Subtribus.SubtribusName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl60Subtribus.SubtribusID == 0 ||
                        dataset.Count != 0 && CurrentTbl60Subtribus.SubtribusID != 0 ||
                        dataset.Count == 0 && CurrentTbl60Subtribus.SubtribusID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl60Subtribus.SubtribusName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateSubtribus(subtribus);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl60Subtribus.SubtribusName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl60Subtribus.SubtribusID == 0)  //new Dataset                        
                Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesByEmail(CurrentTbl60Subtribus.Email));
            if (CurrentTbl60Subtribus.SubtribusID != 0)   //update 
                Tbl60SubtribussesList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60SubtribussesBySubtribusId(CurrentTbl60Subtribus.SubtribusID));

            SubtribussesView = CollectionViewSource.GetDefaultView(Tbl60SubtribussesList);
            SubtribussesView.Refresh();
        }
        #endregion "Public Commands"                  
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl57Tribus"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearTribusCommand;

        public  ICommand ClearTribusCommand => _clearTribusCommand ??
                                                  (_clearTribusCommand = new RelayCommand(delegate { ClearTribus(null); }));

        private RelayCommand _getTribussesByNameOrIdCommand;  

        public  ICommand GetTribussesByNameOrIdCommand => _getTribussesByNameOrIdCommand ??
                                                           (_getTribussesByNameOrIdCommand = new RelayCommand(delegate { GetTribussesByNameOrId(null); }));

        private RelayCommand _addTribusCommand;

        public  ICommand AddTribusCommand => _addTribusCommand ??
                                                (_addTribusCommand = new RelayCommand(delegate { AddTribus(null); }));

        private RelayCommand _copyTribusCommand;

        public  ICommand CopyTribusCommand => _copyTribusCommand ??
                                                 (_copyTribusCommand = new RelayCommand(delegate { CopyTribus(null); }));

        private RelayCommand _saveTribusCommand;

        public  ICommand SaveTribusCommand => _saveTribusCommand ??
                                                 (_saveTribusCommand = new RelayCommand(delegate { SaveTribus(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearTribus(object o)
        {
            SearchTribusName = string.Empty;
            Tbl57TribussesList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        private void GetTribussesByNameOrId(object o)
        {
            Tbl57TribussesList = int.TryParse(SearchTribusName, out var id) ?
                new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusId(id)) :
                new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusName(SearchTribusName));

            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            TribussesView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        private void AddTribus(object o)      
        {
            Tbl57TribussesList = new ObservableCollection<Tbl57Tribus> {new Tbl57Tribus
            {   TribusName = CultRes.StringsRes.DatasetNew    }  };

            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            TribussesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void CopyTribus(object o)
        {
            Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>();

            var tribus = _businessLayer.SingleListTbl57TribussesByTribusId(CurrentTbl57Tribus.TribusID);

            Tbl57TribussesList.Add(new Tbl57Tribus
            {
                SupertribusID = tribus.SupertribusID,
                TribusName = CultRes.StringsRes.DatasetNew,
                Valid =  tribus.Valid,
                ValidYear =  tribus.ValidYear,
                Synonym =  tribus.Synonym,
                Author =  tribus.Author,
                AuthorYear =  tribus.AuthorYear,
                Info =  tribus.Info,
                EngName =  tribus.EngName,
                GerName =  tribus.GerName,
                FraName =  tribus.FraName,
                PorName =  tribus.PorName,
                Memo =  tribus.Memo
            });

            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            TribussesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void SaveTribus(object o)
        {
            try
            {
                var tribus = _businessLayer.SingleListTbl57TribussesByTribusId(CurrentTbl57Tribus.TribusID);
                if (CurrentTbl57Tribus.TribusID != 0)
                {
                    if (tribus != null) //update
                    {
                        tribus.TribusName = CurrentTbl57Tribus.TribusName;
                        tribus.Valid = CurrentTbl57Tribus.Valid;
                        tribus.ValidYear = CurrentTbl57Tribus.ValidYear;       
                        tribus.Synonym = CurrentTbl57Tribus.Synonym;
                        tribus.Author = CurrentTbl57Tribus.Author;
                        tribus.AuthorYear = CurrentTbl57Tribus.AuthorYear;
                        tribus.Info = CurrentTbl57Tribus.Info;
                        tribus.EngName = CurrentTbl57Tribus.EngName;
                        tribus.GerName = CurrentTbl57Tribus.GerName;
                        tribus.FraName = CurrentTbl57Tribus.FraName;
                        tribus.PorName = CurrentTbl57Tribus.PorName;
                        tribus.Updater = Environment.UserName;
                        tribus.UpdaterDate = DateTime.Now;
                        tribus.Memo = CurrentTbl57Tribus.Memo;
                        tribus.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    tribus = new Tbl57Tribus   //add new
                    {
                        TribusName = CurrentTbl57Tribus.TribusName,              
                        SupertribusID = CurrentTbl57Tribus.SupertribusID,     
                        CountID = RandomHelper.Randomnumber(),

                        Valid = CurrentTbl57Tribus.Valid,
                        ValidYear = CurrentTbl57Tribus.ValidYear,
                        Synonym = CurrentTbl57Tribus.Synonym,
                        Author = CurrentTbl57Tribus.Author,
                        AuthorYear = CurrentTbl57Tribus.AuthorYear,
                        Info = CurrentTbl57Tribus.Info,
                        EngName = CurrentTbl57Tribus.EngName,
                        GerName = CurrentTbl57Tribus.GerName,
                        FraName = CurrentTbl57Tribus.FraName,
                        PorName = CurrentTbl57Tribus.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl57Tribus.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //SupertribusID may be not 0
                    if (CurrentTbl57Tribus.SupertribusID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and TribusId already exist       
                    var dataset = _businessLayer.ListTbl57TribussesByTribusNameAndSupertribusId(CurrentTbl57Tribus.TribusName, CurrentTbl57Tribus.SupertribusID);

                    if (dataset.Count != 0 && CurrentTbl57Tribus.TribusID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl57Tribus.TribusName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl57Tribus.TribusID == 0 ||
                        dataset.Count != 0 && CurrentTbl57Tribus.TribusID != 0 ||
                        dataset.Count == 0 && CurrentTbl57Tribus.TribusID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl57Tribus.TribusName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateTribus(tribus);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl57Tribus.TribusName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl57Tribus.TribusID == 0)  //new Dataset                        
                Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusName(CurrentTbl57Tribus.TribusName));
            if (CurrentTbl57Tribus.TribusID != 0)   //update 
                Tbl57TribussesList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57TribussesByTribusId(CurrentTbl57Tribus.TribusID));

            SelectedMainTabIndex = 0;
            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            TribussesView.Refresh();
        }
        #endregion "Public Commands"                  
 
            

 //    Part 3    

 


 //    Part 4    

           
        #region "Public Commands Connect <== Tbl63Infratribus"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearInfratribusCommand;

        public ICommand ClearInfratribusCommand => _clearInfratribusCommand ??
                                                  (_clearInfratribusCommand = new RelayCommand(delegate { ClearInfratribus(null); }));

        private RelayCommand _getInfratribussesByNameOrIdCommand;  

        public  ICommand GetInfratribussesByNameOrIdCommand => _getInfratribussesByNameOrIdCommand ??
                                                           (_getInfratribussesByNameOrIdCommand = new RelayCommand(delegate { GetInfratribussesByNameOrId(null); }));

        private RelayCommand _addInfratribusCommand;

        public ICommand AddInfratribusCommand => _addInfratribusCommand ??
                                                (_addInfratribusCommand = new RelayCommand(delegate { AddInfratribus(null); }));

        private RelayCommand _copyInfratribusCommand;

        public ICommand CopyInfratribusCommand => _copyInfratribusCommand ??
                                                 (_copyInfratribusCommand = new RelayCommand(delegate { CopyInfratribus(null); }));

        private RelayCommand _saveInfratribusCommand;

        public ICommand SaveInfratribusCommand => _saveInfratribusCommand ??
                                                 (_saveInfratribusCommand = new RelayCommand(delegate { SaveInfratribus(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearInfratribus(object o)
        {
            SearchInfratribusName = string.Empty;
            Tbl63InfratribussesList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        private void GetInfratribussesByNameOrId(object o)
        {
            Tbl63InfratribussesList = int.TryParse(SearchInfratribusName, out var id) ?
                new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63InfratribussesByInfratribusId(id)) :
                new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63InfratribussesByInfratribusName(SearchInfratribusName));

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        private void AddInfratribus(object o)      
        {
            Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus> {new Tbl63Infratribus
            {  
                InfratribusName = CultRes.StringsRes.DatasetNew,
                SubtribusID = CurrentTbl60Subtribus.SubtribusID
            }    };

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void CopyInfratribus(object o)
        {
            Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>();

            var infratribus = _businessLayer.SingleListTbl63InfratribussesByInfratribusId(CurrentTbl63Infratribus.InfratribusID);

            Tbl63InfratribussesList.Add(new Tbl63Infratribus
            {
                SubtribusID = infratribus.SubtribusID,
                InfratribusName = CultRes.StringsRes.DatasetNew,
                Valid =  infratribus.Valid,
                ValidYear =  infratribus.ValidYear,
                Synonym =  infratribus.Synonym,
                Author =  infratribus.Author,
                AuthorYear =  infratribus.AuthorYear,
                Info =  infratribus.Info,
                EngName =  infratribus.EngName,
                GerName =  infratribus.GerName,
                FraName =  infratribus.FraName,
                PorName =  infratribus.PorName,
                Memo =  infratribus.Memo
            });

            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void SaveInfratribus(object o)
        {
            try
            {
                var infratribus = _businessLayer.SingleListTbl63InfratribussesByInfratribusId(CurrentTbl63Infratribus.InfratribusID);
                if (CurrentTbl63Infratribus.InfratribusID != 0)
                {
                    if (infratribus != null) //update
                    {
                        infratribus.InfratribusName = CurrentTbl63Infratribus.InfratribusName;
                        infratribus.SubtribusID = CurrentTbl63Infratribus.SubtribusID;
                        infratribus.Valid = CurrentTbl63Infratribus.Valid;
                        infratribus.ValidYear = CurrentTbl63Infratribus.ValidYear;       
                        infratribus.Synonym = CurrentTbl63Infratribus.Synonym;
                        infratribus.Author = CurrentTbl63Infratribus.Author;
                        infratribus.AuthorYear = CurrentTbl63Infratribus.AuthorYear;
                        infratribus.Info = CurrentTbl63Infratribus.Info;
                        infratribus.EngName = CurrentTbl63Infratribus.EngName;
                        infratribus.GerName = CurrentTbl63Infratribus.GerName;
                        infratribus.FraName = CurrentTbl63Infratribus.FraName;
                        infratribus.PorName = CurrentTbl63Infratribus.PorName;
                        infratribus.Updater = Environment.UserName;
                        infratribus.UpdaterDate = DateTime.Now;
                        infratribus.Memo = CurrentTbl63Infratribus.Memo;
                        infratribus.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    infratribus = new Tbl63Infratribus   //add new
                    {
                        InfratribusName = CurrentTbl63Infratribus.InfratribusName,              
                        SubtribusID = CurrentTbl63Infratribus.SubtribusID,     
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl63Infratribus.Valid,
                        ValidYear = CurrentTbl63Infratribus.ValidYear,
                        Synonym = CurrentTbl63Infratribus.Synonym,
                        Author = CurrentTbl63Infratribus.Author,
                        AuthorYear = CurrentTbl63Infratribus.AuthorYear,
                        Info = CurrentTbl63Infratribus.Info,
                        EngName = CurrentTbl63Infratribus.EngName,
                        GerName = CurrentTbl63Infratribus.GerName,
                        FraName = CurrentTbl63Infratribus.FraName,
                        PorName = CurrentTbl63Infratribus.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl63Infratribus.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //SubtribusID may be not 0
                    if (CurrentTbl63Infratribus.SubtribusID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and SubtribusId already exist       
                    var dataset = _businessLayer.ListTbl63InfratribussesByInfratribusNameAndSubtribusId(CurrentTbl63Infratribus.InfratribusName, CurrentTbl63Infratribus.SubtribusID);

                    if (dataset.Count != 0 && CurrentTbl63Infratribus.InfratribusID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl63Infratribus.InfratribusName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl63Infratribus.InfratribusID == 0 ||
                        dataset.Count != 0 && CurrentTbl63Infratribus.InfratribusID != 0 ||
                        dataset.Count == 0 && CurrentTbl63Infratribus.InfratribusID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl63Infratribus.InfratribusName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateInfratribus(infratribus);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl63Infratribus.InfratribusName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl63Infratribus.InfratribusID == 0)  //new Dataset                        
                Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63InfratribussesByInfratribusName(CurrentTbl63Infratribus.InfratribusName));
            if (CurrentTbl63Infratribus.InfratribusID != 0)   //update 
                Tbl63InfratribussesList = new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63InfratribussesByInfratribusId(CurrentTbl63Infratribus.InfratribusID));

            SelectedMainTabIndex = 1;
            InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
            InfratribussesView.Refresh();
        }
        #endregion "Public Commands"                  
 
             

 //    Part 5    

 
                                       
 //    Part 6    

 
            

 //    Part 7    

 

 //    Part 8    

           
        #region "Public Commands Connect ==> Tbl90RefAuthor"
        //-------------------------------------------------------------------------
        private RelayCommand _clearReferenceAuthorCommand;
        public new ICommand ClearReferenceAuthorCommand => _clearReferenceAuthorCommand ??
                                                 (_clearReferenceAuthorCommand = new RelayCommand(delegate { ClearReferenceAuthor(null); }));

        private RelayCommand _getReferenceAuthorsByNameOrIdCommand;

        public new ICommand GetReferenceAuthorsByNameOrIdCommand => _getReferenceAuthorsByNameOrIdCommand ??
                                                            (_getReferenceAuthorsByNameOrIdCommand = new RelayCommand(delegate { GetReferenceAuthorsByNameOrId(null); }));

        private RelayCommand _addReferenceAuthorCommand;

        public new ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??
                                                    (_addReferenceAuthorCommand = new RelayCommand(delegate { AddReferenceAuthor(null); }));

        private RelayCommand _copyReferenceAuthorCommand;

        public new ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??
                        (_copyReferenceAuthorCommand = new RelayCommand(delegate { CopyReferenceAuthor(null); }));


        private RelayCommand _saveReferenceAuthorCommand;

        public new ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??
                     (_saveReferenceAuthorCommand = new RelayCommand(delegate { SaveReferenceAuthor(null); }));
        //-------------------------------------------------------------------------          
     
        private void ClearReferenceAuthor(object o)
        {
            SearchReferenceAuthorName = string.Empty;
            Tbl90ReferenceAuthorsList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        public new void GetReferenceAuthorsByNameOrId(object o)
        {

            Tbl90ReferenceAuthorsList = int.TryParse(SearchReferenceAuthorName, out int id) ? 
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(id)) : 
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceName(SearchReferenceAuthorName));

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
        }
        //----------------------------------------------------------------------            
     
        public new void AddReferenceAuthor(object o)
        {
            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference> { new Tbl90Reference
            {
                Info = CultRes.StringsRes.DatasetNew,
                SubtribusID = CurrentTbl60Subtribus.SubtribusID
            } };

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public new void CopyReferenceAuthor(object o)
        {
            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>();

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID);

            Tbl90ReferenceAuthorsList.Add(new Tbl90Reference
            {
                RefAuthorID = reference.RefAuthorID,
                SubtribusID = reference.SubtribusID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        public new void SaveReferenceAuthor(object o)
        {
            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID);
                if (CurrentTbl90ReferenceAuthor.ReferenceID != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertID = CurrentTbl90ReferenceAuthor.RefExpertID;
                        reference.RefAuthorID = CurrentTbl90ReferenceAuthor.RefAuthorID;
                        reference.RefSourceID = CurrentTbl90ReferenceAuthor.RefSourceID;
                        reference.RegnumID = CurrentTbl90ReferenceAuthor.RegnumID;
                        reference.PhylumID = CurrentTbl90ReferenceAuthor.PhylumID;
                        reference.DivisionID = CurrentTbl90ReferenceAuthor.DivisionID;
                        reference.SubphylumID = CurrentTbl90ReferenceAuthor.SubphylumID;
                        reference.SubdivisionID = CurrentTbl90ReferenceAuthor.SubdivisionID;
                        reference.SuperclassID = CurrentTbl90ReferenceAuthor.SuperclassID;
                        reference.ClassID = CurrentTbl90ReferenceAuthor.ClassID;
                        reference.SubclassID = CurrentTbl90ReferenceAuthor.SubclassID;
                        reference.InfraclassID = CurrentTbl90ReferenceAuthor.InfraclassID;
                        reference.LegioID = CurrentTbl90ReferenceAuthor.LegioID;
                        reference.OrdoID = CurrentTbl90ReferenceAuthor.OrdoID;
                        reference.SubordoID = CurrentTbl90ReferenceAuthor.SubordoID;
                        reference.InfraordoID = CurrentTbl90ReferenceAuthor.InfraordoID;
                        reference.SuperfamilyID = CurrentTbl90ReferenceAuthor.SuperfamilyID;
                        reference.FamilyID = CurrentTbl90ReferenceAuthor.FamilyID;
                        reference.SubfamilyID = CurrentTbl90ReferenceAuthor.SubfamilyID;
                        reference.InfrafamilyID = CurrentTbl90ReferenceAuthor.InfrafamilyID;
                        reference.SupertribusID = CurrentTbl90ReferenceAuthor.SupertribusID;
                        reference.TribusID = CurrentTbl90ReferenceAuthor.TribusID;
                        reference.SubtribusID = CurrentTbl90ReferenceAuthor.SubtribusID;
                        reference.InfratribusID = CurrentTbl90ReferenceAuthor.InfratribusID;
                        reference.GenusID = CurrentTbl90ReferenceAuthor.GenusID;
                        reference.PlSpeciesID = CurrentTbl90ReferenceAuthor.PlSpeciesID;
                        reference.FiSpeciesID = CurrentTbl90ReferenceAuthor.FiSpeciesID;
                        reference.Valid = CurrentTbl90ReferenceAuthor.Valid;
                        reference.ValidYear = CurrentTbl90ReferenceAuthor.ValidYear;
                        reference.Info = CurrentTbl90ReferenceAuthor.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = CurrentTbl90ReferenceAuthor.Memo;

                        reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    reference = new Tbl90Reference     //add new
                    {
                        RefAuthorID = CurrentTbl90ReferenceAuthor.RefAuthorID,
                        RefSourceID = CurrentTbl90ReferenceAuthor.RefSourceID,
                        RefExpertID = CurrentTbl90ReferenceAuthor.RefExpertID,
                        RegnumID = CurrentTbl90ReferenceAuthor.RegnumID,
                        PhylumID = CurrentTbl90ReferenceAuthor.PhylumID,
                        DivisionID = CurrentTbl90ReferenceAuthor.DivisionID,
                        SubphylumID = CurrentTbl90ReferenceAuthor.SubphylumID,
                        SubdivisionID = CurrentTbl90ReferenceAuthor.SubdivisionID,
                        SuperclassID = CurrentTbl90ReferenceAuthor.SuperclassID,
                        ClassID = CurrentTbl90ReferenceAuthor.ClassID,
                        SubclassID = CurrentTbl90ReferenceAuthor.SubclassID,
                        InfraclassID = CurrentTbl90ReferenceAuthor.InfraclassID,
                        LegioID = CurrentTbl90ReferenceAuthor.LegioID,
                        OrdoID = CurrentTbl90ReferenceAuthor.OrdoID,
                        SubordoID = CurrentTbl90ReferenceAuthor.SubordoID,
                        InfraordoID = CurrentTbl90ReferenceAuthor.InfraordoID,
                        SuperfamilyID = CurrentTbl90ReferenceAuthor.SuperfamilyID,
                        FamilyID = CurrentTbl90ReferenceAuthor.FamilyID,
                        SubfamilyID = CurrentTbl90ReferenceAuthor.SubfamilyID,
                        InfrafamilyID = CurrentTbl90ReferenceAuthor.InfrafamilyID,
                        SupertribusID = CurrentTbl90ReferenceAuthor.SupertribusID,
                        TribusID = CurrentTbl90ReferenceAuthor.TribusID,
                        SubtribusID = CurrentTbl90ReferenceAuthor.SubtribusID,
                        InfratribusID = CurrentTbl90ReferenceAuthor.InfratribusID,
                        GenusID = CurrentTbl90ReferenceAuthor.GenusID,
                        PlSpeciesID = CurrentTbl90ReferenceAuthor.PlSpeciesID,
                        FiSpeciesID = CurrentTbl90ReferenceAuthor.FiSpeciesID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl90ReferenceAuthor.Valid,
                        ValidYear = CurrentTbl90ReferenceAuthor.ValidYear,
                        Info = CurrentTbl90ReferenceAuthor.Info,
                        Memo = CurrentTbl90ReferenceAuthor.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //RefExpertID or RefSourceID or RefAuthorID may be not 0
                    if (CurrentTbl90ReferenceAuthor.RefExpertID == null && CurrentTbl90ReferenceAuthor.RefSourceID == null && CurrentTbl90ReferenceAuthor.RefAuthorID == null)
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with vb-name already exist   
                    var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceAuthor);

                    if (dataset.Count != 0 && CurrentTbl90ReferenceAuthor.ReferenceID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceAuthor.ReferenceID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceAuthor.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceAuthor.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceAuthor.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceAuthor.ReferenceID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateReference(reference);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90ReferenceAuthor.ReferenceID.ToString(),
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl90ReferenceAuthor.ReferenceID == 0)  //new Dataset                        
                Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByInfo(CurrentTbl90ReferenceAuthor.Info));
            if (CurrentTbl90ReferenceAuthor.ReferenceID != 0)   //update 
                Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID));

            SelectedMainSubRefTabIndex = 2;

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion "Public Commands"                  
           
        #region "Public Commands Connect ==> Tbl90RefSource" 
        //-------------------------------------------------------------------------
        private RelayCommand _clearReferenceSourceCommand;
        public new ICommand ClearReferenceSourceCommand => _clearReferenceSourceCommand ??
                                                       (_clearReferenceSourceCommand = new RelayCommand(delegate { ClearReferenceSource(null); }));

        private RelayCommand _getReferenceSourcesByNameOrIdCommand;

        public new ICommand GetReferenceSourcesByNameOrIdCommand => _getReferenceSourcesByNameOrIdCommand ??
                                                            (_getReferenceSourcesByNameOrIdCommand = new RelayCommand(delegate { GetReferenceSourcesByNameOrId(null); }));

        private RelayCommand _addReferenceSourceCommand;

        public new ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??
                                                    (_addReferenceSourceCommand = new RelayCommand(delegate { AddReferenceSource(null); }));

        private RelayCommand _copyReferenceSourceCommand;

        public new ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??
                        (_copyReferenceSourceCommand = new RelayCommand(delegate { CopyReferenceSource(null); }));


        private RelayCommand _saveReferenceSourceCommand;

        public new ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??
                     (_saveReferenceSourceCommand = new RelayCommand(delegate { SaveReferenceSource(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearReferenceSource(object o)
        {
            SearchReferenceSourceName = string.Empty;
            Tbl90ReferenceSourcesList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        public new void GetReferenceSourcesByNameOrId(object o)
        {
            Tbl90ReferenceSourcesList = int.TryParse(SearchReferenceSourceName, out var id) ?
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(id)) :
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceName(SearchReferenceSourceName));


            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        public new void AddReferenceSource(object o)
        {
            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference> { new Tbl90Reference
            {
                Info = CultRes.StringsRes.DatasetNew,
                SubtribusID = CurrentTbl60Subtribus.SubtribusID
            } };

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public new void CopyReferenceSource(object o)
        {

            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>();

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);

            Tbl90ReferenceSourcesList.Add(new Tbl90Reference
            {
                RefSourceID = reference.RefSourceID,
                SubtribusID = reference.SubtribusID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        public new void SaveReferenceSource(object o)
        {
            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);
                if (CurrentTbl90ReferenceSource.ReferenceID != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertID = CurrentTbl90ReferenceSource.RefExpertID;
                        reference.RefAuthorID = CurrentTbl90ReferenceSource.RefAuthorID;
                        reference.RefSourceID = CurrentTbl90ReferenceSource.RefSourceID;
                        reference.RegnumID = CurrentTbl90ReferenceSource.RegnumID;
                        reference.PhylumID = CurrentTbl90ReferenceSource.PhylumID;
                        reference.DivisionID = CurrentTbl90ReferenceSource.DivisionID;
                        reference.SubphylumID = CurrentTbl90ReferenceSource.SubphylumID;
                        reference.SubdivisionID = CurrentTbl90ReferenceSource.SubdivisionID;
                        reference.SuperclassID = CurrentTbl90ReferenceSource.SuperclassID;
                        reference.ClassID = CurrentTbl90ReferenceSource.ClassID;
                        reference.SubclassID = CurrentTbl90ReferenceSource.SubclassID;
                        reference.InfraclassID = CurrentTbl90ReferenceSource.InfraclassID;
                        reference.LegioID = CurrentTbl90ReferenceSource.LegioID;
                        reference.OrdoID = CurrentTbl90ReferenceSource.OrdoID;
                        reference.SubordoID = CurrentTbl90ReferenceSource.SubordoID;
                        reference.InfraordoID = CurrentTbl90ReferenceSource.InfraordoID;
                        reference.SuperfamilyID = CurrentTbl90ReferenceSource.SuperfamilyID;
                        reference.FamilyID = CurrentTbl90ReferenceSource.FamilyID;
                        reference.SubfamilyID = CurrentTbl90ReferenceSource.SubfamilyID;
                        reference.InfrafamilyID = CurrentTbl90ReferenceSource.InfrafamilyID;
                        reference.SupertribusID = CurrentTbl90ReferenceSource.SupertribusID;
                        reference.TribusID = CurrentTbl90ReferenceSource.TribusID;
                        reference.SubtribusID = CurrentTbl90ReferenceSource.SubtribusID;
                        reference.InfratribusID = CurrentTbl90ReferenceSource.InfratribusID;
                        reference.GenusID = CurrentTbl90ReferenceSource.GenusID;
                        reference.PlSpeciesID = CurrentTbl90ReferenceSource.PlSpeciesID;
                        reference.FiSpeciesID = CurrentTbl90ReferenceSource.FiSpeciesID;
                        reference.Valid = CurrentTbl90ReferenceSource.Valid;
                        reference.ValidYear = CurrentTbl90ReferenceSource.ValidYear;
                        reference.Info = CurrentTbl90ReferenceSource.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = CurrentTbl90ReferenceSource.Memo;

                        reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    reference = new Tbl90Reference     //add new
                    {
                        RefAuthorID = CurrentTbl90ReferenceSource.RefAuthorID,
                        RefSourceID = CurrentTbl90ReferenceSource.RefSourceID,
                        RefExpertID = CurrentTbl90ReferenceSource.RefExpertID,
                        RegnumID = CurrentTbl90ReferenceSource.RegnumID,
                        PhylumID = CurrentTbl90ReferenceSource.PhylumID,
                        DivisionID = CurrentTbl90ReferenceSource.DivisionID,
                        SubphylumID = CurrentTbl90ReferenceSource.SubphylumID,
                        SubdivisionID = CurrentTbl90ReferenceSource.SubdivisionID,
                        SuperclassID = CurrentTbl90ReferenceSource.SuperclassID,
                        ClassID = CurrentTbl90ReferenceSource.ClassID,
                        SubclassID = CurrentTbl90ReferenceSource.SubclassID,
                        InfraclassID = CurrentTbl90ReferenceSource.InfraclassID,
                        LegioID = CurrentTbl90ReferenceSource.LegioID,
                        OrdoID = CurrentTbl90ReferenceSource.OrdoID,
                        SubordoID = CurrentTbl90ReferenceSource.SubordoID,
                        InfraordoID = CurrentTbl90ReferenceSource.InfraordoID,
                        SuperfamilyID = CurrentTbl90ReferenceSource.SuperfamilyID,
                        FamilyID = CurrentTbl90ReferenceSource.FamilyID,
                        SubfamilyID = CurrentTbl90ReferenceSource.SubfamilyID,
                        InfrafamilyID = CurrentTbl90ReferenceSource.InfrafamilyID,
                        SupertribusID = CurrentTbl90ReferenceSource.SupertribusID,
                        TribusID = CurrentTbl90ReferenceSource.TribusID,
                        SubtribusID = CurrentTbl90ReferenceSource.SubtribusID,
                        InfratribusID = CurrentTbl90ReferenceSource.InfratribusID,
                        GenusID = CurrentTbl90ReferenceSource.GenusID,
                        PlSpeciesID = CurrentTbl90ReferenceSource.PlSpeciesID,
                        FiSpeciesID = CurrentTbl90ReferenceSource.FiSpeciesID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl90ReferenceSource.Valid,
                        ValidYear = CurrentTbl90ReferenceSource.ValidYear,
                        Info = CurrentTbl90ReferenceSource.Info,
                        Memo = CurrentTbl90ReferenceSource.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //RefExpertID or RefSourceID or RefAuthorID may be not 0
                    if (CurrentTbl90ReferenceSource.RefExpertID == null && CurrentTbl90ReferenceSource.RefSourceID == null && CurrentTbl90ReferenceSource.RefAuthorID == null)
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with vb-name already exist   
                    var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceSource);

                    if (dataset.Count != 0 && CurrentTbl90ReferenceSource.ReferenceID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceSource.ReferenceID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceSource.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceSource.ReferenceID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateReference(reference);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90ReferenceSource.ReferenceID.ToString(),
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl90ReferenceSource.ReferenceID == 0)  //new Dataset                        
                Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByInfo(CurrentTbl90ReferenceSource.Info));
            if (CurrentTbl90ReferenceSource.ReferenceID != 0)   //update 
                Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID));

            SelectedMainSubRefTabIndex = 1;

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }
        #endregion "Public Commands"                  
           
        #region "Public Commands Connect ==> Tbl90RefExpert"

        //-------------------------------------------------------------------------
        private RelayCommand _clearReferenceExpertCommand;
        public new ICommand ClearReferenceExpertCommand => _clearReferenceExpertCommand ??
                                                       (_clearReferenceExpertCommand = new RelayCommand(delegate { ClearReferenceExpert(null); }));

        private RelayCommand _getReferenceExpertsByNameOrIdCommand;

        public new ICommand GetReferenceExpertsByNameOrIdCommand => _getReferenceExpertsByNameOrIdCommand ??
                                                            (_getReferenceExpertsByNameOrIdCommand = new RelayCommand(delegate { GetReferenceExpertsByNameOrId(null); }));

        private RelayCommand _addReferenceExpertCommand;

        public new ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??
                                                    (_addReferenceExpertCommand = new RelayCommand(delegate { AddReferenceExpert(null); }));

        private RelayCommand _copyReferenceExpertCommand;

        public new ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??
                        (_copyReferenceExpertCommand = new RelayCommand(delegate { CopyReferenceExpert(null); }));


        private RelayCommand _saveReferenceExpertCommand;

        public new ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??
                     (_saveReferenceExpertCommand = new RelayCommand(delegate { SaveReferenceExpert(null); }));
        //-------------------------------------------------------------------------          
     
        private void ClearReferenceExpert(object o)
        {
            SearchReferenceExpertName = string.Empty;
            Tbl90ReferenceExpertsList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        public new void GetReferenceExpertsByNameOrId(object o)
        {
            Tbl90ReferenceExpertsList = int.TryParse(SearchReferenceExpertName, out var id) ?
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(id)) :
                new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceName(SearchReferenceExpertName));

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
         }
        //----------------------------------------------------------------------            
     
        public new void AddReferenceExpert(object o)
        {
            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference> { new Tbl90Reference
            {
                Info = CultRes.StringsRes.DatasetNew,
                SubtribusID = CurrentTbl60Subtribus.SubtribusID
            } };

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public new void CopyReferenceExpert(object o)
        {
            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>();

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);

            Tbl90ReferenceExpertsList.Add(new Tbl90Reference
            {
                RefExpertID = reference.RefExpertID,
                SubtribusID = reference.SubtribusID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        public new void SaveReferenceExpert(object o)
        {
            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);
                if (CurrentTbl90ReferenceExpert.ReferenceID != 0)
                {
                    if (reference != null) //update
                    {
                        reference.RefExpertID = CurrentTbl90ReferenceExpert.RefExpertID;
                        reference.RefAuthorID = CurrentTbl90ReferenceExpert.RefAuthorID;
                        reference.RefSourceID = CurrentTbl90ReferenceExpert.RefSourceID;
                        reference.RegnumID = CurrentTbl90ReferenceExpert.RegnumID;
                        reference.PhylumID = CurrentTbl90ReferenceExpert.PhylumID;
                        reference.DivisionID = CurrentTbl90ReferenceExpert.DivisionID;
                        reference.SubphylumID = CurrentTbl90ReferenceExpert.SubphylumID;
                        reference.SubdivisionID = CurrentTbl90ReferenceExpert.SubdivisionID;
                        reference.SuperclassID = CurrentTbl90ReferenceExpert.SuperclassID;
                        reference.ClassID = CurrentTbl90ReferenceExpert.ClassID;
                        reference.SubclassID = CurrentTbl90ReferenceExpert.SubclassID;
                        reference.InfraclassID = CurrentTbl90ReferenceExpert.InfraclassID;
                        reference.LegioID = CurrentTbl90ReferenceExpert.LegioID;
                        reference.OrdoID = CurrentTbl90ReferenceExpert.OrdoID;
                        reference.SubordoID = CurrentTbl90ReferenceExpert.SubordoID;
                        reference.InfraordoID = CurrentTbl90ReferenceExpert.InfraordoID;
                        reference.SuperfamilyID = CurrentTbl90ReferenceExpert.SuperfamilyID;
                        reference.FamilyID = CurrentTbl90ReferenceExpert.FamilyID;
                        reference.SubfamilyID = CurrentTbl90ReferenceExpert.SubfamilyID;
                        reference.InfrafamilyID = CurrentTbl90ReferenceExpert.InfrafamilyID;
                        reference.SupertribusID = CurrentTbl90ReferenceExpert.SupertribusID;
                        reference.TribusID = CurrentTbl90ReferenceExpert.TribusID;
                        reference.SubtribusID = CurrentTbl90ReferenceExpert.SubtribusID;
                        reference.InfratribusID = CurrentTbl90ReferenceExpert.InfratribusID;
                        reference.GenusID = CurrentTbl90ReferenceExpert.GenusID;
                        reference.PlSpeciesID = CurrentTbl90ReferenceExpert.PlSpeciesID;
                        reference.FiSpeciesID = CurrentTbl90ReferenceExpert.FiSpeciesID;
                        reference.Valid = CurrentTbl90ReferenceExpert.Valid;
                        reference.ValidYear = CurrentTbl90ReferenceExpert.ValidYear;
                        reference.Info = CurrentTbl90ReferenceExpert.Info;
                        reference.Updater = Environment.UserName;
                        reference.UpdaterDate = DateTime.Now;
                        reference.Memo = CurrentTbl90ReferenceExpert.Memo;

                        reference.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    reference = new Tbl90Reference     //add new
                    {
                        RefAuthorID = CurrentTbl90ReferenceExpert.RefAuthorID,
                        RefSourceID = CurrentTbl90ReferenceExpert.RefSourceID,
                        RefExpertID = CurrentTbl90ReferenceExpert.RefExpertID,
                        RegnumID = CurrentTbl90ReferenceExpert.RegnumID,
                        PhylumID = CurrentTbl90ReferenceExpert.PhylumID,
                        DivisionID = CurrentTbl90ReferenceExpert.DivisionID,
                        SubphylumID = CurrentTbl90ReferenceExpert.SubphylumID,
                        SubdivisionID = CurrentTbl90ReferenceExpert.SubdivisionID,
                        SuperclassID = CurrentTbl90ReferenceExpert.SuperclassID,
                        ClassID = CurrentTbl90ReferenceExpert.ClassID,
                        SubclassID = CurrentTbl90ReferenceExpert.SubclassID,
                        InfraclassID = CurrentTbl90ReferenceExpert.InfraclassID,
                        LegioID = CurrentTbl90ReferenceExpert.LegioID,
                        OrdoID = CurrentTbl90ReferenceExpert.OrdoID,
                        SubordoID = CurrentTbl90ReferenceExpert.SubordoID,
                        InfraordoID = CurrentTbl90ReferenceExpert.InfraordoID,
                        SuperfamilyID = CurrentTbl90ReferenceExpert.SuperfamilyID,
                        FamilyID = CurrentTbl90ReferenceExpert.FamilyID,
                        SubfamilyID = CurrentTbl90ReferenceExpert.SubfamilyID,
                        InfrafamilyID = CurrentTbl90ReferenceExpert.InfrafamilyID,
                        SupertribusID = CurrentTbl90ReferenceExpert.SupertribusID,
                        TribusID = CurrentTbl90ReferenceExpert.TribusID,
                        SubtribusID = CurrentTbl90ReferenceExpert.SubtribusID,
                        InfratribusID = CurrentTbl90ReferenceExpert.InfratribusID,
                        GenusID = CurrentTbl90ReferenceExpert.GenusID,
                        PlSpeciesID = CurrentTbl90ReferenceExpert.PlSpeciesID,
                        FiSpeciesID = CurrentTbl90ReferenceExpert.FiSpeciesID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl90ReferenceExpert.Valid,
                        ValidYear = CurrentTbl90ReferenceExpert.ValidYear,
                        Info = CurrentTbl90ReferenceExpert.Info,
                        Memo = CurrentTbl90ReferenceExpert.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //RefExpertID or RefSourceID or RefAuthorID may be not 0
                    if (CurrentTbl90ReferenceExpert.RefExpertID == null && CurrentTbl90ReferenceExpert.RefSourceID == null && CurrentTbl90ReferenceExpert.RefAuthorID == null)
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with vb-name already exist   
                    var dataset = _businessLayer.ListTbl90ReferencesByRefExpertIdAndRefSourceIdAndRefAuthorIdAndInfo(CurrentTbl90ReferenceExpert);

                    if (dataset.Count != 0 && CurrentTbl90ReferenceExpert.ReferenceID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl90ReferenceExpert.ReferenceID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceExpert.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceExpert.ReferenceID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateReference(reference);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl90ReferenceExpert.ReferenceID.ToString(),
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl90ReferenceExpert.ReferenceID == 0)  //new Dataset                        
                Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByInfo(CurrentTbl90ReferenceExpert.Info));
            if (CurrentTbl90ReferenceExpert.ReferenceID != 0)   //update 
                Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID));

            SelectedMainSubRefTabIndex = 0;

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }
        #endregion "Public Commands"                  
           
        #region "Public Commands Connect ==> Tbl93Comment"

        //-------------------------------------------------------------------------
        private RelayCommand _clearCommentCommand;
        public new ICommand ClearCommentCommand => _clearCommentCommand ??
                                               (_clearCommentCommand = new RelayCommand(delegate { ClearComment(null); }));

        private RelayCommand _getCommentsByNameOrIdCommand;

        public new ICommand GetCommentsByNameOrIdCommand => _getCommentsByNameOrIdCommand ??
                                                            (_getCommentsByNameOrIdCommand = new RelayCommand(delegate { GetCommentsByNameOrId(null); }));

        private RelayCommand _addCommentCommand;

        public new ICommand AddCommentCommand => _addCommentCommand ??
                                                 (_addCommentCommand = new RelayCommand(delegate { AddComment(null); }));

        private RelayCommand _copyCommentCommand;

        public new ICommand CopyCommentCommand => _copyCommentCommand ??
                                                  (_copyCommentCommand = new RelayCommand(delegate { CopyComment(null); }));

        private RelayCommand _saveCommentCommand;

        public new ICommand SaveCommentCommand => _saveCommentCommand ??
                                                  (_saveCommentCommand = new RelayCommand(delegate { SaveComment(null); }));
        //-------------------------------------------------------------------------          
     
        private void ClearComment(object o)
        {
            SearchCommentInfo = string.Empty;
            Tbl93CommentsList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        public new void GetCommentsByNameOrId(object o)
        {
            Tbl93CommentsList = int.TryParse(SearchCommentInfo, out int id) ? 
                new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByCommentId(id)) : 
                new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByInfo(SearchCommentInfo));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
        }            
        //----------------------------------------------------------------------            
     
        public  new void AddComment(object o)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment> { new Tbl93Comment
            {
                Info = CultRes.StringsRes.DatasetNew,
                SubtribusID = CurrentTbl60Subtribus.SubtribusID
            } };

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public new void CopyComment(object o)
        {
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();

            var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);

            Tbl93CommentsList.Add(new Tbl93Comment
            {
                SubtribusID = comment.SubtribusID,
                Valid = comment.Valid,
                ValidYear = comment.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = comment.Memo
            });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void SaveComment(object o)
        {
            try
            {
                var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);
                if (CurrentTbl93Comment.CommentID != 0)
                {
                    if (comment != null) //update
                    {
                        comment.RegnumID = CurrentTbl93Comment.RegnumID;
                        comment.PhylumID = CurrentTbl93Comment.PhylumID;
                        comment.DivisionID = CurrentTbl93Comment.DivisionID;
                        comment.SubphylumID = CurrentTbl93Comment.SubphylumID;
                        comment.SubdivisionID = CurrentTbl93Comment.SubdivisionID;
                        comment.SuperclassID = CurrentTbl93Comment.SuperclassID;
                        comment.ClassID = CurrentTbl93Comment.ClassID;
                        comment.SubclassID = CurrentTbl93Comment.SubclassID;
                        comment.InfraclassID = CurrentTbl93Comment.InfraclassID;
                        comment.LegioID = CurrentTbl93Comment.LegioID;
                        comment.OrdoID = CurrentTbl93Comment.OrdoID;
                        comment.SubordoID = CurrentTbl93Comment.SubordoID;
                        comment.InfraordoID = CurrentTbl93Comment.InfraordoID;
                        comment.SuperfamilyID = CurrentTbl93Comment.SuperfamilyID;
                        comment.FamilyID = CurrentTbl93Comment.FamilyID;
                        comment.SubfamilyID = CurrentTbl93Comment.SubfamilyID;
                        comment.InfrafamilyID = CurrentTbl93Comment.InfrafamilyID;
                        comment.SupertribusID = CurrentTbl93Comment.SupertribusID;
                        comment.TribusID = CurrentTbl93Comment.TribusID;
                        comment.SubtribusID = CurrentTbl93Comment.SubtribusID;
                        comment.InfratribusID = CurrentTbl93Comment.InfratribusID;
                        comment.GenusID = CurrentTbl93Comment.GenusID;
                        comment.PlSpeciesID = CurrentTbl93Comment.PlSpeciesID;
                        comment.FiSpeciesID = CurrentTbl93Comment.FiSpeciesID;
                        comment.Valid = CurrentTbl93Comment.Valid;
                        comment.ValidYear = CurrentTbl93Comment.ValidYear;
                        comment.Info = CurrentTbl93Comment.Info;
                        comment.Memo = CurrentTbl93Comment.Memo;
                        comment.Updater = Environment.UserName;
                        comment.UpdaterDate = DateTime.Now;
                        comment.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    comment = new Tbl93Comment     //add new
                    {
                        RegnumID = CurrentTbl93Comment.RegnumID,
                        PhylumID = CurrentTbl93Comment.PhylumID,
                        DivisionID = CurrentTbl93Comment.DivisionID,
                        SubphylumID = CurrentTbl93Comment.SubphylumID,
                        SubdivisionID = CurrentTbl93Comment.SubdivisionID,
                        SuperclassID = CurrentTbl93Comment.SuperclassID,
                        ClassID = CurrentTbl93Comment.ClassID,
                        SubclassID = CurrentTbl93Comment.SubclassID,
                        InfraclassID = CurrentTbl93Comment.InfraclassID,
                        LegioID = CurrentTbl93Comment.LegioID,
                        OrdoID = CurrentTbl93Comment.OrdoID,
                        SubordoID = CurrentTbl93Comment.SubordoID,
                        InfraordoID = CurrentTbl93Comment.InfraordoID,
                        SuperfamilyID = CurrentTbl93Comment.SuperfamilyID,
                        FamilyID = CurrentTbl93Comment.FamilyID,
                        SubfamilyID = CurrentTbl93Comment.SubfamilyID,
                        InfrafamilyID = CurrentTbl93Comment.InfrafamilyID,
                        SupertribusID = CurrentTbl93Comment.SupertribusID,
                        TribusID = CurrentTbl93Comment.TribusID,
                        SubtribusID = CurrentTbl93Comment.SubtribusID,
                        InfratribusID = CurrentTbl93Comment.InfratribusID,
                        GenusID = CurrentTbl93Comment.GenusID,
                        PlSpeciesID = CurrentTbl93Comment.PlSpeciesID,
                        FiSpeciesID = CurrentTbl93Comment.FiSpeciesID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl93Comment.Valid,
                        ValidYear = CurrentTbl93Comment.ValidYear,
                        Info = CurrentTbl93Comment.Info,
                        Memo = CurrentTbl93Comment.Memo,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //check if dataset with Name and VbIds already exist       
                    var dataset = _businessLayer.ListTbl93CommentsByCurrentItem(CurrentTbl93Comment);

                    if (dataset.Count != 0 && CurrentTbl93Comment.CommentID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl93Comment.Info,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }

                    if (dataset.Count == 0 && CurrentTbl93Comment.CommentID == 0 ||
                        dataset.Count != 0 && CurrentTbl93Comment.CommentID != 0 ||
                        dataset.Count == 0 && CurrentTbl93Comment.CommentID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl93Comment.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateComment(comment);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl93Comment.Info,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl93Comment.CommentID == 0)  //new Dataset                        
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByInfo(CurrentTbl93Comment.Info));
            if (CurrentTbl93Comment.CommentID != 0)   //update 
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID));

            SelectedMainTabIndex = 3;
            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        #endregion "Public Commands"                  
 
            
 //    Part 9    

      
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public new ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??
                                                         (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); }));

        private void GetConnectedTablesById(object o)
        {
            Tbl63InfratribussesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();

            Tbl57TribussesList =  new ObservableCollection<Tbl57Tribus>(
                    _businessLayer.ListTbl57TribussesByTribusId(CurrentTbl60Subtribus.TribusID));
 

            TribussesView = CollectionViewSource.GetDefaultView(Tbl57TribussesList);
            TribussesView.Refresh();

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

        public new int SelectedMainTabIndex
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
                if (_selectedMainTabIndex == 2)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 2;
                }
                if (_selectedMainTabIndex == 3)
                {
                    SelectedDetailTabIndex = 1;
                    SelectedDetailSubTabIndex = 3;
                }
            }
        }

        public new int SelectedMainSubRefTabIndex
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

        public new int SelectedDetailTabIndex
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
                }
                if (_selectedDetailTabIndex == 1)                
                    SelectedDetailSubTabIndex = 1;                
                if (_selectedDetailTabIndex == 2)                
                    SelectedDetailSubTabIndex = 2;               
                if (_selectedDetailTabIndex == 3)
                    SelectedDetailSubTabIndex = 3;
            }
        }

        public new int SelectedDetailSubTabIndex
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
                    Tbl63InfratribussesList =  new ObservableCollection<Tbl63Infratribus>(
                        _businessLayer.ListTbl63InfratribussesBySubtribusId(CurrentTbl60Subtribus.SubtribusID));

                    InfratribussesView = CollectionViewSource.GetDefaultView(Tbl63InfratribussesList);
                    InfratribussesView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                        _businessLayer.ListTbl90RefExperts());
                    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefExpertsBySubtribusId(CurrentTbl60Subtribus.SubtribusID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainTabIndex = 2;
                }
                if (_selectedDetailSubTabIndex == 3)
                {
                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(
                        _businessLayer.ListTbl93CommentsBySubtribusId(CurrentTbl60Subtribus.SubtribusID));

                    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                    CommentsView.Refresh();

                    SelectedMainTabIndex = 3;
                }
            }
        }

        public new int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex;
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value;
                RaisePropertyChanged();
                if (_selectedDetailSubRefTabIndex == 0)
                {
                    Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                        _businessLayer.ListTbl90RefExperts());
                    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefExpertsBySubtribusId(CurrentTbl60Subtribus.SubtribusID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(
                        _businessLayer.ListTbl90RefSources());

                    Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefSourcesBySubtribusId(CurrentTbl60Subtribus.SubtribusID));

                    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                    ReferenceSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(
                        _businessLayer.ListTbl90RefAuthors());

                    Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefAuthorsBySubtribusId(CurrentTbl60Subtribus.SubtribusID));

                    ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                    ReferenceAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"
 

 //    Part 11    

     
        #region "Public Properties Tbl60Subtribus"

        private string _searchSubtribusName = string.Empty;
        public string SearchSubtribusName
        {
            get => _searchSubtribusName; 
            set { _searchSubtribusName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView SubtribussesView;
        private   Tbl60Subtribus CurrentTbl60Subtribus => SubtribussesView?.CurrentItem as Tbl60Subtribus;

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesList;
        public  ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesList
        {
            get => _tbl60SubtribussesList; 
            set {  _tbl60SubtribussesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public  ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList; 
            set {  _tbl60SubtribussesAllList = value; RaisePropertyChanged();   }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl57Tribus"

        private string _searchTribusName = string.Empty;
        public new string SearchTribusName
        {
            get  => _searchTribusName; 
            set { _searchTribusName = value; RaisePropertyChanged(); }
        }

        public new ICollectionView TribussesView;
        private Tbl57Tribus CurrentTbl57Tribus => TribussesView?.CurrentItem as Tbl57Tribus;           

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesList;
        public new ObservableCollection<Tbl57Tribus> Tbl57TribussesList
        {
            get => _tbl57TribussesList; 
            set { _tbl57TribussesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public  ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList; 
            set { _tbl57TribussesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl63Infratribus"

        private string _searchInfratribusName = string.Empty;
        public string SearchInfratribusName
        {
            get => _searchInfratribusName; 
            set { _searchInfratribusName = value; RaisePropertyChanged(); }
        }

        public ICollectionView InfratribussesView;
        private Tbl63Infratribus CurrentTbl63Infratribus => InfratribussesView?.CurrentItem as Tbl63Infratribus;           

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesList;
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesList
        {
            get => _tbl63InfratribussesList; 
            set { _tbl63InfratribussesList = value; RaisePropertyChanged(); }
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
         
        #region "Public Properties Tbl93Comment"

        private string _searchCommentInfo = string.Empty;
        public new string SearchCommentInfo
        {
            get => _searchCommentInfo; 
            set { _searchCommentInfo = value; RaisePropertyChanged(); }
        }
        public new ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public new ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList; 
            set { _tbl93CommentsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"     
         
        #region "Public Properties Tbl93Comment"

        private string _searchCommentInfo = string.Empty;
        public new string SearchCommentInfo
        {
            get => _searchCommentInfo; 
            set { _searchCommentInfo = value; RaisePropertyChanged(); }
        }
        public new ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public new ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList; 
            set { _tbl93CommentsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"     
 

 



   }
}   
