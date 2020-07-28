using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Converters;
using ATIS.Dal.Models;
using ATIS.Ui.Core.Interfaces_UOW;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace ATIS.Ui.Core.Repositories_UOW
{
    public class Tbl90RefAuthorRepository : Repository<Tbl90RefAuthor>, ITbl90RefAuthorRepository
    {
        private readonly AtisDbContext _atisDbContext;

        private List<string> _authorsCollection = new List<string>();

        public Tbl90RefAuthorRepository(AtisDbContext context) : base(context)
        {
            _atisDbContext = context;
        }


        public IEnumerable<Tbl03Regnum> ListTbl03RegnumsByFilterTextAboutAllFields(string filterText)
        {
            return _atisDbContext.Tbl03Regnums
                .Where(
                    e => e.RegnumName.StartsWith(filterText) &&
                         e.RegnumName.Contains("#") == false ||
                         e.Subregnum.Contains(filterText) ||
                         e.EngName.Contains(filterText) ||
                         e.GerName.Contains(filterText) ||
                         e.FraName.Contains(filterText) ||
                         e.PorName.Contains(filterText))
                .OrderBy(r => r.RegnumName + r.Subregnum).ToList();
            //   p => p.Tbl06Phylums, k => k.Tbl09Divisions, r => r.Tbl90References, s => s.Tbl93Comments);
        }



        private void GetAuthorsCombo(string refAuthorName, string articelTitle, string bookName, string page1,
            string publisher, string publicationPlace)
        {
            var authorsCollection = new ObservableCollection<Tbl90RefAuthor>();

            var query = (from authors in _atisDbContext.Tbl90RefAuthors
                         where authors.RefAuthorName == refAuthorName &&
                               authors.ArticelTitle == articelTitle &&
                               authors.BookName == bookName &&
                               authors.Page1 == page1 &&
                               authors.Publisher == publisher &&
                               authors.PublicationPlace == publicationPlace
                         select authors).ToList();

            authorsCollection.Clear();
            foreach (Tbl90RefAuthor authors in query)
            {
                if (authors != null) authorsCollection.Add(authors);
            }
        }




        public IEnumerable<Tbl90RefAuthor> ListTbl90RefAuthorsToCombobox()    //change to select new
        {
            return _atisDbContext.Tbl90RefAuthors.OrderBy(x => x.RefAuthorName).ToList();
        }


        //public IList<Tbl90RefAuthor> ListTbl90RefAuthorsByRefAuthorNameAndArticelTitleAndBookNameAndPage1AndPublisherAndPublicationPlace(
        //    string refAuthorName, string articelTitle, string bookName, string page1, string publisher, string publicationPlace)
        //{
        //    return _tbl90RefAuthorsRepository.ListWhereOrderByInclude(
        //        e => e.RefAuthorName == refAuthorName &&
        //             e.ArticelTitle == articelTitle &&
        //             e.BookName == bookName &&
        //             e.Page1 == page1 &&
        //             e.Publisher == publisher &&
        //             e.PublicationPlace == publicationPlace,
        //        _tbl90RefAuthorsRepository.OrderBy(r => r.RefAuthorName),
        //        p => p.Tbl90References);
        //}

    }

}
