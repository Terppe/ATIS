using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Core.Repositories_Dapper;
using ATIS.Ui.Helper;

namespace ATIS.Ui.Views.Database.D06Phylum
{
    public class PhylumsViewModel2 : ViewModelBase
    {
        /*
         *  Version mit Dapper
         */

        #region [ Constructor ]

        public override string Name => "Phylum 2";
        private readonly RegnumRepository _regnumsDapper = new RegnumRepository();
        private readonly PhylumRepository _phylumsDapper = new PhylumRepository();


        public PhylumsViewModel2()
        {
            RegnumsCollection = new ObservableCollection<Tbl03Regnum>(_regnumsDapper.GetAll());
        }

        #endregion

        #region [Commands Phylum]

        private RelayCommand _getPhylumsByNameOrIdCommand;

        public ICommand GetPhylumsByNameOrIdCommand => _getPhylumsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetPhylumsByNameOrId(SearchPhylumName); });

        private RelayCommand _addPhylumCommand;
        public ICommand AddPhylumCommand => _addPhylumCommand ??= new RelayCommand(delegate { ExecuteAddPhylum(null); });

        private RelayCommand _copyPhylumCommand;

        public ICommand CopyPhylumCommand => _copyPhylumCommand ??= new RelayCommand(delegate { ExecuteCopyPhylum(null); });

        private RelayCommand _deletePhylumCommand;

        public ICommand DeletePhylumCommand => _deletePhylumCommand ??= new RelayCommand(delegate { ExecuteDeletePhylum(null); });

        private RelayCommand _savePhylumCommand;

        public ICommand SavePhylumCommand => _savePhylumCommand ??= new RelayCommand(delegate { ExecuteSavePhylum(null); });

        #endregion

        #region [Methods Phylum]

        public void ExecuteGetPhylumsByNameOrId(string searchName)
        {
            PhylumsCollection = SearchNameReturnPhylumsCollection(SearchPhylumName);
            RaisePropertyChanged("PhylumsCollection");
        }

        private void ExecuteAddPhylum(object o)
        {
            if (PhylumsCollection == null)
                PhylumsCollection = new ObservableCollection<Tbl06Phylum>();


            RegnumsCollection = new ObservableCollection<Tbl03Regnum>(_regnumsDapper.GetAll());


            PhylumsCollection.Insert(0, new Tbl06Phylum { PhylumName = "DatasetNew" });
        }

        private void ExecuteCopyPhylum(object o)
        {
            if (_phylumsDapper != null)
            {
                if (SelectedPhylum == null)
                {
                    MessageBox.Show("NewDataset",
                        "RequiredInput",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var phylum = _phylumsDapper.GetById(SelectedPhylum.PhylumId);

                PhylumsCollection.Insert(0, new Tbl06Phylum()
                {
                    PhylumName = "NewDataset",
                    RegnumId = phylum.RegnumId,
                    Valid = phylum.Valid,
                    ValidYear = phylum.ValidYear,
                    Synonym = phylum.Synonym,
                    Author = phylum.Author,
                    AuthorYear = phylum.AuthorYear,
                    Info = phylum.Info,
                    EngName = phylum.EngName,
                    GerName = phylum.GerName,
                    FraName = phylum.FraName,
                    PorName = phylum.PorName,
                    Memo = phylum.Memo
                });
            }
        }

        private void ExecuteDeletePhylum(object o)
        {
            if (SelectedPhylum == null) //No dataset selected 
            {
                MessageBox.Show("Select Phylum",
                    "Required select",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            if (SelectedPhylum != null && SelectedPhylum.PhylumId == 0)
            {
                UpdateCollection();
                return;
            }
            else
            {
                if (SelectedPhylum != null) _regnumsDapper.Dispose();

                _regnumsDapper.Commit();
                UpdateCollection();
            }
        }

        private void ExecuteSavePhylum(object o)
        {
            if (SelectedPhylum == null) //No dataset selected
            {
                MessageBox.Show("Select Phylum",
                "Required select",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            if (SelectedPhylum != null)
            {
                if (SelectedPhylum.PhylumId != 0)  //update
                {
                    _phylumsDapper.Update(SelectedPhylum);
                }
                else
                {
                    _phylumsDapper.Insert(SelectedPhylum);
                }
            }

            _phylumsDapper.Commit();
            UpdateCollection();
        }

        private void UpdateCollection()
        {
            PhylumsCollection.Clear();
            foreach (Tbl06Phylum phylum in _phylumsDapper.GetAll())
            {
                PhylumsCollection.Add(phylum);
            }
        }

        #endregion

        #region [ Properties ]

        public ObservableCollection<Tbl06Phylum> SearchNameReturnPhylumsCollection(string searchName)
        {
            var phylumsCollection = new ObservableCollection<Tbl06Phylum>();

            switch (searchName)
            {
                case "":
                    return phylumsCollection;
                case "*":
                    if (_phylumsDapper != null)
                        phylumsCollection = new ObservableCollection<Tbl06Phylum>(_phylumsDapper.GetAll());
                    break;
                default:
                    if (_phylumsDapper != null)
                        phylumsCollection = int.TryParse(searchName, out var id)
                            ? new ObservableCollection<Tbl06Phylum>(_phylumsDapper.GetAll().Where(e => e.PhylumId == id))
                            : new ObservableCollection<Tbl06Phylum>(_phylumsDapper.GetAll()
                                .Where(e => searchName != null && e.PhylumName.StartsWith(searchName))
                                .OrderBy(a => a.PhylumName)
                            );
                    break;
            }
            return phylumsCollection;
        }
        public ObservableCollection<Tbl06Phylum> PhylumsCollection { get; set; }
        public ObservableCollection<Tbl03Regnum> RegnumsCollection { get; set; }
        public Tbl06Phylum SelectedPhylum { get; set; }
        public string SearchPhylumName { get; set; }

        #endregion
    }
}
