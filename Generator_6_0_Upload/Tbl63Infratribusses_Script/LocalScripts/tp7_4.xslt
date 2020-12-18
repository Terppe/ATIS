<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[  ]]>
   

<xsl:choose>
<xsl:when test="Table ='++++++Abgeleitet von++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[ 
       #region ]]><xsl:value-of select="Basis"/><![CDATA[
       #region Get ]]><xsl:value-of select="Basis"/><![CDATA[

       // ----------------------------------------   ]]><xsl:value-of select="Basis"/><![CDATA[   ------------------------
        private ObservableCollection<T> Get]]><xsl:value-of select="Basiss"/><![CDATA[CollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.]]><xsl:value-of select="Table"/><![CDATA[
                    .Find(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.]]><xsl:value-of select="Table"/><![CDATA[
                    .Find(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Name.StartsWith(searchName))
                    .OrderBy(a => a.]]><xsl:value-of select="Basis"/><![CDATA[Name)
                    .ThenBy(a => a.Subregnum)
                );
            return collection;
        }

        private ObservableCollection<T> Get]]><xsl:value-of select="Basiss"/><![CDATA[CollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="Table"/><![CDATA[
                .OrderBy(a => a.]]><xsl:value-of select="Basis"/><![CDATA[Name)
                .ThenBy(a => a.Subregnum));
            return collection;
        }
        public ObservableCollection<T> Get]]><xsl:value-of select="Basiss"/><![CDATA[CollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="Table"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="Basis"/><![CDATA[Name)
                .ThenBy(k => k.Subregnum));

            return collection;
        }

                   //-------------------------------------- ]]><xsl:value-of select="BasisTK1"/><![CDATA[   -------------------------
        public ObservableCollection<T> Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="TableTK1"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name));
            return collection;
        }
                   //-------------------------------------- ]]><xsl:value-of select="BasisTK2"/><![CDATA[   -------------------------
        public ObservableCollection<T> Get]]><xsl:value-of select="BasissTK2"/><![CDATA[CollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="TableTK2"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="BasisTK2"/><![CDATA[Name));
            return collection;
        }

                   //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
                   //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
                   //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
                   //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int ]]><xsl:value-of select="Basis"/><![CDATA[IdFrom]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionSelect(int id)
        {
            var ]]><xsl:value-of select="BasissSmTK1"/><![CDATA[coll = _context.]]><xsl:value-of select="TableTK1"/><![CDATA[
                .SingleOrDefault(p => p.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id == id);

            if (]]><xsl:value-of select="BasissSmTK1"/><![CDATA[coll == null) return 0;
            return ]]><xsl:value-of select="BasissSmTK1"/><![CDATA[coll.]]><xsl:value-of select="Basis"/><![CDATA[Id;
        }
        //Function
        public int ]]><xsl:value-of select="Basis"/><![CDATA[IdFrom]]><xsl:value-of select="BasissTK2"/><![CDATA[CollectionSelect(int id)
        {
            var ]]><xsl:value-of select="BasissSmTK2"/><![CDATA[coll = _context.]]><xsl:value-of select="TableTK2"/><![CDATA[
                .SingleOrDefault(p => p.]]><xsl:value-of select="BasisTK2"/><![CDATA[Id == id);

            if (]]><xsl:value-of select="BasissSmTK2"/><![CDATA[coll == null) return 0;
            return ]]><xsl:value-of select="BasissSmTK2"/><![CDATA[coll.]]><xsl:value-of select="Basis"/><![CDATA[Id;
        }

        #endregion 
    
        #region Copy Regnum    
        // ----------------------------------------   Regnum   ------------------------
        public ObservableCollection<Tbl03Regnum> CopyRegnum(Tbl03Regnum selected)
        {
            var dataset = _uow.Tbl03Regnums.GetById(selected.RegnumId);
            var collection = new ObservableCollection<Tbl03Regnum>();

            collection.Insert(0, new Tbl03Regnum
            {
                RegnumName = CultRes.StringsRes.DatasetNew,
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

            return collection;
        }
        // ----------------------------------------   Phylum   ------------------------
        public ObservableCollection<Tbl06Phylum> CopyPhylum(Tbl06Phylum selected)
        {
            var dataset = _uow.Tbl06Phylums.GetById(selected.PhylumId);
            var collection = new ObservableCollection<Tbl06Phylum>();

            collection.Insert(0, new Tbl06Phylum
            {
                PhylumName = CultRes.StringsRes.DatasetNew,
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

            return collection;
        }

        // ----------------------------------------   Division   ------------------------
        public ObservableCollection<Tbl09Division> CopyDivision(Tbl09Division selected)
        {
            var dataset = _uow.Tbl09Divisions.GetById(selected.DivisionId);
            var collection = new ObservableCollection<Tbl09Division>();

            collection.Insert(0, new Tbl09Division
            {
                DivisionName = CultRes.StringsRes.DatasetNew,
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

            return collection;
        }
        // ----------------------------------------   Regnum   ------------------------

        public ObservableCollection<Tbl90Reference> CopyReferenceRegnum(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        RegnumId = dataset.RegnumId,
                        RefExpertId = dataset.RefExpertId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        RegnumId = dataset.RegnumId,
                        RefSourceId = dataset.RefSourceId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        RegnumId = dataset.RegnumId,
                        RefAuthorId = dataset.RefAuthorId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }
            return collection;
        }
        //---------  insert Case in CopyComment
        public ObservableCollection<Tbl93Comment> CopyComment(Tbl93Comment selected, string name)
        {
            var dataset = _uow.Tbl93Comments.GetById(selected.CommentId);
            var collection = new ObservableCollection<Tbl93Comment>();
            switch (name)
            {
                case "]]><xsl:value-of select="Basis"/><![CDATA[":
                    collection.Insert(0, new Tbl93Comment
                    {
                        ]]><xsl:value-of select="Basis"/><![CDATA[Id = dataset.]]><xsl:value-of select="Basis"/><![CDATA[Id,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
              }

            return collection;
        }

        #endregion  
   
        #region Delete Regnum

                //------------------------------ Regnum   --------------------------------------------------------------------------------------------
        public void DeleteRegnum(Tbl03Regnum selected)
        {
            _uow.Tbl03Regnums.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl06Phylum> SearchForConnectedDatasetsWithRegnumIdInTablePhylum(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl06Phylum>(_uow.Tbl06Phylums.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        public ObservableCollection<Tbl09Division> SearchForConnectedDatasetsWithRegnumIdInTableDivision(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl09Division>(_uow.Tbl09Divisions.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithRegnumIdInTableReference(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithRegnumIdInTableComment(Tbl03Regnum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.RegnumId == selected.RegnumId));
            return collection;
        }
        //------------------------------ Phylum   --------------------------------------------------------------------------------------------
        public void DeletePhylum(Tbl06Phylum selected)
        {
            _uow.Tbl06Phylums.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl12Subphylum> SearchForConnectedDatasetsWithPhylumIdInTableSubphylum(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl12Subphylum>(_uow.Tbl12Subphylums.Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithPhylumIdInTableReference(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithPhylumIdInTableComment(Tbl06Phylum selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.PhylumId == selected.PhylumId));
            return collection;
        }
        //------------------------------ Division   --------------------------------------------------------------------------------------------
        public void DeleteDivision(Tbl09Division selected)
        {
            _uow.Tbl09Divisions.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<Tbl15Subdivision> SearchForConnectedDatasetsWithDivisionIdInTableSubdivision(Tbl09Division selected)
        {
            var collection = new ObservableCollection<Tbl15Subdivision>(_uow.Tbl15Subdivisions.Find(x => x.DivisionId == selected.DivisionId));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWithDivisionIdInTableReference(Tbl09Division selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.DivisionId == selected.DivisionId));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWithDivisionIdInTableComment(Tbl09Division selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.DivisionId == selected.DivisionId));
            return collection;
        }
        //-----------------------DeleteReferences, DeleteComments, DeleteReference, DeleteComment-----------------------------------

        public void DeleteReferences(ObservableCollection<Tbl90Reference> coll)
        {
            foreach (var t in coll)
                _uow.Tbl90References.Remove(t);
            _uow.Complete();
        }
        public void DeleteComments(ObservableCollection<Tbl93Comment> coll)
        {
            foreach (var t in coll)
                _uow.Tbl93Comments.Remove(t);
            _uow.Complete();
        }
        public void DeleteReference(Tbl90Reference selected)
        {
            _uow.Tbl90References.Remove(selected);
            _uow.Complete();
        }
        public void DeleteComment(Tbl93Comment selected)
        {
            _uow.Tbl93Comments.Remove(selected);
            _uow.Complete();
        }
        //--------------------------------------------------------------------------------------------------------------------------


        #endregion

        #region Save Regnum

        public Tbl03Regnum RegnumUpdate(Tbl03Regnum home, Tbl03Regnum selected)
        {
            if (home != null) //update
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
            return home;
        }
        public Tbl03Regnum RegnumAdd(Tbl03Regnum selected)
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
            return home;
        }
        public void RegnumSave(Tbl03Regnum home, Tbl03Regnum selected)
        {
            if (selected.RegnumId != 0)   //update
                _uow.Tbl03Regnums.Update(home);
            else                                //add
                _uow.Tbl03Regnums.Add(home);

            _uow.Complete();
        }
        public Tbl90Reference ReferenceExpertRegnumUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.RegnumId = selected.RegnumId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpertRegnumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                RegnumId = selected.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceSourceRegnumUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.RegnumId = selected.RegnumId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSourceRegnumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                RegnumId = selected.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceAuthorRegnumUpdate(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.RegnumId = selected.RegnumId;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthorRegnumAdd(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                RegnumId = selected.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl93Comment CommentRegnumUpdate(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.RegnumId = selected.RegnumId;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment CommentRegnumAdd(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                RegnumId = selected.RegnumId,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return comment;
        }
        //------------------Phylum---------------------------------------
        public Tbl06Phylum PhylumUpdate(Tbl06Phylum home, Tbl06Phylum selected)
        {
            if (home != null) //update
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
            return home;
        }
        public Tbl06Phylum PhylumAdd(Tbl06Phylum selected)
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
            return home;
        }
        public void PhylumSave(Tbl06Phylum home, Tbl06Phylum selected)
        {

            if (selected.PhylumId != 0) //update
            {
                _uow.Tbl06Phylums.Update(home);
            }
            else                                //add
                _uow.Tbl06Phylums.Add(home);
            _uow.Complete();
        }
        //------------------Division---------------------------------------
        public Tbl09Division DivisionUpdate(Tbl09Division home, Tbl09Division selected)
        {
            if (home != null) //update
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
            return home;
        }
        public Tbl09Division DivisionAdd(Tbl09Division selected)
        {
            var res = new Tbl09Division() //add new
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
            return res;
        }
        public void DivisionSave(Tbl09Division home, Tbl09Division selected)
        {
            if (selected.DivisionId != 0) //update
            {
                _uow.Tbl09Divisions.Update(home);
            }
            else                                //add
                _uow.Tbl09Divisions.Add(home);
            _uow.Complete();
        }

        #endregion 
    
        #endregion     

      ]]>
</xsl:when>   
<xsl:otherwise>   <![CDATA[ 
       #region ]]><xsl:value-of select="Basis"/><![CDATA[

         #region Get ]]><xsl:value-of select="Basis"/><![CDATA[

        //----------------------------------------   ]]><xsl:value-of select="Basis"/><![CDATA[   ------------------------
        private ObservableCollection<T> Get]]><xsl:value-of select="Basiss"/><![CDATA[CollectionFromSearchNameOrIdOrderBy<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.]]><xsl:value-of select="Table"/><![CDATA[
                    .Find(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.]]><xsl:value-of select="Table"/><![CDATA[
                    .Find(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Name.StartsWith(searchName))
                    .OrderBy(a => a.]]><xsl:value-of select="Basis"/><![CDATA[Name)
                );
            return collection;
        }

        private ObservableCollection<T> Get]]><xsl:value-of select="Basiss"/><![CDATA[CollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="Table"/><![CDATA[
                .OrderBy(a => a.]]><xsl:value-of select="Basis"/><![CDATA[Name));
            return collection;
        }
        public ObservableCollection<T> Get]]><xsl:value-of select="Basiss"/><![CDATA[CollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="Table"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="Basis"/><![CDATA[Name));

            return collection;
        }

                   //-------------------------------------- ]]><xsl:value-of select="BasisTK1"/><![CDATA[   -------------------------
        public ObservableCollection<T> Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="TableTK1"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name));
            return collection;
        }

                   //-------------------------------------- Reference Experts   -------------------------
        public ObservableCollection<T> GetReferenceExpertsCollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
                   //-------------------------------------- Reference Sources   -------------------------
        public ObservableCollection<T> GetReferenceSourcesCollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
                   //-------------------------------------- Reference Authors   -------------------------
        public ObservableCollection<T> GetReferenceAuthorsCollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
                   //-------------------------------------- Comments   -------------------------
        public ObservableCollection<T> GetCommentsCollectionFrom]]><xsl:value-of select="Basis"/><![CDATA[IdOrderBy<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(e => e.Info));
            return collection;
        }

        //Function
        public int Get]]><xsl:value-of select="Basis"/><![CDATA[IdFrom]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionSelect(int id)
        {
            var ]]><xsl:value-of select="BasissSmTK1"/><![CDATA[coll = _context.]]><xsl:value-of select="TableTK1"/><![CDATA[
                .SingleOrDefault(p => p.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id == id);

            if (]]><xsl:value-of select="BasissSmTK1"/><![CDATA[coll == null) return 0;
            return ]]><xsl:value-of select="BasissSmTK1"/><![CDATA[coll.]]><xsl:value-of select="Basis"/><![CDATA[Id;
        }

          #endregion

         #region Copy ]]><xsl:value-of select="Basis"/><![CDATA[

        // ----------------------------------------   ]]><xsl:value-of select="BasisTK1"/><![CDATA[  ------------------------
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> Copy]]><xsl:value-of select="BasisTK1"/><![CDATA[(]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ selected)  
        {
            var dataset = _uow.]]><xsl:value-of select="TableTK1"/><![CDATA[.GetById(selected.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id);
            var collection = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>();

            collection.Insert(0, new ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[
            {
                ]]><xsl:value-of select="BasisTK1"/><![CDATA[Name = CultRes.StringsRes.DatasetNew,
                ]]><xsl:value-of select="Basis"/><![CDATA[Id = dataset.]]><xsl:value-of select="Basis"/><![CDATA[Id,
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

            return collection;
        }

        public ObservableCollection<Tbl90Reference> CopyReference]]><xsl:value-of select="Basis"/><![CDATA[(Tbl90Reference selected, string refer)
        {
            var dataset = _uow.Tbl90References.GetById(selected.ReferenceId);
            var collection = new ObservableCollection<Tbl90Reference>();
            switch (refer)
            {
                case "Expert":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        ]]><xsl:value-of select="Basis"/><![CDATA[Id = dataset.]]><xsl:value-of select="Basis"/><![CDATA[Id,
                        RefExpertId = dataset.RefExpertId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Source":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        ]]><xsl:value-of select="Basis"/><![CDATA[Id = dataset.]]><xsl:value-of select="Basis"/><![CDATA[Id,
                        RefSourceId = dataset.RefSourceId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
                case "Author":
                    collection.Insert(0, new Tbl90Reference()
                    {
                        ]]><xsl:value-of select="Basis"/><![CDATA[Id = dataset.]]><xsl:value-of select="Basis"/><![CDATA[Id,
                        RefAuthorId = dataset.RefAuthorId,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
            }

            return collection;
        }

        //---------  insert Case in CopyComment  -------------------------
        public ObservableCollection<Tbl93Comment> CopyComment(Tbl93Comment selected, string name)
        {
            var dataset = _uow.Tbl93Comments.GetById(selected.CommentId);
            var collection = new ObservableCollection<Tbl93Comment>();
            switch (name)
            {
                case "]]><xsl:value-of select="Basis"/><![CDATA[":
                    collection.Insert(0, new Tbl93Comment
                    {
                        ]]><xsl:value-of select="Basis"/><![CDATA[Id = dataset.]]><xsl:value-of select="Basis"/><![CDATA[Id,
                        Valid = dataset.Valid,
                        ValidYear = dataset.ValidYear,
                        Info = CultRes.StringsRes.DatasetNew,
                        Memo = dataset.Memo
                    });
                    break;
              }

            return collection;
        }

          #endregion  
   
          #region Delete ]]><xsl:value-of select="Basis"/><![CDATA[

                //------------------------------ ]]><xsl:value-of select="Basis"/><![CDATA[ --------------------------------------------------------------------------------------------
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWith]]><xsl:value-of select="Basis"/><![CDATA[IdInTableReference(]]><xsl:value-of select="LinqModel"/><![CDATA[ selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.]]><xsl:value-of select="Basis"/><![CDATA[Id == selected.]]><xsl:value-of select="Basis"/><![CDATA[Id));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWith]]><xsl:value-of select="Basis"/><![CDATA[IdInTableComment(]]><xsl:value-of select="LinqModel"/><![CDATA[ selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.]]><xsl:value-of select="Basis"/><![CDATA[Id == selected.]]><xsl:value-of select="Basis"/><![CDATA[Id));
            return collection;
        }

                //------------------------------ ]]><xsl:value-of select="BasisTK1"/><![CDATA[ --------------------------------------------------------------------------------------------
        public void Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[(]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ selected)
        {
            _uow.]]><xsl:value-of select="TableTK1"/><![CDATA[.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> SearchForConnectedDatasetsWith]]><xsl:value-of select="BasisTK1"/><![CDATA[IdInTable]]><xsl:value-of select="BasisTK2"/><![CDATA[(]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ selected)
        {
            var collection = new ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>(_uow.]]><xsl:value-of select="TableTK2"/><![CDATA[.Find(x => x.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id == selected.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id));
            return collection;
        }
        public ObservableCollection<Tbl90Reference> DeleteDatasetsWith]]><xsl:value-of select="BasisTK1"/><![CDATA[IdInTableReference(]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ selected)
        {
            var collection = new ObservableCollection<Tbl90Reference>(_uow.Tbl90References.Find(x => x.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id == selected.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id));
            return collection;
        }
        public ObservableCollection<Tbl93Comment> DeleteDatasetsWith]]><xsl:value-of select="BasisTK1"/><![CDATA[IdInTableComment(]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ selected)
        {
            var collection = new ObservableCollection<Tbl93Comment>(_uow.Tbl93Comments.Find(x => x.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id == selected.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id));
            return collection;
        }


        #endregion

        #region Save ]]><xsl:value-of select="Basis"/><![CDATA[ 

        //------------------ ]]><xsl:value-of select="BasisTK1"/><![CDATA[ ---------------------------------------
        public ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[  ]]><xsl:value-of select="BasisTK1"/><![CDATA[Update(]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ home, ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ selected)
        {
            if (home != null) //update
            {
                home.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name = selected.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name;
                home.]]><xsl:value-of select="Basis"/><![CDATA[Id = selected.]]><xsl:value-of select="Basis"/><![CDATA[Id;
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
            return home;
        }
        public ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ ]]><xsl:value-of select="BasisTK1"/><![CDATA[Add(]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ selected)
        {
            var home = new ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[() //add new
            {
                ]]><xsl:value-of select="BasisTK1"/><![CDATA[Name = selected.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name,
                ]]><xsl:value-of select="Basis"/><![CDATA[Id = selected.]]><xsl:value-of select="Basis"/><![CDATA[Id,
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
            return home;
        }
        public void ]]><xsl:value-of select="BasisTK1"/><![CDATA[Save(]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ home, ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ selected)
        {

            if (selected.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id != 0) //update
            {
                _uow.]]><xsl:value-of select="TableTK1"/><![CDATA[.Update(home);
            }
            else                                //add
                _uow.]]><xsl:value-of select="TableTK1"/><![CDATA[.Add(home);
            _uow.Complete();
        }

        public Tbl90Reference ReferenceExpert]]><xsl:value-of select="Basis"/><![CDATA[Update(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefExpertId = selected.RefExpertId;
                reference.]]><xsl:value-of select="Basis"/><![CDATA[Id = selected.]]><xsl:value-of select="Basis"/><![CDATA[Id;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceExpert]]><xsl:value-of select="Basis"/><![CDATA[Add(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefExpertId = selected.RefExpertId,
                ]]><xsl:value-of select="Basis"/><![CDATA[Id = selected.]]><xsl:value-of select="Basis"/><![CDATA[Id,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceSource]]><xsl:value-of select="Basis"/><![CDATA[Update(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefSourceId = selected.RefSourceId;
                reference.]]><xsl:value-of select="Basis"/><![CDATA[Id = selected.]]><xsl:value-of select="Basis"/><![CDATA[Id;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceSource]]><xsl:value-of select="Basis"/><![CDATA[Add(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefSourceId = selected.RefSourceId,
                ]]><xsl:value-of select="Basis"/><![CDATA[Id = selected.]]><xsl:value-of select="Basis"/><![CDATA[Id,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl90Reference ReferenceAuthor]]><xsl:value-of select="Basis"/><![CDATA[Update(Tbl90Reference reference, Tbl90Reference selected)
        {
            if (reference != null) //update
            {
                reference.RefAuthorId = selected.RefAuthorId;
                reference.]]><xsl:value-of select="Basis"/><![CDATA[Id = selected.]]><xsl:value-of select="Basis"/><![CDATA[Id;
                reference.Valid = selected.Valid;
                reference.ValidYear = selected.ValidYear;
                reference.Info = selected.Info;
                reference.Updater = Environment.UserName;
                reference.UpdaterDate = DateTime.Now;
                reference.Memo = selected.Memo;
            }
            return reference;
        }
        public Tbl90Reference ReferenceAuthor]]><xsl:value-of select="Basis"/><![CDATA[Add(Tbl90Reference selected)
        {
            var reference = new Tbl90Reference //add new
            {
                RefAuthorId = selected.RefAuthorId,
                ]]><xsl:value-of select="Basis"/><![CDATA[Id = selected.]]><xsl:value-of select="Basis"/><![CDATA[Id,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return reference;
        }
        public Tbl93Comment Comment]]><xsl:value-of select="Basis"/><![CDATA[Update(Tbl93Comment comment, Tbl93Comment selected)
        {
            if (comment != null) //update
            {
                comment.]]><xsl:value-of select="Basis"/><![CDATA[Id = selected.]]><xsl:value-of select="Basis"/><![CDATA[Id;
                comment.Valid = selected.Valid;
                comment.ValidYear = selected.ValidYear;
                comment.Info = selected.Info;
                comment.Updater = Environment.UserName;
                comment.UpdaterDate = DateTime.Now;
                comment.Memo = selected.Memo;
            }
            return comment;
        }
        public Tbl93Comment Comment]]><xsl:value-of select="Basis"/><![CDATA[Add(Tbl93Comment selected)
        {
            var comment = new Tbl93Comment //add new
            {
                ]]><xsl:value-of select="Basis"/><![CDATA[Id = selected.]]><xsl:value-of select="Basis"/><![CDATA[Id,
                CountId = RandomHelper.Randomnumber(),
                Valid = selected.Valid,
                ValidYear = selected.ValidYear,
                Info = selected.Info,
                Memo = selected.Memo,
                Writer = Environment.UserName,
                WriterDate = DateTime.Now,
                Updater = Environment.UserName,
                UpdaterDate = DateTime.Now,
            };
            return comment;
        }

          #endregion          

        #endregion
   ]]>
</xsl:otherwise>    
</xsl:choose> 




</xsl:template>
</xsl:stylesheet>











