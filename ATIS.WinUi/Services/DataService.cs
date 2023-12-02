using System.Collections.ObjectModel;
using ATIS.WinUi.Contracts.Services;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Tbl18Superclass = ATIS.WinUi.Models.Tbl18Superclass;

namespace ATIS.WinUi.Services;
public class DataService : ObservableObject, IDataService
{
    private const string Value = "#";
    private static readonly AtisDbContext Context = new();
    private readonly AllDialogs _allDialogs = new();

    public DataService()
    {
    }
    #region CRUD

    #region Regnum

    #region Regnum Get
    public ObservableCollection<Tbl03Regnum>? GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnum()
    {
        if (Context.Tbl03Regnums != null)
        {
            var collection = new ObservableCollection<Tbl03Regnum>(Context.Tbl03Regnums
                .OrderBy(a => a.RegnumName)
                .ThenBy(a => a.Subregnum));
            return collection;
        }

        return null;
    }
    public Task<ObservableCollection<Tbl03Regnum>>
        GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromSearchNameOrId(string? searchName)
    {
        ObservableCollection<Tbl03Regnum>? collection = null!;
        if (Context.Tbl03Regnums != null)
        {
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl03Regnum>(Context.Tbl03Regnums
                    .Where(e => e.RegnumId == id))
                : new ObservableCollection<Tbl03Regnum>(Context.Tbl03Regnums
                    .Where(e => searchName != null && e.RegnumName.StartsWith(searchName))
                    .OrderBy(a => a.RegnumName)
                    .ThenBy(a => a.Subregnum)
                );
        }

        return Task.FromResult(collection);
    }
    public ObservableCollection<Tbl03Regnum> GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromRegnumId(int id)
    {
        if (Context.Tbl03Regnums != null)
        {
            var collection = new ObservableCollection<Tbl03Regnum>(Context.Tbl03Regnums
                .Where(e => e.RegnumId == id)
                .OrderBy(k => k.RegnumName)
                .ThenBy(k => k.Subregnum));

            return collection;
        }

        return null!;
    }
    public Tbl03Regnum GetRegnumSingleByRegnumId(int id)
    {
        if (Context.Tbl03Regnums != null)
        {
            var single = Context.Tbl03Regnums.SingleOrDefault(a => a.RegnumId == id);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public static async Task<Tbl03Regnum> GetRegnumSingleByRegnumNameAndSubregnum(string regnumName, string subregnum)
    {
        if (Context.Tbl03Regnums != null)
        {
            var single = Context.Tbl03Regnums.SingleOrDefault(a => a.RegnumName == regnumName && a.Subregnum == subregnum);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public async Task<Tbl03Regnum> GetRegnumSingleFirstDataset()
    {
        if (Context.Tbl03Regnums != null)
        {
            var single = Context.Tbl03Regnums.First();
            await Task.CompletedTask;
            return single;
        }

        return null!;
    }
    public async Task<ObservableCollection<Tbl03Regnum>> GetLastDatasetInTbl03Regnums()
    {
        if (Context.Tbl03Regnums != null)
        {
            var collection = Context.Tbl03Regnums
                .OrderBy(c => c.RegnumId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl03Regnum> { collection };
            }
        }

        return null!;
    }
    #endregion

    #region Regnum Copy
    public async Task<ObservableCollection<Tbl03Regnum>> CopyRegnum(Tbl03Regnum selected)
    {
        if (Context.Tbl03Regnums != null)
        {
            var dataset = Context.Tbl03Regnums.FirstOrDefault(a => a.RegnumId == selected.RegnumId);
            var collection = new ObservableCollection<Tbl03Regnum>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl03Regnum
                {
                    //  RegnumName = CultRes.StringsRes.DatasetNew,
                    RegnumName = "New",
                    Subregnum = dataset.Subregnum,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }

        return null!;
    }
    #endregion

    #region Regnum Delete
    public async Task<bool> DeleteRegnum(Tbl03Regnum selected)
    {
        var returnBool = false;
        try
        {
            var dataset = GetRegnumSingleByRegnumId(selected.RegnumId);
            if (true)
            {
                await DeleteRegnumDataset(dataset);
                returnBool = true;

                await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.RegnumName + " " + selected.Subregnum);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteRegnumDataset(Tbl03Regnum selected)
    {
        if (Context.Tbl03Regnums == null)
        {
        }
        else
        {
            Context.Tbl03Regnums.Remove(selected);
        }

        await Context.SaveChangesAsync();
        await Task.CompletedTask;
    }
    #endregion

    #region Regnum Save
    public async Task<bool> SaveRegnum(Tbl03Regnum selected)
    {
        var returnBool = false;
        try
        {
            if (selected.RegnumName != null)
            {
                if (selected.Subregnum != null)
                {
                    var datasetByName = await GetRegnumSingleByRegnumNameAndSubregnum(selected.RegnumName, selected.Subregnum);

                    if (datasetByName != null && selected.RegnumId == 0)
                    {
                        await AllDialogs.DatasetExistInfoMessageDialogAsync();
                        return false;
                    }
                }
            }

            var dataset = GetRegnumSingleByRegnumId(selected.RegnumId);

            if (!await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.RegnumFullName))
            {
                return false;
            }

            if (selected.RegnumId == 0)
            {
                dataset = await RegnumAdd(selected);
            }
            else
            {
                dataset = await RegnumUpdate(dataset, selected);
            }

            try
            {
                await RegnumSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }
            await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.RegnumName + selected.Subregnum);
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl03Regnum> RegnumUpdate(Tbl03Regnum home, Tbl03Regnum selected)
    {
        if (true) //update
        {
            home.RegnumName = selected.RegnumName;
            home.Subregnum = selected.Subregnum;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        return home;
    }
    public async Task<Tbl03Regnum> RegnumAdd(Tbl03Regnum selected)
    {
        var home = new Tbl03Regnum() //add new
        {
            RegnumName = selected.RegnumName,
            Subregnum = selected.Subregnum,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task RegnumSave(Tbl03Regnum home, Tbl03Regnum selected)
    {
        if (selected.RegnumId != 0)   //update
        {
            Context.Tbl03Regnums?.Update(home);
        }
        else           //add
        {
            Context.Tbl03Regnums?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    #endregion

    #endregion Regnum

    #region Phylum

    #region Phylum Get
    public ObservableCollection<Tbl06Phylum> GetTbl06PhylumsCollectionOrderByPhylumName()
    {
        if (Context.Tbl06Phylums != null)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(Context.Tbl06Phylums
                .OrderBy(a => a.PhylumName)
            );
            return collection;
        }

        return null!;
    }
    public async Task<ObservableCollection<Tbl06Phylum>> GetTbl06PhylumsCollectionOrderByPhylumNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl06Phylums != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl06Phylum>(Context.Tbl06Phylums
                    .Where(e => e.PhylumId == id))
                : new ObservableCollection<Tbl06Phylum>(Context.Tbl06Phylums
                    .Where(e => e.PhylumName.StartsWith(searchName))
                    .OrderBy(a => a.PhylumName)
                );
            await Task.CompletedTask;
            return collection;
        }

        return null!;
    }
    public ObservableCollection<Tbl06Phylum> GetTbl06PhylumsCollectionOrderByPhylumNameFromRegnumId(int regnumId)
    {
        if (Context.Tbl06Phylums != null)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(Context.Tbl06Phylums
                .Where(e => e.RegnumId == regnumId)
                .OrderBy(k => k.PhylumName)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl06Phylum> GetTbl06PhylumsCollectionOrderByPhylumNameFromPhylumId(int phylumId)
    {
        if (Context.Tbl06Phylums != null)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(Context.Tbl06Phylums
                .Where(e => e.PhylumId == phylumId)
                .OrderBy(k => k.PhylumName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl06Phylum>> GetTbl06PhylumsCollectionFromRegnumId(int regnumId)
    {
        if (Context.Tbl06Phylums != null)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(Context.Tbl06Phylums
                .Where(e => e.RegnumId == regnumId)
            );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public Tbl06Phylum GetPhylumSingleByPhylumId(int id)
    {
        if (Context.Tbl06Phylums != null)
        {
            var single = Context.Tbl06Phylums.SingleOrDefault(a => a.PhylumId == id);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public static async Task<Tbl06Phylum> GetPhylumSingleByPhylumName(string phylumName)
    {
        if (Context.Tbl06Phylums != null)
        {
            var single = Context.Tbl06Phylums.SingleOrDefault(a => a.PhylumName == phylumName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl06Phylum> GetPhylumSingleFirstDataset()
    {
        if (Context.Tbl06Phylums != null)
        {
            var single = Context.Tbl06Phylums.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl06Phylum>> GetLastDatasetInTbl06Phylums()
    {
        if (Context.Tbl06Phylums != null)
        {
            var collection = Context.Tbl06Phylums
                .OrderBy(c => c.PhylumId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl06Phylum> { collection };
            }
        }
        return null!;
    }
    #endregion

    #region Phylum Copy
    public async Task<ObservableCollection<Tbl06Phylum>> CopyPhylum(Tbl06Phylum selected)
    {
        if (Context.Tbl06Phylums != null)
        {
            var dataset = Context.Tbl06Phylums.FirstOrDefault(a => a.PhylumId == selected.PhylumId);
            var collection = new ObservableCollection<Tbl06Phylum>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl06Phylum
                {
                    //  PhylumName = CultRes.StringsRes.DatasetNew,
                    PhylumName = "New",
                    RegnumId = dataset.RegnumId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    #endregion

    #region Phylum Delete
    public async Task<bool> DeletePhylum(Tbl06Phylum selected)
    {
        var returnBool = false;

        try
        {
            var dataset = GetPhylumSingleByPhylumId(selected.PhylumId);
            if (true)
            {
                await DeletePhylumDataset(dataset);
                returnBool = true;

                if (selected.PhylumName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.PhylumName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeletePhylumDataset(Tbl06Phylum selected)
    {
        if (Context.Tbl06Phylums == null)
        {
        }
        else
        {
            Context.Tbl06Phylums.Remove(selected);
        }

        await Context.SaveChangesAsync();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl06Phylums(ObservableCollection<Tbl06Phylum> tbl06PhylumsList)
    {
        foreach (var t in tbl06PhylumsList)
        {
            if (Context.Tbl06Phylums == null)
            {
                continue;
            }

            Context.Tbl06Phylums.Remove(t);
        }

        await Context.SaveChangesAsync();
        await Task.CompletedTask;
    }
    public async Task DeleteConnectedPhylums(Tbl03Regnum selected)
    {
        if (Context.Tbl06Phylums != null)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(Context.Tbl06Phylums
                .Where(e => e.RegnumId == selected.RegnumId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl06Phylums.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    #endregion

    #region Phylum Save
    public async Task<bool> SavePhylum(Tbl06Phylum selected)
    {
        var returnBool = false;

        try
        {
            if (selected.PhylumName != null)
            {
                var datasetByName = await GetPhylumSingleByPhylumName(selected.PhylumName);

                if (datasetByName != null && selected.PhylumId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = GetPhylumSingleByPhylumId(selected.PhylumId);

            if (selected.PhylumName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.PhylumName))
            {
                return false;
            }

            if (selected.PhylumId == 0)
            {
                dataset = await PhylumAdd(selected);
            }
            else
            {
                dataset = await PhylumUpdate(dataset, selected);
            }

            try
            {
                await PhylumSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.PhylumName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.PhylumName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl06Phylum> PhylumUpdate(Tbl06Phylum home, Tbl06Phylum selected)
    {
        if (true) //update
        {
            home.PhylumName = selected.PhylumName;
            home.RegnumId = selected.RegnumId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task<Tbl06Phylum> PhylumAdd(Tbl06Phylum selected)
    {
        var home = new Tbl06Phylum() //add new
        {
            PhylumName = selected.PhylumName,
            RegnumId = selected.RegnumId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }
    public async Task PhylumSave(Tbl06Phylum home, Tbl06Phylum selected)
    {
        if (selected.PhylumId != 0)   //update
        {
            Context.Tbl06Phylums?.Update(home);
        }
        else           //add
        {
            Context.Tbl06Phylums?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    #endregion

    #endregion Phylum

    #region Division

    #region Division Get
    public ObservableCollection<Tbl09Division> GetTbl09DivisionsCollectionOrderByDivisionName()
    {
        if (Context.Tbl09Divisions != null)
        {
            var collection = new ObservableCollection<Tbl09Division>(Context.Tbl09Divisions
                .OrderBy(a => a.DivisionName)
            );
            return collection;
        }

        return null!;
    }

    public async Task<ObservableCollection<Tbl09Division>> GetTbl09DivisionsCollectionOrderByDivisionNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl09Divisions != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl09Division>(Context.Tbl09Divisions
                    .Where(e => e.DivisionId == id))
                : new ObservableCollection<Tbl09Division>(Context.Tbl09Divisions
                    .Where(e => e.DivisionName.StartsWith(searchName))
                    .OrderBy(a => a.DivisionName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl09Division> GetTbl09DivisionsCollectionOrderByDivisionNameFromRegnumId(int id)
    {
        if (Context.Tbl09Divisions != null)
        {
            var collection = new ObservableCollection<Tbl09Division>(Context.Tbl09Divisions
                .Where(e => e.RegnumId == id)
                .OrderBy(k => k.DivisionName)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl09Division> GetTbl09DivisionsCollectionOrderByDivisionNameFromDivisionId(int id)
    {
        if (Context.Tbl09Divisions != null)
        {
            var collection = new ObservableCollection<Tbl09Division>(Context.Tbl09Divisions
                .Where(e => e.DivisionId == id)
                .OrderBy(k => k.DivisionName)
            );
            return collection;
        }
        return null!;
    }
    public Tbl09Division GetDivisionSingleByDivisionId(int id)
    {
        if (Context.Tbl09Divisions != null)
        {
            var single = Context.Tbl09Divisions.SingleOrDefault(a => a.DivisionId == id);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public static async Task<Tbl09Division> GetDivisionSingleByDivisionName(string divisionName)
    {
        if (Context.Tbl09Divisions != null)
        {
            var single = Context.Tbl09Divisions.SingleOrDefault(a => a.DivisionName == divisionName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl09Division> GetDivisionSingleFirstDataset()
    {
        if (Context.Tbl09Divisions != null)
        {
            var single = Context.Tbl09Divisions.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl09Division>> GetLastDatasetInTbl09Divisions()
    {
        if (Context.Tbl09Divisions != null)
        {
            var collection = Context.Tbl09Divisions
                .OrderBy(c => c.DivisionId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl09Division> { collection };
            }
        }
        return null!;
    }
    #endregion

    #region Division Copy
    public async Task<ObservableCollection<Tbl09Division>> CopyDivision(Tbl09Division selected)
    {
        if (Context.Tbl09Divisions != null)
        {
            var dataset = Context.Tbl09Divisions.FirstOrDefault(a => a.DivisionId == selected.DivisionId);
            var collection = new ObservableCollection<Tbl09Division>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl09Division
                {
                    //  RegnumName = CultRes.StringsRes.DatasetNew,
                    DivisionName = "New",
                    RegnumId = dataset.RegnumId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    #endregion

    #region Division Delete
    public async Task<bool> DeleteDivision(Tbl09Division selected)
    {
        var returnBool = false;

        try
        {
            var dataset = GetDivisionSingleByDivisionId(selected.DivisionId);
            if (true)
            {
                await DeleteDivisionDataset(dataset);
                returnBool = true;

                if (selected.DivisionName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.DivisionName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteDivisionDataset(Tbl09Division selected)
    {
        if (Context.Tbl09Divisions == null)
        {
        }
        else
        {
            Context.Tbl09Divisions.Remove(selected);
        }

        await Context.SaveChangesAsync();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl09Divisions(ObservableCollection<Tbl09Division> tbl09DivisionsList)
    {
        foreach (var t in tbl09DivisionsList)
        {
            if (Context.Tbl09Divisions == null)
            {
                continue;
            }

            Context.Tbl09Divisions.Remove(t);
        }

        await Context.SaveChangesAsync();
        await Task.CompletedTask;
    }
    public async Task DeleteConnectedDivisions(Tbl03Regnum selected)
    {
        if (Context.Tbl09Divisions != null)
        {
            var collection = new ObservableCollection<Tbl09Division>(Context.Tbl09Divisions
                .Where(e => e.RegnumId == selected.RegnumId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl09Divisions.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    #endregion

    #region Division Save
    public async Task<bool> SaveDivision(Tbl09Division selected)
    {
        var returnBool = false;

        try
        {
            if (selected.DivisionName != null)
            {
                var datasetByName = await GetDivisionSingleByDivisionName(selected.DivisionName);

                if (datasetByName != null && selected.DivisionId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = GetDivisionSingleByDivisionId(selected.DivisionId);

            if (selected.DivisionName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.DivisionName))
            {
                return false;
            }

            if (selected.DivisionId == 0)
            {
                dataset = await DivisionAdd(selected);
            }
            else
            {
                dataset = await DivisionUpdate(dataset, selected);
            }

            try
            {
                await DivisionSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.DivisionName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.DivisionName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl09Division> DivisionUpdate(Tbl09Division home, Tbl09Division selected)
    {
        if (true) //update
        {
            home.DivisionName = selected.DivisionName;
            home.RegnumId = selected.RegnumId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task<Tbl09Division> DivisionAdd(Tbl09Division selected)
    {
        var home = new Tbl09Division() //add new
        {
            DivisionName = selected.DivisionName,
            RegnumId = selected.RegnumId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }
    public async Task DivisionSave(Tbl09Division home, Tbl09Division selected)
    {
        if (selected.DivisionId != 0)   //update
        {
            Context.Tbl09Divisions?.Update(home);
        }
        else           //add
        {
            Context.Tbl09Divisions?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    #endregion

    #endregion

    #region Subphylum

    #region Get Subphylum
    public ObservableCollection<Tbl12Subphylum> GetTbl12SubphylumsCollectionOrderBySubphylumName()
    {
        if (Context.Tbl12Subphylums != null)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(Context.Tbl12Subphylums
                .OrderBy(a => a.SubphylumName)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl12Subphylum> GetTbl12SubphylumsCollectionOrderBySubphylumNameFromPhylumId(int id)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(Context.Tbl12Subphylums
                .Where(e => e.PhylumId == id)
                .OrderBy(k => k.SubphylumName)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl12Subphylum> GetTbl12SubphylumsCollectionOrderBySubphylumNameFromSubphylumId(int subphylum)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(Context.Tbl12Subphylums
                .Where(e => e.SubphylumId == subphylum)
                .OrderBy(k => k.SubphylumName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl12Subphylum>> GetTbl12SubphylumsCollectionOrderBySubphylumNameFromSearchNameOrId(string? searchName)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl12Subphylum>(Context.Tbl12Subphylums
                    .Where(e => e.SubphylumId == id))
                : new ObservableCollection<Tbl12Subphylum>(Context.Tbl12Subphylums
                    .Where(e => searchName != null && e.SubphylumName.StartsWith(searchName))
                    .OrderBy(a => a.SubphylumName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    public Tbl12Subphylum GetSubphylumSingleBySubphylumId(int id)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var single = Context.Tbl12Subphylums.SingleOrDefault(a => a.SubphylumId == id);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public static async Task<Tbl12Subphylum> GetSubphylumSingleBySubphylumName(string subphylumName)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var single = Context.Tbl12Subphylums.SingleOrDefault(a => a.SubphylumName == subphylumName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public async Task<Tbl12Subphylum> GetSubphylumSingleFirstDataset()
    {
        if (Context.Tbl12Subphylums != null)
        {
            var single = Context.Tbl12Subphylums.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl12Subphylum>> GetLastDatasetInTbl12Subphylums()
    {
        if (Context.Tbl12Subphylums != null)
        {
            var collection = Context.Tbl12Subphylums
                .OrderBy(c => c.SubphylumId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl12Subphylum> { collection };
            }
        }
        return null!;
    }
    #endregion

    #region Copy Subphylum
    public async Task<ObservableCollection<Tbl12Subphylum>> CopySubphylum(Tbl12Subphylum selected)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var dataset = Context.Tbl12Subphylums.FirstOrDefault(a => a.SubphylumId == selected.SubphylumId);
            var collection = new ObservableCollection<Tbl12Subphylum>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl12Subphylum
                {
                    //  RegnumName = CultRes.StringsRes.DatasetNew,
                    SubphylumName = "New",
                    PhylumId = dataset.PhylumId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    #endregion

    #region Delete Subphylum
    public async Task<bool> DeleteSubphylum(Tbl12Subphylum selected)
    {
        var returnBool = false;
        try
        {
            var dataset = GetSubphylumSingleBySubphylumId(selected.SubphylumId);
            if (true)
            {
                await DeleteSubphylumDataset(dataset);
                returnBool = true;

                if (selected.SubphylumName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.SubphylumName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    private static async Task DeleteDatasetsInTbl12Subphylums(ObservableCollection<Tbl12Subphylum> tbl12SubphylumsList)
    {
        foreach (var t in tbl12SubphylumsList)
        {
            if (Context.Tbl12Subphylums == null)
            {
                continue;
            }

            Context.Tbl12Subphylums.Remove(t);
        }

        await Context.SaveChangesAsync();
        await Task.CompletedTask;
    }
    public async Task DeleteSubphylumDataset(Tbl12Subphylum selected)
    {
        if (Context.Tbl12Subphylums == null)
        {
        }
        else
        {
            Context.Tbl12Subphylums.Remove(selected);
        }

        await Context.SaveChangesAsync();
        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubphylums(Tbl06Phylum selected)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(Context.Tbl12Subphylums
                .Where(e => e.PhylumId == selected.PhylumId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl12Subphylums.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    #endregion

    #region Save Subphylum
    public async Task<bool> SaveSubphylum(Tbl12Subphylum selected)
    {
        var returnBool = false;

        try
        {
            if (selected.SubphylumName != null)
            {
                var datasetByName = await GetSubphylumSingleBySubphylumName(selected.SubphylumName);

                if (datasetByName != null && selected.SubphylumId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = GetSubphylumSingleBySubphylumId(selected.SubphylumId);

            if (selected.SubphylumName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.SubphylumName))
            {
                return false;
            }

            if (selected.SubphylumId == 0)
            {
                dataset = await SubphylumAdd(selected);
            }
            else
            {
                dataset = await SubphylumUpdate(dataset, selected);
            }

            try
            {
                await SubphylumSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.SubphylumName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.SubphylumName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl12Subphylum> SubphylumUpdate(Tbl12Subphylum home, Tbl12Subphylum selected)
    {
        if (true) //update
        {
            home.SubphylumName = selected.SubphylumName;
            home.PhylumId = selected.PhylumId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task<Tbl12Subphylum> SubphylumAdd(Tbl12Subphylum selected)
    {
        var home = new Tbl12Subphylum() //add new
        {
            SubphylumName = selected.SubphylumName,
            PhylumId = selected.PhylumId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }
    public async Task SubphylumSave(Tbl12Subphylum home, Tbl12Subphylum selected)
    {
        if (selected.SubphylumId != 0)   //update
        {
            Context.Tbl12Subphylums?.Update(home);
        }
        else           //add
        {
            Context.Tbl12Subphylums?.Add(home);
        }

        Context.SaveChanges();
        //    GC.SuppressFinalize(this);
        await Task.CompletedTask;
    }
    #endregion

    #endregion

    #region Subdivision

    #region Get Subdivision
    public ObservableCollection<Tbl15Subdivision> GetTbl15SubdivisionsCollectionOrderBySubdivisionName()
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(Context.Tbl15Subdivisions
                .OrderBy(a => a.SubdivisionName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl15Subdivision>> GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl15Subdivision>(Context.Tbl15Subdivisions
                    .Where(e => e.SubdivisionId == id))
                : new ObservableCollection<Tbl15Subdivision>(Context.Tbl15Subdivisions
                    .Where(e => e.SubdivisionName.StartsWith(searchName))
                    .OrderBy(a => a.SubdivisionName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl15Subdivision> GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromDivisionId(int divisionid)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(Context.Tbl15Subdivisions
                .Where(e => e.DivisionId == divisionid)
                .OrderBy(k => k.SubdivisionName)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl15Subdivision> GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromSubdivisionId(int subdivisionid)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(Context.Tbl15Subdivisions
                .Where(e => e.SubdivisionId == subdivisionid)
                .OrderBy(k => k.SubdivisionName)
            );
            return collection;
        }
        return null!;
    }
    public Tbl15Subdivision GetSubdivisionSingleBySubdivisionId(int id)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var single = Context.Tbl15Subdivisions.SingleOrDefault(a => a.SubdivisionId == id);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public static async Task<Tbl15Subdivision> GetSubdivisionSingleBySubdivisionName(string subdivisionName)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var single = Context.Tbl15Subdivisions.SingleOrDefault(a => a.SubdivisionName == subdivisionName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl15Subdivision>> GetLastDatasetInTbl15Subdivisions()
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var collection = Context.Tbl15Subdivisions
                .OrderBy(c => c.SubdivisionId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl15Subdivision> { collection };
            }
        }
        return null!;
    }
    public async Task<Tbl15Subdivision> GetSubdivisionSingleFirstDataset()
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var single = Context.Tbl15Subdivisions.FirstOrDefault();
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    #endregion

    #region Copy Subdivision
    public async Task<ObservableCollection<Tbl15Subdivision>> CopySubdivision(Tbl15Subdivision selected)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var dataset = Context.Tbl15Subdivisions.FirstOrDefault(a => a.SubdivisionId == selected.SubdivisionId);
            var collection = new ObservableCollection<Tbl15Subdivision>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl15Subdivision
                {
                    //  RegnumName = CultRes.StringsRes.DatasetNew,
                    SubdivisionName = "New",
                    DivisionId = dataset.DivisionId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    #endregion

    #region Delete Subdivision
    public async Task<bool> DeleteSubdivision(Tbl15Subdivision selected)
    {
        var returnBool = false;
        try
        {
            var dataset = GetSubdivisionSingleBySubdivisionId(selected.SubdivisionId);
            if (true)
            {
                await DeleteSubdivisionDataset(dataset);
                returnBool = true;

                if (selected.SubdivisionName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.SubdivisionName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    private static async Task DeleteDatasetsInTbl15Subdivisions(ObservableCollection<Tbl15Subdivision> tbl15SubdivisionsList)
    {
        foreach (var t in tbl15SubdivisionsList)
        {
            Context.Tbl15Subdivisions?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteSubdivisionDataset(Tbl15Subdivision selected)
    {
        Context.Tbl15Subdivisions?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubdivisions(Tbl09Division selected)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(Context.Tbl15Subdivisions
                .Where(e => e.DivisionId == selected.DivisionId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl15Subdivisions.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    #endregion

    #region Save Subdivision
    public async Task<bool> SaveSubdivision(Tbl15Subdivision selected)
    {
        var returnBool = false;

        try
        {
            if (selected.SubdivisionName != null)
            {
                var datasetByName = await GetSubdivisionSingleBySubdivisionName(selected.SubdivisionName);

                if (datasetByName != null && selected.SubdivisionId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = GetSubdivisionSingleBySubdivisionId(selected.SubdivisionId);

            if (selected.SubdivisionName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.SubdivisionName))
            {
                return false;
            }

            if (selected.SubdivisionId == 0)
            {
                dataset = await SubdivisionAdd(selected);
            }
            else
            {
                dataset = await SubdivisionUpdate(dataset, selected);
            }

            try
            {
                await SubdivisionSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.SubdivisionName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.SubdivisionName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl15Subdivision> SubdivisionUpdate(Tbl15Subdivision home, Tbl15Subdivision selected)
    {
        if (true) //update
        {
            home.SubdivisionName = selected.SubdivisionName;
            home.DivisionId = selected.DivisionId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task<Tbl15Subdivision> SubdivisionAdd(Tbl15Subdivision selected)
    {
        var home = new Tbl15Subdivision() //add new
        {
            SubdivisionName = selected.SubdivisionName,
            DivisionId = selected.DivisionId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }
    public async Task SubdivisionSave(Tbl15Subdivision home, Tbl15Subdivision selected)
    {
        if (selected.SubdivisionId != 0)   //update
        {
            Context.Tbl15Subdivisions?.Update(home);
        }
        else           //add
        {
            Context.Tbl15Subdivisions?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    #endregion

    #endregion Subdivision

    #region Superclass

    #region Get Superclass
    public ObservableCollection<Tbl18Superclass> GetTbl18SuperclassesCollectionOrderBySuperclassName()
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                .OrderBy(a => a.SuperclassName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl18Superclass>> GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                    .Where(e => e.SuperclassId == id))
                : new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                    .Where(e => e.SuperclassName.StartsWith(searchName))
                    .OrderBy(a => a.SuperclassName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl18Superclass> GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSuperclassId(int superclassId)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                .Where(e => e.SuperclassId == superclassId)
                .OrderBy(k => k.SuperclassName)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl18Superclass> GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSubphylumId(int subphylumId)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                .Where(e => e.SubphylumId == subphylumId)
                .OrderBy(k => k.SuperclassName)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl18Superclass> GetTbl18SuperclassesCollectionOrderBySuperclassNameFromSubdivisionId(int subdivisionId)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                .Where(e => e.SubdivisionId == subdivisionId)
                .OrderBy(k => k.SuperclassName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl18Superclass>> GetLastDatasetInTbl18Superclasses()
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = Context.Tbl18Superclasses
                .OrderBy(c => c.SuperclassId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl18Superclass> { collection };
            }
        }
        return null!;
    }
    public Tbl18Superclass GetSuperclassSingleBySuperclassId(int superclassId)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var single = Context.Tbl18Superclasses.SingleOrDefault(a => a.SuperclassId == superclassId);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public static async Task<Tbl18Superclass> GetSuperclassSingleBySuperclassName(string superclassName)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var single = Context.Tbl18Superclasses.SingleOrDefault(a => a.SuperclassName == superclassName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public async Task<Tbl18Superclass> GetSuperclassSingleFirstDataset()
    {
        if (Context.Tbl18Superclasses != null)
        {
            var single = Context.Tbl18Superclasses.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }
    #endregion

    #region Copy Superclass
    public async Task<ObservableCollection<Tbl18Superclass>> CopySuperclass(Tbl18Superclass selected)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var dataset = Context.Tbl18Superclasses.FirstOrDefault(a => a.SuperclassId == selected.SuperclassId);
            var collection = new ObservableCollection<Tbl18Superclass>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl18Superclass
                {
                    //  RegnumName = CultRes.StringsRes.DatasetNew,
                    SuperclassName = "New",
                    SubphylumId = dataset.SubphylumId,
                    SubdivisionId = dataset.SubdivisionId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    #endregion

    #region Delete Superclass
    public async Task<bool> DeleteSuperclass(Tbl18Superclass selected)
    {
        var returnBool = false;
        try
        {
            var dataset = GetSuperclassSingleBySuperclassId(selected.SuperclassId);
            if (true)
            {
                await DeleteSuperclassDataset(dataset);
                returnBool = true;

                if (selected.SuperclassName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.SuperclassName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteSuperclassDataset(Tbl18Superclass selected)
    {
        Context.Tbl18Superclasses?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl18Superclasses(ObservableCollection<Tbl18Superclass> tbl18SuperclassesList)
    {
        foreach (var t in tbl18SuperclassesList)
        {
            Context.Tbl18Superclasses?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSuperclassesWithSubphylum(Tbl12Subphylum selected)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                .Where(e => e.SubphylumId == selected.SubphylumId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl18Superclasses.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSuperclassesWithSubdivision(Tbl15Subdivision selected)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                .Where(e => e.SubdivisionId == selected.SubdivisionId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl18Superclasses.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    #endregion

    #region Save Superclass
    public async Task<bool> SaveSuperclass(Tbl18Superclass selected)
    {
        var returnBool = false;

        try
        {
            if (selected.SuperclassName != null)
            {
                var datasetByName = await GetSuperclassSingleBySuperclassName(selected.SuperclassName);

                if (datasetByName != null && selected.SuperclassId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = GetSuperclassSingleBySuperclassId(selected.SuperclassId);

            if (selected.SuperclassName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.SuperclassName))
            {
                return false;
            }

            if (selected.SuperclassId == 0)
            {
                dataset = await SuperclassAdd(selected);
            }
            else
            {
                dataset = await SuperclassUpdate(dataset, selected);
            }

            try
            {
                await SuperclassSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.SuperclassName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.SuperclassName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl18Superclass> SuperclassUpdate(Tbl18Superclass home, Tbl18Superclass selected)
    {
        if (true) //update
        {
            home.SuperclassName = selected.SuperclassName;
            home.SubphylumId = selected.SubphylumId;
            home.SubdivisionId = selected.SubdivisionId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task<Tbl18Superclass> SuperclassAdd(Tbl18Superclass selected)
    {
        var home = new Tbl18Superclass() //add new
        {
            SuperclassName = selected.SuperclassName,
            SubdivisionId = selected.SubdivisionId,
            SubphylumId = selected.SubphylumId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }
    public async Task SuperclassSave(Tbl18Superclass home, Tbl18Superclass selected)
    {
        if (selected.SuperclassId != 0)   //update
        {
            Context.Tbl18Superclasses?.Update(home);
        }
        else           //add
        {
            Context.Tbl18Superclasses?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    #endregion


    #endregion Superclass

    #region Class

    #region Get Class
    public ObservableCollection<Tbl21Class> GetTbl21ClassesCollectionOrderByClassName()
    {
        if (Context.Tbl21Classes != null)
        {
            var collection = new ObservableCollection<Tbl21Class>(Context.Tbl21Classes
                .OrderBy(a => a.ClassName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl21Class>> GetTbl21ClassesCollectionOrderByClassNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl21Classes != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl21Class>(Context.Tbl21Classes
                    .Where(e => e.ClassId == id))
                : new ObservableCollection<Tbl21Class>(Context.Tbl21Classes
                    .Where(e => e.ClassName.StartsWith(searchName))
                    .OrderBy(a => a.ClassName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl21Class> GetTbl21ClassesCollectionOrderByClassNameFromClassId(int classId)
    {
        if (Context.Tbl21Classes != null)
        {
            var collection = new ObservableCollection<Tbl21Class>(Context.Tbl21Classes
                .Where(e => e.ClassId == classId)
                .OrderBy(k => k.ClassName)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl21Class> GetTbl21ClassesCollectionOrderByClassNameFromSuperclassId(int superclassId)
    {
        if (Context.Tbl21Classes != null)
        {
            var collection = new ObservableCollection<Tbl21Class>(Context.Tbl21Classes
                .Where(e => e.SuperclassId == superclassId)
                .OrderBy(k => k.ClassName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl21Class>> GetLastDatasetInTbl21Classes()
    {
        if (Context.Tbl21Classes != null)
        {
            var collection = Context.Tbl21Classes
                .OrderBy(c => c.ClassId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl21Class> { collection };
            }
        }
        return null!;
    }
    public Tbl21Class GetClassSingleByClassId(int classeId)
    {
        if (Context.Tbl21Classes != null)
        {
            var single = Context.Tbl21Classes.SingleOrDefault(a => a.ClassId == classeId);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public static async Task<Tbl21Class> GetClassSingleByClassName(string className)
    {
        if (Context.Tbl21Classes != null)
        {
            var single = Context.Tbl21Classes.SingleOrDefault(a => a.ClassName == className);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public async Task<Tbl21Class> GetClassSingleFirstDataset()
    {
        if (Context.Tbl21Classes != null)
        {
            var single = Context.Tbl21Classes.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    #endregion

    #region Copy Class 
    public async Task<ObservableCollection<Tbl21Class>> CopyClass(Tbl21Class selected)
    {
        if (Context.Tbl21Classes != null)
        {
            var dataset = Context.Tbl21Classes.FirstOrDefault(a => a.ClassId == selected.ClassId);
            var collection = new ObservableCollection<Tbl21Class>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl21Class
                {
                    //  ClassName = CultRes.StringsRes.DatasetNew,
                    ClassName = "New",
                    SuperclassId = dataset.SuperclassId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    #endregion

    #region Delete Class

    public async Task DeleteConnectedClasses(Tbl18Superclass selected)
    {
        if (Context.Tbl21Classes != null)
        {
            var collection = new ObservableCollection<Tbl21Class>(Context.Tbl21Classes
                .Where(e => e.SuperclassId == selected.SuperclassId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl21Classes.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    public async Task<bool> DeleteClass(Tbl21Class selected)
    {
        var returnBool = false;
        try
        {
            var dataset = GetClassSingleByClassId(selected.ClassId);
            if (dataset != null)
            {
                await DeleteClassDataset(dataset);
                returnBool = true;

                if (selected.ClassName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.ClassName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteDatasetsInTbl21Classes(ObservableCollection<Tbl21Class> tbl21Classes)
    {
        foreach (var t in tbl21Classes)
        {
            if (Context.Tbl21Classes == null)
            {
                continue;
            }

            Context.Tbl21Classes.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    public async Task DeleteClassDataset(Tbl21Class selected)
    {
        if (Context.Tbl21Classes == null)
        {
        }
        else
        {
            Context.Tbl21Classes.Remove(selected);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Class
    public async Task<bool> SaveClass(Tbl21Class selected)
    {
        var returnBool = false;

        try
        {
            if (selected.ClassName != null)
            {
                var datasetByName = await GetClassSingleByClassName(selected.ClassName);
                if (datasetByName != null && selected.ClassId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = GetClassSingleByClassId(selected.ClassId);

            if (selected.ClassName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.ClassName))
            {
                return false;
            }

            if (selected.ClassId == 0)
            {
                dataset = await ClassAdd(selected);
            }
            else
            {
                dataset = await ClassUpdate(dataset, selected);
            }

            try
            {
                await ClassSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.ClassName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.ClassName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl21Class> ClassUpdate(Tbl21Class home, Tbl21Class selected)
    {
        if (home != null) //update
        {
            home.ClassName = selected.ClassName;
            home.SuperclassId = selected.SuperclassId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task<Tbl21Class> ClassAdd(Tbl21Class selected)
    {
        var home = new Tbl21Class() //add new
        {
            ClassName = selected.ClassName,
            SuperclassId = selected.SuperclassId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }
    public async Task ClassSave(Tbl21Class home, Tbl21Class selected)
    {
        if (selected.ClassId != 0)   //update
        {
            if (Context.Tbl21Classes == null)
            {
            }
            else
            {
                Context.Tbl21Classes.Update(home);
            }
        }
        else           //add
        {
            Context.Tbl21Classes?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Class

    #region Subclass

    #region Get Subclass
    public ObservableCollection<Tbl24Subclass> GetTbl24SubclassesCollectionOrderBySubclassNameFromClassId(int classId)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(Context.Tbl24Subclasses
                .Where(e => e.ClassId == classId)
                .OrderBy(k => k.SubclassName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl24Subclass>> GetLastDatasetInTbl24Subclasses()
    {
        if (Context.Tbl24Subclasses != null)
        {
            var collection = Context.Tbl24Subclasses
                .OrderBy(c => c.SubclassId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl24Subclass> { collection };
            }
        }
        return null!;
    }
    public ObservableCollection<Tbl24Subclass> GetTbl24SubclassesCollectionOrderBySubclassName()
    {
        if (Context.Tbl24Subclasses != null)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(Context.Tbl24Subclasses
                .OrderBy(a => a.SubclassName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl24Subclass>> GetTbl24SubclassesCollectionOrderBySubclassNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl24Subclass>(Context.Tbl24Subclasses
                    .Where(e => e.SubclassId == id))
                : new ObservableCollection<Tbl24Subclass>(Context.Tbl24Subclasses
                    .Where(e => e.SubclassName.StartsWith(searchName))
                    .OrderBy(a => a.SubclassName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl24Subclass> GetTbl24SubclassesCollectionOrderBySubclassNameFromSubclassId(int subclassId)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(Context.Tbl24Subclasses
                .Where(e => e.SubclassId == subclassId)
                .OrderBy(k => k.SubclassName)
            );
            return collection;
        }
        return null!;
    }
    public Tbl24Subclass GetSubclassSingleBySubclassId(int subclassId)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var single = Context.Tbl24Subclasses.SingleOrDefault(a => a.SubclassId == subclassId);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public static async Task<Tbl24Subclass> GetSubclassSingleBySubclassName(string subclassName)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var single = Context.Tbl24Subclasses.SingleOrDefault(a => a.SubclassName == subclassName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public async Task<Tbl24Subclass> GetSubclassSingleFirstDataset()
    {
        if (Context.Tbl24Subclasses != null)
        {
            var single = Context.Tbl24Subclasses.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }
    #endregion

    #region Copy Subclass

    public async Task<ObservableCollection<Tbl24Subclass>> CopySubclass(Tbl24Subclass selected)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var dataset = Context.Tbl24Subclasses.FirstOrDefault(a => a.SubclassId == selected.SubclassId);
            var collection = new ObservableCollection<Tbl24Subclass>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl24Subclass
                {
                    //  SubclassName = CultRes.StringsRes.DatasetNew,
                    SubclassName = "New",
                    ClassId = dataset.ClassId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Subclass
    public async Task DeleteConnectedSubclasses(Tbl21Class selected)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(Context.Tbl24Subclasses
                .Where(e => e.ClassId == selected.ClassId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl24Subclasses.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task<bool> DeleteSubclass(Tbl24Subclass selected)
    {
        var returnBool = false;
        try
        {
            var dataset = GetSubclassSingleBySubclassId(selected.SubclassId);
            if (dataset != null)
            {
                await DeleteSubclassDataset(dataset);
                returnBool = true;

                if (selected.SubclassName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.SubclassName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteDatasetsInTbl24Subclasses(ObservableCollection<Tbl24Subclass> tbl24Subclasses)
    {
        foreach (var t in tbl24Subclasses)
        {
            if (Context.Tbl24Subclasses == null)
            {
                continue;
            }

            Context.Tbl24Subclasses.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteSubclassDataset(Tbl24Subclass selected)
    {
        if (Context.Tbl24Subclasses == null)
        {
        }
        else
        {
            Context.Tbl24Subclasses.Remove(selected);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    #endregion

    #region Save Subclass
    public async Task<bool> SaveSubclass(Tbl24Subclass selected)
    {
        var returnBool = false;

        try
        {
            if (selected.SubclassName != null)
            {
                var datasetByName = await GetSubclassSingleBySubclassName(selected.SubclassName);

                if (datasetByName != null && selected.SubclassId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = GetSubclassSingleBySubclassId(selected.SubclassId);

            if (selected.SubclassName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.SubclassName))
            {
                return false;
            }

            if (selected.SubclassId == 0)
            {
                dataset = await SubclassAdd(selected);
            }
            else
            {
                dataset = await SubclassUpdate(dataset, selected);
            }

            try
            {
                await SubclassSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.SubclassName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.SubclassName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl24Subclass> SubclassUpdate(Tbl24Subclass home, Tbl24Subclass selected)
    {
        if (home != null) //update
        {
            home.SubclassName = selected.SubclassName;
            home.ClassId = selected.ClassId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task<Tbl24Subclass> SubclassAdd(Tbl24Subclass selected)
    {
        var home = new Tbl24Subclass() //add new
        {
            SubclassName = selected.SubclassName,
            ClassId = selected.ClassId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }
    public async Task SubclassSave(Tbl24Subclass home, Tbl24Subclass selected)
    {
        if (selected.SubclassId != 0)   //update
        {
            if (Context.Tbl24Subclasses == null)
            {
            }
            else
            {
                Context.Tbl24Subclasses.Update(home);
            }
        }
        else           //add
        {
            Context.Tbl24Subclasses?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    #endregion

    #endregion Subclass

    #region Infraclass

    #region Get Infraclass
    public ObservableCollection<Tbl27Infraclass> GetTbl27InfraclassesCollectionOrderByInfraclassNameFromSubclassId(int subclassId)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(Context.Tbl27Infraclasses
                .Where(e => e.SubclassId == subclassId)
                .OrderBy(k => k.InfraclassName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl27Infraclass>> GetLastDatasetInTbl27Infraclasses()
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var collection = Context.Tbl27Infraclasses
                .OrderBy(c => c.InfraclassId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl27Infraclass> { collection };
            }
        }
        return null!;
    }
    public Tbl27Infraclass GetInfraclassSingleByInfraclassId(int infraclassId)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var single = Context.Tbl27Infraclasses.SingleOrDefault(a => a.InfraclassId == infraclassId);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public static async Task<Tbl27Infraclass> GetInfraclassSingleByInfraclassName(string infraclassName)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var single = Context.Tbl27Infraclasses.SingleOrDefault(a => a.InfraclassName == infraclassName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public async Task<Tbl27Infraclass> GetInfraclassSingleFirstDataset()
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var single = Context.Tbl27Infraclasses.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }
    public ObservableCollection<Tbl27Infraclass> GetTbl27InfraclassesCollectionOrderByInfraclassName()
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(Context.Tbl27Infraclasses
                .OrderBy(a => a.InfraclassName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl27Infraclass>> GetTbl27InfraclassesCollectionOrderByInfraclassNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl27Infraclass>(Context.Tbl27Infraclasses
                    .Where(e => e.InfraclassId == id))
                : new ObservableCollection<Tbl27Infraclass>(Context.Tbl27Infraclasses
                    .Where(e => e.InfraclassName.StartsWith(searchName))
                    .OrderBy(a => a.InfraclassName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl27Infraclass> GetTbl27InfraclassesCollectionOrderByInfraclassNameFromInfraclassId(int infraclassId)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(Context.Tbl27Infraclasses
                .Where(e => e.InfraclassId == infraclassId)
                .OrderBy(k => k.InfraclassName)
            );
            return collection;
        }
        return null!;
    }
    #endregion

    #region Copy Infraclass
    public async Task<ObservableCollection<Tbl27Infraclass>> CopyInfraclass(Tbl27Infraclass selected)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var dataset = Context.Tbl27Infraclasses.FirstOrDefault(a => a.InfraclassId == selected.InfraclassId);
            var collection = new ObservableCollection<Tbl27Infraclass>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl27Infraclass
                {
                    //  InfraclassName = CultRes.StringsRes.DatasetNew,
                    InfraclassName = "New",
                    SubclassId = dataset.SubclassId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    #endregion

    #region Delete Infraclass
    public async Task DeleteConnectedInfraclasses(Tbl24Subclass selected)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(Context.Tbl27Infraclasses
                .Where(e => e.SubclassId == selected.SubclassId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl27Infraclasses.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task<bool> DeleteInfraclass(Tbl27Infraclass selected)
    {
        var returnBool = false;
        try
        {
            var dataset = GetInfraclassSingleByInfraclassId(selected.InfraclassId);
            if (dataset != null)
            {
                await DeleteInfraclassDataset(dataset);
                returnBool = true;

                if (selected.InfraclassName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.InfraclassName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteInfraclassDataset(Tbl27Infraclass selected)
    {
        if (Context.Tbl27Infraclasses == null)
        {
        }
        else
        {
            Context.Tbl27Infraclasses.Remove(selected);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl27Infraclasses(ObservableCollection<Tbl27Infraclass> tbl27Infraclasses)
    {
        foreach (var t in tbl27Infraclasses)
        {
            if (Context.Tbl27Infraclasses == null)
            {
                continue;
            }

            Context.Tbl27Infraclasses.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }


    #endregion

    #region Save Infraclass
    public async Task<bool> SaveInfraclass(Tbl27Infraclass selected)
    {
        var returnBool = false;

        try
        {
            if (selected.InfraclassName != null)
            {
                var datasetByName = await GetInfraclassSingleByInfraclassName(selected.InfraclassName);

                if (datasetByName != null && selected.InfraclassId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = GetInfraclassSingleByInfraclassId(selected.InfraclassId);

            if (selected.InfraclassName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.InfraclassName))
            {
                return false;
            }

            if (selected.InfraclassId == 0)
            {
                dataset = await InfraclassAdd(selected);
            }
            else
            {
                dataset = await InfraclassUpdate(dataset, selected);
            }

            try
            {
                await InfraclassSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.InfraclassName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.InfraclassName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl27Infraclass> InfraclassUpdate(Tbl27Infraclass home, Tbl27Infraclass selected)
    {
        if (home != null) //update
        {
            home.InfraclassName = selected.InfraclassName;
            home.SubclassId = selected.SubclassId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task<Tbl27Infraclass> InfraclassAdd(Tbl27Infraclass selected)
    {
        var home = new Tbl27Infraclass() //add new
        {
            InfraclassName = selected.InfraclassName,
            SubclassId = selected.SubclassId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }
    public async Task InfraclassSave(Tbl27Infraclass home, Tbl27Infraclass selected)
    {
        if (selected.InfraclassId != 0)   //update
        {
            Context.Tbl27Infraclasses?.Update(home);
        }
        else           //add
        {
            Context.Tbl27Infraclasses?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    #endregion

    #endregion Infraclass

    #region Legio

    #region Get Legio
    public ObservableCollection<Tbl30Legio> GetTbl30LegiosCollectionOrderByLegioNameFromInfraclassId(int infraclassId)
    {
        if (Context.Tbl30Legios != null)
        {
            var collection = new ObservableCollection<Tbl30Legio>(Context.Tbl30Legios
                .Where(e => e.InfraclassId == infraclassId)
                .OrderBy(k => k.LegioName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl30Legio>> GetLastDatasetInTbl30Legios()
    {
        if (Context.Tbl30Legios != null)
        {
            var collection = Context.Tbl30Legios
                .OrderBy(c => c.LegioId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl30Legio> { collection };
            }
        }
        return null!;
    }
    public Tbl30Legio GetLegioSingleByLegioId(int legioId)
    {
        if (Context.Tbl30Legios != null)
        {
            var single = Context.Tbl30Legios.SingleOrDefault(a => a.LegioId == legioId);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public static async Task<Tbl30Legio> GetLegioSingleByLegioName(string legioName)
    {
        if (Context.Tbl30Legios != null)
        {
            var single = Context.Tbl30Legios.SingleOrDefault(a => a.LegioName == legioName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl30Legio> GetLegioSingleFirstDataset()
    {
        if (Context.Tbl30Legios != null)
        {
            var single = Context.Tbl30Legios.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    public ObservableCollection<Tbl30Legio> GetTbl30LegiosCollectionOrderByLegioName()
    {
        if (Context.Tbl30Legios != null)
        {
            var collection = new ObservableCollection<Tbl30Legio>(Context.Tbl30Legios
                .OrderBy(a => a.LegioName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl30Legio>> GetTbl30LegiosCollectionOrderByLegioNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl30Legios != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl30Legio>(Context.Tbl30Legios
                    .Where(e => e.LegioId == id))
                : new ObservableCollection<Tbl30Legio>(Context.Tbl30Legios
                    .Where(e => e.LegioName.StartsWith(searchName))
                    .OrderBy(a => a.LegioName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl30Legio> GetTbl30LegiosCollectionOrderByLegioNameFromLegioId(int legioId)
    {
        if (Context.Tbl30Legios != null)
        {
            var collection = new ObservableCollection<Tbl30Legio>(Context.Tbl30Legios
                .Where(e => e.LegioId == legioId)
                .OrderBy(k => k.LegioName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Legio

    public async Task<ObservableCollection<Tbl30Legio>> CopyLegio(Tbl30Legio selected)
    {
        if (Context.Tbl30Legios != null)
        {
            var dataset = Context.Tbl30Legios.FirstOrDefault(a => a.LegioId == selected.LegioId);
            var collection = new ObservableCollection<Tbl30Legio>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl30Legio
                {
                    //  LegioName = CultRes.StringsRes.DatasetNew,
                    LegioName = "New",
                    InfraclassId = dataset.InfraclassId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Legio
    public async Task DeleteConnectedLegios(Tbl27Infraclass selected)
    {
        if (Context.Tbl30Legios != null)
        {
            var collection = new ObservableCollection<Tbl30Legio>(Context.Tbl30Legios
                .Where(e => e.InfraclassId == selected.InfraclassId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl30Legios.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task<bool> DeleteLegio(Tbl30Legio selected)
    {
        var returnBool = false;
        try
        {
            var dataset = GetLegioSingleByLegioId(selected.LegioId);
            if (dataset != null)
            {
                await DeleteLegioDataset(dataset);
                returnBool = true;

                if (selected.LegioName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.LegioName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteLegioDataset(Tbl30Legio selected)
    {
        Context.Tbl30Legios?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl30Legios(ObservableCollection<Tbl30Legio> tbl30Legios)
    {
        foreach (var t in tbl30Legios)
        {
            if (Context.Tbl30Legios == null)
            {
                continue;
            }

            Context.Tbl30Legios.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Legio
    public async Task<bool> SaveLegio(Tbl30Legio selected)
    {
        var returnBool = false;

        try
        {
            if (selected.LegioName != null)
            {
                var datasetByName = await GetLegioSingleByLegioName(selected.LegioName);

                if (datasetByName != null && selected.LegioId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = GetLegioSingleByLegioId(selected.LegioId);

            if (selected.LegioName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.LegioName))
            {
                return false;
            }

            if (selected.LegioId == 0)
            {
                dataset = await LegioAdd(selected);
            }
            else
            {
                dataset = await LegioUpdate(dataset, selected);
            }

            try
            {
                await LegioSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.LegioName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.LegioName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl30Legio> LegioUpdate(Tbl30Legio home, Tbl30Legio selected)
    {
        if (home != null) //update
        {
            home.LegioName = selected.LegioName;
            home.InfraclassId = selected.InfraclassId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl30Legio> LegioAdd(Tbl30Legio selected)
    {
        var home = new Tbl30Legio() //add new
        {
            LegioName = selected.LegioName,
            InfraclassId = selected.InfraclassId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task LegioSave(Tbl30Legio home, Tbl30Legio selected)
    {
        if (selected.LegioId != 0)   //update
        {
            if (Context.Tbl30Legios == null)
            {
            }
            else
            {
                Context.Tbl30Legios.Update(home);
            }
        }
        else           //add
        {
            Context.Tbl30Legios?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Legio

    #region Ordo

    #region Get Ordo
    public ObservableCollection<Tbl33Ordo> GetTbl33OrdosCollectionOrderByOrdoNameFromLegioId(int legioId)
    {
        if (Context.Tbl33Ordos != null)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(Context.Tbl33Ordos
                .Where(e => e.LegioId == legioId)
                .OrderBy(k => k.OrdoName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl33Ordo>> GetLastDatasetInTbl33Ordos()
    {
        if (Context.Tbl33Ordos != null)
        {
            var collection = Context.Tbl33Ordos
                .OrderBy(c => c.OrdoId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl33Ordo> { collection };
            }
        }
        return null!;
    }

    public async Task<Tbl33Ordo> GetOrdoSingleByOrdoId(int ordoId)
    {
        if (Context.Tbl33Ordos != null)
        {
            var single = Context.Tbl33Ordos.SingleOrDefault(a => a.OrdoId == ordoId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public static async Task<Tbl33Ordo> GetOrdoSingleByOrdoName(string ordoName)
    {
        if (Context.Tbl33Ordos != null)
        {
            var single = Context.Tbl33Ordos.SingleOrDefault(a => a.OrdoName == ordoName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl33Ordo> GetOrdoSingleFirstDataset()
    {
        if (Context.Tbl33Ordos != null)
        {
            var single = Context.Tbl33Ordos.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    public ObservableCollection<Tbl33Ordo> GetTbl33OrdosCollectionOrderByOrdoName()
    {
        if (Context.Tbl33Ordos != null)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(Context.Tbl33Ordos
                .OrderBy(a => a.OrdoName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl33Ordo>> GetTbl33OrdosCollectionOrderByOrdoNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl33Ordos != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl33Ordo>(Context.Tbl33Ordos
                    .Where(e => e.OrdoId == id))
                : new ObservableCollection<Tbl33Ordo>(Context.Tbl33Ordos
                    .Where(e => e.OrdoName.StartsWith(searchName))
                    .OrderBy(a => a.OrdoName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl33Ordo> GetTbl33OrdosCollectionOrderByOrdoNameFromOrdoId(int ordoId)
    {
        if (Context.Tbl33Ordos != null)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(Context.Tbl33Ordos
                .Where(e => e.OrdoId == ordoId)
                .OrderBy(k => k.OrdoName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Ordo

    public async Task<ObservableCollection<Tbl33Ordo>> CopyOrdo(Tbl33Ordo selected)
    {
        if (Context.Tbl33Ordos != null)
        {
            var dataset = Context.Tbl33Ordos.FirstOrDefault(a => a.OrdoId == selected.OrdoId);
            var collection = new ObservableCollection<Tbl33Ordo>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl33Ordo
                {
                    //  OrdoName = CultRes.StringsRes.DatasetNew,
                    OrdoName = "New",
                    LegioId = dataset.LegioId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Ordo
    public async Task DeleteConnectedOrdos(Tbl30Legio selected)
    {
        if (Context.Tbl33Ordos != null)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(Context.Tbl33Ordos
                .Where(e => e.LegioId == selected.LegioId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl33Ordos.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    public async Task<bool> DeleteOrdo(Tbl33Ordo selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetOrdoSingleByOrdoId(selected.OrdoId);
            if (dataset != null)
            {
                await DeleteOrdoDataset(dataset);
                returnBool = true;

                if (selected.OrdoName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.OrdoName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteOrdoDataset(Tbl33Ordo selected)
    {
        if (Context.Tbl33Ordos == null)
        {
        }
        else
        {
            Context.Tbl33Ordos.Remove(selected);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl33Ordos(ObservableCollection<Tbl33Ordo> tbl33Ordos)
    {
        foreach (var t in tbl33Ordos)
        {
            if (Context.Tbl33Ordos == null)
            {
                continue;
            }

            Context.Tbl33Ordos.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Ordo
    public async Task<bool> SaveOrdo(Tbl33Ordo selected)
    {
        var returnBool = false;

        try
        {
            if (selected.OrdoName != null)
            {
                var datasetByName = await GetOrdoSingleByOrdoName(selected.OrdoName);

                if (datasetByName != null && selected.OrdoId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = await GetOrdoSingleByOrdoId(selected.OrdoId);

            if (selected.OrdoName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.OrdoName))
            {
                return false;
            }

            if (selected.OrdoId == 0)
            {
                dataset = await OrdoAdd(selected);
            }
            else
            {
                dataset = await OrdoUpdate(dataset, selected);
            }

            try
            {
                await OrdoSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.OrdoName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.OrdoName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl33Ordo> OrdoUpdate(Tbl33Ordo home, Tbl33Ordo selected)
    {
        if (home != null) //update
        {
            home.OrdoName = selected.OrdoName;
            home.LegioId = selected.LegioId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl33Ordo> OrdoAdd(Tbl33Ordo selected)
    {
        var home = new Tbl33Ordo() //add new
        {
            OrdoName = selected.OrdoName,
            LegioId = selected.LegioId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task OrdoSave(Tbl33Ordo home, Tbl33Ordo selected)
    {
        if (selected.OrdoId != 0)   //update
        {
            if (Context.Tbl33Ordos == null)
            {
            }
            else
            {
                Context.Tbl33Ordos.Update(home);
            }
        }
        else           //add
        {
            Context.Tbl33Ordos?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Ordo

    #region Subordo

    #region Get Subordo
    public ObservableCollection<Tbl36Subordo> GetTbl36SubordosCollectionOrderBySubordoNameFromOrdoId(int ordoId)
    {
        if (Context.Tbl36Subordos != null)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(Context.Tbl36Subordos
                .Where(e => e.OrdoId == ordoId)
                .OrderBy(k => k.SubordoName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl36Subordo>> GetLastDatasetInTbl36Subordos()
    {
        if (Context.Tbl36Subordos != null)
        {
            var collection = Context.Tbl36Subordos
                .OrderBy(c => c.SubordoId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl36Subordo> { collection };
            }
        }
        return null!;
    }

    public async Task<Tbl36Subordo> GetSubordoSingleBySubordoId(int subordoId)
    {
        if (Context.Tbl36Subordos != null)
        {
            var single = Context.Tbl36Subordos.SingleOrDefault(a => a.SubordoId == subordoId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public static async Task<Tbl36Subordo> GetSubordoSingleBySubordoName(string subordoName)
    {
        if (Context.Tbl36Subordos != null)
        {
            var single = Context.Tbl36Subordos.SingleOrDefault(a => a.SubordoName == subordoName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl36Subordo> GetSubordoSingleFirstDataset()
    {
        if (Context.Tbl36Subordos != null)
        {
            var single = Context.Tbl36Subordos.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    public ObservableCollection<Tbl36Subordo> GetTbl36SubordosCollectionOrderBySubordoName()
    {
        if (Context.Tbl36Subordos != null)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(Context.Tbl36Subordos
                .OrderBy(a => a.SubordoName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl36Subordo>> GetTbl36SubordosCollectionOrderBySubordoNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl36Subordos != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl36Subordo>(Context.Tbl36Subordos
                    .Where(e => e.SubordoId == id))
                : new ObservableCollection<Tbl36Subordo>(Context.Tbl36Subordos
                    .Where(e => e.SubordoName.StartsWith(searchName))
                    .OrderBy(a => a.SubordoName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl36Subordo> GetTbl36SubordosCollectionOrderBySubordoNameFromSubordoId(int subordoId)
    {
        if (Context.Tbl36Subordos != null)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(Context.Tbl36Subordos
                .Where(e => e.SubordoId == subordoId)
                .OrderBy(k => k.SubordoName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Subordo

    public async Task<ObservableCollection<Tbl36Subordo>> CopySubordo(Tbl36Subordo selected)
    {
        if (Context.Tbl36Subordos != null)
        {
            var dataset = Context.Tbl36Subordos.FirstOrDefault(a => a.SubordoId == selected.SubordoId);
            var collection = new ObservableCollection<Tbl36Subordo>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl36Subordo
                {
                    //  SubordoName = CultRes.StringsRes.DatasetNew,
                    SubordoName = "New",
                    OrdoId = dataset.OrdoId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Subordo
    public async Task DeleteConnectedSubordos(Tbl33Ordo selected)
    {
        if (Context.Tbl36Subordos != null)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(Context.Tbl36Subordos
                .Where(e => e.OrdoId == selected.OrdoId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl36Subordos.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task<bool> DeleteSubordo(Tbl36Subordo selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetSubordoSingleBySubordoId(selected.SubordoId);
            if (dataset != null)
            {
                await DeleteSubordoDataset(dataset);
                returnBool = true;

                if (selected.SubordoName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.SubordoName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteSubordoDataset(Tbl36Subordo selected)
    {
        Context.Tbl36Subordos?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl36Subordos(ObservableCollection<Tbl36Subordo> tbl36Subordos)
    {
        foreach (var t in tbl36Subordos)
        {
            if (Context.Tbl36Subordos == null)
            {
                continue;
            }

            Context.Tbl36Subordos.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Subordo
    public async Task<bool> SaveSubordo(Tbl36Subordo selected)
    {
        var returnBool = false;

        try
        {
            if (selected.SubordoName != null)
            {
                var datasetByName = await GetSubordoSingleBySubordoName(selected.SubordoName);

                if (datasetByName != null && selected.SubordoId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = await GetSubordoSingleBySubordoId(selected.SubordoId);

            if (selected.SubordoName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.SubordoName))
            {
                return false;
            }

            if (selected.SubordoId == 0)
            {
                dataset = await SubordoAdd(selected);
            }
            else
            {
                dataset = await SubordoUpdate(dataset, selected);
            }

            try
            {
                await SubordoSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.SubordoName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.SubordoName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl36Subordo> SubordoUpdate(Tbl36Subordo home, Tbl36Subordo selected)
    {
        if (home != null) //update
        {
            home.SubordoName = selected.SubordoName;
            home.OrdoId = selected.OrdoId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl36Subordo> SubordoAdd(Tbl36Subordo selected)
    {
        var home = new Tbl36Subordo() //add new
        {
            SubordoName = selected.SubordoName,
            OrdoId = selected.OrdoId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task SubordoSave(Tbl36Subordo home, Tbl36Subordo selected)
    {
        if (selected.SubordoId != 0)   //update
        {
            if (Context.Tbl36Subordos == null)
            {
            }
            else
            {
                Context.Tbl36Subordos.Update(home);
            }
        }
        else           //add
        {
            Context.Tbl36Subordos?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Subordo

    #region Infraordo

    #region Get Infraordo
    public ObservableCollection<Tbl39Infraordo> GetTbl39InfraordosCollectionOrderByInfraordoNameFromSubordoId(int subordoId)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(Context.Tbl39Infraordos
                .Where(e => e.SubordoId == subordoId)
                .OrderBy(k => k.InfraordoName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl39Infraordo>> GetLastDatasetInTbl39Infraordos()
    {
        if (Context.Tbl39Infraordos != null)
        {
            var collection = Context.Tbl39Infraordos
                .OrderBy(c => c.InfraordoId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl39Infraordo> { collection };
            }
        }
        return null!;
    }

    public async Task<Tbl39Infraordo> GetInfraordoSingleByInfraordoId(int infraordoId)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var single = Context.Tbl39Infraordos.SingleOrDefault(a => a.InfraordoId == infraordoId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public static async Task<Tbl39Infraordo> GetInfraordoSingleByInfraordoName(string infraordoName)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var single = Context.Tbl39Infraordos.SingleOrDefault(a => a.InfraordoName == infraordoName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl39Infraordo> GetInfraordoSingleFirstDataset()
    {
        if (Context.Tbl39Infraordos != null)
        {
            var single = Context.Tbl39Infraordos.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    public ObservableCollection<Tbl39Infraordo> GetTbl39InfraordosCollectionOrderByInfraordoName()
    {
        if (Context.Tbl39Infraordos != null)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(Context.Tbl39Infraordos
                .OrderBy(a => a.InfraordoName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl39Infraordo>> GetTbl39InfraordosCollectionOrderByInfraordoNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl39Infraordo>(Context.Tbl39Infraordos
                    .Where(e => e.InfraordoId == id))
                : new ObservableCollection<Tbl39Infraordo>(Context.Tbl39Infraordos
                    .Where(e => e.InfraordoName.StartsWith(searchName))
                    .OrderBy(a => a.InfraordoName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl39Infraordo> GetTbl39InfraordosCollectionOrderByInfraordoNameFromInfraordoId(int infraordoId)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(Context.Tbl39Infraordos
                .Where(e => e.InfraordoId == infraordoId)
                .OrderBy(k => k.InfraordoName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Infraordo

    public async Task<ObservableCollection<Tbl39Infraordo>> CopyInfraordo(Tbl39Infraordo selected)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var dataset = Context.Tbl39Infraordos.FirstOrDefault(a => a.InfraordoId == selected.InfraordoId);
            var collection = new ObservableCollection<Tbl39Infraordo>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl39Infraordo
                {
                    //  InfraordoName = CultRes.StringsRes.DatasetNew,
                    InfraordoName = "New",
                    SubordoId = dataset.SubordoId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Infraordo
    public async Task DeleteConnectedInfraordos(Tbl36Subordo selected)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(Context.Tbl39Infraordos
                .Where(e => e.SubordoId == selected.SubordoId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl39Infraordos.Remove(t);
                }

                Context.SaveChanges();
            }
        }
        await Task.CompletedTask;
    }
    public async Task<bool> DeleteInfraordo(Tbl39Infraordo selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetInfraordoSingleByInfraordoId(selected.InfraordoId);
            if (dataset != null)
            {
                await DeleteInfraordoDataset(dataset);
                returnBool = true;

                if (selected.InfraordoName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.InfraordoName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteInfraordoDataset(Tbl39Infraordo selected)
    {
        Context.Tbl39Infraordos?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl39Infraordos(ObservableCollection<Tbl39Infraordo> tbl39Infraordos)
    {
        foreach (var t in tbl39Infraordos)
        {
            Context.Tbl39Infraordos?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Infraordo
    public async Task<bool> SaveInfraordo(Tbl39Infraordo selected)
    {
        var returnBool = false;

        try
        {
            if (selected.InfraordoName != null)
            {
                var datasetByName = await GetInfraordoSingleByInfraordoName(selected.InfraordoName);

                if (datasetByName != null && selected.InfraordoId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = await GetInfraordoSingleByInfraordoId(selected.InfraordoId);

            if (selected.InfraordoName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.InfraordoName))
            {
                return false;
            }

            if (selected.InfraordoId == 0)
            {
                dataset = await InfraordoAdd(selected);
            }
            else
            {
                dataset = await InfraordoUpdate(dataset, selected);
            }

            try
            {
                await InfraordoSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.InfraordoName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.InfraordoName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl39Infraordo> InfraordoUpdate(Tbl39Infraordo home, Tbl39Infraordo selected)
    {
        if (home != null) //update
        {
            home.InfraordoName = selected.InfraordoName;
            home.SubordoId = selected.SubordoId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl39Infraordo> InfraordoAdd(Tbl39Infraordo selected)
    {
        var home = new Tbl39Infraordo() //add new
        {
            InfraordoName = selected.InfraordoName,
            SubordoId = selected.SubordoId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task InfraordoSave(Tbl39Infraordo home, Tbl39Infraordo selected)
    {
        if (selected.InfraordoId != 0)   //update
        {
            Context.Tbl39Infraordos?.Update(home);
        }
        else           //add
        {
            Context.Tbl39Infraordos?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Infraordo

    #region Superfamily

    #region Get Superfamily
    public ObservableCollection<Tbl42Superfamily> GetTbl42SuperfamiliesCollectionOrderBySuperfamilyNameFromInfraordoId(int infraordoId)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(Context.Tbl42Superfamilies
                .Where(e => e.InfraordoId == infraordoId)
                .OrderBy(k => k.SuperfamilyName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl42Superfamily>> GetLastDatasetInTbl42Superfamilies()
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var collection = Context.Tbl42Superfamilies
                .OrderBy(c => c.SuperfamilyId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl42Superfamily> { collection };
            }
        }
        return null!;
    }

    public async Task<Tbl42Superfamily> GetSuperfamilySingleBySuperfamilyId(int superfamilyId)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var single = Context.Tbl42Superfamilies.SingleOrDefault(a => a.SuperfamilyId == superfamilyId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public static async Task<Tbl42Superfamily> GetSuperfamilySingleBySuperfamilyName(string superfamilyName)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var single = Context.Tbl42Superfamilies.SingleOrDefault(a => a.SuperfamilyName == superfamilyName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl42Superfamily> GetSuperfamilySingleFirstDataset()
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var single = Context.Tbl42Superfamilies.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    public ObservableCollection<Tbl42Superfamily> GetTbl42SuperfamiliesCollectionOrderBySuperfamilyName()
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(Context.Tbl42Superfamilies
                .OrderBy(a => a.SuperfamilyName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl42Superfamily>> GetTbl42SuperfamiliesCollectionOrderBySuperfamilyNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl42Superfamily>(Context.Tbl42Superfamilies
                    .Where(e => e.SuperfamilyId == id))
                : new ObservableCollection<Tbl42Superfamily>(Context.Tbl42Superfamilies
                    .Where(e => e.SuperfamilyName.StartsWith(searchName))
                    .OrderBy(a => a.SuperfamilyName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl42Superfamily> GetTbl42SuperfamiliesCollectionOrderBySuperfamilyNameFromSuperfamilyId(int superfamilyId)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(Context.Tbl42Superfamilies
                .Where(e => e.SuperfamilyId == superfamilyId)
                .OrderBy(k => k.SuperfamilyName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Superfamily

    public async Task<ObservableCollection<Tbl42Superfamily>> CopySuperfamily(Tbl42Superfamily selected)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var dataset = Context.Tbl42Superfamilies.FirstOrDefault(a => a.SuperfamilyId == selected.SuperfamilyId);
            var collection = new ObservableCollection<Tbl42Superfamily>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl42Superfamily
                {
                    //  SuperfamilyName = CultRes.StringsRes.DatasetNew,
                    SuperfamilyName = "New",
                    InfraordoId = dataset.InfraordoId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Superfamily
    public async Task DeleteConnectedSuperfamilies(Tbl39Infraordo selected)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(Context.Tbl42Superfamilies
                .Where(e => e.InfraordoId == selected.InfraordoId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl42Superfamilies.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    public async Task<bool> DeleteSuperfamily(Tbl42Superfamily selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetSuperfamilySingleBySuperfamilyId(selected.SuperfamilyId);
            if (dataset != null)
            {
                await DeleteSuperfamilyDataset(dataset);
                returnBool = true;

                if (selected.SuperfamilyName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.SuperfamilyName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteSuperfamilyDataset(Tbl42Superfamily selected)
    {
        Context.Tbl42Superfamilies?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl42Superfamilies(ObservableCollection<Tbl42Superfamily> tbl42Superfamilies)
    {
        foreach (var t in tbl42Superfamilies)
        {
            Context.Tbl42Superfamilies?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Superfamily
    public async Task<bool> SaveSuperfamily(Tbl42Superfamily selected)
    {
        var returnBool = false;

        try
        {
            if (selected.SuperfamilyName != null)
            {
                var datasetByName = await GetSuperfamilySingleBySuperfamilyName(selected.SuperfamilyName);

                if (datasetByName != null && selected.SuperfamilyId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = await GetSuperfamilySingleBySuperfamilyId(selected.SuperfamilyId);

            if (selected.SuperfamilyName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.SuperfamilyName))
            {
                return false;
            }

            if (selected.SuperfamilyId == 0)
            {
                dataset = await SuperfamilyAdd(selected);
            }
            else
            {
                dataset = await SuperfamilyUpdate(dataset, selected);
            }

            try
            {
                await SuperfamilySave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.SuperfamilyName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.SuperfamilyName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl42Superfamily> SuperfamilyUpdate(Tbl42Superfamily home, Tbl42Superfamily selected)
    {
        if (home != null) //update
        {
            home.SuperfamilyName = selected.SuperfamilyName;
            home.InfraordoId = selected.InfraordoId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl42Superfamily> SuperfamilyAdd(Tbl42Superfamily selected)
    {
        var home = new Tbl42Superfamily() //add new
        {
            SuperfamilyName = selected.SuperfamilyName,
            InfraordoId = selected.InfraordoId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task SuperfamilySave(Tbl42Superfamily home, Tbl42Superfamily selected)
    {
        if (selected.SuperfamilyId != 0)   //update
        {
            Context.Tbl42Superfamilies?.Update(home);
        }
        else           //add
        {
            Context.Tbl42Superfamilies?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Superfamily

    #region Family

    #region Get Family
    public ObservableCollection<Tbl45Family> GetTbl45FamiliesCollectionOrderByFamilyNameFromSuperfamilyId(int superfamilyId)
    {
        if (Context.Tbl45Families != null)
        {
            var collection = new ObservableCollection<Tbl45Family>(Context.Tbl45Families
                .Where(e => e.SuperfamilyId == superfamilyId)
                .OrderBy(k => k.FamilyName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl45Family>> GetLastDatasetInTbl45Families()
    {
        if (Context.Tbl45Families != null)
        {
            var collection = Context.Tbl45Families
                .OrderBy(c => c.FamilyId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl45Family> { collection };
            }
        }
        return null!;
    }
    public async Task<Tbl45Family> GetFamilySingleByFamilyId(int familyId)
    {
        if (Context.Tbl45Families != null)
        {
            var single = Context.Tbl45Families.SingleOrDefault(a => a.FamilyId == familyId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public static async Task<Tbl45Family> GetFamilySingleByFamilyName(string familyName)
    {
        if (Context.Tbl45Families != null)
        {
            var single = Context.Tbl45Families.SingleOrDefault(a => a.FamilyName == familyName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public async Task<Tbl45Family> GetFamilySingleFirstDataset()
    {
        if (Context.Tbl45Families != null)
        {
            var single = Context.Tbl45Families.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    public ObservableCollection<Tbl45Family> GetTbl45FamiliesCollectionOrderByFamilyName()
    {
        if (Context.Tbl45Families != null)
        {
            var collection = new ObservableCollection<Tbl45Family>(Context.Tbl45Families
                .OrderBy(a => a.FamilyName)
            );
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl45Family>> GetTbl45FamiliesCollectionOrderByFamilyNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl45Families != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl45Family>(Context.Tbl45Families
                    .Where(e => e.FamilyId == id))
                : new ObservableCollection<Tbl45Family>(Context.Tbl45Families
                    .Where(e => e.FamilyName.StartsWith(searchName))
                    .OrderBy(a => a.FamilyName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl45Family> GetTbl45FamiliesCollectionOrderByFamilyNameFromFamilyId(int familyId)
    {
        if (Context.Tbl45Families != null)
        {
            var collection = new ObservableCollection<Tbl45Family>(Context.Tbl45Families
                .Where(e => e.FamilyId == familyId)
                .OrderBy(k => k.FamilyName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Family

    public async Task<ObservableCollection<Tbl45Family>> CopyFamily(Tbl45Family selected)
    {
        if (Context.Tbl45Families != null)
        {
            var dataset = Context.Tbl45Families.FirstOrDefault(a => a.FamilyId == selected.FamilyId);
            var collection = new ObservableCollection<Tbl45Family>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl45Family
                {
                    //  FamilyName = CultRes.StringsRes.DatasetNew,
                    FamilyName = "New",
                    SuperfamilyId = dataset.SuperfamilyId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Family
    public async Task DeleteConnectedFamilies(Tbl42Superfamily selected)
    {
        if (Context.Tbl45Families != null)
        {
            var collection = new ObservableCollection<Tbl45Family>(Context.Tbl45Families
                .Where(e => e.SuperfamilyId == selected.SuperfamilyId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl45Families.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task<bool> DeleteFamily(Tbl45Family selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetFamilySingleByFamilyId(selected.FamilyId);
            if (dataset != null)
            {
                await DeleteFamilyDataset(dataset);
                returnBool = true;

                if (selected.FamilyName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.FamilyName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteFamilyDataset(Tbl45Family selected)
    {
        Context.Tbl45Families?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl45Families(ObservableCollection<Tbl45Family> tbl45Families)
    {
        foreach (var t in tbl45Families)
        {
            Context.Tbl45Families?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Family
    public async Task<bool> SaveFamily(Tbl45Family selected)
    {
        var returnBool = false;

        try
        {
            if (selected.FamilyName != null)
            {
                var datasetByName = await GetFamilySingleByFamilyName(selected.FamilyName);

                if (datasetByName != null && selected.FamilyId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = await GetFamilySingleByFamilyId(selected.FamilyId);

            if (selected.FamilyName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.FamilyName))
            {
                return false;
            }

            if (selected.FamilyId == 0)
            {
                dataset = await FamilyAdd(selected);
            }
            else
            {
                dataset = await FamilyUpdate(dataset, selected);
            }

            try
            {
                await FamilySave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.FamilyName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.FamilyName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl45Family> FamilyUpdate(Tbl45Family home, Tbl45Family selected)
    {
        if (home != null) //update
        {
            home.FamilyName = selected.FamilyName;
            home.SuperfamilyId = selected.SuperfamilyId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl45Family> FamilyAdd(Tbl45Family selected)
    {
        var home = new Tbl45Family() //add new
        {
            FamilyName = selected.FamilyName,
            SuperfamilyId = selected.SuperfamilyId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task FamilySave(Tbl45Family home, Tbl45Family selected)
    {
        if (selected.FamilyId != 0)   //update
        {
            Context.Tbl45Families?.Update(home);
        }
        else           //add
        {
            Context.Tbl45Families?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Family

    #region Subfamily

    #region Get Subfamily
    public ObservableCollection<Tbl48Subfamily> GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromFamilyId(int familyId)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var collection = new ObservableCollection<Tbl48Subfamily>(Context.Tbl48Subfamilies
                .Where(e => e.FamilyId == familyId)
                .OrderBy(k => k.SubfamilyName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl48Subfamily>> GetLastDatasetInTbl48Subfamilies()
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var collection = Context.Tbl48Subfamilies
                .OrderBy(c => c.SubfamilyId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl48Subfamily> { collection };
            }
        }
        return null!;
    }

    public async Task<Tbl48Subfamily> GetSubfamilySingleBySubfamilyId(int subfamilyId)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var single = Context.Tbl48Subfamilies.SingleOrDefault(a => a.SubfamilyId == subfamilyId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public static async Task<Tbl48Subfamily> GetSubfamilySingleBySubfamilyName(string subfamilyName)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var single = Context.Tbl48Subfamilies.SingleOrDefault(a => a.SubfamilyName == subfamilyName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl48Subfamily> GetSubfamilySingleFirstDataset()
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var single = Context.Tbl48Subfamilies.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    public ObservableCollection<Tbl48Subfamily> GetTbl48SubfamiliesCollectionOrderBySubfamilyName()
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var collection = new ObservableCollection<Tbl48Subfamily>(Context.Tbl48Subfamilies
                .OrderBy(a => a.SubfamilyName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl48Subfamily>> GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl48Subfamily>(Context.Tbl48Subfamilies
                    .Where(e => e.SubfamilyId == id))
                : new ObservableCollection<Tbl48Subfamily>(Context.Tbl48Subfamilies
                    .Where(e => e.SubfamilyName.StartsWith(searchName))
                    .OrderBy(a => a.SubfamilyName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl48Subfamily> GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromSubfamilyId(int subfamilyId)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var collection = new ObservableCollection<Tbl48Subfamily>(Context.Tbl48Subfamilies
                .Where(e => e.SubfamilyId == subfamilyId)
                .OrderBy(k => k.SubfamilyName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Subfamily

    public async Task<ObservableCollection<Tbl48Subfamily>> CopySubfamily(Tbl48Subfamily selected)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var dataset = Context.Tbl48Subfamilies.FirstOrDefault(a => a.SubfamilyId == selected.SubfamilyId);
            var collection = new ObservableCollection<Tbl48Subfamily>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl48Subfamily
                {
                    //  SubfamilyName = CultRes.StringsRes.DatasetNew,
                    SubfamilyName = "New",
                    FamilyId = dataset.FamilyId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Subfamily
    public async Task DeleteConnectedSubfamilies(Tbl45Family selected)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var collection = new ObservableCollection<Tbl48Subfamily>(Context.Tbl48Subfamilies
                .Where(e => e.FamilyId == selected.FamilyId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl48Subfamilies.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task<bool> DeleteSubfamily(Tbl48Subfamily selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetSubfamilySingleBySubfamilyId(selected.SubfamilyId);
            if (dataset != null)
            {
                await DeleteSubfamilyDataset(dataset);
                returnBool = true;

                if (selected.SubfamilyName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.SubfamilyName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteSubfamilyDataset(Tbl48Subfamily selected)
    {
        Context.Tbl48Subfamilies?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl48Subfamilies(ObservableCollection<Tbl48Subfamily> tbl48Subfamilies)
    {
        foreach (var t in tbl48Subfamilies)
        {
            Context.Tbl48Subfamilies?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Subfamily
    public async Task<bool> SaveSubfamily(Tbl48Subfamily selected)
    {
        var returnBool = false;

        try
        {
            if (selected.SubfamilyName != null)
            {
                var datasetByName = await GetSubfamilySingleBySubfamilyName(selected.SubfamilyName);

                if (datasetByName != null && selected.SubfamilyId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = await GetSubfamilySingleBySubfamilyId(selected.SubfamilyId);

            if (selected.SubfamilyName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.SubfamilyName))
            {
                return false;
            }

            if (selected.SubfamilyId == 0)
            {
                dataset = await SubfamilyAdd(selected);
            }
            else
            {
                dataset = await SubfamilyUpdate(dataset, selected);
            }

            try
            {
                await SubfamilySave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.SubfamilyName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.SubfamilyName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl48Subfamily> SubfamilyUpdate(Tbl48Subfamily home, Tbl48Subfamily selected)
    {
        if (home != null) //update
        {
            home.SubfamilyName = selected.SubfamilyName;
            home.FamilyId = selected.FamilyId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl48Subfamily> SubfamilyAdd(Tbl48Subfamily selected)
    {
        var home = new Tbl48Subfamily() //add new
        {
            SubfamilyName = selected.SubfamilyName,
            FamilyId = selected.FamilyId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task SubfamilySave(Tbl48Subfamily home, Tbl48Subfamily selected)
    {
        if (selected.SubfamilyId != 0)   //update
        {
            Context.Tbl48Subfamilies?.Update(home);
        }
        else           //add
        {
            Context.Tbl48Subfamilies?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Subfamily

    #region Infrafamily

    #region Get Infrafamily
    public ObservableCollection<Tbl51Infrafamily> GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromSubfamilyId(int subfamilyId)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var collection = new ObservableCollection<Tbl51Infrafamily>(Context.Tbl51Infrafamilies
                .Where(e => e.SubfamilyId == subfamilyId)
                .OrderBy(k => k.InfrafamilyName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl51Infrafamily>> GetLastDatasetInTbl51Infrafamilies()
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var collection = Context.Tbl51Infrafamilies
                .OrderBy(c => c.InfrafamilyId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl51Infrafamily> { collection };
            }
        }
        return null!;
    }

    public async Task<Tbl51Infrafamily> GetInfrafamilySingleByInfrafamilyId(int infrafamilyId)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var single = Context.Tbl51Infrafamilies.SingleOrDefault(a => a.InfrafamilyId == infrafamilyId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public static async Task<Tbl51Infrafamily> GetInfrafamilySingleByInfrafamilyName(string infrafamilyName)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var single = Context.Tbl51Infrafamilies.SingleOrDefault(a => a.InfrafamilyName == infrafamilyName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl51Infrafamily> GetInfrafamilySingleFirstDataset()
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var single = Context.Tbl51Infrafamilies.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    public ObservableCollection<Tbl51Infrafamily> GetTbl51InfrafamiliesCollectionOrderByInfrafamilyName()
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var collection = new ObservableCollection<Tbl51Infrafamily>(Context.Tbl51Infrafamilies
                .OrderBy(a => a.InfrafamilyName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl51Infrafamily>> GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl51Infrafamily>(Context.Tbl51Infrafamilies
                    .Where(e => e.InfrafamilyId == id))
                : new ObservableCollection<Tbl51Infrafamily>(Context.Tbl51Infrafamilies
                    .Where(e => e.InfrafamilyName.StartsWith(searchName))
                    .OrderBy(a => a.InfrafamilyName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl51Infrafamily> GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromInfrafamilyId(int infrafamilyId)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var collection = new ObservableCollection<Tbl51Infrafamily>(Context.Tbl51Infrafamilies
                .Where(e => e.InfrafamilyId == infrafamilyId)
                .OrderBy(k => k.InfrafamilyName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Infrafamily

    public async Task<ObservableCollection<Tbl51Infrafamily>> CopyInfrafamily(Tbl51Infrafamily selected)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var dataset = Context.Tbl51Infrafamilies.FirstOrDefault(a => a.InfrafamilyId == selected.InfrafamilyId);
            var collection = new ObservableCollection<Tbl51Infrafamily>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl51Infrafamily
                {
                    //  InfrafamilyName = CultRes.StringsRes.DatasetNew,
                    InfrafamilyName = "New",
                    SubfamilyId = dataset.SubfamilyId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Infrafamily
    public async Task DeleteConnectedInfrafamilies(Tbl48Subfamily selected)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var collection = new ObservableCollection<Tbl51Infrafamily>(Context.Tbl51Infrafamilies
                .Where(e => e.SubfamilyId == selected.SubfamilyId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl51Infrafamilies.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task<bool> DeleteInfrafamily(Tbl51Infrafamily selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetInfrafamilySingleByInfrafamilyId(selected.InfrafamilyId);
            if (dataset != null)
            {
                await DeleteInfrafamilyDataset(dataset);
                returnBool = true;

                if (selected.InfrafamilyName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.InfrafamilyName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteInfrafamilyDataset(Tbl51Infrafamily selected)
    {
        Context.Tbl51Infrafamilies?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl51Infrafamilies(ObservableCollection<Tbl51Infrafamily> tbl51Infrafamilies)
    {
        foreach (var t in tbl51Infrafamilies)
        {
            Context.Tbl51Infrafamilies?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Infrafamily
    public async Task<bool> SaveInfrafamily(Tbl51Infrafamily selected)
    {
        var returnBool = false;

        try
        {
            if (selected.InfrafamilyName != null)
            {
                var datasetByName = await GetInfrafamilySingleByInfrafamilyName(selected.InfrafamilyName);

                if (datasetByName != null && selected.InfrafamilyId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = await GetInfrafamilySingleByInfrafamilyId(selected.InfrafamilyId);

            if (selected.InfrafamilyName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.InfrafamilyName))
            {
                return false;
            }

            if (selected.InfrafamilyId == 0)
            {
                dataset = await InfrafamilyAdd(selected);
            }
            else
            {
                dataset = await InfrafamilyUpdate(dataset, selected);
            }

            try
            {
                await InfrafamilySave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.InfrafamilyName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.InfrafamilyName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl51Infrafamily> InfrafamilyUpdate(Tbl51Infrafamily home, Tbl51Infrafamily selected)
    {
        if (home != null) //update
        {
            home.InfrafamilyName = selected.InfrafamilyName;
            home.SubfamilyId = selected.SubfamilyId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl51Infrafamily> InfrafamilyAdd(Tbl51Infrafamily selected)
    {
        var home = new Tbl51Infrafamily() //add new
        {
            InfrafamilyName = selected.InfrafamilyName,
            SubfamilyId = selected.SubfamilyId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task InfrafamilySave(Tbl51Infrafamily home, Tbl51Infrafamily selected)
    {
        if (selected.InfrafamilyId != 0)   //update
        {
            Context.Tbl51Infrafamilies?.Update(home);
        }
        else           //add
        {
            Context.Tbl51Infrafamilies?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Infrafamily

    #region Supertribus

    #region Get Supertribus
    public ObservableCollection<Tbl54Supertribus> GetTbl54SupertribussesCollectionOrderBySupertribusNameFromInfrafamilyId(int infrafamilyId)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var collection = new ObservableCollection<Tbl54Supertribus>(Context.Tbl54Supertribusses
                .Where(e => e.InfrafamilyId == infrafamilyId)
                .OrderBy(k => k.SupertribusName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl54Supertribus>> GetLastDatasetInTbl54Supertribusses()
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var collection = Context.Tbl54Supertribusses
                .OrderBy(c => c.SupertribusId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl54Supertribus> { collection };
            }
        }
        return null!;
    }

    public async Task<Tbl54Supertribus> GetSupertribusSingleBySupertribusId(int supertribusId)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var single = Context.Tbl54Supertribusses.SingleOrDefault(a => a.SupertribusId == supertribusId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public static async Task<Tbl54Supertribus> GetSupertribusSingleBySupertribusName(string supertribusName)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var single = Context.Tbl54Supertribusses.SingleOrDefault(a => a.SupertribusName == supertribusName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl54Supertribus> GetSupertribusSingleFirstDataset()
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var single = Context.Tbl54Supertribusses.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    public ObservableCollection<Tbl54Supertribus> GetTbl54SupertribussesCollectionOrderBySupertribusName()
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var collection = new ObservableCollection<Tbl54Supertribus>(Context.Tbl54Supertribusses
                .OrderBy(a => a.SupertribusName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl54Supertribus>> GetTbl54SupertribussesCollectionOrderBySupertribusNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl54Supertribus>(Context.Tbl54Supertribusses
                    .Where(e => e.SupertribusId == id))
                : new ObservableCollection<Tbl54Supertribus>(Context.Tbl54Supertribusses
                    .Where(e => e.SupertribusName.StartsWith(searchName))
                    .OrderBy(a => a.SupertribusName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl54Supertribus> GetTbl54SupertribussesCollectionOrderBySupertribusNameFromSupertribusId(int supertribusId)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var collection = new ObservableCollection<Tbl54Supertribus>(Context.Tbl54Supertribusses
                .Where(e => e.SupertribusId == supertribusId)
                .OrderBy(k => k.SupertribusName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Supertribus

    public async Task<ObservableCollection<Tbl54Supertribus>> CopySupertribus(Tbl54Supertribus selected)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var dataset = Context.Tbl54Supertribusses.FirstOrDefault(a => a.SupertribusId == selected.SupertribusId);
            var collection = new ObservableCollection<Tbl54Supertribus>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl54Supertribus
                {
                    //  SupertribusName = CultRes.StringsRes.DatasetNew,
                    SupertribusName = "New",
                    InfrafamilyId = dataset.InfrafamilyId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Supertribus
    public async Task DeleteConnectedSupertribusses(Tbl51Infrafamily selected)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var collection = new ObservableCollection<Tbl54Supertribus>(Context.Tbl54Supertribusses
                .Where(e => e.InfrafamilyId == selected.InfrafamilyId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl54Supertribusses.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task<bool> DeleteSupertribus(Tbl54Supertribus selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetSupertribusSingleBySupertribusId(selected.SupertribusId);
            if (dataset != null)
            {
                await DeleteSupertribusDataset(dataset);
                returnBool = true;

                if (selected.SupertribusName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.SupertribusName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteSupertribusDataset(Tbl54Supertribus selected)
    {
        Context.Tbl54Supertribusses?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl54Supertribusses(ObservableCollection<Tbl54Supertribus> tbl54Supertribusses)
    {
        foreach (var t in tbl54Supertribusses)
        {
            Context.Tbl54Supertribusses?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Supertribus
    public async Task<bool> SaveSupertribus(Tbl54Supertribus selected)
    {
        var returnBool = false;

        try
        {
            if (selected.SupertribusName != null)
            {
                var datasetByName = await GetSupertribusSingleBySupertribusName(selected.SupertribusName);

                if (datasetByName != null && selected.SupertribusId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = await GetSupertribusSingleBySupertribusId(selected.SupertribusId);

            if (selected.SupertribusName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.SupertribusName))
            {
                return false;
            }

            if (selected.SupertribusId == 0)
            {
                dataset = await SupertribusAdd(selected);
            }
            else
            {
                dataset = await SupertribusUpdate(dataset, selected);
            }

            try
            {
                await SupertribusSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.SupertribusName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.SupertribusName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl54Supertribus> SupertribusUpdate(Tbl54Supertribus home, Tbl54Supertribus selected)
    {
        if (home != null) //update
        {
            home.SupertribusName = selected.SupertribusName;
            home.InfrafamilyId = selected.InfrafamilyId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl54Supertribus> SupertribusAdd(Tbl54Supertribus selected)
    {
        var home = new Tbl54Supertribus() //add new
        {
            SupertribusName = selected.SupertribusName,
            InfrafamilyId = selected.InfrafamilyId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task SupertribusSave(Tbl54Supertribus home, Tbl54Supertribus selected)
    {
        if (selected.SupertribusId != 0)   //update
        {
            Context.Tbl54Supertribusses?.Update(home);
        }
        else           //add
        {
            Context.Tbl54Supertribusses?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Supertribus

    #region Tribus

    #region Get Tribus
    public ObservableCollection<Tbl57Tribus> GetTbl57TribussesCollectionOrderByTribusNameFromSupertribusId(int supertribusId)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var collection = new ObservableCollection<Tbl57Tribus>(Context.Tbl57Tribusses
                .Where(e => e.SupertribusId == supertribusId)
                .OrderBy(k => k.TribusName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl57Tribus>> GetLastDatasetInTbl57Tribusses()
    {
        if (Context.Tbl57Tribusses != null)
        {
            var collection = Context.Tbl57Tribusses
                .OrderBy(c => c.TribusId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl57Tribus> { collection };
            }
        }
        return null!;
    }

    public async Task<Tbl57Tribus> GetTribusSingleByTribusId(int tribusId)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var single = Context.Tbl57Tribusses.SingleOrDefault(a => a.TribusId == tribusId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public static async Task<Tbl57Tribus> GetTribusSingleByTribusName(string tribusName)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var single = Context.Tbl57Tribusses.SingleOrDefault(a => a.TribusName == tribusName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl57Tribus> GetTribusSingleFirstDataset()
    {
        if (Context.Tbl57Tribusses != null)
        {
            var single = Context.Tbl57Tribusses.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }
    public ObservableCollection<Tbl57Tribus> GetTbl57TribussesCollectionOrderByTribusName()
    {
        if (Context.Tbl57Tribusses != null)
        {
            var collection = new ObservableCollection<Tbl57Tribus>(Context.Tbl57Tribusses
                .OrderBy(a => a.TribusName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl57Tribus>> GetTbl57TribussesCollectionOrderByTribusNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl57Tribus>(Context.Tbl57Tribusses
                    .Where(e => e.TribusId == id))
                : new ObservableCollection<Tbl57Tribus>(Context.Tbl57Tribusses
                    .Where(e => e.TribusName.StartsWith(searchName))
                    .OrderBy(a => a.TribusName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl57Tribus> GetTbl57TribussesCollectionOrderByTribusNameFromTribusId(int tribusId)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var collection = new ObservableCollection<Tbl57Tribus>(Context.Tbl57Tribusses
                .Where(e => e.TribusId == tribusId)
                .OrderBy(k => k.TribusName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Tribus

    public async Task<ObservableCollection<Tbl57Tribus>> CopyTribus(Tbl57Tribus selected)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var dataset = Context.Tbl57Tribusses.FirstOrDefault(a => a.TribusId == selected.TribusId);
            var collection = new ObservableCollection<Tbl57Tribus>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl57Tribus
                {
                    //  TribusName = CultRes.StringsRes.DatasetNew,
                    TribusName = "New",
                    SupertribusId = dataset.SupertribusId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Tribus
    public async Task DeleteConnectedTribusses(Tbl54Supertribus selected)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var collection = new ObservableCollection<Tbl57Tribus>(Context.Tbl57Tribusses
                .Where(e => e.SupertribusId == selected.SupertribusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl57Tribusses.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task<bool> DeleteTribus(Tbl57Tribus selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetTribusSingleByTribusId(selected.TribusId);
            if (dataset != null)
            {
                await DeleteTribusDataset(dataset);
                returnBool = true;

                if (selected.TribusName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.TribusName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteTribusDataset(Tbl57Tribus selected)
    {
        Context.Tbl57Tribusses?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl57Tribusses(ObservableCollection<Tbl57Tribus> tbl57Tribusses)
    {
        foreach (var t in tbl57Tribusses)
        {
            Context.Tbl57Tribusses?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Tribus
    public async Task<bool> SaveTribus(Tbl57Tribus selected)
    {
        var returnBool = false;

        try
        {
            if (selected.TribusName != null)
            {
                var datasetByName = await GetTribusSingleByTribusName(selected.TribusName);

                if (datasetByName != null && selected.TribusId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = await GetTribusSingleByTribusId(selected.TribusId);

            if (selected.TribusName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.TribusName))
            {
                return false;
            }

            if (selected.TribusId == 0)
            {
                dataset = await TribusAdd(selected);
            }
            else
            {
                dataset = await TribusUpdate(dataset, selected);
            }

            try
            {
                await TribusSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.TribusName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.TribusName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl57Tribus> TribusUpdate(Tbl57Tribus home, Tbl57Tribus selected)
    {
        if (home != null) //update
        {
            home.TribusName = selected.TribusName;
            home.SupertribusId = selected.SupertribusId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl57Tribus> TribusAdd(Tbl57Tribus selected)
    {
        var home = new Tbl57Tribus() //add new
        {
            TribusName = selected.TribusName,
            SupertribusId = selected.SupertribusId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task TribusSave(Tbl57Tribus home, Tbl57Tribus selected)
    {
        if (selected.TribusId != 0)   //update
        {
            Context.Tbl57Tribusses?.Update(home);
        }
        else           //add
        {
            Context.Tbl57Tribusses?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Tribus

    #region Subtribus

    #region Get Subtribus
    public ObservableCollection<Tbl60Subtribus> GetTbl60SubtribussesCollectionOrderBySubtribusNameFromTribusId(int tribusId)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var collection = new ObservableCollection<Tbl60Subtribus>(Context.Tbl60Subtribusses
                .Where(e => e.TribusId == tribusId)
                .OrderBy(k => k.SubtribusName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl60Subtribus>> GetLastDatasetInTbl60Subtribusses()
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var collection = Context.Tbl60Subtribusses
                .OrderBy(c => c.SubtribusId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl60Subtribus> { collection };
            }
        }
        return null!;
    }

    public async Task<Tbl60Subtribus> GetSubtribusSingleBySubtribusId(int subtribusId)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var single = Context.Tbl60Subtribusses.SingleOrDefault(a => a.SubtribusId == subtribusId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public static async Task<Tbl60Subtribus> GetSubtribusSingleBySubtribusName(string subtribusName)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var single = Context.Tbl60Subtribusses.SingleOrDefault(a => a.SubtribusName == subtribusName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl60Subtribus> GetSubtribusSingleFirstDataset()
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var single = Context.Tbl60Subtribusses.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }
    public ObservableCollection<Tbl60Subtribus> GetTbl60SubtribussesCollectionOrderBySubtribusName()
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var collection = new ObservableCollection<Tbl60Subtribus>(Context.Tbl60Subtribusses
                .OrderBy(a => a.SubtribusName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl60Subtribus>> GetTbl60SubtribussesCollectionOrderBySubtribusNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl60Subtribus>(Context.Tbl60Subtribusses
                    .Where(e => e.SubtribusId == id))
                : new ObservableCollection<Tbl60Subtribus>(Context.Tbl60Subtribusses
                    .Where(e => e.SubtribusName.StartsWith(searchName))
                    .OrderBy(a => a.SubtribusName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl60Subtribus> GetTbl60SubtribussesCollectionOrderBySubtribusNameFromSubtribusId(int subtribusId)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var collection = new ObservableCollection<Tbl60Subtribus>(Context.Tbl60Subtribusses
                .Where(e => e.SubtribusId == subtribusId)
                .OrderBy(k => k.SubtribusName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Subtribus

    public async Task<ObservableCollection<Tbl60Subtribus>> CopySubtribus(Tbl60Subtribus selected)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var dataset = Context.Tbl60Subtribusses.FirstOrDefault(a => a.SubtribusId == selected.SubtribusId);
            var collection = new ObservableCollection<Tbl60Subtribus>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl60Subtribus
                {
                    //  SubtribusName = CultRes.StringsRes.DatasetNew,
                    SubtribusName = "New",
                    TribusId = dataset.TribusId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Subtribus
    public async Task DeleteConnectedSubtribusses(Tbl57Tribus selected)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var collection = new ObservableCollection<Tbl60Subtribus>(Context.Tbl60Subtribusses
                .Where(e => e.TribusId == selected.TribusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl60Subtribusses.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    public async Task<bool> DeleteSubtribus(Tbl60Subtribus selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetSubtribusSingleBySubtribusId(selected.SubtribusId);
            if (dataset != null)
            {
                await DeleteSubtribusDataset(dataset);
                returnBool = true;

                if (selected.SubtribusName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.SubtribusName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteSubtribusDataset(Tbl60Subtribus selected)
    {
        Context.Tbl60Subtribusses?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl60Subtribusses(ObservableCollection<Tbl60Subtribus> tbl60Subtribusses)
    {
        foreach (var t in tbl60Subtribusses)
        {
            Context.Tbl60Subtribusses?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Subtribus
    public async Task<bool> SaveSubtribus(Tbl60Subtribus selected)
    {
        var returnBool = false;

        try
        {
            if (selected.SubtribusName != null)
            {
                var datasetByName = await GetSubtribusSingleBySubtribusName(selected.SubtribusName);

                if (datasetByName != null && selected.SubtribusId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = await GetSubtribusSingleBySubtribusId(selected.SubtribusId);

            if (selected.SubtribusName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.SubtribusName))
            {
                return false;
            }

            if (selected.SubtribusId == 0)
            {
                dataset = await SubtribusAdd(selected);
            }
            else
            {
                dataset = await SubtribusUpdate(dataset, selected);
            }

            try
            {
                await SubtribusSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.SubtribusName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.SubtribusName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl60Subtribus> SubtribusUpdate(Tbl60Subtribus home, Tbl60Subtribus selected)
    {
        if (home != null) //update
        {
            home.SubtribusName = selected.SubtribusName;
            home.TribusId = selected.TribusId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl60Subtribus> SubtribusAdd(Tbl60Subtribus selected)
    {
        var home = new Tbl60Subtribus() //add new
        {
            SubtribusName = selected.SubtribusName,
            TribusId = selected.TribusId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task SubtribusSave(Tbl60Subtribus home, Tbl60Subtribus selected)
    {
        if (selected.SubtribusId != 0)   //update
        {
            Context.Tbl60Subtribusses?.Update(home);
        }
        else           //add
        {
            Context.Tbl60Subtribusses?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Subtribus

    #region Infratribus

    #region Get Infratribus
    public ObservableCollection<Tbl63Infratribus> GetTbl63InfratribussesCollectionOrderByInfratribusNameFromSubtribusId(int subtribusId)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var collection = new ObservableCollection<Tbl63Infratribus>(Context.Tbl63Infratribusses
                .Where(e => e.SubtribusId == subtribusId)
                .OrderBy(k => k.InfratribusName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl63Infratribus>> GetLastDatasetInTbl63Infratribusses()
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var collection = Context.Tbl63Infratribusses
                .OrderBy(c => c.InfratribusId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl63Infratribus> { collection };
            }
        }
        return null!;
    }

    public Tbl63Infratribus GetInfratribusSingleByInfratribusId(int infratribusId)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var single = Context.Tbl63Infratribusses.SingleOrDefault(a => a.InfratribusId == infratribusId);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    //public Tbl63Infratribus GetInfratribusPdfSingleByInfratribusId(int id)
    //{
    //   // Tbl63Infratribus single = _uow.Tbl63Infratribusses.GetById(id);
    //   if (Context.Tbl63Infratribusses != null)
    //   {
    //       Tbl63Infratribus single = Context.Tbl63Infratribusses.SingleOrDefault(a => a.InfratribusId == id);
    //       return single;
    //   }
    //   return null!;
    //}

    public static async Task<Tbl63Infratribus> GetInfratribusSingleByInfratribusName(string infratribusName)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var single = Context.Tbl63Infratribusses.SingleOrDefault(a => a.InfratribusName == infratribusName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl63Infratribus> GetInfratribusSingleFirstDataset()
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var single = Context.Tbl63Infratribusses.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }
    public ObservableCollection<Tbl63Infratribus> GetTbl63InfratribussesCollectionOrderByInfratribusName()
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var collection = new ObservableCollection<Tbl63Infratribus>(Context.Tbl63Infratribusses
                .OrderBy(a => a.InfratribusName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl63Infratribus>> GetTbl63InfratribussesCollectionOrderByInfratribusNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl63Infratribus>(Context.Tbl63Infratribusses
                    .Where(e => e.InfratribusId == id))
                : new ObservableCollection<Tbl63Infratribus>(Context.Tbl63Infratribusses
                    .Where(e => e.InfratribusName.StartsWith(searchName))
                    .OrderBy(a => a.InfratribusName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl63Infratribus> GetTbl63InfratribussesCollectionOrderByInfratribusNameFromInfratribusId(int infratribusId)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var collection = new ObservableCollection<Tbl63Infratribus>(Context.Tbl63Infratribusses
                .Where(e => e.InfratribusId == infratribusId)
                .OrderBy(k => k.InfratribusName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Infratribus

    public async Task<ObservableCollection<Tbl63Infratribus>> CopyInfratribus(Tbl63Infratribus selected)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var dataset = Context.Tbl63Infratribusses.FirstOrDefault(a => a.InfratribusId == selected.InfratribusId);
            var collection = new ObservableCollection<Tbl63Infratribus>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl63Infratribus
                {
                    //  InfratribusName = CultRes.StringsRes.DatasetNew,
                    InfratribusName = "New",
                    SubtribusId = dataset.SubtribusId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Infratribus
    public async Task DeleteConnectedInfratribusses(Tbl60Subtribus selected)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var collection = new ObservableCollection<Tbl63Infratribus>(Context.Tbl63Infratribusses
                .Where(e => e.SubtribusId == selected.SubtribusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl63Infratribusses.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }


    public async Task<bool> DeleteInfratribus(Tbl63Infratribus selected)
    {
        var returnBool = false;
        try
        {
            var dataset = GetInfratribusSingleByInfratribusId(selected.InfratribusId);
            if (dataset != null)
            {
                await DeleteInfratribusDataset(dataset);
                returnBool = true;

                if (selected.InfratribusName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.InfratribusName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteInfratribusDataset(Tbl63Infratribus selected)
    {
        Context.Tbl63Infratribusses?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl63Infratribusses(ObservableCollection<Tbl63Infratribus> tbl63Infratribusses)
    {
        foreach (var t in tbl63Infratribusses)
        {
            Context.Tbl63Infratribusses?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Infratribus
    public async Task<bool> SaveInfratribus(Tbl63Infratribus selected)
    {
        var returnBool = false;

        try
        {
            if (selected.InfratribusName != null)
            {
                if (selected.InfratribusName != null)
                {
                    var datasetByName = await GetInfratribusSingleByInfratribusName(selected.InfratribusName);

                    if (datasetByName != null && selected.InfratribusId == 0)
                    {
                        await AllDialogs.DatasetExistInfoMessageDialogAsync();
                        return false;
                    }
                }
            }

            var dataset = GetInfratribusSingleByInfratribusId(selected.InfratribusId);

            if (selected.InfratribusName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.InfratribusName))
            {
                return false;
            }

            if (selected.InfratribusId == 0)
            {
                dataset = await InfratribusAdd(selected);
            }
            else
            {
                dataset = await InfratribusUpdate(dataset, selected);
            }

            try
            {
                await InfratribusSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.InfratribusName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.InfratribusName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl63Infratribus> InfratribusUpdate(Tbl63Infratribus home, Tbl63Infratribus selected)
    {
        if (home != null) //update
        {
            home.InfratribusName = selected.InfratribusName;
            home.SubtribusId = selected.SubtribusId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl63Infratribus> InfratribusAdd(Tbl63Infratribus selected)
    {
        var home = new Tbl63Infratribus() //add new
        {
            InfratribusName = selected.InfratribusName,
            SubtribusId = selected.SubtribusId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task InfratribusSave(Tbl63Infratribus home, Tbl63Infratribus selected)
    {
        if (selected.InfratribusId != 0)   //update
        {
            Context.Tbl63Infratribusses?.Update(home);
        }
        else           //add
        {
            Context.Tbl63Infratribusses?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Infratribus

    #region Genus

    #region Get Genus
    public ObservableCollection<Tbl66Genus> GetTbl66GenussesCollectionOrderByGenusNameFromInfratribusId(int infratribusId)
    {
        if (Context.Tbl66Genusses != null)
        {
            var collection = new ObservableCollection<Tbl66Genus>(Context.Tbl66Genusses
                .Where(e => e.InfratribusId == infratribusId)
                .OrderBy(k => k.GenusName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl66Genus>> GetLastDatasetInTbl66Genusses()
    {
        if (Context.Tbl66Genusses != null)
        {
            var collection = Context.Tbl66Genusses
                .OrderBy(c => c.GenusId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl66Genus> { collection };
            }
        }
        return null!;
    }

    public Tbl66Genus GetGenusSingleByGenusId(int genusId)
    {
        if (Context.Tbl66Genusses != null)
        {
            var single = Context.Tbl66Genusses.SingleOrDefault(a => a.GenusId == genusId);
            //    await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    //public async Task<Tbl66Genus> GetGenusPdfSingleByGenusId(int id)
    //{
    //    // Tbl66Genus single = _uow.Tbl66Genusses.GetById(id);
    //    if (Context.Tbl66Genusses != null)
    //    {
    //        Tbl66Genus single = Context.Tbl66Genusses.SingleOrDefault(a => a.GenusId == id);
    //        return single;
    //    }
    //    return null!;
    //}

    public async Task<Tbl66Genus> GetGenusSingleByGenusName(string genusName)
    {
        if (Context.Tbl66Genusses != null)
        {
            var single = Context.Tbl66Genusses.SingleOrDefault(a => a.GenusName == genusName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl66Genus> GetGenusSingleFirstDataset()
    {
        if (Context.Tbl66Genusses != null)
        {
            var single = Context.Tbl66Genusses.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }
    public ObservableCollection<Tbl66Genus> GetTbl66GenussesCollectionOrderByGenusName()
    {
        if (Context.Tbl66Genusses != null)
        {
            var collection = new ObservableCollection<Tbl66Genus>(Context.Tbl66Genusses
                .OrderBy(a => a.GenusName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl66Genus>> GetTbl66GenussesCollectionOrderByGenusNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl66Genusses != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl66Genus>(Context.Tbl66Genusses
                    .Where(e => e.GenusId == id))
                : new ObservableCollection<Tbl66Genus>(Context.Tbl66Genusses
                    .Where(e => e.GenusName.StartsWith(searchName))
                    .OrderBy(a => a.GenusName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl66Genus> GetTbl66GenussesCollectionOrderByGenusNameFromGenusId(int genusId)
    {
        if (Context.Tbl66Genusses != null)
        {
            var collection = new ObservableCollection<Tbl66Genus>(Context.Tbl66Genusses
                .Where(e => e.GenusId == genusId)
                .OrderBy(k => k.GenusName)
            );
            return collection;
        }
        return null!;
    }

    #endregion

    #region Copy Genus

    public async Task<ObservableCollection<Tbl66Genus>> CopyGenus(Tbl66Genus selected)
    {
        if (Context.Tbl66Genusses != null)
        {
            var dataset = Context.Tbl66Genusses.FirstOrDefault(a => a.GenusId == selected.GenusId);
            var collection = new ObservableCollection<Tbl66Genus>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl66Genus
                {
                    //  GenusName = CultRes.StringsRes.DatasetNew,
                    GenusName = "New",
                    InfratribusId = dataset.InfratribusId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Genus
    public async Task DeleteConnectedGenusses(Tbl63Infratribus selected)
    {
        if (Context.Tbl66Genusses != null)
        {
            var collection = new ObservableCollection<Tbl66Genus>(Context.Tbl66Genusses
                .Where(e => e.InfratribusId == selected.InfratribusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl66Genusses.Remove(t);
                }

                Context.SaveChanges();
            }
        }
        await Task.CompletedTask;
    }

    public async Task<bool> DeleteGenus(Tbl66Genus selected)
    {
        var returnBool = false;
        try
        {
            var dataset = GetGenusSingleByGenusId(selected.GenusId);
            if (dataset != null)
            {
                await DeleteGenusDataset(dataset);
                returnBool = true;

                if (selected.GenusName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.GenusName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteGenusDataset(Tbl66Genus selected)
    {
        Context.Tbl66Genusses?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl66Genusses(ObservableCollection<Tbl66Genus> tbl66Genusses)
    {
        foreach (var t in tbl66Genusses)
        {
            Context.Tbl66Genusses?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Genus
    public async Task<bool> SaveGenus(Tbl66Genus selected)
    {
        var returnBool = false;

        try
        {
            if (selected.GenusName != null)
            {
                var datasetByName = await GetGenusSingleByGenusName(selected.GenusName);

                if (datasetByName != null && selected.GenusId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = GetGenusSingleByGenusId(selected.GenusId);

            if (selected.GenusName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.GenusName))
            {
                return false;
            }

            if (selected.GenusId == 0)
            {
                dataset = await GenusAdd(selected);
            }
            else
            {
                dataset = await GenusUpdate(dataset, selected);
            }

            try
            {
                await GenusSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.GenusName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.GenusName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl66Genus> GenusUpdate(Tbl66Genus home, Tbl66Genus selected)
    {
        if (home != null) //update
        {
            home.GenusName = selected.GenusName;
            home.InfratribusId = selected.InfratribusId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl66Genus> GenusAdd(Tbl66Genus selected)
    {
        var home = new Tbl66Genus() //add new
        {
            GenusName = selected.GenusName,
            InfratribusId = selected.InfratribusId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task GenusSave(Tbl66Genus home, Tbl66Genus selected)
    {
        if (selected.GenusId != 0)   //update
        {
            Context.Tbl66Genusses?.Update(home);
        }
        else           //add
        {
            Context.Tbl66Genusses?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Genus

    #region Speciesgroup

    #region Get Speciesgroup

    public ObservableCollection<Tbl68Speciesgroup> GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroup()
    {
        if (Context.Tbl68Speciesgroups != null)
        {
            var collection = new ObservableCollection<Tbl68Speciesgroup>(Context.Tbl68Speciesgroups
                .OrderBy(a => a.SpeciesgroupName)
                .ThenBy(a => a.Subspeciesgroup));
            return collection;
        }
        return null!;
    }

    public async Task<Tbl68Speciesgroup> GetSpeciesgroupSingleBySpeciesgroupId(int speciesgroupId)
    {
        if (Context.Tbl68Speciesgroups != null)
        {
            var single = Context.Tbl68Speciesgroups.SingleOrDefault(a => a.SpeciesgroupId == speciesgroupId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public static async Task<Tbl68Speciesgroup> GetSpeciesgroupSingleBySpeciesgroupNameAndSubspeciesgroup(string speciesgroupName, string subspeciesgroup)
    {
        if (Context.Tbl68Speciesgroups != null)
        {
            var single = Context.Tbl68Speciesgroups.SingleOrDefault(a => a.SpeciesgroupName == speciesgroupName && a.Subspeciesgroup == subspeciesgroup);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl68Speciesgroup>
        GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroupFromSpeciesgroupId(
            int? speciesgroupId)
    {
        if (Context.Tbl68Speciesgroups != null)
        {
            var collection = new ObservableCollection<Tbl68Speciesgroup>(Context.Tbl68Speciesgroups
                .Where(e => e.SpeciesgroupId == speciesgroupId)
                .OrderBy(k => k.SpeciesgroupName)
                .ThenBy(a => a.Subspeciesgroup));
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl68Speciesgroup>> GetTbl68SpeciesgroupsCollectionOrderBySpeciesgroupNameAndSubspeciesgroupFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl68Speciesgroups != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl68Speciesgroup>(Context.Tbl68Speciesgroups
                    .Where(e => e.SpeciesgroupId == id))
                : new ObservableCollection<Tbl68Speciesgroup>(Context.Tbl68Speciesgroups
                    .Where(e => e.SpeciesgroupName.StartsWith(searchName))
                    .OrderBy(a => a.SpeciesgroupName)
                    .ThenBy(a => a.Subspeciesgroup));
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl68Speciesgroup>> GetLastDatasetInTbl68Speciesgroups()
    {
        if (Context.Tbl68Speciesgroups != null)
        {
            var collection = Context.Tbl68Speciesgroups
                .OrderBy(c => c.SpeciesgroupId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl68Speciesgroup> { collection };
            }
        }
        return null!;
    }

    #endregion

    #region Copy Speciesgroup

    public async Task<ObservableCollection<Tbl68Speciesgroup>> CopySpeciesgroup(Tbl68Speciesgroup selected)
    {
        if (Context.Tbl68Speciesgroups != null)
        {
            var dataset = Context.Tbl68Speciesgroups.FirstOrDefault(a => a.SpeciesgroupId == selected.SpeciesgroupId);
            var collection = new ObservableCollection<Tbl68Speciesgroup>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl68Speciesgroup
                {
                    SpeciesgroupName = "New",
                    //SpeciesgroupName = CultRes.StringsRes.DatasetNew,
                    Subspeciesgroup = dataset.Subspeciesgroup,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Synonym = dataset.Synonym,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    EngName = dataset.EngName,
                    GerName = dataset.GerName,
                    FraName = dataset.FraName,
                    PorName = dataset.PorName,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Speciesgroup

    public async Task<bool> DeleteSpeciesgroup(Tbl68Speciesgroup selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetSpeciesgroupSingleBySpeciesgroupId(selected.SpeciesgroupId);
            if (dataset != null)
            {
                await DeleteSpeciesgroupDataset(dataset);
                returnBool = true;

                if (selected.SpeciesgroupName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.SpeciesgroupName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteSpeciesgroupDataset(Tbl68Speciesgroup selected)
    {
        Context.Tbl68Speciesgroups?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Speciesgroup

    public async Task<bool> SaveSpeciesgroup(Tbl68Speciesgroup selected)
    {
        var returnBool = false;

        try
        {
            var datasetByName = await GetSpeciesgroupSingleBySpeciesgroupNameAndSubspeciesgroup(selected.SpeciesgroupName, selected.Subspeciesgroup);

            if (datasetByName != null && selected.SpeciesgroupId == 0)
            {
                await AllDialogs.DatasetExistInfoMessageDialogAsync();
                return false;
            }

            var dataset = await GetSpeciesgroupSingleBySpeciesgroupId(selected.SpeciesgroupId);

            if (selected.SpeciesgroupName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.SpeciesgroupName))
            {
                return false;
            }

            if (selected.SpeciesgroupId == 0)
            {
                dataset = await SpeciesgroupAdd(selected);
            }
            else
            {
                dataset = await SpeciesgroupUpdate(dataset, selected);
            }

            try
            {
                await SpeciesgroupSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.SpeciesgroupName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.SpeciesgroupName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl68Speciesgroup> SpeciesgroupUpdate(Tbl68Speciesgroup home, Tbl68Speciesgroup selected)
    {
        if (home != null) //update
        {
            home.SpeciesgroupName = selected.SpeciesgroupName;
            home.Subspeciesgroup = selected.Subspeciesgroup;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Synonym = selected.Synonym;
            home.EngName = selected.EngName;
            home.GerName = selected.GerName;
            home.FraName = selected.FraName;
            home.PorName = selected.PorName;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl68Speciesgroup> SpeciesgroupAdd(Tbl68Speciesgroup selected)
    {
        var home = new Tbl68Speciesgroup() //add new
        {
            SpeciesgroupName = selected.SpeciesgroupName,
            Subspeciesgroup = selected.Subspeciesgroup,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Synonym = selected.Synonym,
            EngName = selected.EngName,
            GerName = selected.GerName,
            FraName = selected.FraName,
            PorName = selected.PorName,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task SpeciesgroupSave(Tbl68Speciesgroup home, Tbl68Speciesgroup selected)
    {
        if (selected.SpeciesgroupId != 0)   //update
        {
            Context.Tbl68Speciesgroups?.Update(home);
        }
        else           //add
        {
            Context.Tbl68Speciesgroups?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }


    #endregion

    #endregion

    #region FiSpecies

    #region Get FiSpecies
    public ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromGenusId(int genusId)
    {
        //var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
        //    .Include(d => d.Tbl66Genusses)
        //    .Where(e => e.GenusId == genusId)
        //    .OrderBy(a => a.Tbl66Genusses.GenusName)
        //    .ThenBy(a => a.FiSpeciesName)
        //    .ThenBy(a => a.Subspecies)
        //    .ThenBy(a => a.Divers));
        //return collection;
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .Where(e => e.GenusId == genusId)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.FiSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromSpeciesgroupId(
        int speciesgroupId)
    {
        //var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
        //    .Include(d => d.Tbl66Genusses)
        //    .Where(e => e.GenusId == genusId)
        //    .OrderBy(a => a.Tbl66Genusses.GenusName)
        //    .ThenBy(a => a.FiSpeciesName)
        //    .ThenBy(a => a.Subspecies)
        //    .ThenBy(a => a.Divers));
        //return collection;
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .Where(e => e.SpeciesgroupId == speciesgroupId)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.FiSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFiSpeciesId(
        int fispeciesId)
    {
        //var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
        //    .Include(d => d.Tbl66Genusses)
        //    .Where(e => e.GenusId == genusId)
        //    .OrderBy(a => a.Tbl66Genusses.GenusName)
        //    .ThenBy(a => a.FiSpeciesName)
        //    .ThenBy(a => a.Subspecies)
        //    .ThenBy(a => a.Divers));
        //return collection;
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .Where(e => e.FiSpeciesId == fispeciesId)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.FiSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl69FiSpecies>> GetLastDatasetInTbl69FiSpeciesses()
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = Context.Tbl69FiSpeciesses
                .OrderBy(c => c.FiSpeciesId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl69FiSpecies> { collection };
            }
        }
        return null!;
    }

    public Tbl69FiSpecies GetFiSpeciesSingleModelByFiSpeciesId(int fispeciesId)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var single = Context.Tbl69FiSpeciesses.SingleOrDefault(a => a.FiSpeciesId == fispeciesId);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public Tbl69FiSpecies GetFiSpeciesSingleByFiSpeciesId(int fispeciesId)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var single = Context.Tbl69FiSpeciesses.SingleOrDefault(a => a.FiSpeciesId == fispeciesId);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }


    public async Task<Tbl69FiSpecies> GetFiSpeciesSingleByFiSpeciesNameAndSubspeciesAndDivers(string fiSpeciesName, string subspecies, string divers)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var single = Context.Tbl69FiSpeciesses.SingleOrDefault(a => a.FiSpeciesName == fiSpeciesName &
                                                                        a.Subspecies == subspecies &
                                                                        a.Divers == divers);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl69FiSpecies> GetFiSpeciesSingleFirstDataset()
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var single = Context.Tbl69FiSpeciesses.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    public ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByFiSpeciesName()
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .OrderBy(a => a.FiSpeciesName)
            );
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDivers()
    {
        //var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
        //    .Include(d => d.Tbl66Genusses)
        //    .OrderBy(a => a.Tbl66Genusses.GenusName)
        //    .ThenBy(a => a.FiSpeciesName)
        //    .ThenBy(a => a.Subspecies)
        //    .ThenBy(a => a.Divers));
        //return collection;
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.FiSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl69FiSpecies>> GetTbl69FiSpeciessesCollectionOrderByFiSpeciesNameFromSearchNameOrId(string searchName)
    {
        //var collection = int.TryParse(searchName, out var id)
        //    ? new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
        //        .Where(e => e.FiSpeciesId == id))
        //    : new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
        //        .Include(d => d.Tbl66Genusses)
        //        .Where(e => e.Tbl66Genusses.GenusName.StartsWith(searchName))
        //        .OrderBy(a => a.Tbl66Genusses.GenusName)
        //        .ThenBy(a => a.FiSpeciesName)
        //        .ThenBy(a => a.Subspecies)
        //        .ThenBy(a => a.Divers)
        //    );
        //await Task.CompletedTask;
        //return collection;
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                    .Where(e => e.FiSpeciesId == id))
                : new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                    .Where(e => e.Tbl66Genusses.GenusName.StartsWith(searchName))
                    .OrderBy(a => a.Tbl66Genusses.GenusName)
                    .ThenBy(a => a.FiSpeciesName)
                    .ThenBy(a => a.Subspecies)
                    .ThenBy(a => a.Divers)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;

        //var collection = int.TryParse(searchName, out var id)
        //    ? new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
        //        .Where(e => e.FiSpeciesId == id))
        //    : new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
        //        .Where(e => e.FiSpeciesName.StartsWith(searchName))
        //        .OrderBy(a => a.FiSpeciesName)
        //    );
        //await Task.CompletedTask;
        //return collection;
    }

    public ObservableCollection<Tbl69FiSpecies> GetTbl69FiSpeciessesCollectionOrderByFiSpeciesNameFromFiSpeciesId(int fispeciesId)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .Where(e => e.FiSpeciesId == fispeciesId)
                .OrderBy(k => k.FiSpeciesName)
            );
            return collection;
        }
        return null!;
    }

    public Tbl69FiSpecies GetFiSpeciesSingleByFiSpeciesName(string name)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var single = Context.Tbl69FiSpeciesses.SingleOrDefault(a => a.FiSpeciesName == name);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    #endregion

    #region Copy FiSpecies

    public async Task<ObservableCollection<Tbl69FiSpecies>> CopyFiSpecies(Tbl69FiSpecies selected)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var dataset = Context.Tbl69FiSpeciesses.FirstOrDefault(a => a.FiSpeciesId == selected.FiSpeciesId);
            var collection = new ObservableCollection<Tbl69FiSpecies>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl69FiSpecies
                {
                    //  FiSpeciesName = CultRes.StringsRes.DatasetNew,
                    FiSpeciesName = "New",
                    Subspecies = "",
                    Divers = "",
                    GenusId = dataset.GenusId,
                    SpeciesgroupId = dataset.SpeciesgroupId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    MemoSpecies = dataset.MemoSpecies,
                    TradeName = dataset.TradeName,
                    Importer = dataset.Importer,
                    ImportingYear = dataset.ImportingYear,
                    TypeSpecies = dataset.TypeSpecies,
                    LNumber = dataset.LNumber,
                    LOrigin = dataset.LOrigin,
                    LdaOrigin = dataset.LdaOrigin,
                    LdaNumber = dataset.LdaNumber,
                    BasinLength = dataset.BasinLength,
                    FishLength = dataset.FishLength,
                    Karnivore = dataset.Karnivore,
                    Herbivore = dataset.Herbivore,
                    Limnivore = dataset.Limnivore,
                    Omnivore = dataset.Omnivore,
                    MemoFoods = dataset.MemoFoods,
                    Difficult1 = dataset.Difficult1,
                    Difficult2 = dataset.Difficult2,
                    Difficult3 = dataset.Difficult3,
                    Difficult4 = dataset.Difficult4,
                    RegionTop = dataset.RegionTop,
                    RegionMiddle = dataset.RegionMiddle,
                    RegionBottom = dataset.RegionBottom,
                    MemoRegion = dataset.MemoRegion,
                    MemoTech = dataset.MemoTech,
                    Ph1 = dataset.Ph1,
                    Ph2 = dataset.Ph2,
                    Temp1 = dataset.Temp1,
                    Temp2 = dataset.Temp2,
                    Hardness1 = dataset.Hardness1,
                    Hardness2 = dataset.Hardness2,
                    CarboHardness1 = dataset.CarboHardness1,
                    CarboHardness2 = dataset.CarboHardness2,
                    MemoHusbandry = dataset.MemoHusbandry,
                    MemoBuilt = dataset.MemoBuilt,
                    MemoColor = dataset.MemoColor,
                    MemoSozial = dataset.MemoSozial,
                    MemoDomorphism = dataset.MemoDomorphism,
                    MemoSpecial = dataset.MemoSpecial,
                    MemoBreeding = dataset.MemoBreeding
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete FiSpecies
    public async Task DeleteConnectedFiSpeciesses(Tbl66Genus selected)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .Where(e => e.GenusId == selected.GenusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl69FiSpeciesses.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    public async Task<bool> DeleteFiSpecies(Tbl69FiSpecies selected)
    {
        var returnBool = false;
        try
        {
            var dataset = GetFiSpeciesSingleModelByFiSpeciesId(selected.FiSpeciesId);
            if (dataset != null)
            {
                await DeleteFiSpeciesDataset(dataset);
                returnBool = true;

                await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.FiSpeciesName + " " + selected.Subspecies + " " + selected.Divers);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteFiSpeciesDataset(Tbl69FiSpecies selected)
    {
        Context.Tbl69FiSpeciesses?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl69FiSpeciesses(ObservableCollection<Tbl69FiSpecies> tbl69FiSpeciesses)
    {
        foreach (var t in tbl69FiSpeciesses)
        {
            Context.Tbl69FiSpeciesses?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save FiSpecies
    public async Task<bool> SaveFiSpecies(Tbl69FiSpecies selected)
    {
        var returnBool = false;

        try
        {
            if (selected.FiSpeciesName != null)
            {
                if (selected.Subspecies != null)
                {
                    if (selected.Divers != null)
                    {
                        var datasetByName = await GetFiSpeciesSingleByFiSpeciesNameAndSubspeciesAndDivers(
                            selected.FiSpeciesName, selected.Subspecies, selected.Divers);

                        if (datasetByName != null && selected.FiSpeciesId == 0)
                        {
                            await AllDialogs.DatasetExistInfoMessageDialogAsync();
                            return false;
                        }
                    }
                }
            }

            var dataset = GetFiSpeciesSingleByFiSpeciesId(selected.FiSpeciesId);

            if (selected.FiSpeciesName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.FiSpeciesName))
            {
                return false;
            }

            if (selected.FiSpeciesId == 0)
            {
                dataset = await FiSpeciesAdd(selected);
            }
            else
            {
                dataset = await FiSpeciesUpdate(dataset, selected);
            }

            try
            {
                await FiSpeciesSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.FiSpeciesName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.FiSpeciesName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl69FiSpecies> FiSpeciesUpdate(Tbl69FiSpecies home, Tbl69FiSpecies selected)
    {
        if (home != null) //update
        {
            home.FiSpeciesName = selected.FiSpeciesName;
            home.Subspecies = selected.Subspecies;
            home.Divers = selected.Divers;
            home.GenusId = selected.GenusId;
            home.SpeciesgroupId = selected.SpeciesgroupId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.MemoSpecies = selected.MemoSpecies;
            home.TradeName = selected.TradeName;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Importer = selected.Importer;
            home.ImportingYear = selected.ImportingYear;
            home.TypeSpecies = selected.TypeSpecies;
            home.LNumber = selected.LNumber;
            home.LOrigin = selected.LOrigin;
            home.LdaOrigin = selected.LdaOrigin;
            home.LdaNumber = selected.LdaNumber;
            home.BasinLength = selected.BasinLength;
            home.FishLength = selected.FishLength;
            home.Karnivore = selected.Karnivore;
            home.Herbivore = selected.Herbivore;
            home.Limnivore = selected.Limnivore;
            home.Omnivore = selected.Omnivore;
            home.MemoFoods = selected.MemoFoods;
            home.Difficult1 = selected.Difficult1;
            home.Difficult2 = selected.Difficult2;
            home.Difficult3 = selected.Difficult3;
            home.Difficult4 = selected.Difficult4;
            home.RegionTop = selected.RegionTop;
            home.RegionMiddle = selected.RegionMiddle;
            home.RegionBottom = selected.RegionBottom;
            home.MemoRegion = selected.MemoRegion;
            home.MemoTech = selected.MemoTech;
            home.Ph1 = selected.Ph1;
            home.Ph2 = selected.Ph2;
            home.Temp1 = selected.Temp1;
            home.Temp2 = selected.Temp2;
            home.Hardness1 = selected.Hardness1;
            home.Hardness2 = selected.Hardness2;
            home.CarboHardness1 = selected.CarboHardness1;
            home.CarboHardness2 = selected.CarboHardness2;
            home.MemoHusbandry = selected.MemoHusbandry;
            home.MemoBuilt = selected.MemoBuilt;
            home.MemoColor = selected.MemoColor;
            home.MemoSozial = selected.MemoSozial;
            home.MemoDomorphism = selected.MemoDomorphism;
            home.MemoSpecial = selected.MemoSpecial;
            home.MemoBreeding = selected.MemoBreeding;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl69FiSpecies> FiSpeciesAdd(Tbl69FiSpecies selected)
    {
        var home = new Tbl69FiSpecies() //add new
        {
            FiSpeciesName = selected.FiSpeciesName,
            Subspecies = selected.Subspecies,
            Divers = selected.Divers,
            GenusId = selected.GenusId,
            SpeciesgroupId = selected.SpeciesgroupId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            MemoSpecies = selected.MemoSpecies,
            TradeName = selected.TradeName,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Importer = selected.Importer,
            ImportingYear = selected.ImportingYear,
            TypeSpecies = selected.TypeSpecies,
            LNumber = selected.LNumber,
            LOrigin = selected.LOrigin,
            LdaOrigin = selected.LdaOrigin,
            LdaNumber = selected.LdaNumber,
            BasinLength = selected.BasinLength,
            FishLength = selected.FishLength,
            Karnivore = selected.Karnivore,
            Herbivore = selected.Herbivore,
            Limnivore = selected.Limnivore,
            Omnivore = selected.Omnivore,
            MemoFoods = selected.MemoFoods,
            Difficult1 = selected.Difficult1,
            Difficult2 = selected.Difficult2,
            Difficult3 = selected.Difficult3,
            Difficult4 = selected.Difficult4,
            RegionTop = selected.RegionTop,
            RegionMiddle = selected.RegionMiddle,
            RegionBottom = selected.RegionBottom,
            MemoRegion = selected.MemoRegion,
            MemoTech = selected.MemoTech,
            Ph1 = selected.Ph1,
            Ph2 = selected.Ph2,
            Temp1 = selected.Temp1,
            Temp2 = selected.Temp2,
            Hardness1 = selected.Hardness1,
            Hardness2 = selected.Hardness2,
            CarboHardness1 = selected.CarboHardness1,
            CarboHardness2 = selected.CarboHardness2,
            MemoHusbandry = selected.MemoHusbandry,
            MemoBuilt = selected.MemoBuilt,
            MemoColor = selected.MemoColor,
            MemoSozial = selected.MemoSozial,
            MemoDomorphism = selected.MemoDomorphism,
            MemoSpecial = selected.MemoSpecial,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
            MemoBreeding = selected.MemoBreeding,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task FiSpeciesSave(Tbl69FiSpecies home, Tbl69FiSpecies selected)
    {
        if (selected.FiSpeciesId != 0)   //update
        {
            Context.Tbl69FiSpeciesses?.Update(home);
        }
        else           //add
        {
            Context.Tbl69FiSpeciesses?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion FiSpecies

    #region PlSpecies

    #region Get PlSpecies

    public ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromGenusId(int genusId)
    {
        //var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
        //    .Include(d => d.Tbl66Genusses)
        //    .Where(e => e.GenusId == genusId)
        //    .OrderBy(a => a.Tbl66Genusses.GenusName)
        //    .ThenBy(a => a.PlSpeciesName)
        //    .ThenBy(a => a.Subspecies)
        //    .ThenBy(a => a.Divers));
        //return collection;
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Where(e => e.GenusId == genusId)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromSpeciesgroupId(
        int speciesgroupId)
    {
        //var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
        //    .Include(d => d.Tbl66Genusses)
        //    .Where(e => e.GenusId == genusId)
        //    .OrderBy(a => a.Tbl66Genusses.GenusName)
        //    .ThenBy(a => a.PlSpeciesName)
        //    .ThenBy(a => a.Subspecies)
        //    .ThenBy(a => a.Divers));
        //return collection;
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Where(e => e.SpeciesgroupId == speciesgroupId)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromPlSpeciesId(
        int plspeciesId)
    {
        //var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
        //    .Include(d => d.Tbl66Genusses)
        //    .Where(e => e.GenusId == genusId)
        //    .OrderBy(a => a.Tbl66Genusses.GenusName)
        //    .ThenBy(a => a.PlSpeciesName)
        //    .ThenBy(a => a.Subspecies)
        //    .ThenBy(a => a.Divers));
        //return collection;
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Where(e => e.PlSpeciesId == plspeciesId)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByPlSpeciesName()
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .OrderBy(a => a.PlSpeciesName)
            );
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDivers()
    {
        //var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
        //    .Include(d => d.Tbl66Genusses)
        //    .OrderBy(a => a.Tbl66Genusses.GenusName)
        //    .ThenBy(a => a.PlSpeciesName)
        //    .ThenBy(a => a.Subspecies)
        //    .ThenBy(a => a.Divers));
        //return collection;
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl72PlSpecies>> GetTbl72PlSpeciessesCollectionOrderByPlSpeciesNameFromSearchNameOrId(string searchName)
    {
        //var collection = int.TryParse(searchName, out var id)
        //    ? new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
        //        .Where(e => e.PlSpeciesId == id))
        //    : new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
        //        .Include(d => d.Tbl66Genusses)
        //        .Where(e => e.Tbl66Genusses.GenusName.StartsWith(searchName))
        //        .OrderBy(a => a.Tbl66Genusses.GenusName)
        //        .ThenBy(a => a.PlSpeciesName)
        //        .ThenBy(a => a.Subspecies)
        //        .ThenBy(a => a.Divers)
        //    );
        //await Task.CompletedTask;
        //return collection;
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                    .Where(e => e.PlSpeciesId == id))
                : new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                    .Where(e => e.Tbl66Genusses.GenusName.StartsWith(searchName))
                    .OrderBy(a => a.Tbl66Genusses.GenusName)
                    .ThenBy(a => a.PlSpeciesName)
                    .ThenBy(a => a.Subspecies)
                    .ThenBy(a => a.Divers)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByPlSpeciesNameFromPlSpeciesId(int plspeciesId)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Where(e => e.PlSpeciesId == plspeciesId)
                .OrderBy(k => k.PlSpeciesName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl72PlSpecies>> GetLastDatasetInTbl72PlSpeciesses()
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = Context.Tbl72PlSpeciesses
                .OrderBy(c => c.PlSpeciesId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl72PlSpecies> { collection };
            }
        }
        return null!;
    }

    public Tbl72PlSpecies GetPlSpeciesSingleModelByPlSpeciesId(int plspeciesId)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var single = Context.Tbl72PlSpeciesses.SingleOrDefault(a => a.PlSpeciesId == plspeciesId);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl72PlSpecies> GetPlSpeciesSingleByPlSpeciesId(int plspeciesId)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var single = Context.Tbl72PlSpeciesses.SingleOrDefault(a => a.PlSpeciesId == plspeciesId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public async Task<Tbl72PlSpecies> GetPlSpeciesSingleByPlSpeciesNameAndSubspeciesAndDivers(string plSpeciesName, string subspecies, string divers)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var single = Context.Tbl72PlSpeciesses.SingleOrDefault(a => a.PlSpeciesName == plSpeciesName &
                                                                        a.Subspecies == subspecies &
                                                                        a.Divers == divers);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public Tbl72PlSpecies GetPlSpeciesSingleByPlSpeciesName(string name)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var single = Context.Tbl72PlSpeciesses.SingleOrDefault(a => a.PlSpeciesName == name);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public async Task<Tbl72PlSpecies> GetPlSpeciesSingleFirstDataset()
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var single = Context.Tbl72PlSpeciesses.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    #endregion

    #region Copy PlSpecies

    public async Task<ObservableCollection<Tbl72PlSpecies>> CopyPlSpecies(Tbl72PlSpecies selected)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var dataset = Context.Tbl72PlSpeciesses.FirstOrDefault(a => a.PlSpeciesId == selected.PlSpeciesId);
            var collection = new ObservableCollection<Tbl72PlSpecies>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl72PlSpecies
                {
                    PlSpeciesName = CultRes.StringsRes.DatasetNew,
                    GenusId = dataset.GenusId,
                    SpeciesgroupId = dataset.SpeciesgroupId,
                    Valid = selected.Valid,
                    ValidYear = selected.ValidYear,
                    MemoSpecies = selected.MemoSpecies,
                    TradeName = selected.TradeName,
                    Author = selected.Author,
                    AuthorYear = selected.AuthorYear,
                    Importer = selected.Importer,
                    ImportingYear = selected.ImportingYear,
                    BasinHeight = selected.BasinHeight,
                    PlantLength = selected.PlantLength,
                    Difficult1 = selected.Difficult1,
                    Difficult2 = selected.Difficult2,
                    Difficult3 = selected.Difficult3,
                    Difficult4 = selected.Difficult4,
                    MemoTech = selected.MemoTech,
                    Ph1 = selected.Ph1,
                    Ph2 = selected.Ph2,
                    Temp1 = selected.Temp1,
                    Temp2 = selected.Temp2,
                    Hardness1 = selected.Hardness1,
                    Hardness2 = selected.Hardness2,
                    CarboHardness1 = selected.CarboHardness1,
                    CarboHardness2 = selected.CarboHardness2,
                    MemoBuilt = selected.MemoBuilt,
                    MemoColor = selected.MemoColor,
                    MemoReproduction = selected.MemoReproduction,
                    MemoCulture = selected.MemoCulture,
                    MemoGlobal = selected.MemoGlobal
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete PlSpecies

    public async Task DeleteDatasetsInTbl72PlSpeciesses(ObservableCollection<Tbl72PlSpecies> tbl72PlSpeciesses)
    {
        foreach (var t in tbl72PlSpeciesses)
        {
            Context.Tbl72PlSpeciesses?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteConnectedPlSpeciesses(Tbl66Genus selected)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Where(e => e.GenusId == selected.GenusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl72PlSpeciesses.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    public async Task<bool> DeletePlSpecies(Tbl72PlSpecies selected)
    {
        var returnBool = false;
        try
        {
            var dataset = GetPlSpeciesSingleModelByPlSpeciesId(selected.PlSpeciesId);
            if (dataset != null)
            {
                await DeletePlSpeciesDataset(dataset);
                returnBool = true;

                await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.PlSpeciesName + " " + selected.Subspecies + " " + selected.Divers);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeletePlSpeciesDataset(Tbl72PlSpecies selected)
    {
        Context.Tbl72PlSpeciesses?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save PlSpecies

    public async Task<bool> SavePlSpecies(Tbl72PlSpecies selected)
    {
        var returnBool = false;

        try
        {
            var datasetByName = await GetPlSpeciesSingleByPlSpeciesNameAndSubspeciesAndDivers(
                selected.PlSpeciesName, selected.Subspecies, selected.Divers);

            if (datasetByName != null && selected.PlSpeciesId == 0)
            {
                await AllDialogs.DatasetExistInfoMessageDialogAsync();
                return false;
            }


            var dataset = await GetPlSpeciesSingleByPlSpeciesId(selected.PlSpeciesId);

            if (selected.PlSpeciesName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.PlSpeciesName))
            {
                return false;
            }

            if (selected.PlSpeciesId == 0)
            {
                dataset = await PlSpeciesAdd(selected);
            }
            else
            {
                dataset = await PlSpeciesUpdate(dataset, selected);
            }

            try
            {
                await PlSpeciesSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.PlSpeciesName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.PlSpeciesName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl72PlSpecies> PlSpeciesUpdate(Tbl72PlSpecies home, Tbl72PlSpecies selected)
    {
        if (home != null) //update
        {
            home.PlSpeciesName = selected.PlSpeciesName;
            home.Subspecies = selected.Subspecies;
            home.Divers = selected.Divers;
            home.GenusId = selected.GenusId;
            home.SpeciesgroupId = home.SpeciesgroupId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.MemoSpecies = selected.MemoSpecies;
            home.TradeName = selected.TradeName;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Importer = selected.Importer;
            home.ImportingYear = selected.ImportingYear;
            home.BasinHeight = selected.BasinHeight;
            home.PlantLength = selected.PlantLength;
            home.Difficult1 = selected.Difficult1;
            home.Difficult2 = selected.Difficult2;
            home.Difficult3 = selected.Difficult3;
            home.Difficult4 = selected.Difficult4;
            home.MemoTech = selected.MemoTech;
            home.Ph1 = selected.Ph1;
            home.Ph2 = selected.Ph2;
            home.Temp1 = selected.Temp1;
            home.Temp2 = selected.Temp2;
            home.Hardness1 = selected.Hardness1;
            home.Hardness2 = selected.Hardness2;
            home.CarboHardness1 = selected.CarboHardness1;
            home.CarboHardness2 = selected.CarboHardness2;
            home.MemoBuilt = selected.MemoBuilt;
            home.MemoColor = selected.MemoColor;
            home.MemoReproduction = selected.MemoReproduction;
            home.MemoCulture = selected.MemoCulture;
            home.MemoGlobal = selected.MemoGlobal;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl72PlSpecies> PlSpeciesAdd(Tbl72PlSpecies selected)
    {
        var home = new Tbl72PlSpecies() //add new
        {
            PlSpeciesName = selected.PlSpeciesName,
            Subspecies = selected.Subspecies,
            Divers = selected.Divers,
            GenusId = selected.GenusId,
            SpeciesgroupId = selected.SpeciesgroupId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            MemoSpecies = selected.MemoSpecies,
            TradeName = selected.TradeName,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Importer = selected.Importer,
            ImportingYear = selected.ImportingYear,
            BasinHeight = selected.BasinHeight,
            PlantLength = selected.PlantLength,
            Difficult1 = selected.Difficult1,
            Difficult2 = selected.Difficult2,
            Difficult3 = selected.Difficult3,
            Difficult4 = selected.Difficult4,
            MemoTech = selected.MemoTech,
            Ph1 = selected.Ph1,
            Ph2 = selected.Ph2,
            Temp1 = selected.Temp1,
            Temp2 = selected.Temp2,
            Hardness1 = selected.Hardness1,
            Hardness2 = selected.Hardness2,
            CarboHardness1 = selected.CarboHardness1,
            CarboHardness2 = selected.CarboHardness2,
            MemoBuilt = selected.MemoBuilt,
            MemoColor = selected.MemoColor,
            MemoReproduction = selected.MemoReproduction,
            MemoCulture = selected.MemoCulture,
            MemoGlobal = selected.MemoGlobal,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task PlSpeciesSave(Tbl72PlSpecies home, Tbl72PlSpecies selected)
    {
        if (selected.PlSpeciesId != 0)   //update
        {
            Context.Tbl72PlSpeciesses?.Update(home);
        }
        else           //add
        {
            Context.Tbl72PlSpeciesses?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }


    #endregion

    #endregion


    #region Name

    #region Get Name
    public async Task<ObservableCollection<Tbl78Name>> GetTbl78NamesCollectionOrderByNameNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl78Names != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl78Name>(Context.Tbl78Names
                    .Where(e => e.NameId == id))
                : new ObservableCollection<Tbl78Name>(Context.Tbl78Names
                    .Where(e => e.NameName.StartsWith(searchName))
                    .OrderBy(a => a.NameName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl78Name> GetTbl78NamesCollectionOrderByNameNameFromFiSpeciesId(int fispeciesId)
    {
        if (Context.Tbl78Names != null)
        {
            var collection = new ObservableCollection<Tbl78Name>(Context.Tbl78Names
                .Where(e => e.FiSpeciesId == fispeciesId)
                .OrderBy(k => k.NameName)
            );
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl78Name> GetTbl78NamesCollectionOrderByNameNameFromPlSpeciesId(int plspeciesId)
    {
        if (Context.Tbl78Names != null)
        {
            var collection = new ObservableCollection<Tbl78Name>(Context.Tbl78Names
                .Where(e => e.PlSpeciesId == plspeciesId)
                .OrderBy(k => k.NameName)
            );
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl78Name> GetTbl78NamesCollectionOrderByNameNameFromNameId(int nameId)
    {
        if (Context.Tbl78Names != null)
        {
            var collection = new ObservableCollection<Tbl78Name>(Context.Tbl78Names
                .Where(e => e.NameId == nameId)
                .OrderBy(k => k.NameName)
            );
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl78Name> GetTbl78NamesCollectionOrderByNameName()
    {
        if (Context.Tbl78Names != null)
        {
            var collection = new ObservableCollection<Tbl78Name>(Context.Tbl78Names
                .OrderBy(a => a.NameName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl78Name>> GetLastDatasetInTbl78Names()
    {
        if (Context.Tbl78Names != null)
        {
            var collection = Context.Tbl78Names
                .OrderBy(c => c.NameId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl78Name> { collection };
            }
        }
        return null!;
    }

    public async Task<Tbl78Name> GetNameSingleByNameId(int nameId)
    {
        if (Context.Tbl78Names != null)
        {
            var single = Context.Tbl78Names.SingleOrDefault(a => a.NameId == nameId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl78Name> GetNameSingleByNameNameAndLanguage(string name, string language)
    {
        if (Context.Tbl78Names != null)
        {
            var single = Context.Tbl78Names.SingleOrDefault(a => a.NameName == name && a.Language == language);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl78Name> GetNameSingleFirstDataset()
    {
        if (Context.Tbl78Names != null)
        {
            var single = Context.Tbl78Names.First();
            await Task.CompletedTask;
            return single;
        }
        return null!;
    }

    #endregion

    #region Copy Name

    public async Task<ObservableCollection<Tbl78Name>> CopyName(Tbl78Name selected)
    {
        if (Context.Tbl78Names != null)
        {
            var dataset = Context.Tbl78Names.FirstOrDefault(a => a.NameId == selected.NameId);
            var collection = new ObservableCollection<Tbl78Name>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl78Name
                {
                    //  NameName = CultRes.StringsRes.DatasetNew,
                    NameName = "New",
                    FiSpeciesId = dataset.FiSpeciesId,
                    PlSpeciesId = dataset.PlSpeciesId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Language = dataset.Language,
                    Info = dataset.Info,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Name
    public async Task DeleteConnectedNames(Tbl69FiSpecies selected)
    {
        if (Context.Tbl78Names != null)
        {
            var collection = new ObservableCollection<Tbl78Name>(Context.Tbl78Names
                .Where(e => e.FiSpeciesId == selected.FiSpeciesId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl78Names.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedNames(Tbl72PlSpecies selected)
    {
        if (Context.Tbl78Names != null)
        {
            var collection = new ObservableCollection<Tbl78Name>(Context.Tbl78Names
                .Where(e => e.PlSpeciesId == selected.PlSpeciesId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl78Names.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task<bool> DeleteName(Tbl78Name selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetNameSingleByNameId(selected.NameId);
            if (dataset != null)
            {
                await DeleteNameDataset(dataset);
                returnBool = true;

                if (selected.NameName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.NameName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteNameDataset(Tbl78Name selected)
    {
        Context.Tbl78Names?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Name
    public async Task<bool> SaveName(Tbl78Name selected)
    {
        var returnBool = false;
        try
        {
            var datasetByName = await GetNameSingleByNameNameAndLanguage(selected.NameName, selected.Language);

            if (datasetByName != null && selected.NameId == 0)
            {
                await AllDialogs.DatasetExistInfoMessageDialogAsync();
                return false;
            }

            var dataset = await GetNameSingleByNameId(selected.NameId);

            if (selected.NameName != null && selected.NameName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.NameName))
            {
                return false;
            }

            if (selected.NameId == 0)
            {
                dataset = await NameAdd(selected);
            }
            else
            {
                dataset = await NameUpdate(dataset, selected);
            }

            try
            {
                await NameSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.NameName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.NameName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl78Name> NameUpdate(Tbl78Name home, Tbl78Name selected)
    {
        if (home != null) //update
        {
            home.NameName = selected.NameName;
            home.FiSpeciesId = selected.FiSpeciesId;
            home.PlSpeciesId = selected.PlSpeciesId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Language = selected.Language;
            home.Info = selected.Info;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task<Tbl78Name> NameAdd(Tbl78Name selected)
    {
        var home = new Tbl78Name() //add new
        {
            NameName = selected.NameName,
            FiSpeciesId = selected.FiSpeciesId,
            PlSpeciesId = selected.PlSpeciesId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Language = selected.Language,
            Info = selected.Info,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now
        };
        await Task.CompletedTask;
        return home;
    }
    public async Task NameSave(Tbl78Name home, Tbl78Name selected)
    {
        if (selected.NameId != 0)   //update
        {
            Context.Tbl78Names?.Update(home);
        }
        else           //add
        {
            Context.Tbl78Names?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion Name

    #region Image

    #region Get Image
    public async Task<ObservableCollection<Tbl81Image>> GetTbl81ImagesCollectionOrderByInfoFromSearchImageId(string searchInfo)
    {
        if (Context.Tbl81Images != null)
        {
            var collection = new ObservableCollection<Tbl81Image>(Context.Tbl81Images
                .Where(e => e.ImageId == Convert.ToInt32(searchInfo)));
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    //special no use of Info
    public async Task<ObservableCollection<Tbl81Image>> GetTbl81ImagesCollectionOrderByInfoFromSearchInfoOrImageId(string searchInfoOrId)
    {
        if (Context.Tbl81Images != null)
        {
            var collection = int.TryParse(searchInfoOrId, out var id)
                ? new ObservableCollection<Tbl81Image>(Context.Tbl81Images
                    .Where(e => e.ImageId == id))
                : new ObservableCollection<Tbl81Image>(Context.Tbl81Images
                    .Where(e => e.Info.StartsWith(searchInfoOrId))
                    .OrderBy(a => a.Info)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;

    }
    public ObservableCollection<Tbl81Image> GetTbl81ImagesCollectionOrderByInfoFromImageId(int imageId)
    {
        if (Context.Tbl81Images != null)
        {
            var collection = new ObservableCollection<Tbl81Image>(Context.Tbl81Images
                .Where(e => e.ImageId == imageId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl81Image> GetTbl81ImagesCollectionOrderByInfoFromFiSpeciesId(int fispeciesId)
    {
        if (Context.Tbl81Images != null)
        {
            var collection = new ObservableCollection<Tbl81Image>(Context.Tbl81Images
                .Where(e => e.FiSpeciesId == fispeciesId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl81Image> GetTbl81ImagesCollectionOrderByInfoFromPlSpeciesId(int plspeciesId)
    {
        if (Context.Tbl81Images != null)
        {
            var collection = new ObservableCollection<Tbl81Image>(Context.Tbl81Images
                .Where(e => e.PlSpeciesId == plspeciesId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }

    public async Task<Tbl81Image> GetImageSingleByImageId(int imageId)
    {
        if (Context.Tbl81Images != null)
        {
            var single = Context.Tbl81Images.SingleOrDefault(a => a.ImageId == imageId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl81Image>> GetLastDatasetInTbl81Images()
    {
        if (Context.Tbl81Images != null)
        {
            var collection = Context.Tbl81Images
                .OrderBy(c => c.ImageId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl81Image> { collection };
            }
        }
        return null!;
    }

    #endregion

    #region Copy Image
    public async Task<ObservableCollection<Tbl81Image>> CopyImage(Tbl81Image selected)
    {
        if (Context.Tbl81Images != null)
        {
            var dataset = Context.Tbl81Images.FirstOrDefault(a => a.ImageId == selected.ImageId);
            var collection = new ObservableCollection<Tbl81Image>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl81Image
                {
                    FiSpeciesId = dataset.FiSpeciesId,
                    PlSpeciesId = dataset.PlSpeciesId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    ShotDate = dataset.ShotDate,
                    Info = dataset.Info,
                    ImageData = dataset.ImageData,
                    ImageMimeType = dataset.ImageMimeType,
                    FilestreamId = Guid.NewGuid(),
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Image

    public async Task DeleteConnectedImages(Tbl69FiSpecies selected)
    {
        if (Context.Tbl81Images != null)
        {
            var collection = new ObservableCollection<Tbl81Image>(Context.Tbl81Images
                .Where(e => e.FiSpeciesId == selected.FiSpeciesId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl81Images.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    public async Task DeleteConnectedImages(Tbl72PlSpecies selected)
    {
        if (Context.Tbl81Images != null)
        {
            var collection = new ObservableCollection<Tbl81Image>(Context.Tbl81Images
                .Where(e => e.PlSpeciesId == selected.PlSpeciesId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl81Images.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    public async Task<bool> DeleteImage(Tbl81Image selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetImageSingleByImageId(selected.ImageId);
            if (dataset != null)
            {
                await DeleteImageDataset(dataset);
                returnBool = true;

                await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.ImageId.ToString());
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteImageDataset(Tbl81Image selected)
    {
        Context.Tbl81Images?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #region Save Image
    public async Task<bool> SaveImage(Tbl81Image selected, string selectedPath, byte[] selectedFilestream)
    {
        var returnBool = false;

        try
        {
            var dataset = await GetImageSingleByImageId(selected.ImageId);

            if (selected.Info != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.Info))
            {
                return false;
            }

            if (selected.ImageId == 0)
            {
                dataset = await ImageAdd(selected, selectedPath);
            }
            else
            {
                dataset = await ImageUpdate(dataset, selected, selectedPath, selectedFilestream);
            }

            try
            {
                await ImageSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }
            await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.ImageId.ToString());
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl81Image> ImageUpdate(Tbl81Image home, Tbl81Image selected, string selectedPath, byte[] selectedFilestream)
    {
        if (home != null) //update
        {
            home.FiSpeciesId = selected.FiSpeciesId;
            home.PlSpeciesId = selected.PlSpeciesId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.ShotDate = Convert.ToDateTime(selected.ShotDate);
            home.Info = selected.Info;
            home.Memo = selected.Memo;
            //    home.ImageData = selected.ImageData;
            home.ImageMimeType = selected.ImageMimeType;
            if (string.IsNullOrEmpty(selectedPath))
            {
                home.Filestream = selectedFilestream;
                home.FilestreamId = selected.FilestreamId;
            }
            else
            {
                home.FilestreamId = Guid.NewGuid();
                home.Filestream = LoadImageData(selectedPath);
            }
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task<Tbl81Image> ImageAdd(Tbl81Image selected, string selectedPath)
    {
        var home = new Tbl81Image() //add new
        {
            FiSpeciesId = selected.FiSpeciesId,
            PlSpeciesId = selected.PlSpeciesId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            ShotDate = Convert.ToDateTime(selected.ShotDate),
            Info = selected.Info,
            Memo = selected.Memo,
            ImageData = selected.ImageData, //empty
            ImageMimeType = selected.ImageMimeType,
            Filestream = LoadImageData(selectedPath),
            FilestreamId = Guid.NewGuid(),
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now
        };
        await Task.CompletedTask;
        return home;
    }
    private static byte[] LoadImageData(string filePath)
    {
        var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var br = new BinaryReader(fs);
        var imageBytes = br.ReadBytes((int)fs.Length);
        br.Close();
        fs.Close();
        return imageBytes;
    }
    public async Task ImageSave(Tbl81Image home, Tbl81Image selected)
    {
        if (selected.ImageId != 0) //update
        {
            Context.Tbl81Images?.Update(home);
        }
        else                                //add
        {
            Context.Tbl81Images?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion

    #endregion

    #region Synonym

    #region Get Synonym
    public async Task<ObservableCollection<Tbl84Synonym>> GetTbl84SynonymsCollectionOrderBySynonymNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl84Synonyms != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl84Synonym>(Context.Tbl84Synonyms
                    .Where(e => e.SynonymId == id))
                : new ObservableCollection<Tbl84Synonym>(Context.Tbl84Synonyms
                    .Where(e => e.SynonymName.StartsWith(searchName))
                    .OrderBy(a => a.SynonymName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl84Synonym> GetTbl84SynonymsCollectionOrderBySynonymNameFromFiSpeciesId(int fispeciesId)
    {
        if (Context.Tbl84Synonyms != null)
        {
            var collection = new ObservableCollection<Tbl84Synonym>(Context.Tbl84Synonyms
                .Where(e => e.FiSpeciesId == fispeciesId)
                .OrderBy(k => k.SynonymName)
            );
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl84Synonym> GetTbl84SynonymsCollectionOrderBySynonymNameFromPlSpeciesId(int plspeciesId)
    {
        if (Context.Tbl84Synonyms != null)
        {
            var collection = new ObservableCollection<Tbl84Synonym>(Context.Tbl84Synonyms
                .Where(e => e.PlSpeciesId == plspeciesId)
                .OrderBy(k => k.SynonymName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl84Synonym>> GetLastDatasetInTbl84Synonyms()
    {
        if (Context.Tbl84Synonyms != null)
        {
            var collection = Context.Tbl84Synonyms
                .OrderBy(c => c.SynonymId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl84Synonym> { collection };
            }
        }
        return null!;
    }

    public async Task<Tbl84Synonym> GetSynonymSingleBySynonymId(int synonymId)
    {
        if (Context.Tbl84Synonyms != null)
        {
            var single = Context.Tbl84Synonyms.SingleOrDefault(a => a.SynonymId == synonymId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl84Synonym> GetNameSingleBySynonymNameAndAuthorAndAuthorYear(string synonymName, string author, string authorYear)
    {
        if (Context.Tbl84Synonyms != null)
        {
            var single = Context.Tbl84Synonyms.SingleOrDefault(a => a.SynonymName == synonymName && a.Author == author && a.AuthorYear == authorYear);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    #endregion Get Synonym

    #region Copy Synonym

    public async Task<ObservableCollection<Tbl84Synonym>> CopySynonym(Tbl84Synonym selected)
    {
        if (Context.Tbl84Synonyms != null)
        {
            var dataset = Context.Tbl84Synonyms.FirstOrDefault(a => a.SynonymId == selected.SynonymId);
            var collection = new ObservableCollection<Tbl84Synonym>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl84Synonym
                {
                    SynonymName = "New",
                    FiSpeciesId = dataset.FiSpeciesId,
                    PlSpeciesId = dataset.PlSpeciesId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion Copy Synonym

    #region Delete Synonym
    public async Task DeleteConnectedSynonyms(Tbl69FiSpecies selected)
    {
        if (Context.Tbl84Synonyms != null)
        {
            var collection = new ObservableCollection<Tbl84Synonym>(Context.Tbl84Synonyms
                .Where(e => e.FiSpeciesId == selected.FiSpeciesId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl84Synonyms.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    public async Task DeleteConnectedSynonyms(Tbl72PlSpecies selected)
    {
        if (Context.Tbl84Synonyms != null)
        {
            var collection = new ObservableCollection<Tbl84Synonym>(Context.Tbl84Synonyms
                .Where(e => e.PlSpeciesId == selected.PlSpeciesId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl84Synonyms.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    public async Task<bool> DeleteSynonym(Tbl84Synonym selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetSynonymSingleBySynonymId(selected.SynonymId);
            if (dataset != null)
            {
                await DeleteSynonymDataset(dataset);
                returnBool = true;

                if (selected.SynonymName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.SynonymName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteSynonymDataset(Tbl84Synonym selected)
    {
        Context.Tbl84Synonyms?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }


    #endregion Delete Synonym

    #region Save Synonym
    public async Task<bool> SaveSynonym(Tbl84Synonym selected)
    {
        var returnBool = false;

        try
        {
            var datasetByName = await GetNameSingleBySynonymNameAndAuthorAndAuthorYear(selected.SynonymName, selected.Author, selected.AuthorYear);

            if (datasetByName != null && selected.SynonymId == 0)
            {
                await AllDialogs.DatasetExistInfoMessageDialogAsync();
                return false;
            }

            var dataset = await GetSynonymSingleBySynonymId(selected.SynonymId);

            if (selected.SynonymName != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.SynonymName))
            {
                return false;
            }

            if (selected.SynonymId == 0)
            {
                dataset = await SynonymAdd(selected);
            }
            else
            {
                dataset = await SynonymUpdate(dataset, selected);
            }

            try
            {
                await SynonymSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.SynonymName != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.SynonymName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl84Synonym> SynonymUpdate(Tbl84Synonym home, Tbl84Synonym selected)
    {
        if (home != null) //update
        {
            home.SynonymName = selected.SynonymName;
            home.FiSpeciesId = selected.FiSpeciesId;
            home.PlSpeciesId = selected.PlSpeciesId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = home.Author;
            home.AuthorYear = home.AuthorYear;
            home.Info = selected.Info;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl84Synonym> SynonymAdd(Tbl84Synonym selected)
    {
        var home = new Tbl84Synonym() //add new
        {
            SynonymName = selected.SynonymName,
            FiSpeciesId = selected.FiSpeciesId,
            PlSpeciesId = selected.PlSpeciesId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task SynonymSave(Tbl84Synonym home, Tbl84Synonym selected)
    {
        if (selected.SynonymId != 0)   //update
        {
            Context.Tbl84Synonyms?.Update(home);
        }
        else           //add
        {
            Context.Tbl84Synonyms?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion Save Synonym

    #endregion Synonym

    #region Geographic

    #region Get Geographic
    public async Task<ObservableCollection<Tbl87Geographic>> GetTbl87GeographicsCollectionOrderByInfoFromSearchGeographicId(string searchInfo)
    {
        if (Context.Tbl87Geographics != null)
        {
            var collection = int.TryParse(searchInfo, out var id)
                ? new ObservableCollection<Tbl87Geographic>(Context.Tbl87Geographics
                    .Where(e => e.GeographicId == id))
                : new ObservableCollection<Tbl87Geographic>(Context.Tbl87Geographics
                    .Where(e => e.Info.StartsWith(searchInfo))
                    .OrderBy(a => a.Info)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl87Geographic>> GetTbl87GeographicsCollectionOrderByInfoFromSearchInfoOrGeographicId(string searchInfoOrId)
    {
        if (Context.Tbl87Geographics != null)
        {
            var collection = int.TryParse(searchInfoOrId, out var id)
                ? new ObservableCollection<Tbl87Geographic>(Context.Tbl87Geographics
                    .Where(e => e.GeographicId == id))
                : new ObservableCollection<Tbl87Geographic>(Context.Tbl87Geographics
                    .Where(e => e.Info.StartsWith(searchInfoOrId))
                    .OrderBy(a => a.Info)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl87Geographic> GetTbl87GeographicsCollectionOrderByInfoFromGeographicId(int geographicId)
    {
        if (Context.Tbl87Geographics != null)
        {
            var collection = new ObservableCollection<Tbl87Geographic>(Context.Tbl87Geographics
                .Where(e => e.GeographicId == geographicId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl87Geographic> GetTbl87GeographicsCollectionOrderByInfoFromFiSpeciesId(int fispeciesId)
    {
        if (Context.Tbl87Geographics != null)
        {
            var collection = new ObservableCollection<Tbl87Geographic>(Context.Tbl87Geographics
                .Where(e => e.FiSpeciesId == fispeciesId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl87Geographic> GetTbl87GeographicsCollectionOrderByInfoFromPlSpeciesId(int plspeciesId)
    {
        if (Context.Tbl87Geographics != null)
        {
            var collection = new ObservableCollection<Tbl87Geographic>(Context.Tbl87Geographics
                .Where(e => e.PlSpeciesId == plspeciesId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }

    public async Task<Tbl87Geographic> GetGeographicSingleByGeographicId(int geographicId)
    {
        if (Context.Tbl87Geographics != null)
        {
            var single = Context.Tbl87Geographics.SingleOrDefault(a => a.GeographicId == geographicId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl87Geographic>> GetLastDatasetInTbl87Geographics()
    {
        if (Context.Tbl87Geographics != null)
        {
            var collection = Context.Tbl87Geographics
                .OrderBy(c => c.GeographicId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl87Geographic> { collection };
            }
        }
        return null!;
    }

    #endregion

    #region Copy Geographic
    public async Task<ObservableCollection<Tbl87Geographic>> CopyGeographic(Tbl87Geographic selected)
    {
        if (Context.Tbl87Geographics != null)
        {
            var dataset = Context.Tbl87Geographics.FirstOrDefault(a => a.GeographicId == selected.GeographicId);
            var collection = new ObservableCollection<Tbl87Geographic>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl87Geographic
                {
                    FiSpeciesId = dataset.FiSpeciesId,
                    PlSpeciesId = dataset.PlSpeciesId,
                    Address = dataset.Address,
                    Continent = dataset.Continent,
                    Country = dataset.Country,
                    Http = dataset.Http,
                    Latitude = dataset.Latitude,
                    Longitude = dataset.Longitude,
                    Latitude1 = dataset.Latitude1,
                    Longitude1 = dataset.Longitude1,
                    Latitude2 = dataset.Latitude2,
                    Longitude2 = dataset.Longitude2,
                    Latitude3 = dataset.Latitude3,
                    Longitude3 = dataset.Longitude3,
                    ZoomLevel = dataset.ZoomLevel,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Author = dataset.Author,
                    AuthorYear = dataset.AuthorYear,
                    Info = dataset.Info,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Delete Geographic

    public async Task DeleteConnectedGeographics(Tbl69FiSpecies selected)
    {
        if (Context.Tbl87Geographics != null)
        {
            var collection = new ObservableCollection<Tbl87Geographic>(Context.Tbl87Geographics
                .Where(e => e.FiSpeciesId == selected.FiSpeciesId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl87Geographics.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    public async Task DeleteConnectedGeographics(Tbl72PlSpecies selected)
    {
        if (Context.Tbl87Geographics != null)
        {
            var collection = new ObservableCollection<Tbl87Geographic>(Context.Tbl87Geographics
                .Where(e => e.PlSpeciesId == selected.PlSpeciesId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl87Geographics.Remove(t);
                }

                Context.SaveChanges();
            }
        }
        await Task.CompletedTask;
    }

    public async Task<bool> DeleteGeographic(Tbl87Geographic selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetGeographicSingleByGeographicId(selected.GeographicId);
            if (dataset != null)
            {
                await DeleteGeographicDataset(dataset);
                returnBool = true;

                await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.GeographicId.ToString());
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteGeographicDataset(Tbl87Geographic selected)
    {
        Context.Tbl87Geographics?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }


    #endregion

    #region Save Geographic
    public async Task<bool> SaveGeographic(Tbl87Geographic selected)
    {
        var returnBool = false;

        try
        {
            var dataset = await GetGeographicSingleByGeographicId(selected.GeographicId);

            if (selected.Info != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.Info))
            {
                return false;
            }

            if (selected.GeographicId == 0)
            {
                dataset = await GeographicAdd(selected);
            }
            else
            {
                dataset = await GeographicUpdate(dataset, selected);
            }

            try
            {
                await GeographicSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }
            await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.GeographicId.ToString());
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl87Geographic> GeographicUpdate(Tbl87Geographic home, Tbl87Geographic selected)
    {
        if (home != null) //update
        {
            home.FiSpeciesId = selected.FiSpeciesId;
            home.PlSpeciesId = selected.PlSpeciesId;
            home.Address = selected.Address;
            home.Continent = selected.Continent;
            home.Country = selected.Country;
            home.Http = selected.Http;
            home.Latitude = selected.Latitude;
            home.Longitude = selected.Longitude;
            home.Latitude1 = selected.Latitude1;
            home.Longitude1 = selected.Longitude1;
            home.Latitude2 = selected.Latitude2;
            home.Longitude2 = selected.Longitude2;
            home.Latitude3 = selected.Latitude3;
            home.Longitude3 = selected.Longitude3;
            home.ZoomLevel = selected.ZoomLevel;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.AuthorYear = selected.AuthorYear;
            home.Info = selected.Info;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl87Geographic> GeographicAdd(Tbl87Geographic selected)
    {
        var home = new Tbl87Geographic() //add new
        {
            FiSpeciesId = selected.FiSpeciesId,
            PlSpeciesId = selected.PlSpeciesId,
            CountId = RandomHelper.Randomnumber(),
            Address = selected.Address,
            Continent = selected.Continent,
            Country = selected.Country,
            Http = selected.Http,
            Latitude = selected.Latitude,
            Longitude = selected.Longitude,
            Latitude1 = selected.Latitude1,
            Longitude1 = selected.Longitude1,
            Latitude2 = selected.Latitude2,
            Longitude2 = selected.Longitude2,
            Latitude3 = selected.Latitude3,
            Longitude3 = selected.Longitude3,
            ZoomLevel = selected.ZoomLevel,
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            AuthorYear = selected.AuthorYear,
            Info = selected.Info,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task GeographicSave(Tbl87Geographic home, Tbl87Geographic selected)
    {
        if (selected.GeographicId != 0) //update
        {
            Context.Tbl87Geographics?.Update(home);
        }
        else                                //add
        {
            Context.Tbl87Geographics?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }


    #endregion

    #endregion

    #region Country

    #region Get Country
    public ObservableCollection<TblCountry> GetTblCountriesCollectionOrderByName()
    {
        if (Context.TblCountries != null)
        {
            var collection = new ObservableCollection<TblCountry>(Context.TblCountries
                .OrderBy(a => a.Name)
            );
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<TblCountry>> GetTblCountriesCollectionOrderByNameFromSearchNameOrId(string searchName)
    {
        if (Context.TblCountries != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<TblCountry>(Context.TblCountries
                    .Where(e => e.CountryId == id))
                : new ObservableCollection<TblCountry>(Context.TblCountries
                    .Where(e => e.Name.StartsWith(searchName))
                    .OrderBy(a => a.Name)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<TblCountry>> GetLastDatasetInTblCountries()
    {
        if (Context.TblCountries != null)
        {
            var collection = Context.TblCountries
                .OrderBy(c => c.CountryId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<TblCountry> { collection };
            }
        }
        return null!;
    }

    public async Task<TblCountry> GetCountrySingleByCountryId(int countryId)
    {
        if (Context.TblCountries != null)
        {
            var single = Context.TblCountries.SingleOrDefault(a => a.CountryId == countryId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<TblCountry> GetCountrySingleByName(string name)
    {
        if (Context.TblCountries != null)
        {
            var single = Context.TblCountries.SingleOrDefault(a => a.Name == name);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    #endregion Country

    #region Copy Country
    public async Task<ObservableCollection<TblCountry>> CopyCountry(TblCountry selected)
    {
        if (Context.TblCountries != null)
        {
            var dataset = Context.TblCountries.FirstOrDefault(a => a.CountryId == selected.CountryId);
            var collection = new ObservableCollection<TblCountry>();

            if (dataset != null)
            {
                collection.Insert(0, new TblCountry
                {
                    Name = dataset.Name,
                    Regex = dataset.Regex
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }


    #endregion Country

    #region Delete Country
    public async Task<bool> DeleteCountry(TblCountry selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetCountrySingleByCountryId(selected.CountryId);
            if (dataset != null)
            {
                await DeleteCountryDataset(dataset);
                returnBool = true;

                if (selected.Name != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.Name);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteCountryDataset(TblCountry selected)
    {
        Context.TblCountries?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion Country

    #region Save Country
    public async Task<bool> SaveCountry(TblCountry selected)
    {
        var returnBool = false;

        try
        {
            if (selected.Name != null)
            {
                var datasetByName = await GetCountrySingleByName(selected.Name);

                if (datasetByName != null && selected.CountryId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = await GetCountrySingleByCountryId(selected.CountryId);

            if (selected.Name != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.Name))
            {
                return false;
            }

            if (selected.CountryId == 0)
            {
                dataset = await CountryAdd(selected);
            }
            else
            {
                dataset = await CountryUpdate(dataset, selected);
            }

            try
            {
                await CountrySave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.Name != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.Name);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<TblCountry> CountryUpdate(TblCountry home, TblCountry selected)
    {
        if (home != null) //update
        {
            home.Name = selected.Name;
            home.Regex = selected.Regex;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<TblCountry> CountryAdd(TblCountry selected)
    {
        var home = new TblCountry() //add new
        {
            Name = selected.Name,
            Regex = selected.Regex

        };
        await Task.CompletedTask;
        return home;
    }

    public async Task CountrySave(TblCountry home, TblCountry selected)
    {
        if (selected.CountryId != 0)   //update
        {
            Context.TblCountries?.Update(home);
        }
        else           //add
        {
            Context.TblCountries?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }


    #endregion Country

    #endregion Country

    #region UserProfile

    #region Get UserProfile

    public async Task<ObservableCollection<TblUserProfile>> GetTblUserProfilesCollectionOrderByNameFromSearchNameOrId(string searchName)
    {
        if (Context.TblUserProfiles != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<TblUserProfile>(Context.TblUserProfiles
                    .Where(e => e.UserProfileId == id))
                : new ObservableCollection<TblUserProfile>(Context.TblUserProfiles
                    .Where(e => e.Email.StartsWith(searchName))
                    .OrderBy(a => a.Email)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<TblUserProfile>> GetTblUserProfilesCollectionOrderByEmailFromSearchEmailOrId(string searchEmail)
    {
        if (Context.TblUserProfiles != null)
        {
            var collection = int.TryParse(searchEmail, out var id)
                ? new ObservableCollection<TblUserProfile>(Context.TblUserProfiles
                    .Where(e => e.UserProfileId == id))
                : new ObservableCollection<TblUserProfile>(Context.TblUserProfiles
                    .Where(e => e.Email.StartsWith(searchEmail))
                    .OrderBy(a => a.Email)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }


    public async Task<ObservableCollection<TblUserProfile>> GetLastDatasetInTblUserProfiles()
    {
        if (Context.TblUserProfiles != null)
        {
            var collection = Context.TblUserProfiles
                .OrderBy(c => c.UserProfileId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<TblUserProfile> { collection };
            }
        }
        return null!;
    }

    public async Task<TblUserProfile> GetUserProfileSingleByUserProfileId(int userprofileId)
    {
        if (Context.TblUserProfiles != null)
        {
            var single = Context.TblUserProfiles.SingleOrDefault(a => a.UserProfileId == userprofileId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<TblUserProfile> GetUserProfileSingleByEMail(string email)
    {
        if (Context.TblUserProfiles != null)
        {
            var single = Context.TblUserProfiles.SingleOrDefault(a => a.Email == email);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    #endregion

    #region Copy UserProfile

    public async Task<ObservableCollection<TblUserProfile>> CopyUserProfile(TblUserProfile selected)
    {
        if (Context.TblUserProfiles != null)
        {
            var dataset = Context.TblUserProfiles.FirstOrDefault(a => a.UserProfileId == selected.UserProfileId);
            var collection = new ObservableCollection<TblUserProfile>();

            if (collection != null)
            {
                if (dataset != null)
                {
                    collection.Insert(0, new TblUserProfile
                    {
                        Email = "New",
                        Role = dataset.Role,
                        Flag = dataset.Flag,
                        Colour = dataset.Colour,
                        Title = dataset.Title,
                        FirstName = dataset.FirstName,
                        LastName = dataset.LastName,
                        BirthDate = dataset.BirthDate,
                        Gender = dataset.Gender,
                        Country = dataset.Country,
                        Postcode = dataset.Postcode,
                        City = dataset.City,
                        Street1 = dataset.Street1,
                        Street2 = dataset.Street2,
                        Tel = dataset.Tel,
                        Mobil = dataset.Mobil,
                        Fax = dataset.Fax,
                        HomePageUrl = dataset.HomePageUrl,
                        Business = dataset.Business,
                        Company = dataset.Company,
                        Filestream = dataset.Filestream,
                        ImageMimeType = dataset.ImageMimeType,
                        FilestreamId = Guid.NewGuid(),
                        Signature = dataset.Signature,
                        MailNewsletter = dataset.MailNewsletter,
                        MaulHtml = dataset.MaulHtml,
                        Known = dataset.Known,
                        StartDate = dataset.StartDate,
                        EndDate = dataset.EndDate,
                        Memo = dataset.Memo
                    });
                }

                await Task.CompletedTask;
                return collection;
            }
        }
        return null!;
    }


    #endregion

    #region Delete UserProfile

    public async Task<bool> DeleteUserProfile(TblUserProfile selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetUserProfileSingleByUserProfileId(selected.UserProfileId);
            if (dataset != null)
            {
                await DeleteUserProfileDataset(dataset);
                returnBool = true;

                if (selected.Email != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.Email);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteUserProfileDataset(TblUserProfile selected)
    {
        Context.TblUserProfiles?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }


    #endregion

    #region Save UserProfile

    public async Task<bool> SaveUserProfile(TblUserProfile selected)
    {
        var returnBool = false;

        try
        {
            if (selected.Email != null)
            {
                var datasetByName = await GetUserProfileSingleByEMail(selected.Email);

                if (datasetByName != null && selected.UserProfileId == 0)
                {
                    await AllDialogs.DatasetExistInfoMessageDialogAsync();
                    return false;
                }
            }

            var dataset = await GetUserProfileSingleByUserProfileId(selected.UserProfileId);

            if (selected.Email != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.Email))
            {
                return false;
            }

            if (selected.UserProfileId == 0)
            {
                dataset = await UserProfileAdd(selected);
            }
            else
            {
                dataset = await UserProfileUpdate(dataset, selected);
            }

            try
            {
                await UserProfileSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.Email != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.Email);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<TblUserProfile> UserProfileUpdate(TblUserProfile home, TblUserProfile selected)
    {
        if (home != null) //update
        {
            home.Email = selected.Email;
            //password extra ohne scrypt bearbeiten userprofile.Password = Crypt.CalculateHash(selected.Password, selected.Email);
            home.Role = selected.Role;
            home.Flag = selected.Flag;
            home.Colour = selected.Colour;
            home.Title = selected.Title;
            home.FirstName = selected.FirstName;
            home.LastName = selected.LastName;
            home.BirthDate = selected.BirthDate;
            home.Gender = selected.Gender;
            home.Country = selected.Country;
            home.Postcode = selected.Postcode;
            home.City = selected.City;
            home.Street1 = selected.Street1;
            home.Street2 = selected.Street2;
            home.Tel = selected.Tel;
            home.Mobil = selected.Mobil;
            home.Fax = selected.Fax;
            home.HomePageUrl = selected.HomePageUrl;
            home.Business = selected.Business;
            home.Company = selected.Company;
            home.Filestream = selected.Filestream;
            home.ImageMimeType = selected.ImageMimeType;
            home.FilestreamId = selected.FilestreamId;
            home.Signature = selected.Signature;
            home.MailNewsletter = selected.MailNewsletter;
            home.MaulHtml = selected.MaulHtml;
            home.Known = selected.Known;
            home.StartDate = DateTime.Now;
            home.EndDate = DateTime.Now;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
            home.Memo = selected.Memo;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task<TblUserProfile> UserProfileAdd(TblUserProfile selected)
    {
        var home = new TblUserProfile() //add new
        {
            Email = selected.Email,
            CountId = RandomHelper.Randomnumber(),
            Password = Crypt.CalculateHash(selected.Password, selected.Email),
            Role = selected.Role,
            Flag = selected.Flag,
            Colour = selected.Colour,
            Title = selected.Title,
            FirstName = selected.FirstName,
            LastName = selected.LastName,
            BirthDate = selected.BirthDate,
            Gender = selected.Gender,
            Country = selected.Country,
            Postcode = selected.Postcode,
            City = selected.City,
            Street1 = selected.Street1,
            Street2 = selected.Street2,
            Tel = selected.Tel,
            Mobil = selected.Mobil,
            Fax = selected.Fax,
            HomePageUrl = selected.HomePageUrl,
            Business = selected.Business,
            Company = selected.Company,
            Filestream = selected.Filestream,
            ImageMimeType = selected.ImageMimeType,
            FilestreamId = Guid.NewGuid(),
            Signature = selected.Signature,
            MailNewsletter = selected.MailNewsletter,
            MaulHtml = selected.MaulHtml,
            Known = selected.Known,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
            Memo = selected.Memo

        };
        await Task.CompletedTask;
        return home;
    }

    public async Task UserProfileSave(TblUserProfile home, TblUserProfile selected)
    {
        if (selected.UserProfileId != 0)   //update
        {
            Context.TblUserProfiles?.Update(home);
        }
        else           //add
        {
            Context.TblUserProfiles?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }


    #endregion

    #endregion


    #region References Expert, Source, Author from Regnum-Userprofile

    #region Reference 

    #region Reference Get
    public ObservableCollection<Tbl90Reference> GetTbl90ReferencesCollectionOrderByInfoFromSearchInfoOrId(string searchInfo)
    {
        ObservableCollection<Tbl90Reference> collection = null!;
        if (Context.Tbl90References != null)
        {
            collection = int.TryParse(searchInfo, out var id)
                ? new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                    .Where(e => e.ReferenceId == id))
                : new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                    .Where(e => e.Info.StartsWith(searchInfo))
                    .OrderBy(a => a.Info)
                );
        }

        return collection;
    }

    //public ObservableCollection<Tbl90Reference> GetTbl90ReferencesCollectionOrderByInfoFromSearchInfoOrId(string searchInfo)
    //{
    //    ObservableCollection<Tbl90Reference> collection;
    //    collection = int.TryParse(searchInfo, out var id)
    //        ? new ObservableCollection<Tbl90Reference>(Context.Tbl90References
    //            .Include(a => a.Tbl90RefExperts)
    //            .Include(a => a.Tbl90RefSources)
    //            .Include(a => a.Tbl90RefAuthors)
    //            .Where(e => e.ReferenceId == id))
    //        : new ObservableCollection<Tbl90Reference>(Context.Tbl90References
    //            .Include(a => a.Tbl90RefExperts)
    //            .Include(a => a.Tbl90RefSources)
    //            .Include(a => a.Tbl90RefAuthors)
    //            .Where(e => e.Info.StartsWith(searchInfo))
    //            .OrderBy(a => a.Info)
    //        );
    //    //    await Task.CompletedTask;
    //    return collection;
    //}

    public ObservableCollection<Tbl90Reference> GetTbl90ReferencesExpertsCollectionOrderByInfoFromSearchInfoOrId(string searchInfo)
    {
        ObservableCollection<Tbl90Reference> collection = null!;
        if (Context.Tbl90References != null)
        {
            collection = int.TryParse(searchInfo, out var id)
                ? new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                    .Include(a => a.Tbl90RefExperts)
                    .Where(e => e.ReferenceId == id))
                : new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                    .Include(a => a.Tbl90RefExperts)
                    .Where(e => e.Info.StartsWith(searchInfo) && e.RefAuthorId == null && e.RefSourceId == null)
                    .OrderBy(a => a.Info)
                );
        }

        return collection;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferencesSourcesCollectionOrderByInfoFromSearchInfoOrId(string searchInfo)
    {
        ObservableCollection<Tbl90Reference> collection = null!;
        if (Context.Tbl90References != null)
        {
            collection = int.TryParse(searchInfo, out var id)
                ? new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                    .Include(a => a.Tbl90RefSources)
                    .Where(e => e.ReferenceId == id))
                : new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                    .Include(a => a.Tbl90RefSources)
                    .Where(e => e.Info.StartsWith(searchInfo) && e.RefAuthorId == null && e.RefExpertId == null)
                    .OrderBy(a => a.Info)
                );
        }

        return collection;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferencesAuthorsCollectionOrderByInfoFromSearchInfoOrId(string searchInfo)
    {
        ObservableCollection<Tbl90Reference> collection = null!;
        if (Context.Tbl90References != null)
        {
            collection = int.TryParse(searchInfo, out var id)
                ? new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                    .Include(a => a.Tbl90RefAuthors)
                    .Where(e => e.ReferenceId == id))
                : new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                    .Include(a => a.Tbl90RefAuthors)
                    .Where(e => e.Info.StartsWith(searchInfo) && e.RefSourceId == null && e.RefExpertId == null)
                    .OrderBy(a => a.Info)
                );
        }

        return collection;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferencesCollectionOrderByInfoFromReferenceId(int referenceId)
    {
        ObservableCollection<Tbl90Reference> collection = null!;
        if (Context.Tbl90References != null)
        {
            collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.ReferenceId == referenceId)
                .OrderBy(k => k.Info)
            );
        }

        return collection;
    }

    public async Task<ObservableCollection<Tbl90Reference>> GetTbl90ReferencesCollectionOrderByFromRegnumId(int regnumId)
    {
        ObservableCollection<Tbl90Reference> collection = null!;
        if (Context.Tbl90References != null)
        {
            collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.RegnumId == regnumId)
                .OrderBy(k => k.Info)
            );
        }

        await Task.CompletedTask;
        return collection;
    }
    public ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceExpertsCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? regnumId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.RegnumId == regnumId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceSourcesCollectionOrderByInfoFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? regnumId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.RegnumId == regnumId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }

        //var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
        //    .Where(e => e.RegnumId == regnumId && e.RefAuthorId.Equals(null) && e.RefExpertId.Equals(null))
        //    .OrderBy(k => k.Info));
        //return collection;
        return null!;
    }
    public ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceAuthorsCollectionOrderByInfoFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? regnumId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.RegnumId.Equals(regnumId) && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //------------------------------------------------------------------------------------
    public ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceExpertsCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? phylumId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.PhylumId == phylumId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceSourcesCollectionOrderByInfoFromPhylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? phylumId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.PhylumId == phylumId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceAuthorsCollectionOrderByInfoFromPhylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? phylumId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.PhylumId == phylumId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromDivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNull(
        int? divisionId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.DivisionId == divisionId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromDivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(
        int? divisionId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.DivisionId == divisionId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromDivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(
        int? divisionId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.DivisionId == divisionId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubphylumIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? subphylumId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubphylumId == subphylumId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? subphylumId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubphylumId == subphylumId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubphylumIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? subphylumId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubphylumId == subphylumId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference>
        GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubdivisionIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? subdivisionId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubdivisionId == subdivisionId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(
        int? subdivisionId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubdivisionId == subdivisionId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubdivisionIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(
        int? subdivisionId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubdivisionId == subdivisionId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSuperclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(
        int? superclassId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SuperclassId == superclassId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(
        int? superclassId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SuperclassId == superclassId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSuperclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(
        int? superclassId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SuperclassId == superclassId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromClassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? classId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.ClassId == classId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? classId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.ClassId == classId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromClassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? classId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.ClassId == classId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(
        int? subclassId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubclassId == subclassId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(
        int? subclassId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubclassId == subclassId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(
        int? subclassId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubclassId == subclassId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfraclassIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? infraclassId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfraclassId == infraclassId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? infraclassId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfraclassId == infraclassId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfraclassIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? infraclassId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfraclassId == infraclassId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }


    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromLegioIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? legioId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.LegioId == legioId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? legioId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.LegioId == legioId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromLegioIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? legioId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.LegioId == legioId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromOrdoIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? ordoId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.OrdoId == ordoId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromOrdoIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? ordoId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.OrdoId == ordoId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromOrdoIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? ordoId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.OrdoId == ordoId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubordoIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? subordoId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubordoId == subordoId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubordoIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? subordoId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubordoId == subordoId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubordoIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? subordoId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubordoId == subordoId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfraordoIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? infraordoId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfraordoId == infraordoId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfraordoIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? infraordoId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfraordoId == infraordoId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfraordoIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? infraordoId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfraordoId == infraordoId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSuperfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? superfamilyId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SuperfamilyId == superfamilyId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSuperfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? superfamilyId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SuperfamilyId == superfamilyId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSuperfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? superfamilyId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SuperfamilyId == superfamilyId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromFamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? familyId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.FamilyId == familyId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? familyId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.FamilyId == familyId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromFamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? familyId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.FamilyId == familyId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubfamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? subfamilyId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubfamilyId == subfamilyId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? subfamilyId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubfamilyId == subfamilyId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubfamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? subfamilyId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubfamilyId == subfamilyId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfrafamilyIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? infrafamilyId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfrafamilyId == infrafamilyId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfrafamilyIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? infrafamilyId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfrafamilyId == infrafamilyId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfrafamilyIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? infrafamilyId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfrafamilyId == infrafamilyId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSupertribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? supertribusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SupertribusId == supertribusId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? supertribusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SupertribusId == supertribusId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSupertribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? supertribusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SupertribusId == supertribusId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromTribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? tribusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.TribusId == tribusId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromTribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? tribusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.TribusId == tribusId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromTribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? tribusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.TribusId == tribusId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromSubtribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? subtribusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubtribusId == subtribusId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromSubtribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? subtribusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubtribusId == subtribusId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromSubtribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? subtribusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubtribusId == subtribusId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromInfratribusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? infratribusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfratribusId == infratribusId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? infratribusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfratribusId == infratribusId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromInfratribusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? infratribusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfratribusId == infratribusId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromGenusIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? genusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.GenusId == genusId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? genusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.GenusId == genusId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromGenusIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? genusId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.GenusId == genusId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }
    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromFiSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? fispeciesId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.FiSpeciesId == fispeciesId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? fispeciesId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.FiSpeciesId == fispeciesId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromFiSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? fispeciesId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.FiSpeciesId == fispeciesId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    //-----------------------------------------------------------
    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceAuthorsCollectionOrderByInfoFromPlSpeciesIdAndRefSourceIdIsNullAndRefExpertIdIsNull(int? plspeciesId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.PlSpeciesId == plspeciesId && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceSourcesCollectionOrderByInfoFromPlSpeciesIdAndRefAuthorIdIsNullAndRefExpertIdIsNull(int? plspeciesId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.PlSpeciesId == plspeciesId && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90Reference> GetTbl90ReferenceExpertsCollectionOrderByInfoFromPlSpeciesIdAndRefAuthorIdIsNullAndRefSourceIdIsNull(int? plspeciesId)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.PlSpeciesId == plspeciesId && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        return null!;
    }

    //-----------------------------------------------------------

    public async Task<Tbl90Reference> GetReferenceSingleByReferenceId(int referenceId)
    {
        if (Context.Tbl90References != null)
        {
            var single = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == referenceId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl90Reference>> GetLastDatasetInTbl90References()
    {
        if (Context.Tbl90References != null)
        {
            var collection = Context.Tbl90References
                .OrderBy(c => c.ReferenceId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl90Reference> { collection };
            }
        }
        return null!;
    }


    #endregion

    #region Reference Copy

    public async Task<ObservableCollection<Tbl90Reference>> CopyReference(Tbl90Reference selected)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl90Reference
                {
                    RegnumId = dataset.RegnumId,
                    PhylumId = dataset.PhylumId,
                    DivisionId = dataset.DivisionId,
                    SubphylumId = dataset.SubphylumId,
                    SubdivisionId = dataset.SubdivisionId,
                    SuperclassId = dataset.SuperclassId,
                    ClassId = dataset.ClassId,
                    SubclassId = dataset.SubclassId,
                    InfraclassId = dataset.InfraclassId,
                    LegioId = dataset.LegioId,
                    OrdoId = dataset.OrdoId,
                    SubordoId = dataset.SubordoId,
                    InfraordoId = dataset.InfraordoId,
                    SuperfamilyId = dataset.SuperfamilyId,
                    FamilyId = dataset.FamilyId,
                    SubfamilyId = dataset.SubfamilyId,
                    InfrafamilyId = dataset.InfrafamilyId,
                    SupertribusId = dataset.SupertribusId,
                    TribusId = dataset.TribusId,
                    SubtribusId = dataset.SubtribusId,
                    InfratribusId = dataset.InfratribusId,
                    GenusId = dataset.GenusId,
                    PlSpeciesId = dataset.PlSpeciesId,
                    FiSpeciesId = dataset.FiSpeciesId,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Info = dataset.Info,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceRegnum(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            RegnumId = dataset.RegnumId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            RegnumId = dataset.RegnumId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            RegnumId = dataset.RegnumId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl90Reference>> CopyReferencePhylum(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            PhylumId = dataset.PhylumId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            PhylumId = dataset.PhylumId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            PhylumId = dataset.PhylumId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceDivision(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            DivisionId = dataset.DivisionId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            DivisionId = dataset.DivisionId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            DivisionId = dataset.DivisionId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceSubphylum(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubphylumId = dataset.SubphylumId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubphylumId = dataset.SubphylumId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubphylumId = dataset.SubphylumId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceSubdivision(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubdivisionId = dataset.SubdivisionId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubdivisionId = dataset.SubdivisionId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubdivisionId = dataset.SubdivisionId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceSuperclass(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SuperclassId = dataset.SuperclassId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SuperclassId = dataset.SuperclassId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SuperclassId = dataset.SuperclassId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceClass(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            ClassId = dataset.ClassId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            ClassId = dataset.ClassId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            ClassId = dataset.ClassId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceSubclass(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubclassId = dataset.SubclassId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubclassId = dataset.SubclassId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubclassId = dataset.SubclassId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceInfraclass(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            InfraclassId = dataset.InfraclassId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            InfraclassId = dataset.InfraclassId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            InfraclassId = dataset.InfraclassId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceLegio(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            LegioId = dataset.LegioId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            LegioId = dataset.LegioId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            LegioId = dataset.LegioId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceOrdo(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            OrdoId = dataset.OrdoId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            OrdoId = dataset.OrdoId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            OrdoId = dataset.OrdoId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceSubordo(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubordoId = dataset.SubordoId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubordoId = dataset.SubordoId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubordoId = dataset.SubordoId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceInfraordo(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            InfraordoId = dataset.InfraordoId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            InfraordoId = dataset.InfraordoId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            InfraordoId = dataset.InfraordoId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceSuperfamily(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SuperfamilyId = dataset.SuperfamilyId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SuperfamilyId = dataset.SuperfamilyId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SuperfamilyId = dataset.SuperfamilyId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceFamily(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            FamilyId = dataset.FamilyId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            FamilyId = dataset.FamilyId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            FamilyId = dataset.FamilyId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceSubfamily(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubfamilyId = dataset.SubfamilyId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubfamilyId = dataset.SubfamilyId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubfamilyId = dataset.SubfamilyId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceInfrafamily(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            InfrafamilyId = dataset.InfrafamilyId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            InfrafamilyId = dataset.InfrafamilyId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            InfrafamilyId = dataset.InfrafamilyId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceSupertribus(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SupertribusId = dataset.SupertribusId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SupertribusId = dataset.SupertribusId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SupertribusId = dataset.SupertribusId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceTribus(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            TribusId = dataset.TribusId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            TribusId = dataset.TribusId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            TribusId = dataset.TribusId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceSubtribus(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubtribusId = dataset.SubtribusId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubtribusId = dataset.SubtribusId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            SubtribusId = dataset.SubtribusId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceInfratribus(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            InfratribusId = dataset.InfratribusId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            InfratribusId = dataset.InfratribusId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            InfratribusId = dataset.InfratribusId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceGenus(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            GenusId = dataset.GenusId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            GenusId = dataset.GenusId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            GenusId = dataset.GenusId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceFiSpecies(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            FiSpeciesId = dataset.FiSpeciesId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            FiSpeciesId = dataset.FiSpeciesId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            FiSpeciesId = dataset.FiSpeciesId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferencePlSpecies(Tbl90Reference selected, string reference)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.SingleOrDefault(a => a.ReferenceId == selected.ReferenceId);

            var collection = new ObservableCollection<Tbl90Reference>();
            switch (reference)
            {
                case "Expert":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            PlSpeciesId = dataset.PlSpeciesId,
                            RefExpertId = dataset.RefExpertId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Source":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            PlSpeciesId = dataset.PlSpeciesId,
                            RefSourceId = dataset.RefSourceId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
                case "Author":
                    if (dataset != null)
                    {
                        collection.Insert(index: 0, item: new Tbl90Reference()
                        {
                            PlSpeciesId = dataset.PlSpeciesId,
                            RefAuthorId = dataset.RefAuthorId,
                            Valid = dataset.Valid,
                            ValidYear = dataset.ValidYear,
                            Info = "New",
                            Memo = dataset.Memo
                        });
                    }

                    break;
            }
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90Reference>> CopyReferenceSource(Tbl90Reference selected)
    {
        if (Context.Tbl90References != null)
        {
            var dataset = Context.Tbl90References.FirstOrDefault(a => a.ReferenceId == selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl90Reference
                {
                    //  RegnumName = CultRes.StringsRes.DatasetNew,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Info = "New",
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion

    #region Reference Delete
    public async Task<bool> DeleteReference(Tbl90Reference selected)
    {
        var returnBool = false;

        try
        {
            var dataset = await GetReferenceSingleByReferenceId(selected.ReferenceId);
            if (dataset != null)
            {
                await DeleteReferenceDataset(dataset);
                returnBool = true;

                await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.ReferenceId.ToString());
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteReferenceDataset(Tbl90Reference selected)
    {
        Context.Tbl90References?.Remove(selected);

        await Context.SaveChangesAsync();
        await Task.CompletedTask;
    }
    Task IDataService.DeleteTbl90ReferenceDatasetsFromPhylumId(int phylumId)
    {
        return DeleteTbl90ReferenceDatasetsFromPhylumId(phylumId);
    }

    private async Task DeleteTbl90ReferenceDatasetsFromRegnumId(int regnumId)
    {
        if (Context.Tbl90References != null)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.RegnumId == regnumId));
        }

        if (Tbl90ReferencesList.Count <= 0)
        {
            return;
        }
        //   if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

        await DeleteDatasetsInTbl90References(Tbl90ReferencesList);

        await Task.CompletedTask;
    }
    private async Task DeleteTbl90ReferenceDatasetsFromPhylumId(int phylumId)
    {
        if (Context.Tbl90References != null)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.PhylumId == phylumId));
        }

        if (Tbl90ReferencesList.Count <= 0)
        {
            return;
        }
        //   if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

        await DeleteDatasetsInTbl90References(Tbl90ReferencesList);

        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl90References(ObservableCollection<Tbl90Reference> tbl90ReferencesList)
    {
        foreach (var t in tbl90ReferencesList)
        {
            Context.Tbl90References?.Remove(t);
        }

        await Context.SaveChangesAsync();
        await Task.CompletedTask;
    }

    public async Task DeleteConnectedRegnumReferences(Tbl03Regnum selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.RegnumId == selected.RegnumId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedPhylumReferences(Tbl06Phylum selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.PhylumId == selected.PhylumId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedDivisionReferences(Tbl09Division selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.DivisionId == selected.DivisionId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubphylumReferences(Tbl12Subphylum selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubphylumId == selected.SubphylumId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubdivisionReferences(Tbl15Subdivision selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubdivisionId == selected.SubdivisionId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSuperclassReferences(Tbl18Superclass selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SuperclassId == selected.SuperclassId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedClassReferences(Tbl21Class selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.ClassId == selected.ClassId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubclassReferences(Tbl24Subclass selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubclassId == selected.SubclassId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedInfraclassReferences(Tbl27Infraclass selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfraclassId == selected.InfraclassId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedLegioReferences(Tbl30Legio selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.LegioId == selected.LegioId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedOrdoReferences(Tbl33Ordo selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.OrdoId == selected.OrdoId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubordoReferences(Tbl36Subordo selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubordoId == selected.SubordoId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedInfraordoReferences(Tbl39Infraordo selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfraordoId == selected.InfraordoId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSuperfamilyReferences(Tbl42Superfamily selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SuperfamilyId == selected.SuperfamilyId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedFamilyReferences(Tbl45Family selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.FamilyId == selected.FamilyId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubfamilyReferences(Tbl48Subfamily selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubfamilyId == selected.SubfamilyId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedInfrafamilyReferences(Tbl51Infrafamily selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfrafamilyId == selected.InfrafamilyId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSupertribusReferences(Tbl54Supertribus selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SupertribusId == selected.SupertribusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedTribusReferences(Tbl57Tribus selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.TribusId == selected.TribusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubtribusReferences(Tbl60Subtribus selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubtribusId == selected.SubtribusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedInfratribusReferences(Tbl63Infratribus selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.InfratribusId == selected.InfratribusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedGenusReferences(Tbl66Genus selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.GenusId == selected.GenusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedFiSpeciesReferences(Tbl69FiSpecies selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.FiSpeciesId == selected.FiSpeciesId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedPlSpeciesReferences(Tbl72PlSpecies selected)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.PlSpeciesId == selected.PlSpeciesId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl90References.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    //--------------------------------------------------------
    public async Task DeleteRegnumReferences(int regnumId)
    {
        //  Tbl90ReferencesList = _extCrud.DeleteDatasetsWithRegnumIdInTableReference(regnumId); 
        if (Context.Tbl90References != null)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.RegnumId == regnumId));
        }

        if (Tbl90ReferencesList.Count <= 0)
        {
            return;
        }
        //   if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

        await DeleteReferences(Tbl90ReferencesList);

        //  _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        //---------------
        // //  Tbl90ReferencesList = await DeleteTbl90ReferencesCollectionWithRegnumId(regnumId);
        //   Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References.Where(x => x.RegnumId == regnumId));
        ////   Tbl90ReferencesList = await DeleteDatasetsWithRegnumIdInTableReferenceAsync(regnumId);
        //   if (Tbl90ReferencesList.Count <= 0) return;
        //   //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("ReferenceAuthor" + " " + "ReferenceSource" + " " + "ReferenceExpert")) return;

        //   await DeleteReferences(Tbl90ReferencesList);

        //   //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Reference");
        await Task.CompletedTask;
    }
    public async Task DeletePhylumReferences(int phylumId)
    {
        //  Tbl90ReferencesList = _extCrud.DeleteDatasetsWithRegnumIdInTableReference(regnumId); 
        if (Context.Tbl90References != null)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.PhylumId == phylumId));
        }

        if (Tbl90ReferencesList.Count <= 0)
        {
            return;
        }
        //   if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

        await DeleteReferences(Tbl90ReferencesList);

        //  _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        //---------------------
        ////   Tbl90ReferencesList = await DeleteTbl90ReferencesCollectionWithPhylumId(phylumId);
        //   Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References.Where(x => x.PhylumId == phylumId));
        //   //   Tbl90ReferencesList = await DeleteDatasetsWithRegnumIdInTableReferenceAsync(regnumId);
        //   if (Tbl90ReferencesList.Count <= 0) return;
        //   //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("ReferenceAuthor" + " " + "ReferenceSource" + " " + "ReferenceExpert")) return;

        //   await DeleteReferences(Tbl90ReferencesList);

        //   //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Reference");
        await Task.CompletedTask;
    }
    public async Task DeleteDivisionReferences(int divisionId)
    {
        //  Tbl90ReferencesList = _extCrud.DeleteDatasetsWithRegnumIdInTableReference(regnumId); 
        if (Context.Tbl90References != null)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.DivisionId == divisionId));
        }

        if (Tbl90ReferencesList.Count <= 0)
        {
            return;
        }
        //   if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

        await DeleteReferences(Tbl90ReferencesList);

        //  _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        //----------------
        ////   Tbl90ReferencesList = await DeleteTbl90ReferencesCollectionWithDivisionId(divisionId);
        //   Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References.Where(x => x.DivisionId == divisionId));
        //   //   Tbl90ReferencesList = await DeleteDatasetsWithRegnumIdInTableReferenceAsync(regnumId);
        //   if (Tbl90ReferencesList.Count <= 0) return;
        //   //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("ReferenceAuthor" + " " + "ReferenceSource" + " " + "ReferenceExpert")) return;

        //   await DeleteReferences(Tbl90ReferencesList);

        //   //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Reference");
        await Task.CompletedTask;
    }
    public async Task DeleteSubphylumReferences(int subphylumId)
    {
        //  Tbl90ReferencesList = _extCrud.DeleteDatasetsWithRegnumIdInTableReference(regnumId); 
        if (Context.Tbl90References != null)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubphylumId == subphylumId));
        }

        if (Tbl90ReferencesList.Count <= 0)
        {
            return;
        }
        //   if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

        await DeleteReferences(Tbl90ReferencesList);

        //  _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        //----------------
        ////  Tbl90ReferencesList = await DeleteTbl90ReferencesCollectionWithSubphylumId(subphylumId);
        //  Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References.Where(x => x.SubphylumId == subphylumId));
        //  //   Tbl90ReferencesList = await DeleteDatasetsWithRegnumIdInTableReferenceAsync(regnumId);
        //  if (Tbl90ReferencesList.Count <= 0) return;
        //  //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("ReferenceAuthor" + " " + "ReferenceSource" + " " + "ReferenceExpert")) return;

        //  await DeleteReferences(Tbl90ReferencesList);

        //  //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Reference");
        await Task.CompletedTask;
    }
    public async Task DeleteSubdivisionReferences(int subdivisionId)
    {
        //  Tbl90ReferencesList = _extCrud.DeleteDatasetsWithRegnumIdInTableReference(regnumId); 
        if (Context.Tbl90References != null)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SubdivisionId == subdivisionId));
        }

        if (Tbl90ReferencesList.Count <= 0)
        {
            return;
        }
        //   if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

        await DeleteReferences(Tbl90ReferencesList);

        //  _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        //----------------

        ////  Tbl90ReferencesList = await DeleteTbl90ReferencesCollectionWithSubdivisionId(subdivisionId);
        //  Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References.Where(x => x.SubdivisionId == subdivisionId));
        //  //   Tbl90ReferencesList = await DeleteDatasetsWithRegnumIdInTableReferenceAsync(regnumId);
        //  if (Tbl90ReferencesList.Count <= 0) return;
        //  //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("ReferenceAuthor" + " " + "ReferenceSource" + " " + "ReferenceExpert")) return;

        //  await DeleteReferences(Tbl90ReferencesList);

        //  //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Reference");
        await Task.CompletedTask;
    }
    public async Task DeleteSuperclassReferences(int superclassId)
    {
        //  Tbl90ReferencesList = _extCrud.DeleteDatasetsWithRegnumIdInTableReference(regnumId); 
        if (Context.Tbl90References != null)
        {
            Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Where(e => e.SuperclassId == superclassId));
        }

        if (Tbl90ReferencesList.Count <= 0)
        {
            return;
        }
        //   if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

        await DeleteReferences(Tbl90ReferencesList);

        //  _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
        //----------------

        //// Tbl90ReferencesList = await DeleteTbl90ReferencesCollectionWithSuperclassId(superclassId);
        // Tbl90ReferencesList = new ObservableCollection<Tbl90Reference>(Context.Tbl90References.Where(x => x.SuperclassId == superclassId));

        // //   Tbl90ReferencesList = await DeleteDatasetsWithRegnumIdInTableReferenceAsync(regnumId);
        // if (Tbl90ReferencesList.Count <= 0) return;
        // //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("ReferenceAuthor" + " " + "ReferenceSource" + " " + "ReferenceExpert")) return;

        // await DeleteReferences(Tbl90ReferencesList);

        // //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Reference");
        await Task.CompletedTask;
    }
    public async Task DeleteReferences(ObservableCollection<Tbl90Reference> coll)
    {
        foreach (var t in coll)
        {
            Context.Tbl90References?.Remove(t);
        }

        await Context.SaveChangesAsync();
        await Task.CompletedTask;
    }

    #endregion

    #region Reference Save

    public async Task<bool> SaveReference(Tbl90Reference selected, string reference)
    {
        var returnBool = false;

        try
        {
            var dataset = await GetReferenceSingleByReferenceId(selected.ReferenceId);

            if (selected.Info != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.Info))
            {
                return false;
            }

            if (selected.ReferenceId == 0)
            {
                dataset = await ReferenceAdd(selected, reference);
            }
            else
            {
                dataset = await ReferenceUpdate(dataset, selected, reference);
            }

            try
            {
                await ReferenceSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.Info != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.Info);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl90Reference> ReferenceUpdate(Tbl90Reference home, Tbl90Reference selected, string reference)
    {
        if (true) //update
        {
            home.RefExpertId = selected.RefExpertId;
            home.RefAuthorId = selected.RefAuthorId;
            home.RefSourceId = selected.RefSourceId;
            home.RegnumId = selected.RegnumId;
            home.PhylumId = selected.PhylumId;
            home.DivisionId = selected.DivisionId;
            home.SubphylumId = selected.SubphylumId;
            home.SubdivisionId = selected.SubdivisionId;
            home.SuperclassId = selected.SuperclassId;
            home.ClassId = selected.ClassId;
            home.SubclassId = selected.SubclassId;
            home.InfraclassId = selected.InfraclassId;
            home.LegioId = selected.LegioId;
            home.OrdoId = selected.OrdoId;
            home.SubordoId = selected.SubordoId;
            home.InfraordoId = selected.InfraordoId;
            home.SuperfamilyId = selected.SuperfamilyId;
            home.FamilyId = selected.FamilyId;
            home.SubfamilyId = selected.SubfamilyId;
            home.InfrafamilyId = selected.InfrafamilyId;
            home.SupertribusId = selected.SupertribusId;
            home.TribusId = selected.TribusId;
            home.SubtribusId = selected.SubtribusId;
            home.InfratribusId = selected.InfratribusId;
            home.GenusId = selected.GenusId;
            home.PlSpeciesId = selected.PlSpeciesId;
            home.FiSpeciesId = selected.FiSpeciesId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Info = selected.Info;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;

    }
    public async Task<Tbl90Reference> ReferenceAdd(Tbl90Reference selected, string reference)
    {
        var home = new Tbl90Reference() //add new
        {
            RefAuthorId = selected.RefAuthorId,
            RefSourceId = selected.RefSourceId,
            RefExpertId = selected.RefExpertId,
            RegnumId = selected.RegnumId,
            PhylumId = selected.PhylumId,
            DivisionId = selected.DivisionId,
            SubphylumId = selected.SubphylumId,
            SubdivisionId = selected.SubdivisionId,
            SuperclassId = selected.SuperclassId,
            ClassId = selected.ClassId,
            SubclassId = selected.SubclassId,
            InfraclassId = selected.InfraclassId,
            LegioId = selected.LegioId,
            OrdoId = selected.OrdoId,
            SubordoId = selected.SubordoId,
            InfraordoId = selected.InfraordoId,
            SuperfamilyId = selected.SuperfamilyId,
            FamilyId = selected.FamilyId,
            SubfamilyId = selected.SubfamilyId,
            InfrafamilyId = selected.InfrafamilyId,
            SupertribusId = selected.SupertribusId,
            TribusId = selected.TribusId,
            SubtribusId = selected.SubtribusId,
            InfratribusId = selected.InfratribusId,
            GenusId = selected.GenusId,
            PlSpeciesId = selected.PlSpeciesId,
            FiSpeciesId = selected.FiSpeciesId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Info = selected.Info,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now
        };
        await Task.CompletedTask;
        return home;
    }
    public async Task ReferenceSave(Tbl90Reference home, Tbl90Reference selected)
    {
        if (selected.ReferenceId != 0)   //update
        {
            Context.Tbl90References?.Update(home);
        }
        else                             //add
        {
            Context.Tbl90References?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }


    #endregion

    #endregion Reference 

    #region RefAuthor

    #region Get RefAuthor
    public ObservableCollection<Tbl90RefAuthor> GetTbl90RefAuthorsCollectionOrderByRefAuthorNameAndBookNameAndPage1()
    {
        if (Context.Tbl90RefAuthors != null)
        {
            var collection = new ObservableCollection<Tbl90RefAuthor>(Context.Tbl90RefAuthors
                .OrderBy(k => k.RefAuthorName)
                .ThenBy(a => a.BookName)
                .ThenBy(a => a.Page1));
            return collection;
        }

        return null!;
    }

    public async Task<ObservableCollection<Tbl90RefAuthor>> GetTbl90RefAuthorsCollectionOrderByRefAuthorNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl90RefAuthors != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl90RefAuthor>(Context.Tbl90RefAuthors
                    .Where(e => e.RefAuthorId == id))
                : new ObservableCollection<Tbl90RefAuthor>(Context.Tbl90RefAuthors
                    .Where(e => e.RefAuthorName.StartsWith(searchName))
                    .OrderBy(a => a.RefAuthorName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90RefAuthor> GetTbl90RefAuthorsCollectionOrderByRefAuthorNameFromRefAuthorId(int? refAuthorId)
    {
        if (Context.Tbl90RefAuthors != null)
        {
            var collection = new ObservableCollection<Tbl90RefAuthor>(Context.Tbl90RefAuthors
                .Where(e => e.RefAuthorId == refAuthorId)
                .OrderBy(k => k.RefAuthorName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<Tbl90RefAuthor> GetRefAuthorSingleByRefAuthorId(int refAuthorId)
    {
        if (Context.Tbl90RefAuthors != null)
        {
            var single = Context.Tbl90RefAuthors.SingleOrDefault(a => a.RefAuthorId == refAuthorId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl90RefAuthor> GetRefAuthorSingleByRefAuthorNameAndArticelTitleAndBookNameAndPage(string refAuthorName, string articelTitle, string bookName, string page)
    {
        if (Context.Tbl90RefAuthors != null)
        {
            var single = Context.Tbl90RefAuthors.SingleOrDefault(a => a.RefAuthorName == refAuthorName &&
                                                                      a.ArticelTitle == articelTitle &&
                                                                      a.BookName == bookName &&
                                                                      a.Page1 == page);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90RefAuthor>> GetLastDatasetInTbl90RefAuthors()
    {
        if (Context.Tbl90RefAuthors != null)
        {
            var collection = Context.Tbl90RefAuthors
                .OrderBy(c => c.RefAuthorId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl90RefAuthor> { collection };
            }
        }
        return null!;
    }

    #endregion Get RefAuthor

    #region Copy RefAuthor

    public async Task<ObservableCollection<Tbl90RefAuthor>> CopyRefAuthor(Tbl90RefAuthor selected)
    {
        if (Context.Tbl90RefAuthors != null)
        {
            var dataset = Context.Tbl90RefAuthors.FirstOrDefault(a => a.RefAuthorId == selected.RefAuthorId);
            var collection = new ObservableCollection<Tbl90RefAuthor>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl90RefAuthor
                {
                    RefAuthorName = "New",
                    //RefAuthorName = CultRes.StringsRes.DatasetNew,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    PublicationYear = dataset.PublicationYear,
                    ArticelTitle = dataset.ArticelTitle,
                    BookName = dataset.BookName,
                    Info = dataset.Info,
                    Page1 = dataset.Page1,
                    Publisher = dataset.Publisher,
                    PublicationPlace = dataset.PublicationPlace,
                    ISBN = dataset.ISBN,
                    Notes = dataset.Notes,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    #endregion Copy RefAuthor

    #region Delete RefAuthor

    public async Task<bool> DeleteRefAuthor(Tbl90RefAuthor selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetRefAuthorSingleByRefAuthorId(selected.RefAuthorId);
            if (dataset != null)
            {
                await DeleteRefAuthorDataset(dataset);
                returnBool = true;

                if (selected.RefAuthorName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.RefAuthorName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteRefAuthorDataset(Tbl90RefAuthor selected)
    {
        Context.Tbl90RefAuthors?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }


    #endregion Delete RefAuthor

    #region Save RefAuthor

    public async Task<bool> SaveRefAuthor(Tbl90RefAuthor selected)
    {
        var returnBool = false;

        try
        {
            var datasetByName = await GetRefAuthorSingleByRefAuthorNameAndArticelTitleAndBookNameAndPage(selected.RefAuthorName, selected.ArticelTitle, selected.BookName, selected.Page1);

            if (datasetByName != null && selected.RefAuthorId == 0)
            {
                await AllDialogs.DatasetExistInfoMessageDialogAsync();
                return false;
            }

            var dataset = await GetRefAuthorSingleByRefAuthorId(selected.RefAuthorId);

            if (!await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.RefAuthorName))
            {
                return false;
            }

            if (selected.RefAuthorId == 0)
            {
                dataset = await RefAuthorAdd(selected);
            }
            else
            {
                dataset = await RefAuthorUpdate(dataset, selected);
            }

            try
            {
                await RefAuthorSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }
            await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.RefAuthorName);
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl90RefAuthor> RefAuthorUpdate(Tbl90RefAuthor home, Tbl90RefAuthor selected)
    {
        if (true) //update
        {
            home.RefAuthorName = selected.RefAuthorName;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.PublicationYear = selected.PublicationYear;
            home.ArticelTitle = selected.ArticelTitle;
            home.BookName = selected.BookName;
            home.Info = selected.Info;
            home.Page1 = selected.Page1;
            home.Publisher = selected.Publisher;
            home.PublicationPlace = selected.PublicationPlace;
            home.ISBN = selected.ISBN;
            home.Notes = selected.Notes;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl90RefAuthor> RefAuthorAdd(Tbl90RefAuthor selected)
    {
        var home = new Tbl90RefAuthor() //add new
        {
            RefAuthorName = selected.RefAuthorName,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            PublicationYear = selected.PublicationYear,
            ArticelTitle = selected.ArticelTitle,
            BookName = selected.BookName,
            Info = selected.Info,
            Page1 = selected.Page1,
            Publisher = selected.Publisher,
            PublicationPlace = selected.PublicationPlace,
            ISBN = selected.ISBN,
            Notes = selected.Notes,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
            Memo = selected.Memo

        };
        await Task.CompletedTask;
        return home;
    }

    public async Task RefAuthorSave(Tbl90RefAuthor home, Tbl90RefAuthor selected)
    {
        if (selected.RefAuthorId != 0)   //update
        {
            Context.Tbl90RefAuthors?.Update(home);
        }
        else           //add
        {
            Context.Tbl90RefAuthors?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion Save RefAuthor

    #endregion RefAuthor

    #region RefSource

    #region Get RefSource
    public ObservableCollection<Tbl90RefSource> GetTbl90RefSourcesCollectionOrderByRefSourceName()
    {
        if (Context.Tbl90RefSources != null)
        {
            var collection = new ObservableCollection<Tbl90RefSource>(Context.Tbl90RefSources
                .OrderBy(k => k.RefSourceName));
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl90RefSource>> GetTbl90RefSourcesCollectionOrderByRefSourceNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl90RefSources != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl90RefSource>(Context.Tbl90RefSources
                    .Where(e => e.RefSourceId == id))
                : new ObservableCollection<Tbl90RefSource>(Context.Tbl90RefSources
                    .Where(e => e.RefSourceName.StartsWith(searchName))
                    .OrderBy(a => a.RefSourceName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90RefSource> GetTbl90RefSourcesCollectionOrderByRefSourceNameFromRefSourceId(int? refSourceId)
    {
        if (Context.Tbl90RefSources != null)
        {
            var collection = new ObservableCollection<Tbl90RefSource>(Context.Tbl90RefSources
                .Where(e => e.RefSourceId == refSourceId)
                .OrderBy(k => k.RefSourceName)
            );
            return collection;
        }
        return null!;
    }


    public async Task<Tbl90RefSource> GetRefSourceSingleByRefSourceId(int refSourceId)
    {
        if (Context.Tbl90RefSources != null)
        {
            var single = Context.Tbl90RefSources.SingleOrDefault(a => a.RefSourceId == refSourceId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl90RefSource> GetRefSourceSingleByRefSourceNameAndAuthor(string refSourceName, string author)
    {
        if (Context.Tbl90RefSources != null)
        {
            var single = Context.Tbl90RefSources.SingleOrDefault(a => a.RefSourceName == refSourceName && a.Author == author);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90RefSource>> GetLastDatasetInTbl90RefSources()
    {
        if (Context.Tbl90RefSources != null)
        {
            var collection = Context.Tbl90RefSources
                .OrderBy(c => c.RefSourceId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl90RefSource> { collection };
            }
        }
        return null!;
    }


    #endregion Get RefSource

    #region Copy RefSource
    public async Task<ObservableCollection<Tbl90RefSource>> CopyRefSource(Tbl90RefSource selected)
    {
        if (Context.Tbl90RefSources != null)
        {
            var dataset = Context.Tbl90RefSources.FirstOrDefault(a => a.RefSourceId == selected.RefSourceId);
            var collection = new ObservableCollection<Tbl90RefSource>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl90RefSource
                {
                    RefSourceName = "New",
                    //RefSourceName = CultRes.StringsRes.DatasetNew,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Author = dataset.Author,
                    SourceYear = dataset.SourceYear,
                    Info = dataset.Info,
                    Notes = dataset.Notes,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }


    #endregion Copy RefSource

    #region Delete RefSource
    public async Task<bool> DeleteRefSource(Tbl90RefSource selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetRefSourceSingleByRefSourceId(selected.RefSourceId);
            if (dataset != null)
            {
                await DeleteRefSourceDataset(dataset);
                returnBool = true;

                if (selected.RefSourceName != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.RefSourceName);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteRefSourceDataset(Tbl90RefSource selected)
    {
        Context.Tbl90RefSources?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }


    #endregion Delete RefSource

    #region Save RefSource
    public async Task<bool> SaveRefSource(Tbl90RefSource selected)
    {
        var returnBool = false;

        try
        {
            var datasetByName = await GetRefSourceSingleByRefSourceNameAndAuthor(selected.RefSourceName, selected.Author);

            if (datasetByName != null && selected.RefSourceId == 0)
            {
                await AllDialogs.DatasetExistInfoMessageDialogAsync();
                return false;
            }

            var dataset = await GetRefSourceSingleByRefSourceId(selected.RefSourceId);

            if (!await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.RefSourceName + " " + selected.Author))
            {
                return false;
            }

            if (selected.RefSourceId == 0)
            {
                dataset = await RefSourceAdd(selected);
            }
            else
            {
                dataset = await RefSourceUpdate(dataset, selected);
            }

            try
            {
                await RefSourceSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }
            await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.RefSourceName + " " + selected.Author);
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl90RefSource> RefSourceUpdate(Tbl90RefSource home, Tbl90RefSource selected)
    {
        if (true) //update
        {
            home.RefSourceName = selected.RefSourceName;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Author = selected.Author;
            home.SourceYear = selected.SourceYear;
            home.Info = selected.Info;
            home.Notes = selected.Notes;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl90RefSource> RefSourceAdd(Tbl90RefSource selected)
    {
        var home = new Tbl90RefSource() //add new
        {
            RefSourceName = selected.RefSourceName,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Author = selected.Author,
            SourceYear = selected.SourceYear,
            Info = selected.Info,
            Notes = selected.Notes,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now

        };
        await Task.CompletedTask;
        return home;
    }

    public async Task RefSourceSave(Tbl90RefSource home, Tbl90RefSource selected)
    {
        if (selected.RefSourceId != 0)   //update
        {
            Context.Tbl90RefSources?.Update(home);
        }
        else           //add
        {
            Context.Tbl90RefSources?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion Save RefSource

    #endregion RefAuthor

    #region RefExpert

    #region Get RefExpert
    public ObservableCollection<Tbl90RefExpert> GetTbl90RefExpertsCollectionOrderByRefExpertName()
    {
        if (Context.Tbl90RefExperts != null)
        {
            var collection = new ObservableCollection<Tbl90RefExpert>(Context.Tbl90RefExperts
                .OrderBy(k => k.RefExpertName));
            return collection;
        }
        return null!;
    }
    public async Task<ObservableCollection<Tbl90RefExpert>> GetTbl90RefExpertsCollectionOrderByRefExpertNameFromSearchNameOrId(string searchName)
    {
        if (Context.Tbl90RefExperts != null)
        {
            var collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl90RefExpert>(Context.Tbl90RefExperts
                    .Where(e => e.RefExpertId == id))
                : new ObservableCollection<Tbl90RefExpert>(Context.Tbl90RefExperts
                    .Where(e => e.RefExpertName.StartsWith(searchName))
                    .OrderBy(a => a.RefExpertName)
                );
            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl90RefExpert> GetTbl90RefExpertsCollectionOrderByRefExpertNameFromRefExpertId(int? refExpertId)
    {
        if (Context.Tbl90RefExperts != null)
        {
            var collection = new ObservableCollection<Tbl90RefExpert>(Context.Tbl90RefExperts
                .Where(e => e.RefExpertId == refExpertId)
                .OrderBy(k => k.RefExpertName)
            );
            return collection;
        }
        return null!;
    }

    public async Task<Tbl90RefExpert> GetRefExpertSingleByRefExpertId(int refExpertId)
    {
        if (Context.Tbl90RefExperts != null)
        {
            var single = Context.Tbl90RefExperts.SingleOrDefault(a => a.RefExpertId == refExpertId);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<Tbl90RefExpert> GetRefExpertSingleByRefExpertName(string refExpertName)
    {
        if (Context.Tbl90RefExperts != null)
        {
            var single = Context.Tbl90RefExperts.SingleOrDefault(a => a.RefExpertName == refExpertName);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public async Task<ObservableCollection<Tbl90RefExpert>> GetLastDatasetInTbl90RefExperts()
    {
        if (Context.Tbl90RefExperts != null)
        {
            var collection = Context.Tbl90RefExperts
                .OrderBy(c => c.RefExpertId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            if (collection != null)
            {
                return new ObservableCollection<Tbl90RefExpert> { collection };
            }
        }
        return null!;
    }

    #endregion Get RefExpert

    #region Copy RefExpert
    public async Task<ObservableCollection<Tbl90RefExpert>> CopyRefExpert(Tbl90RefExpert selected)
    {
        if (Context.Tbl90RefExperts != null)
        {
            var dataset = Context.Tbl90RefExperts.FirstOrDefault(a => a.RefExpertId == selected.RefExpertId);
            var collection = new ObservableCollection<Tbl90RefExpert>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl90RefExpert
                {
                    RefExpertName = "New",
                    //RefExpertName = CultRes.StringsRes.DatasetNew,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Info = dataset.Info,
                    Notes = dataset.Notes,
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }


    #endregion Copy RefExpert

    #region Delete RefExpert
    public async Task<bool> DeleteRefExpert(Tbl90RefExpert selected)
    {
        var returnBool = false;
        try
        {
            var dataset = await GetRefExpertSingleByRefExpertId(selected.RefExpertId);
            if (dataset != null)
            {
                await DeleteRefExpertDataset(dataset);
                returnBool = true;

                await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.RefExpertName);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }

    public async Task DeleteRefExpertDataset(Tbl90RefExpert selected)
    {
        Context.Tbl90RefExperts?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion Delete RefExpert

    #region Save RefExpert
    public async Task<bool> SaveRefExpert(Tbl90RefExpert selected)
    {
        var returnBool = false;

        try
        {
            var datasetByName = await GetRefExpertSingleByRefExpertName(selected.RefExpertName);

            if (datasetByName != null && selected.RefExpertId == 0)
            {
                await AllDialogs.DatasetExistInfoMessageDialogAsync();
                return false;
            }

            var dataset = await GetRefExpertSingleByRefExpertId(selected.RefExpertId);

            if (!await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.RefExpertName))
            {
                return false;
            }

            if (selected.RefExpertId == 0)
            {
                dataset = await RefExpertAdd(selected);
            }
            else
            {
                dataset = await RefExpertUpdate(dataset, selected);
            }

            try
            {
                await RefExpertSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }
            await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.RefExpertName);
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }

    public async Task<Tbl90RefExpert> RefExpertUpdate(Tbl90RefExpert home, Tbl90RefExpert selected)
    {
        if (true) //update
        {
            home.RefExpertName = selected.RefExpertName;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Info = selected.Info;
            home.Notes = selected.Notes;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }

    public async Task<Tbl90RefExpert> RefExpertAdd(Tbl90RefExpert selected)
    {
        var home = new Tbl90RefExpert() //add new
        {
            RefExpertName = selected.RefExpertName,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Info = selected.Info,
            Notes = selected.Notes,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now,
        };
        await Task.CompletedTask;
        return home;
    }

    public async Task RefExpertSave(Tbl90RefExpert home, Tbl90RefExpert selected)
    {
        if (selected.RefExpertId != 0)   //update
        {
            Context.Tbl90RefExperts?.Update(home);
        }
        else           //add
        {
            Context.Tbl90RefExperts?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }

    #endregion Save RefExpert

    #endregion RefExpert

    #endregion References Expert, Source, Author from Regnum-Userprofile

    #region Comments  from Regnum-Userprofile

    #region Get Comment
    public async Task<ObservableCollection<Tbl93Comment>> GetTbl93CommentsCollectionOrderByInfoFromSearchInfoOrId(string searchInfo)
    {
        ObservableCollection<Tbl93Comment> collection = null!;
        if (Context.Tbl93Comments != null)
        {
            collection = int.TryParse(searchInfo, out var id)
                ? new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                    .Where(e => e.CommentId == id))
                : new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                    .Where(e => e.Info.StartsWith(searchInfo))
                    .OrderBy(a => a.Info)
                );
        }

        await Task.CompletedTask;
        if (collection != null)
        {
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromRegnumId(int? regnumId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.RegnumId == regnumId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromPhylumId(int? phylumId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.PhylumId == phylumId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromDivisionId(int? divisionId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.DivisionId == divisionId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSubphylumId(int? subphylumId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubphylumId == subphylumId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSubdivisionId(int? subdivisionId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubdivisionId == subdivisionId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSuperclassId(int? superclassId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SuperclassId == superclassId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromClassId(int? classId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.ClassId == classId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSubclassId(int? subclassId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubclassId == subclassId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromInfraclassId(int? infraclassId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.InfraclassId == infraclassId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromLegioId(int? legioId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.LegioId == legioId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromOrdoId(int? ordoId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.OrdoId == ordoId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSubordoId(int? subordoId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubordoId == subordoId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromInfraordoId(int? infraordoId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.InfraordoId == infraordoId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSuperfamilyId(int? superfamilyId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SuperfamilyId == superfamilyId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromFamilyId(int? familyId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.FamilyId == familyId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSubfamilyId(int? subfamilyId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubfamilyId == subfamilyId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromInfrafamilyId(int? infrafamilyId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.InfrafamilyId == infrafamilyId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSupertribusId(int? supertribusId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SupertribusId == supertribusId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromTribusId(int? tribusId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.TribusId == tribusId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromSubtribusId(int? subtribusId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubtribusId == subtribusId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromInfratribusId(int? infratribusId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.InfratribusId == infratribusId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromGenusId(int? genusId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.GenusId == genusId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromFiSpeciesId(int? fispeciesId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.FiSpeciesId == fispeciesId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl93Comment> GetTbl93CommentsCollectionOrderByInfoFromPlSpeciesId(int? plspeciesId)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.PlSpeciesId == plspeciesId)
                .OrderBy(k => k.Info)
            );
            return collection;
        }
        return null!;
    }

    //--------------------------------------------------------------------
    public async Task<ObservableCollection<Tbl93Comment>> GetLastDatasetInTbl93Comments()
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = Context.Tbl93Comments
                .OrderBy(c => c.CommentId)
                .AsNoTracking()
                .LastOrDefault();

            await Task.CompletedTask;
            return new ObservableCollection<Tbl93Comment> { collection! };
        }
        return null!;
    }
    public async Task<Tbl93Comment> GetCommentSingleByCommentId(int id)
    {
        //  Tbl93Comment single = _uow.Tbl93Comments.GetById(id);
        if (Context.Tbl93Comments != null)
        {
            var single = Context.Tbl93Comments.FirstOrDefault(a => a.CommentId == id);
            await Task.CompletedTask;
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }
    #endregion Get Comment

    #region Copy Comment
    public async Task<ObservableCollection<Tbl93Comment>> CopyComment(Tbl93Comment selected)
    {
        //  var dataset = _uow.Tbl93Comments.GetById(selected.CommentId);
        if (Context.Tbl93Comments != null)
        {
            var dataset = Context.Tbl93Comments.FirstOrDefault(a => a.CommentId == selected.CommentId);
            var collection = new ObservableCollection<Tbl93Comment>();

            if (dataset != null)
            {
                collection.Insert(0, new Tbl93Comment
                {
                    RegnumId = dataset.RegnumId,
                    PhylumId = dataset.PhylumId,
                    DivisionId = dataset.DivisionId,
                    SubphylumId = dataset.SubphylumId,
                    SubdivisionId = dataset.SubdivisionId,
                    SuperclassId = dataset.SuperclassId,
                    ClassId = dataset.ClassId,
                    SubclassId = dataset.SubclassId,
                    InfraclassId = dataset.InfraclassId,
                    LegioId = dataset.LegioId,
                    OrdoId = dataset.OrdoId,
                    SubordoId = dataset.SubordoId,
                    InfraordoId = dataset.InfraordoId,
                    SuperfamilyId = dataset.SuperfamilyId,
                    FamilyId = dataset.FamilyId,
                    SubfamilyId = dataset.SubfamilyId,
                    InfrafamilyId = dataset.InfrafamilyId,
                    SupertribusId = dataset.SupertribusId,
                    TribusId = dataset.TribusId,
                    SubtribusId = dataset.SubtribusId,
                    InfratribusId = dataset.InfratribusId,
                    GenusId = dataset.GenusId,
                    PlSpeciesId = dataset.PlSpeciesId,
                    FiSpeciesId = dataset.FiSpeciesId,
                    //Info = CultRes.StringsRes.DatasetNew,
                    Valid = dataset.Valid,
                    ValidYear = dataset.ValidYear,
                    Info = "New",
                    Memo = dataset.Memo
                });
            }

            await Task.CompletedTask;
            return collection;
        }
        return null!;
    }
    #endregion Copy Comment

    #region Delete Comment
    private async Task DeleteTbl93CommentDatasetsFromRegnumId(int regnumId)
    {
        if (Context.Tbl93Comments != null)
        {
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments.Where(x => x.RegnumId == regnumId));
        }

        if (Tbl93CommentsList.Count <= 0)
        {
            return;
        }
        //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("Comment")) return;

        await DeleteDatasetsInTbl93Comments(Tbl93CommentsList);

        //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Comment");
        await Task.CompletedTask;
    }
    public async Task DeleteDatasetsInTbl93Comments(ObservableCollection<Tbl93Comment> tbl93CommentsList)
    {
        foreach (var t in tbl93CommentsList)
        {
            Context.Tbl93Comments?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteConnectedRegnumComments(Tbl03Regnum selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.RegnumId == selected.RegnumId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedPhylumComments(Tbl06Phylum selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.PhylumId == selected.PhylumId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedDivisionComments(Tbl09Division selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.DivisionId == selected.DivisionId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubphylumComments(Tbl12Subphylum selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubphylumId == selected.SubphylumId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubdivisionComments(Tbl15Subdivision selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubdivisionId == selected.SubdivisionId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSuperclassComments(Tbl18Superclass selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SuperclassId == selected.SuperclassId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedClassComments(Tbl21Class selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.ClassId == selected.ClassId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubclassComments(Tbl24Subclass selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubclassId == selected.SubclassId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedInfraclassComments(Tbl27Infraclass selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.InfraclassId == selected.InfraclassId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedLegioComments(Tbl30Legio selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.LegioId == selected.LegioId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedOrdoComments(Tbl33Ordo selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.OrdoId == selected.OrdoId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubordoComments(Tbl36Subordo selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubordoId == selected.SubordoId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedInfraordoComments(Tbl39Infraordo selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.InfraordoId == selected.InfraordoId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSuperfamilyComments(Tbl42Superfamily selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SuperfamilyId == selected.SuperfamilyId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedFamilyComments(Tbl45Family selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.FamilyId == selected.FamilyId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubfamilyComments(Tbl48Subfamily selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubfamilyId == selected.SubfamilyId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedInfrafamilyComments(Tbl51Infrafamily selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.InfrafamilyId == selected.InfrafamilyId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSupertribusComments(Tbl54Supertribus selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SupertribusId == selected.SupertribusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedTribusComments(Tbl57Tribus selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.TribusId == selected.TribusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedSubtribusComments(Tbl60Subtribus selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubtribusId == selected.SubtribusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedInfratribusComments(Tbl63Infratribus selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.InfratribusId == selected.InfratribusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedGenusComments(Tbl66Genus selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.GenusId == selected.GenusId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedFiSpeciesComments(Tbl69FiSpecies selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.FiSpeciesId == selected.FiSpeciesId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }
    public async Task DeleteConnectedPlSpeciesComments(Tbl72PlSpecies selected)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.PlSpeciesId == selected.PlSpeciesId));

            if (collection.Count > 0)
            {
                foreach (var t in collection)
                {
                    Context.Tbl93Comments.Remove(t);
                }

                Context.SaveChanges();
            }
        }

        await Task.CompletedTask;
    }

    //-----------------------------------------------------------------
    public async Task DeleteRegnumComments(int regnumId)
    {
        if (Context.Tbl93Comments != null)
        {
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments.Where(x => x.RegnumId == regnumId));
        }

        if (Tbl93CommentsList.Count <= 0)
        {
            return;
        }
        //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("Comment")) return;

        await DeleteComments(Tbl93CommentsList);

        //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Comment");
        await Task.CompletedTask;
    }
    public async Task DeletePhylumComments(int phylumId)
    {
        if (Context.Tbl93Comments != null)
        {
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments.Where(x => x.PhylumId == phylumId));
        }

        if (Tbl93CommentsList.Count <= 0)
        {
            return;
        }
        //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("Comment")) return;

        await DeleteComments(Tbl93CommentsList);

        //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Comment");
        await Task.CompletedTask;
    }
    public async Task DeleteDivisionComments(int divisionId)
    {
        if (Context.Tbl93Comments != null)
        {
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments.Where(x => x.DivisionId == divisionId));
        }

        if (Tbl93CommentsList.Count <= 0)
        {
            return;
        }
        //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("Comment")) return;

        await DeleteComments(Tbl93CommentsList);

        //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Comment");
        await Task.CompletedTask;
    }
    public async Task DeleteSubphylumComments(int subphylumId)
    {
        if (Context.Tbl93Comments != null)
        {
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments.Where(x => x.SubphylumId == subphylumId));
        }

        if (Tbl93CommentsList.Count <= 0)
        {
            return;
        }
        //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("Comment")) return;

        await DeleteComments(Tbl93CommentsList);

        //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Comment");
        await Task.CompletedTask;
    }
    public async Task DeleteSubdivisionComments(int subdivisionId)
    {
        if (Context.Tbl93Comments != null)
        {
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments.Where(x =>
                    x.SubdivisionId == subdivisionId));
        }

        if (Tbl93CommentsList.Count <= 0)
        {
            return;
        }
        //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("Comment")) return;

        await DeleteComments(Tbl93CommentsList);

        //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Comment");
        await Task.CompletedTask;
    }
    public async Task DeleteSuperclassComments(int superclassId)
    {
        if (Context.Tbl93Comments != null)
        {
            Tbl93CommentsList =
                new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments.Where(x =>
                    x.SuperclassId == superclassId));
        }

        if (Tbl93CommentsList.Count <= 0)
        {
            return;
        }
        //if (_allMessageBoxes.DeleteDatasetQuestionMessageBox("Comment")) return;

        await DeleteComments(Tbl93CommentsList);

        //_allMessageBoxes.InfoMessageBox("DeleteSuccess", "Comment");
        await Task.CompletedTask;
    }
    public Task DeleteTbl93CommentDatasetsFromPhylumId(int phylumId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteComment(Tbl93Comment selected)
    {
        var returnBool = false;

        try
        {
            var dataset = await GetCommentSingleByCommentId(selected.CommentId);
            if (dataset != null)
            {
                await DeleteCommentDataset(dataset);
                returnBool = true;

                if (selected.Info != null)
                {
                    await _allDialogs.InfoSuccessfulDeleteMessageDialogAsync(selected.Info);
                }
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }

        await Task.CompletedTask;
        return returnBool;
    }
    public async Task DeleteCommentDataset(Tbl93Comment selected)
    {
        Context.Tbl93Comments?.Remove(selected);

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    public async Task DeleteComments(ObservableCollection<Tbl93Comment> coll)
    {
        foreach (var t in coll)
        {
            Context.Tbl93Comments?.Remove(t);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    #endregion Delete Comment

    #region Save Comment
    public async Task<bool> SaveComment(Tbl93Comment selected)
    {
        var returnBool = false;

        try
        {
            var dataset = await GetCommentSingleByCommentId(selected.CommentId);

            if (selected.Info != null && !await _allDialogs.SaveDatasetQuestionConfirmationDialogAsync(selected.Info))
            {
                return false;
            }

            if (selected.CommentId == 0)
            {
                dataset = await CommentAdd(selected);
            }
            else
            {
                dataset = await CommentUpdate(dataset, selected);
            }

            try
            {
                await CommentSave(dataset, selected);
                returnBool = true;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null)
                {
                    await _allDialogs.ErrorMessageDialogAsync(e.InnerException.ToString());
                }

                SimpleLog.Log(e);
                return false;
            }
            catch (Exception e)
            {
                await _allDialogs.ErrorMessageDialogAsync(e.Message);
                SimpleLog.Log(e);
                return false;
            }

            if (selected.Info != null)
            {
                await _allDialogs.InfoSuccessfulSaveMessageDialogAsync(selected.Info);
            }
        }
        catch (Exception e)
        {
            await _allDialogs.ErrorMessageDialogAsync(e.Message);
            SimpleLog.Log(e);
        }
        await Task.CompletedTask;
        return returnBool;
    }
    public async Task<Tbl93Comment> CommentUpdate(Tbl93Comment home, Tbl93Comment selected)
    {
        if (true) //update
        {
            home.RegnumId = selected.RegnumId;
            home.PhylumId = selected.PhylumId;
            home.DivisionId = selected.DivisionId;
            home.SubphylumId = selected.SubphylumId;
            home.SubdivisionId = selected.SubdivisionId;
            home.SuperclassId = selected.SuperclassId;
            home.ClassId = selected.ClassId;
            home.SubclassId = selected.SubclassId;
            home.InfraclassId = selected.InfraclassId;
            home.LegioId = selected.LegioId;
            home.OrdoId = selected.OrdoId;
            home.SubordoId = selected.SubordoId;
            home.InfraordoId = selected.InfraordoId;
            home.SuperfamilyId = selected.SuperfamilyId;
            home.FamilyId = selected.FamilyId;
            home.SubfamilyId = selected.SubfamilyId;
            home.InfrafamilyId = selected.InfrafamilyId;
            home.SupertribusId = selected.SupertribusId;
            home.TribusId = selected.TribusId;
            home.SubtribusId = selected.SubtribusId;
            home.InfratribusId = selected.InfratribusId;
            home.GenusId = selected.GenusId;
            home.PlSpeciesId = selected.PlSpeciesId;
            home.FiSpeciesId = selected.FiSpeciesId;
            home.Valid = selected.Valid;
            home.ValidYear = selected.ValidYear;
            home.Info = selected.Info;
            home.Memo = selected.Memo;
            home.Updater = Environment.UserName;
            home.UpdaterDate = DateTime.Now;
        }
        await Task.CompletedTask;
        if (home != null)
        {
            return home;
        }
        return null!;
    }
    public async Task<Tbl93Comment> CommentAdd(Tbl93Comment selected)
    {
        var home = new Tbl93Comment() //add new
        {
            RegnumId = selected.RegnumId,
            PhylumId = selected.PhylumId,
            DivisionId = selected.DivisionId,
            SubphylumId = selected.SubphylumId,
            SubdivisionId = selected.SubdivisionId,
            SuperclassId = selected.SuperclassId,
            ClassId = selected.ClassId,
            SubclassId = selected.SubclassId,
            InfraclassId = selected.InfraclassId,
            LegioId = selected.LegioId,
            OrdoId = selected.OrdoId,
            SubordoId = selected.SubordoId,
            InfraordoId = selected.InfraordoId,
            SuperfamilyId = selected.SuperfamilyId,
            FamilyId = selected.FamilyId,
            SubfamilyId = selected.SubfamilyId,
            InfrafamilyId = selected.InfrafamilyId,
            SupertribusId = selected.SupertribusId,
            TribusId = selected.TribusId,
            SubtribusId = selected.SubtribusId,
            InfratribusId = selected.InfratribusId,
            GenusId = selected.GenusId,
            PlSpeciesId = selected.PlSpeciesId,
            FiSpeciesId = selected.FiSpeciesId,
            CountId = RandomHelper.Randomnumber(),
            Valid = selected.Valid,
            ValidYear = selected.ValidYear,
            Info = selected.Info,
            Memo = selected.Memo,
            Writer = Environment.UserName,
            WriterDate = DateTime.Now,
            Updater = Environment.UserName,
            UpdaterDate = DateTime.Now
        };
        await Task.CompletedTask;
        return home;
    }
    public async Task CommentSave(Tbl93Comment home, Tbl93Comment selected)
    {
        if (selected.CommentId != 0)   //update
        {
            Context.Tbl93Comments?.Update(home);
        }
        else                            //add
        {
            Context.Tbl93Comments?.Add(home);
        }

        Context.SaveChanges();
        await Task.CompletedTask;
    }
    #endregion Save Comment

    #endregion


    #endregion CRUD

    #region Search_Gets
    public ObservableCollection<Tbl03Regnum> GetTbl03RegnumsCollectionOrderByRegnumNameAndSubregnumFromFilterText(string filterText)
    {
        if (Context.Tbl03Regnums != null)
        {
            var collection = new ObservableCollection<Tbl03Regnum>(Context.Tbl03Regnums
                .Where(e => e.RegnumName.StartsWith(filterText) &&
                            e.RegnumName.Contains(Value) == false ||
                            e.Subregnum.Contains(filterText) ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(e => e.RegnumName)
                .ThenBy(e => e.Subregnum));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl06Phylum> GetTbl06PhylumsCollectionOrderByPhylumNameFromFilterText(string filterText)
    {
        if (Context.Tbl06Phylums != null)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(Context.Tbl06Phylums
                .Where(e => e.PhylumName.StartsWith(filterText) &&
                            e.PhylumName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.PhylumName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl09Division> GetTbl09DivisionsCollectionOrderByDivisionNameFromFilterText(string filterText)
    {
        if (Context.Tbl09Divisions != null)
        {
            var collection = new ObservableCollection<Tbl09Division>(Context.Tbl09Divisions
                .Where(e => e.DivisionName.StartsWith(filterText) &&
                            e.DivisionName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.DivisionName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl12Subphylum> GetTbl12SubphylumsCollectionOrderBySubphylumNameFromFilterText(string filterText)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(Context.Tbl12Subphylums
                .Where(e => e.SubphylumName.StartsWith(filterText) &&
                            e.SubphylumName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SubphylumName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl15Subdivision> GetTbl15SubdivisionsCollectionOrderBySubdivisionNameFromFilterText(string filterText)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(Context.Tbl15Subdivisions
                .Where(e => e.SubdivisionName.StartsWith(filterText) &&
                            e.SubdivisionName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SubdivisionName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl18Superclass> GetTbl18SuperclassesCollectionOrderBySuperclassNameFromFilterText(string filterText)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                .Where(e => e.SuperclassName.StartsWith(filterText) &&
                            e.SuperclassName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SuperclassName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl21Class> GetTbl21ClassesCollectionOrderByClassNameFromFilterText(string filterText)
    {
        if (Context.Tbl21Classes != null)
        {
            var collection = new ObservableCollection<Tbl21Class>(Context.Tbl21Classes
                .Where(e => e.ClassName.StartsWith(filterText) &&
                            e.ClassName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.ClassName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl24Subclass> GetTbl24SubclassesCollectionOrderBySubclassNameFromFilterText(string filterText)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(Context.Tbl24Subclasses
                .Where(e => e.SubclassName.StartsWith(filterText) &&
                            e.SubclassName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SubclassName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl27Infraclass> GetTbl27InfraclassesCollectionOrderByInfraclassNameFromFilterText(string filterText)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(Context.Tbl27Infraclasses
                .Where(e => e.InfraclassName.StartsWith(filterText) &&
                            e.InfraclassName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.InfraclassName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl30Legio> GetTbl30LegiosCollectionOrderByLegioNameFromFilterText(string filterText)
    {
        if (Context.Tbl30Legios != null)
        {
            var collection = new ObservableCollection<Tbl30Legio>(Context.Tbl30Legios
                .Where(e => e.LegioName.StartsWith(filterText) &&
                            e.LegioName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.LegioName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl33Ordo> GetTbl33OrdosCollectionOrderByOrdoNameFromFilterText(string filterText)
    {
        if (Context.Tbl33Ordos != null)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(Context.Tbl33Ordos
                .Where(e => e.OrdoName.StartsWith(filterText) &&
                            e.OrdoName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.OrdoName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl36Subordo> GetTbl36SubordosCollectionOrderBySubordoNameFromFilterText(string filterText)
    {
        if (Context.Tbl36Subordos != null)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(Context.Tbl36Subordos
                .Where(e => e.SubordoName.StartsWith(filterText) &&
                            e.SubordoName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SubordoName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl39Infraordo> GetTbl39InfraordosCollectionOrderByInfraordoNameFromFilterText(string filterText)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(Context.Tbl39Infraordos
                .Where(e => e.InfraordoName.StartsWith(filterText) &&
                            e.InfraordoName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.InfraordoName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl42Superfamily> GetTbl42SuperfamiliesCollectionOrderBySuperfamilyNameFromFilterText(string filterText)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(Context.Tbl42Superfamilies
                .Where(e => e.SuperfamilyName.StartsWith(filterText) &&
                            e.SuperfamilyName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SuperfamilyName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl45Family> GetTbl45FamiliesCollectionOrderByFamilyNameFromFilterText(string filterText)
    {
        if (Context.Tbl45Families != null)
        {
            var collection = new ObservableCollection<Tbl45Family>(Context.Tbl45Families
                .Where(e => e.FamilyName.StartsWith(filterText) &&
                            e.FamilyName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.FamilyName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl48Subfamily> GetTbl48SubfamiliesCollectionOrderBySubfamilyNameFromFilterText(string filterText)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var collection = new ObservableCollection<Tbl48Subfamily>(Context.Tbl48Subfamilies
                .Where(e => e.SubfamilyName.StartsWith(filterText) &&
                            e.SubfamilyName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SubfamilyName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl51Infrafamily> GetTbl51InfrafamiliesCollectionOrderByInfrafamilyNameFromFilterText(string filterText)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var collection = new ObservableCollection<Tbl51Infrafamily>(Context.Tbl51Infrafamilies
                .Where(e => e.InfrafamilyName.StartsWith(filterText) &&
                            e.InfrafamilyName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.InfrafamilyName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl54Supertribus> GetTbl54SupertribussesCollectionOrderBySupertribusNameFromFilterText(string filterText)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var collection = new ObservableCollection<Tbl54Supertribus>(Context.Tbl54Supertribusses
                .Where(e => e.SupertribusName.StartsWith(filterText) &&
                            e.SupertribusName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SupertribusName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl57Tribus> GetTbl57TribussesCollectionOrderByTribusNameFromFilterText(string filterText)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var collection = new ObservableCollection<Tbl57Tribus>(Context.Tbl57Tribusses
                .Where(e => e.TribusName.StartsWith(filterText) &&
                            e.TribusName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.TribusName));

            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl60Subtribus> GetTbl60SubtribussesCollectionOrderBySubtribusNameFromFilterText(string filterText)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var collection = new ObservableCollection<Tbl60Subtribus>(Context.Tbl60Subtribusses
                .Where(e => e.SubtribusName.StartsWith(filterText) &&
                            e.SubtribusName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.SubtribusName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl63Infratribus> GetTbl63InfratribussesCollectionOrderByInfratribusNameFromFilterText(string filterText)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var collection = new ObservableCollection<Tbl63Infratribus>(Context.Tbl63Infratribusses
                .Where(e => e.InfratribusName.StartsWith(filterText) &&
                            e.InfratribusName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.InfratribusName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl66Genus> GetTbl66GenussesCollectionOrderByGenusNameFromFilterText(string filterText)
    {
        if (Context.Tbl66Genusses != null)
        {
            var collection = new ObservableCollection<Tbl66Genus>(Context.Tbl66Genusses
                .Where(e => e.GenusName.StartsWith(filterText) &&
                            e.GenusName.Contains(Value) == false ||
                            e.EngName.Contains(filterText) ||
                            e.GerName.Contains(filterText) ||
                            e.FraName.Contains(filterText) ||
                            e.PorName.Contains(filterText)
                )
                .OrderBy(a => a.GenusName));

            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl69FiSpecies>
        GetTbl69FiSpeciessesCollectionOrderByGenusNameAndFiSpeciesNameAndSubspeciesAndDiversFromFilterText(string filterText)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .Include(a => a.Tbl66Genusses)
                .Where(e => e.FiSpeciesName.StartsWith(filterText) &&
                            e.FiSpeciesName.Contains(Value) == false ||
                            e.Tbl66Genusses.GenusName.Contains(filterText) ||
                            e.Subspecies.Contains(filterText) ||
                            e.Divers.Contains(filterText) ||
                            e.TradeName.Contains(filterText) ||
                            e.Author.Contains(filterText)
                )
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.FiSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers)
            );

            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl72PlSpecies> GetTbl72PlSpeciessesCollectionOrderByGenusNameAndPlSpeciesNameAndSubspeciesAndDiversFromFilterText(
        string filterText)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Include(a => a.Tbl66Genusses)
                .Where(e => e.PlSpeciesName.StartsWith(filterText) &&
                            e.PlSpeciesName.Contains(Value) == false ||
                            e.Tbl66Genusses.GenusName.Contains(filterText) ||
                            e.Subspecies.Contains(filterText) ||
                            e.Divers.Contains(filterText) ||
                            e.TradeName.Contains(filterText) ||
                            e.Author.Contains(filterText)
                )
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers)
            );

            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl78Name> GetTbl78NamesCollectionOrderByNameNameFromFilterText(string filterText)
    {
        if (Context.Tbl78Names != null)
        {
            var collection = new ObservableCollection<Tbl78Name>(Context.Tbl78Names
                .Include(a => a.Tbl69FiSpeciesses)
                .Include(a => a.Tbl72PlSpeciesses)
                .Where(e => e.NameName.StartsWith(filterText) &&
                            e.Tbl69FiSpeciesses.FiSpeciesId == e.FiSpeciesId &&
                            e.Tbl72PlSpeciesses.PlSpeciesId == e.PlSpeciesId
                )
                .OrderBy(a => a.NameName));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl84Synonym> GetTbl84SynonymsCollectionOrderBySynonymNameFromFilterText(string filterText)
    {
        if (Context.Tbl84Synonyms != null)
        {
            var collection = new ObservableCollection<Tbl84Synonym>(Context.Tbl84Synonyms
                .Include(a => a.Tbl69FiSpeciesses)
                .Include(a => a.Tbl72PlSpeciesses)
                .Where(e => e.SynonymName.StartsWith(filterText) &&
                            e.Tbl69FiSpeciesses.FiSpeciesId == e.FiSpeciesId &&
                            e.Tbl72PlSpeciesses.PlSpeciesId == e.PlSpeciesId
                )
                .OrderBy(a => a.SynonymName));
            return collection;
        }
        return null!;
    }

    #endregion Search_Gets

    #region Report_Gets

    #region Regnum
    public Tbl03Regnum GetRegnumSingleByRegnumIdAndHash(int id)
    {
        if (Context.Tbl03Regnums != null)
        {
            var single = Context.Tbl03Regnums.SingleOrDefault(a => a.RegnumId == id &&
                                                                  a.RegnumName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl03Regnum> CollRegnumsByRegnumId(int id)
    {
        if (Context.Tbl03Regnums != null)
        {
            var collection = new ObservableCollection<Tbl03Regnum>(Context.Tbl03Regnums
                .Where(e => e.RegnumId == id));
            return collection;
        }
        return null!;
    }
    //direct children
    public ObservableCollection<Tbl06Phylum> CollPhylumsByRegnumIdAndHash(int id)
    {
        if (Context.Tbl06Phylums != null)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(Context.Tbl06Phylums
                .Where(e => e.RegnumId == id &&
                            e.PhylumName.Contains(Value) == false)
                .OrderBy(a => a.PhylumName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl09Division> CollDivisionsByRegnumIdAndHash(int id)
    {
        if (Context.Tbl09Divisions != null)
        {
            var collection = new ObservableCollection<Tbl09Division>(Context.Tbl09Divisions
                .Where(e => e.RegnumId == id &&
                            e.DivisionName.Contains(Value) == false)
                .OrderBy(a => a.DivisionName));
            return collection;
        }
        return null!;
    }
    //References
    public ObservableCollection<Tbl90Reference> CollExpertsByRegnumId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.RegnumId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByRegnumId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.RegnumId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByRegnumId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.RegnumId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    //Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByRegnumId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.RegnumId == id));
            return collection;
        }
        return null!;
    }
    #endregion Regnum

    #region Phylum
    public Tbl06Phylum GetPhylumSingleByPhylumIdAndHash(int id)
    {
        if (Context.Tbl06Phylums != null)
        {
            var single = Context.Tbl06Phylums.SingleOrDefault(a => a.PhylumId == id &&
                                                                  a.PhylumName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl06Phylum> CollPhylumsByPhylumId(int id)
    {
        if (Context.Tbl06Phylums != null)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(Context.Tbl06Phylums
                .Where(e => e.PhylumId == id));
            return collection;
        }
        return null!;
    }
    //direct children
    public ObservableCollection<Tbl12Subphylum> CollSubphylumsByPhylumIdAndHash(int id)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(Context.Tbl12Subphylums
                .Where(e => e.PhylumId == id &&
                            e.SubphylumName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int RegnumIdFromPhylumsCollectionSelect(int id)
    {
        if (Context.Tbl06Phylums != null)
        {
            var dbset = Context.Tbl06Phylums.SingleOrDefault(p => p.PhylumId == id);

            if (dbset != null)
            {
                return dbset.RegnumId;
            }
        }

        return 0;
    }
    //ForeignKey
    public ObservableCollection<Tbl03Regnum> CollRegnumsByRegnumIdAndHash(int id)
    {
        if (Context.Tbl03Regnums != null)
        {
            var collection = new ObservableCollection<Tbl03Regnum>(Context.Tbl03Regnums
                .Where(e => e.RegnumId == id &&
                            e.RegnumName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //References
    public ObservableCollection<Tbl90Reference> CollExpertsByPhylumId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.PhylumId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByPhylumId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.PhylumId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByPhylumId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.PhylumId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    //Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByPhylumId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.PhylumId == id));
            return collection;
        }
        return null!;
    }
    #endregion Phylum

    #region Division
    public Tbl09Division GetDivisionSingleByDivisionIdAndHash(int id)
    {
        if (Context.Tbl09Divisions != null)
        {
            var single = Context.Tbl09Divisions.SingleOrDefault(a => a.DivisionId == id &&
                                                                    a.DivisionName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl09Division> CollDivisionsByDivisionId(int id)
    {
        if (Context.Tbl09Divisions != null)
        {
            var collection = new ObservableCollection<Tbl09Division>(Context.Tbl09Divisions
                .Where(e => e.DivisionId == id));
            return collection;
        }
        return null!;
    }
    //direct children
    public ObservableCollection<Tbl15Subdivision> CollSubdivisionsByDivisionIdAndHash(int id)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(Context.Tbl15Subdivisions
                .Where(e => e.DivisionId == id &&
                            e.SubdivisionName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int RegnumIdFromDivisionsCollectionSelect(int id)
    {
        if (Context.Tbl09Divisions != null)
        {
            var dbset = Context.Tbl09Divisions
                .SingleOrDefault(p => p.DivisionId == id);

            if (dbset != null)
            {
                return dbset.RegnumId;
            }
        }

        return 0;
    }
    //ForeignKey

    //References
    public ObservableCollection<Tbl90Reference> CollExpertsByDivisionId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.DivisionId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByDivisionId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.DivisionId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByDivisionId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.DivisionId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    //Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByDivisionId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.DivisionId == id));
            return collection;
        }
        return null!;
    }
    #endregion Division

    #region Subphylum
    public Tbl12Subphylum GetSubphylumSingleBySubphylumIdAndHash(int id)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var single = Context.Tbl12Subphylums.SingleOrDefault(a => a.SubphylumId == id &&
                                                                     a.SubphylumName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl12Subphylum> CollSubphylumsBySubphylumId(int id)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(Context.Tbl12Subphylums
                .Where(e => e.SubphylumId == id));
            return collection;
        }
        return null!;
    }
    //direct children
    public ObservableCollection<Tbl18Superclass> CollSuperclassesBySubphylumIdAndHash(int id)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                .Where(e => e.SubphylumId == id &&
                            e.SuperclassName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int PhylumIdFromSubphylumsCollectionSelect(int id)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var dbset = Context.Tbl12Subphylums
                .SingleOrDefault(p => p.SubphylumId == id);

            if (dbset != null)
            {
                return dbset.PhylumId;
            }
        }

        return 0;
    }
    //ForeignKey
    public ObservableCollection<Tbl06Phylum> CollPhylumsByPhylumIdAndHash(int id)
    {
        if (Context.Tbl06Phylums != null)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(Context.Tbl06Phylums
                .Where(e => e.PhylumId == id &&
                            e.PhylumName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //References
    public ObservableCollection<Tbl90Reference> CollExpertsBySubphylumId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SubphylumId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesBySubphylumId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SubphylumId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsBySubphylumId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SubphylumId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    //Comments
    public ObservableCollection<Tbl93Comment> CollCommentsBySubphylumId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubphylumId == id));
            return collection;
        }
        return null!;
    }
    #endregion Subphylum

    #region Subdivision

    public Tbl15Subdivision GetSubdivisionSingleBySubdivisionIdAndHash(int id)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var single = Context.Tbl15Subdivisions.SingleOrDefault(a => a.SubdivisionId == id &&
                                                                        a.SubdivisionName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }

        return null!;
    }

    public ObservableCollection<Tbl15Subdivision> CollSubdivisionsBySubdivisionId(int id)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(Context.Tbl15Subdivisions
                .Where(e => e.SubdivisionId == id));
            return collection;
        }
        return null!;
    }
    //direct children
    public ObservableCollection<Tbl18Superclass> CollSuperclassesBySubdivisionIdAndHash(int id)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                .Where(e => e.SubdivisionId == id &&
                            e.SuperclassName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int DivisionIdFromSubdivisionsCollectionSelect(int id)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var dbset = Context.Tbl15Subdivisions
                .SingleOrDefault(p => p.SubdivisionId == id);

            if (dbset == null)
            {
                return 0;
            }

            return dbset.DivisionId;
        }
        return 0;
    }
    //ForeignKey
    public ObservableCollection<Tbl09Division> CollDivisionsByDivisionIdAndHash(int id)
    {
        if (Context.Tbl09Divisions != null)
        {
            var collection = new ObservableCollection<Tbl09Division>(Context.Tbl09Divisions
                .Where(e => e.DivisionId == id &&
                            e.DivisionName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //References
    public ObservableCollection<Tbl90Reference> CollExpertsBySubdivisionId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SubdivisionId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesBySubdivisionId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SubdivisionId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsBySubdivisionId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SubdivisionId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    //Comments
    public ObservableCollection<Tbl93Comment> CollCommentsBySubdivisionId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubdivisionId == id));
            return collection;
        }
        return null!;
    }
    #endregion Subdivision

    #region Superclass
    public Tbl18Superclass GetSuperclassSingleBySuperclassIdAndHash(int id)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var single = Context.Tbl18Superclasses.SingleOrDefault(a => a.SuperclassId == id &&
                                                                      a.SuperclassName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }

        return null!;
    }

    public ObservableCollection<Tbl18Superclass> CollSuperclassesBySuperclassId(int id)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                .Where(e => e.SuperclassId == id));
            return collection;
        }
        return null!;
    }
    //direct children
    public ObservableCollection<Tbl21Class> CollClassesBySuperclassIdAndHash(int id)
    {
        if (Context.Tbl21Classes != null)
        {
            var collection = new ObservableCollection<Tbl21Class>(Context.Tbl21Classes
                .Where(e => e.SuperclassId == id &&
                            e.ClassName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int SubdivisionIdFromSuperclassesCollectionSelect(int id)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var dbset = Context.Tbl18Superclasses
                .SingleOrDefault(p => p.SuperclassId == id);

            if (dbset != null)
            {
                return dbset.SubdivisionId;
            }
        }

        return 0;
    }
    public int SubphylumIdFromSuperclassesCollectionSelect(int id)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var dbset = Context.Tbl18Superclasses
                .SingleOrDefault(p => p.SuperclassId == id);

            if (dbset != null)
            {
                return dbset.SubphylumId;
            }
        }

        return 0;
    }
    //ForeignKey
    public ObservableCollection<Tbl12Subphylum> CollSubphylumsBySubphylumIdAndHash(int id)
    {
        if (Context.Tbl12Subphylums != null)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(Context.Tbl12Subphylums
                .Where(e => e.SubphylumId == id &&
                            e.SubphylumName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl15Subdivision> CollSubdivisionsBySubdivisionIdAndHash(int id)
    {
        if (Context.Tbl15Subdivisions != null)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(Context.Tbl15Subdivisions
                .Where(e => e.SubdivisionId == id &&
                            e.SubdivisionName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //References
    public ObservableCollection<Tbl90Reference> CollExpertsBySuperclassId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SuperclassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesBySuperclassId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SuperclassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsBySuperclassId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SuperclassId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    //Comments
    public ObservableCollection<Tbl93Comment> CollCommentsBySuperclassId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SuperclassId == id));
            return collection;
        }
        return null!;
    }
    #endregion Superclass

    #region Class
    public Tbl21Class GetClassSingleByClassIdAndHash(int id)
    {
        if (Context.Tbl21Classes != null)
        {
            var single = Context.Tbl21Classes.SingleOrDefault(a => a.ClassId == id &&
                                                                 a.ClassName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }

        return null!;
    }

    public ObservableCollection<Tbl21Class> CollClassesByClassId(int id)
    {
        if (Context.Tbl21Classes != null)
        {
            var collection = new ObservableCollection<Tbl21Class>(Context.Tbl21Classes
                .Where(e => e.ClassId == id));
            return collection;
        }
        return null!;
    }
    //direct children
    public ObservableCollection<Tbl24Subclass> CollSubclassesByClassIdAndHash(int id)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(Context.Tbl24Subclasses
                .Where(e => e.ClassId == id &&
                            e.SubclassName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int SuperclassIdFromClassesCollectionSelect(int id)
    {
        if (Context.Tbl21Classes != null)
        {
            var dbset = Context.Tbl21Classes
                .SingleOrDefault(p => p.ClassId == id);

            if (dbset != null)
            {
                return dbset.SuperclassId;
            }
        }

        return 0;
    }
    //ForeignKey
    public ObservableCollection<Tbl18Superclass> CollSuperclassesBySuperclassIdAndHash(int id)
    {
        if (Context.Tbl18Superclasses != null)
        {
            var collection = new ObservableCollection<Tbl18Superclass>(Context.Tbl18Superclasses
                .Where(e => e.SuperclassId == id &&
                            e.SuperclassName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //References
    public ObservableCollection<Tbl90Reference> CollExpertsByClassId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.ClassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByClassId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.ClassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByClassId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.ClassId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    //Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByClassId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.ClassId == id));
            return collection;
        }
        return null!;
    }
    #endregion Class

    #region Subclass
    public Tbl24Subclass GetSubclassSingleBySubclassIdAndHash(int id)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var single = Context.Tbl24Subclasses.SingleOrDefault(a => a.SubclassId == id &&
                                                                      a.SubclassName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl24Subclass> CollSubclassesBySubclassId(int id)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(Context.Tbl24Subclasses
                .Where(e => e.SubclassId == id));
            //var collection = new ObservableCollection<Tbl24Subclass>(_uow.Tbl24Subclasses
            //    .Find(e => e.SubclassId == id));
            return collection;
        }
        return null!;
    }
    //direct children
    public ObservableCollection<Tbl27Infraclass> CollInfraclassesBySubclassIdAndHash(int id)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(Context.Tbl27Infraclasses
                .Where(e => e.SubclassId == id &&
                            e.InfraclassName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl27Infraclass>(_uow.Tbl27Infraclasses
            //    .Find(e => e.SubclassId == id &&
            //               e.InfraclassName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int ClassIdFromSubclassesCollectionSelect(int id)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var dbset = Context.Tbl24Subclasses
                .SingleOrDefault(p => p.SubclassId == id);

            if (dbset != null)
            {
                return dbset.ClassId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl21Class> CollClassesByClassIdAndHash(int id)
    {
        if (Context.Tbl21Classes != null)
        {
            var collection = new ObservableCollection<Tbl21Class>(Context.Tbl21Classes
                .Where(e => e.ClassId == id &&
                            e.ClassName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl21Class>(_uow.Tbl21Classes
            //    .Find(e => e.ClassId == id &&
            //               e.ClassName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    // References
    public ObservableCollection<Tbl90Reference> CollExpertsBySubclassId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SubclassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesBySubclassId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SubclassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsBySubclassId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SubclassId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsBySubclassId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubclassId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.SubclassId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Infraclass
    public Tbl27Infraclass GetInfraclassSingleByInfraclassIdAndHash(int id)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var single = Context.Tbl27Infraclasses.SingleOrDefault(a => a.InfraclassId == id &&
                                                                      a.InfraclassName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl27Infraclass> CollInfraclassesByInfraclassId(int id)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(Context.Tbl27Infraclasses
                .Where(e => e.InfraclassId == id));
            //var collection = new ObservableCollection<Tbl27Infraclass>(_uow.Tbl27Infraclasses
            //    .Find(e => e.InfraclassId == id));
            return collection;
        }
        return null!;
    }
    //direct children
    public ObservableCollection<Tbl30Legio> CollLegiosByInfraclassIdAndHash(int id)
    {
        if (Context.Tbl30Legios != null)
        {
            var collection = new ObservableCollection<Tbl30Legio>(Context.Tbl30Legios
                .Where(e => e.InfraclassId == id &&
                            e.LegioName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl30Legio>(_uow.Tbl30Legios
            //    .Find(e => e.InfraclassId == id &&
            //               e.LegioName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int SubclassIdFromInfraclassesCollectionSelect(int id)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var dbset = Context.Tbl27Infraclasses
                .SingleOrDefault(p => p.InfraclassId == id);

            if (dbset != null)
            {
                return dbset.SubclassId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl24Subclass> CollSubclassesBySubclassIdAndHash(int id)
    {
        if (Context.Tbl24Subclasses != null)
        {
            var collection = new ObservableCollection<Tbl24Subclass>(Context.Tbl24Subclasses
                .Where(e => e.SubclassId == id &&
                            e.SubclassName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl24Subclass>(_uow.Tbl24Subclasses
            //    .Find(e => e.SubclassId == id &&
            //               e.SubclassName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollExpertsByInfraclassId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.InfraclassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByInfraclassId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.InfraclassId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByInfraclassId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.InfraclassId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByInfraclassId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.InfraclassId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.InfraclassId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Legio
    public Tbl30Legio GetLegioSingleByLegioIdAndHash(int id)
    {
        if (Context.Tbl30Legios != null)
        {
            var single = Context.Tbl30Legios.SingleOrDefault(a => a.LegioId == id &&
                                                                 a.LegioName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl30Legio> CollLegiosByLegioId(int id)
    {
        if (Context.Tbl30Legios != null)
        {
            var collection = new ObservableCollection<Tbl30Legio>(Context.Tbl30Legios
                .Where(e => e.LegioId == id));
            //var collection = new ObservableCollection<Tbl30Legio>(_uow.Tbl30Legios
            //    .Find(e => e.LegioId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl33Ordo> CollOrdosByLegioIdAndHash(int id)
    {
        if (Context.Tbl33Ordos != null)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(Context.Tbl33Ordos
                .Where(e => e.LegioId == id &&
                            e.OrdoName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl33Ordo>(_uow.Tbl33Ordos
            //    .Find(e => e.LegioId == id &&
            //               e.OrdoName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int InfraclassIdFromLegiosCollectionSelect(int id)
    {
        if (Context.Tbl30Legios != null)
        {
            var dbset = Context.Tbl30Legios
                .SingleOrDefault(p => p.LegioId == id);

            if (dbset != null)
            {
                return dbset.InfraclassId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl27Infraclass> CollInfraclassesByInfraclassIdAndHash(int id)
    {
        if (Context.Tbl27Infraclasses != null)
        {
            var collection = new ObservableCollection<Tbl27Infraclass>(Context.Tbl27Infraclasses
                .Where(e => e.InfraclassId == id &&
                            e.InfraclassName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl27Infraclass>(_uow.Tbl27Infraclasses
            //    .Find(e => e.InfraclassId == id &&
            //               e.InfraclassName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollExpertsByLegioId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.LegioId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByLegioId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.LegioId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByLegioId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.LegioId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByLegioId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.LegioId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.LegioId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Ordo
    public Tbl33Ordo GetOrdoSingleByOrdoIdAndHash(int id)
    {
        if (Context.Tbl33Ordos != null)
        {
            var single = Context.Tbl33Ordos.SingleOrDefault(a => a.OrdoId == id &&
                                                                a.OrdoName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl33Ordo> CollOrdosByOrdoId(int id)
    {
        if (Context.Tbl33Ordos != null)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(Context.Tbl33Ordos
                .Where(e => e.OrdoId == id));
            //var collection = new ObservableCollection<Tbl33Ordo>(_uow.Tbl33Ordos
            //    .Find(e => e.OrdoId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl36Subordo> CollSubordosByOrdoIdAndHash(int id)
    {
        if (Context.Tbl36Subordos != null)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(Context.Tbl36Subordos
                .Where(e => e.OrdoId == id &&
                            e.SubordoName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl36Subordo>(_uow.Tbl36Subordos
            //    .Find(e => e.OrdoId == id &&
            //               e.SubordoName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int LegioIdFromOrdosCollectionSelect(int id)
    {
        if (Context.Tbl33Ordos != null)
        {
            var dbset = Context.Tbl33Ordos
                .SingleOrDefault(p => p.OrdoId == id);

            if (dbset != null)
            {
                return dbset.LegioId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl30Legio> CollLegiosByLegioIdAndHash(int id)
    {
        if (Context.Tbl30Legios != null)
        {
            var collection = new ObservableCollection<Tbl30Legio>(Context.Tbl30Legios
                .Where(e => e.LegioId == id &&
                            e.LegioName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl30Legio>(_uow.Tbl30Legios
            //    .Find(e => e.LegioId == id &&
            //               e.LegioName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsByOrdoId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.OrdoId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByOrdoId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.OrdoId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByOrdoId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.OrdoId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByOrdoId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.OrdoId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.OrdoId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Subordo
    public Tbl36Subordo GetSubordoSingleBySubordoIdAndHash(int id)
    {
        if (Context.Tbl36Subordos != null)
        {
            var single = Context.Tbl36Subordos.SingleOrDefault(a => a.SubordoId == id &&
                                                                   a.SubordoName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl36Subordo> CollSubordosBySubordoId(int id)
    {
        if (Context.Tbl36Subordos != null)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(Context.Tbl36Subordos
                .Where(e => e.SubordoId == id));
            //var collection = new ObservableCollection<Tbl36Subordo>(_uow.Tbl36Subordos
            //    .Find(e => e.SubordoId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl39Infraordo> CollInfraordosBySubordoIdAndHash(int id)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(Context.Tbl39Infraordos
                .Where(e => e.SubordoId == id &&
                            e.InfraordoName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl39Infraordo>(_uow.Tbl39Infraordos
            //    .Find(e => e.SubordoId == id &&
            //               e.InfraordoName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int OrdoIdFromSubordosCollectionSelect(int id)
    {
        if (Context.Tbl36Subordos != null)
        {
            var dbset = Context.Tbl36Subordos
                .SingleOrDefault(p => p.SubordoId == id);

            if (dbset != null)
            {
                return dbset.OrdoId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl33Ordo> CollOrdosByOrdoIdAndHash(int id)
    {
        if (Context.Tbl33Ordos != null)
        {
            var collection = new ObservableCollection<Tbl33Ordo>(Context.Tbl33Ordos
                .Where(e => e.OrdoId == id &&
                            e.OrdoName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl33Ordo>(_uow.Tbl33Ordos
            //    .Find(e => e.OrdoId == id &&
            //               e.OrdoName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsBySubordoId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SubordoId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesBySubordoId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SubordoId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsBySubordoId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SubordoId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsBySubordoId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubordoId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.SubordoId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Infraordo
    public Tbl39Infraordo GetInfraordoSingleByInfraordoIdAndHash(int id)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var single = Context.Tbl39Infraordos.SingleOrDefault(a => a.InfraordoId == id &&
                                                                     a.InfraordoName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl39Infraordo> CollInfraordosByInfraordoId(int id)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(Context.Tbl39Infraordos
                .Where(e => e.InfraordoId == id));
            //var collection = new ObservableCollection<Tbl39Infraordo>(_uow.Tbl39Infraordos
            //    .Find(e => e.InfraordoId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl42Superfamily> CollSuperfamiliesByInfraordoIdAndHash(int id)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(Context.Tbl42Superfamilies
                .Where(e => e.InfraordoId == id &&
                            e.SuperfamilyName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl42Superfamily>(_uow.Tbl42Superfamilies
            //    .Find(e => e.InfraordoId == id &&
            //               e.SuperfamilyName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int SubordoIdFromInfraordosCollectionSelect(int id)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var dbset = Context.Tbl39Infraordos
                .SingleOrDefault(p => p.InfraordoId == id);

            if (dbset != null)
            {
                return dbset.SubordoId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl36Subordo> CollSubordosBySubordoIdAndHash(int id)
    {
        if (Context.Tbl36Subordos != null)
        {
            var collection = new ObservableCollection<Tbl36Subordo>(Context.Tbl36Subordos
                .Where(e => e.SubordoId == id &&
                            e.SubordoName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl36Subordo>(_uow.Tbl36Subordos
            //    .Find(e => e.SubordoId == id &&
            //               e.SubordoName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsByInfraordoId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.InfraordoId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByInfraordoId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.InfraordoId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByInfraordoId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.InfraordoId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByInfraordoId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.InfraordoId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.InfraordoId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Superfamily
    public Tbl42Superfamily GetSuperfamilySingleBySuperfamilyIdAndHash(int id)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var single = Context.Tbl42Superfamilies.SingleOrDefault(a => a.SuperfamilyId == id &&
                                                                       a.SuperfamilyName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl42Superfamily> CollSuperfamiliesBySuperfamilyId(int id)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(Context.Tbl42Superfamilies
                .Where(e => e.SuperfamilyId == id));
            //var collection = new ObservableCollection<Tbl42Superfamily>(_uow.Tbl42Superfamilies
            //    .Find(e => e.SuperfamilyId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl45Family> CollFamiliesBySuperfamilyIdAndHash(int id)
    {
        if (Context.Tbl45Families != null)
        {
            var collection = new ObservableCollection<Tbl45Family>(Context.Tbl45Families
                .Where(e => e.SuperfamilyId == id &&
                            e.FamilyName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl45Family>(_uow.Tbl45Families
            //    .Find(e => e.SuperfamilyId == id &&
            //               e.FamilyName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int InfraordoIdFromSuperfamiliesCollectionSelect(int id)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var dbset = Context.Tbl42Superfamilies
                .SingleOrDefault(p => p.SuperfamilyId == id);

            if (dbset != null)
            {
                return dbset.InfraordoId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl39Infraordo> CollInfraordosByInfraordoIdAndHash(int id)
    {
        if (Context.Tbl39Infraordos != null)
        {
            var collection = new ObservableCollection<Tbl39Infraordo>(Context.Tbl39Infraordos
                .Where(e => e.InfraordoId == id &&
                            e.InfraordoName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl39Infraordo>(_uow.Tbl39Infraordos
            //    .Find(e => e.InfraordoId == id &&
            //               e.InfraordoName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsBySuperfamilyId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SuperfamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesBySuperfamilyId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SuperfamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsBySuperfamilyId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SuperfamilyId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsBySuperfamilyId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SuperfamilyId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.SuperfamilyId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Family
    public Tbl45Family GetFamilySingleByFamilyIdAndHash(int id)
    {
        if (Context.Tbl45Families != null)
        {
            var single = Context.Tbl45Families.SingleOrDefault(a => a.FamilyId == id &&
                                                                  a.FamilyName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl45Family> CollFamiliesByFamilyId(int id)
    {
        if (Context.Tbl45Families != null)
        {
            var collection = new ObservableCollection<Tbl45Family>(Context.Tbl45Families
                .Where(e => e.FamilyId == id));
            //var collection = new ObservableCollection<Tbl45Family>(_uow.Tbl45Families
            //    .Find(e => e.FamilyId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl48Subfamily> CollSubfamiliesByFamilyIdAndHash(int id)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var collection = new ObservableCollection<Tbl48Subfamily>(Context.Tbl48Subfamilies
                .Where(e => e.FamilyId == id &&
                            e.SubfamilyName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl48Subfamily>(_uow.Tbl48Subfamilies
            //    .Find(e => e.FamilyId == id &&
            //               e.SubfamilyName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int SuperfamilyIdFromFamiliesCollectionSelect(int id)
    {
        if (Context.Tbl45Families != null)
        {
            var dbset = Context.Tbl45Families
                .SingleOrDefault(p => p.FamilyId == id);

            if (dbset != null)
            {
                return dbset.SuperfamilyId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl42Superfamily> CollSuperfamiliesBySuperfamilyIdAndHash(int id)
    {
        if (Context.Tbl42Superfamilies != null)
        {
            var collection = new ObservableCollection<Tbl42Superfamily>(Context.Tbl42Superfamilies
                .Where(e => e.SuperfamilyId == id &&
                            e.SuperfamilyName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl42Superfamily>(_uow.Tbl42Superfamilies
            //    .Find(e => e.SuperfamilyId == id &&
            //               e.SuperfamilyName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsByFamilyId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.FamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByFamilyId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.FamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByFamilyId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.FamilyId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByFamilyId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.FamilyId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.FamilyId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Subfamily
    public Tbl48Subfamily GetSubfamilySingleBySubfamilyIdAndHash(int id)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var single = Context.Tbl48Subfamilies.SingleOrDefault(a => a.SubfamilyId == id &&
                                                                     a.SubfamilyName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl48Subfamily> CollSubfamiliesBySubfamilyId(int id)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var collection = new ObservableCollection<Tbl48Subfamily>(Context.Tbl48Subfamilies
                .Where(e => e.SubfamilyId == id));
            //var collection = new ObservableCollection<Tbl48Subfamily>(_uow.Tbl48Subfamilies
            //    .Find(e => e.SubfamilyId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl51Infrafamily> CollInfrafamiliesBySubfamilyIdAndHash(int id)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var collection = new ObservableCollection<Tbl51Infrafamily>(Context.Tbl51Infrafamilies
                .Where(e => e.SubfamilyId == id &&
                            e.InfrafamilyName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl51Infrafamily>(_uow.Tbl51Infrafamilies
            //    .Find(e => e.SubfamilyId == id &&
            //               e.InfrafamilyName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int FamilyIdFromSubfamiliesCollectionSelect(int id)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var dbset = Context.Tbl48Subfamilies
                .SingleOrDefault(p => p.SubfamilyId == id);

            if (dbset != null)
            {
                return dbset.FamilyId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl45Family> CollFamiliesByFamilyIdAndHash(int id)
    {
        if (Context.Tbl45Families != null)
        {
            var collection = new ObservableCollection<Tbl45Family>(Context.Tbl45Families
                .Where(e => e.FamilyId == id &&
                            e.FamilyName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl45Family>(_uow.Tbl45Families
            //    .Find(e => e.FamilyId == id &&
            //               e.FamilyName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsBySubfamilyId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SubfamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesBySubfamilyId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SubfamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsBySubfamilyId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SubfamilyId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsBySubfamilyId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubfamilyId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.SubfamilyId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Infrafamily
    public Tbl51Infrafamily GetInfrafamilySingleByInfrafamilyIdAndHash(int id)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var single = Context.Tbl51Infrafamilies.SingleOrDefault(a => a.InfrafamilyId == id &&
                                                                       a.InfrafamilyName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl51Infrafamily> CollInfrafamiliesByInfrafamilyId(int id)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var collection = new ObservableCollection<Tbl51Infrafamily>(Context.Tbl51Infrafamilies
                .Where(e => e.InfrafamilyId == id));
            //var collection = new ObservableCollection<Tbl51Infrafamily>(_uow.Tbl51Infrafamilies
            //    .Find(e => e.InfrafamilyId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl54Supertribus> CollSupertribussesByInfrafamilyIdAndHash(int id)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var collection = new ObservableCollection<Tbl54Supertribus>(Context.Tbl54Supertribusses
                .Where(e => e.InfrafamilyId == id &&
                            e.SupertribusName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl54Supertribus>(_uow.Tbl54Supertribusses
            //    .Find(e => e.InfrafamilyId == id &&
            //               e.SupertribusName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int SubfamilyIdFromInfrafamiliesCollectionSelect(int id)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var dbset = Context.Tbl51Infrafamilies
                .SingleOrDefault(p => p.InfrafamilyId == id);

            if (dbset != null)
            {
                return dbset.SubfamilyId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl48Subfamily> CollSubfamiliesBySubfamilyIdAndHash(int id)
    {
        if (Context.Tbl48Subfamilies != null)
        {
            var collection = new ObservableCollection<Tbl48Subfamily>(Context.Tbl48Subfamilies
                .Where(e => e.SubfamilyId == id &&
                            e.SubfamilyName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl48Subfamily>(_uow.Tbl48Subfamilies
            //    .Find(e => e.SubfamilyId == id &&
            //               e.SubfamilyName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsByInfrafamilyId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.InfrafamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByInfrafamilyId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.InfrafamilyId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByInfrafamilyId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.InfrafamilyId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByInfrafamilyId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.InfrafamilyId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.InfrafamilyId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Supertribus
    public Tbl54Supertribus GetSupertribusSingleBySupertribusIdAndHash(int id)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var single = Context.Tbl54Supertribusses.SingleOrDefault(a => a.SupertribusId == id &&
                                                                       a.SupertribusName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl54Supertribus> CollSupertribussesBySupertribusId(int id)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var collection = new ObservableCollection<Tbl54Supertribus>(Context.Tbl54Supertribusses
                .Where(e => e.SupertribusId == id));
            //var collection = new ObservableCollection<Tbl54Supertribus>(_uow.Tbl54Supertribusses
            //    .Find(e => e.SupertribusId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl57Tribus> CollTribussesBySupertribusIdAndHash(int id)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var collection = new ObservableCollection<Tbl57Tribus>(Context.Tbl57Tribusses
                .Where(e => e.SupertribusId == id &&
                            e.TribusName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl57Tribus>(_uow.Tbl57Tribusses
            //    .Find(e => e.SupertribusId == id &&
            //               e.TribusName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int InfrafamilyIdFromSupertribussesCollectionSelect(int id)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var dbset = Context.Tbl54Supertribusses
                .SingleOrDefault(p => p.SupertribusId == id);

            if (dbset != null)
            {
                return dbset.InfrafamilyId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl51Infrafamily> CollInfrafamiliesByInfrafamilyIdAndHash(int id)
    {
        if (Context.Tbl51Infrafamilies != null)
        {
            var collection = new ObservableCollection<Tbl51Infrafamily>(Context.Tbl51Infrafamilies
                .Where(e => e.InfrafamilyId == id &&
                            e.InfrafamilyName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl51Infrafamily>(_uow.Tbl51Infrafamilies
            //    .Find(e => e.InfrafamilyId == id &&
            //               e.InfrafamilyName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsBySupertribusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SupertribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesBySupertribusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SupertribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsBySupertribusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SupertribusId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsBySupertribusId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SupertribusId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.SupertribusId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Tribus
    public Tbl57Tribus GetTribusSingleByTribusIdAndHash(int id)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var single = Context.Tbl57Tribusses.SingleOrDefault(a => a.TribusId == id &&
                                                                  a.TribusName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl57Tribus> CollTribussesByTribusId(int id)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var collection = new ObservableCollection<Tbl57Tribus>(Context.Tbl57Tribusses
                .Where(e => e.TribusId == id));
            //var collection = new ObservableCollection<Tbl57Tribus>(_uow.Tbl57Tribusses
            //    .Find(e => e.TribusId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl60Subtribus> CollSubtribussesByTribusIdAndHash(int id)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var collection = new ObservableCollection<Tbl60Subtribus>(Context.Tbl60Subtribusses
                .Where(e => e.TribusId == id &&
                            e.SubtribusName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl60Subtribus>(_uow.Tbl60Subtribusses
            //    .Find(e => e.TribusId == id &&
            //               e.SubtribusName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int SupertribusIdFromTribussesCollectionSelect(int id)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var dbset = Context.Tbl57Tribusses
                .SingleOrDefault(p => p.TribusId == id);

            if (dbset != null)
            {
                return dbset.SupertribusId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl54Supertribus> CollSupertribussesBySupertribusIdAndHash(int id)
    {
        if (Context.Tbl54Supertribusses != null)
        {
            var collection = new ObservableCollection<Tbl54Supertribus>(Context.Tbl54Supertribusses
                .Where(e => e.SupertribusId == id &&
                            e.SupertribusName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl54Supertribus>(_uow.Tbl54Supertribusses
            //    .Find(e => e.SupertribusId == id &&
            //               e.SupertribusName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsByTribusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.TribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByTribusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.TribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByTribusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.TribusId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByTribusId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.TribusId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.TribusId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Subtribus
    public Tbl60Subtribus GetSubtribusSingleBySubtribusIdAndHash(int id)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var single = Context.Tbl60Subtribusses.SingleOrDefault(a => a.SubtribusId == id &&
                                                                     a.SubtribusName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl60Subtribus> CollSubtribussesBySubtribusId(int id)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var collection = new ObservableCollection<Tbl60Subtribus>(Context.Tbl60Subtribusses
                .Where(e => e.SubtribusId == id));
            //var collection = new ObservableCollection<Tbl60Subtribus>(_uow.Tbl60Subtribusses
            //    .Find(e => e.SubtribusId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl63Infratribus> CollInfratribussesBySubtribusIdAndHash(int id)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var collection = new ObservableCollection<Tbl63Infratribus>(Context.Tbl63Infratribusses
                .Where(e => e.SubtribusId == id &&
                            e.InfratribusName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl63Infratribus>(_uow.Tbl63Infratribusses
            //    .Find(e => e.SubtribusId == id &&
            //               e.InfratribusName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int TribusIdFromSubtribussesCollectionSelect(int id)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var dbset = Context.Tbl60Subtribusses
                .SingleOrDefault(p => p.SubtribusId == id);

            if (dbset != null)
            {
                return dbset.TribusId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl57Tribus> CollTribussesByTribusIdAndHash(int id)
    {
        if (Context.Tbl57Tribusses != null)
        {
            var collection = new ObservableCollection<Tbl57Tribus>(Context.Tbl57Tribusses
                .Where(e => e.TribusId == id &&
                            e.TribusName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl57Tribus>(_uow.Tbl57Tribusses
            //    .Find(e => e.TribusId == id &&
            //               e.TribusName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsBySubtribusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.SubtribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesBySubtribusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.SubtribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsBySubtribusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.SubtribusId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsBySubtribusId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.SubtribusId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.SubtribusId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Infratribus
    public Tbl63Infratribus GetInfratribusSingleByInfratribusIdAndHash(int id)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var single = Context.Tbl63Infratribusses.SingleOrDefault(a => a.InfratribusId == id &&
                                                                       a.InfratribusName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl63Infratribus> CollInfratribussesByInfratribusId(int id)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var collection = new ObservableCollection<Tbl63Infratribus>(Context.Tbl63Infratribusses
                .Where(e => e.InfratribusId == id));
            //var collection = new ObservableCollection<Tbl63Infratribus>(_uow.Tbl63Infratribusses
            //    .Find(e => e.InfratribusId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl66Genus> CollGenussesByInfratribusIdAndHash(int id)
    {
        if (Context.Tbl66Genusses != null)
        {
            var collection = new ObservableCollection<Tbl66Genus>(Context.Tbl66Genusses
                .Where(e => e.InfratribusId == id &&
                            e.GenusName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl66Genus>(_uow.Tbl66Genusses
            //    .Find(e => e.InfratribusId == id &&
            //               e.GenusName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Function
    public int SubtribusIdFromInfratribussesCollectionSelect(int id)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var dbset = Context.Tbl63Infratribusses
                .SingleOrDefault(p => p.InfratribusId == id);

            if (dbset != null)
            {
                return dbset.SubtribusId;
            }
        }

        return 0;
    }
    // ForeignKey
    public ObservableCollection<Tbl60Subtribus> CollSubtribussesBySubtribusIdAndHash(int id)
    {
        if (Context.Tbl60Subtribusses != null)
        {
            var collection = new ObservableCollection<Tbl60Subtribus>(Context.Tbl60Subtribusses
                .Where(e => e.SubtribusId == id &&
                            e.SubtribusName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl60Subtribus>(_uow.Tbl60Subtribusses
            //    .Find(e => e.SubtribusId == id &&
            //               e.SubtribusName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsByInfratribusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.InfratribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByInfratribusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.InfratribusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByInfratribusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.InfratribusId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByInfratribusId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.InfratribusId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.InfratribusId == id));
            return collection;
        }
        return null!;
    }
    #endregion

    #region Genus
    public Tbl66Genus GetGenusSingleByGenusIdAndHash(int id)
    {
        if (Context.Tbl66Genusses != null)
        {
            var single = Context.Tbl66Genusses.SingleOrDefault(a => a.GenusId == id &&
                                                                 a.GenusName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl66Genus> CollGenussesByGenusId(int id)
    {
        if (Context.Tbl66Genusses != null)
        {
            var collection = new ObservableCollection<Tbl66Genus>(Context.Tbl66Genusses
                .Where(e => e.GenusId == id));
            return collection;
        }
        return null!;
    }
    //direct children
    public ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByGenusIdAndHash(int id)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .Where(e => e.GenusId == id &&
                            e.FiSpeciesName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByGenusIdAndSubspeciesIsNullAndHash(int id)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                //        .Include(a => a.Tbl66Genusses)
                .Where(e => e.GenusId == id &&
                            e.Subspecies == null &&
                            e.FiSpeciesName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByGenusIdAndHash(int id)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Where(e => e.GenusId == id &&
                            e.PlSpeciesName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByGenusIdAndSubspeciesIsNullAndHash(int id)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Include(a => a.Tbl66Genusses)
                .Where(e => e.GenusId == id &&
                            e.Subspecies == null &&
                            e.PlSpeciesName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }

    //Function
    public int InfratribusIdFromGenussesCollectionSelect(int id)
    {
        if (Context.Tbl66Genusses != null)
        {
            var dbset = Context.Tbl66Genusses
                .SingleOrDefault(p => p.GenusId == id);

            if (dbset != null)
            {
                return dbset.InfratribusId;
            }
        }

        return 0;
    }
    //ForeignKey
    public ObservableCollection<Tbl63Infratribus> CollInfratribussesByInfratribusIdAndHash(int id)
    {
        if (Context.Tbl63Infratribusses != null)
        {
            var collection = new ObservableCollection<Tbl63Infratribus>(Context.Tbl63Infratribusses
                .Where(e => e.InfratribusId == id &&
                            e.InfratribusName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //References
    public ObservableCollection<Tbl90Reference> CollExpertsByGenusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.GenusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByGenusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.GenusId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByGenusId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.GenusId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    //Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByGenusId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.GenusId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
            //    .Where(e => e.GenusId == id));
            return collection;
        }
        return null!;
    }
    #endregion Genus

    #region FiSpecies
    public Tbl69FiSpecies GetFiSpeciesSingleByFiSpeciesIdAndHash(int id)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var single = Context.Tbl69FiSpeciesses.SingleOrDefault(a => a.FiSpeciesId == id &&
                                                                     a.FiSpeciesName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    //public Tbl69FiSpecies GetFiSpeciesSingleByFiSpeciesId(int id)
    //{
    //    // var single = _uow.Tbl69FiSpeciesses.GetById(id);
    //    Tbl69FiSpecies single = Context.Tbl69FiSpeciesses.FirstOrDefault(a => a.FiSpeciesId == id);
    //    return single;
    //}
    public ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByFiSpeciesId(int id)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .Where(e => e.FiSpeciesId == id));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByFiSpeciesNameAndNotEmptySubspecies(int id, string name)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .Include(d => d.Tbl66Genusses)
                .Where(e => e.GenusId == id &&
                            e.FiSpeciesName == name &&
                            e.Subspecies != null &&
                            e.Subspecies != string.Empty)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.FiSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }

    //FiSpeciessesCollection = new ObservableCollection<Tbl69FiSpecies>(_context.Tbl69FiSpeciesses
    //.Include(a => a.Tbl66Genusses)
    //.Where(e => e.GenusId == id &&
    //e.Subspecies == null &&
    //e.FiSpeciesName.Contains(Value) == false));

    public ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByGenusIdAndFiSpeciesNameAndEmptySubspecies(int id, string name)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .Include(d => d.Tbl66Genusses)
                .Where(e => e.GenusId == id &&
                            e.FiSpeciesName == name &&
                            (e.Subspecies == null || e.Subspecies == string.Empty) &&
                            (e.Divers == null || e.Divers == string.Empty))
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.FiSpeciesName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl69FiSpecies> CollFiSpeciessesByFiSpeciesNameAndSubspeciesAndDivers(string fiSpeciesName, string subspecies,
        string divers)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl69FiSpecies>(Context.Tbl69FiSpeciesses
                .Include(d => d.Tbl66Genusses)
                .Where(e => e.FiSpeciesName == fiSpeciesName && e.Subspecies == subspecies && e.Divers == divers)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.FiSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }
    //direct children
    public ObservableCollection<Tbl78Name> CollNamesByFiSpeciesIdAndHash(int id)
    {
        if (Context.Tbl78Names != null)
        {
            var collection = new ObservableCollection<Tbl78Name>(Context.Tbl78Names
                .Where(e => e.FiSpeciesId == id &&
                            e.NameName.Contains(Value) == false)
                .OrderBy(r => r.NameName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl84Synonym> CollSynonymsByFiSpeciesIdAndHash(int id)
    {
        if (Context.Tbl84Synonyms != null)
        {
            var collection = new ObservableCollection<Tbl84Synonym>(Context.Tbl84Synonyms
                .Where(e => e.FiSpeciesId == id &&
                            e.SynonymName.Contains(Value) == false)
                .OrderBy(r => r.SynonymName));
            return collection;
        }
        return null!;
    }
    //Function
    public int GenusIdFromFiSpeciessesCollectionSelect(int id)
    {
        if (Context.Tbl69FiSpeciesses != null)
        {
            var dbset = Context.Tbl69FiSpeciesses
                .SingleOrDefault(p => p.FiSpeciesId == id);

            if (dbset != null)
            {
                return dbset.GenusId;
            }
        }

        return 0;
    }
    //ForeignKey
    public ObservableCollection<Tbl66Genus> CollGenussesByGenusIdAndHash(int id)
    {
        if (Context.Tbl66Genusses != null)
        {
            var collection = new ObservableCollection<Tbl66Genus>(Context.Tbl66Genusses
                .Where(e => e.GenusId == id &&
                            e.GenusName.Contains(Value) == false));
            //var collection = new ObservableCollection<Tbl66Genus>(_uow.Tbl66Genusses
            //    .Find(e => e.GenusId == id &&
            //               e.GenusName.Contains(Value) == false));
            return collection;
        }
        return null!;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsByFiSpeciesId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.FiSpeciesId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByFiSpeciesId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.FiSpeciesId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByFiSpeciesId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.FiSpeciesId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByFiSpeciesId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.FiSpeciesId == id));
            return collection;
        }
        return null!;
    }
    //Images
    public ObservableCollection<Tbl81Image> CollImagesByFiSpeciesId(int id)
    {
        if (Context.Tbl81Images != null)
        {
            var collection = new ObservableCollection<Tbl81Image>(Context.Tbl81Images
                .Where(e => e.FiSpeciesId == id));
            return collection;
        }
        return null!;
    }
    //Geographics
    public ObservableCollection<Tbl87Geographic> CollGeographicsByFiSpeciesId(int id)
    {
        if (Context.Tbl87Geographics != null)
        {
            var collection = new ObservableCollection<Tbl87Geographic>(Context.Tbl87Geographics
                .Where(e => e.FiSpeciesId == id));
            return collection;
        }
        return null!;
    }


    #endregion

    #region PlSpecies
    public Tbl72PlSpecies GetPlSpeciesSingleByPlSpeciesIdAndHash(int id)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var single = Context.Tbl72PlSpeciesses.SingleOrDefault(a => a.PlSpeciesId == id &&
                                                                     a.PlSpeciesName.Contains(Value) == false);
            if (single != null)
            {
                return single;
            }
        }
        return null!;
    }

    public ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByPlSpeciesId(int id)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Where(e => e.PlSpeciesId == id));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByGenusIdAndPlSpeciesNameAndEmptySubspecies(int id, string name)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Include(d => d.Tbl66Genusses)
                .Where(e => e.GenusId == id &&
                            e.PlSpeciesName == name &&
                            (e.Subspecies == null || e.Subspecies == string.Empty) &&
                            (e.Divers == null || e.Divers == string.Empty)
                )
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByPlSpeciesNameAndNotEmptySubspecies(int id, string name)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Include(d => d.Tbl66Genusses)
                .Where(e => e.GenusId == id &&
                            e.PlSpeciesName == name &&
                            e.Subspecies != null &&
                            e.Subspecies != string.Empty)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }

    public ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByPlSpeciesNameAndNotEmptySubspeciesAndHash(string name)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Include(d => d.Tbl66Genusses)
                .Where(e => e.PlSpeciesName == name &&
                            e.PlSpeciesName.Contains(Value) == false &&
                            e.Subspecies != null)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl72PlSpecies> CollPlSpeciessesByPlSpeciesNameAndSubspeciesAndDivers(string fiSpeciesName, string subspecies,
        string divers)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var collection = new ObservableCollection<Tbl72PlSpecies>(Context.Tbl72PlSpeciesses
                .Include(d => d.Tbl66Genusses)
                .Where(e => e.PlSpeciesName == fiSpeciesName && e.Subspecies == subspecies && e.Divers == divers)
                .OrderBy(a => a.Tbl66Genusses.GenusName)
                .ThenBy(a => a.PlSpeciesName)
                .ThenBy(a => a.Subspecies)
                .ThenBy(a => a.Divers));
            return collection;
        }
        return null!;
    }
    //direct children
    public ObservableCollection<Tbl78Name> CollNamesByPlSpeciesIdAndHash(int id)
    {
        if (Context.Tbl78Names != null)
        {
            var collection = new ObservableCollection<Tbl78Name>(Context.Tbl78Names
                .Where(e => e.PlSpeciesId == id &&
                            e.NameName.Contains(Value) == false)
                .OrderBy(r => r.NameName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl84Synonym> CollSynonymsByPlSpeciesIdAndHash(int id)
    {
        if (Context.Tbl84Synonyms != null)
        {
            var collection = new ObservableCollection<Tbl84Synonym>(Context.Tbl84Synonyms
                .Where(e => e.PlSpeciesId == id &&
                            e.SynonymName.Contains(Value) == false)
                .OrderBy(r => r.SynonymName));
            return collection;
        }
        return null!;
    }
    //Function
    public int GenusIdFromPlSpeciessesCollectionSelect(int id)
    {
        if (Context.Tbl72PlSpeciesses != null)
        {
            var dbset = Context.Tbl72PlSpeciesses
                .SingleOrDefault(p => p.PlSpeciesId == id);

            if (dbset != null)
            {
                return dbset.GenusId;
            }
        }

        return 0;
    }
    //Reference
    public ObservableCollection<Tbl90Reference> CollExpertsByPlSpeciesId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RefExpertId == e.Tbl90RefExperts.RefExpertId &&
                            e.PlSpeciesId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefSourceId.HasValue == false)
                .OrderBy(a => a.Tbl90RefExperts.RefExpertName));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollSourcesByPlSpeciesId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RefSourceId == e.Tbl90RefSources.RefSourceId &&
                            e.PlSpeciesId == id &&
                            e.RefAuthorId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(a => a.Tbl90RefSources.RefSourceName)
                .ThenBy(a => a.Tbl90RefSources.SourceYear));
            return collection;
        }
        return null!;
    }
    public ObservableCollection<Tbl90Reference> CollAuthorsByPlSpeciesId(int id)
    {
        if (Context.Tbl90References != null)
        {
            var collection = new ObservableCollection<Tbl90Reference>(Context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RefAuthorId == e.Tbl90RefAuthors.RefAuthorId &&
                            e.PlSpeciesId == id &&
                            e.RefSourceId.HasValue == false &&
                            e.RefExpertId.HasValue == false)
                .OrderBy(e => e.Tbl90RefAuthors.RefAuthorName)
                .ThenBy(e => e.Tbl90RefAuthors.ArticelTitle)
                .ThenBy(e => e.Tbl90RefAuthors.BookName)
                .ThenBy(e => e.Tbl90RefAuthors.Page1)
                .ThenBy(e => e.Tbl90RefAuthors.Publisher));
            return collection;
        }
        return null!;
    }
    // Comments
    public ObservableCollection<Tbl93Comment> CollCommentsByPlSpeciesId(int id)
    {
        if (Context.Tbl93Comments != null)
        {
            var collection = new ObservableCollection<Tbl93Comment>(Context.Tbl93Comments
                .Where(e => e.PlSpeciesId == id));
            //var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments
            //    .Find(e => e.PlSpeciesId == id));
            return collection;
        }
        return null!;
    }
    //Images
    public ObservableCollection<Tbl81Image> CollImagesByPlSpeciesId(int id)
    {
        if (Context.Tbl81Images != null)
        {
            var collection = new ObservableCollection<Tbl81Image>(Context.Tbl81Images
                .Where(e => e.PlSpeciesId == id));
            //var collection = new ObservableCollection<Tbl81Image>(_uow.Tbl81Images
            //    .Find(e => e.PlSpeciesId == id));
            return collection;
        }
        return null!;
    }
    //Geographics
    public ObservableCollection<Tbl87Geographic> CollGeographicsByPlSpeciesId(int id)
    {
        if (Context.Tbl87Geographics != null)
        {
            var collection = new ObservableCollection<Tbl87Geographic>(Context.Tbl87Geographics
                .Where(e => e.PlSpeciesId == id));
            //var collection = new ObservableCollection<Tbl87Geographic>(_uow.Tbl87Geographics
            //    .Find(e => e.PlSpeciesId == id));
            return collection;
        }
        return null!;
    }

    #endregion

    #endregion

    #region "Public ObservableCollections"

    public string SearchRegnumName
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl09Division> Tbl09DivisionsList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl21Class> Tbl21ClassesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl24Subclass> Tbl24SubclassesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl30Legio> Tbl30LegiosList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl33Ordo> Tbl33OrdosList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl36Subordo> Tbl36SubordosList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl45Family> Tbl45FamiliesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl57Tribus> Tbl57TribussesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl66Genus> Tbl66GenussesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl78Name> Tbl78NamesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl81Image> Tbl81ImagesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl84Synonym> Tbl84SynonymsList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl87Geographic> Tbl87GeographicsList
    {
        get; set;
    } = null!;


    public ObservableCollection<Tbl90Reference> Tbl90ReferencesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl93Comment> Tbl93CommentsList
    {
        get; set;
    } = null!;

    #endregion "Public ObservableCollections"     

    #region "Collections"

    public string FilterText
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl03Regnum> RegnumsCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl06Phylum> PhylumsCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl12Subphylum> SubphylumsCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl09Division> DivisionsCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl15Subdivision> SubdivisionsCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl18Superclass> SuperclassesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl21Class> ClassesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl24Subclass> SubclassesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl27Infraclass> InfraclassesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl30Legio> LegiosCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl33Ordo> OrdosCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl36Subordo> SubordosCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl39Infraordo> InfraordosCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl42Superfamily> SuperfamiliesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl45Family> FamiliesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl48Subfamily> SubfamiliesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl51Infrafamily> InfrafamiliesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl54Supertribus> SupertribussesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl57Tribus> TribussesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl60Subtribus> SubtribussesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl63Infratribus> InfratribussesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl66Genus> GenussesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl69FiSpecies> FiSpeciessesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl72PlSpecies> PlSpeciessesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl78Name> NamesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl84Synonym> SynonymsCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl90Reference> AuthorsCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl90Reference> Tbl90RefAuthorsList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl90Reference> SourcesCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl90Reference> Tbl90RefSourcesList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl90Reference> ExpertsCollection
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl90Reference> Tbl90RefExpertsList
    {
        get; set;
    } = null!;

    public ObservableCollection<Tbl93Comment> CommentsCollection
    {
        get; set;
    } = null!;

    #endregion "Collections"


}
