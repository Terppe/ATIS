using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.BusinessLayer;
using Te.Atis.Ui.Desktop.Domain;
using Te.Atis.Ui.Desktop.Domain.Helper;
using Te.Atis.Ui.Desktop.MessageBox;    

    
         //    Tbl18SuperclassesViewModel Skriptdatum:  07.11.2018  12:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl18SuperclassesViewModel : ViewModelBase                     
    {     
         
        #region "Private Data Members"
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;
        private int _position;   
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl18SuperclassesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {    
        
                // Code runs "for real" 
                _entityException = new DbEntityException();
            }
        }     
        #endregion "Constructor"         
 

 //    Part 1    

             
        #region "Public Commands Basic Tbl18Superclass"
        //-------------------------------------------------------------------------
        private RelayCommand _clearSuperclassCommand;

        public ICommand ClearSuperclassCommand => _clearSuperclassCommand ??
                                                  (_clearSuperclassCommand = new RelayCommand(delegate { ClearSuperclass(null); }));         
             
        private RelayCommand _getSuperclassesByNameOrIdCommand;  

        public  ICommand GetSuperclassesByNameOrIdCommand => _getSuperclassesByNameOrIdCommand ??
                                                           (_getSuperclassesByNameOrIdCommand = new RelayCommand(delegate { GetSuperclassesByNameOrId(null); }));        
             
        private RelayCommand _addSuperclassCommand;

        public ICommand AddSuperclassCommand => _addSuperclassCommand ??
                                                (_addSuperclassCommand = new RelayCommand(delegate { AddSuperclass(null); }));

        private RelayCommand _copySuperclassCommand;

        public ICommand CopySuperclassCommand => _copySuperclassCommand ??
                                                 (_copySuperclassCommand = new RelayCommand(delegate { CopySuperclass(null); }));      
             
        private RelayCommand _deleteSuperclassCommand;

        public ICommand DeleteSuperclassCommand => _deleteSuperclassCommand ??
                                                   (_deleteSuperclassCommand = new RelayCommand(delegate { DeleteSuperclass(null); }));    
             
        private RelayCommand _saveSuperclassCommand;

        public ICommand SaveSuperclassCommand => _saveSuperclassCommand ??
                                                 (_saveSuperclassCommand = new RelayCommand(delegate { SaveSuperclass(null); }));
        //-------------------------------------------------------------------------          
     
        private void ClearSuperclass(object o)
        {
            SearchSuperclassName = "";

            SelectedMainTabIndex = 0;  //change tab
            SelectedDetailTabIndex = 0;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;

            Tbl12SubphylumsList?.Clear();
            Tbl18SuperclassesList?.Clear();
            Tbl21ClassesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();
        }
        //----------------------------------------------------------------------                  
        
        private void GetSuperclassesByNameOrId(object o)
        {
            if (SearchSuperclassName != "")
            {
                Tbl18SuperclassesList?.Clear();
                if (SearchSuperclassName == "*") // show whole table
                {
                    SearchSuperclassName = "";
                    _businessLayer = new BusinessLayer.BusinessLayer();
                        Tbl12SubphylumsAllList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12Subphylums());
                        Tbl15SubdivisionsAllList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15Subdivisions());
                    Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassName(SearchSuperclassName));
                    SearchSuperclassName = "*";
                }
                else
                {
                     _businessLayer = new BusinessLayer.BusinessLayer();
                        Tbl12SubphylumsAllList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12Subphylums());
                        Tbl15SubdivisionsAllList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15Subdivisions());
                   Tbl18SuperclassesList = int.TryParse(SearchSuperclassName, out var id) ?
                        new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassId(id)) :
                        new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassName(SearchSuperclassName));
                }

                if (Tbl18SuperclassesList.Count == 0)
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Tables, CultRes.StringsRes.DatasetNot,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    Tbl12SubphylumsList?.Clear();
                    Tbl15SubdivisionsList?.Clear();
                    Tbl21ClassesList?.Clear();
                    Tbl90ReferenceExpertsList?.Clear();
                    Tbl90ReferenceSourcesList?.Clear();
                    Tbl90ReferenceAuthorsList?.Clear();
                    Tbl93CommentsList?.Clear();
                }
            }
            else
            {
                WpfMessageBox.Show(CultRes.StringsRes.SearchNameOrId, CultRes.StringsRes.InputRequested,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.MoveCurrentToLast();
            SuperclassesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                          
        
        private void AddSuperclass(object o)
        {
            if (Tbl18SuperclassesList == null)
                Tbl18SuperclassesList =  new ObservableCollection<Tbl18Superclass>( );

            Tbl18SuperclassesList.Insert(0, new Tbl18Superclass   {   SuperclassName = CultRes.StringsRes.DatasetNew  }  );

                    _businessLayer = new BusinessLayer.BusinessLayer();
                Tbl12SubphylumsAllList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12Subphylums());
                Tbl15SubdivisionsAllList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15Subdivisions());

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopySuperclass(object o)
        {
            if (CurrentTbl18Superclass == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var superclass = _businessLayer.SingleListTbl18SuperclassesBySuperclassId(CurrentTbl18Superclass.SuperclassID);

            Tbl18SuperclassesList.Insert(0, new Tbl18Superclass
            {
                 SuperclassName = CultRes.StringsRes.DatasetNew,
                SubphylumID = superclass.SubphylumID,
                SubdivisionID = superclass.SubdivisionID,
                Valid =  superclass.Valid,
                ValidYear =  superclass.ValidYear,
                Synonym =  superclass.Synonym,
                Author =  superclass.Author,
                AuthorYear =  superclass.AuthorYear,
                Info =  superclass.Info,
                EngName =  superclass.EngName,
                GerName =  superclass.GerName,
                FraName =  superclass.FraName,
                PorName =  superclass.PorName,
                Memo =  superclass.Memo
            });

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
     
        private void DeleteSuperclass(object o)
        {
            if (CurrentTbl18Superclass == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            var ret = false;
            //check if in Tbl21Classes connected datasets, than return
            Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesBySuperclassId(CurrentTbl18Superclass.SuperclassID));
            if (Tbl21ClassesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Class + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                 ret = true;              
            }
            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsBySuperclassId(CurrentTbl18Superclass.SuperclassID));
            if (Tbl90ReferenceAuthorsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesBySuperclassId(CurrentTbl18Superclass.SuperclassID));
            if (Tbl90ReferenceSourcesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsBySuperclassId(CurrentTbl18Superclass.SuperclassID));
            if (Tbl90ReferenceExpertsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceExpert + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBySuperclassId(CurrentTbl18Superclass.SuperclassID));
            if (Tbl93CommentsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Comment + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            if (ret)  return;
            {
            try
            {
                var superclass = _businessLayer.SingleListTbl18SuperclassesBySuperclassId(CurrentTbl18Superclass.SuperclassID);
                if (superclass != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl18Superclass.SuperclassName,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    superclass.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveSuperclass(superclass);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl18Superclass.SuperclassName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl18Superclass.SuperclassName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }
         }
            if (SearchSuperclassName != "")
            {
                if (SearchSuperclassName == "*")  //show all datasets
                {
                    SearchSuperclassName = "";
                    Tbl18SuperclassesList.Clear();
                    
                Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassName(SearchSuperclassName));            
                    SearchSuperclassName = "*";
                }
                else
                {               
                    Tbl18SuperclassesList =  new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassName(SearchSuperclassName));

                }
                SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                SuperclassesView.Refresh();
            }
            else  //SearchName = empty
            {
                Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassName(SearchSuperclassName));

                SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                SuperclassesView.MoveCurrentToFirst();
             }
        }
        //-------------------------------------------------------------------------------------------------                    
        
        private void SaveSuperclass(object o)
        {
            if (CurrentTbl18Superclass == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }
            _businessLayer = new BusinessLayer.BusinessLayer();

            try
            {
                var superclass = _businessLayer.SingleListTbl18SuperclassesBySuperclassId(CurrentTbl18Superclass.SuperclassID);
                if (CurrentTbl18Superclass.SuperclassID != 0)
                {
                    if (superclass != null) //update
                    {
                        superclass.SuperclassName = CurrentTbl18Superclass.SuperclassName;
                        superclass.SubphylumID = CurrentTbl18Superclass.SubphylumID;
                        superclass.SubdivisionID = CurrentTbl18Superclass.SubdivisionID;
                        superclass.Valid = CurrentTbl18Superclass.Valid;
                        superclass.ValidYear = CurrentTbl18Superclass.ValidYear;       
                        superclass.Synonym = CurrentTbl18Superclass.Synonym;
                        superclass.Author = CurrentTbl18Superclass.Author;
                        superclass.AuthorYear = CurrentTbl18Superclass.AuthorYear;
                        superclass.Info = CurrentTbl18Superclass.Info;
                        superclass.EngName = CurrentTbl18Superclass.EngName;
                        superclass.GerName = CurrentTbl18Superclass.GerName;
                        superclass.FraName = CurrentTbl18Superclass.FraName;
                        superclass.PorName = CurrentTbl18Superclass.PorName;
                        superclass.Updater = Environment.UserName;
                        superclass.UpdaterDate = DateTime.Now;
                        superclass.Memo = CurrentTbl18Superclass.Memo;
                        superclass.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    superclass = new Tbl18Superclass   //add new
                    {
                        SuperclassName = CurrentTbl18Superclass.SuperclassName,
                        SubphylumID = CurrentTbl18Superclass.SubphylumID,
                        SubdivisionID = CurrentTbl18Superclass.SubdivisionID,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl18Superclass.Valid,
                        ValidYear = CurrentTbl18Superclass.ValidYear,
                        Synonym = CurrentTbl18Superclass.Synonym,
                        Author = CurrentTbl18Superclass.Author,
                        AuthorYear = CurrentTbl18Superclass.AuthorYear,
                        Info = CurrentTbl18Superclass.Info,
                        EngName = CurrentTbl18Superclass.EngName,
                        GerName = CurrentTbl18Superclass.GerName,
                        FraName = CurrentTbl18Superclass.FraName,
                        PorName = CurrentTbl18Superclass.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl18Superclass.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //SubphylumID && SubdivisionID may be not 0
                    if (CurrentTbl18Superclass.SubphylumID == 0 && CurrentTbl18Superclass.SubdivisionID == 0)
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and SubphylumId already exist       
                    var dataset = _businessLayer.ListTbl18SuperclassesBySuperclassNameAndSubphylumIdAndSubdivisionId(CurrentTbl18Superclass.SuperclassName, CurrentTbl18Superclass.SubphylumID, CurrentTbl18Superclass.SubdivisionID);

                    if (dataset.Count != 0 && CurrentTbl18Superclass.SuperclassID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl18Superclass.SuperclassName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl18Superclass.SuperclassID == 0 ||
                        dataset.Count != 0 && CurrentTbl18Superclass.SuperclassID != 0 ||
                        dataset.Count == 0 && CurrentTbl18Superclass.SuperclassID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl18Superclass.SuperclassName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateSuperclass(superclass);
                                _position = SuperclassesView.CurrentPosition;

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl18Superclass.SuperclassID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl18Superclass.SuperclassName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }
            if (SearchSuperclassName != "")
            {
                if (SearchSuperclassName == "*")  //show all datasets
                {
                    SearchSuperclassName = "";
                    Tbl18SuperclassesList.Clear();
                    
                Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassName(SearchSuperclassName));            
                    SearchSuperclassName = "*";
                }
                else
                {               
                    Tbl18SuperclassesList = int.TryParse(SearchSuperclassName, out var id)
                        ? new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassId(id))
                        : new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassName(SearchSuperclassName));

                }
                SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                SuperclassesView.MoveCurrentToPosition(_position);
            }
            else  
            {
                Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassName(CurrentTbl18Superclass.SuperclassName));

                SuperclassesView= CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
                SuperclassesView.Refresh();
            }
        }
        #endregion "Public Commands"                  
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl12Subphylum"                 
        //-------------------------------------------------------------------------

        private RelayCommand _saveSubphylumCommand;

        public ICommand SaveSubphylumCommand => _saveSubphylumCommand ??
                                                 (_saveSubphylumCommand = new RelayCommand(delegate { SaveSubphylum(null); }));

        //-------------------------------------------------------------------------          
     
        private void SaveSubphylum(object o)
        {
            if (CurrentTbl12Subphylum == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var subphylum = _businessLayer.SingleListTbl12SubphylumsBySubphylumId(CurrentTbl12Subphylum.SubphylumID);
                if (CurrentTbl12Subphylum.SubphylumID != 0)
                {
                    if (subphylum != null) //update
                    {
                        subphylum.SubphylumName = CurrentTbl12Subphylum.SubphylumName;
                        subphylum.Valid = CurrentTbl12Subphylum.Valid;
                        subphylum.ValidYear = CurrentTbl12Subphylum.ValidYear;       
                        subphylum.Synonym = CurrentTbl12Subphylum.Synonym;
                        subphylum.Author = CurrentTbl12Subphylum.Author;
                        subphylum.AuthorYear = CurrentTbl12Subphylum.AuthorYear;
                        subphylum.Info = CurrentTbl12Subphylum.Info;
                        subphylum.EngName = CurrentTbl12Subphylum.EngName;
                        subphylum.GerName = CurrentTbl12Subphylum.GerName;
                        subphylum.FraName = CurrentTbl12Subphylum.FraName;
                        subphylum.PorName = CurrentTbl12Subphylum.PorName;
                        subphylum.Updater = Environment.UserName;
                        subphylum.UpdaterDate = DateTime.Now;
                        subphylum.Memo = CurrentTbl12Subphylum.Memo;
                        subphylum.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    subphylum = new Tbl12Subphylum   //add new
                    {
                        SubphylumName = CurrentTbl12Subphylum.SubphylumName,              
                        PhylumID = CurrentTbl12Subphylum.PhylumID,     
                        CountID = RandomHelper.Randomnumber(),

                        Valid = CurrentTbl12Subphylum.Valid,
                        ValidYear = CurrentTbl12Subphylum.ValidYear,
                        Synonym = CurrentTbl12Subphylum.Synonym,
                        Author = CurrentTbl12Subphylum.Author,
                        AuthorYear = CurrentTbl12Subphylum.AuthorYear,
                        Info = CurrentTbl12Subphylum.Info,
                        EngName = CurrentTbl12Subphylum.EngName,
                        GerName = CurrentTbl12Subphylum.GerName,
                        FraName = CurrentTbl12Subphylum.FraName,
                        PorName = CurrentTbl12Subphylum.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl12Subphylum.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //PhylumID may be not 0
                    if (CurrentTbl12Subphylum.PhylumID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and SubphylumId already exist       
                    var dataset = _businessLayer.ListTbl12SubphylumsBySubphylumNameAndPhylumId(CurrentTbl12Subphylum.SubphylumName, CurrentTbl12Subphylum.PhylumID);

                    if (dataset.Count != 0 && CurrentTbl12Subphylum.SubphylumID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl12Subphylum.SubphylumName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl12Subphylum.SubphylumID == 0 ||
                        dataset.Count != 0 && CurrentTbl12Subphylum.SubphylumID != 0 ||
                        dataset.Count == 0 && CurrentTbl12Subphylum.SubphylumID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl12Subphylum.SubphylumName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateSubphylum(subphylum);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl12Subphylum.SubphylumID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl12Subphylum.SubphylumName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

                Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumId(CurrentTbl18Superclass.SubphylumID));            

            SelectedMainTabIndex = 0;
            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.Refresh();
        }
        #endregion "Public Commands"                  
                                                          

 //    Part 3    

           
        #region "Public Commands Connect <== Tbl15Subdivision"                 
        //-------------------------------------------------------------------------
        private RelayCommand _saveSubdivisionCommand;

        public ICommand SaveSubdivisionCommand => _saveSubdivisionCommand ??
                                                 (_saveSubdivisionCommand = new RelayCommand(delegate { SaveSubdivision(null); }));

        //-------------------------------------------------------------------------          
     
        private void SaveSubdivision(object o)
        {
            if (CurrentTbl15Subdivision == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var subdivision = _businessLayer.SingleListTbl15SubdivisionsBySubdivisionId(CurrentTbl15Subdivision.SubdivisionID);
                if (CurrentTbl15Subdivision.SubdivisionID != 0)
                {
                    if (subdivision != null) //update
                    {
                        subdivision.SubdivisionName = CurrentTbl15Subdivision.SubdivisionName;
                        subdivision.DivisionID = CurrentTbl15Subdivision.DivisionID;
                        subdivision.Valid = CurrentTbl15Subdivision.Valid;
                        subdivision.ValidYear = CurrentTbl15Subdivision.ValidYear;       
                        subdivision.Synonym = CurrentTbl15Subdivision.Synonym;
                        subdivision.Author = CurrentTbl15Subdivision.Author;
                        subdivision.AuthorYear = CurrentTbl15Subdivision.AuthorYear;
                        subdivision.Info = CurrentTbl15Subdivision.Info;
                        subdivision.EngName = CurrentTbl15Subdivision.EngName;
                        subdivision.GerName = CurrentTbl15Subdivision.GerName;
                        subdivision.FraName = CurrentTbl15Subdivision.FraName;
                        subdivision.PorName = CurrentTbl15Subdivision.PorName;
                        subdivision.Updater = Environment.UserName;
                        subdivision.UpdaterDate = DateTime.Now;
                        subdivision.Memo = CurrentTbl15Subdivision.Memo;
                        subdivision.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    subdivision = new Tbl15Subdivision   //add new
                    {
                        SubdivisionName = CurrentTbl15Subdivision.SubdivisionName,              
                        DivisionID = CurrentTbl15Subdivision.DivisionID,     
                        CountID = RandomHelper.Randomnumber(),

                        Valid = CurrentTbl15Subdivision.Valid,
                        ValidYear = CurrentTbl15Subdivision.ValidYear,
                        Synonym = CurrentTbl15Subdivision.Synonym,
                        Author = CurrentTbl15Subdivision.Author,
                        AuthorYear = CurrentTbl15Subdivision.AuthorYear,
                        Info = CurrentTbl15Subdivision.Info,
                        EngName = CurrentTbl15Subdivision.EngName,
                        GerName = CurrentTbl15Subdivision.GerName,
                        FraName = CurrentTbl15Subdivision.FraName,
                        PorName = CurrentTbl15Subdivision.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl15Subdivision.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //DivisionID may be not 0
                    if (CurrentTbl15Subdivision.DivisionID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and DivisionId already exist       
                    var dataset = _businessLayer.ListTbl15SubdivisionsBySubdivisionNameAndDivisionId(CurrentTbl15Subdivision.SubdivisionName, CurrentTbl15Subdivision.DivisionID);

                    if (dataset.Count != 0 && CurrentTbl15Subdivision.SubdivisionID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl15Subdivision.SubdivisionName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl15Subdivision.SubdivisionID == 0 ||
                        dataset.Count != 0 && CurrentTbl15Subdivision.SubdivisionID != 0 ||
                        dataset.Count == 0 && CurrentTbl15Subdivision.SubdivisionID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl15Subdivision.SubdivisionName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateSubdivision(subdivision);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl15Subdivision.SubdivisionID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl15Subdivision.SubdivisionName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

                Tbl15SubdivisionsList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15SubdivisionsBySubdivisionId(CurrentTbl18Superclass.SubdivisionID));            

            SelectedMainTabIndex = 1;
            SubdivisionsView = CollectionViewSource.GetDefaultView(Tbl15SubdivisionsList);
            SubdivisionsView.Refresh();
        }
        #endregion "Public Commands"                  
                                                          



 //    Part 4    

           
        #region "Public Commands Connect ==> Tbl21Class"                 
        //-------------------------------------------------------------------------
        private RelayCommand _addClassCommand;

        public ICommand AddClassCommand => _addClassCommand ??
                                                (_addClassCommand = new RelayCommand(delegate { AddClass(null); }));

        private RelayCommand _copyClassCommand;

        public ICommand CopyClassCommand => _copyClassCommand ??
                                                 (_copyClassCommand = new RelayCommand(delegate { CopyClass(null); }));

        private RelayCommand _deleteClassCommand;

        public ICommand DeleteClassCommand => _deleteClassCommand ??
                                                 (_deleteClassCommand = new RelayCommand(delegate { DeleteClass(null); }));

        private RelayCommand _saveClassCommand;

        public ICommand SaveClassCommand => _saveClassCommand ??
                                                 (_saveClassCommand = new RelayCommand(delegate { SaveClass(null); }));

        //-------------------------------------------------------------------------          
     
        private void AddClass(object o)      
        {
            if (Tbl21ClassesList == null)
                Tbl21ClassesList =  new ObservableCollection<Tbl21Class>( );

            Tbl21ClassesList.Insert(0, new Tbl21Class  { ClassName = CultRes.StringsRes.DatasetNew});

            _businessLayer = new BusinessLayer.BusinessLayer();
                Tbl18SuperclassesAllList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18Superclasses());

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void CopyClass(object o)
        {
            if (CurrentTbl21Class == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var classe = _businessLayer.SingleListTbl21ClassesByClassId(CurrentTbl21Class.ClassID);

            Tbl21ClassesList.Insert(0, new Tbl21Class
            {
                ClassName = CultRes.StringsRes.DatasetNew,
                Valid =  classe.Valid,
                ValidYear =  classe.ValidYear,
                Synonym =  classe.Synonym,
                Author =  classe.Author,
                AuthorYear =  classe.AuthorYear,
                Info =  classe.Info,
                EngName =  classe.EngName,
                GerName =  classe.GerName,
                FraName =  classe.FraName,
                PorName =  classe.PorName,
                Memo =  classe.Memo
            });

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
           
        private void DeleteClass(object o)
        {
            if (CurrentTbl21Class == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var ret = false;
            //check if in Tbl24Subclasses connected datasets, than return
            Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesByClassId(CurrentTbl21Class.ClassID));
            if (Tbl24SubclassesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Subclass + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                 ret = true;              
            }
            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsByClassId(CurrentTbl21Class.ClassID));
            if (Tbl90ReferenceAuthorsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesByClassId(CurrentTbl21Class.ClassID));
            if (Tbl90ReferenceSourcesList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsByClassId(CurrentTbl21Class.ClassID));
            if (Tbl90ReferenceExpertsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.ReferenceExpert + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsByClassId(CurrentTbl21Class.ClassID));
            if (Tbl93CommentsList.Count != 0)
            {
                WpfMessageBox.Show(CultRes.StringsRes.ConnectedTable, CultRes.StringsRes.Comment + " " + CultRes.StringsRes.ConnectedDataset,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                ret = true;
            }
            if (ret)  return;
            {
            try
            {
                var classe = _businessLayer.SingleListTbl21ClassesByClassId(CurrentTbl21Class.ClassID);
                if (classe!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl21Class.ClassName,
                         MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    classe.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveClass(classe);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl21Class.ClassName,
                       MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);  
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl21Class.ClassName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }
         }
            Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesBySuperclassId(CurrentTbl18Superclass.SuperclassID));

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
     
        private void SaveClass(object o)
        {
            if (CurrentTbl21Class == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl21Class.SuperclassID = CurrentTbl18Superclass.SuperclassID;

            try
            {
                var classe = _businessLayer.SingleListTbl21ClassesByClassId(CurrentTbl21Class.ClassID);
                if (CurrentTbl21Class.ClassID != 0)
                {
                    if (classe != null) //update
                    {
                        classe.ClassName = CurrentTbl21Class.ClassName;
                        classe.SuperclassID = CurrentTbl21Class.SuperclassID;
                        classe.Valid = CurrentTbl21Class.Valid;
                        classe.ValidYear = CurrentTbl21Class.ValidYear;       
                        classe.Synonym = CurrentTbl21Class.Synonym;
                        classe.Author = CurrentTbl21Class.Author;
                        classe.AuthorYear = CurrentTbl21Class.AuthorYear;
                        classe.Info = CurrentTbl21Class.Info;
                        classe.EngName = CurrentTbl21Class.EngName;
                        classe.GerName = CurrentTbl21Class.GerName;
                        classe.FraName = CurrentTbl21Class.FraName;
                        classe.PorName = CurrentTbl21Class.PorName;
                        classe.Updater = Environment.UserName;
                        classe.UpdaterDate = DateTime.Now;
                        classe.Memo = CurrentTbl21Class.Memo;
                        classe.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    classe = new Tbl21Class   //add new
                    {
                        ClassName = CurrentTbl21Class.ClassName,              
                        SuperclassID = CurrentTbl21Class.SuperclassID,     
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl21Class.Valid,
                        ValidYear = CurrentTbl21Class.ValidYear,
                        Synonym = CurrentTbl21Class.Synonym,
                        Author = CurrentTbl21Class.Author,
                        AuthorYear = CurrentTbl21Class.AuthorYear,
                        Info = CurrentTbl21Class.Info,
                        EngName = CurrentTbl21Class.EngName,
                        GerName = CurrentTbl21Class.GerName,
                        FraName = CurrentTbl21Class.FraName,
                        PorName = CurrentTbl21Class.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl21Class.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //SuperclassID may be not 0
                    if (CurrentTbl21Class.SuperclassID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and SuperclassId already exist       
                    var dataset = _businessLayer.ListTbl21ClassesByClassNameAndSuperclassId(CurrentTbl21Class.ClassName, CurrentTbl21Class.SuperclassID);

                    if (dataset.Count != 0 && CurrentTbl21Class.ClassID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl21Class.ClassName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl21Class.ClassID == 0 ||
                        dataset.Count != 0 && CurrentTbl21Class.ClassID != 0 ||
                        dataset.Count == 0 && CurrentTbl21Class.ClassID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl21Class.ClassName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateClass(classe);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl21Class.ClassID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl21Class.ClassName,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesBySuperclassId(CurrentTbl18Superclass.SuperclassID));            

            SelectedMainTabIndex = 1;
            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.Refresh();
        }
        #endregion "Public Commands"                  
                                                          


 //    Part 5    

                                                          
                      
 //    Part 6    

 
            

 //    Part 7    

 

 //    Part 8    

           
        #region "Public Commands Connect ==> Tbl90ReferenceAuthor"
        //-------------------------------------------------------------------------
        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??
                                                    (_addReferenceAuthorCommand = new RelayCommand(delegate { AddReferenceAuthor(null); }));

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??
                        (_copyReferenceAuthorCommand = new RelayCommand(delegate { CopyReferenceAuthor(null); }));

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??
                                               (_deleteReferenceAuthorCommand = new RelayCommand(delegate { DeleteReferenceAuthor(null); }));

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??
                     (_saveReferenceAuthorCommand = new RelayCommand(delegate { SaveReferenceAuthor(null); }));
        //-------------------------------------------------------------------------                    
     
        public void AddReferenceAuthor(object o)
        {
            if (Tbl90ReferenceAuthorsList == null)
                Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>();

            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference   { Info = CultRes.StringsRes.DatasetNew });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public void CopyReferenceAuthor(object o)
        {
            if (CurrentTbl90ReferenceAuthor == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID);

            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference
            {
                RefAuthorID = reference.RefAuthorID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void DeleteReferenceAuthor(object o)
        {
            if (CurrentTbl90ReferenceAuthor == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

                 try
                {
                    var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceAuthor.ReferenceID);
                    if (reference != null)
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceAuthor.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        reference.EntityState = EntityState.Deleted;
                        _businessLayer.RemoveReference(reference);

                        WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceAuthor.Info,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    else
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceAuthor.Info + " " + CultRes.StringsRes.DeleteCan1,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    _entityException.EntityException(ex);
                }

            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsBySuperclassId(CurrentTbl18Superclass.SuperclassID));

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }

        //----------------------------------------------------------------------            
     
        public void SaveReferenceAuthor(object o)
        {
            if (CurrentTbl90ReferenceAuthor == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceAuthor.SuperclassID = CurrentTbl18Superclass.SuperclassID;

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
                        if (CurrentTbl90ReferenceAuthor.RefExpertID == null &&
                            CurrentTbl90ReferenceAuthor.RefSourceID == null &&
                            CurrentTbl90ReferenceAuthor.RefAuthorID == null)
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
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceAuthor.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceAuthor.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceAuthor.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceAuthor.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateReference(reference);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl90ReferenceAuthor.ReferenceID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl90ReferenceAuthor.ReferenceID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefAuthorsBySuperclassId(CurrentTbl18Superclass.SuperclassID));           
     

            SelectedMainSubRefTabIndex = 2;

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion "Public Commands"                  
           
        #region "Public Commands Connect ==> Tbl90ReferenceSource" 
        //-------------------------------------------------------------------------
        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??
                                                    (_addReferenceSourceCommand = new RelayCommand(delegate { AddReferenceSource(null); }));

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??
                        (_copyReferenceSourceCommand = new RelayCommand(delegate { CopyReferenceSource(null); }));

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??
                                                        (_deleteReferenceSourceCommand = new RelayCommand(delegate { DeleteReferenceSource(null); }));

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??
                     (_saveReferenceSourceCommand = new RelayCommand(delegate { SaveReferenceSource(null); }));

        //-------------------------------------------------------------------------          
     
        public void AddReferenceSource(object o)
        {
            if (Tbl90ReferenceSourcesList == null)
                Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>();

            Tbl90ReferenceSourcesList .Insert(0, new Tbl90Reference  { Info = CultRes.StringsRes.DatasetNew });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public void CopyReferenceSource(object o)
        {
            if (CurrentTbl90ReferenceSource == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);

            Tbl90ReferenceSourcesList.Insert(0, new Tbl90Reference
            {
                RefSourceID = reference.RefSourceID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void DeleteReferenceSource(object o)
        {
            if (CurrentTbl90ReferenceSource == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceSource.ReferenceID);
                if (reference != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceSource.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    reference.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveReference(reference);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceSource.Info,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceSource.Info + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesBySuperclassId(CurrentTbl18Superclass.SuperclassID));

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }        
        //----------------------------------------------------------------------            
     
        public void SaveReferenceSource(object o)
        {
            if (CurrentTbl90ReferenceSource == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceSource.SuperclassID = CurrentTbl18Superclass.SuperclassID;

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
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceSource.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceSource.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceSource.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateReference(reference);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl90ReferenceSource.ReferenceID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                             : CurrentTbl90ReferenceSource.ReferenceID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefSourcesBySuperclassId(CurrentTbl18Superclass.SuperclassID));           
     
            SelectedMainSubRefTabIndex = 1;

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }
        #endregion "Public Commands"                  
           
        #region "Public Commands Connect ==> Tbl90ReferenceExpert"
        //-------------------------------------------------------------------------
 
        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??
                                                    (_addReferenceExpertCommand = new RelayCommand(delegate { AddReferenceExpert(null); }));

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??
                        (_copyReferenceExpertCommand = new RelayCommand(delegate { CopyReferenceExpert(null); }));

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??
                                                        (_deleteReferenceExpertCommand = new RelayCommand(delegate { DeleteReferenceExpert(null); }));
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??
                     (_saveReferenceExpertCommand = new RelayCommand(delegate { SaveReferenceExpert(null); }));
        //-------------------------------------------------------------------------          
     
        public void AddReferenceExpert(object o)
        {
            if (Tbl90ReferenceExpertsList == null)
                Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>();

            Tbl90ReferenceExpertsList .Insert(0, new Tbl90Reference   { Info = CultRes.StringsRes.DatasetNew });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public void CopyReferenceExpert(object o)
        {
            if (CurrentTbl90ReferenceExpert == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);

            Tbl90ReferenceExpertsList.Insert(0, new Tbl90Reference
            {
                RefExpertID = reference.RefExpertID,
                Valid = reference.Valid,
                ValidYear = reference.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = reference.Memo
            });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void DeleteReferenceExpert(object o)
        {
            if (CurrentTbl90ReferenceExpert == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var reference = _businessLayer.SingleListTbl90ReferencesByReferenceId(CurrentTbl90ReferenceExpert.ReferenceID);
                if (reference != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceExpert.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    reference.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveReference(reference);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceExpert.Info,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceExpert.Info + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsBySuperclassId(CurrentTbl18Superclass.SuperclassID));

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        public void SaveReferenceExpert(object o)
        {
            if (CurrentTbl90ReferenceExpert == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceExpert.SuperclassID = CurrentTbl18Superclass.SuperclassID;

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
                        return;
                    }
                    if (dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceID == 0 ||
                        dataset.Count != 0 && CurrentTbl90ReferenceExpert.ReferenceID != 0 ||
                        dataset.Count == 0 && CurrentTbl90ReferenceExpert.ReferenceID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl90ReferenceExpert.Info,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                                _businessLayer.UpdateReference(reference);

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl90ReferenceExpert.ReferenceID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                             : CurrentTbl90ReferenceExpert.ReferenceID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_businessLayer.ListTbl90ReferenceListRefExpertsBySuperclassId(CurrentTbl18Superclass.SuperclassID));     
     
            SelectedMainSubRefTabIndex = 0;

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }
        #endregion "Public Commands"                  
           
        #region "Public Commands Connect ==> Tbl93Comment"

        //-------------------------------------------------------------------------
        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??
                                                 (_addCommentCommand = new RelayCommand(delegate { AddComment(null); }));

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??
                                                  (_copyCommentCommand = new RelayCommand(delegate { CopyComment(null); }));

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??
                                                        (_deleteCommentCommand = new RelayCommand(delegate { DeleteComment(null); }));

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??
                                                  (_saveCommentCommand = new RelayCommand(delegate { SaveComment(null); }));
        //-------------------------------------------------------------------------          
     
        public void AddComment(object o)
        {
            if (Tbl93CommentsList == null)
                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>();

            Tbl93CommentsList .Insert(0, new Tbl93Comment  { Info = CultRes.StringsRes.DatasetNew });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
         }
        //----------------------------------------------------------------------            
     
        public void CopyComment(object o)
        {
            if (CurrentTbl93Comment == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);

            Tbl93CommentsList.Insert(0, new Tbl93Comment
            {
                Valid = comment.Valid,
                ValidYear = comment.ValidYear,
                Info = CultRes.StringsRes.DatasetNew,
                Memo = comment.Memo
            });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void DeleteComment(object o)
        {
            if (CurrentTbl93Comment == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var comment = _businessLayer.SingleListTbl93CommentsByCommentId(CurrentTbl93Comment.CommentID);
                if (comment != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.Info,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    comment.EntityState = EntityState.Deleted;
                    _businessLayer.RemoveComment(comment);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.Info,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBySuperclassId(CurrentTbl18Superclass.SuperclassID));

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        private void SaveComment(object o)
        {
            if (CurrentTbl93Comment == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            CurrentTbl93Comment.SuperclassID = CurrentTbl18Superclass.SuperclassID;

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
                            return;
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

                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        CurrentTbl93Comment.CommentID == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : CurrentTbl93Comment.Info,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_businessLayer.ListTbl93CommentsBySuperclassId(CurrentTbl18Superclass.SuperclassID));          
     
            SelectedMainTabIndex = 3;

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        #endregion "Public Commands"                  
 
             
 //    Part 9    

      
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??
                                                         (_getConnectedTablesCommand = new RelayCommand(delegate { GetConnectedTablesById(null); }));

        private void GetConnectedTablesById(object o)
        {
            Tbl21ClassesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();

            SelectedMainTabIndex = 0;  //change to Connect tab
            SelectedMainSubRefTabIndex = 0;
            SelectedDetailTabIndex = 1;
            SelectedDetailSubTabIndex = 0;
            SelectedDetailSubRefTabIndex = 0;

            _businessLayer = new BusinessLayer.BusinessLayer();
             Tbl06PhylumsAllList =  new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06Phylums());

             Tbl12SubphylumsList =  new ObservableCollection<Tbl12Subphylum>(
                       _businessLayer.ListTbl12SubphylumsBySubphylumId(CurrentTbl18Superclass.SubphylumID));
 
            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.Refresh();
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
                }
                if (_selectedDetailTabIndex == 1)                
                    SelectedDetailSubTabIndex = 1;                
                if (_selectedDetailTabIndex == 2)                
                    SelectedDetailSubTabIndex = 2;               
                if (_selectedDetailTabIndex == 3)
                    SelectedDetailSubTabIndex = 3;
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
                    Tbl12SubphylumsList =  new ObservableCollection<Tbl12Subphylum>(
                        _businessLayer.ListTbl12SubphylumsBySubphylumId(CurrentTbl18Superclass.SubphylumID));
 
                    Tbl06PhylumsAllList =  new ObservableCollection<Tbl06Phylum>(
                        _businessLayer.ListTbl06Phylums());

                    SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
                    SubphylumsView.Refresh();

                    SelectedMainTabIndex = 0;
                }
                if (_selectedDetailSubTabIndex == 1)
                {
                    Tbl21ClassesList =  new ObservableCollection<Tbl21Class>(
                        _businessLayer.ListTbl21ClassesBySuperclassId(CurrentTbl18Superclass.SuperclassID));

                    ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
                    ClassesView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                        _businessLayer.ListTbl90RefExperts());
                    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefExpertsBySuperclassId(CurrentTbl18Superclass.SuperclassID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainTabIndex = 2;
                }
                if (_selectedDetailSubTabIndex == 3)
                {
                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(
                        _businessLayer.ListTbl93CommentsBySuperclassId(CurrentTbl18Superclass.SuperclassID));

                    CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                    CommentsView.Refresh();

                    SelectedMainTabIndex = 3;
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
                    Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                        _businessLayer.ListTbl90RefExperts());
                    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefExpertsBySuperclassId(CurrentTbl18Superclass.SuperclassID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(
                        _businessLayer.ListTbl90RefSources());

                    Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefSourcesBySuperclassId(CurrentTbl18Superclass.SuperclassID));

                    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                    ReferenceSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(
                        _businessLayer.ListTbl90RefAuthors());

                    Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefAuthorsBySuperclassId(CurrentTbl18Superclass.SuperclassID));

                    ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                    ReferenceAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"
 

 //    Part 11    

     
        #region "Public Properties Tbl18Superclass"

        private string _searchSuperclassName = "";
        public string SearchSuperclassName
        {
            get => _searchSuperclassName; 
            set { _searchSuperclassName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView SuperclassesView;
        private   Tbl18Superclass CurrentTbl18Superclass => SuperclassesView?.CurrentItem as Tbl18Superclass;

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList;
        public  ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
        {
            get => _tbl18SuperclassesList; 
            set {  _tbl18SuperclassesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public  ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList; 
            set {  _tbl18SuperclassesAllList = value; RaisePropertyChanged();   }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl12Subphylum"

        public  ICollectionView SubphylumsView;
        private Tbl12Subphylum CurrentTbl12Subphylum => SubphylumsView?.CurrentItem as Tbl12Subphylum;           

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList;
        public  ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
        {
            get => _tbl12SubphylumsList; 
            set { _tbl12SubphylumsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public  ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList; 
            set { _tbl12SubphylumsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
  
       
        #region "Public Properties Tbl15Subdivision"

        public  ICollectionView SubdivisionsView;
        private  Tbl15Subdivision CurrentTbl15Subdivision => SubdivisionsView?.CurrentItem as Tbl15Subdivision;           

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsList;
        public   ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
        {
            get => _tbl15SubdivisionsList; 
            set { _tbl15SubdivisionsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;
        public  ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get => _tbl15SubdivisionsAllList; 
            set { _tbl15SubdivisionsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl21Class"

        public ICollectionView ClassesView;
        private Tbl21Class CurrentTbl21Class => ClassesView?.CurrentItem as Tbl21Class;           

        private ObservableCollection<Tbl21Class> _tbl21ClassesList;
        public  ObservableCollection<Tbl21Class> Tbl21ClassesList
        {
            get => _tbl21ClassesList; 
            set { _tbl21ClassesList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl24Subclass"

        public ICollectionView SubclassesView;
        private Tbl24Subclass CurrentTbl24Subclass => SubclassesView?.CurrentItem as Tbl24Subclass;           

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesList;
        public  ObservableCollection<Tbl24Subclass> Tbl24SubclassesList
        {
            get => _tbl24SubclassesList; 
            set { _tbl24SubclassesList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties"     
        
        #region "Public Properties Tbl06Phylum"

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public  ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList; 
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"     
        
        #region "Public Properties Tbl09Division"

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public  ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList; 
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"     
           
        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public  ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList; 
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public  ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList; 
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList; 
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90ReferenceAuthor"

        public ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList; 
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        public ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList; 
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        public ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList; 
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
         
        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList; 
            set { _tbl93CommentsList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"     
 

 



   }
}   
