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

    
         //    Tbl21ClassesViewModel Skriptdatum:  19.06.2018  18:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl21ClassesViewModel : Tbl03RegnumsViewModel
    {     
         
        #region "Private Data Members"

        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;   
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl21ClassesViewModel()
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

             
        #region "Public Commands Basic Tbl21Class"
        //-------------------------------------------------------------------------
        private RelayCommand _clearClassCommand;

        public ICommand ClearClassCommand => _clearClassCommand ??
                                                  (_clearClassCommand = new RelayCommand(delegate { ClearClass(null); }));         
             
        private RelayCommand _getClassesByNameOrIdCommand;  

        public  ICommand GetClassesByNameOrIdCommand => _getClassesByNameOrIdCommand ??
                                                           (_getClassesByNameOrIdCommand = new RelayCommand(delegate { GetClassesByNameOrId(null); }));        
             
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
     
        private void ClearClass(object o)
        {
            SearchClassName = string.Empty;

            Tbl18SuperclassesList?.Clear();
            Tbl21ClassesList?.Clear();
            Tbl24SubclassesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();
        }
        //----------------------------------------------------------------------                  
     
        private void GetClassesByNameOrId(object o)
        {
            Tbl18SuperclassesList?.Clear();
            Tbl24SubclassesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();

            Tbl21ClassesList = int.TryParse(SearchClassName, out var id) ?
                new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassId(id)) :
                new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassName(SearchClassName));

            Tbl18SuperclassesAllList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18Superclasses());

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
     
        private void AddClass(object o)
        {
            Tbl21ClassesList = new ObservableCollection<Tbl21Class> {new Tbl21Class
            {
                ClassName = CultRes.StringsRes.DatasetNew,
                SuperclassID = CurrentTbl21Class.SuperclassID
            }  };

            Tbl18SuperclassesAllList?.Clear();
            Tbl18SuperclassesAllList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18Superclasses());

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
        
        private void CopyClass(object o)
        {
            Tbl21ClassesList = new ObservableCollection<Tbl21Class>();

            var classe = _businessLayer.SingleListTbl21ClassesByClassId(CurrentTbl21Class.ClassID);

            Tbl21ClassesList.Add(new Tbl21Class
            {
                SuperclassID = classe.SuperclassID,
                ClassName = CultRes.StringsRes.DatasetNew,
                Valid = classe.Valid,
                ValidYear = classe.ValidYear,
                Synonym = classe.Synonym,
                Author = classe.Author,
                AuthorYear = classe.AuthorYear,
                Info = classe.Info,
                EngName = classe.EngName,
                GerName = classe.GerName,
                FraName = classe.FraName,
                PorName = classe.PorName,
                Memo = classe.Memo
            });

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
        
        private void DeleteClass(object o)
        {
            try
            {
                var classe = _businessLayer.SingleListTbl21ClassesByClassId(CurrentTbl21Class.ClassID);
                if (classe != null)
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

            Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassName(SearchClassName));

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
        
        private void SaveClass(object o)
        {
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

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl21Class.ClassName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl21Class.ClassID == 0)  //new Dataset                        
                Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassName(CurrentTbl21Class.ClassName));
            if (CurrentTbl21Class.ClassID != 0)   //update 
                Tbl21ClassesList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21ClassesByClassId(CurrentTbl21Class.ClassID));

            ClassesView = CollectionViewSource.GetDefaultView(Tbl21ClassesList);
            ClassesView.Refresh();
        }
        #endregion "Public Commands"                  
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl18Superclass"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearSuperclassCommand;

        public  ICommand ClearSuperclassCommand => _clearSuperclassCommand ??
                                                  (_clearSuperclassCommand = new RelayCommand(delegate { ClearSuperclass(null); }));

        private RelayCommand _getSuperclassesByNameOrIdCommand;  

        public  ICommand GetSuperclassesByNameOrIdCommand => _getSuperclassesByNameOrIdCommand ??
                                                           (_getSuperclassesByNameOrIdCommand = new RelayCommand(delegate { GetSuperclassesByNameOrId(null); }));

        private RelayCommand _addSuperclassCommand;

        public  ICommand AddSuperclassCommand => _addSuperclassCommand ??
                                                (_addSuperclassCommand = new RelayCommand(delegate { AddSuperclass(null); }));

        private RelayCommand _copySuperclassCommand;

        public  ICommand CopySuperclassCommand => _copySuperclassCommand ??
                                                 (_copySuperclassCommand = new RelayCommand(delegate { CopySuperclass(null); }));

        private RelayCommand _saveSuperclassCommand;

        public  ICommand SaveSuperclassCommand => _saveSuperclassCommand ??
                                                 (_saveSuperclassCommand = new RelayCommand(delegate { SaveSuperclass(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearSuperclass(object o)
        {
            SearchSuperclassName = string.Empty;
            Tbl18SuperclassesList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        private void GetSuperclassesByNameOrId(object o)
        {
            Tbl18SuperclassesList = int.TryParse(SearchSuperclassName, out var id) ?
                new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassId(id)) :
                new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassName(SearchSuperclassName));

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        private void AddSuperclass(object o)      
        {
            Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass> {new Tbl18Superclass
            {   SuperclassName = CultRes.StringsRes.DatasetNew    }  };

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void CopySuperclass(object o)
        {
            Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>();

            var superclass = _businessLayer.SingleListTbl18SuperclassesBySuperclassId(CurrentTbl18Superclass.SuperclassID);

            Tbl18SuperclassesList.Add(new Tbl18Superclass
            {
                SubdivisionID = superclass.SubdivisionID,
                SuperclassName = CultRes.StringsRes.DatasetNew,
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
        //----------------------------------------------------------------------            
     
        private void SaveSuperclass(object o)
        {
            try
            {
                var superclass = _businessLayer.SingleListTbl18SuperclassesBySuperclassId(CurrentTbl18Superclass.SuperclassID);
                if (CurrentTbl18Superclass.SuperclassID != 0)
                {
                    if (superclass != null) //update
                    {
                        superclass.SuperclassName = CurrentTbl18Superclass.SuperclassName;
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

                    //check if dataset with Name and SuperclassId already exist       
                    var dataset = _businessLayer.ListTbl18SuperclassesBySuperclassNameAndSubdivisionIdAndSubphylumId(CurrentTbl18Superclass.SuperclassName, CurrentTbl18Superclass.SubdivisionID, CurrentTbl18Superclass.SubphylumID);

                    if (dataset.Count != 0 && CurrentTbl18Superclass.SuperclassID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl18Superclass.SuperclassName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
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

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl18Superclass.SuperclassName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl18Superclass.SuperclassID == 0)  //new Dataset                        
                Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassName(CurrentTbl18Superclass.SuperclassName));
            if (CurrentTbl18Superclass.SuperclassID != 0)   //update 
                Tbl18SuperclassesList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18SuperclassesBySuperclassId(CurrentTbl18Superclass.SuperclassID));

            SelectedMainTabIndex = 0;
            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();
        }
        #endregion "Public Commands"                  
 
            

 //    Part 3    

 


 //    Part 4    

           
        #region "Public Commands Connect <== Tbl24Subclass"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearSubclassCommand;

        public ICommand ClearSubclassCommand => _clearSubclassCommand ??
                                                  (_clearSubclassCommand = new RelayCommand(delegate { ClearSubclass(null); }));

        private RelayCommand _getSubclassesByNameOrIdCommand;  

        public  ICommand GetSubclassesByNameOrIdCommand => _getSubclassesByNameOrIdCommand ??
                                                           (_getSubclassesByNameOrIdCommand = new RelayCommand(delegate { GetSubclassesByNameOrId(null); }));

        private RelayCommand _addSubclassCommand;

        public ICommand AddSubclassCommand => _addSubclassCommand ??
                                                (_addSubclassCommand = new RelayCommand(delegate { AddSubclass(null); }));

        private RelayCommand _copySubclassCommand;

        public ICommand CopySubclassCommand => _copySubclassCommand ??
                                                 (_copySubclassCommand = new RelayCommand(delegate { CopySubclass(null); }));

        private RelayCommand _saveSubclassCommand;

        public ICommand SaveSubclassCommand => _saveSubclassCommand ??
                                                 (_saveSubclassCommand = new RelayCommand(delegate { SaveSubclass(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearSubclass(object o)
        {
            SearchSubclassName = string.Empty;
            Tbl24SubclassesList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        private void GetSubclassesByNameOrId(object o)
        {
            Tbl24SubclassesList = int.TryParse(SearchSubclassName, out var id) ?
                new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassId(id)) :
                new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassName(SearchSubclassName));

            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        private void AddSubclass(object o)      
        {
            Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass> {new Tbl24Subclass
            {  
                SubclassName = CultRes.StringsRes.DatasetNew,
                ClassID = CurrentTbl21Class.ClassID
            }    };

            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void CopySubclass(object o)
        {
            Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>();

            var subclass = _businessLayer.SingleListTbl24SubclassesBySubclassId(CurrentTbl24Subclass.SubclassID);

            Tbl24SubclassesList.Add(new Tbl24Subclass
            {
                ClassID = subclass.ClassID,
                SubclassName = CultRes.StringsRes.DatasetNew,
                Valid =  subclass.Valid,
                ValidYear =  subclass.ValidYear,
                Synonym =  subclass.Synonym,
                Author =  subclass.Author,
                AuthorYear =  subclass.AuthorYear,
                Info =  subclass.Info,
                EngName =  subclass.EngName,
                GerName =  subclass.GerName,
                FraName =  subclass.FraName,
                PorName =  subclass.PorName,
                Memo =  subclass.Memo
            });

            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void SaveSubclass(object o)
        {
            try
            {
                var subclass = _businessLayer.SingleListTbl24SubclassesBySubclassId(CurrentTbl24Subclass.SubclassID);
                if (CurrentTbl24Subclass.SubclassID != 0)
                {
                    if (subclass != null) //update
                    {
                        subclass.SubclassName = CurrentTbl24Subclass.SubclassName;
                        subclass.ClassID = CurrentTbl24Subclass.ClassID;
                        subclass.Valid = CurrentTbl24Subclass.Valid;
                        subclass.ValidYear = CurrentTbl24Subclass.ValidYear;       
                        subclass.Synonym = CurrentTbl24Subclass.Synonym;
                        subclass.Author = CurrentTbl24Subclass.Author;
                        subclass.AuthorYear = CurrentTbl24Subclass.AuthorYear;
                        subclass.Info = CurrentTbl24Subclass.Info;
                        subclass.EngName = CurrentTbl24Subclass.EngName;
                        subclass.GerName = CurrentTbl24Subclass.GerName;
                        subclass.FraName = CurrentTbl24Subclass.FraName;
                        subclass.PorName = CurrentTbl24Subclass.PorName;
                        subclass.Updater = Environment.UserName;
                        subclass.UpdaterDate = DateTime.Now;
                        subclass.Memo = CurrentTbl24Subclass.Memo;
                        subclass.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    subclass = new Tbl24Subclass   //add new
                    {
                        SubclassName = CurrentTbl24Subclass.SubclassName,              
                        ClassID = CurrentTbl24Subclass.ClassID,     
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl24Subclass.Valid,
                        ValidYear = CurrentTbl24Subclass.ValidYear,
                        Synonym = CurrentTbl24Subclass.Synonym,
                        Author = CurrentTbl24Subclass.Author,
                        AuthorYear = CurrentTbl24Subclass.AuthorYear,
                        Info = CurrentTbl24Subclass.Info,
                        EngName = CurrentTbl24Subclass.EngName,
                        GerName = CurrentTbl24Subclass.GerName,
                        FraName = CurrentTbl24Subclass.FraName,
                        PorName = CurrentTbl24Subclass.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl24Subclass.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //ClassID may be not 0
                    if (CurrentTbl24Subclass.ClassID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and ClassId already exist       
                    var dataset = _businessLayer.ListTbl24SubclassesBySubclassNameAndClassId(CurrentTbl24Subclass.SubclassName, CurrentTbl24Subclass.ClassID);

                    if (dataset.Count != 0 && CurrentTbl24Subclass.SubclassID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl24Subclass.SubclassName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl24Subclass.SubclassID == 0 ||
                        dataset.Count != 0 && CurrentTbl24Subclass.SubclassID != 0 ||
                        dataset.Count == 0 && CurrentTbl24Subclass.SubclassID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl24Subclass.SubclassName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateSubclass(subclass);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl24Subclass.SubclassName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl24Subclass.SubclassID == 0)  //new Dataset                        
                Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassName(CurrentTbl24Subclass.SubclassName));
            if (CurrentTbl24Subclass.SubclassID != 0)   //update 
                Tbl24SubclassesList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24SubclassesBySubclassId(CurrentTbl24Subclass.SubclassID));

            SelectedMainTabIndex = 1;
            SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
            SubclassesView.Refresh();
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
                ClassID = CurrentTbl21Class.ClassID
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
                ClassID = reference.ClassID,
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
                ClassID = CurrentTbl21Class.ClassID
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
                ClassID = reference.ClassID,
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
                ClassID = CurrentTbl21Class.ClassID
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
                ClassID = reference.ClassID,
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
                ClassID = CurrentTbl21Class.ClassID
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
                ClassID = comment.ClassID,
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
            Tbl24SubclassesList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();

            Tbl18SuperclassesList =  new ObservableCollection<Tbl18Superclass>(
                    _businessLayer.ListTbl18SuperclassesBySuperclassId(CurrentTbl21Class.SuperclassID));
 

            SuperclassesView = CollectionViewSource.GetDefaultView(Tbl18SuperclassesList);
            SuperclassesView.Refresh();

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
                    Tbl24SubclassesList =  new ObservableCollection<Tbl24Subclass>(
                        _businessLayer.ListTbl24SubclassesByClassId(CurrentTbl21Class.ClassID));

                    SubclassesView = CollectionViewSource.GetDefaultView(Tbl24SubclassesList);
                    SubclassesView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                        _businessLayer.ListTbl90RefExperts());
                    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefExpertsByClassId(CurrentTbl21Class.ClassID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainTabIndex = 2;
                }
                if (_selectedDetailSubTabIndex == 3)
                {
                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(
                        _businessLayer.ListTbl93CommentsByClassId(CurrentTbl21Class.ClassID));

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
                        _businessLayer.ListTbl90ReferenceListRefExpertsByClassId(CurrentTbl21Class.ClassID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(
                        _businessLayer.ListTbl90RefSources());

                    Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefSourcesByClassId(CurrentTbl21Class.ClassID));

                    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                    ReferenceSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(
                        _businessLayer.ListTbl90RefAuthors());

                    Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefAuthorsByClassId(CurrentTbl21Class.ClassID));

                    ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                    ReferenceAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"
 

 //    Part 11    

     
        #region "Public Properties Tbl21Class"

        private string _searchClassName = string.Empty;
        public string SearchClassName
        {
            get => _searchClassName; 
            set { _searchClassName = value; RaisePropertyChanged();  }
        }

        public  ICollectionView ClassesView;
        private   Tbl21Class CurrentTbl21Class => ClassesView?.CurrentItem as Tbl21Class;

        private ObservableCollection<Tbl21Class> _tbl21ClassesList;
        public  ObservableCollection<Tbl21Class> Tbl21ClassesList
        {
            get => _tbl21ClassesList; 
            set {  _tbl21ClassesList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;
        public  ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList; 
            set {  _tbl21ClassesAllList = value; RaisePropertyChanged();   }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl18Superclass"

        private string _searchSuperclassName = string.Empty;
        public new string SearchSuperclassName
        {
            get  => _searchSuperclassName; 
            set { _searchSuperclassName = value; RaisePropertyChanged(); }
        }

        public new ICollectionView SuperclassesView;
        private Tbl18Superclass CurrentTbl18Superclass => SuperclassesView?.CurrentItem as Tbl18Superclass;           

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesList;
        public new ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
        {
            get => _tbl18SuperclassesList; 
            set { _tbl18SuperclassesList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public  ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList; 
            set { _tbl18SuperclassesAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl24Subclass"

        private string _searchSubclassName = string.Empty;
        public string SearchSubclassName
        {
            get => _searchSubclassName; 
            set { _searchSubclassName = value; RaisePropertyChanged(); }
        }

        public ICollectionView SubclassesView;
        private Tbl24Subclass CurrentTbl24Subclass => SubclassesView?.CurrentItem as Tbl24Subclass;           

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesList
        {
            get => _tbl24SubclassesList; 
            set { _tbl24SubclassesList = value; RaisePropertyChanged(); }
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
