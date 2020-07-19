using System;

namespace ATIS.Ui.Core.Interfaces_UOW
{
    public interface IUnitOfWork : IDisposable
    {
        ITbl03RegnumRepository Tbl03Regnums { get; }
        ITbl06PhylumRepository Tbl06Phylums { get; }
        ITbl09DivisionRepository Tbl09Divisions { get; }


        ITbl90ReferenceRepository Tbl90References { get; }
        ITbl90RefExpertRepository Tbl90RefExperts { get; }
        ITbl90RefSourceRepository Tbl90RefSources { get; }
        ITbl90RefAuthorRepository Tbl90RefAuthors { get; }
        ITbl93CommentRepository Tbl93Comments { get; }

        int Complete();
    }
}
