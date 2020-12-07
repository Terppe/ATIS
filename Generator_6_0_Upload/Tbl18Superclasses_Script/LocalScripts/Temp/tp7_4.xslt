<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[  ]]>
   

<xsl:choose>
<xsl:when test="Table ='++++++Abgeleitet von++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>   <![CDATA[ 

        ----------------------------------------   ]]><xsl:value-of select="BasisTK1"/><![CDATA[   ------------------------


        //------------------------------Delete ]]><xsl:value-of select="BasisTK1"/><![CDATA[ -----------------------------------
        public void Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[(]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ selected)
        {
            _uow.]]><xsl:value-of select="TableTK1"/><![CDATA[.Remove(selected);
            _uow.Complete();
        }
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> SearchForConnectedDatasetsWith]]><xsl:value-of select="BasisTK1"/><![CDATA[IdInTable]]><xsl:value-of select="BasisTK2"/><![CDATA[(]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ selected)
        {
            var collection = new ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>(_uow.]]><xsl:value-of select="TableTK2"/><![CDATA[.Find(x => x.]]><xsl:value-of select="BasisTK2"/><![CDATA[Id == selected.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id));
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

          //------------------------------Get ]]><xsl:value-of select="BasisFK1"/><![CDATA[ -----------------------------------
      public ObservableCollection<T> Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="TableFK1"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="BasisFK1"/><![CDATA[Name));
            return collection;
        }

        //------------------------------Get ]]><xsl:value-of select="BasisTK1"/><![CDATA[ -----------------------------------
        public ObservableCollection<T> Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="TableTK1"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name));
            return collection;
        }

        //-------------------------------Get ]]><xsl:value-of select="Basis"/><![CDATA[  -------------------------
        private ObservableCollection<T> Get]]><xsl:value-of select="Basiss"/><![CDATA[CollectionAllOrderBy<T>()
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="Table"/><![CDATA[
                .OrderBy(a => a.]]><xsl:value-of select="Basis"/><![CDATA[Name));
            return collection;
        }
        private ObservableCollection<T> Get]]><xsl:value-of select="Basiss"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[NameStartsWithOr]]><xsl:value-of select="Basis"/><![CDATA[Id<T>(string searchName)
        {
            ObservableCollection<T> collection;
            collection = int.TryParse(searchName, out var id)
                ? new ObservableCollection<T>((IEnumerable<T>)_uow.]]><xsl:value-of select="Table"/><![CDATA[
                    .Find(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id))
                : new ObservableCollection<T>((IEnumerable<T>)_uow.]]><xsl:value-of select="Table"/><![CDATA[.Find(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Name.StartsWith(searchName)));
            return collection;
        }
        public ObservableCollection<T> GetReferenceAuthorsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefSourceIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceSourcesCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetReferenceExpertsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefSourceIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }
        public ObservableCollection<T> GetCommentsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<T>(int? id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.Info));
            return collection;
        }

         //------------------------Save ]]><xsl:value-of select="BasisTK1"/><![CDATA[ ------------------------------------
        public ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[      ]]><xsl:value-of select="BasisTK1"/><![CDATA[Update(]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ home, ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ selected)
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
        public ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[       ]]><xsl:value-of select="BasisTK1"/><![CDATA[Add(]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ selected)
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
            if (selected.]]><xsl:value-of select="Basis"/><![CDATA[Id != 0) //update
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

        //--------------------------Copy ]]><xsl:value-of select="Basis"/><![CDATA[ --------------------------------

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

        //----------------------------------------------------------
        //----------------------------------------------------------
        //--------------------------search ]]><xsl:value-of select="BasisTK1"/><![CDATA[ --------------------------------

        public ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> SearchForConnectedDatasetsWith]]><xsl:value-of select="Basis"/><![CDATA[IdInTable]]><xsl:value-of select="BasisTK1"/><![CDATA[(]]><xsl:value-of select="LinqModel"/><![CDATA[ selected)
        {
            var collection = new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(_uow.]]><xsl:value-of select="TableTK1"/><![CDATA[.Find(x => x.]]><xsl:value-of select="Basis"/><![CDATA[Id == selected.]]><xsl:value-of select="Basis"/><![CDATA[Id));
            return collection;
        }

        //--------------------------Delete Dataset ]]><xsl:value-of select="Basis"/><![CDATA[ --------------------------------

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

        public void Delete]]><xsl:value-of select="Basis"/><![CDATA[(]]><xsl:value-of select="LinqModel"/><![CDATA[ selected)
        {
            _uow.]]><xsl:value-of select="Table"/><![CDATA[.Remove(selected);
            _uow.Complete();
        }

        //--------------------------Get  ]]><xsl:value-of select="BasisFK1"/><![CDATA[ --------------------------------

        public ObservableCollection<T> Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="TableFK1"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="BasisFK1"/><![CDATA[Name));

            return collection;
        }

        //--------------------------Get  ]]><xsl:value-of select="Basis"/><![CDATA[ --------------------------------

        public ObservableCollection<T> Get]]><xsl:value-of select="Basiss"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="Table"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="Basis"/><![CDATA[Name));
            return collection;
        }

        //--------------------------Get  ]]><xsl:value-of select="BasisTK1"/><![CDATA[ --------------------------------

        public ObservableCollection<T> Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFromSubphylumId<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.]]><xsl:value-of select="TableTK1"/><![CDATA[
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name));
            return collection;
        }

        //--------------------------Get Reference  ]]><xsl:value-of select="Basis"/><![CDATA[ --------------------------------

        public ObservableCollection<T> GetReferenceExpertsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefSourceIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));
            return collection;
        }

        public ObservableCollection<T> GetReferenceSourcesCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefExpertIdIsNull<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }

        public ObservableCollection<T> GetReferenceAuthorsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefSourceIdIsNullAndRefExpertIdIsNull<T>(int id)   
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));
            return collection;
        }

        //--------------------------Get Comment  ]]><xsl:value-of select="Basis"/><![CDATA[ --------------------------------

        public ObservableCollection<T> GetCommentsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<T>(int id)
        {
            ObservableCollection<T> collection;
            collection = new ObservableCollection<T>((IEnumerable<T>)_context.Tbl93Comments
                .Where(e => e.]]><xsl:value-of select="Basis"/><![CDATA[Id == id)
                .OrderBy(k => k.Info));
            return collection;
        }

                 ------- ControlTemplateButtons---------------
    <!--  Subclass  -->
    <ControlTemplate x:Key="Search]]><xsl:value-of select="BasissTK1"/><![CDATA[Button" TargetType="Button">   
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding Get]]><xsl:value-of select="BasissTK1"/><![CDATA[ByNameOrIdCommand}"
            Foreground="{DynamicResource MahApps.Brushes.Accent}"
            IsEnabled="True"
            Style="{StaticResource MahApps.Styles.Button.Circle}">
            <Button.ContentTemplate>
                <DataTemplate>
                    <iconPacks:PackIconModern
                        Width="20"
                        Height="20"
                        Kind="Loop" />
                </DataTemplate>
            </Button.ContentTemplate>
        </Button>
    </ControlTemplate>

    <ControlTemplate x:Key="Add]]><xsl:value-of select="BasissTK1"/><![CDATA[Button" TargetType="Button">
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding Add]]><xsl:value-of select="BasissTK1"/><![CDATA[Command}"
            Foreground="{DynamicResource MahApps.Brushes.Accent}"
            IsEnabled="True"
            Style="{StaticResource MahApps.Styles.Button.Circle}">
            <Button.ContentTemplate>
                <DataTemplate>
                    <iconPacks:PackIconModern
                        Width="20"
                        Height="20"
                        Kind="Add" />
                </DataTemplate>
            </Button.ContentTemplate>
        </Button>
    </ControlTemplate>
    <ControlTemplate x:Key="Copy]]><xsl:value-of select="BasissTK1"/><![CDATA[Button" TargetType="Button">
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding Copy]]><xsl:value-of select="BasissTK1"/><![CDATA[Command}"
            Foreground="{DynamicResource MahApps.Brushes.Accent}"
            IsEnabled="True"
            Style="{StaticResource MahApps.Styles.Button.Circle}">
            <Button.ContentTemplate>
                <DataTemplate>
                    <iconPacks:PackIconModern
                        Width="20"
                        Height="20"
                        Kind="PageCopy" />
                </DataTemplate>
            </Button.ContentTemplate>
        </Button>
    </ControlTemplate>
    <ControlTemplate x:Key="Save]]><xsl:value-of select="BasissTK1"/><![CDATA[Button" TargetType="Button">
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding SaveSubclassCommand}"
            Foreground="{DynamicResource MahApps.Brushes.Accent}"
            IsEnabled="True"
            Style="{StaticResource MahApps.Styles.Button.Circle}">
            <Button.ContentTemplate>
                <DataTemplate>
                    <iconPacks:PackIconModern
                        Width="20"
                        Height="20"
                        Kind="Save" />
                </DataTemplate>
            </Button.ContentTemplate>
        </Button>
    </ControlTemplate>
    <ControlTemplate x:Key="Delete]]><xsl:value-of select="BasissTK1"/><![CDATA[Button" TargetType="Button">
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding DeleteSubclassCommand}"
            Foreground="{DynamicResource MahApps.Brushes.Accent}"
            IsEnabled="True"
            Style="{StaticResource MahApps.Styles.Button.Circle}">
            <Button.ContentTemplate>
                <DataTemplate>
                    <iconPacks:PackIconModern
                        Width="20"
                        Height="20"
                        Kind="Delete" />
                </DataTemplate>
            </Button.ContentTemplate>
        </Button>
    </ControlTemplate>

   ]]>
</xsl:otherwise>    
</xsl:choose> 




</xsl:template>
</xsl:stylesheet>










