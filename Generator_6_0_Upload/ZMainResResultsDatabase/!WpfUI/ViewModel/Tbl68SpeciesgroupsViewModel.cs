using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using DAL.Helper;
using DAL.Models;
using DAL.Repositories.Tbl03Regnums;
using DAL.Repositories.Tbl06Phylums;
using DAL.Repositories.Tbl09Divisions;
using DAL.Repositories.Tbl90RefAuthors;
using DAL.Repositories.Tbl90References;
using DAL.Repositories.Tbl90RefExperts;
using DAL.Repositories.Tbl90RefSources;
using DAL.Repositories.Tbl93Comments;
using DAL.Repositories.TblCounters;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

//    Tbl68SpeciesgroupsViewModel Skriptdatum:  08.04.2014  10:32    

namespace WPFUI.Views.Database
{   


    
    public class Tbl68SpeciesgroupsViewModel : Tbl03RegnumsViewModel
    {     
        
        #region "Private Data Members"
 
        protected readonly Tbl68SpeciesgroupsRepository Tbl68SpeciesgroupsRepository = new Tbl68SpeciesgroupsRepository();    
    

        #endregion "Private Data Members"            
    
        #region "Constructor"

        public Tbl68SpeciesgroupsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
        }
        private new bool IsInDesignMode { get; set; }

        #endregion "Constructor"           
 

 //    Part 1    

       
        #region "Public Commands Basic Tbl68Speciesgroup"

        private RelayCommand _getSpeciesgroupByNameCommand;
        public new ICommand GetSpeciesgroupByNameCommand
        {
            get { return _getSpeciesgroupByNameCommand ?? (_getSpeciesgroupByNameCommand = new RelayCommand(delegate { GetSpeciesgroupByNameOrId(null); })); }   
        }

        private void GetSpeciesgroupByNameOrId(object o)       
        {   
Tbl68SpeciesgroupsList =  new ObservableCollection<Tbl68Speciesgroup>
                                                       (from x in Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups
                                                        where x.SpeciesgroupName.StartsWith(SearchSpeciesgroupName)
                                                        orderby x.SpeciesgroupName
                                                        select x);

            Tbl68SpeciesgroupsAllList =  new ObservableCollection<Tbl68Speciesgroup>
                                                       (from y in Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups
                                                        orderby y.SpeciesgroupName
                                                        select y);
  
SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            if (SpeciesgroupsView != null)
                SpeciesgroupsView.CurrentChanged += tbl68SpeciesgroupView_CurrentChanged;                   
            RaisePropertyChanged();
        }
        //------------------------------------------------------------------------------
        private RelayCommand _addSpeciesgroupCommand;
        public new ICommand AddSpeciesgroupCommand
        {
            get { return _addSpeciesgroupCommand ?? (_addSpeciesgroupCommand = new RelayCommand(AddSpeciesgroup)); }
        }

        private void AddSpeciesgroup(object o)
        {
            if (Tbl68SpeciesgroupsList == null)
                Tbl68SpeciesgroupsList = new ObservableCollection<Tbl68Speciesgroup>();
            Tbl68SpeciesgroupsList.Add(new Tbl68Speciesgroup{ SpeciesgroupName= CultRes.StringsRes.DatasetNew });
            SpeciesgroupsView = CollectionViewSource.GetDefaultView(Tbl68SpeciesgroupsList);
            if (SpeciesgroupsView != null)
                SpeciesgroupsView.CurrentChanged += tbl68SpeciesgroupView_CurrentChanged;
            RaisePropertyChanged();
        }
        //---------------------------------------------------------------------------------------
  
       
        private RelayCommand _deleteSpeciesgroupCommand;
        public new ICommand DeleteSpeciesgroupCommand
        {
            get { return _deleteSpeciesgroupCommand ?? (_deleteSpeciesgroupCommand = new RelayCommand(delegate { DeleteSpeciesgroup(null); })); }
        }

        private void DeleteSpeciesgroup(object o)
        {
            try
            {
                var speciesgroup= Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups.FirstOrDefault(x => x.SpeciesgroupID== CurrentTbl68Speciesgroup.SpeciesgroupID);
                if (speciesgroup!= null)
                {
                    if (MessageBox.Show(CultRes.StringsRes.DeleteQuestion
                                        + " " +  CurrentTbl68Speciesgroup.SpeciesgroupName, CultRes.StringsRes.DeleteQuestion1, MessageBoxButton.YesNo) !=
                        MessageBoxResult.Yes) return;
                    Tbl68SpeciesgroupsRepository.Delete(speciesgroup);
                    Tbl68SpeciesgroupsRepository.Save();
                    MessageBox.Show(CurrentTbl68Speciesgroup.SpeciesgroupName + " " + CultRes.StringsRes.DeleteSuccess);
                    GetSpeciesgroupByNameOrId(o); //Refresh
                }
                else
                {
                    MessageBox.Show(CultRes.StringsRes.DeleteCan + " " + CurrentTbl68Speciesgroup.SpeciesgroupName+ " " + CultRes.StringsRes.DeleteCan1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------------------------------------------------------------------
        private RelayCommand _saveSpeciesgroupCommand;
        public new ICommand SaveSpeciesgroupCommand
        {
            get { return _saveSpeciesgroupCommand ?? (_saveSpeciesgroupCommand = new RelayCommand(delegate { SaveSpeciesgroup(null); })); }
        }

        private void SaveSpeciesgroup(objext o)
        {
            try
            {
                var speciesgroup= Tbl68SpeciesgroupsRepository.Tbl68Speciesgroups.FirstOrDefault(x => x.SpeciesgroupID== CurrentTbl68Speciesgroup.SpeciesgroupID);
                if (CurrentTbl68Speciesgroup == null)
                {
                    MessageBox.Show(CultRes.StringsRes.DatasetNotExist);
                }
                else
                {
                    if (CurrentTbl68Speciesgroup.SpeciesgroupID!= 0)
                    {
                        if (speciesgroup!= null) //update
                        {
                            speciesgroup.Updater = Environment.UserName;
                            speciesgroup.UpdaterDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        Tbl68SpeciesgroupsRepository.Add(new Tbl68Speciesgroup
                        {
                            SpeciesgroupName= CurrentTbl68Speciesgroup.SpeciesgroupName,              
                            CountID = RandomHelper.Randomnumber(),
                            Valid = CurrentTbl68Speciesgroup.Valid,
                            ValidYear = CurrentTbl68Speciesgroup.ValidYear,
                            Synonym = CurrentTbl68Speciesgroup.Synonym,
                            Author = CurrentTbl68Speciesgroup.Author,
                            AuthorYear = CurrentTbl68Speciesgroup.AuthorYear,
                            Info = CurrentTbl68Speciesgroup.Info,
                            EngName = CurrentTbl68Speciesgroup.EngName,
                            GerName = CurrentTbl68Speciesgroup.GerName,
                            FraName = CurrentTbl68Speciesgroup.FraName,
                            PorName = CurrentTbl68Speciesgroup.PorName,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl68Speciesgroup.Memo
                        });
                    }
                    {
                        Tbl68SpeciesgroupsRepository.Save();
                        MessageBox.Show(CurrentTbl68Speciesgroup.SpeciesgroupName+ " " + CultRes.StringsRes.SaveSuccess);
                        GetSpeciesgroupByNameOrId(o);  //Refresh
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion "Public Commands"  
    

 //    Part 2    

    

 //    Part 3    

    

 //    Part 4    

    

 //    Part 5    

    

 //    Part 6    

 

 //    Part 7    

 

 //    Part 8    

    

 //    Part 9    


 

 //    Part 10    

 

 //    Part 11    


    
     
        #region "Public Properties Tbl68Speciesgroup"

        public new ICollectionView SpeciesgroupsView;
        public new Tbl68Speciesgroup CurrentTbl68Speciesgroup
        {
            get
            {
                if (SpeciesgroupsView != null)
                    return SpeciesgroupsView.CurrentItem as Tbl68Speciesgroup;
                return null;
            }
        }
        //--------------------------------------------
        private string _searchSpeciesgroupName;
        public new string SearchSpeciesgroupName
        {
            get { return _searchSpeciesgroupName; }
            set
            {
                if (value == _searchSpeciesgroupName) return;
                _searchSpeciesgroupName = value;
                RaisePropertyChanged("SearchSpeciesgroupName");
            }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsList;
        public new ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsList
        {
            get { return _tbl68SpeciesgroupsList; }
            set
            {
                if (_tbl68SpeciesgroupsList == value) return;
                _tbl68SpeciesgroupsList = value;
                RaisePropertyChanged("Tbl68SpeciesgroupsList");
            }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get { return _tbl68SpeciesgroupsAllList; }
            set
            {
                if (_tbl68SpeciesgroupsAllList == value) return;
                _tbl68SpeciesgroupsAllList = value;
                RaisePropertyChanged("Tbl68SpeciesgroupsAllList");
            }
        }

        #endregion "Public Properties"   
 

 //    Part 11    

  
        #region "Private Methods"

        public void tbl68SpeciesgroupView_CurrentChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged("CurrentTbl68Speciesgroup");
        }   
  
         #endregion "Private Methods"  
      }
}   
