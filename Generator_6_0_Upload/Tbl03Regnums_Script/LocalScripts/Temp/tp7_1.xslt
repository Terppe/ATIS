<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition"><![CDATA[using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Common.Logging;
using ATIS.Dal.Models;
using ATIS.Ui.Core;
using ATIS.Ui.Helper;
using ATIS.Ui.Views.Database.CrudHelper;
using ATIS.Ui.Views.Database.DatabaseHelper;
using Microsoft.EntityFrameworkCore;    ]]>      

<xsl:choose>
<xsl:when test="Table ='Header++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">   <![CDATA[ 
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">   <![CDATA[ 
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;  ]]>
</xsl:when> 
<xsl:when test="Table ='Tbl78Names'">   <![CDATA[ 
using System.Collections.Generic;  ]]>
</xsl:when> 
<xsl:when test="Table ='Tbl81Images'">   <![CDATA[ 
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.Command;
using Tyrrrz.Extensions;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.ClosedCaptions;
using YoutubeExplode.Models.MediaStreams;
using RelayCommand = Te.Atis.Ui.Desktop.Domain.RelayCommand; ]]>
</xsl:when> 
<xsl:when test="Table ='Tbl87Geographics'">   <![CDATA[ 
using System.Globalization;
using System.Collections.Generic; 
using RelayCommand = Te.Atis.Ui.Desktop.Domain.RelayCommand;  ]]>
</xsl:when>  
<xsl:when test="Table ='TblUserProfiles'">   <![CDATA[ 
using System.Collections.Generic;  ]]>
</xsl:when> 
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='namespace++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>   <![CDATA[ 
         //    ]]><xsl:value-of select="Basiss"/><![CDATA[ViewModel Skriptdatum: ]]> <xsl:value-of select="DateTime"/>  <![CDATA[  

namespace ATIS.Ui.Views.Database.]]><xsl:value-of select="Layout"/><![CDATA[
{  ]]>   
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Abgeleitet von++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>   <![CDATA[ 
    public class ]]><xsl:value-of select="Basiss"/><![CDATA[ViewModel : ViewModelBase                     
    {  
        // Version with Generic Unit Of Work and AtisDbContext for general use   ]]>
</xsl:otherwise>    
</xsl:choose> 
   
<xsl:choose>
<xsl:when test="Table ='Data Members Top+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">      <![CDATA[ 
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<]]><xsl:value-of select="LinqModel"/><![CDATA[> _gen]]><xsl:value-of select="Basis"/><![CDATA[MessageBoxes = new GenericMessageBoxes<]]><xsl:value-of select="LinqModel"/><![CDATA[>();
        private readonly GenericMessageBoxes<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> _gen]]><xsl:value-of select="BasisTK1"/><![CDATA[MessageBoxes = new GenericMessageBoxes<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>();
        private readonly GenericMessageBoxes<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> _gen]]><xsl:value-of select="BasisTK2"/><![CDATA[MessageBoxes = new GenericMessageBoxes<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private readonly BasicGet _extGet = new BasicGet();
        private readonly BasicCopy _extCopy = new BasicCopy();
        private readonly BasicDelete _extDelete = new BasicDelete();
        private readonly BasicSave _extSave = new BasicSave();        
        private int _position;  ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">   <![CDATA[ 
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<]]><xsl:value-of select="LinqModel"/><![CDATA[> _gen]]><xsl:value-of select="Basis"/><![CDATA[MessageBoxes = new GenericMessageBoxes<]]><xsl:value-of select="LinqModel"/><![CDATA[>();
        private readonly GenericMessageBoxes<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> _gen]]><xsl:value-of select="BasisFK1"/><![CDATA[MessageBoxes = new GenericMessageBoxes<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>();
        private readonly GenericMessageBoxes<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> _gen]]><xsl:value-of select="BasisFK2"/><![CDATA[MessageBoxes = new GenericMessageBoxes<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[>();
        private readonly GenericMessageBoxes<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> _gen]]><xsl:value-of select="BasisTK1"/><![CDATA[MessageBoxes = new GenericMessageBoxes<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private readonly BasicGet _extGet = new BasicGet();
        private readonly BasicCopy _extCopy = new BasicCopy();
        private readonly BasicDelete _extDelete = new BasicDelete();
        private readonly BasicSave _extSave = new BasicSave();        
        private int _position;  ]]> 
</xsl:when> 
<xsl:otherwise>        <![CDATA[ 
        #region [Private Data Members]
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private readonly GenericMessageBoxes<]]><xsl:value-of select="LinqModel"/><![CDATA[> _gen]]><xsl:value-of select="Basis"/><![CDATA[MessageBoxes = new GenericMessageBoxes<]]><xsl:value-of select="LinqModel"/><![CDATA[>();
        private readonly GenericMessageBoxes<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> _gen]]><xsl:value-of select="BasisFK1"/><![CDATA[MessageBoxes = new GenericMessageBoxes<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>();
        private readonly GenericMessageBoxes<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> _gen]]><xsl:value-of select="BasisTK1"/><![CDATA[MessageBoxes = new GenericMessageBoxes<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genExpertMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genSourceMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl90Reference> _genAuthorMessageBoxes = new GenericMessageBoxes<Tbl90Reference>();
        private readonly GenericMessageBoxes<Tbl93Comment> _genCommentMessageBoxes = new GenericMessageBoxes<Tbl93Comment>();
        private readonly BasicGet _extGet = new BasicGet();
        private readonly BasicCopy _extCopy = new BasicCopy();
        private readonly BasicDelete _extDelete = new BasicDelete();
        private readonly BasicSave _extSave = new BasicSave();        
        private int _position;  ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Data Members Top 1 +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">      <![CDATA[ 

        //YouTube
        private readonly YoutubeClient _client;
        private bool _isBusy;
        private string _query;
        private Video _video;
  //      private readonly Channel _channel;
   //     private readonly MediaStreamInfoSet _mediaStreamInfos;
    //    private readonly IReadOnlyList<ClosedCaptionTrackInfo> _closedCaptionTrackInfos;
        private double _progress;        
        private bool _isProgressIndeterminate;   ]]>
</xsl:when>  
<xsl:otherwise>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Data Members Top 3 +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>        <![CDATA[ 
        #endregion [Private Data Members]         ]]>      
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Constructor 2 Top +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>     <![CDATA[ 
        #region [Constructor]

        public ]]><xsl:value-of select="Basiss"/><![CDATA[ViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {          ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Constructor 2 +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">   <![CDATA[ 
                GetValueLanguage();
                GetValueContinent();
                GetValueMimeType();
                RegisterCommands(); 
                _entityException = new DbEntityException();
            }
        }
        #endregion "Constructor"          ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">   <![CDATA[ 
                GetValueLanguage();
                GetValueContinent();
                GetValueMimeType();
                RegisterCommands();
                _entityException = new DbEntityException();
            }
        }
        #endregion "Constructor"           ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">   <![CDATA[ 
                GetValueLanguage();
                _entityException = new DbEntityException();
            }
        }
        #endregion "Constructor"          ]]>
</xsl:when> 
<xsl:when test="Table ='Tbl81Images'">     <![CDATA[ 
                //Image;
                GetValueMimeType();
                RegisterCommands();

                //YouTube
                _client = new YoutubeClient();

                // Commands
                GetVideoCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(GetVideo,
                    () => !IsBusy && Query.IsNotBlank());
                DownloadMediaStreamCommand = new RelayCommand<MediaStreamInfo>(DownloadMediaStream,
                    _ => !IsBusy);
                DownloadClosedCaptionTrackCommand = new RelayCommand<ClosedCaptionTrackInfo>(
                    DownloadClosedCaptionTrack, _ => !IsBusy);

                _entityException = new DbEntityException();
            }
        }   
        #endregion "Constructor"                  ]]>      
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">     <![CDATA[ 
                // Code runs "for real" 
                GetValueContinent();
                _entityException = new DbEntityException();
            }
        }

        #endregion "Constructor"                 ]]>      
</xsl:when>   
<xsl:when test="Table ='TblUserProfiles'">     <![CDATA[ 
                 GetValueRole();
                 GetValueGender();
                 GetValueTitle();
                _entityException = new DbEntityException();  
            }
        }

        #endregion "Constructor"               ]]>      
</xsl:when>   
<xsl:otherwise>       <![CDATA[ 
                // Code runs "for real" 
                ]]><xsl:value-of select="Table"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>();    
            }
        }     
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]         ]]>
</xsl:otherwise>    
</xsl:choose> 

<![CDATA[ //    Part 1    ]]>

<xsl:choose>
<xsl:when test="Table ='Public Commands 1 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>      <![CDATA[   

        #region [Commands ]]><xsl:value-of select="Basis"/><![CDATA[]

        private RelayCommand _get]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrIdCommand;
        public ICommand Get]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrIdCommand => _get]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrIdCommand ??= new RelayCommand(delegate {ExecuteGet]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrId(Search]]><xsl:value-of select="Basis"/><![CDATA[Name); });    ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 2  Basic Add and  Copy  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>      <![CDATA[       
        private RelayCommand _add]]><xsl:value-of select="Basis"/><![CDATA[Command;
        public ICommand Add]]><xsl:value-of select="Basis"/><![CDATA[Command => _add]]><xsl:value-of select="Basis"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(null); });

        private RelayCommand _copy]]><xsl:value-of select="Basis"/><![CDATA[Command;
        public ICommand Copy]]><xsl:value-of select="Basis"/><![CDATA[Command => _copy]]><xsl:value-of select="Basis"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteCopy]]><xsl:value-of select="Basis"/><![CDATA[(null); });      ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 2  Basic Delete  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>      <![CDATA[       
        private RelayCommand _delete]]><xsl:value-of select="Basis"/><![CDATA[Command;
        public ICommand Delete]]><xsl:value-of select="Basis"/><![CDATA[Command => _delete]]><xsl:value-of select="Basis"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteDelete]]><xsl:value-of select="Basis"/><![CDATA[(Search]]><xsl:value-of select="Basis"/><![CDATA[Name); });    ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 2  Basic Save  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>      <![CDATA[       
        private RelayCommand _save]]><xsl:value-of select="Basis"/><![CDATA[Command;
        public ICommand Save]]><xsl:value-of select="Basis"/><![CDATA[Command => _save]]><xsl:value-of select="Basis"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteSave]]><xsl:value-of select="Basis"/><![CDATA[(Search]]><xsl:value-of select="Basis"/><![CDATA[Name); });    

        #endregion [Commands ]]><xsl:value-of select="Basis"/><![CDATA[]       ]]>

</xsl:otherwise>    
</xsl:choose> 
              
<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Basic Get  Middle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 1  Basic Get  Middle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[ 
        #region [Methods ]]><xsl:value-of select="Basis"/><![CDATA[]

        private void ExecuteGet]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrId(string searchName)
       {
            ]]><xsl:value-of select="Table"/><![CDATA[List = _extGet.SearchNameAndIdReturnCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>(Search]]><xsl:value-of select="Basis"/><![CDATA[Name, "]]><xsl:value-of select="BasisSm"/><![CDATA[");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 0;

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }                     ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">       <![CDATA[ 
        #region [Methods ]]><xsl:value-of select="Basis"/><![CDATA[]

        private void ExecuteGet]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrId(string searchName)
       {
            ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>("]]><xsl:value-of select="BasisSmFK1"/><![CDATA[");
            ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[>("]]><xsl:value-of select="BasisSmFK2"/><![CDATA[");
            ]]><xsl:value-of select="Table"/><![CDATA[List = _extGet.SearchNameAndIdReturnCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>(Search]]><xsl:value-of select="Basis"/><![CDATA[Name, "]]><xsl:value-of select="BasisSm"/><![CDATA[");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }                     ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">       <![CDATA[ 

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }
        //------------------------------------------------------------------------------------                        ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">       <![CDATA[ 

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }
        //------------------------------------------------------------------------------------                        ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">       <![CDATA[ 

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }
        //------------------------------------------------------------------------------------                        ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">       <![CDATA[ 

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }
        //------------------------------------------------------------------------------------                        ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">       <![CDATA[ 

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }
        //------------------------------------------------------------------------------------                        ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">       <![CDATA[ 

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }
        //------------------------------------------------------------------------------------                        ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl90RefExperts'">       <![CDATA[ 

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }
        //------------------------------------------------------------------------------------                        ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl90RefSources'">       <![CDATA[ 

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }
        //------------------------------------------------------------------------------------                        ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl93Comments'">       <![CDATA[ 

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }
        //------------------------------------------------------------------------------------                        ]]>  
</xsl:when>  
<xsl:when test="Table ='TblCountries'">       <![CDATA[ 

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }
        //------------------------------------------------------------------------------------                        ]]>  
</xsl:when>  
<xsl:when test="Table ='TblUserProfiles'">       <![CDATA[ 

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }
        //------------------------------------------------------------------------------------                        ]]>  
</xsl:when>  
<xsl:otherwise>    <![CDATA[ 
        #region [Methods ]]><xsl:value-of select="Basis"/><![CDATA[]

        private void ExecuteGet]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrId(string searchName)
        {
            ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>("]]><xsl:value-of select="BasisSmFK1"/><![CDATA[");
            ]]><xsl:value-of select="Table"/><![CDATA[List = _extGet.SearchNameAndIdReturnCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>(Search]]><xsl:value-of select="Basis"/><![CDATA[Name, "]]><xsl:value-of select="BasisSm"/><![CDATA[");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }                   ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Basic Add  Middle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 1  Basic Add  Middle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[   {   ]]><xsl:value-of select="Name"/><![CDATA[ = CultRes.StringsRes.DatasetNew  }  );

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }                        ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[   {   ]]><xsl:value-of select="Name"/><![CDATA[ = CultRes.StringsRes.DatasetNew  }  );

            ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>("]]><xsl:value-of select="BasisSmFK1"/><![CDATA[");
            ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[>("]]><xsl:value-of select="BasisSmFK2"/><![CDATA[");

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl68Speciesgroups'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {  
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[ {   ]]><xsl:value-of select="Name"/><![CDATA[ = CultRes.StringsRes.DatasetNew}  );

            ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[());

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[   {   ]]><xsl:value-of select="Name"/><![CDATA[ = CultRes.StringsRes.DatasetNew  }  );

                ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[());
                ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList = new ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK2"/><![CDATA[());

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[   {   Info = CultRes.StringsRes.DatasetNew  }  );

                ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[());
                ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList = new ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK2"/><![CDATA[());

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[   {   ]]><xsl:value-of select="Name"/><![CDATA[ = CultRes.StringsRes.DatasetNew  }  );

                ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[());
                ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList = new ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK2"/><![CDATA[());

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[   {   Info = CultRes.StringsRes.DatasetNew  }  );

                TblCountriesAllList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountries());
                ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList = new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK1"/><![CDATA[());
                ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList = new ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableFK2"/><![CDATA[());

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
             ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[ {   Info = CultRes.StringsRes.DatasetNew }  );

            Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03Regnums());
            Tbl06PhylumsAllList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06Phylums());
            Tbl09DivisionsAllList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09Divisions());
            Tbl12SubphylumsAllList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12Subphylums());
            Tbl15SubdivisionsAllList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15Subdivisions());
            Tbl18SuperclassesAllList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18Superclasses());
            Tbl21ClassesAllList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21Classes());
            Tbl24SubclassesAllList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24Subclasses());
            Tbl27InfraclassesAllList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27Infraclasses());
            Tbl30LegiosAllList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30Legios());
            Tbl33OrdosAllList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33Ordos());
            Tbl36SubordosAllList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36Subordos());
            Tbl39InfraordosAllList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39Infraordos());
            Tbl42SuperfamiliesAllList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42Superfamilies());
            Tbl45FamiliesAllList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45Families());
            Tbl48SubfamiliesAllList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48Subfamilies());
            Tbl51InfrafamiliesAllList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51Infrafamilies());
            Tbl54SupertribussesAllList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54Supertribusses());
            Tbl57TribussesAllList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57Tribusses());
            Tbl60SubtribussesAllList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60Subtribusses());
            Tbl63InfratribussesAllList = new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63Infratribusses());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());
            Tbl69FiSpeciessesAllList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciesses());
            Tbl72PlSpeciessesAllList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciesses());

            Tbl90RefExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_businessLayer.ListTbl90RefExperts());       
            Tbl90RefSourcesAllList = new ObservableCollection<Tbl90RefSource>(_businessLayer.ListTbl90RefSources());
            Tbl90RefAuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_businessLayer.ListTbl90RefAuthors());

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[ {   ]]><xsl:value-of select="Name"/><![CDATA[ = CultRes.StringsRes.DatasetNew}  );

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl90RefExperts'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[ {   ]]><xsl:value-of select="Name"/><![CDATA[ = CultRes.StringsRes.DatasetNew}  );

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl90RefSources'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[ {   ]]><xsl:value-of select="Name"/><![CDATA[ = CultRes.StringsRes.DatasetNew}  );

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:when test="Table ='Tbl93Comments'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {  
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[ {   Info = CultRes.StringsRes.DatasetNew }  );

            Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_businessLayer.ListTbl03Regnums());
            Tbl06PhylumsAllList = new ObservableCollection<Tbl06Phylum>(_businessLayer.ListTbl06Phylums());
            Tbl09DivisionsAllList = new ObservableCollection<Tbl09Division>(_businessLayer.ListTbl09Divisions());
            Tbl12SubphylumsAllList = new ObservableCollection<Tbl12Subphylum>(_businessLayer.ListTbl12Subphylums());
            Tbl15SubdivisionsAllList = new ObservableCollection<Tbl15Subdivision>(_businessLayer.ListTbl15Subdivisions());
            Tbl18SuperclassesAllList = new ObservableCollection<Tbl18Superclass>(_businessLayer.ListTbl18Superclasses());
            Tbl21ClassesAllList = new ObservableCollection<Tbl21Class>(_businessLayer.ListTbl21Classes());
            Tbl24SubclassesAllList = new ObservableCollection<Tbl24Subclass>(_businessLayer.ListTbl24Subclasses());
            Tbl27InfraclassesAllList = new ObservableCollection<Tbl27Infraclass>(_businessLayer.ListTbl27Infraclasses());
            Tbl30LegiosAllList = new ObservableCollection<Tbl30Legio>(_businessLayer.ListTbl30Legios());
            Tbl33OrdosAllList = new ObservableCollection<Tbl33Ordo>(_businessLayer.ListTbl33Ordos());
            Tbl36SubordosAllList = new ObservableCollection<Tbl36Subordo>(_businessLayer.ListTbl36Subordos());
            Tbl39InfraordosAllList = new ObservableCollection<Tbl39Infraordo>(_businessLayer.ListTbl39Infraordos());
            Tbl42SuperfamiliesAllList = new ObservableCollection<Tbl42Superfamily>(_businessLayer.ListTbl42Superfamilies());
            Tbl45FamiliesAllList = new ObservableCollection<Tbl45Family>(_businessLayer.ListTbl45Families());
            Tbl48SubfamiliesAllList = new ObservableCollection<Tbl48Subfamily>(_businessLayer.ListTbl48Subfamilies());
            Tbl51InfrafamiliesAllList = new ObservableCollection<Tbl51Infrafamily>(_businessLayer.ListTbl51Infrafamilies());
            Tbl54SupertribussesAllList = new ObservableCollection<Tbl54Supertribus>(_businessLayer.ListTbl54Supertribusses());
            Tbl57TribussesAllList = new ObservableCollection<Tbl57Tribus>(_businessLayer.ListTbl57Tribusses());
            Tbl60SubtribussesAllList = new ObservableCollection<Tbl60Subtribus>(_businessLayer.ListTbl60Subtribusses());
            Tbl63InfratribussesAllList = new ObservableCollection<Tbl63Infratribus>(_businessLayer.ListTbl63Infratribusses());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());
            Tbl69FiSpeciessesAllList = new ObservableCollection<Tbl69FiSpecies>(_businessLayer.ListTbl69FiSpeciesses());
            Tbl72PlSpeciessesAllList = new ObservableCollection<Tbl72PlSpecies>(_businessLayer.ListTbl72PlSpeciesses());

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:when test="Table ='TblCountries'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[ {   Name = CultRes.StringsRes.DatasetNew}  );

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:when test="Table ='TblUserProfiles'">       <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[ {   ]]><xsl:value-of select="Name"/><![CDATA[ = CultRes.StringsRes.DatasetNew}  );

            TblCountriesAllList = new ObservableCollection<TblCountry>(_businessLayer.ListTblCountries());
            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }
        //------------------------------------------------------------------------------------                             ]]>  
</xsl:when>  
<xsl:otherwise>    <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Table"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModel"/><![CDATA[   {   ]]><xsl:value-of select="Name"/><![CDATA[ = CultRes.StringsRes.DatasetNew  }  );
            ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>("]]><xsl:value-of select="BasisSmFK1"/><![CDATA[");

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }                     ]]>  
</xsl:otherwise>    
</xsl:choose> 
              
<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Basic Copy  Middle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 1  Basic Copy  Middle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        private void ExecuteCopy]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            if (_gen]]><xsl:value-of select="Basis"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModel"/><![CDATA[)) return;

            ]]><xsl:value-of select="Table"/><![CDATA[List = _extCopy.Copy]]><xsl:value-of select="Basis"/><![CDATA[(Current]]><xsl:value-of select="LinqModel"/><![CDATA[);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }                       ]]>  
</xsl:otherwise>    
</xsl:choose> 
 
<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Basic Delete  Middle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 1  Basic Delete  Middle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        private void ExecuteDelete]]><xsl:value-of select="Basis"/><![CDATA[(string searchName)
        {
            if (_gen]]><xsl:value-of select="Basis"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModel"/><![CDATA[)) return;             ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Basic Delete  Middle 1++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       <![CDATA[ 
            //check if in ]]><xsl:value-of select="TableTK1"/><![CDATA[ connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            ]]><xsl:value-of select="TableTK1"/><![CDATA[List = _extDelete.SearchForConnectedDatasetsWith]]><xsl:value-of select="Basis"/><![CDATA[IdInTable]]><xsl:value-of select="BasisTK1"/><![CDATA[(Current]]><xsl:value-of select="LinqModel"/><![CDATA[);
            ]]><xsl:value-of select="TableTK2"/><![CDATA[List = _extDelete.SearchForConnectedDatasetsWith]]><xsl:value-of select="Basis"/><![CDATA[IdInTable]]><xsl:value-of select="BasisTK2"/><![CDATA[(Current]]><xsl:value-of select="LinqModel"/><![CDATA[);    ]]>  
</xsl:when>  
<xsl:otherwise> 
   <![CDATA[ 
            //check if in ]]><xsl:value-of select="TableTK1"/><![CDATA[ connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return

            ]]><xsl:value-of select="TableTK1"/><![CDATA[List = _extDelete.SearchForConnectedDatasetsWith]]><xsl:value-of select="Basis"/><![CDATA[IdInTable]]><xsl:value-of select="BasisTK1"/><![CDATA[(Current]]><xsl:value-of select="LinqModel"/><![CDATA[);   ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Basic Delete  Middle 2++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 1  Basic Delete  Middle 2++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(]]><xsl:value-of select="TableTK1"/><![CDATA[List.Count, "]]><xsl:value-of select="BasisTK1"/><![CDATA[")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extDelete.DeleteDatasetsWith]]><xsl:value-of select="Basis"/><![CDATA[IdInTableReference(Current]]><xsl:value-of select="LinqModel"/><![CDATA[);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extDelete.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extDelete.DeleteDatasetsWith]]><xsl:value-of select="Basis"/><![CDATA[IdInTableComment(Current]]><xsl:value-of select="LinqModel"/><![CDATA[);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extDelete.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }
            try
            {
                var ]]><xsl:value-of select="BasisSm"/><![CDATA[= _uow.]]><xsl:value-of select="Table"/><![CDATA[.GetById(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);
                if (]]><xsl:value-of select="BasisSm"/><![CDATA[!= null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Name)) return;

                    _extDelete.Delete]]><xsl:value-of select="Basis"/><![CDATA[(]]><xsl:value-of select="BasisSm"/><![CDATA[);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Name);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Name + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGet]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrId(searchName);

            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToFirst();
        }              ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Basic Save  Middle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 1  Basic Save  Middle ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        private void ExecuteSave]]><xsl:value-of select="Basis"/><![CDATA[(string searchName)
        {
            if (_gen]]><xsl:value-of select="Basis"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModel"/><![CDATA[)) return;    ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Basic Save  Middle 1++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 1  Basic Save  Middle 1++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">   
</xsl:when>  
<xsl:otherwise>      <![CDATA[ 
            //Combobox select ]]><xsl:value-of select="BasisFK1"/><![CDATA[ID  may be not 0
            if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id == 0)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }   ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 1  Basic Save  Middle 2++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 1  Basic Save  Middle 2 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">      <![CDATA[ 
        private void Save]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
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
        #endregion "Public Commands"                ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">      <![CDATA[ 
        private void Save]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
        }
        #endregion "Public Commands"                ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">      <![CDATA[ 
        private void Save]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="Basiss"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="Table"/><![CDATA[List);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.Refresh();
        }
        #endregion "Public Commands"                ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl90References'">      <![CDATA[ 
        private void Save]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
        }
        #endregion "Public Commands"                ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'">      <![CDATA[ 
        private void Save]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
        }
        #endregion "Public Commands"                ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">      <![CDATA[ 
        private void Save]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
 
        }
        #endregion "Public Commands"                ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">      <![CDATA[ 
        private void Save]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
        }
        #endregion "Public Commands"                ]]>   
</xsl:when>
<xsl:when test="Table ='TblCountries'">      <![CDATA[ 
        private void Save]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
        }
        #endregion "Public Commands"                ]]>   
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">      <![CDATA[ 
        private void Save]]><xsl:value-of select="Basis"/><![CDATA[(object o)
        {
        }
        #endregion "Public Commands"                ]]>   
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
            try
            {
                var ]]><xsl:value-of select="BasisSm"/><![CDATA[ = _uow.]]><xsl:value-of select="Table"/><![CDATA[ .GetById(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);
                //   var phylum = _context.]]><xsl:value-of select="Table"/><![CDATA[.AsNoTracking().FirstOrDefault(a=>a.]]><xsl:value-of select="Basis"/><![CDATA[Id == Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);
                //          _context.Entry(]]><xsl:value-of select="BasisSm"/><![CDATA[).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Name))
                    return;

                if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id == 0)
                    ]]><xsl:value-of select="BasisSm"/><![CDATA[ = _extSave.]]><xsl:value-of select="Basis"/><![CDATA[Add(Current]]><xsl:value-of select="LinqModel"/><![CDATA[);
                else
                    ]]><xsl:value-of select="BasisSm"/><![CDATA[ = _extSave.]]><xsl:value-of select="Basis"/><![CDATA[Update(]]><xsl:value-of select="BasisSm"/><![CDATA[, Current]]><xsl:value-of select="LinqModel"/><![CDATA[);

                _position = ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentPosition;

                try
                {
                    _extSave.]]><xsl:value-of select="Basis"/><![CDATA[Save(]]><xsl:value-of select="BasisSm"/><![CDATA[, Current]]><xsl:value-of select="LinqModel"/><![CDATA[);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave); 
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error); 
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.SaveSuccess, Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id == 0
                    ? "DatasetNew"
                    : Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Name);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error); 
                Log.Error(e);
            }
            ExecuteGet]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrId(searchName);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToPosition(_position);
        }
        #endregion [Methods ]]><xsl:value-of select="Basis"/><![CDATA[]              ]]>  
</xsl:otherwise>    
</xsl:choose> 
 

<![CDATA[ //    Part 2    ]]>

<xsl:choose>
<xsl:when test="Table ='Public Commands 2  FK1 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 2  FK1 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 2  FK1 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>      <![CDATA[     
        #region "Public Commands Connect <== ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["                 
        

        private RelayCommand _save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command;

        public ICommand Save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command => _save]]><xsl:value-of select="BasisFK1"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteSave]]><xsl:value-of select="BasisFK1"/><![CDATA[(null); });        ]]>
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 2  FK1 Basic Get  Top 1 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>      <![CDATA[     
        private void ExecuteSave]]><xsl:value-of select="BasisFK1"/><![CDATA[(string searchName)
        {
            if (_gen]]><xsl:value-of select="BasisFK1"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[)) return;

            try
            {
                var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[ = _uow.]]><xsl:value-of select="TableFK1"/><![CDATA[.GetById(Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id);

                if (Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id == 0)
                    ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisFK1"/><![CDATA[Add(Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[);
                else
                    ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisFK1"/><![CDATA[Update(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[, Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[);

                _position = ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentPosition;   ]]>
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 2  FK1 Basic Get  Top 1 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">    <![CDATA[ 
                var cap = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Name + " " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))        return;             ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">    <![CDATA[ 
                var cap = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Name + " " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))        return;             ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>      <![CDATA[ 
                var cap = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Name;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))        return;             ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 2  FK1 Basic Get  Top 1 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>      <![CDATA[ 
                try
                {
                    _extSave.]]><xsl:value-of select="BasisFK1"/><![CDATA[Save(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[, Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave); 
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }          ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 2  FK1 Basic Get  Top 1 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">    <![CDATA[ 
                _allMessageBoxes.InfoMessageBox("Save Successfull", Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id == 0
                    ? CultRes.StringsRes.DatasetNew
                    : Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Name + " " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum);
            }     ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">    <![CDATA[ 
                _allMessageBoxes.InfoMessageBox("Save Successfull", Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id == 0
                    ? CultRes.StringsRes.DatasetNew
                    : Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Name + " " + Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.Subregnum);
            }     ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>     <![CDATA[ 
                _allMessageBoxes.InfoMessageBox("Save Successfull", Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id == 0
                    ? CultRes.StringsRes.DatasetNew
                    : Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Name);
            }     ]]>  
</xsl:otherwise>    
</xsl:choose> 
            

<xsl:choose>
<xsl:when test="Table ='Public Commands 2 FK1  Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 2 FK1  Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">  
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
            ExecuteGet]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrId(searchName);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Property Commands 2  FK1 Connect  Save  Bottom Names + Image + Synonym + Geographic +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Property Commands 2  F12 Connect  Save  Bottom Names + Image + Synonym + Geographic +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">       
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">       
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">       
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl36Subordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl39Infraordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">       
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">       
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">             
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">             
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">             
</xsl:when>
<xsl:when test="Table ='TblCountries'">             
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">             
</xsl:when>
<xsl:otherwise>      <![CDATA[ 
        private void ExecuteSave]]><xsl:value-of select="BasisFK1"/><![CDATA[(string searchName)
        {
            if (_gen]]><xsl:value-of select="BasisFK1"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[)) return;

            try
            {
                var ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[ = _uow.]]><xsl:value-of select="TableFK1"/><![CDATA[.GetById(Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id);

                if (Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id == 0)
                    ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisFK1"/><![CDATA[Add(Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[);
                else
                    ]]><xsl:value-of select="BasisSmFK1"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisFK1"/><![CDATA[Update(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[, Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[);

                _position = ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentPosition;

                var cap = Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Name;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))                return;

                try
                {
                    _extSave.]]><xsl:value-of select="BasisFK1"/><![CDATA[Save(]]><xsl:value-of select="BasisSmFK1"/><![CDATA[, Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave); 
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id == 0
                    ? CultRes.StringsRes.DatasetNew
                    : Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Name;
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGet]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrId(searchName);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToPosition(_position);
        }
        #endregion "Public Commands"                ]]>        
</xsl:otherwise>    
</xsl:choose>                                                          

<![CDATA[ //    Part 3    ]]>

<xsl:choose>
<xsl:when test="Table ='Public Commands 3  FK2 Connect  Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 3  FK2 Connect  Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 3  FK2 Connect Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">             
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">             
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">             
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">             
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">             
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">             
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">             
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">             
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">             
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">             
</xsl:when>
<xsl:when test="Table ='Tbl36Subordos'">             
</xsl:when>
<xsl:when test="Table ='Tbl39Infraordos'">             
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">             
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">             
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">             
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">             
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>      <![CDATA[     
        #region "Public Commands Connect <== ]]><xsl:value-of select="LinqModelFK2"/><![CDATA["                 
        //-------------------------------------------------------------------------
        private RelayCommand _save]]><xsl:value-of select="BasisFK2"/><![CDATA[Command;

        public ICommand Save]]><xsl:value-of select="BasisFK2"/><![CDATA[Command => _save]]><xsl:value-of select="BasisFK2"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteSave]]><xsl:value-of select="BasisFK2"/><![CDATA[(null); });

        //-------------------------------------------------------------------------          ]]>
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 3 FK2  Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 3 FK2  Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">             
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">             
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">             
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">             
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">             
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">             
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">             
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">             
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">             
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">             
</xsl:when>
<xsl:when test="Table ='Tbl36Subordos'">             
</xsl:when>
<xsl:when test="Table ='Tbl39Infraordos'">             
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">             
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">             
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">             
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">             
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">  
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       <![CDATA[ 
        private void Save]]><xsl:value-of select="BasisFK2"/><![CDATA[(object o)
        {
            if (_gen]]><xsl:value-of select="BasisFK2"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[)) return;

            try
            {
                var ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[ = _uow.]]><xsl:value-of select="TableFK2"/><![CDATA[.GetById(Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Id);

                if (Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Id == 0)
                    ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisFK2"/><![CDATA[Add(Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[);
                else
                    ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisFK2"/><![CDATA[Update(]]><xsl:value-of select="BasisSmFK2"/><![CDATA[, Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[);

                _position = ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentPosition;

                var cap = Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Name;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))                return;

                try
                {
                    _extSave.]]><xsl:value-of select="BasisFK2"/><![CDATA[Save(]]><xsl:value-of select="BasisSmFK2"/><![CDATA[, Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave); 
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Id == 0
                    ? CultRes.StringsRes.DatasetNew
                    : Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Name);
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGet]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrId(searchName);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToPosition(_position);
        }
        
        #endregion "Public Commands"                ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       <![CDATA[ 
        private void Save]]><xsl:value-of select="BasisFK2"/><![CDATA[(object o)
        {
            if (_gen]]><xsl:value-of select="BasisFK2"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[)) return;

            try
            {
                var ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[ = _uow.]]><xsl:value-of select="TableFK2"/><![CDATA[.GetById(Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Id);

                if (Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Id == 0)
                    ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisFK2"/><![CDATA[Add(Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[);
                else
                    ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisFK2"/><![CDATA[Update(]]><xsl:value-of select="BasisSmFK2"/><![CDATA[, Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[);

                _position = ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentPosition;

                var cap = Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Name;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))                return;

                try
                {
                    _extSave.]]><xsl:value-of select="BasisFK2"/><![CDATA[Save(]]><xsl:value-of select="BasisSmFK2"/><![CDATA[, Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave); 
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Id == 0
                    ? CultRes.StringsRes.DatasetNew
                    : Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Name);
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGet]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrId(searchName);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">   
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">    
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">   
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">    
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        private void ExecuteSave]]><xsl:value-of select="BasisFK2"/><![CDATA[(string searchName)
        {
            if (_gen]]><xsl:value-of select="BasisFK2"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[)) return;

            try
            {
                var ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[ = _uow.]]><xsl:value-of select="TableFK2"/><![CDATA[.GetById(Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Id);

                if (Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Id == 0)
                    ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisFK2"/><![CDATA[Add(Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[);
                else
                    ]]><xsl:value-of select="BasisSmFK2"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisFK2"/><![CDATA[Update(]]><xsl:value-of select="BasisSmFK2"/><![CDATA[, Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[);

                _position = ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentPosition;

                var cap = Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Name;
                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(cap))                return;

                try
                {
                    _extSave.]]><xsl:value-of select="BasisFK2"/><![CDATA[Save(]]><xsl:value-of select="BasisSmFK2"/><![CDATA[, Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave); 
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Id == 0
                    ? CultRes.StringsRes.DatasetNew
                    : Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Name);
            }

            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ExecuteGet]]><xsl:value-of select="Basiss"/><![CDATA[ByNameOrId(searchName);
            ]]><xsl:value-of select="Basiss"/><![CDATA[View.MoveCurrentToPosition(_position);
        }

        #endregion "Public Commands"                ]]>  
</xsl:otherwise>    
</xsl:choose> 


<xsl:choose>
<xsl:when test="Table ='Property Commands 3  FK2 Connect  Save  Bottom Names + Image + Synonym + Geographic +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Property Commands 3  F12 Connect  Save  Bottom Names + Image + Synonym + Geographic +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">       
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">       
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">       
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl36Subordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl39Infraordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">       
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">       
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">             
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">             
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">             
</xsl:when>
<xsl:when test="Table ='TblCountries'">             
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">             
</xsl:when>
<xsl:otherwise>      <![CDATA[ 
        private void Save]]><xsl:value-of select="BasisFK2"/><![CDATA[(object o)
        {           
 
        }
        #endregion "Public Commands"                ]]>  
</xsl:otherwise>    
</xsl:choose>                                                          



<![CDATA[ //    Part 4    ]]>

<xsl:choose>
<xsl:when test="Table ='Public Commands 4  TK1 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 4  TK1 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 4  TK1 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>      <![CDATA[     
        #region [Public Commands Connect ==> ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[]                 
        
        private RelayCommand _add]]><xsl:value-of select="BasisTK1"/><![CDATA[Command;
        public ICommand Add]]><xsl:value-of select="BasisTK1"/><![CDATA[Command => _add]]><xsl:value-of select="BasisTK1"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteAdd]]><xsl:value-of select="BasisTK1"/><![CDATA[(null); });

        private RelayCommand _copy]]><xsl:value-of select="BasisTK1"/><![CDATA[Command;
        public ICommand Copy]]><xsl:value-of select="BasisTK1"/><![CDATA[Command => _copy]]><xsl:value-of select="BasisTK1"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteCopy]]><xsl:value-of select="BasisTK1"/><![CDATA[(null); });

        private RelayCommand _delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Command;
        public ICommand Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Command => _delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteDelete]]><xsl:value-of select="BasisTK1"/><![CDATA[(Search]]><xsl:value-of select="Basis"/><![CDATA[Name); });

        private RelayCommand _save]]><xsl:value-of select="BasisTK1"/><![CDATA[Command;
        public ICommand Save]]><xsl:value-of select="BasisTK1"/><![CDATA[Command => _save]]><xsl:value-of select="BasisTK1"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteSave]]><xsl:value-of select="BasisTK1"/><![CDATA[(Search]]><xsl:value-of select="Basis"/><![CDATA[Name); });    

        #endregion [Public Commands Connect ==> ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[]    

        #region [Public Methods Connect ==> ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[]                   ]]>
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 4 TK1  Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 4 TK1  Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">           <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)      
        {
            ]]><xsl:value-of select="TableTK1"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[  { ]]><xsl:value-of select="NameTK1"/><![CDATA[ = CultRes.StringsRes.DatasetNew});

            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>    
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">           <![CDATA[ 
        private void Add]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)      
        {
            ]]><xsl:value-of select="TableTK1"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[  { ]]><xsl:value-of select="NameTK1"/><![CDATA[ = CultRes.StringsRes.DatasetNew});

            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>    
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">           <![CDATA[ 
        private void Add]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)      
        {
            ]]><xsl:value-of select="TableTK1"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[  { ]]><xsl:value-of select="NameTK1"/><![CDATA[ = CultRes.StringsRes.DatasetNew});

            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>    
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">   
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        private void ExecuteAdd]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)      
        {
            ]]><xsl:value-of select="TableTK1"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[  { ]]><xsl:value-of select="NameTK1"/><![CDATA[ = CultRes.StringsRes.DatasetNew});
            ]]><xsl:value-of select="Table"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>("]]><xsl:value-of select="BasisSm"/><![CDATA[");

            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.MoveCurrentToFirst();
        }       ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 4 TK1  Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 4 TK1  Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">            <![CDATA[ 
        private void Copy]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">            <![CDATA[ 
        private void Copy]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">            <![CDATA[ 
        private void Copy]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">            <![CDATA[ 
        private void Copy]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)
        {
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">       
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        private void ExecuteCopy]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)
        {
            if (_gen]]><xsl:value-of select="BasisTK1"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[)) return;

            ]]><xsl:value-of select="TableTK1"/><![CDATA[List = _extCopy.Copy]]><xsl:value-of select="BasisTK1"/><![CDATA[(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.MoveCurrentToFirst();
        }      ]]>  
</xsl:otherwise>    
</xsl:choose> 
      
<xsl:choose>
<xsl:when test="Table ='Public Commands 4  TK1 Delete  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 4  TK1 Delete  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">         <![CDATA[            
        private void ExecuteDelete]]><xsl:value-of select="BasisTK1"/><![CDATA[(string searchName)
        {
             if (_gen]]><xsl:value-of select="BasisTK1"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[)) return;

            //check if in ]]><xsl:value-of select="TableTK3"/><![CDATA[ connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return
            ]]><xsl:value-of select="TableTK3"/><![CDATA[List = _extDelete.SearchForConnectedDatasetsWith]]><xsl:value-of select="BasisTK1"/><![CDATA[IdInTable]]><xsl:value-of select="BasisTK3"/><![CDATA[(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(]]><xsl:value-of select="TableTK3"/><![CDATA[List.Count, "]]><xsl:value-of select="BasisTK3"/><![CDATA[")) return;     ]]>                                                                                                              
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">             
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">             
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">             
</xsl:when>
<xsl:when test="Table ='TblCountries'">             
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">             
</xsl:when>
<xsl:otherwise>       <![CDATA[           
        private void ExecuteDelete]]><xsl:value-of select="BasisTK1"/><![CDATA[(string searchName)
        {
             if (_gen]]><xsl:value-of select="BasisTK1"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[)) return;

            //check if in ]]><xsl:value-of select="TableTK2"/><![CDATA[ connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return
            ]]><xsl:value-of select="TableTK2"/><![CDATA[List = _extDelete.SearchForConnectedDatasetsWith]]><xsl:value-of select="BasisTK1"/><![CDATA[IdInTable]]><xsl:value-of select="BasisTK2"/><![CDATA[(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(]]><xsl:value-of select="TableTK2"/><![CDATA[List.Count, "]]><xsl:value-of select="BasisTK2"/><![CDATA[")) return;     ]]>                                                                                                              
</xsl:otherwise>    
</xsl:choose>                                                          
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 4  TK1 Delete  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 4  TK1 Delete  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">            <![CDATA[ 
        private void Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)
        {

            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>      <![CDATA[     
            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extDelete.DeleteDatasetsWith]]><xsl:value-of select="BasisTK1"/><![CDATA[IdInTableReference(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extDelete.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extDelete.DeleteDatasetsWith]]><xsl:value-of select="BasisTK1"/><![CDATA[IdInTableComment(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extDelete.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }

            try 
            {
                var ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[ = _uow.]]><xsl:value-of select="TableTK1"/><![CDATA[.GetById(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id);
                if (]]><xsl:value-of select="BasisSmTK1"/><![CDATA[ != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name)) return;

                    _extDelete.Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[(]]><xsl:value-of select="BasisSmTK1"/><![CDATA[);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ]]><xsl:value-of select="TableTK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.MoveCurrentToFirst();
        }               ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 4 TK1  Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 4 TK1  Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">             
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">             
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">             
</xsl:when>
<xsl:when test="Table ='TblCountries'">             
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">             
</xsl:when>
<xsl:otherwise>       <![CDATA[           
        private void ExecuteSave]]><xsl:value-of select="BasisTK1"/><![CDATA[(string searchName)
        {
             if (_gen]]><xsl:value-of select="BasisTK1"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[)) return;

            Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id;        ]]>                                                                                                              
</xsl:otherwise>    
</xsl:choose>                                                          
      
<xsl:choose>
<xsl:when test="Table ='Public Commands 4 TK1  Save 1++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 4 TK1  Save 1 ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">       <![CDATA[           
            //Search for CurrentTbl18Superclass.SubdivisionID with Plantae#Regnum# 
            var plantaeRegnum = _context.Tbl15Subdivisions.FirstOrDefault(e => e.SubdivisionName == "Plantae#Regnum#");
            if (plantaeRegnum != null) CurrentTbl18Superclass.SubdivisionId = plantaeRegnum.SubdivisionId;
       ]]>                                                                                                              
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">      <![CDATA[           
            //Search for CurrentTbl18Superclass.SubphylumID with Animalia#Regnum# 
            var plantaeRegnum = _context.Tbl12Subphylums.FirstOrDefault(e => e.SubphylumName == "Plantae#Regnum#");
            if (plantaeRegnum != null) CurrentTbl18Superclass.SubphylumId  = plantaeRegnum.SubphylumId ;
        ]]>                                                                                                              
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">             
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">             
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">             
</xsl:when>
<xsl:when test="Table ='TblCountries'">             
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">             
</xsl:when>
<xsl:otherwise>   
</xsl:otherwise>    
</xsl:choose>                                                          
      
<xsl:choose>
<xsl:when test="Table ='Public Commands 4 TK1  Save 2++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 4 TK1  Save 2++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">             
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">             
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">             
</xsl:when>
<xsl:when test="Table ='TblCountries'">             
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">             
</xsl:when>
<xsl:otherwise>       <![CDATA[       
            try
            {
                var ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[ = _uow.]]><xsl:value-of select="TableTK1"/><![CDATA[.GetById(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id);

                if (Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id == 0)
                    ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisTK1"/><![CDATA[Add(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[);
                else
                    ]]><xsl:value-of select="BasisSmTK1"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisTK1"/><![CDATA[Update(]]><xsl:value-of select="BasisSmTK1"/><![CDATA[, Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[);

              //  _position = ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name))  return;

                try
                {
                    _extSave.]]><xsl:value-of select="BasisTK1"/><![CDATA[Save(]]><xsl:value-of select="BasisSmTK1"/><![CDATA[, Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="BasisTK1"/><![CDATA[Id == 0
                    ? CultRes.StringsRes.DatasetNew
                    : Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="BasisTK1"/><![CDATA[Name);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ]]><xsl:value-of select="TableTK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[]                                ]]>                                                                                                            
</xsl:otherwise>    
</xsl:choose>                                                          
      
<xsl:choose>
<xsl:when test="Table ='Save Tbl66Genusses + Tbl68Speciesgroups ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Save Tbl66Genusses + Tbl68Speciesgroups ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">       
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">       
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl36Subordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl39Infraordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">       
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">             
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">             
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">             
</xsl:when>
<xsl:when test="Table ='TblCountries'">             
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">             
</xsl:when>
<xsl:otherwise>       <![CDATA[           
        private void Save]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)
        {
            SelectedMainTabIndex = 1;
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.Refresh();
        }
        #endregion "Public Commands"                ]]>                                                                                                              
</xsl:otherwise>    
</xsl:choose>                                                          
      
<xsl:choose>
<xsl:when test="Table ='Save 4 Tbl69FiSpeciesses + Tbl72PlSpeciesses ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Save 4 Tbl69FiSpeciesses + Tbl72PlSpeciesses ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">       
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">       
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl36Subordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl39Infraordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">       
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">       
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">      
   <![CDATA[ 
        private void Save]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)
        {
 

            Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;

            //Search for CurrentTbl78Name.PlSpeciesID with Plantae#Regnum# 
            var plantaeRegnum = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesName("Plantae#Regnum#");
            CurrentTbl78Name.PlSpeciesID = plantaeRegnum.PlSpeciesID;

            SelectedMainTabIndex = 2;
            SelectedDetailSubTabIndex = 2;
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.Refresh();
        }
        #endregion "Public Commands"                ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">      
   <![CDATA[ 
        private void Save]]><xsl:value-of select="BasisTK1"/><![CDATA[(object o)
        {
 

            Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;

            //Search for CurrentTbl78Name.FiSpeciesID with Animalia#Regnum# 
            var animaliaRegnum = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesName("Animalia#Regnum#");
            CurrentTbl78Name.FiSpeciesID = animaliaRegnum.FiSpeciesID;


            SelectedMainTabIndex = 2;
            SelectedDetailSubTabIndex = 2;
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.Refresh();
        }
        #endregion "Public Commands"                ]]>   
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">             
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">             
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">             
</xsl:when>
<xsl:when test="Table ='TblCountries'">             
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">             
</xsl:when>
<xsl:otherwise>     
</xsl:otherwise>    
</xsl:choose>                                                          


<![CDATA[ //    Part 5    ]]>

<xsl:choose>
<xsl:when test="Table ='Public Commands 5  TK2 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 5  TK2 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 5  TK2  Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">    
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[ 
               
        #region [Public Commands Connect ==> ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[]                
        
        private RelayCommand _add]]><xsl:value-of select="BasisTK2"/><![CDATA[Command;

        public ICommand Add]]><xsl:value-of select="BasisTK2"/><![CDATA[Command => _add]]><xsl:value-of select="BasisTK2"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteAdd]]><xsl:value-of select="BasisTK2"/><![CDATA[(null); });

        private RelayCommand _copy]]><xsl:value-of select="BasisTK2"/><![CDATA[Command;

        public ICommand Copy]]><xsl:value-of select="BasisTK2"/><![CDATA[Command => _copy]]><xsl:value-of select="BasisTK2"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteCopy]]><xsl:value-of select="BasisTK2"/><![CDATA[(null); });

        private RelayCommand _delete]]><xsl:value-of select="BasisTK2"/><![CDATA[Command;

        public ICommand Delete]]><xsl:value-of select="BasisTK2"/><![CDATA[Command => _delete]]><xsl:value-of select="BasisTK2"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteDelete]]><xsl:value-of select="BasisTK2"/><![CDATA[(null); });

        private RelayCommand _save]]><xsl:value-of select="BasisTK2"/><![CDATA[Command;

        public ICommand Save]]><xsl:value-of select="BasisTK2"/><![CDATA[Command => _save]]><xsl:value-of select="BasisTK2"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteSave]]><xsl:value-of select="BasisTK2"/><![CDATA[(null); });

        #endregion [Public Commands Connect ==> ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[]                

        #region [Public Methods Connect ==> ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[]                        ]]>
  </xsl:if> 
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[                
        #region "Public Commands Connect ==> ]]><xsl:value-of select="LinqModelTK2"/><![CDATA["                 
        //-------------------------------------------------------------------------
        private RelayCommand _add]]><xsl:value-of select="BasisTK2"/><![CDATA[Command;

        public ICommand Add]]><xsl:value-of select="BasisTK2"/><![CDATA[Command => _add]]><xsl:value-of select="BasisTK2"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteAdd]]><xsl:value-of select="BasisTK2"/><![CDATA[(null); });

        private RelayCommand _copy]]><xsl:value-of select="BasisTK2"/><![CDATA[Command;

        public ICommand Copy]]><xsl:value-of select="BasisTK2"/><![CDATA[Command => _copy]]><xsl:value-of select="BasisTK2"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteCopy]]><xsl:value-of select="BasisTK2"/><![CDATA[(null); });

        private RelayCommand _delete]]><xsl:value-of select="BasisTK2"/><![CDATA[Command;

        public ICommand Delete]]><xsl:value-of select="BasisTK2"/><![CDATA[Command => _delete]]><xsl:value-of select="BasisTK2"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteDelete]]><xsl:value-of select="BasisTK2"/><![CDATA[(null); });

        private RelayCommand _save]]><xsl:value-of select="BasisTK2"/><![CDATA[Command;

        public ICommand Save]]><xsl:value-of select="BasisTK2"/><![CDATA[Command => _save]]><xsl:value-of select="BasisTK2"/><![CDATA[Command ??= new RelayCommand(delegate { ExecuteSave]]><xsl:value-of select="BasisTK2"/><![CDATA[(null); });

        //-------------------------------------------------------------------------          ]]>
  </xsl:if> 
</xsl:when>
<xsl:otherwise>         
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 5  TK2   Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 5  TK2   Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">                     
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[                
        private void ExecuteAdd]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)      
        {
            ]]><xsl:value-of select="TableTK2"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[  { ]]><xsl:value-of select="NameTK2"/><![CDATA[ = CultRes.StringsRes.DatasetNew });

            ]]><xsl:value-of select="Table"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>("]]><xsl:value-of select="BasisSm"/><![CDATA[");

            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.MoveCurrentToFirst();
        }        ]]>  
  </xsl:if> 
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">    
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[                
        private void Add]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)      
        {
            ]]><xsl:value-of select="TableTK2"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[  { ]]><xsl:value-of select="NameTK2"/><![CDATA[ = CultRes.StringsRes.DatasetNew });

            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.MoveCurrentToFirst();
        }        ]]>  
  </xsl:if> 
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">    
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[                
        private void Add]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)      
        {
            if (Tbl72PlSpeciessesList == null)
                Tbl72PlSpeciessesList = new ObservableCollection<Tbl72PlSpecies>();

            ]]><xsl:value-of select="TableTK2"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[   { ]]><xsl:value-of select="NameTK2"/><![CDATA[ = CultRes.StringsRes.DatasetNew });

            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
  </xsl:if> 
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">           <![CDATA[ 
        private void Add]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)      
        {
            if (]]><xsl:value-of select="TableTK2"/><![CDATA[List == null)
                ]]><xsl:value-of select="TableTK2"/><![CDATA[List =  new ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>( );

            ]]><xsl:value-of select="TableTK2"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[   { Info = CultRes.StringsRes.DatasetNew});

            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>    
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[ 
        private void Add]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)      
        {
            if (]]><xsl:value-of select="TableTK2"/><![CDATA[List == null)
                ]]><xsl:value-of select="TableTK2"/><![CDATA[List =  new ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>( );

            ]]><xsl:value-of select="TableTK2"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[   { Info = CultRes.StringsRes.DatasetNew});

            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>    
</xsl:when>
<xsl:otherwise>   
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 5  TK2   Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 5  TK2   Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">        <![CDATA[                
        private void ExecuteCopy]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)
        {
            if (_gen]]><xsl:value-of select="BasisTK2"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[)) return;

            ]]><xsl:value-of select="TableTK2"/><![CDATA[List = _extCopy.Copy]]><xsl:value-of select="BasisTK2"/><![CDATA[(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[);

            // evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.MoveCurrentToFirst();
        }        ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">           
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[                
        private void Copy]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)
        {
            if (Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            var ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK2"/><![CDATA[By]]><xsl:value-of select="BasisTK2"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="IDTK2"/><![CDATA[);

            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
  </xsl:if>              
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">           
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[                
        private void Copy]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)
        {

            var ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK2"/><![CDATA[By]]><xsl:value-of select="BasisTK2"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="IDTK2"/><![CDATA[);


            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
  </xsl:if>              
</xsl:when>
<xsl:otherwise>   
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 5  TK2 Delete  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 5  TK2 Delete  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">        <![CDATA[                
        private void ExecuteDelete]]><xsl:value-of select="BasisTK2"/><![CDATA[(string searchName)
        {
             if (_gen]]><xsl:value-of select="BasisTK2"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[)) return;

            //check if in ]]><xsl:value-of select="TableTK4"/><![CDATA[ connected datasets no delete possible, Expert, Sources, Authors and Comment delete and than return
            ]]><xsl:value-of select="TableTK4"/><![CDATA[List = _extDelete.SearchForConnectedDatasetsWith]]><xsl:value-of select="BasisTK2"/><![CDATA[IdInTable]]><xsl:value-of select="BasisTK4"/><![CDATA[(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[);
            if (_allMessageBoxes.DoNotDeleteDatasetInfoMessageBox(]]><xsl:value-of select="TableTK4"/><![CDATA[List.Count, "]]><xsl:value-of select="BasisTK4"/><![CDATA[")) return;

            //Delete all References Experts, Sources, Authors  ----------------------------------------------------
            Tbl90ReferencesList = _extDelete.DeleteDatasetsWith]]><xsl:value-of select="BasisTK2"/><![CDATA[IdInTableReference(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[);
            if (Tbl90ReferencesList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.ReferenceAuthor + " " + CultRes.StringsRes.ReferenceSource + " " + CultRes.StringsRes.ReferenceSource)) return;

                _extDelete.DeleteReferences(Tbl90ReferencesList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Reference);
            }

            //Delete all Comments  ----------------------------------------------------
            Tbl93CommentsList = _extDelete.DeleteDatasetsWith]]><xsl:value-of select="BasisTK2"/><![CDATA[IdInTableComment(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[);
            if (Tbl93CommentsList.Count > 0)
            {
                if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.Comment)) return;

                _extDelete.DeleteComments(Tbl93CommentsList);

                _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CultRes.StringsRes.Comment);
            }

            try 
            {
                var ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[ = _uow.]]><xsl:value-of select="TableTK2"/><![CDATA[.GetById(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="BasisTK2"/><![CDATA[Id);
                if (]]><xsl:value-of select="BasisSmTK2"/><![CDATA[ != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="BasisTK2"/><![CDATA[Name)) return;

                    _extDelete.Delete]]><xsl:value-of select="BasisTK2"/><![CDATA[(]]><xsl:value-of select="BasisSmTK2"/><![CDATA[);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="BasisTK2"/><![CDATA[Name);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="BasisTK2"/><![CDATA[Name + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ]]><xsl:value-of select="TableTK2"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissTK2"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.MoveCurrentToFirst();
        }                ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">           
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">           
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">             
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">             
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">    
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">             
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">             
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">             
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">             
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">             
</xsl:when>
<xsl:when test="Table ='Tbl36Subordos'">             
</xsl:when>
<xsl:when test="Table ='Tbl39Infraordos'">             
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">             
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">             
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">             
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">             
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">             
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">    
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">        <![CDATA[                
        private void Delete]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)
        {


            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[                
        private void Delete]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)
        {

            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[                
        private void Delete]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)
        {

            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 5  TK2   Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 5  TK2   Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">             <![CDATA[ 
        private void ExecuteSave]]><xsl:value-of select="BasisTK2"/><![CDATA[(string searchName)
        {
             if (_gen]]><xsl:value-of select="BasisTK2"/><![CDATA[MessageBoxes.NoDatasetSelectedInfoMessageBox(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[)) return;

            Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id;                                                                                                                    

            try
            {
                var ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[ = _uow.]]><xsl:value-of select="TableTK2"/><![CDATA[.GetById(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="BasisTK2"/><![CDATA[Id);

                if (Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="BasisTK2"/><![CDATA[Id == 0)
                    ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisTK2"/><![CDATA[Add(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[);
                else
                    ]]><xsl:value-of select="BasisSmTK2"/><![CDATA[ = _extSave.]]><xsl:value-of select="BasisTK2"/><![CDATA[Update(]]><xsl:value-of select="BasisSmTK2"/><![CDATA[, Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[);

              //  _position = ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="BasisTK2"/><![CDATA[Name))  return;

                try
                {
                    _extSave.]]><xsl:value-of select="BasisTK2"/><![CDATA[Save(]]><xsl:value-of select="BasisSmTK2"/><![CDATA[, Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    //         Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="BasisTK2"/><![CDATA[Id == 0
                    ? CultRes.StringsRes.DatasetNew
                    : Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="BasisTK2"/><![CDATA[Name);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ]]><xsl:value-of select="TableTK2"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissTK2"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>(Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);
        
            SelectedMainTabIndex = 2;
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.MoveCurrentToFirst();
        }
        #endregion [Public Methods  Connect ==> ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[]                               ]]>                                                                                                                   
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">  
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">           
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">           
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">           
</xsl:when>
<xsl:otherwise> 
</xsl:otherwise>    
</xsl:choose> 
                 
<xsl:choose>
<xsl:when test="Table ='Save 5 Tbl66Genusses + Tbl68Speciesgroups+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Save 5 Tbl66Genusses + Tbl68Speciesgroups+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl15Subdivisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">       
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">       
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl36Subordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl39Infraordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">       
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">             
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">             
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">             
</xsl:when>
<xsl:when test="Table ='TblCountries'">             
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">             
</xsl:when>
<xsl:otherwise>   
</xsl:otherwise>    
</xsl:choose>                                                          

<xsl:choose>
<xsl:when test="Table ='Save 5 Tbl69FiSpeciesses + Tbl72PlSpeciesses+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Save 5 Tbl69FiSpeciesses + Tbl72PlSpeciesses+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">        
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">       
</xsl:when>
<xsl:when test="Table ='Tbl06Phylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl09Divisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl12Subphylums'">       
</xsl:when>
<xsl:when test="Table ='Tbl15Sundivisions'">       
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl21Classes'">       
</xsl:when>
<xsl:when test="Table ='Tbl24Subclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl27Infraclasses'">       
</xsl:when>
<xsl:when test="Table ='Tbl30Legios'">       
</xsl:when>
<xsl:when test="Table ='Tbl33Ordos'">       
</xsl:when>
<xsl:when test="Table ='Tbl36Subordoss'">       
</xsl:when>
<xsl:when test="Table ='Tbl39Infra0rdos'">       
</xsl:when>
<xsl:when test="Table ='Tbl42Superfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl45Families'">       
</xsl:when>
<xsl:when test="Table ='Tbl48Subfamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl51Infrafamilies'">       
</xsl:when>
<xsl:when test="Table ='Tbl54Supertribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl57Tribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl60Subtribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl66Genusses'">       
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">       
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">     
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[                
        private void Save]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)
        {

            Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;

            //Search for CurrentTbl81Image.PlSpeciesID with Plantae#Regnum# 
            var plantaeRegnum = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesName("Plantae#Regnum#");
            CurrentTbl81Image.PlSpeciesID = plantaeRegnum.PlSpeciesID;


            SelectedMainTabIndex = 3;
            SelectedDetailSubTabIndex = 3;
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.Refresh();
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
        #endregion "Public Commands"                ]]>  
  </xsl:if>   
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">   
  <xsl:if test="TableTK2 !='NULL'">       <![CDATA[                
        private void Save]]><xsl:value-of select="BasisTK2"/><![CDATA[(object o)
        {

            Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;

            //Search for CurrentTbl81Image.FiSpeciesID with Animalia#Regnum# 
            var animaliaRegnum = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesName("Animalia#Regnum#");
            CurrentTbl81Image.FiSpeciesID = animaliaRegnum.FiSpeciesID;


            SelectedMainTabIndex = 3;
            SelectedDetailSubTabIndex = 3;
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.Refresh();
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
        #endregion "Public Commands"                ]]>  
  </xsl:if>       
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">  
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">  
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'">             
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'">             
</xsl:when>
<xsl:when test="Table ='Tbl90References'">    
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'">             
</xsl:when>
<xsl:when test="Table ='TblCountries'">             
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">             
</xsl:when>
<xsl:otherwise>   
</xsl:otherwise>    
</xsl:choose>                                                          
                      
<![CDATA[ //    Part 6    ]]>

<xsl:choose>
<xsl:when test="Table ='Public Commands 6  TK3 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 6  TK3 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 6  TK3 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">               
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">               
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:otherwise>          
   <xsl:if test="TableTK3 !='NULL'">       <![CDATA[      
       #region "Public Commands Connect ==> ]]><xsl:value-of select="LinqModelTK3"/><![CDATA["                 
        //-------------------------------------------------------------------------
        private RelayCommand _add]]><xsl:value-of select="BasisTK3"/><![CDATA[Command;

        public ICommand Add]]><xsl:value-of select="BasisTK3"/><![CDATA[Command => _add]]><xsl:value-of select="BasisTK3"/><![CDATA[Command ??= new RelayCommand(delegate { Add]]><xsl:value-of select="BasisTK3"/><![CDATA[(null); });

        private RelayCommand _copy]]><xsl:value-of select="BasisTK3"/><![CDATA[Command;

        public ICommand Copy]]><xsl:value-of select="BasisTK3"/><![CDATA[Command => _copy]]><xsl:value-of select="BasisTK3"/><![CDATA[Command ??= new RelayCommand(delegate { Copy]]><xsl:value-of select="BasisTK3"/><![CDATA[(null); });

        private RelayCommand _delete]]><xsl:value-of select="BasisTK3"/><![CDATA[Command;

        public ICommand Delete]]><xsl:value-of select="BasisTK3"/><![CDATA[Command => _delete]]><xsl:value-of select="BasisTK3"/><![CDATA[Command ??= new RelayCommand(delegate { Delete]]><xsl:value-of select="BasisTK3"/><![CDATA[(null); });

        private RelayCommand _save]]><xsl:value-of select="BasisTK3"/><![CDATA[Command;

        public ICommand Save]]><xsl:value-of select="BasisTK3"/><![CDATA[Command => _save]]><xsl:value-of select="BasisTK3"/><![CDATA[Command ??= new RelayCommand(delegate { Save]]><xsl:value-of select="BasisTK3"/><![CDATA[(null); });

        //-------------------------------------------------------------------------          ]]>
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 6  TK3   Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 6  TK3   Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">               
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">               
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">           <![CDATA[ 
        private void Add]]><xsl:value-of select="BasisTK3"/><![CDATA[(object o)      
        {
            if (]]><xsl:value-of select="TableTK3"/><![CDATA[List == null)
                ]]><xsl:value-of select="TableTK3"/><![CDATA[List =  new ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[>( );

             ]]><xsl:value-of select="TableTK3"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[  { ]]><xsl:value-of select="NameTK3"/><![CDATA[ = CultRes.StringsRes.DatasetNew});

            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK3"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>    
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">           <![CDATA[ 
        private void Add]]><xsl:value-of select="BasisTK3"/><![CDATA[(object o)      
        {
            if (]]><xsl:value-of select="TableTK3"/><![CDATA[List == null)
                ]]><xsl:value-of select="TableTK3"/><![CDATA[List =  new ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[>( );

             ]]><xsl:value-of select="TableTK3"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[  { ]]><xsl:value-of select="NameTK3"/><![CDATA[ = CultRes.StringsRes.DatasetNew});

            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK3"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>    
</xsl:when>
<xsl:otherwise>    
  <xsl:if test="TableTK3 !='NULL'">       <![CDATA[      
        private void Add]]><xsl:value-of select="BasisTK3"/><![CDATA[(object o)      
        {
             ]]><xsl:value-of select="TableTK3"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[  { ]]><xsl:value-of select="NameTK3"/><![CDATA[ = CultRes.StringsRes.DatasetNew});

            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK3"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 6  TK3   Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 6  TK3   Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">               
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">               
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">            
  <xsl:if test="TableTK3 !='NULL'">       <![CDATA[      
        private void Copy]]><xsl:value-of select="BasisTK3"/><![CDATA[(object o)
        {

            ]]><xsl:value-of select="TableTK3"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[>();

            var ]]><xsl:value-of select="BasisSmTK3"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="BasisTK3"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="IDTK3"/><![CDATA[);

            ]]><xsl:value-of select="TableTK3"/><![CDATA[List.Add(new ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[
            {
                SynonymName = CultRes.StringsRes.DatasetNew,
                Valid = synonym.Valid,
                ValidYear = synonym.ValidYear,
                Author = synonym.Author,
                AuthorYear = synonym.AuthorYear,
                Info = synonym.Info,
                Memo = synonym.Memo      
            });

            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK3"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
  </xsl:if> 
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">           
  <xsl:if test="TableTK3 !='NULL'">       <![CDATA[      
        private void Copy]]><xsl:value-of select="BasisTK3"/><![CDATA[(object o)
        {

            ]]><xsl:value-of select="TableTK3"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[>();

            var ]]><xsl:value-of select="BasisSmTK3"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="BasisTK3"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="IDTK3"/><![CDATA[);

            ]]><xsl:value-of select="TableTK3"/><![CDATA[List.Add(new ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[
            {
                SynonymName = CultRes.StringsRes.DatasetNew,
                Valid = synonym.Valid,
                ValidYear = synonym.ValidYear,
                Author = synonym.Author,
                AuthorYear = synonym.AuthorYear,
                Info = synonym.Info,
                Memo = synonym.Memo     
            });

            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK3"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
  </xsl:if> 
</xsl:when>
<xsl:otherwise>    
  <xsl:if test="TableTK3 !='NULL'">       <![CDATA[      
        private void Copy]]><xsl:value-of select="BasisTK3"/><![CDATA[(object o)
        {

            ]]><xsl:value-of select="TableTK3"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[>();

            var ]]><xsl:value-of select="BasisSmTK3"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="BasisTK3"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="IDTK3"/><![CDATA[);


            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK3"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 
   
<xsl:choose>
<xsl:when test="Table ='Public Commands 6  TK3   Delete ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 6  TK3   Delete ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">               
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">               
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[                
        private void Delete]]><xsl:value-of select="BasisTK3"/><![CDATA[(object o)
        {

            try
            {
                var ]]><xsl:value-of select="BasisSmTK3"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="BasisTK3"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="IDTK3"/><![CDATA[);
                if (]]><xsl:value-of select="BasisSmTK3"/><![CDATA[!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="NameTK3"/><![CDATA[,
                         MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    ]]><xsl:value-of select="BasisSmTK3"/><![CDATA[.EntityState = EntityState.Deleted;
                    _businessLayer.Remove]]><xsl:value-of select="BasisTK3"/><![CDATA[(]]><xsl:value-of select="BasisSmTK3"/><![CDATA[);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="NameTK3"/><![CDATA[,
                       MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);  
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="NameTK3"/><![CDATA[ + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                 Log.Error(ex);
           }
         
            ]]><xsl:value-of select="TableTK3"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[));

            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK3"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[                
        private void Delete]]><xsl:value-of select="BasisTK3"/><![CDATA[(object o)
        {

            try
            {
                var ]]><xsl:value-of select="BasisSmTK3"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="BasisTK3"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="IDTK3"/><![CDATA[);
                if (]]><xsl:value-of select="BasisSmTK3"/><![CDATA[!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="NameTK3"/><![CDATA[,
                         MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    ]]><xsl:value-of select="BasisSmTK3"/><![CDATA[.EntityState = EntityState.Deleted;
                    _businessLayer.Remove]]><xsl:value-of select="BasisTK3"/><![CDATA[(]]><xsl:value-of select="BasisSmTK3"/><![CDATA[);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="NameTK3"/><![CDATA[,
                       MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);  
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="NameTK3"/><![CDATA[ + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
            }
         
            ]]><xsl:value-of select="TableTK3"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK3"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[));

            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK3"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                  ]]>  
</xsl:when>
<xsl:otherwise>    
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 6  TK3  Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 6  TK3 Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">               
</xsl:when>
<xsl:when test="Table ='Tbl63Infratribusses'">               
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">         
  <xsl:if test="TableTK3 !='NULL'">       <![CDATA[      
        private void Save]]><xsl:value-of select="BasisTK3"/><![CDATA[(object o)
        {

            Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;

            //Search for CurrentTbl84Synonym.PlSpeciesID with Plantae#Regnum# 
            var plantaeRegnum = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesName("Plantae#Regnum#");
            CurrentTbl84Synonym.PlSpeciesID = plantaeRegnum.PlSpeciesID;


            SelectedMainTabIndex = 4;
            SelectedDetailSubTabIndex = 4;
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK3"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View.Refresh();
        }
        #endregion "Public Commands"                ]]>  
  </xsl:if> 
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">           
  <xsl:if test="TableTK3 !='NULL'">       <![CDATA[      
        private void Save]]><xsl:value-of select="BasisTK3"/><![CDATA[(object o)
        {


            Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;

            //Search for CurrentTbl84Synonym.FiSpeciesID with Animalia#Regnum# 
            var animaliaRegnum = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesName("Animalia#Regnum#");
            CurrentTbl84Synonym.FiSpeciesID = animaliaRegnum.FiSpeciesID;


            SelectedMainTabIndex = 4;
            SelectedDetailSubTabIndex = 4;
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK3"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View.Refresh();
        }
        #endregion "Public Commands"                ]]>  
  </xsl:if> 
</xsl:when>
<xsl:otherwise>    
  <xsl:if test="TableTK3 !='NULL'">       <![CDATA[      
        private void ExecuteSave]]><xsl:value-of select="BasisTK3"/><![CDATA[(object o)
        {

            Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;


            SelectedMainTabIndex = 1;
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK3"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK3"/><![CDATA[View.Refresh();
        }
        #endregion "Public Commands"                ]]>  
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 
            

<![CDATA[ //    Part 7    ]]>

<xsl:choose>
<xsl:when test="Table ='Public Commands 7 TK4 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 7 TK4 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 7 TK4 Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">               
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:otherwise>          
   <xsl:if test="TableTK4 !='NULL'">       <![CDATA[      
       #region "Public Commands Connect ==> ]]><xsl:value-of select="LinqModelTK4"/><![CDATA["                 
        //-------------------------------------------------------------------------
        private RelayCommand _add]]><xsl:value-of select="BasisTK4"/><![CDATA[Command;

        public ICommand Add]]><xsl:value-of select="BasisTK4"/><![CDATA[Command => _add]]><xsl:value-of select="BasisTK4"/><![CDATA[Command ??= new RelayCommand(delegate { Add]]><xsl:value-of select="BasisTK4"/><![CDATA[(null); });

        private RelayCommand _copy]]><xsl:value-of select="BasisTK4"/><![CDATA[Command;

        public ICommand Copy]]><xsl:value-of select="BasisTK4"/><![CDATA[Command => _copy]]><xsl:value-of select="BasisTK4"/><![CDATA[Command ??= new RelayCommand(delegate { Copy]]><xsl:value-of select="BasisTK4"/><![CDATA[(null); });

        private RelayCommand _delete]]><xsl:value-of select="BasisTK4"/><![CDATA[Command;

        public ICommand Delete]]><xsl:value-of select="BasisTK4"/><![CDATA[Command => _delete]]><xsl:value-of select="BasisTK4"/><![CDATA[Command ??= new RelayCommand(delegate { Delete]]><xsl:value-of select="BasisTK4"/><![CDATA[(null); });

        private RelayCommand _save]]><xsl:value-of select="BasisTK4"/><![CDATA[Command;

        public ICommand Save]]><xsl:value-of select="BasisTK4"/><![CDATA[Command => _save]]><xsl:value-of select="BasisTK4"/><![CDATA[Command ??= new RelayCommand(delegate { Save]]><xsl:value-of select="BasisTK4"/><![CDATA[(null); });

        //-------------------------------------------------------------------------          ]]>
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 7 TK4   Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 7 TK4   Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">               
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">           <![CDATA[ 
        private void Add]]><xsl:value-of select="BasisTK4"/><![CDATA[(object o)      
        {
            if (]]><xsl:value-of select="TableTK4"/><![CDATA[List == null)
                ]]><xsl:value-of select="TableTK4"/><![CDATA[List =  new ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[>( );

            ]]><xsl:value-of select="TableTK4"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[  { Info = CultRes.StringsRes.DatasetNew});

            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK4"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>    
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    <![CDATA[ 
        private void Add]]><xsl:value-of select="BasisTK4"/><![CDATA[(object o)      
        {
            if (]]><xsl:value-of select="TableTK4"/><![CDATA[List == null)
                ]]><xsl:value-of select="TableTK4"/><![CDATA[List =  new ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[>( );

            ]]><xsl:value-of select="TableTK4"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[  { Info = CultRes.StringsRes.DatasetNew});

            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK4"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>    
</xsl:when>
<xsl:otherwise>    
  <xsl:if test="TableTK4 !='NULL'">       <![CDATA[      
        private void Add]]><xsl:value-of select="BasisTK4"/><![CDATA[(object o)      
        {
            ]]><xsl:value-of select="TableTK4"/><![CDATA[List.Insert(0, new ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[  { Info = CultRes.StringsRes.DatasetNew});

            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK4"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 7 TK4   Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 7 TK4   Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">               
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">            
  <xsl:if test="TableTK4 !='NULL'">       <![CDATA[      
        private void Copy]]><xsl:value-of select="BasisTK4"/><![CDATA[(object o)
        {
            if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            ]]><xsl:value-of select="TableTK4"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[>();

            var ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="BasisTK4"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[);

            ]]><xsl:value-of select="TableTK4"/><![CDATA[List.Add(new ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[
            {
                Address = geographic.Address,
                Continent = geographic.Continent,
                Country = geographic.Country,
                Http = geographic.Http,
                Latitude = geographic.Latitude,
                Longitude = geographic.Longitude,
                Latitude1 = geographic.Latitude1,
                Longitude1 = geographic.Longitude1,
                Latitude2 = geographic.Latitude2,
                Longitude2 = geographic.Longitude2,
                Latitude3 = geographic.Latitude3,
                Longitude3 = geographic.Longitude3,
                ZoomLevel = geographic.ZoomLevel,
                Valid = geographic.Valid,
                ValidYear = geographic.ValidYear,
                Author = geographic.Author,
                AuthorYear = geographic.AuthorYear,
                Info = geographic.Info,
                Memo = geographic.Memo
            });

            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK4"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
  </xsl:if> 
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">           
  <xsl:if test="TableTK4 !='NULL'">       <![CDATA[      
        private void Copy]]><xsl:value-of select="BasisTK4"/><![CDATA[(object o)
        {
            if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            ]]><xsl:value-of select="TableTK4"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[>();

            var ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="BasisTK4"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[);

            ]]><xsl:value-of select="TableTK4"/><![CDATA[List.Add(new ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[
            {
                Address = geographic.Address,
                Continent = geographic.Continent,
                Country = geographic.Country,
                Http = geographic.Http,
                Latitude = geographic.Latitude,
                Longitude = geographic.Longitude,
                Latitude1 = geographic.Latitude1,
                Longitude1 = geographic.Longitude1,
                Latitude2 = geographic.Latitude2,
                Longitude2 = geographic.Longitude2,
                Latitude3 = geographic.Latitude3,
                Longitude3 = geographic.Longitude3,
                ZoomLevel = geographic.ZoomLevel,
                Valid = geographic.Valid,
                ValidYear = geographic.ValidYear,
                Author = geographic.Author,
                AuthorYear = geographic.AuthorYear,
                Info = geographic.Info,
                Memo = geographic.Memo
            });

            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK4"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
  </xsl:if> 
</xsl:when>
<xsl:otherwise>    
  <xsl:if test="TableTK4 !='NULL'">       <![CDATA[      
        private void Copy]]><xsl:value-of select="BasisTK4"/><![CDATA[(object o)
        {
            if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            ]]><xsl:value-of select="TableTK4"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[>();

            var ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="BasisTK4"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[);

            ]]><xsl:value-of select="TableTK4"/><![CDATA[List.Add(new ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[
            {
                ]]><xsl:value-of select="NameTK4"/><![CDATA[ = CultRes.StringsRes.DatasetNew,
                Valid =  ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.Valid,
                ValidYear =  ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.ValidYear,
                Synonym =  ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.Synonym,
                Author =  ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.Author,
                AuthorYear =  ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.AuthorYear,
                Info =  ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.Info,
                EngName =  ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.EngName,
                GerName =  ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.GerName,
                FraName =  ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.FraName,
                PorName =  ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.PorName,
                Memo =  ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.Memo
            });

            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK4"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View.MoveCurrentToFirst();
        }
        //----------------------------------------------------------------------          ]]>  
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 
         
<xsl:choose>
<xsl:when test="Table ='Public Commands 6  TK3   Delete ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 6  TK3   Delete ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">               
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[                
        private void Delete]]><xsl:value-of select="BasisTK4"/><![CDATA[(object o)
        {
            if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="BasisTK4"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[);
                if (]]><xsl:value-of select="BasisSmTK4"/><![CDATA[!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[,
                         MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.EntityState = EntityState.Deleted;
                    _businessLayer.Remove]]><xsl:value-of select="BasisTK4"/><![CDATA[(]]><xsl:value-of select="BasisSmTK4"/><![CDATA[);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[.ToString(),
                       MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);  
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
            }
         
            ]]><xsl:value-of select="TableTK4"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[));

            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK4"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">        <![CDATA[                
        private void Delete]]><xsl:value-of select="BasisTK4"/><![CDATA[(object o)
        {
            if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            try
            {
                var ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="BasisTK4"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[);
                if (]]><xsl:value-of select="BasisSmTK4"/><![CDATA[!= null)
                {
                    if (WpfMessageBox.Show(CultRes.StringsRes.DeleteQuestion1, CultRes.StringsRes.DeleteQuestion + " " + Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[,
                         MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes) 
                    return;
                    ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.EntityState = EntityState.Deleted;
                    _businessLayer.Remove]]><xsl:value-of select="BasisTK4"/><![CDATA[(]]><xsl:value-of select="BasisSmTK4"/><![CDATA[);

                    WpfMessageBox.Show(CultRes.StringsRes.DeleteSuccess, Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[.ToString(),
                       MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);  
                }
                else
                {
                    WpfMessageBox.Show(CultRes.StringsRes.Information, CultRes.StringsRes.DeleteCan + " " + Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ + " " + CultRes.StringsRes.DeleteCan1,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
            }
         
            ]]><xsl:value-of select="TableTK4"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[));

            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK4"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View.Refresh();
        }
        //-------------------------------------------------------------------------------------------------                  ]]>  
</xsl:when>
<xsl:otherwise>    
</xsl:otherwise>    
</xsl:choose> 
               
<xsl:choose>
<xsl:when test="Table ='Public Commands 7 TK4  Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 7 TK4 Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">               
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">         
  <xsl:if test="TableTK4 !='NULL'">       <![CDATA[      
        private void Save]]><xsl:value-of select="BasisTK4"/><![CDATA[(object o)
        {
            if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;

            //Search for CurrentTbl87Geographic.PlSpeciesID with Plantae#Regnum# 
            var plantaeRegnum = _businessLayer.SingleListTbl72PlSpeciessesByPlSpeciesName("Plantae#Regnum#");
            CurrentTbl87Geographic.PlSpeciesID = plantaeRegnum.PlSpeciesID;
            try
            {
                var ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="BasisTK4"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[);
                if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ != 0)
                {
                    if (]]><xsl:value-of select="BasisSmTK4"/><![CDATA[ != null) //update
                    {
                            geographic.FiSpeciesID = CurrentTbl87Geographic.FiSpeciesID;
                            geographic.PlSpeciesID = CurrentTbl87Geographic.PlSpeciesID;
                            geographic.Address = CurrentTbl87Geographic.Address;
                            geographic.Continent = CurrentTbl87Geographic.Continent;
                            geographic.Country = CurrentTbl87Geographic.Country;
                            geographic.Http = CurrentTbl87Geographic.Http;
                            geographic.Latitude = CurrentTbl87Geographic.Latitude;
                            geographic.Longitude = CurrentTbl87Geographic.Longitude;
                            geographic.Latitude1 = CurrentTbl87Geographic.Latitude1;
                            geographic.Longitude1 = CurrentTbl87Geographic.Longitude1;
                            geographic.Latitude2 = CurrentTbl87Geographic.Latitude2;
                            geographic.Longitude2 = CurrentTbl87Geographic.Longitude2;
                            geographic.Latitude3 = CurrentTbl87Geographic.Latitude3;
                            geographic.Longitude3 = CurrentTbl87Geographic.Longitude3;
                            geographic.ZoomLevel = CurrentTbl87Geographic.ZoomLevel;
                            geographic.Valid = CurrentTbl87Geographic.Valid;
                            geographic.ValidYear = CurrentTbl87Geographic.ValidYear;
                            geographic.Info = CurrentTbl87Geographic.Info;
                            geographic.Memo = CurrentTbl87Geographic.Memo;
                            geographic.Updater = Environment.UserName;
                            geographic.UpdaterDate = DateTime.Now;
                             geographic.EntityState = EntityState.Modified;
                       }
                    }
                    else
                    {
                        geographic = new ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[     //add new
                        {
                            FiSpeciesID = CurrentTbl87Geographic.FiSpeciesID,
                            PlSpeciesID = CurrentTbl87Geographic.PlSpeciesID,
                            CountID = RandomHelper.Randomnumber(),
                            Address = CurrentTbl87Geographic.Address,
                            Continent = CurrentTbl87Geographic.Continent,
                            Country = CurrentTbl87Geographic.Country,
                            Http = CurrentTbl87Geographic.Http,
                            Latitude = CurrentTbl87Geographic.Latitude,
                            Longitude = CurrentTbl87Geographic.Longitude,
                            Latitude1 = CurrentTbl87Geographic.Latitude1,
                            Longitude1 = CurrentTbl87Geographic.Longitude1,
                            Latitude2 = CurrentTbl87Geographic.Latitude2,
                            Longitude2 = CurrentTbl87Geographic.Longitude2,
                            Latitude3 = CurrentTbl87Geographic.Latitude3,
                            Longitude3 = CurrentTbl87Geographic.Longitude3,
                            ZoomLevel = CurrentTbl87Geographic.ZoomLevel,
                            Valid = CurrentTbl87Geographic.Valid,
                            ValidYear = CurrentTbl87Geographic.ValidYear,
                            Info = CurrentTbl87Geographic.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl87Geographic.Memo,
                                EntityState = EntityState.Added
                                              };
                }
                {
                    //]]><xsl:value-of select="ID"/><![CDATA[ may be not 0
                    if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and ]]><xsl:value-of select="Basis"/><![CDATA[Id already exist       
                    var dataset = _businessLayer.List]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="BasisTK4"/><![CDATA[IdAnd]]><xsl:value-of select="Basis"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="BasisTK4"/><![CDATA[ID, Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[);

                    if (dataset.Count != 0 && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="BasisTK4"/><![CDATA[ID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ == 0 ||
                        dataset.Count != 0 && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ != 0 ||
                        dataset.Count == 0 && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="BasisTK4"/><![CDATA[ID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                               _businessLayer.Update]]><xsl:value-of select="BasisTK4"/><![CDATA[(]]><xsl:value-of select="BasisSmTK4"/><![CDATA[);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="BasisTK4"/><![CDATA[ID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
                  return;
           }

            ]]><xsl:value-of select="TableTK4"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[));            

            SelectedMainTabIndex = 5;
            SelectedDetailSubTabIndex = 5;
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK4"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View.Refresh();
        }
        #endregion "Public Commands"                ]]>  
  </xsl:if> 
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">           
  <xsl:if test="TableTK4 !='NULL'">       <![CDATA[      
        private void Save]]><xsl:value-of select="BasisTK4"/><![CDATA[(object o)
        {
            if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;

            //Search for CurrentTbl87Geographic.FiSpeciesID with Animalia#Regnum# 
            var animaliaRegnum = _businessLayer.SingleListTbl69FiSpeciessesByFiSpeciesName("Animalia#Regnum#");
            CurrentTbl87Geographic.FiSpeciesID = animaliaRegnum.FiSpeciesID;

            try
            {
                var ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="BasisTK4"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[);
                if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ != 0)
                {
                    if (]]><xsl:value-of select="BasisSmTK4"/><![CDATA[ != null) //update
                    {
                            geographic.FiSpeciesID = CurrentTbl87Geographic.FiSpeciesID;
                            geographic.PlSpeciesID = CurrentTbl87Geographic.PlSpeciesID;
                            geographic.Address = CurrentTbl87Geographic.Address;
                            geographic.Continent = CurrentTbl87Geographic.Continent;
                            geographic.Country = CurrentTbl87Geographic.Country;
                            geographic.Http = CurrentTbl87Geographic.Http;
                            geographic.Latitude = CurrentTbl87Geographic.Latitude;
                            geographic.Longitude = CurrentTbl87Geographic.Longitude;
                            geographic.Latitude1 = CurrentTbl87Geographic.Latitude1;
                            geographic.Longitude1 = CurrentTbl87Geographic.Longitude1;
                            geographic.Latitude2 = CurrentTbl87Geographic.Latitude2;
                            geographic.Longitude2 = CurrentTbl87Geographic.Longitude2;
                            geographic.Latitude3 = CurrentTbl87Geographic.Latitude3;
                            geographic.Longitude3 = CurrentTbl87Geographic.Longitude3;
                            geographic.ZoomLevel = CurrentTbl87Geographic.ZoomLevel;
                            geographic.Valid = CurrentTbl87Geographic.Valid;
                            geographic.ValidYear = CurrentTbl87Geographic.ValidYear;
                            geographic.Info = CurrentTbl87Geographic.Info;
                            geographic.Memo = CurrentTbl87Geographic.Memo;
                            geographic.Updater = Environment.UserName;
                            geographic.UpdaterDate = DateTime.Now;
                             geographic.EntityState = EntityState.Modified;
                       }
                    }
                    else
                    {
                        geographic = new ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[     //add new
                        {
                            FiSpeciesID = CurrentTbl87Geographic.FiSpeciesID,
                            PlSpeciesID = CurrentTbl87Geographic.PlSpeciesID,
                            CountID = RandomHelper.Randomnumber(),
                            Address = CurrentTbl87Geographic.Address,
                            Continent = CurrentTbl87Geographic.Continent,
                            Country = CurrentTbl87Geographic.Country,
                            Http = CurrentTbl87Geographic.Http,
                            Latitude = CurrentTbl87Geographic.Latitude,
                            Longitude = CurrentTbl87Geographic.Longitude,
                            Latitude1 = CurrentTbl87Geographic.Latitude1,
                            Longitude1 = CurrentTbl87Geographic.Longitude1,
                            Latitude2 = CurrentTbl87Geographic.Latitude2,
                            Longitude2 = CurrentTbl87Geographic.Longitude2,
                            Latitude3 = CurrentTbl87Geographic.Latitude3,
                            Longitude3 = CurrentTbl87Geographic.Longitude3,
                            ZoomLevel = CurrentTbl87Geographic.ZoomLevel,
                            Valid = CurrentTbl87Geographic.Valid,
                            ValidYear = CurrentTbl87Geographic.ValidYear,
                            Info = CurrentTbl87Geographic.Info,
                            Writer = Environment.UserName,
                            WriterDate = DateTime.Now,
                            Updater = Environment.UserName,
                            UpdaterDate = DateTime.Now,
                            Memo = CurrentTbl87Geographic.Memo,
                                EntityState = EntityState.Added
                                              };
                }
                {
                    //]]><xsl:value-of select="ID"/><![CDATA[ may be not 0
                    if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and ]]><xsl:value-of select="Basis"/><![CDATA[Id already exist       
                    var dataset = _businessLayer.List]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="BasisTK4"/><![CDATA[IdAnd]]><xsl:value-of select="Basis"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="BasisTK4"/><![CDATA[ID, Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[);

                    if (dataset.Count != 0 && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="BasisTK4"/><![CDATA[ID.ToString(),
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ == 0 ||
                        dataset.Count != 0 && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ != 0 ||
                        dataset.Count == 0 && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="BasisTK4"/><![CDATA[ID.ToString(),
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                               _businessLayer.Update]]><xsl:value-of select="BasisTK4"/><![CDATA[(]]><xsl:value-of select="BasisSmTK4"/><![CDATA[);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="BasisTK4"/><![CDATA[ID.ToString(),
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
                   return;
           }

            ]]><xsl:value-of select="TableTK4"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[));            

            SelectedMainTabIndex = 5;
            SelectedDetailSubTabIndex = 5;
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK4"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View.Refresh();
        }
        #endregion "Public Commands"                ]]>  
  </xsl:if> 
</xsl:when>
<xsl:otherwise>    
  <xsl:if test="TableTK4 !='NULL'">       <![CDATA[      
        private void ExecuteSave]]><xsl:value-of select="BasisTK4"/><![CDATA[(object o)
        {
            if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ == null)
            {
                WpfMessageBox.Show(CultRes.StringsRes.DatasetNew,
                    CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                return;
            }

            Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;

            try
            {
                var ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[ = _businessLayer.SingleList]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="BasisTK4"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[);
                if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ != 0)
                {
                    if (]]><xsl:value-of select="BasisSmTK4"/><![CDATA[ != null) //update
                    {
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.]]><xsl:value-of select="NameTK4"/><![CDATA[ = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="NameTK4"/><![CDATA[;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.Valid = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.Valid;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.ValidYear = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.ValidYear;       
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.Synonym = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.Synonym;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.Author = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.Author;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.AuthorYear = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.AuthorYear;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.Info = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.Info;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.EngName = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.EngName;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.GerName = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.GerName;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.FraName = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.FraName;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.PorName = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.PorName;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.Updater = Environment.UserName;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.UpdaterDate = DateTime.Now;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.Memo = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.Memo;
                        ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[.EntityState = EntityState.Modified;
                    }
                }
                else
                {
                    ]]><xsl:value-of select="BasisSmTK4"/><![CDATA[ = new ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[   //add new
                    {
                        ]]><xsl:value-of select="NameTK4"/><![CDATA[ = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="NameTK4"/><![CDATA[,              
                        ]]><xsl:value-of select="ID"/><![CDATA[ = Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[,     
                        CountID = RandomHelper.Randomnumber(),
                        Valid = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.Valid,
                        ValidYear = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.ValidYear,
                        Synonym = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.Synonym,
                        Author = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.Author,
                        AuthorYear = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.AuthorYear,
                        Info = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.Info,
                        EngName = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.EngName,
                        GerName = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.GerName,
                        FraName = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.FraName,
                        PorName = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.PorName,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now,
                        Memo = Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.Memo,
                        EntityState = EntityState.Added
                    };
                }
                {
                    //]]><xsl:value-of select="ID"/><![CDATA[ may be not 0
                    if (Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[ == 0)          

                    {
                        WpfMessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                            MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }

                    //check if dataset with Name and ]]><xsl:value-of select="Basis"/><![CDATA[Id already exist       
                    var dataset = _businessLayer.List]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="BasisTK4"/><![CDATA[NameAnd]]><xsl:value-of select="Basis"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="BasisTK4"/><![CDATA[Name, Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[);

                    if (dataset.Count != 0 && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ == 0)  //dataset exist
                    {
                        WpfMessageBox.Show(CultRes.StringsRes.DatasetExist, Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="BasisTK4"/><![CDATA[Name,
                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        return;
                    }
                    if (dataset.Count == 0 && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ == 0 ||
                        dataset.Count != 0 && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ != 0 ||
                        dataset.Count == 0 && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ != 0) //new dataset and update
                    {
                        if (WpfMessageBox.Show(CultRes.StringsRes.SaveQuestion2, Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="BasisTK4"/><![CDATA[Name,
                                MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question) != MessageBoxResult.Yes)
                            return;
                        {
                            try
                            {
                               _businessLayer.Update]]><xsl:value-of select="BasisTK4"/><![CDATA[(]]><xsl:value-of select="BasisSmTK4"/><![CDATA[);
                            }
                            catch (DbUpdateException e)
                            {
                                if (e.InnerException != null)
                                    System.Windows.MessageBox.Show(e.InnerException.ToString(), CultRes.StringsRes.FailedToSave,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);

                                Log.Error(e);
                                return;
                            }
                            catch (Exception e)
                            {
                                System.Windows.MessageBox.Show(e.Message, CultRes.StringsRes.Error,
                                    MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                                Log.Error(e);
                                return;
                            }
                                    WpfMessageBox.Show(CultRes.StringsRes.SaveSuccess,
                                        Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ == 0
                                            ? CultRes.StringsRes.DatasetNew
                                            : Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="BasisTK4"/><![CDATA[Name,
                                        MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                _entityException.EntityException(ex);
                                Log.Error(ex);
                 return;
            }

            if (Search]]><xsl:value-of select="NameTK4"/><![CDATA[ != "" && Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ == 0)    //new dataset        
                ]]><xsl:value-of select="TableTK4"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="NameTK4"/><![CDATA[(Search]]><xsl:value-of select="NameTK4"/><![CDATA[));            
            else if (Search]]><xsl:value-of select="NameTK4"/><![CDATA[ == "" && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ == 0)   //new dataset          
                ]]><xsl:value-of select="TableTK4"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="NameTK4"/><![CDATA[(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="NameTK4"/><![CDATA[));
            else if (Search]]><xsl:value-of select="NameTK4"/><![CDATA[ != "" && Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[.]]><xsl:value-of select="IDTK4"/><![CDATA[ != 0)   //update dataset
                ]]><xsl:value-of select="TableTK4"/><![CDATA[List = new ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[>(_businessLayer.List]]><xsl:value-of select="TableTK4"/><![CDATA[By]]><xsl:value-of select="NameTK4"/><![CDATA[(Search]]><xsl:value-of select="v"/><![CDATA[));

            SelectedMainTabIndex = 1;
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK4"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK4"/><![CDATA[View.Refresh();
        }
        #endregion "Public Commands"                ]]>  
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<![CDATA[ //    Part 8    ]]>

<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefAuthors Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefAuthors Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefAuthors Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>      <![CDATA[     
        #region [Commands ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });        

        #endregion [Commands ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl90Reference Author]      ]]>          
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefAuthors   Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefAuthors   Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        #region [Methods ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl90Reference Author]

        public void ExecuteAddReferenceAuthor(object o)
        {
            Tbl90ReferenceAuthorsList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90AuthorsAllList = _extGet.AllCollection<Tbl90RefAuthor>("author");
            Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference   { Info = CultRes.StringsRes.DatasetNew });

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
         }       ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefAuthors   Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefAuthors   Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        public void ExecuteCopyReferenceAuthor(object o)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            Tbl90ReferenceAuthorsList = _extCopy.CopyReference]]><xsl:value-of select="Basis"/><![CDATA[(CurrentTbl90ReferenceAuthor, "Author");

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.MoveCurrentToFirst();
        }        ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefAuthors   Delete ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefAuthors   Delete ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        private void ExecuteDeleteReferenceAuthor(string searchName)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceAuthor.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceAuthor.Info)) return;

                    _extDelete.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceAuthor.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceAuthor.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }        ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefAuthors   Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefAuthors   Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        public void ExecuteSaveReferenceAuthor(string searchName)
        {
            if (_genAuthorMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            CurrentTbl90ReferenceAuthor.]]><xsl:value-of select="Basis"/><![CDATA[Id = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id;

            //Combobox select RefExpertId or RefSourceId or RefAuthorId may be not null
            if (CurrentTbl90ReferenceAuthor.RefExpertId == null &&
                CurrentTbl90ReferenceAuthor.RefSourceId == null &&
                CurrentTbl90ReferenceAuthor.RefAuthorId == null)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceAuthor.ReferenceId);


                if (CurrentTbl90ReferenceAuthor.ReferenceId == 0)
                    reference = _extSave.ReferenceAuthor]]><xsl:value-of select="Basis"/><![CDATA[Add(CurrentTbl90ReferenceAuthor);

                else
                    reference = _extSave.ReferenceAuthor]]><xsl:value-of select="Basis"/><![CDATA[Update(reference, CurrentTbl90ReferenceAuthor);

                //    _position = ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceAuthor.Info))  return;

                try
                {
                    _extSave.ReferenceAuthorSave(reference, CurrentTbl90ReferenceAuthor);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl90ReferenceAuthor.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90ReferenceAuthor.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }
           Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);          ]]>  
</xsl:otherwise>    
</xsl:choose> 
           
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefAuthors   Save   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefAuthors   Save   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">     <![CDATA[ 

            SelectedMainTabIndex = 6;
            SelectedDetailSubTabIndex = 6;
            SelectedMainSubRefTabIndex = 2;

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion "Public Commands"                ]]>               
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">     <![CDATA[ 

            SelectedMainTabIndex = 6;
            SelectedDetailSubTabIndex = 6;
            SelectedMainSubRefTabIndex = 2;

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion "Public Commands"                ]]>                            
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 

            ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            ReferenceAuthorsView.Refresh();
        }
        #endregion [Methods ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl90Reference Author]            ]]>  
</xsl:otherwise>    
</xsl:choose> 
           

<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefSources  Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefSources  Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefSources  Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>      <![CDATA[     
        #region [Commands ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate {ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });

            
        #endregion [Commands ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl90Reference Source]         ]]>
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefSources  Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefSources  Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        #region [Methods ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl90Reference Source]      

        public void ExecuteAddReferenceSource(object o)
        {
            Tbl90ReferenceSourcesList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90SourcesAllList = _extGet.AllCollection<Tbl90RefSource>("source");

            Tbl90ReferenceSourcesList .Insert(0, new Tbl90Reference  { Info = CultRes.StringsRes.DatasetNew });

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
         }       ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefSources  Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefSources  Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        public void ExecuteCopyReferenceSource(object o)
        {
            if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            Tbl90ReferenceAuthorsList = _extCopy.CopyReference]]><xsl:value-of select="Basis"/><![CDATA[(CurrentTbl90ReferenceSource, "Source");

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }         ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefSources  Delete ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefSources  Delete ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        private void ExecuteDeleteReferenceSource(object o)
        {
            if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceSource.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceSource.Info)) return;

                    _extDelete.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceSource.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceSource.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

           Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);          

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }                ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefSources  Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefSources  Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        public void ExecuteSaveReferenceSource(object o)
        { 
           if (_genSourceMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            //RefExpertId or RefSourceId or RefAuthorId may be not 0
            if (CurrentTbl90ReferenceSource.RefExpertId == null &&
                CurrentTbl90ReferenceSource.RefSourceId == null &&
                CurrentTbl90ReferenceSource.RefAuthorId == null)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceSource.]]><xsl:value-of select="Basis"/><![CDATA[Id = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceSource.ReferenceId);


                if (CurrentTbl90ReferenceSource.ReferenceId == 0)
                    reference = _extSave.ReferenceSource]]><xsl:value-of select="Basis"/><![CDATA[Add(CurrentTbl90ReferenceSource);
                else
                    reference = _extSave.ReferenceSource]]><xsl:value-of select="Basis"/><![CDATA[Update(reference, CurrentTbl90ReferenceSource);

        //        _position = ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceSource.Info))  return;

                try
                {
                    _extSave.ReferenceSourceSave(reference, CurrentTbl90ReferenceSource);

                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl90ReferenceSource.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90ReferenceSource.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

           Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);          ]]>  

</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefSources  Save   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefSources  Save   ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">     <![CDATA[ 
            SelectedMainTabIndex = 6;
            SelectedDetailSubTabIndex = 6;
            SelectedMainSubRefTabIndex = 1;

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }
        #endregion "Public Commands"                ]]>              
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">       <![CDATA[ 
            SelectedMainTabIndex = 6;
            SelectedDetailSubTabIndex = 6;
            SelectedMainSubRefTabIndex = 1;

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.Refresh();
        }
        #endregion "Public Commands"                ]]>            
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 

            ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            ReferenceSourcesView.MoveCurrentToFirst();
        }
        #endregion [Methods ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl90Reference Source]                  ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefExperts  Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefExperts  Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefExperts  Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>      <![CDATA[     
        #region [Commands ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl90Reference Expert]                 
 
        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl90Reference Expert]                  ]]>  
     
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefExperts  Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefExperts  Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        #region [Methods ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl90Reference Expert]                 

        public void ExecuteAddReferenceExpert(object o)
        {
            Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();

            Tbl90ExpertsAllList = _extGet.AllCollection<Tbl90RefExpert>("expert");
            Tbl90ReferenceExpertsList .Insert(0, new Tbl90Reference   { Info = CultRes.StringsRes.DatasetNew });

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
         }        ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefExperts  Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefExperts  Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        public void ExecuteCopyReferenceExpert(object o)
        {
            if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            Tbl90ReferenceExpertsList = _extCopy.CopyReference]]><xsl:value-of select="Basis"/><![CDATA[(CurrentTbl90ReferenceExpert, "Expert");

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }       ]]>  
</xsl:otherwise>    
</xsl:choose> 
      
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefExperts  Delete ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefExperts  Delete ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        private void ExecuteDeleteReferenceExpert(object o)
        {
            if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceExpert.ReferenceId);
                if (reference != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl90ReferenceExpert.Info)) return;

                    _extDelete.DeleteReference(reference);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl90ReferenceExpert.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl90ReferenceExpert.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

           Tbl90ReferenceExpertsList= _extGet.GetReferenceExpertsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);           

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }        ]]>  
</xsl:otherwise>    
</xsl:choose> 
      
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefExperts  Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefExperts  Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        public void ExecuteSaveReferenceExpert(object o)
        {
             if (_genExpertMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            //RefExpertId or RefSourceId or RefAuthorId may be not 0
            if (CurrentTbl90ReferenceExpert.RefExpertId == null &&
                CurrentTbl90ReferenceExpert.RefSourceId == null &&
                CurrentTbl90ReferenceExpert.RefAuthorId == null)
            {
                MessageBox.Show(CultRes.StringsRes.RequiredGenealogyConnect, CultRes.StringsRes.RequiredInput,
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            CurrentTbl90ReferenceExpert.]]><xsl:value-of select="Basis"/><![CDATA[Id = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id;

            try
            {
                var reference = _uow.Tbl90References.GetById(CurrentTbl90ReferenceExpert.ReferenceId);


                if (CurrentTbl90ReferenceExpert.ReferenceId == 0)
                    reference = _extSave.ReferenceExpert]]><xsl:value-of select="Basis"/><![CDATA[Add(CurrentTbl90ReferenceExpert);
                else
                    reference = _extSave.ReferenceExpert]]><xsl:value-of select="Basis"/><![CDATA[Update(reference, CurrentTbl90ReferenceExpert);

                //        _position = PhylumsView.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl90ReferenceExpert.Info))  return;

                try
                {
                    _extSave.ReferenceExpertSave(reference, CurrentTbl90ReferenceExpert);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl90ReferenceExpert.ReferenceId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl90ReferenceExpert.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

           Tbl90ReferenceExpertsList= _extGet.GetReferenceExpertsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);                   ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  RefExperts  Save  Tbl69FiSpeciesses + Tbl72PlSpeciesses ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  RefExperts  Save Tbl69FiSpeciesses + Tbl72PlSpeciesses ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">        <![CDATA[ 
            SelectedMainTabIndex = 6;
            SelectedDetailSubTabIndex = 6;
            SelectedMainSubRefTabIndex = 0;

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }
        #endregion "Public Commands"                ]]>           
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">          <![CDATA[ 
            SelectedMainTabIndex = 6;
            SelectedDetailSubTabIndex = 6;
            SelectedMainSubRefTabIndex = 0;

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.Refresh();
        }
        #endregion "Public Commands"                ]]>                   
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 

            ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            ReferenceExpertsView.MoveCurrentToFirst();
        }
        #endregion [Methods ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl90Reference Expert]                             ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  Comment Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  Comment Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  Comment Basic Get  Top ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>      <![CDATA[     
       #region [Commands ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl93Comments]        
   
       private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

       #endregion [Commands ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl93Comments]        
   ]]>
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  Comment Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  Comment Add ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 

       #region [Methods ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl93Comments]        

        public void ExecuteAddComment(object o)
        {
            Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();

            Tbl93CommentsList .Insert(0, new Tbl93Comment  { Info = CultRes.StringsRes.DatasetNew });

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
         }        ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  Comment Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  Comment Copy ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        public void ExecuteCopyComment(object o)
        {

            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            Tbl93CommentsList = _extCopy.CopyComment(CurrentTbl93Comment, "Comment");

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }       ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  Comment Delete ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  Comment Delete ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        private void ExecuteDeleteComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);
                if (comment != null)
                {
                    if (_allMessageBoxes.DeleteDatasetQuestionMessageBox(CultRes.StringsRes.DeleteQuestion + " " + CurrentTbl93Comment.Info)) return;

                    _extDelete.DeleteComment(comment);

                    _allMessageBoxes.InfoMessageBox(CultRes.StringsRes.DeleteSuccess, CurrentTbl93Comment.Info);
                }
                else _allMessageBoxes.InfoMessageBox("Not To Delete", CultRes.StringsRes.DeleteCan + " " + CurrentTbl93Comment.Info + " " + CultRes.StringsRes.DeleteCan1);
            }
            catch (Exception e)
            {
                _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<Tbl93Comment>(CurrentTbl93Comment.]]><xsl:value-of select="Basis"/><![CDATA[Id);

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }      ]]>  
</xsl:otherwise>    
</xsl:choose> 
            
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  Comment Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">   
</xsl:when>
<xsl:when test="Table ='Public Commands 8  Comment Save ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
        private void ExecuteSaveComment(object o)
        {
            if (_genCommentMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            CurrentTbl93Comment.]]><xsl:value-of select="Basis"/><![CDATA[Id = Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id;

            try
            {
                var comment = _uow.Tbl93Comments.GetById(CurrentTbl93Comment.CommentId);


                if (CurrentTbl93Comment.CommentId == 0)
                    comment = _extSave.Comment]]><xsl:value-of select="Basis"/><![CDATA[Add(CurrentTbl93Comment);
                else
                    comment = _extSave.Comment]]><xsl:value-of select="Basis"/><![CDATA[Update(comment, CurrentTbl93Comment);

                //        _position = ]]><xsl:value-of select="Basiss"/><![CDATA[View.CurrentPosition;

                if (_allMessageBoxes.SaveDatasetQuestionMessageBox(CurrentTbl93Comment.Info))
                    return;

                try
                {
                    _extSave.CommentSave(comment, CurrentTbl93Comment);
                }
                catch (DbUpdateException e)
                {
                    if (e.InnerException != null)
                        _allMessageBoxes.WarningMessageBox(e.InnerException.ToString(),
                            CultRes.StringsRes.FailedToSave);
                    Log.Error(e);
                    return;
                }
                catch (Exception e)
                {
                    _allMessageBoxes.InfoMessageBox(e.Message, CultRes.StringsRes.Error);
                    Log.Error(e);
                    return;
                }

                _allMessageBoxes.InfoMessageBox("Save Successfull", CurrentTbl93Comment.CommentId == 0
                    ? CultRes.StringsRes.DatasetNew
                    : CurrentTbl93Comment.Info);
            }
            catch (Exception e)
            {
                _allMessageBoxes.WarningMessageBox(e.Message, CultRes.StringsRes.Error);
                Log.Error(e);
            }

            Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<Tbl93Comment>(CurrentTbl93Comment.]]><xsl:value-of select="Basis"/><![CDATA[Id);               ]]>  
</xsl:otherwise>    
</xsl:choose> 
           
<xsl:choose>
<xsl:when test="Table ='Public Commands 8  Comment Save  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Public Commands 8  Comment Save  ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">      <![CDATA[ 
            SelectedMainTabIndex = 7;
            SelectedDetailSubTabIndex = 7;

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        #endregion "Public Commands"                ]]>             
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">          <![CDATA[ 
            SelectedMainTabIndex = 7;
            SelectedDetailSubTabIndex = 7;

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.Refresh();
        }
        #endregion "Public Commands"                ]]>                      
</xsl:when>
<xsl:when test="Table ='Tbl68Speciesgroups'">               
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">          
</xsl:when>
<xsl:when test="Table ='Tbl81Images'">          
</xsl:when>
<xsl:when test="Table ='Tbl84Synonyms'">          
</xsl:when>
<xsl:when test="Table ='Tbl87Geographics'">          
</xsl:when>
<xsl:when test="Table ='Tbl90References'">          
</xsl:when>
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 

            CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            CommentsView.MoveCurrentToFirst();
        }
        #endregion [Methods ]]><xsl:value-of select="Basis"/><![CDATA[ ==> Tbl93Comments]               ]]>  
</xsl:otherwise>    
</xsl:choose> 
             
<![CDATA[ //    Part 9    ]]>


<xsl:choose>
<xsl:when test="Table ='++++++DoubleClick Header 1++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++DoubleClick Header 1++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>    <![CDATA[ 
        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {         ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++DoubleClick Header 2++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++DoubleClick Header 2++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">   <![CDATA[  
            ]]><xsl:value-of select="TableTK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

            ]]><xsl:value-of select="Table"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>("]]><xsl:value-of select="BasisSm"/><![CDATA[");

            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.Refresh();    

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;            ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">   <![CDATA[  
            ]]><xsl:value-of select="TableFK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id);

            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();    ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">   <![CDATA[  
            ]]><xsl:value-of select="TableFK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id);

            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();    ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[   
            ]]><xsl:value-of select="TableFK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id);

            ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[>("]]><xsl:value-of select="BasisSmBK1"/><![CDATA[");
   
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh(); 

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 2;            ]]>
</xsl:when> 
<xsl:when test="Table ='Tbl21Classes'">      <![CDATA[   
            ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[>("]]><xsl:value-of select="BasisSmBK1"/><![CDATA[");
            ]]><xsl:value-of select="TableBK2"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[>("]]><xsl:value-of select="BasisSmBK2"/><![CDATA[");

            ]]><xsl:value-of select="TableFK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id);
 
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();    ]]>
</xsl:when> 
<xsl:when test="Table ='Tbl68Speciesgroups'">   <![CDATA[  
            ]]><xsl:value-of select="TableTK1"/><![CDATA[List =  new ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(
                    _businessLayer.List]]><xsl:value-of select="TableTK1"/><![CDATA[By]]><xsl:value-of select="Basis"/><![CDATA[Id(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="ID"/><![CDATA[));

            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.Refresh();   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">      <![CDATA[  
            Tbl66GenussesList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66GenussesByGenusId(CurrentTbl69FiSpecies.GenusID));

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();   ]]> 
</xsl:when>   
<xsl:when test="Table ='Tbl72PlSpeciesses'">     <![CDATA[  
            Tbl66GenussesList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66GenussesByGenusId(CurrentTbl72PlSpecies.GenusID));

            GenussesView = CollectionViewSource.GetDefaultView(Tbl66GenussesList);
            GenussesView.Refresh();   ]]> 
</xsl:when>   
<xsl:when test="Table ='Tbl78Names'">      <![CDATA[  
            Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68Speciesgroups());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>( _businessLayer.ListTbl66Genusses());

            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>( _businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl78Name.FiSpeciesID)); 

            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();    ]]>
</xsl:when>   
<xsl:when test="Table ='Tbl81Images'">      <![CDATA[  
            Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68Speciesgroups());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());

            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(
                _businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl81Image.FiSpeciesID));

            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();     ]]>
</xsl:when>   
<xsl:when test="Table ='Tbl84Synonyms'">     <![CDATA[  
            Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>( _businessLayer.ListTbl68Speciesgroups());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>( _businessLayer.ListTbl66Genusses());

            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(
                _businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl84Synonym.FiSpeciesID));

            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();    ]]>
</xsl:when>   
<xsl:when test="Table ='Tbl87Geographics'">      <![CDATA[  
            Tbl68SpeciesgroupsAllList = new ObservableCollection<Tbl68Speciesgroup>(_businessLayer.ListTbl68Speciesgroups());
            Tbl66GenussesAllList = new ObservableCollection<Tbl66Genus>(_businessLayer.ListTbl66Genusses());

            Tbl69FiSpeciessesList = new ObservableCollection<Tbl69FiSpecies>(
                _businessLayer.ListTbl69FiSpeciessesByFiSpeciesId(CurrentTbl87Geographic.FiSpeciesID));

            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();     ]]>
</xsl:when>   
<xsl:when test="Table ='Tbl90References'">   <![CDATA[  
            ]]><xsl:value-of select="TableFK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id);
 
            Tbl90RefSourcesList?.Clear();

            Tbl90RefSourcesList = new ObservableCollection<Tbl90RefSource>(
                _businessLayer.ListTbl90RefSourcesByRefSourceId(CurrentTbl90Reference.RefSourceID));

            Tbl90RefAuthorsList?.Clear();

            Tbl90RefAuthorsList = new ObservableCollection<Tbl90RefAuthor>(
                _businessLayer.ListTbl90RefAuthorsByRefAuthorId(CurrentTbl90Reference.RefAuthorID));

            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();    ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefExperts'"> 
</xsl:when>
<xsl:when test="Table ='Tbl90RefSources'"> 
</xsl:when>
<xsl:when test="Table ='Tbl93Comments'"> 
</xsl:when>
<xsl:when test="Table ='TblCountries'"> 
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'"> 
</xsl:when>
<xsl:otherwise>    <![CDATA[ 
            ]]><xsl:value-of select="TableFK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id);

            ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[>("]]><xsl:value-of select="BasisSmBK1"/><![CDATA[");

            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
            ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();    ]]> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++DoubleClick Footer 1++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++DoubleClick Footer 1++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>    <![CDATA[ 
        }

        #endregion "Public Method Connected Tables by DoubleClick"     ]]>
</xsl:otherwise>    
</xsl:choose> 


<![CDATA[ //    Part 10    ]]>

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items Top 1+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>    <![CDATA[ 
        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;

        public  int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex; 
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; RaisePropertyChanged("");      ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 2 SelectedMainTabIndex 0+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 2 SelectedMainTabIndex 0+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">      <![CDATA[ 
                if (_selectedMainTabIndex == 0)             
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ]]><xsl:value-of select="Table"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>("]]><xsl:value-of select="BasisSm"/><![CDATA[");

                        ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.Refresh();
                    }
                    SelectedDetailTabIndex = 1;
                }       ]]>  
</xsl:when> 
<xsl:when test="Table ='Tbl06Phylums'">      <![CDATA[ 
                if (_selectedMainTabIndex == 0)             
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id);

                        ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }       ]]>  
</xsl:when> 
<xsl:when test="Table ='Tbl09Divisions'">      <![CDATA[ 
                if (_selectedMainTabIndex == 0)             
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id);

                        ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }       ]]>  
</xsl:when> 
<xsl:otherwise>    <![CDATA[ 
                if (_selectedMainTabIndex == 0)             
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id);

                        ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[>("]]><xsl:value-of select="BasisSmBK1"/><![CDATA[");

                        ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();
                    }
                    SelectedDetailTabIndex = 0;
                }       ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 2 SelectedMainTabIndex 1+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 2 SelectedMainTabIndex 1+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>
<xsl:when test="Table ='Tbl03Regnums'">      <![CDATA[ 
                if (_selectedMainTabIndex == 1)             
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableTK2"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissTK2"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ]]><xsl:value-of select="Table"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>("]]><xsl:value-of select="BasisSm"/><![CDATA[");

                        ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.Refresh();
                    }
                    SelectedDetailTabIndex = 2;
                }       ]]>  
</xsl:when>   
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedMainTabIndex == 1)             
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableFK2"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissFK2"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK2"/><![CDATA[Id<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Id);

                        ]]><xsl:value-of select="TableBK2"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[>("]]><xsl:value-of select="BasisSmBK2"/><![CDATA[");

                        ]]><xsl:value-of select="BasissFK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK2"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissFK2"/><![CDATA[View.Refresh();
                    }
                    SelectedDetailTabIndex = 1;
                }       ]]>  
</xsl:when>   
<xsl:otherwise>    <![CDATA[ 
                if (_selectedMainTabIndex == 1)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ]]><xsl:value-of select="Table"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>("]]><xsl:value-of select="BasisSm"/><![CDATA[");

                        ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.Refresh();
                    }
                    SelectedDetailTabIndex = 2;   
               }    ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 2 SelectedMainTabIndex 2+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 2 SelectedMainTabIndex 2+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedMainTabIndex == 2)             
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ]]><xsl:value-of select="Table"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>("]]><xsl:value-of select="BasisSm"/><![CDATA[");

                        ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                }       ]]>  
</xsl:when>    
<xsl:otherwise>    <![CDATA[ 
                if (_selectedMainTabIndex == 2)
                {
                        SelectedDetailTabIndex = 3;
                        SelectedMainSubRefTabIndex = 0;                  
                }         ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 2 SelectedMainTabIndex 3+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 2 SelectedMainTabIndex 3+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedMainTabIndex == 3)
                {
                        SelectedDetailTabIndex = 4;
                        SelectedMainSubRefTabIndex = 0;     
                }         ]]>  
</xsl:when>     
<xsl:otherwise>    <![CDATA[ 
                if (_selectedMainTabIndex == 3)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<Tbl93Comment>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedDetailTabIndex = 6;
                }      ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 2 SelectedMainTabIndex 4+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 2 SelectedMainTabIndex 4+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedMainTabIndex == 4)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<Tbl93Comment>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedDetailTabIndex = 7;
                }         ]]>  
</xsl:when>     
<xsl:otherwise> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>    <![CDATA[ 
            }
        }

        public  int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex; 
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value;    RaisePropertyChanged("");     ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 0  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 0  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">      <![CDATA[ 
                if (_selectedDetailTabIndex == 0)
                {
                    SelectedMainTabIndex = 0;  
                 }     ]]>  
</xsl:when> 
<xsl:otherwise>    <![CDATA[ 
                if (_selectedDetailTabIndex == 0)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableFK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissFK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK1"/><![CDATA[Id<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK1"/><![CDATA[Id);

                        ]]><xsl:value-of select="BasissFK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK1"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissFK1"/><![CDATA[View.Refresh();
                    }
                    SelectedMainTabIndex = 0;  
               }   ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 1  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 1  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedDetailTabIndex == 1)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableFK2"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissFK2"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="BasisFK2"/><![CDATA[Id<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="BasisFK2"/><![CDATA[Id);

                        ]]><xsl:value-of select="TableBK2"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[>("]]><xsl:value-of select="BasisSmBK2"/><![CDATA[");

                        ]]><xsl:value-of select="BasissFK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableFK2"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissFK2"/><![CDATA[View.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }         ]]>  
</xsl:when>     
<xsl:otherwise>    <![CDATA[ 
                if (_selectedDetailTabIndex == 1)                
                {
                    SelectedMainTabIndex = 0;
                }  ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 2  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 2  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">      <![CDATA[ 
                if (_selectedDetailTabIndex == 2)                
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableTK2"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissTK2"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ]]><xsl:value-of select="Table"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>("]]><xsl:value-of select="BasisSm"/><![CDATA[");

                        ]]><xsl:value-of select="BasissTK2"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK2"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissTK2"/><![CDATA[View.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }  ]]>  
</xsl:when>
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedDetailTabIndex == 2)                
                {
                    SelectedMainTabIndex = 0;
                }  ]]>  
</xsl:when>        
<xsl:otherwise>    <![CDATA[ 
                if (_selectedDetailTabIndex == 2)                
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ]]><xsl:value-of select="Table"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>("]]><xsl:value-of select="BasisSm"/><![CDATA[");

                        ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.Refresh();
                    }
                    SelectedMainTabIndex = 1;
               }  ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 3  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 3  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedDetailTabIndex == 3)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        ]]><xsl:value-of select="TableTK1"/><![CDATA[List = _extGet.Get]]><xsl:value-of select="BasissTK1"/><![CDATA[CollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ]]><xsl:value-of select="Table"/><![CDATA[AllList = _extGet.AllCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[>("]]><xsl:value-of select="BasisSm"/><![CDATA[");

                        ]]><xsl:value-of select="BasissTK1"/><![CDATA[View = CollectionViewSource.GetDefaultView(]]><xsl:value-of select="TableTK1"/><![CDATA[List);
                        ]]><xsl:value-of select="BasissTK1"/><![CDATA[View.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                }         ]]>  
</xsl:when>      
<xsl:otherwise>    <![CDATA[ 
                if (_selectedDetailTabIndex == 3)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }      ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 4  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 4  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedDetailTabIndex == 4)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                    SelectedMainSubRefTabIndex = 0;
                }         ]]>  
</xsl:when>      
<xsl:otherwise>    <![CDATA[ 
                if (_selectedDetailTabIndex == 4)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }      ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 5  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 5  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedDetailTabIndex == 5)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                    SelectedMainSubRefTabIndex = 1;
                }         ]]>  
</xsl:when>       
<xsl:otherwise>    <![CDATA[ 
                if (_selectedDetailTabIndex == 5)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }     ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 6  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 6  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedDetailTabIndex == 6)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                    SelectedMainSubRefTabIndex = 2;
                }         ]]>  
</xsl:when>       
<xsl:otherwise>    <![CDATA[ 
                if (_selectedDetailTabIndex == 6)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<Tbl93Comment>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }     ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 7  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedDetailTabIndex 7  +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>    <![CDATA[ 
                if (_selectedDetailTabIndex == 7)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl93CommentsList = _extGet.GetCommentsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[Id<Tbl93Comment>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        CommentsView.Refresh();
                    }
                    SelectedMainTabIndex = 4;
                }     ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedMainSubRefTabIndex+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedMainSubRefTabIndex+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>    <![CDATA[ 
            }
        }

        public int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex;
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value;  RaisePropertyChanged("");   ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedMainSubRefTabIndex 0 +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedMainSubRefTabIndex 0 +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedMainSubRefTabIndex == 0)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 3;
                }      ]]>  
</xsl:when>       
<xsl:otherwise>    <![CDATA[ 
                if (_selectedMainSubRefTabIndex == 0)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl90ExpertsAllList = new ObservableCollection<Tbl90RefExpert>(_uow.Tbl90RefExperts.ListTbl90RefExpertsOrderBy());

                        Tbl90ReferenceExpertsList = _extGet.GetReferenceExpertsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefSourceIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                }      ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedMainSubRefTabIndex 1 +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedMainSubRefTabIndex 1 +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 5;
                    SelectedMainTabIndex = 3;
                }    ]]>  
</xsl:when>       
<xsl:otherwise>    <![CDATA[ 
                if (_selectedMainSubRefTabIndex == 1)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl90SourcesAllList = new ObservableCollection<Tbl90RefSource>(_uow.Tbl90RefSources.ListTbl90RefSourcesOrderBy());

                        Tbl90ReferenceSourcesList = _extGet.GetReferenceSourcesCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefAuthorIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                    SelectedMainTabIndex = 2;
                }    ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedMainSubRefTabIndex 2 +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Top 1 SelectedMainSubRefTabIndex 2 +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">      <![CDATA[ 
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedDetailTabIndex = 6;
                    SelectedMainTabIndex = 3;
                }    ]]>  
</xsl:when>       
<xsl:otherwise>    <![CDATA[ 
                if (_selectedMainSubRefTabIndex == 2)
                {
                    if (Current]]><xsl:value-of select="LinqModel"/><![CDATA[ != null)
                    {
                        Tbl90AuthorsAllList = new ObservableCollection<Tbl90RefAuthor>(_uow.Tbl90RefAuthors.ListTbl90RefAuthorsOrderBy());

                        Tbl90ReferenceAuthorsList = _extGet.GetReferenceAuthorsCollectionOrderByFrom]]><xsl:value-of select="Basis"/><![CDATA[IdAndRefSourceIdIsNullAndRefExpertIdIsNull<Tbl90Reference>(Current]]><xsl:value-of select="LinqModel"/><![CDATA[.]]><xsl:value-of select="Basis"/><![CDATA[Id);

                        ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        ReferenceAuthorsView.Refresh();
                    }
                    SelectedDetailTabIndex = 5;
                    SelectedMainTabIndex = 2;
                }    ]]>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Open Detail Items  Footer 1 SelectedMainSubRefTabIndex   +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:when test="Table ='++++++Open Detail Items  Footer 1 SelectedMainSubRefTabIndex   +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++'">
</xsl:when>  
<xsl:otherwise>    <![CDATA[                 
            }
        }    
        #endregion "Public Commands to open Detail TabItems"        ]]>  
</xsl:otherwise>    
</xsl:choose> 

<![CDATA[ //    Part 11    ]]>

<xsl:choose>
<xsl:when test="Table ='++++++Properties Basis++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">    <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[ = "";
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        private   ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; RaisePropertyChanged("");   }
        }

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">    <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[ = "";
        public  string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        private   ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; RaisePropertyChanged("");   }
        }

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">    <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[ = "";
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        private   ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; RaisePropertyChanged("");   }
        }

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">    <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[ = "";
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        private   ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; RaisePropertyChanged("");   }
        }

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">    <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private int _searchImageId = 0;
        public int SearchImageId
        {
            get => _searchImageId;
            set { _searchImageId = value; RaisePropertyChanged(""); }
        }

        public  ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        private   ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">    <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private int _searchGeographicId = 0;
        public int SearchGeographicId
        {
            get => _searchGeographicId ;
            set { _searchGeographicId = value; RaisePropertyChanged(""); }
        }

        public  ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        private   ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">    <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Basis"/><![CDATA[Info = "";
        public string Search]]><xsl:value-of select="Basis"/><![CDATA[Info
        {
            get => _search]]><xsl:value-of select="Basis"/><![CDATA[Info; 
            set { _search]]><xsl:value-of select="Basis"/><![CDATA[Info = value; RaisePropertyChanged("");  }
        }

        public   ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        private  ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90RefExpert> _tbl90RefExpertsAllList;

        public ObservableCollection<Tbl90RefExpert> Tbl90RefExpertsAllList
        {
            get => _tbl90RefExpertsAllList;
            set  {  _tbl90RefExpertsAllList = value;  RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90RefSource> _tbl90RefSourcesAllList;

        public ObservableCollection<Tbl90RefSource> Tbl90RefSourcesAllList
        {
            get => _tbl90RefSourcesAllList;
            set  {  _tbl90RefSourcesAllList = value;  RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl90RefAuthor> _tbl90RefAuthorsAllList;

        public ObservableCollection<Tbl90RefAuthor> Tbl90RefAuthorsAllList
        {
            get => _tbl90RefAuthorsAllList;
            set  {  _tbl90RefAuthorsAllList = value;  RaisePropertyChanged("");  }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList;
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList;
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList;
            set { _tbl12SubphylumsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get => _tbl15SubdivisionsAllList;
            set { _tbl15SubdivisionsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList;
            set { _tbl18SuperclassesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;
        public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList;
            set { _tbl21ClassesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList;
            set { _tbl24SubclassesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList;
            set { _tbl27InfraclassesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
        {
            get => _tbl30LegiosAllList;
            set { _tbl30LegiosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
        {
            get => _tbl33OrdosAllList;
            set { _tbl33OrdosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList;
            set { _tbl36SubordosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
        {
            get => _tbl39InfraordosAllList;
            set { _tbl39InfraordosAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesAllList;
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesAllList
        {
            get => _tbl42SuperfamiliesAllList;
            set { _tbl42SuperfamiliesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get => _tbl45FamiliesAllList;
            set { _tbl45FamiliesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList;
        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
        {
            get => _tbl48SubfamiliesAllList;
            set { _tbl48SubfamiliesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList;
            set { _tbl51InfrafamiliesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList;
        public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
        {
            get => _tbl54SupertribussesAllList;
            set { _tbl54SupertribussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList;
            set { _tbl57TribussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList;
            set { _tbl60SubtribussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList;
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
        {
            get => _tbl63InfratribussesAllList;
            set { _tbl63InfratribussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList;
            set { _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList;
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl93Comments'">      <![CDATA[  
        #region "Public Properties Tbl93Comment"

        private string _searchCommentInfo = "";
        public string SearchCommentInfo
        {
            get => _searchCommentInfo;
            set { _searchCommentInfo = value; RaisePropertyChanged(""); }
        }

        public ICollectionView CommentsView;
        public Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList;
            set { _tbl93CommentsList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl93Comment> _tbl93CommentsAllList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsAllList
        {
            get => _tbl93CommentsAllList;
            set { _tbl93CommentsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList;
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList;
            set { _tbl09DivisionsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList;
            set { _tbl12SubphylumsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get => _tbl15SubdivisionsAllList;
            set { _tbl15SubdivisionsAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList;
            set { _tbl18SuperclassesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;
        public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList;
            set { _tbl21ClassesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList;
            set { _tbl24SubclassesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList;
            set { _tbl27InfraclassesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
        {
            get => _tbl30LegiosAllList;
            set { _tbl30LegiosAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
        {
            get => _tbl33OrdosAllList;
            set { _tbl33OrdosAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList;
            set { _tbl36SubordosAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
        {
            get => _tbl39InfraordosAllList;
            set { _tbl39InfraordosAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesAllList;
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesAllList
        {
            get => _tbl42SuperfamiliesAllList;
            set { _tbl42SuperfamiliesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get => _tbl45FamiliesAllList;
            set { _tbl45FamiliesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList;
        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
        {
            get => _tbl48SubfamiliesAllList;
            set { _tbl48SubfamiliesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList;
            set { _tbl51InfrafamiliesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList;
        public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
        {
            get => _tbl54SupertribussesAllList;
            set { _tbl54SupertribussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList;
            set { _tbl57TribussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList;
            set { _tbl60SubtribussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList;
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
        {
            get => _tbl63InfratribussesAllList;
            set { _tbl63InfratribussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList;
            set { _tbl69FiSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList;
            set { _tbl72PlSpeciessesAllList = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"     ]]>
</xsl:when>
<xsl:when test="Table ='TblUserProfiles'">      <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _searchEmail = "";
        public string SearchEmail
        {
            get => _searchEmail; 
            set { _searchEmail = value; RaisePropertyChanged();  }
        }

        public  ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        private   ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; RaisePropertyChanged();   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; RaisePropertyChanged();   }
        }

        private string _passwordBox;
        public string PasswordBox
        {
            get => _passwordBox;

            set { _passwordBox = value; RaisePropertyChanged("PasswordBox"); }
        }

        private void GetValueRole()
        {
            _roles = new List<Role>()
            {
                new Role {Name = "Administrator"},
                new Role {Name = "Developer"},
                new Role {Name = "Biologist"},
                new Role {Name = "Zoologist"},
                new Role {Name = "User"},};

            _selectedRole = new Role();
        }

        private void GetValueTitle()
        {
            _titles = new List<Title>()
            {
                new Title {Name = ""},
                new Title {Name = "Dr."},
                new Title {Name = "Prof."},
                new Title {Name = "Dipl.-Ing."},
                new Title {Name = "Ing. grad"},
                new Title {Name = "Dr. Ing"}
            };

            _selectedTitle = new Title();
        }

        private void GetValueGender()
        {
            _genders = new List<Gender>()
            {
                new Gender {Name = "Female"},
                new Gender {Name = "Male"}
            };

            _selectedGender = new Gender();
        }

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get => _tblCountriesAllList;
            set { _tblCountriesAllList = value; RaisePropertyChanged(); }
        }

        //-----------------------------------------------------------
        private List<Role> _roles;
        public List<Role> Roles
        {
            get => _roles;
            set { _roles = value; RaisePropertyChanged(); }
        }

        private Role _selectedRole;
        public Role SelectedRole
        {
            get => _selectedRole;
            set { _selectedRole = value; RaisePropertyChanged(); }
        }

        private List<Gender> _genders;
        public List<Gender> Genders
        {
            get => _genders;
            set { _genders = value; RaisePropertyChanged(); }
        }

        private Gender _selectedGender;
        public Gender SelectedGender
        {
            get => _selectedGender;
            set { _selectedGender = value; RaisePropertyChanged(); }
        }

        private List<Title> _titles;
        public List<Title> Titles
        {
            get => _titles;
            set { _titles = value; RaisePropertyChanged(); }
        }

        private Title _selectedTitle;
        public Title SelectedTitle
        {
            get => _selectedTitle;
            set { _selectedTitle = value; RaisePropertyChanged(); }
        }

        #endregion "Public Properties"      


        //    Part 12    



        #region "Private Classes"

        public class Gender
        {
            public string Name
            {
                get;
                set;
            }
        }
        public class Title
        {
            public string Name
            {
                get;
                set;
            }
        }

        public class Role
        {
            public string Name
            {
                get;
                set;
            }
        }

        #endregion "Private Methods"  ]]>
</xsl:when>  
<xsl:otherwise>   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[ = "";
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; RaisePropertyChanged("");  }
        }

        public  ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        private   ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; RaisePropertyChanged("");   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="EntitysTK1"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="TableTK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysTK1"/><![CDATA[AllList; 
            set {  ]]><xsl:value-of select="EntitysTK1"/><![CDATA[AllList = value; RaisePropertyChanged("");   }
        }

        #endregion "Public Properties"   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties FK1 ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">   
</xsl:when>  
<xsl:when test="Table ='Tbl68Speciesgroups'">
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private ObservableCollection<Tbl66Genus> _tbl66GenussesList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesList
        {
            get => _tbl66GenussesList;
            set { _tbl66GenussesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"  ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        public  ICollectionView GenussesView;
        private Tbl66Genus CurrentTbl66Genus => GenussesView?.CurrentItem as Tbl66Genus;

        private ObservableCollection<Tbl66Genus> _tbl66GenussesList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesList
        {
            get => _tbl66GenussesList;
            set { _tbl66GenussesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"  ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        public  ICollectionView GenussesView;
        private Tbl66Genus CurrentTbl66Genus => GenussesView?.CurrentItem as Tbl66Genus;

        private ObservableCollection<Tbl66Genus> _tbl66GenussesList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesList
        {
            get => _tbl66GenussesList;
            set { _tbl66GenussesList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public  ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"  ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = string.Empty;
        public  string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get  => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; RaisePropertyChanged(""); }
        }

        public  ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        private ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"  ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">    
</xsl:when> 
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when> 
<xsl:when test="Table ='TblCountries'">    
</xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">    
</xsl:when> 
<xsl:otherwise>   
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        public  ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        private ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties FK2 ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">
  <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK2"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK2"/><![CDATA[ = string.Empty;
        public  string Search]]><xsl:value-of select="NameFK2"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK2"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK2"/><![CDATA[ = value; RaisePropertyChanged(""); }
        }

        public  ICollectionView ]]><xsl:value-of select="BasissFK2"/><![CDATA[View;
        private  ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ => ]]><xsl:value-of select="BasissFK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List;
        public   ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"  ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">    
</xsl:when> 
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when> 
<xsl:when test="Table ='TblCountries'">    
</xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">    
</xsl:when> 
<xsl:otherwise>    
  <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK2"/><![CDATA["

        public  ICollectionView ]]><xsl:value-of select="BasissFK2"/><![CDATA[View;
        private  ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ => ]]><xsl:value-of select="BasissFK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List;
        public   ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties TK1 ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">
  <xsl:if test="TableTK1 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameTK1"/><![CDATA[ = string.Empty;
        public string Search]]><xsl:value-of select="NameTK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameTK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameTK1"/><![CDATA[ = value; RaisePropertyChanged(""); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissTK1"/><![CDATA[View;
        private ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ => ]]><xsl:value-of select="BasissTK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="TableTK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">    
</xsl:when> 
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when> 
<xsl:when test="Table ='TblCountries'">    
</xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">    
</xsl:when> 
<xsl:otherwise>    
  <xsl:if test="TableTK1 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK1"/><![CDATA["

        public ICollectionView ]]><xsl:value-of select="BasissTK1"/><![CDATA[View;
        private ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ => ]]><xsl:value-of select="BasissTK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="TableTK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties TK2 ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl66Genusses'">
  <xsl:if test="TableTK2 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK2"/><![CDATA["

        public ICollectionView ]]><xsl:value-of select="BasissTK2"/><![CDATA[View;
        private ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ => ]]><xsl:value-of select="BasissTK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="TableTK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties" 

        #region "Public Properties Tbl68Speciesgroup"

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList;
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"      ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">    
</xsl:when> 
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when> 
<xsl:when test="Table ='TblCountries'">    
</xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">    
</xsl:when> 
<xsl:otherwise>    
  <xsl:if test="TableTK2 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK2"/><![CDATA["

        public ICollectionView ]]><xsl:value-of select="BasissTK2"/><![CDATA[View;
        private ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ => ]]><xsl:value-of select="BasissTK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="TableTK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties TK3 ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
  <xsl:if test="TableTK3 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK3"/><![CDATA["

        public ICollectionView ]]><xsl:value-of select="BasissTK3"/><![CDATA[View;
        private ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[ => ]]><xsl:value-of select="BasissTK3"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> ]]><xsl:value-of select="EntitysTK3"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> ]]><xsl:value-of select="TableTK3"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK3"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK3"/><![CDATA[List = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">    
</xsl:when> 
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when> 
<xsl:when test="Table ='TblCountries'">    
</xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">    
</xsl:when> 
<xsl:otherwise>    
  <xsl:if test="TableTK3 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK3"/><![CDATA["

        private string _search]]><xsl:value-of select="NameTK3"/><![CDATA[ = string.Empty;
        public string Search]]><xsl:value-of select="NameTK3"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameTK3"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameTK3"/><![CDATA[ = value; RaisePropertyChanged(""); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissTK3"/><![CDATA[View;
        private ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[ => ]]><xsl:value-of select="BasissTK3"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> ]]><xsl:value-of select="EntitysTK3"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> ]]><xsl:value-of select="TableTK3"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK3"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK3"/><![CDATA[List = value; RaisePropertyChanged(""); }
        }
        private ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> ]]><xsl:value-of select="EntitysTK3"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> ]]><xsl:value-of select="TableTK3"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysTK3"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysTK3"/><![CDATA[AllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties TK4 ++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">
  <xsl:if test="TableTK4 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK4"/><![CDATA["

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[> ]]><xsl:value-of select="EntitysTK4"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[> ]]><xsl:value-of select="TableTK4"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK4"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK4"/><![CDATA[List = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">    
</xsl:when> 
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when> 
<xsl:when test="Table ='TblCountries'">    
</xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">    
</xsl:when> 
<xsl:otherwise>    
  <xsl:if test="TableTK4 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK4"/><![CDATA["

        private string _search]]><xsl:value-of select="NameTK4"/><![CDATA[ = string.Empty;
        public string Search]]><xsl:value-of select="NameTK4"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameTK4"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameTK4"/><![CDATA[ = value; RaisePropertyChanged(""); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissTK4"/><![CDATA[View;
        private ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ => ]]><xsl:value-of select="BasissTK4"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[> ]]><xsl:value-of select="EntitysTK4"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[> ]]><xsl:value-of select="TableTK4"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK4"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK4"/><![CDATA[List = value; RaisePropertyChanged(""); }
        }
        private ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[> ]]><xsl:value-of select="EntitysTK4"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[> ]]><xsl:value-of select="TableTK4"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysTK4"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysTK4"/><![CDATA[AllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties BK1 ++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">
</xsl:when>   
<xsl:when test="Table ='Tbl69FiSpeciesses'">
  <xsl:if test="TableBK1 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelBK1"/><![CDATA["

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="EntitysBK1"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysBK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysBK1"/><![CDATA[AllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl72Speciesses'">
  <xsl:if test="TableBK1 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelBK1"/><![CDATA["

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="EntitysBK1"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysBK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysBK1"/><![CDATA[AllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">    
</xsl:when> 
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when> 
<xsl:when test="Table ='TblCountries'">    
</xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">    
</xsl:when> 
<xsl:otherwise>    
  <xsl:if test="TableBK1 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelBK1"/><![CDATA["

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="EntitysBK1"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysBK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysBK1"/><![CDATA[AllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties BK2 ++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl03Regnums'">
</xsl:when>   
<xsl:when test="Table ='Tbl69FiSpeciesses'">
  <xsl:if test="TableBK2 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelBK2"/><![CDATA["

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[> ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[> ]]><xsl:value-of select="TableBK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl72Speciesses'">
  <xsl:if test="TableBK2 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelBK2"/><![CDATA["

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[> ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[> ]]><xsl:value-of select="TableBK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">    
</xsl:when> 
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when> 
<xsl:when test="Table ='TblCountries'">    
</xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">    
</xsl:when> 
<xsl:otherwise>    
  <xsl:if test="TableBK2 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelBK2"/><![CDATA["

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[> ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[> ]]><xsl:value-of select="TableBK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList = value; RaisePropertyChanged(""); }       
        }

        #endregion "Public Properties"    ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties Continent ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">     <![CDATA[  
        #region "Private Methods"

        private void GetValueContinent()
        {
            _continents = new List<Continent>()
            {
                new Continent {Name = "Africa"},
                new Continent {Name = "Antarctica"},
                new Continent {Name = "Asia"},
                new Continent {Name = "Australia"},
                new Continent {Name = "Central/South America"},
                new Continent {Name = "Europe"},
                new Continent {Name = "North America/Caribbean"}
            };

            _selectedContinent = new Continent();
        }

        private List<Continent> _continents;
        public List<Continent> Continents
        {
            get => _continents;
            set {  _continents = value;  RaisePropertyChanged(""); }
        }

        private Continent _selectedContinent;
        public Continent SelectedContinent
        {
            get => _selectedContinent;
            set  {  _selectedContinent = value;  RaisePropertyChanged("");  }
        }

        public class Continent
        {
            public string Name { get; set; }
        }

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList;
            set  {  _tblCountriesList = value;  RaisePropertyChanged("");  }
        }

        #endregion "Private Methods"      ]]> 
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">     <![CDATA[  
        #region "Private Methods"

        private void GetValueContinent()
        {
            _continents = new List<Continent>()
            {
                new Continent {Name = "Africa"},
                new Continent {Name = "Antarctica"},
                new Continent {Name = "Asia"},
                new Continent {Name = "Australia"},
                new Continent {Name = "Central/South America"},
                new Continent {Name = "Europe"},
                new Continent {Name = "North America/Caribbean"}
            };

            _selectedContinent = new Continent();
        }

        private List<Continent> _continents;
        public List<Continent> Continents
        {
            get => _continents;
            set {  _continents = value;  RaisePropertyChanged(""); }
        }

        private Continent _selectedContinent;
        public Continent SelectedContinent
        {
            get => _selectedContinent;
            set  {  _selectedContinent = value;  RaisePropertyChanged("");  }
        }

        public class Continent
        {
            public string Name { get; set; }
        }

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList;
            set  {  _tblCountriesList = value;  RaisePropertyChanged("");  }
        }

        #endregion "Private Methods"      ]]> 
</xsl:when> 
<xsl:when test="Table ='Tbl81Images'">     <![CDATA[  
        #region Video
        public bool IsBusy
        {
            get => _isBusy;
            private set
            {
                Set(ref _isBusy, value);
                GetVideoCommand.RaiseCanExecuteChanged();
                DownloadMediaStreamCommand.RaiseCanExecuteChanged();
            }
        }

        public string Query
        {
            get => _query;
            set
            {
                Set(ref _query, value);
                GetVideoCommand.RaiseCanExecuteChanged();
            }
        }

        public Video Video
        {
            get => _video;
            private set
            {
                Set(ref _video, value);
                RaisePropertyChanged(() => IsVideoAvailable);
            }
        }

        public bool IsVideoAvailable => Video != null;

        public double Progress
        {
            get => _progress;
            private set => Set(ref _progress, value);
        }

        public bool IsProgressIndeterminate
        {
            get => _isProgressIndeterminate;
            private set => Set(ref _isProgressIndeterminate, value);
        }


        // Commands
        public GalaSoft.MvvmLight.CommandWpf.RelayCommand GetVideoCommand { get; }
        public RelayCommand<MediaStreamInfo> DownloadMediaStreamCommand { get; }
        public RelayCommand<ClosedCaptionTrackInfo> DownloadClosedCaptionTrackCommand { get; }

        private async void GetVideo()
        {
            IsBusy = true;
            IsProgressIndeterminate = true;

            // Reset data
            Video = null;

            // Parse URL if necessary
            if (!YoutubeClient.TryParseVideoId(Query, out string videoId))
                videoId = Query;

            // Perform the request
            Video = await _client.GetVideoAsync(videoId);

            IsBusy = false;
            IsProgressIndeterminate = false;

        }

        private  void DownloadMediaStream(MediaStreamInfo info)
        {
            // Create dialog
            var fileExt = info.Container.GetFileExtension();
            var defaultFileName = $"{Video.Title}.{fileExt}"
                .Replace(Path.GetInvalidFileNameChars(), '_');
            var sfd = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = fileExt,
                FileName = defaultFileName,
                Filter = $"{info.Container} Files|*.{fileExt}|All Files|*.*"
            };

            // Select file path
            if (sfd.ShowDialog() != true)
                return;

            var filePath = sfd.FileName;

            // Download to file
            IsBusy = true;
            Progress = 0;

            var progressHandler = new Progress<double>(p => Progress = p);
             _client.DownloadMediaStreamAsync(info, filePath, progressHandler);

            IsBusy = false;
            Progress = 0;
        }

        private async void DownloadClosedCaptionTrack(ClosedCaptionTrackInfo info)
        {
            // Create dialog
            var fileExt = $"{Video.Title}.{info.Language.Name}.srt"
                .Replace(Path.GetInvalidFileNameChars(), '_');
            var filter = "SRT Files|*.srt|All Files|*.*";
            var sfd = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = "srt",
                FileName = fileExt,
                Filter = filter
            };

            // Select file path
            if (sfd.ShowDialog() != true)
                return;

            var filePath = sfd.FileName;

            // Download to file
            IsBusy = true;
            Progress = 0;

            var progressHandler = new Progress<double>(p => Progress = p);
            await _client.DownloadClosedCaptionTrackAsync(info, filePath, progressHandler);

            IsBusy = false;
            Progress = 0;
        } 
        #endregion ]]>
</xsl:when>  
<xsl:otherwise>    
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Properties References ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Rqqqqqegnums'">     <![CDATA[  
        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList; 
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList; 
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList; 
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90ReferenceAuthor"

        public ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList; 
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        public ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList; 
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        public ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList; 
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl68Speciesgroups'">
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">    
</xsl:when> 
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when> 
<xsl:when test="Table ='TblCountries'">    
</xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">    
</xsl:when> 
<xsl:otherwise>       <![CDATA[  
        #region Public Properties Tbl90References

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesList;

        public ObservableCollection<Tbl90Reference> Tbl90ReferencesList
        {
            get => _tbl90ReferencesList;
            set { _tbl90ReferencesList = value; RaisePropertyChanged(""); }
        }

        #endregion

        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public  ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList; 
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public  ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList; 
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList; 
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90ReferenceAuthor"

        public ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList; 
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        public ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList; 
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        public ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList; 
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties Comments ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">    <![CDATA[  
        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList; 
            set { _tbl93CommentsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"     ]]>

</xsl:when>  
<xsl:when test="Table ='Tbl68Speciesgroups'">
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">    
</xsl:when> 
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when> 
<xsl:when test="Table ='TblCountries'">    
</xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">    
</xsl:when> 
<xsl:otherwise>       <![CDATA[  
        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList; 
            set { _tbl93CommentsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"     ]]>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties language ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">    <![CDATA[  
        #region "Public Property  TblCountry"

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get { return _tblCountriesAllList; }
            set { _tblCountriesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"  

        private void GetValueLanguage()
             {
                 _languages = new List<Language>()
                 {
                     new Language {Name = "GER"},
                     new Language {Name = "ENG"},
                     new Language {Name = "FRE"},
                     new Language {Name = "POR"}
                 };

                 _selectedLanguage = new Language();
             }

             private List<Language> _languages;

             public List<Language> Languages
             {
                 get => _languages;
                 set { _languages = value; RaisePropertyChanged(""); }
             }

             private Language _selectedLanguage;

             public Language SelectedLanguage
             {
                 get => _selectedLanguage;
                 set { _selectedLanguage = value; RaisePropertyChanged(""); }
             }

             public class Language
             {
                 public string Name
                 {
                     get;
                     set;
                 }
             }

 
             #region Mimetype

             private void GetValueMimeType()
             {
                 _mimeTypes = new List<MimeType>()
                 {
                     new MimeType {Name = "jpg"},
                     new MimeType {Name = "png"},
                     new MimeType {Name = "bmp"},
                     new MimeType {Name = "tiff"},
                     new MimeType {Name = "gif"},
                     new MimeType {Name = "icon"},
                     new MimeType {Name = "jpeg"},
                     new MimeType {Name = "wmf"},
                     new MimeType {Name = "wmv"},
                     new MimeType {Name = "mpg"},
                     new MimeType {Name = "mp4"},
                     new MimeType {Name = "avi"},
                     new MimeType {Name = "mov"},
                     new MimeType {Name = "swf"},
                     new MimeType {Name = "flv"}
                 };

                 _selectedMimeType = new MimeType();
             }

             private List<MimeType> _mimeTypes;

             public List<MimeType> MimeTypes
             {
                 get => _mimeTypes;
                 set {  _mimeTypes = value;  RaisePropertyChanged("");  }
             }

             private MimeType _selectedMimeType;
             public MimeType SelectedMimeType
             {
                 get => _selectedMimeType;
                 set  {  _selectedMimeType = value;  RaisePropertyChanged(""); }
             }

             public class MimeType
             {
                 public string Name { get; set; }
             }
        #endregion

        #region OpenfileDialog

        public static RelayCommand OpenCommand { get; set; }
        private string _selectedPath;

        public string SelectedPath
        {
            get => _selectedPath;
            set { _selectedPath = value; RaisePropertyChanged(""); }
        }

        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get => _imageSource;
            set { _imageSource = value; RaisePropertyChanged(""); }
        }

        public readonly string DefaultPath;

        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
        }

        private void ExecuteOpenFileDialog(object o)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select A File",
                InitialDirectory = DefaultPath,
                Filter = "All images|*.jpg;*.jpeg;*.jpe;*.bmp;*.gif;*.ico;*.png;*.tif;*.tiff;*.hpd;*.jxr;*.wdp|" +
                         "JPEG image|*.jpg;*.jpeg;*.jpe|Windows BMP image|*.bmp|GIF image|*.gif|Microsoft Windows icon|*.ico|" +
                         "PNG image|*.png|TIFF image|*.tif;*.tiff|JPEG XR|*.hpd;*.jxr;*.wdp",

                FilterIndex = 1
            };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
            ImageSource = new BitmapImage(new Uri(dialog.FileName));
        }

        #endregion    ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">    <![CDATA[  

        #region "Public Property  TblCountry"

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get { return _tblCountriesAllList; }
            set { _tblCountriesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"  

        private void GetValueLanguage()
             {
                 _languages = new List<Language>()
                 {
                     new Language {Name = "GER"},
                     new Language {Name = "ENG"},
                     new Language {Name = "FRE"},
                     new Language {Name = "POR"}
                 };

                 _selectedLanguage = new Language();
             }

             private List<Language> _languages;

             public List<Language> Languages
             {
                 get => _languages;
                 set { _languages = value; RaisePropertyChanged(""); }
             }

             private Language _selectedLanguage;

             public Language SelectedLanguage
             {
                 get => _selectedLanguage;
                 set { _selectedLanguage = value; RaisePropertyChanged(""); }
             }

             public class Language
             {
                 public string Name
                 {
                     get;
                     set;
                 }
             }

 
             #region Mimetype

             private void GetValueMimeType()
             {
                 _mimeTypes = new List<MimeType>()
                 {
                     new MimeType {Name = "jpg"},
                     new MimeType {Name = "png"},
                     new MimeType {Name = "bmp"},
                     new MimeType {Name = "tiff"},
                     new MimeType {Name = "gif"},
                     new MimeType {Name = "icon"},
                     new MimeType {Name = "jpeg"},
                     new MimeType {Name = "wmf"},
                     new MimeType {Name = "wmv"},
                     new MimeType {Name = "mpg"},
                     new MimeType {Name = "mp4"},
                     new MimeType {Name = "avi"},
                     new MimeType {Name = "mov"},
                     new MimeType {Name = "swf"},
                     new MimeType {Name = "flv"}
                 };

                 _selectedMimeType = new MimeType();
             }

             private List<MimeType> _mimeTypes;

             public List<MimeType> MimeTypes
             {
                 get => _mimeTypes;
                 set {  _mimeTypes = value;  RaisePropertyChanged("");  }
             }

             private MimeType _selectedMimeType;
             public MimeType SelectedMimeType
             {
                 get => _selectedMimeType;
                 set  {  _selectedMimeType = value;  RaisePropertyChanged(""); }
             }

             public class MimeType
             {
                 public string Name { get; set; }
             }
        #endregion

        #region OpenfileDialog

        public static RelayCommand OpenCommand { get; set; }
        private string _selectedPath;

        public string SelectedPath
        {
            get => _selectedPath;
            set { _selectedPath = value; RaisePropertyChanged(""); }
        }

        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get => _imageSource;
            set { _imageSource = value; RaisePropertyChanged(""); }
        }

        public readonly string DefaultPath;

        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
        }

        private void ExecuteOpenFileDialog(object o)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select A File",
                InitialDirectory = DefaultPath,
                Filter = "All images|*.jpg;*.jpeg;*.jpe;*.bmp;*.gif;*.ico;*.png;*.tif;*.tiff;*.hpd;*.jxr;*.wdp|" +
                         "JPEG image|*.jpg;*.jpeg;*.jpe|Windows BMP image|*.bmp|GIF image|*.gif|Microsoft Windows icon|*.ico|" +
                         "PNG image|*.png|TIFF image|*.tif;*.tiff|JPEG XR|*.hpd;*.jxr;*.wdp",

                FilterIndex = 1
            };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
            ImageSource = new BitmapImage(new Uri(dialog.FileName));
        }

        #endregion    ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">      <![CDATA[  
        #region "Public Properties Tbl68Speciesgroup"

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList;
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties Tbl68Speciesgroup"

        #region "Public Properties Tbl66Genus"

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties Tbl66Genus"

        #region Language

        private void GetValueLanguage()
        {
            _languages = new List<Language>()
            {
                new Language {Name = "GER"},
                new Language {Name = "ENG"},
                new Language {Name = "FRE"},
                new Language {Name = "POR"}
            };

            _selectedLanguage = new Language();
        }

        private List<Language> _languages;

        public List<Language> Languages
        {
            get => _languages;
            set { _languages = value; RaisePropertyChanged(""); }
        }

        private Language _selectedLanguage;

        public Language SelectedLanguage
        {
            get => _selectedLanguage;
            set { _selectedLanguage = value; RaisePropertyChanged(""); }
        }

        public class Language
        {
            public string Name
            {
                get;
                set;
            }
        }

        #endregion Language  ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">     <![CDATA[  
        #region "Public Properties Tbl68Speciesgroup"

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList;
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties Tbl68Speciesgroup"

        #region "Public Properties Tbl66Genus"

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties Tbl66Genus" 

        #region Mimetype

        private void GetValueMimeType()
        {
            _mimeTypes = new List<MimeType>()
            {
                new MimeType {Name = "jpg"},
                new MimeType {Name = "png"},
                new MimeType {Name = "bmp"},
                new MimeType {Name = "tiff"},
                new MimeType {Name = "gif"},
                new MimeType {Name = "icon"},
                new MimeType {Name = "jpeg"},
                new MimeType {Name = "wmf"},
                new MimeType {Name = "wmv"},
                new MimeType {Name = "mpg"},
                new MimeType {Name = "mp4"},
                new MimeType {Name = "avi"},
                new MimeType {Name = "mov"},
                new MimeType {Name = "swf"},
                new MimeType {Name = "flv"}
            };

            _selectedMimeType = new MimeType();
        }

        private List<MimeType> _mimeTypes;

        public List<MimeType> MimeTypes
        {
            get => _mimeTypes;
            set { _mimeTypes = value; RaisePropertyChanged(""); }
        }

        private MimeType _selectedMimeType;
        public MimeType SelectedMimeType
        {
            get => _selectedMimeType;
            set { _selectedMimeType = value; RaisePropertyChanged(""); }
        }

        public class MimeType
        {
            public string Name { get; set; }
        }

        #endregion

        #region OpenfileDialog

        public static RelayCommand OpenCommand { get; set; }
        private string _selectedPath;

        public string SelectedPath
        {
            get => _selectedPath;
            set { _selectedPath = value; RaisePropertyChanged(""); }
        }


        private BitmapImage _imageSource;

        public BitmapImage ImageSource
        {
            get => _imageSource;
            set { _imageSource = value; RaisePropertyChanged(""); }
        }

        public readonly string DefaultPath;

        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
        }

        private void ExecuteOpenFileDialog(object o)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select A File",
                InitialDirectory = DefaultPath,
                Filter = "All images|*.jpg;*.jpeg;*.jpe;*.bmp;*.gif;*.ico;*.png;*.tif;*.tiff;*.hpd;*.jxr;*.wdp|" +
                         "JPEG image|*.jpg;*.jpeg;*.jpe|Windows BMP image|*.bmp|GIF image|*.gif|Microsoft Windows icon|*.ico|" +
                         "PNG image|*.png|TIFF image|*.tif;*.tiff|JPEG XR|*.hpd;*.jxr;*.wdp",

                FilterIndex = 1
            };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
            ImageSource = new BitmapImage(new Uri(dialog.FileName));
        }

        #endregion ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">       <![CDATA[  
        #region "Public Properties Tbl68Speciesgroup"

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList;
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties Tbl68Speciesgroup"

        #region "Public Properties Tbl66Genus"

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(); }
        }
        #endregion "Public Properties Tbl66Genus"  ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">     <![CDATA[  
        #region "Public Properties Tbl68Speciesgroup"

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList;
            set { _tbl68SpeciesgroupsAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties Tbl68Speciesgroup"

        #region "Public Properties Tbl66Genus"

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList;
            set { _tbl66GenussesAllList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties Tbl66Genus"

        #region "Private Continent, Country"

        private void GetValueContinent()
        {
            _continents = new List<Continent>()
            {
                new Continent {Name = "Africa"},
                new Continent {Name = "Antarctica"},
                new Continent {Name = "Asia"},
                new Continent {Name = "Australia"},
                new Continent {Name = "Central/South America"},
                new Continent {Name = "Europe"},
                new Continent {Name = "North America/Caribbean"}
            };

            _selectedContinent = new Continent();
        }

        private List<Continent> _continents;

        public List<Continent> Continents
        {
            get => _continents;
            set { _continents = value; RaisePropertyChanged(""); }
        }

        private Continent _selectedContinent;

        public Continent SelectedContinent
        {
            get => _selectedContinent;
            set { _selectedContinent = value; RaisePropertyChanged(""); }
        }

        public class Continent
        {
            public string Name
            {
                get;
                set;
            }
        }

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get => _tblCountriesAllList;
            set { _tblCountriesAllList = value; RaisePropertyChanged(""); }
        }     

        #endregion "Private Methods"  ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">    
</xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">    
</xsl:when> 
<xsl:when test="Table ='Tbl93Comments'">    
</xsl:when> 
<xsl:when test="Table ='TblCountries'">    
</xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">    
</xsl:when> 
<xsl:otherwise>   
</xsl:otherwise>    
</xsl:choose> 

   <![CDATA[}
}]]>   
</xsl:template>
</xsl:stylesheet>






