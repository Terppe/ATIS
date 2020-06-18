using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces;
using ATIS.Ui.Core.Repositories;
using ATIS.Ui.Helper;

namespace ATIS.Ui.Views.Database.D06Phylum
{
    public class PhylumsViewModel : ViewModelBase
    {

        /*
         *  Version mit Generic Repository
         */
        public override string Name => "Phylum";

        private readonly IGenericRepository<Tbl06Phylum> _repPhylumRepository = null;
        private readonly IGenericRepository<Tbl03Regnum> _repRegnumRepository = null;

        public PhylumsViewModel()
        {
            _repPhylumRepository = new GenericRepository<Tbl06Phylum>();
            _repRegnumRepository = new GenericRepository<Tbl03Regnum>();
            RegnumsCollection = new ObservableCollection<Tbl03Regnum>(_repRegnumRepository.SelectAll());
        }

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

        public void ExecuteGetPhylumsByNameOrId(string searchName)
        {
            PhylumsCollection = SearchNameReturnPhylumsCollection(SearchPhylumName);
            RaisePropertyChanged("PhylumsCollection");
        }

        private void ExecuteAddPhylum(object o)
        {
            if (PhylumsCollection == null)
                PhylumsCollection = new ObservableCollection<Tbl06Phylum>();

            if (SelectedPhylum == null) //No dataset selected 
            {
                MessageBox.Show("Select Phylum",
                    "Required select",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            RegnumsCollection = new ObservableCollection<Tbl03Regnum>(_repRegnumRepository.SelectAll());


            PhylumsCollection.Insert(0, new Tbl06Phylum { PhylumName = "DatasetNew" });
        }

        private void ExecuteCopyPhylum(object o)
        {
            if (_repPhylumRepository != null)
            {
                if (SelectedPhylum == null)
                {
                    MessageBox.Show("NewDataset",
                        "RequiredInput",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var phylum = _repPhylumRepository.SelectById(SelectedPhylum.PhylumId);

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
                if (SelectedPhylum != null) _repPhylumRepository.Delete(SelectedPhylum.PhylumId);

                _repPhylumRepository.Save();
                UpdateCollection();
            }
        }

        private void ExecuteSavePhylum(object o)
        {
            if (SelectedPhylum == null  ) //No dataset selected
            {
                    MessageBox.Show("Select Phylum",
                    "Required select",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var phylum = _repPhylumRepository.SelectById(SelectedPhylum.PhylumId);
                if (phylum != null) //update
                {
                    phylum.PhylumName = SelectedPhylum.PhylumName;
                    phylum.RegnumId = SelectedPhylum.RegnumId;
                    phylum.Valid = SelectedPhylum.Valid;
                    phylum.ValidYear = SelectedPhylum.ValidYear;
                    phylum.Synonym = SelectedPhylum.Synonym;
                    phylum.Author = SelectedPhylum.Author;
                    phylum.AuthorYear = SelectedPhylum.AuthorYear;
                    phylum.Info = SelectedPhylum.Info;
                    phylum.EngName = SelectedPhylum.EngName;
                    phylum.GerName = SelectedPhylum.GerName;
                    phylum.FraName = SelectedPhylum.FraName;
                    phylum.PorName = SelectedPhylum.PorName;
                    phylum.Updater = Environment.UserName;
                    phylum.UpdaterDate = DateTime.Now;
                    phylum.Memo = SelectedPhylum.Memo;
                }
            else
                phylum = new Tbl06Phylum //add new
                {
                    PhylumName = SelectedPhylum.PhylumName,
                    RegnumId = SelectedPhylum.RegnumId,
                    CountId = RandomHelper.Randomnumber(),
                    Valid = SelectedPhylum.Valid,
                    ValidYear = SelectedPhylum.ValidYear,
                    Synonym = SelectedPhylum.Synonym,
                    Author = SelectedPhylum.Author,
                    AuthorYear = SelectedPhylum.AuthorYear,
                    Info = SelectedPhylum.Info,
                    EngName = SelectedPhylum.EngName,
                    GerName = SelectedPhylum.GerName,
                    FraName = SelectedPhylum.FraName,
                    PorName = SelectedPhylum.PorName,
                    Writer = Environment.UserName,
                    WriterDate = DateTime.Now,
                    Updater = Environment.UserName,
                    UpdaterDate = DateTime.Now,
                    Memo = SelectedPhylum.Memo,
                };
            if (SelectedPhylum.PhylumId != 0)  //update
            {
                _repPhylumRepository.Update(phylum);
            }
            else
            {
                _repPhylumRepository.Insert(phylum);
            }

            _repPhylumRepository.Save();
            UpdateCollection();
        }

        private void UpdateCollection()
        {
            PhylumsCollection.Clear();
            foreach (Tbl06Phylum phylum in _repPhylumRepository.SelectAll())
            {
                PhylumsCollection.Add(phylum);
            }
        }

        public ObservableCollection<Tbl06Phylum> SearchNameReturnPhylumsCollection(string searchName)
        {
            var phylumsCollection = new ObservableCollection<Tbl06Phylum>();

            switch (searchName)
            {
                case "":
                    return phylumsCollection;
                case "*":
                    if (_repPhylumRepository != null)
                        phylumsCollection = new ObservableCollection<Tbl06Phylum>(_repPhylumRepository.SelectAll());
                    break;
                default:
                    if (_repPhylumRepository != null)
                        phylumsCollection = int.TryParse(searchName, out var id)
                            ? new ObservableCollection<Tbl06Phylum>(_repPhylumRepository.SelectAll()
                                .Where(e => e.PhylumId == id))
                            : new ObservableCollection<Tbl06Phylum>(_repPhylumRepository.SelectAll()
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

    }
}
