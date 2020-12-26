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

    
         //    Tbl06PhylumsViewModel Skriptdatum:  13.06.2018  12:32    

namespace Te.Atis.Ui.Desktop.Views.Database
{     
    
    public class Tbl06PhylumsViewModel : Tbl03RegnumsViewModel
    {     
         
        #region "Private Data Members"

        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;   
         
        #endregion "Private Data Members"               
      
        #region "Constructor"

        public Tbl06PhylumsViewModel()
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

    
        #region "Public Commands Basic Tbl06Phylum"
        //-------------------------------------------------------------------------
        private RelayCommand _clearPhylumCommand;

        public new  ICommand ClearPhylumCommand => _clearPhylumCommand ??
                                                  (_clearPhylumCommand = new RelayCommand(delegate { ClearPhylum(null); }));         
    
        private RelayCommand _getPhylumsByNameOrIdCommand;  

        public new ICommand GetPhylumsByNameOrIdCommand => _getPhylumsByNameOrIdCommand ??
                                                           (_getPhylumsByNameOrIdCommand = new RelayCommand(delegate { GetPhylumsByNameOrId(null); }));        
    
        private RelayCommand _addPhylumCommand;

        public new ICommand AddPhylumCommand => _addPhylumCommand ??
                                                (_addPhylumCommand = new RelayCommand(delegate { AddPhylum(null); }));

        private RelayCommand _copyPhylumCommand;

        public new ICommand CopyPhylumCommand => _copyPhylumCommand ??
                                                 (_copyPhylumCommand = new RelayCommand(delegate { CopyPhylum(null); }));       
             
        private RelayCommand _deletePhylumCommand;

        public ICommand DeletePhylumCommand => _deletePhylumCommand ??
                                                   (_deletePhylumCommand = new RelayCommand(delegate { DeletePhylum(null); }));    
    
        private RelayCommand _savePhylumCommand;

        public new ICommand SavePhylumCommand => _savePhylumCommand ??
                                                 (_savePhylumCommand = new RelayCommand(delegate { SavePhylum(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearPhylum(object o)
        {
            SearchPhylumName = string.Empty;

            Tbl03RegnumsList?.Clear();
            Tbl06PhylumsList?.Clear();
            Tbl12SubphylumsList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();
        }
        //----------------------------------------------------------------------                  
     
        private void GetPhylumsByNameOrId(object o)
        {
            Tbl03RegnumsList?.Clear();
            Tbl12SubphylumsList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();

            Tbl06PhylumsList = int.TryParse(SearchPhylumName, out var id) ?
                new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumId(id)) :
                new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumName(SearchPhylumName));

            Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03Regnums());

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();
        }
        //------------------------------------------------------------------------------------                          
     
        private void AddPhylum(object o)
        {
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum> {new Tbl06Phylum
            {
                PhylumName = CultRes.StringsRes.DatasetNew,
                RegnumID = CurrentTbl06Phylum.RegnumID
            }  };

            Tbl03RegnumsAllList?.Clear();
            Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03Regnums());

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                               
     
        private void CopyPhylum(object o)
        {
            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>();

            var phylum = _businessLayer.SingleListTbl06PhylumsByPhylumId(CurrentTbl06Phylum.PhylumID);

            Tbl06PhylumsList.Add(new Tbl06Phylum
            {
                 RegnumID =  phylum. RegnumID,
                 PhylumName = CultRes.StringsRes.DatasetNew,
                Valid =  phylum.Valid,
                ValidYear =  phylum.ValidYear,
                Synonym =  phylum.Synonym,
                Author =  phylum.Author,
                AuthorYear =  phylum.AuthorYear,
                Info =  phylum.Info,
                EngName =  phylum.EngName,
                GerName =  phylum.GerName,
                FraName =  phylum.FraName,
                PorName =  phylum.PorName,
                Memo =  phylum.Memo
            });

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.MoveCurrentToFirst();
        }
        //---------------------------------------------------------------------------------------                            
     
        private void DeletePhylum(object o)
        {
            try
            {
                var phylum = _businessLayer.SingleListTbl06PhylumsByPhylumId(CurrentTbl06Phylum.PhylumID);
                if (phylum != null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl06Phylum.PhylumName,
                            MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                        return;
                    phylum.EntityState = EntityState.Deleted;
                    _businessLayer.RemovePhylum(phylum);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, CurrentTbl06Phylum.PhylumName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + CurrentTbl06Phylum.PhylumName + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumName(SearchPhylumName));

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                    
     
        private void SavePhylum(object o)
        {
            try
            {
                var phylum = _businessLayer.SingleListTbl06PhylumsByPhylumId(CurrentTbl06Phylum.PhylumID);
                if (CurrentTbl06Phylum.PhylumID != 0)
                {
                    if (phylum != null) //update
                    {
                        phylum.PhylumName = CurrentTbl06Phylum.PhylumName;
                        phylum.RegnumID = CurrentTbl06Phylum.RegnumID;
                        phylum.Valid = CurrentTbl06Phylum.Valid;
                        phylum.ValidYear = CurrentTbl06Phylum.ValidYear;       
                        phylum.Synonym = CurrentTbl06Phylum.Synonym;
                        phylum.Author = CurrentTbl06Phylum.Author;
                        phylum.AuthorYear = CurrentTbl06Phylum.AuthorYear;
                        phylum.Info = CurrentTbl06Phylum.Info;
                        phylum.EngName = CurrentTbl06Phylum.EngName;
                        phylum.GerName = CurrentTbl06Phylum.GerName;
                        phylum.FraName = CurrentTbl06Phylum.FraName;
                        phylum.PorName = CurrentTbl06Phylum.PorName;
                        phylum.Updater = Environment.UserName;
                        phylum.UpdaterDate = DateTime.Now;
                        phylum.Memo = CurrentTbl06Phylum.Memo;
                        phylum.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    phylum = new Tbl06Phylum   //add new
                    {
                        PhylumName = CurrentTbl06Phylum.PhylumName,
                        RegnumID = CurrentTbl06Phylum.RegnumID,

                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl06Phylum.Valid,
                        ValidYear = CurrentTbl06Phylum.ValidYear,
                        Synonym = CurrentTbl06Phylum.Synonym,
                        Author = CurrentTbl06Phylum.Author,
                        AuthorYear = CurrentTbl06Phylum.AuthorYear,
                        Info = CurrentTbl06Phylum.Info,
                        EngName = CurrentTbl06Phylum.EngName,
                        GerName = CurrentTbl06Phylum.GerName,
                        FraName = CurrentTbl06Phylum.FraName,
                        PorName = CurrentTbl06Phylum.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl06Phylum.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //RegnumID may be not 0
                    if (CurrentTbl06Phylum.RegnumID == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and RegnumId already exist       
                    var dataset = _businessLayer.ListTbl06PhylumsByPhylumNameAndRegnumId(CurrentTbl06Phylum.PhylumName, CurrentTbl06Phylum.RegnumID);

                    if (dataset.Count != 0 && CurrentTbl06Phylum.PhylumID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl06Phylum.PhylumName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl06Phylum.PhylumID == 0 ||
                        dataset.Count != 0 && CurrentTbl06Phylum.PhylumID != 0 ||
                        dataset.Count == 0 && CurrentTbl06Phylum.PhylumID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl06Phylum.PhylumName,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdatePhylum(phylum);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl06Phylum.PhylumName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl06Phylum.PhylumID == 0)  //new Dataset                        
                Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByEmail(CurrentTbl06Phylum.Email));
            if (CurrentTbl06Phylum.PhylumID != 0)   //update 
                Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06PhylumsByPhylumId(CurrentTbl06Phylum.PhylumID));

            PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            PhylumsView.Refresh();
        }
        #endregion "Public Commands"                  
 
 

 //    Part 2    

           
        #region "Public Commands Connect <== Tbl03Regnum"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearRegnumCommand;

        public new ICommand ClearRegnumCommand => _clearRegnumCommand ??
                                                  (_clearRegnumCommand = new RelayCommand(delegate { ClearRegnum(null); }));

        private RelayCommand _getRegnumsByNameOrIdCommand;  

        public new ICommand GetRegnumsByNameOrIdCommand => _getRegnumsByNameOrIdCommand ??
                                                           (_getRegnumsByNameOrIdCommand = new RelayCommand(delegate { GetRegnumsByNameOrId(null); }));

        private RelayCommand _addRegnumCommand;

        public new ICommand AddRegnumCommand => _addRegnumCommand ??
                                                (_addRegnumCommand = new RelayCommand(delegate { AddRegnum(null); }));

        private RelayCommand _copyRegnumCommand;

        public new ICommand CopyRegnumCommand => _copyRegnumCommand ??
                                                 (_copyRegnumCommand = new RelayCommand(delegate { CopyRegnum(null); }));

        private RelayCommand _saveRegnumCommand;

        public new ICommand SaveRegnumCommand => _saveRegnumCommand ??
                                                 (_saveRegnumCommand = new RelayCommand(delegate { SaveRegnum(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearRegnum(object o)
        {
            SearchRegnumName = string.Empty;
            Tbl03RegnumsList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        private void GetRegnumsByNameOrId(object o)
        {
            Tbl03RegnumsList = int.TryParse(SearchRegnumName, out var id) ?
                new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumId(id)) :
                new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumName(SearchRegnumName));

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        private void AddRegnum(object o)      
        {
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum> {new Tbl03Regnum
            {   RegnumName = CultRes.StringsRes.DatasetNew    }  };

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
      
        private void CopyRegnum(object o)
        {
            Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>();

            var regnum = _businessLayer.SingleListTbl03RegnumsByRegnumId(CurrentTbl03Regnum.RegnumID);

            Tbl03RegnumsList.Add(new Tbl03Regnum
            {
                 RegnumName = CultRes.StringsRes.DatasetNew,
                Subregnum = regnum.Subregnum,
                Valid =  regnum.Valid,
                ValidYear =  regnum.ValidYear,
                Synonym =  regnum.Synonym,
                Author =  regnum.Author,
                AuthorYear =  regnum.AuthorYear,
                Info =  regnum.Info,
                EngName =  regnum.EngName,
                GerName =  regnum.GerName,
                FraName =  regnum.FraName,
                PorName =  regnum.PorName,
                Memo =  regnum.Memo
            });

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void SaveRegnum(object o)
        {
            try
            {
                var regnum = _businessLayer.SingleListTbl03RegnumsByRegnumId(CurrentTbl03Regnum.RegnumID);
                if (CurrentTbl03Regnum.RegnumID != 0)
                {
                    if (regnum != null) //update
                    {
                        regnum.RegnumName = CurrentTbl03Regnum.RegnumName;
                        regnum.Subregnum = CurrentTbl03Regnum.Subregnum;
                        regnum.Valid = CurrentTbl03Regnum.Valid;
                        regnum.ValidYear = CurrentTbl03Regnum.ValidYear;       
                        regnum.Synonym = CurrentTbl03Regnum.Synonym;
                        regnum.Author = CurrentTbl03Regnum.Author;
                        regnum.AuthorYear = CurrentTbl03Regnum.AuthorYear;
                        regnum.Info = CurrentTbl03Regnum.Info;
                        regnum.EngName = CurrentTbl03Regnum.EngName;
                        regnum.GerName = CurrentTbl03Regnum.GerName;
                        regnum.FraName = CurrentTbl03Regnum.FraName;
                        regnum.PorName = CurrentTbl03Regnum.PorName;
                        regnum.Updater = Environment.UserName;
                        regnum.UpdaterDate = DateTime.Now;
                        regnum.Memo = CurrentTbl03Regnum.Memo;
                        regnum.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    regnum = new Tbl03Regnum   //add new
                    {
                        RegnumName = CurrentTbl03Regnum.RegnumName,
                        Subregnum = CurrentTbl03Regnum.Subregnum,
                        CountID = RandomHelper.Randomnumber(),
                        Valid = CurrentTbl03Regnum.Valid,
                        ValidYear = CurrentTbl03Regnum.ValidYear,
                        Synonym = CurrentTbl03Regnum.Synonym,
                        Author = CurrentTbl03Regnum.Author,
                        AuthorYear = CurrentTbl03Regnum.AuthorYear,
                        Info = CurrentTbl03Regnum.Info,
                        EngName = CurrentTbl03Regnum.EngName,
                        GerName = CurrentTbl03Regnum.GerName,
                        FraName = CurrentTbl03Regnum.FraName,
                        PorName = CurrentTbl03Regnum.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = CurrentTbl03Regnum.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //check if dataset with Name already exist       
                    var dataset = _businessLayer.ListTbl03RegnumsByRegnumNameAndSubregnum(CurrentTbl03Regnum.RegnumName, CurrentTbl03Regnum.Subregnum);

                    if (dataset.Count != 0 && CurrentTbl03Regnum.RegnumID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl03Regnum.RegnumName  + " " +  CurrentTbl03Regnum.Subregnum,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    if (dataset.Count == 0 && CurrentTbl03Regnum.RegnumID == 0 ||
                        dataset.Count != 0 && CurrentTbl03Regnum.RegnumID != 0 ||
                        dataset.Count == 0 && CurrentTbl03Regnum.RegnumID != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, CurrentTbl03Regnum.RegnumName  + " " +  CurrentTbl03Regnum.Subregnum,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            _businessLayer.UpdateRegnum(regnum);

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl03Regnum.RegnumName  + " " +  CurrentTbl03Regnum.Subregnum,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl03Regnum.RegnumID == 0)  //new Dataset                        
                Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumNameAndSubregnum(CurrentTbl03Regnum.RegnumName, CurrentTbl03Regnum.Subregnum));
            if (CurrentTbl03Regnum.RegnumID != 0)   //update 
                Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03RegnumsByRegnumId(CurrentTbl03Regnum.RegnumID));

            SelectedMainTabIndex = 0;
            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.Refresh();
        }
        #endregion "Public Commands"                  
 
            

 //    Part 3    

 


 //    Part 4    

           
        #region "Public Commands Connect <== Tbl12Subphylum"                 
        //-------------------------------------------------------------------------
        private RelayCommand _clearSubphylumCommand;

        public ICommand ClearSubphylumCommand => _clearSubphylumCommand ??
                                                  (_clearSubphylumCommand = new RelayCommand(delegate { ClearSubphylum(null); }));

        private RelayCommand _getSubphylumsByNameOrIdCommand;  

        public  ICommand GetSubphylumsByNameOrIdCommand => _getSubphylumsByNameOrIdCommand ??
                                                           (_getSubphylumsByNameOrIdCommand = new RelayCommand(delegate { GetSubphylumsByNameOrId(null); }));

        private RelayCommand _addSubphylumCommand;

        public ICommand AddSubphylumCommand => _addSubphylumCommand ??
                                                (_addSubphylumCommand = new RelayCommand(delegate { AddSubphylum(null); }));

        private RelayCommand _copySubphylumCommand;

        public ICommand CopySubphylumCommand => _copySubphylumCommand ??
                                                 (_copySubphylumCommand = new RelayCommand(delegate { CopySubphylum(null); }));

        private RelayCommand _saveSubphylumCommand;

        public ICommand SaveSubphylumCommand => _saveSubphylumCommand ??
                                                 (_saveSubphylumCommand = new RelayCommand(delegate { SaveSubphylum(null); }));

        //-------------------------------------------------------------------------          
     
        private void ClearSubphylum(object o)
        {
            SearchSubphylumName = string.Empty;
            Tbl12SubphylumsList?.Clear();
        }
        //----------------------------------------------------------------------            
     
        private void GetSubphylumsByNameOrId(object o)
        {
            Tbl12SubphylumsList = int.TryParse(SearchSubphylumName, out var id) ?
                new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumId(id)) :
                new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumName(SearchSubphylumName));

            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.Refresh();
        }
        //----------------------------------------------------------------------            
     
        private void AddSubphylum(object o)      
        {
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum> {new Tbl12Subphylum
            {  
                SubphylumName = CultRes.StringsRes.DatasetNew,
                PhylumID = CurrentTbl06Phylum.PhylumID
            }    };

            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void CopySubphylum(object o)
        {
            Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>();

            var subphylum = _businessLayer.SingleListTbl12SubphylumsBySubphylumId(CurrentTbl12Subphylum.SubphylumID);

            Tbl12SubphylumsList.Add(new Tbl12Subphylum
            {
                PhylumID = subphylum.PhylumID,
                SubphylumName = CultRes.StringsRes.DatasetNew,
                Valid =  subphylum.Valid,
                ValidYear =  subphylum.ValidYear,
                Synonym =  subphylum.Synonym,
                Author =  subphylum.Author,
                AuthorYear =  subphylum.AuthorYear,
                Info =  subphylum.Info,
                EngName =  subphylum.EngName,
                GerName =  subphylum.GerName,
                FraName =  subphylum.FraName,
                PorName =  subphylum.PorName,
                Memo =  subphylum.Memo
            });

            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------            
     
        private void SaveSubphylum(object o)
        {
            try
            {
                var subphylum = _businessLayer.SingleListTbl12SubphylumsBySubphylumId(CurrentTbl12Subphylum.SubphylumID);
                if (CurrentTbl12Subphylum.SubphylumID != 0)
                {
                    if (subphylum != null) //update
                    {
                        subphylum.SubphylumName = CurrentTbl12Subphylum.SubphylumName;
                        subphylum.PhylumID = CurrentTbl12Subphylum.PhylumID;
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

                    //check if dataset with Name and PhylumId already exist       
                    var dataset = _businessLayer.ListTbl12SubphylumsBySubphylumNameAndPhylumId(CurrentTbl12Subphylum.SubphylumName, CurrentTbl12Subphylum.PhylumID);

                    if (dataset.Count != 0 && CurrentTbl12Subphylum.SubphylumID == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, CurrentTbl12Subphylum.SubphylumName,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
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

                            WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess, CurrentTbl12Subphylum.SubphylumName,
                                MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
            }

            if (CurrentTbl12Subphylum.SubphylumID == 0)  //new Dataset                        
                Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumName(CurrentTbl12Subphylum.SubphylumName));
            if (CurrentTbl12Subphylum.SubphylumID != 0)   //update 
                Tbl12SubphylumsList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12SubphylumsBySubphylumId(CurrentTbl12Subphylum.SubphylumID));

            SelectedMainTabIndex = 1;
            SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
            SubphylumsView.Refresh();
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
                PhylumID = CurrentTbl06Phylum.PhylumID
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
                PhylumID = reference.PhylumID,
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
                PhylumID = CurrentTbl06Phylum.PhylumID
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
                PhylumID = reference.PhylumID,
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
                PhylumID = CurrentTbl06Phylum.PhylumID
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
                PhylumID = reference.PhylumID,
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
                PhylumID = CurrentTbl06Phylum.PhylumID
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
                PhylumID = comment.PhylumID,
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
            Tbl12SubphylumsList?.Clear();
            Tbl90ReferenceExpertsList?.Clear();
            Tbl90ReferenceSourcesList?.Clear();
            Tbl90ReferenceAuthorsList?.Clear();
            Tbl93CommentsList?.Clear();

            Tbl03RegnumsList =  new ObservableCollection<Tbl03Regnum>(
                    _businessLayer.ListTbl03RegnumsByRegnumId(CurrentTbl06Phylum.RegnumID));
 

            RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            RegnumsView.Refresh();

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
                    Tbl12SubphylumsList =  new ObservableCollection<Tbl12Subphylum>(
                        _businessLayer.ListTbl12SubphylumsByPhylumId(CurrentTbl06Phylum.PhylumID));

                    SubphylumsView = CollectionViewSource.GetDefaultView(Tbl12SubphylumsList);
                    SubphylumsView.Refresh();

                    SelectedMainTabIndex = 1;
                }
                if (_selectedDetailSubTabIndex == 2)
                {
                    Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(
                        _businessLayer.ListTbl90RefExperts());
                    Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefExpertsByPhylumId(CurrentTbl06Phylum.PhylumID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainTabIndex = 2;
                }
                if (_selectedDetailSubTabIndex == 3)
                {
                    Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(
                        _businessLayer.ListTbl93CommentsByPhylumId(CurrentTbl06Phylum.PhylumID));

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
                        _businessLayer.ListTbl90ReferenceListRefExpertsByPhylumId(CurrentTbl06Phylum.PhylumID));

                    ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                    ReferenceExpertsView.Refresh();

                    SelectedMainSubRefTabIndex = 0;
                }
                if (_selectedDetailSubRefTabIndex == 1)
                {
                    Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(
                        _businessLayer.ListTbl90RefSources());

                    Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefSourcesByPhylumId(CurrentTbl06Phylum.PhylumID));

                    ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                    ReferenceSourcesView.Refresh();

                    SelectedMainSubRefTabIndex = 1;
                }
                if (_selectedDetailSubRefTabIndex == 2)
                {
                    Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(
                        _businessLayer.ListTbl90RefAuthors());

                    Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(
                        _businessLayer.ListTbl90ReferenceListRefAuthorsByPhylumId(CurrentTbl06Phylum.PhylumID));

                    ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                    ReferenceAuthorsView.Refresh();

                    SelectedMainSubRefTabIndex = 2;
                }
            }
        }

        #endregion "Public Commands to open Detail TabItems"
 

 //    Part 11    

      
        #region "Public Properties Tbl06Phylum"

        private string _searchPhylumName = string.Empty;
        public new string SearchPhylumName
        {
            get => _searchPhylumName; 
            set { _searchPhylumName = value; RaisePropertyChanged();  }
        }

        public new  ICollectionView PhylumsView;
        private   Tbl06Phylum CurrentTbl06Phylum => PhylumsView?.CurrentItem as Tbl06Phylum;

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList;
        public  new ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
        {
            get => _tbl06PhylumsList; 
            set {  _tbl06PhylumsList = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public  new ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList; 
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"   
       
        #region "Public Properties Tbl03Regnum"

        private string _searchRegnumName = string.Empty;
        public new string SearchRegnumName
        {
            get  => _searchRegnumName; 
            set { _searchRegnumName = value; RaisePropertyChanged(); }
        }

        public new ICollectionView RegnumsView;
        private Tbl03Regnum CurrentTbl03Regnum => RegnumsView?.CurrentItem as Tbl03Regnum;           

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;
        public new ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        {
            get => _tbl03RegnumsList; 
            set { _tbl03RegnumsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public  ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList; 
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(); }       
        }

        #endregion "Public Properties"   
        
        #region "Public Properties Tbl12Subphylum"

        private string _searchSubphylumName = string.Empty;
        public string SearchSubphylumName
        {
            get => _searchSubphylumName; 
            set { _searchSubphylumName = value; RaisePropertyChanged(); }
        }

        public ICollectionView SubphylumsView;
        private Tbl12Subphylum CurrentTbl12Subphylum => SubphylumsView?.CurrentItem as Tbl12Subphylum;           

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
        {
            get => _tbl12SubphylumsList; 
            set { _tbl12SubphylumsList = value; RaisePropertyChanged(); }
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
