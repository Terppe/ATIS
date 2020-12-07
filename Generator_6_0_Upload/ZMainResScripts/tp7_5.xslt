<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
<xsl:output method="text" version="1.0" encoding="UTF-8" indent="yes"/>
<xsl:template match="Definition">    


<xsl:choose>
<xsl:when test="Table ='ControlTemplateButtons++++++++++++++++++'">        
</xsl:when>
<xsl:otherwise>     <![CDATA[      
                 ------- ControlTemplateButtons---------------
    <!--  ]]><xsl:value-of select="BasisTK1"/><![CDATA[-->
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

    <ControlTemplate x:Key="Add]]><xsl:value-of select="BasisTK1"/><![CDATA[Button" TargetType="Button">
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding Add]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}"
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
    <ControlTemplate x:Key="Copy]]><xsl:value-of select="BasisTK1"/><![CDATA[Button" TargetType="Button">
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding Copy]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}"
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
    <ControlTemplate x:Key="Save]]><xsl:value-of select="BasisTK1"/><![CDATA[Button" TargetType="Button">
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding Save]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}"
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
    <ControlTemplate x:Key="Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Button" TargetType="Button">
        <Button
            Width="36"
            Height="36"
            Margin="4"
            Command="{Binding Delete]]><xsl:value-of select="BasisTK1"/><![CDATA[Command}"
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



<![CDATA[ //    Part 11    ]]>

<xsl:choose>
<xsl:when test="Table ='++++++Properties Basis++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl06Phylums'">   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public new string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public new ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public new ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl09Divisions'">   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public new string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public new ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public new ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public new ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl12Subphylums'">   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl15Subdivisions'">   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();       }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl18Superclasses'">
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();     }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl66Genusses'">
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();   }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsAllList
        {
            get => _tbl68SpeciesgroupsAllList; 
            set { _tbl68SpeciesgroupsAllList = value; OnPropertyChanged(); }       
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl68Speciesgroups'">    
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList; 
            set { _tbl66GenussesAllList = value; OnPropertyChanged(); }
        }
        #endregion "Public Properties"   

        #region "Public Properties Tbl69FiSpecies"

        private string _searchFiSpeciesName;
        public string SearchFiSpeciesName
        {
            get => _searchFiSpeciesName; 
            set { _searchFiSpeciesName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciesList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesList
        {
            get => _tbl69FiSpeciesList; 
            set { _tbl69FiSpeciesList = value; OnPropertyChanged(); }
        }

        public ICollectionView FiSpeciessesView;
        public Tbl69FiSpecies CurrentTbl69FiSpecies => FiSpeciessesView?.CurrentItem as Tbl69FiSpecies;

        #endregion "Public Properties"

        #region "Public Properties Tbl72PlSpecies"

        private string _searchPlSpeciesName;
        public string SearchPlSpeciesName
        {
            get => _searchPlSpeciesName; 
            set { _searchPlSpeciesName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciesList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesList
        {
            get => _tbl72PlSpeciesList; 
            set { _tbl72PlSpeciesList = value; OnPropertyChanged(); }
        }

        public ICollectionView PlSpeciessesView;
        public Tbl72PlSpecies CurrentTbl72PlSpecies => PlSpeciessesView?.CurrentItem as Tbl72PlSpecies;

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">      <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public new  string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public new ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public new  ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public new  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();    }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public new  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties" 

        #region "Public Properties TblCountries"

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get => _tblCountriesAllList;
            set { _tblCountriesAllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"  ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">      <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public new  string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public new ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public new  ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public new  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();    }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public new  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"   

        #region "Public Properties TblCountries"

        private ObservableCollection<TblCountry> _tblCountriesAllList;
        public ObservableCollection<TblCountry> TblCountriesAllList
        {
            get => _tblCountriesAllList;
            set { _tblCountriesAllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        public ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;
        
        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _searchNameName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { _tbl78NamesList = value; OnPropertyChanged();      }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList; 
            set { _tbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList; 
            set { _tbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsTbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsTbl69FiSpeciessesAllList
        {
            get => _tbl68SpeciesgroupsTbl69FiSpeciessesAllList; 
            set { _tbl68SpeciesgroupsTbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsTbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsTbl72PlSpeciessesAllList
        {
            get => _tbl68SpeciesgroupsTbl72PlSpeciessesAllList; 
            set { _tbl68SpeciesgroupsTbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { _tbl78NamesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl69FiSpeciessesAllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[Tbl69FiSpeciessesAllList
        {
            get => ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl69FiSpeciessesAllList; 
            set { _tbl66GenussesTbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl72PlSpeciessesAllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[Tbl72PlSpeciessesAllList
        {
            get => ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl72PlSpeciessesAllList; 
            set { _tbl66GenussesTbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }
        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        public ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;
        
        private int _search]]><xsl:value-of select="Basis"/><![CDATA[Id;
        public int Search]]><xsl:value-of select="Basis"/><![CDATA[Id
        {
            get => _search]]><xsl:value-of select="Basis"/><![CDATA[Id; 
            set { _search]]><xsl:value-of select="Basis"/><![CDATA[Id = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();     }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList; 
            set { _tbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList; 
            set { _tbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsTbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsTbl69FiSpeciessesAllList
        {
            get => _tbl68SpeciesgroupsTbl69FiSpeciessesAllList; 
            set { _tbl68SpeciesgroupsTbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsTbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsTbl72PlSpeciessesAllList
        {
            get => _tbl68SpeciesgroupsTbl72PlSpeciessesAllList; 
            set { _tbl68SpeciesgroupsTbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl69FiSpeciessesAllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[Tbl69FiSpeciessesAllList
        {
            get => ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl69FiSpeciessesAllList; 
            set { _tbl66GenussesTbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }


        private ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl72PlSpeciessesAllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[Tbl72PlSpeciessesAllList
        {
            get => ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl72PlSpeciessesAllList; 
            set { _tbl66GenussesTbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }
        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        public ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;
        
        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();    }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList; 
            set { _tbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList; 
            set { _tbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsTbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsTbl69FiSpeciessesAllList
        {
            get => _tbl68SpeciesgroupsTbl69FiSpeciessesAllList; 
            set { _tbl68SpeciesgroupsTbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsTbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsTbl72PlSpeciessesAllList
        {
            get => _tbl68SpeciesgroupsTbl72PlSpeciessesAllList; 
            set { _tbl68SpeciesgroupsTbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl69FiSpeciessesAllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[Tbl69FiSpeciessesAllList
        {
            get => ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl69FiSpeciessesAllList; 
            set { _tbl66GenussesTbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl72PlSpeciessesAllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[Tbl72PlSpeciessesAllList
        {
            get => ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl72PlSpeciessesAllList; 
            set { _tbl66GenussesTbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }
        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        public ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;
        
        private int _search]]><xsl:value-of select="Basis"/><![CDATA[Id;
        public int Search]]><xsl:value-of select="Basis"/><![CDATA[Id
        {
            get => _search]]><xsl:value-of select="Basis"/><![CDATA[Id; 
            set { _search]]><xsl:value-of select="Basis"/><![CDATA[Id = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();    }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList; 
            set { _tbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList; 
            set { _tbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsTbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsTbl69FiSpeciessesAllList
        {
            get => _tbl68SpeciesgroupsTbl69FiSpeciessesAllList; 
            set { _tbl68SpeciesgroupsTbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl68Speciesgroup> _tbl68SpeciesgroupsTbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl68Speciesgroup> Tbl68SpeciesgroupsTbl72PlSpeciessesAllList
        {
            get => _tbl68SpeciesgroupsTbl72PlSpeciessesAllList; 
            set { _tbl68SpeciesgroupsTbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl69FiSpeciessesAllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[Tbl69FiSpeciessesAllList
        {
            get => ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl69FiSpeciessesAllList; 
            set { _tbl66GenussesTbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl72PlSpeciessesAllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[Tbl72PlSpeciessesAllList
        {
            get => ]]><xsl:value-of select="EntitysBK1"/><![CDATA[Tbl72PlSpeciessesAllList; 
            set { _tbl66GenussesTbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }
        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Basis"/><![CDATA[Info;
        public string Search]]><xsl:value-of select="Basis"/><![CDATA[Info
        {
            get => _search]]><xsl:value-of select="Basis"/><![CDATA[Info; 
            set { _search]]><xsl:value-of select="Basis"/><![CDATA[Info = value; OnPropertyChanged(); }
        }

        public ICollectionView ReferencesView;
        public Tbl90Reference CurrentTbl90Reference => ReferencesView?.CurrentItem as Tbl90Reference;
        
        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();    }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList= value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList= value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK3"/><![CDATA[> ]]><xsl:value-of select="EntitysFK3"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK3"/><![CDATA[> ]]><xsl:value-of select="TableFK3"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK3"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK3"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList; 
            set { _tbl03RegnumsAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList; 
            set { _tbl06PhylumsAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList; 
            set { _tbl09DivisionsAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList; 
            set { _tbl12SubphylumsAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get => _tbl15SubdivisionsAllList; 
            set { _tbl15SubdivisionsAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList; 
            set { _tbl18SuperclassesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;
        public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList; 
            set { _tbl21ClassesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList; 
            set { _tbl24SubclassesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList; 
            set { _tbl27InfraclassesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
        {
            get => _tbl30LegiosAllList; 
            set { _tbl30LegiosAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
        {
            get => _tbl33OrdosAllList; 
            set { _tbl33OrdosAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList; 
            set { _tbl36SubordosAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
        {
            get => _tbl39InfraordosAllList; 
            set { _tbl39InfraordosAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesAllList;
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesAllList
        {
            get => _tbl42SuperfamiliesAllList; 
            set { _tbl42SuperfamiliesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get => _tbl45FamiliesAllList; 
            set { _tbl45FamiliesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList;
        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
        {
            get => _tbl48SubfamiliesAllList; 
            set { _tbl48SubfamiliesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList; 
            set { _tbl51InfrafamiliesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList;
        public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
        {
            get => _tbl54SupertribussesAllList; 
            set { _tbl54SupertribussesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList; 
            set { _tbl57TribussesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList; 
            set { _tbl60SubtribussesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList;
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
        {
            get => _tbl63InfratribussesAllList; 
            set { _tbl63InfratribussesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList; 
            set { _tbl66GenussesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList; 
            set { _tbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList; 
            set { _tbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        public ICollectionView AuthorsView;
        public Tbl90RefAuthor CurrentTbl90RefAuthor => AuthorsView?.CurrentItem as Tbl90RefAuthor;
        
        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl90RefSources'">   
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        public ICollectionView SourcesView;
        public Tbl90RefSource CurrentTbl90RefSource => SourcesView?.CurrentItem as Tbl90RefSource;
        
        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl90RefExperts'">  
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        public ICollectionView ExpertsView;
        public Tbl90RefExpert CurrentTbl90RefExpert => ExpertsView?.CurrentItem as Tbl90RefExpert;
        
        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl93Comments'">
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Basis"/><![CDATA[Info;
        public string Search]]><xsl:value-of select="Basis"/><![CDATA[Info
        {
            get => _search]]><xsl:value-of select="Basis"/><![CDATA[Info; 
            set { _search]]><xsl:value-of select="Basis"/><![CDATA[Info = value; OnPropertyChanged(); }
        }

        public ICollectionView CommentsView;
        public Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;     
   
        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList; 
            set { _tbl03RegnumsAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList; 
            set { _tbl06PhylumsAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl09Division> _tbl09DivisionsAllList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsAllList
        {
            get => _tbl09DivisionsAllList; 
            set { _tbl09DivisionsAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsAllList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsAllList
        {
            get => _tbl12SubphylumsAllList; 
            set { _tbl12SubphylumsAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsAllList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsAllList
        {
            get => _tbl15SubdivisionsAllList; 
            set { _tbl15SubdivisionsAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl18Superclass> _tbl18SuperclassesAllList;
        public ObservableCollection<Tbl18Superclass> Tbl18SuperclassesAllList
        {
            get => _tbl18SuperclassesAllList; 
            set { _tbl18SuperclassesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl21Class> _tbl21ClassesAllList;
        public ObservableCollection<Tbl21Class> Tbl21ClassesAllList
        {
            get => _tbl21ClassesAllList; 
            set { _tbl21ClassesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl24Subclass> _tbl24SubclassesAllList;
        public ObservableCollection<Tbl24Subclass> Tbl24SubclassesAllList
        {
            get => _tbl24SubclassesAllList; 
            set { _tbl24SubclassesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl27Infraclass> _tbl27InfraclassesAllList;
        public ObservableCollection<Tbl27Infraclass> Tbl27InfraclassesAllList
        {
            get => _tbl27InfraclassesAllList; 
            set { _tbl27InfraclassesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl30Legio> _tbl30LegiosAllList;
        public ObservableCollection<Tbl30Legio> Tbl30LegiosAllList
        {
            get => _tbl30LegiosAllList; 
            set { _tbl30LegiosAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl33Ordo> _tbl33OrdosAllList;
        public ObservableCollection<Tbl33Ordo> Tbl33OrdosAllList
        {
            get => _tbl33OrdosAllList; 
            set { _tbl33OrdosAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl36Subordo> _tbl36SubordosAllList;
        public ObservableCollection<Tbl36Subordo> Tbl36SubordosAllList
        {
            get => _tbl36SubordosAllList; 
            set { _tbl36SubordosAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl39Infraordo> _tbl39InfraordosAllList;
        public ObservableCollection<Tbl39Infraordo> Tbl39InfraordosAllList
        {
            get => _tbl39InfraordosAllList; 
            set { _tbl39InfraordosAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl42Superfamily> _tbl42SuperfamiliesAllList;
        public ObservableCollection<Tbl42Superfamily> Tbl42SuperfamiliesAllList
        {
            get => _tbl42SuperfamiliesAllList; 
            set { _tbl42SuperfamiliesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl45Family> _tbl45FamiliesAllList;
        public ObservableCollection<Tbl45Family> Tbl45FamiliesAllList
        {
            get => _tbl45FamiliesAllList; 
            set { _tbl45FamiliesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl48Subfamily> _tbl48SubfamiliesAllList;
        public ObservableCollection<Tbl48Subfamily> Tbl48SubfamiliesAllList
        {
            get => _tbl48SubfamiliesAllList; 
            set { _tbl48SubfamiliesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl51Infrafamily> _tbl51InfrafamiliesAllList;
        public ObservableCollection<Tbl51Infrafamily> Tbl51InfrafamiliesAllList
        {
            get => _tbl51InfrafamiliesAllList; 
            set { _tbl51InfrafamiliesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl54Supertribus> _tbl54SupertribussesAllList;
        public ObservableCollection<Tbl54Supertribus> Tbl54SupertribussesAllList
        {
            get => _tbl54SupertribussesAllList; 
            set { _tbl54SupertribussesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl57Tribus> _tbl57TribussesAllList;
        public ObservableCollection<Tbl57Tribus> Tbl57TribussesAllList
        {
            get => _tbl57TribussesAllList; 
            set { _tbl57TribussesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl60Subtribus> _tbl60SubtribussesAllList;
        public ObservableCollection<Tbl60Subtribus> Tbl60SubtribussesAllList
        {
            get => _tbl60SubtribussesAllList; 
            set { _tbl60SubtribussesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl63Infratribus> _tbl63InfratribussesAllList;
        public ObservableCollection<Tbl63Infratribus> Tbl63InfratribussesAllList
        {
            get => _tbl63InfratribussesAllList; 
            set { _tbl63InfratribussesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl66Genus> _tbl66GenussesAllList;
        public ObservableCollection<Tbl66Genus> Tbl66GenussesAllList
        {
            get => _tbl66GenussesAllList; 
            set { _tbl66GenussesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl69FiSpecies> _tbl69FiSpeciessesAllList;
        public ObservableCollection<Tbl69FiSpecies> Tbl69FiSpeciessesAllList
        {
            get => _tbl69FiSpeciessesAllList; 
            set { _tbl69FiSpeciessesAllList = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl72PlSpecies> _tbl72PlSpeciessesAllList;
        public ObservableCollection<Tbl72PlSpecies> Tbl72PlSpeciessesAllList
        {
            get => _tbl72PlSpeciessesAllList; 
            set { _tbl72PlSpeciessesAllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:when test="Table ='TblCountries'">  
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public  string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public  ICollectionView CountriesView;
        public  ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => CountriesView?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();     }      
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"      ]]>
</xsl:when>  
<xsl:when test="Table ='TblUserProfiles'">  
   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        public ICollectionView UserProfilesView;
        public TblUserProfile CurrentTblUserProfile => UserProfilesView?.CurrentItem as TblUserProfile;
        
        private string _searchEmail;
        public string SearchEmail
        {
            get => _searchEmail; 
            set { _searchEmail = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        private string _passwordBox;
        public string PasswordBox
        {
            get => _passwordBox; 

            set { _passwordBox = value; OnPropertyChanged("PasswordBox"); }
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

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList; 
            set { _tblCountriesList = value; OnPropertyChanged(); }
        }

        //-----------------------------------------------------------
        private List<Role> _roles;
        public List<Role> Roles
        {
            get => _roles; 
            set { _roles = value; OnPropertyChanged(); }
        }

        private Role _selectedRole;
        public Role SelectedRole
        {
            get => _selectedRole; 
            set { _selectedRole = value; OnPropertyChanged(); }
        }

        private List<Gender> _genders;
        public List<Gender> Genders
        {
            get => _genders; 
            set { _genders = value; OnPropertyChanged(); }
        }

        private Gender _selectedGender;
        public Gender SelectedGender
        {
            get => _selectedGender; 
            set { _selectedGender = value; OnPropertyChanged(); }
        }

        private List<Title> _titles;
        public List<Title> Titles
        {
            get => _titles; 
            set { _titles = value; OnPropertyChanged(); }
        }

        private Title _selectedTitle;
        public Title SelectedTitle
        {
            get => _selectedTitle; 
            set { _selectedTitle = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"      ]]>
</xsl:when>  
<xsl:otherwise>   <![CDATA[  
        #region "Public Properties ]]><xsl:value-of select="LinqModel"/><![CDATA["

        private string _search]]><xsl:value-of select="Name"/><![CDATA[;
        public  string Search]]><xsl:value-of select="Name"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="Name"/><![CDATA[; 
            set { _search]]><xsl:value-of select="Name"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public  ICollectionView ]]><xsl:value-of select="Basiss"/><![CDATA[View;
        public  ]]><xsl:value-of select="LinqModel"/><![CDATA[ Current]]><xsl:value-of select="LinqModel"/><![CDATA[ => ]]><xsl:value-of select="Basiss"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModel"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="Entitys"/><![CDATA[List = value; OnPropertyChanged();   }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Entitys"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModel"/><![CDATA[> ]]><xsl:value-of select="Table"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="Entitys"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="Entitys"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"   ]]>
</xsl:otherwise>    
</xsl:choose> 



<xsl:choose>
<xsl:when test="Table ='++++++Properties Connect FK1++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl06Phylums'">    
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public new string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public new  ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public new  ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public new  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList;
        public new  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>   
<xsl:when test="Table ='Tbl09Divisions'">    
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public new string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public new  ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public new  ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public new  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList;
        public new  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>   
<xsl:when test="Table ='Tbl12Subphylums'">  
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public new string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public new ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public new ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList;
        public new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if>   
</xsl:when>   
<xsl:when test="Table ='Tbl15Subdivisions'">   
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public new string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public new ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public new ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList;
        public new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if>    
</xsl:when>   
<xsl:when test="Table ='Tbl18Superclasses'">    
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public  ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public  ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>   
<xsl:when test="Table ='Tbl68Speciesgroups'">    
</xsl:when>   
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public new string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public new ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public new ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;


        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>   
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public new string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public new ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public new ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;


        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public new ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>   
<xsl:when test="Table ='Tbl78Names'">    
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>   
<xsl:when test="Table ='Tbl81Images'">    
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">    
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>   
<xsl:when test="Table ='Tbl87Geographics'">    
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when> 
<xsl:when test="Table ='Tbl90References'">   
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView RefExpertsView;
        public  Tbl90RefExpert CurrentTbl90RefExpert => RefExpertsView?.CurrentItem as Tbl90RefExpert;

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if>  
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">   
 </xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">   
 </xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">   
 </xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">   
 </xsl:when> 
<xsl:otherwise>
  <xsl:if test="TableFK1 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK1"/><![CDATA[;
        public string Search]]><xsl:value-of select="NameFK1"/><![CDATA[
        {
            get  => _search]]><xsl:value-of select="NameFK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK1"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissFK1"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK1"/><![CDATA[ => ]]><xsl:value-of select="BasissFK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK1"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK1"/><![CDATA[> ]]><xsl:value-of select="TableFK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK1"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties Connect FK2++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl68Speciesgroups'">    
</xsl:when>
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
  <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK2"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK2"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK2"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK2"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK2"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public  ICollectionView ]]><xsl:value-of select="BasissFK2"/><![CDATA[View;
        public  ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ => ]]><xsl:value-of select="BasissFK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList;
        public new ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
  <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK2"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK2"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK2"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK2"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK2"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public  ICollectionView ]]><xsl:value-of select="BasissFK2"/><![CDATA[View;
        public  ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ => ]]><xsl:value-of select="BasissFK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList;
        public new ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>
<xsl:when test="Table ='Tbl78Names'">    
  <xsl:if test="TableFK2 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK2"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK2"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK2"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK2"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK2"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView ]]><xsl:value-of select="BasissFK2"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ => ]]><xsl:value-of select="BasissFK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List = value; OnPropertyChanged();  }
        }      
  ]]> 
  </xsl:if> 
</xsl:when>    
<xsl:when test="Table ='Tbl81Images'">    
  <xsl:if test="TableFK2 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK2"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK2"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK2"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK2"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK2"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView ]]><xsl:value-of select="BasissFK2"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ => ]]><xsl:value-of select="BasissFK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List = value; OnPropertyChanged();  }
        }      
  ]]> 
  </xsl:if> 
</xsl:when>    
<xsl:when test="Table ='Tbl84Synonyms'">    
  <xsl:if test="TableFK2 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK2"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK2"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK2"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK2"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK2"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView ]]><xsl:value-of select="BasissFK2"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ => ]]><xsl:value-of select="BasissFK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List = value; OnPropertyChanged();  }
        }      
  ]]> 
  </xsl:if> 
</xsl:when>    
<xsl:when test="Table ='Tbl87Geographics'">    
  <xsl:if test="TableFK2 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK2"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK2"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK2"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK2"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK2"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView ]]><xsl:value-of select="BasissFK2"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ => ]]><xsl:value-of select="BasissFK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[;

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List = value; OnPropertyChanged();  }
        }      
  ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">   
  <xsl:if test="TableFK2 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK2"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK2"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK2"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK2"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK2"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView RefSourcesView;
        public  Tbl90RefSource CurrentTbl90RefSource => RefSourcesView?.CurrentItem as Tbl90RefSource;

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if>  
  <xsl:if test="TableFK3 !='NULL'">       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK3"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK3"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK3"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK3"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK3"/><![CDATA[ = value; OnPropertyChanged();  }
        }

        public ICollectionView RefAuthorsView;
        public  Tbl90RefAuthor CurrentTbl90RefAuthor => RefAuthorsView?.CurrentItem as Tbl90RefAuthor;

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK3"/><![CDATA[> ]]><xsl:value-of select="EntitysFK3"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK3"/><![CDATA[> ]]><xsl:value-of select="TableFK3"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK3"/><![CDATA[List; 
            set {  ]]><xsl:value-of select="EntitysFK3"/><![CDATA[List = value; OnPropertyChanged();  }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if>  
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">   
 </xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">   
 </xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">   
 </xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">   
 </xsl:when> 
<xsl:otherwise>
  <xsl:if test="TableFK2 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelFK2"/><![CDATA["

        private string _search]]><xsl:value-of select="NameFK2"/><![CDATA[;
        public  string Search]]><xsl:value-of select="NameFK2"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameFK2"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameFK2"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public  ICollectionView ]]><xsl:value-of select="BasissFK2"/><![CDATA[View;
        public  ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelFK2"/><![CDATA[ => ]]><xsl:value-of select="BasissFK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelFK2"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysFK2"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelFK2"/><![CDATA[> ]]><xsl:value-of select="TableFK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysFK2"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties Connect TK1++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl18Superclasses'">    
  <xsl:if test="TableTK1 !='NULL'">      <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameTK1"/><![CDATA[;
        public string Search]]><xsl:value-of select="NameTK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameTK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameTK1"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissTK1"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ => ]]><xsl:value-of select="BasissTK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="TableTK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>   
<xsl:when test="Table ='Tbl68Speciesgroups'">    
</xsl:when>   
<xsl:when test="Table ='Tbl78Names'">    <![CDATA[
        #endregion "Public Properties"  ]]>
</xsl:when>   
<xsl:when test="Table ='Tbl81Images'">     <![CDATA[
        #endregion "Public Properties"  ]]>
</xsl:when>   
<xsl:when test="Table ='Tbl84Synonyms'">     <![CDATA[
        #endregion "Public Properties"  ]]>
</xsl:when>   
<xsl:when test="Table ='Tbl87Geographics'"> <![CDATA[
		private ObservableCollection<PhoneValidator> _phoneValidatorsList;
		public ObservableCollection<PhoneValidator> PhoneValidatorsList
		{
	                    get => _phoneValidatorsList; 
	                    set { _phoneValidatorsList = value; OnPropertyChanged();     }
		}

        #endregion "Public Properties"  ]]>
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">   
 </xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">   
 </xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">   
 </xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">   
 </xsl:when> 
<xsl:otherwise>
  <xsl:if test="TableTK1 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK1"/><![CDATA["

        private string _search]]><xsl:value-of select="NameTK1"/><![CDATA[;
        public string Search]]><xsl:value-of select="NameTK1"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameTK1"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameTK1"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissTK1"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK1"/><![CDATA[ => ]]><xsl:value-of select="BasissTK1"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK1"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="TableTK1"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK1"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="EntitysTK1"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK1"/><![CDATA[> ]]><xsl:value-of select="TableTK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysTK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysTK1"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties Connect TK2++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl68Speciesgroups'">    
</xsl:when>   
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
  <xsl:if test="TableTK2 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK2"/><![CDATA["

        private int _searchImageId;
        public int SearchImageId
        {
            get => _searchImageId; 
            set { _searchImageId = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissTK2"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ => ]]><xsl:value-of select="BasissTK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="TableTK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List = value; OnPropertyChanged(); }
        }



        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>   
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
  <xsl:if test="TableTK2 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK2"/><![CDATA["

        private int _searchImageId;
        public int SearchImageId
        {
            get => _searchImageId; 
            set { _searchImageId = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissTK2"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ => ]]><xsl:value-of select="BasissTK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="TableTK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">   
 </xsl:when> 
<xsl:when test="Table ='Tbl90RefSources'">   
 </xsl:when> 
<xsl:when test="Table ='Tbl90RefExperts'">   
 </xsl:when> 
<xsl:when test="Table ='TblUserProfiles'">   
 </xsl:when>  
<xsl:otherwise>
  <xsl:if test="TableTK2 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK2"/><![CDATA["

        private string _search]]><xsl:value-of select="NameTK2"/><![CDATA[;
        public string Search]]><xsl:value-of select="NameTK2"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameTK2"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameTK2"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissTK2"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK2"/><![CDATA[ => ]]><xsl:value-of select="BasissTK2"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK2"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="TableTK2"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK2"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="EntitysTK2"/><![CDATA[AllList;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK2"/><![CDATA[> ]]><xsl:value-of select="TableTK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysTK2"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysTK2"/><![CDATA[AllList = value; OnPropertyChanged(); }       
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='++++++Properties Connect TK3+TK4+++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl68Speciesgroups'">    
</xsl:when>   
<xsl:when test="Table ='Tbl69FiSpeciesses'">    
  <xsl:if test="TableTK3 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK3"/><![CDATA["

        private string _search]]><xsl:value-of select="NameTK3"/><![CDATA[;
        public string Search]]><xsl:value-of select="NameTK3"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameTK3"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameTK3"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissTK3"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[ => ]]><xsl:value-of select="BasissTK3"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> ]]><xsl:value-of select="EntitysTK3"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> ]]><xsl:value-of select="TableTK3"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK3"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK3"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
  <xsl:if test="TableTK4 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK4"/><![CDATA["

        private int _search]]><xsl:value-of select="BasisTK4"/><![CDATA[Id;
        public int Search]]><xsl:value-of select="BasisTK4"/><![CDATA[Id
        {
            get => _search]]><xsl:value-of select="BasisTK4"/><![CDATA[Id; 
            set { _search]]><xsl:value-of select="BasisTK4"/><![CDATA[Id = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissTK4"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ => ]]><xsl:value-of select="BasissTK4"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[> ]]><xsl:value-of select="EntitysTK4"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[> ]]><xsl:value-of select="TableTK4"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK4"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK4"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 

</xsl:when>   
<xsl:when test="Table ='Tbl72PlSpeciesses'">    
  <xsl:if test="TableTK3 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK3"/><![CDATA["

        private string _search]]><xsl:value-of select="NameTK3"/><![CDATA[;
        public string Search]]><xsl:value-of select="NameTK3"/><![CDATA[
        {
            get => _search]]><xsl:value-of select="NameTK3"/><![CDATA[; 
            set { _search]]><xsl:value-of select="NameTK3"/><![CDATA[ = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissTK3"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK3"/><![CDATA[ => ]]><xsl:value-of select="BasissTK3"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK3"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> ]]><xsl:value-of select="EntitysTK3"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK3"/><![CDATA[> ]]><xsl:value-of select="TableTK3"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK3"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK3"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
  <xsl:if test="TableTK4 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelTK4"/><![CDATA["

        private int _search]]><xsl:value-of select="BasisTK4"/><![CDATA[Id;
        public int Search]]><xsl:value-of select="BasisTK4"/><![CDATA[Id
        {
            get => _search]]><xsl:value-of select="BasisTK4"/><![CDATA[Id; 
            set { _search]]><xsl:value-of select="BasisTK4"/><![CDATA[Id = value; OnPropertyChanged(); }
        }

        public ICollectionView ]]><xsl:value-of select="BasissTK4"/><![CDATA[View;
        public ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ Current]]><xsl:value-of select="LinqModelTK4"/><![CDATA[ => ]]><xsl:value-of select="BasissTK4"/><![CDATA[View?.CurrentItem as ]]><xsl:value-of select="LinqModelTK4"/><![CDATA[;           

        private ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[> ]]><xsl:value-of select="EntitysTK4"/><![CDATA[List;
        public ObservableCollection<]]><xsl:value-of select="LinqModelTK4"/><![CDATA[> ]]><xsl:value-of select="TableTK4"/><![CDATA[List
        {
            get => ]]><xsl:value-of select="EntitysTK4"/><![CDATA[List; 
            set { ]]><xsl:value-of select="EntitysTK4"/><![CDATA[List = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if> 
</xsl:when>   
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Properties BK1 ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl12Subphylums'">   
</xsl:when>  
<xsl:when test="Table ='Tbl15Subdivisions'">     
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">     
</xsl:when>  
<xsl:when test="Table ='Tbl93Comments'">
</xsl:when>                          
<xsl:otherwise>
  <xsl:if test="TableBK1 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelBK1"/><![CDATA["

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="EntitysBK1"/><![CDATA[AllList;
        public  new ObservableCollection<]]><xsl:value-of select="LinqModelBK1"/><![CDATA[> ]]><xsl:value-of select="TableBK1"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysBK1"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysBK1"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if>  
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Properties BK2 ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl12Subphylums'">   
</xsl:when>  
<xsl:when test="Table ='Tbl15Subdivisions'">     
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">     
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">     
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">     
</xsl:when>  
<xsl:when test="Table ='Tbl93Comments'">
</xsl:when>                          
<xsl:otherwise>
  <xsl:if test="TableBK2 !='NULL'">
       <![CDATA[
        #region "Public Properties ]]><xsl:value-of select="LinqModelBK2"/><![CDATA["

        private ObservableCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[> ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList;
        public  ObservableCollection<]]><xsl:value-of select="LinqModelBK2"/><![CDATA[> ]]><xsl:value-of select="TableBK2"/><![CDATA[AllList
        {
            get => ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList; 
            set { ]]><xsl:value-of select="EntitysBK2"/><![CDATA[AllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties"
  ]]> 
  </xsl:if>  
</xsl:otherwise>    
</xsl:choose> 


<xsl:choose>
<xsl:when test="Table ='+++Properties References ++++++++'">
</xsl:when> 
 <xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[  
        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList; 
            set { _tbl90AuthorsAllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList; 
            set { _tbl90SourcesAllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList; 
            set { _tbl90ExpertsAllList = value; OnPropertyChanged(); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90RefAuthor"

        private string _searchRefAuthorName;
        public string SearchRefAuthorName
        {
            get => _searchRefAuthorName; 
            set { _searchRefAuthorName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90RefAuthorsList
        {
            get => _tbl90RefAuthorsList; 
            set { _tbl90RefAuthorsList = value; OnPropertyChanged(); }
        }

        public ICollectionView RefAuthorsView;
        public Tbl90Reference CurrentTbl90RefAuthor => RefAuthorsView?.CurrentItem as Tbl90Reference;

        #endregion "Public Properties"

        #region "Public Properties Tbl90RefSource"

        private string _searchRefSourceName;
        public string SearchRefSourceName
        {
            get => _searchRefSourceName; 
            set { _searchRefSourceName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90RefSourcesList
        {
            get => _tbl90RefSourcesList; 
            set { _tbl90RefSourcesList = value; OnPropertyChanged(); }
        }

        public ICollectionView RefSourcesView;
        public Tbl90Reference CurrentTbl90RefSource => RefSourcesView?.CurrentItem as Tbl90Reference;

        #endregion "Public Properties"

        #region "Public Properties Tbl90RefExpert"

        private string _searchRefExpertName;
        public string SearchRefExpertName
        {
            get => _searchRefExpertName; 
            set { _searchRefExpertName = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl90Reference> _tbl90RefExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90RefExpertsList
        {
            get => _tbl90RefExpertsList; 
            set { _tbl90RefExpertsList = value; OnPropertyChanged(); }
        }

        public ICollectionView RefExpertsView;
        public Tbl90Reference CurrentTbl90RefExpert => RefExpertsView?.CurrentItem as Tbl90Reference;

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<xsl:choose>
<xsl:when test="Table ='+++Properties Comments ++++++++'">
</xsl:when>  
<xsl:when test="Table ='Tbl03Regnums'">
   <![CDATA[  
        #region "Public Properties Tbl93Comment"

        private string _searchCommentInfo;
        public string SearchCommentInfo
        {
            get => _searchCommentInfo; 
            set { _searchCommentInfo = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList; 
            set { _tbl93CommentsList = value; OnPropertyChanged(); }
        }

        public ICollectionView CommentsView;
        public Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        #endregion "Public Properties"
   ]]>
</xsl:when>  
<xsl:otherwise>
</xsl:otherwise>    
</xsl:choose> 

<![CDATA[ //    Part 12    ]]>

<xsl:choose>
<xsl:when test="Table ='+++Private Methods ++++++++'">
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
<xsl:when test="Table ='Tbl66Genusses'">  
</xsl:when>  
<xsl:when test="Table ='Tbl68Speciesgroups'">  
</xsl:when>  
<xsl:when test="Table ='Tbl69FiSpeciesses'">  
</xsl:when>  
<xsl:when test="Table ='Tbl72PlSpeciesses'">  
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">  
</xsl:when>  
<xsl:when test="Table ='Tbl81Images'">       <![CDATA[  
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
            set { _mimeTypes = value; OnPropertyChanged(); }
        }

        private MimeType _selectedMimeType;
        public MimeType SelectedMimeType
        {
            get => _selectedMimeType; 
            set { _selectedMimeType = value; OnPropertyChanged(); }
        }

        public class MimeType
        {
            public string Name
            {
                get;
                set;
            }
        }

        #endregion


        #region OpenfileDialog

        public static RelayCommand OpenCommand { get; set; }
        private string _selectedPath;
        public string SelectedPath
        {
            get => _selectedPath; 
            set { _selectedPath = value;  OnPropertyChanged(); }
        }

        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get => _imageSource; 
            set { _imageSource = value; OnPropertyChanged(); }
        }

        private readonly string _defaultPath;


        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
        }

        private void ExecuteOpenFileDialog()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select A File",
                InitialDirectory = _defaultPath,
                Filter = "All images|*.jpg;*.jpeg;*.jpe;*.bmp;*.gif;*.ico;*.png;*.tif;*.tiff;*.hpd;*.jxr;*.wdp|" +
                    "JPEG image|*.jpg;*.jpeg;*.jpe|Windows BMP image|*.bmp|GIF image|*.gif|Microsoft Windows icon|*.ico|" +
                    "PNG image|*.png|TIFF image|*.tif;*.tiff|JPEG XR|*.hpd;*.jxr;*.wdp",

            FilterIndex = 1
            };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
            ImageSource = new BitmapImage(new Uri(dialog.FileName));
        }

        #endregion
]]>
</xsl:when>  
<xsl:when test="Table ='Tbl84Synonyms'">  
</xsl:when>  
<xsl:when test="Table ='Tbl87Geographics'">  
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">
</xsl:when>  
<xsl:when test="Table ='Tbl90References'">
</xsl:when>  
<xsl:when test="Table ='Tbl90RefSources'">
</xsl:when>  
<xsl:when test="Table ='Tbl90RefExperts'">
</xsl:when> 
<xsl:when test="Table ='Tbl93Comments'">
</xsl:when>  
<xsl:when test="Table ='TblUserProfiles'">      <![CDATA[  

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

        #endregion "Private Methods"
   ]]>
</xsl:when>   
<xsl:when test="Table ='Tbl93Comments'">
</xsl:when>                          
<xsl:otherwise>      
</xsl:otherwise>    
</xsl:choose> 


<xsl:choose>
<xsl:when test="Table ='+++Private Methods Connect TK1++++++++'">
</xsl:when> 
<xsl:when test="Table ='Tbl68Speciesgroups'">      
</xsl:when> 
<xsl:when test="Table ='Tbl69FiSpeciesses'">      <![CDATA[  
 
        #region "Private Language, Continent, Country"

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
            set { _languages= value; OnPropertyChanged(); }
        }

        private Language _selectedLanguage;
        public Language SelectedLanguage
        {
            get => _selectedLanguage; 
            set { _selectedLanguage = value; OnPropertyChanged(); }
        }

        public class Language
        {
            public string Name
            {
                get;
                set;
            }
        }
        //-------------------------------------------
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
            set { _continents = value; OnPropertyChanged(); }
        }

        private Continent _selectedContinent;
        public Continent SelectedContinent
        {
            get => _selectedContinent; 
            set { _selectedContinent = value; OnPropertyChanged(); }
        }

        public class Continent
        {
            public string Name
            {
                get;
                set;
            }
        }

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList; 
            set { _tblCountriesList = value; OnPropertyChanged(); }
        }

        #endregion "Private Methods"

        //    Part 12   

        #region Mimetype

        private void GetValueMimeType()
        {
            _mimeTypes = new List<Tbl81ImagesViewModel.MimeType>()
            {
                new Tbl81ImagesViewModel.MimeType {Name = "jpg"},
                new Tbl81ImagesViewModel.MimeType {Name = "png"},
                new Tbl81ImagesViewModel.MimeType {Name = "bmp"},
                new Tbl81ImagesViewModel.MimeType {Name = "tiff"},
                new Tbl81ImagesViewModel.MimeType {Name = "gif"},
                new Tbl81ImagesViewModel.MimeType {Name = "icon"},
                new Tbl81ImagesViewModel.MimeType {Name = "jpeg"},
                new Tbl81ImagesViewModel.MimeType {Name = "wmf"},
                new Tbl81ImagesViewModel.MimeType {Name = "wmv"},
                new Tbl81ImagesViewModel.MimeType {Name = "mpg"},
                new Tbl81ImagesViewModel.MimeType {Name = "mp4"},
                new Tbl81ImagesViewModel.MimeType {Name = "avi"},
                new Tbl81ImagesViewModel.MimeType {Name = "mov"},
                new Tbl81ImagesViewModel.MimeType {Name = "swf"},
                new Tbl81ImagesViewModel.MimeType {Name = "flv"}
            };

            _selectedMimeType = new Tbl81ImagesViewModel.MimeType();
        }

        private List<Tbl81ImagesViewModel.MimeType> _mimeTypes;
        public List<Tbl81ImagesViewModel.MimeType> MimeTypes
        {
            get => _mimeTypes; 
            set { _mimeTypes = value; OnPropertyChanged(); }
        }

        private Tbl81ImagesViewModel.MimeType _selectedMimeType;
        public Tbl81ImagesViewModel.MimeType SelectedMimeType
        {
            get => _selectedMimeType; 
            set { _selectedMimeType = value; OnPropertyChanged(); }
        }

        public class MimeType
        {
            public string Name
            {
                get;
                set;
            }
        }

        #endregion


        #region OpenfileDialog

        public static RelayCommand OpenCommand { get; set; }
        private string _selectedPath;
        public string SelectedPath
        {
            get => _selectedPath; 
            set { _selectedPath = value; OnPropertyChanged(); }
        }

        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get => _imageSource; 
            set { _imageSource = value; OnPropertyChanged(); }
        }

        public readonly string _defaultPath;


        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
        }

        private void ExecuteOpenFileDialog()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select A File",
                InitialDirectory = _defaultPath,
                Filter = "All images|*.jpg;*.jpeg;*.jpe;*.bmp;*.gif;*.ico;*.png;*.tif;*.tiff;*.hpd;*.jxr;*.wdp|" +
                    "JPEG image|*.jpg;*.jpeg;*.jpe|Windows BMP image|*.bmp|GIF image|*.gif|Microsoft Windows icon|*.ico|" +
                    "PNG image|*.png|TIFF image|*.tif;*.tiff|JPEG XR|*.hpd;*.jxr;*.wdp",

                FilterIndex = 1
            };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
            ImageSource = new BitmapImage(new Uri(dialog.FileName));
        }

        #endregion
   ]]>
</xsl:when>         
<xsl:when test="Table ='Tbl72PlSpeciesses'">      <![CDATA[  
 
        #region "Private Language, Continent, Country"

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
            set { _languages = value; OnPropertyChanged(); }
        }

        private Language _selectedLanguage;
        public Language SelectedLanguage
        {
            get => _selectedLanguage; 
            set { _selectedLanguage = value; OnPropertyChanged(); }
        }

        public class Language
        {
            public string Name
            {
                get;
                set;
            }
        }
        //-------------------------------------------
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
            set { _continents = value; OnPropertyChanged(); }
        }

        private Continent _selectedContinent;
        public Continent SelectedContinent
        {
            get => _selectedContinent; 
            set { _selectedContinent = value; OnPropertyChanged(); }
        }

        public class Continent
        {
            public string Name
            {
                get;
                set;
            }
        }

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList; 
            set { _tblCountriesList = value; OnPropertyChanged(); }
        }

        #endregion "Private Methods"

        //    Part 12   

        #region Mimetype

        private void GetValueMimeType()
        {
            _mimeTypes = new List<Tbl81ImagesViewModel.MimeType>()
            {
                new Tbl81ImagesViewModel.MimeType {Name = "jpg"},
                new Tbl81ImagesViewModel.MimeType {Name = "png"},
                new Tbl81ImagesViewModel.MimeType {Name = "bmp"},
                new Tbl81ImagesViewModel.MimeType {Name = "tiff"},
                new Tbl81ImagesViewModel.MimeType {Name = "gif"},
                new Tbl81ImagesViewModel.MimeType {Name = "icon"},
                new Tbl81ImagesViewModel.MimeType {Name = "jpeg"},
                new Tbl81ImagesViewModel.MimeType {Name = "wmf"},
                new Tbl81ImagesViewModel.MimeType {Name = "wmv"},
                new Tbl81ImagesViewModel.MimeType {Name = "mpg"},
                new Tbl81ImagesViewModel.MimeType {Name = "mp4"},
                new Tbl81ImagesViewModel.MimeType {Name = "avi"},
                new Tbl81ImagesViewModel.MimeType {Name = "mov"},
                new Tbl81ImagesViewModel.MimeType {Name = "swf"},
                new Tbl81ImagesViewModel.MimeType {Name = "flv"}
            };

            _selectedMimeType = new Tbl81ImagesViewModel.MimeType();
        }

        private List<Tbl81ImagesViewModel.MimeType> _mimeTypes;
        public List<Tbl81ImagesViewModel.MimeType> MimeTypes
        {
            get => _mimeTypes; 
            set { _mimeTypes = value; OnPropertyChanged(); }
        }

        private Tbl81ImagesViewModel.MimeType _selectedMimeType;
        public Tbl81ImagesViewModel.MimeType SelectedMimeType
        {
            get => _selectedMimeType; 
            set { _selectedMimeType = value; OnPropertyChanged(); }
        }

        public class MimeType
        {
            public string Name
            {
                get;
                set;
            }
        }

        #endregion


        #region OpenfileDialog

        public static RelayCommand OpenCommand { get; set; }
        private string _selectedPath;
        public string SelectedPath
        {
            get => _selectedPath; 
            set { _selectedPath = value; OnPropertyChanged(); }
        }

        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get => _imageSource; 
            set { _imageSource = value; OnPropertyChanged(); }
        }

        public readonly string _defaultPath;


        private void RegisterCommands()
        {
            OpenCommand = new RelayCommand(ExecuteOpenFileDialog);
        }

        private void ExecuteOpenFileDialog()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Select A File",
                InitialDirectory = _defaultPath,
                Filter = "All images|*.jpg;*.jpeg;*.jpe;*.bmp;*.gif;*.ico;*.png;*.tif;*.tiff;*.hpd;*.jxr;*.wdp|" +
                    "JPEG image|*.jpg;*.jpeg;*.jpe|Windows BMP image|*.bmp|GIF image|*.gif|Microsoft Windows icon|*.ico|" +
                    "PNG image|*.png|TIFF image|*.tif;*.tiff|JPEG XR|*.hpd;*.jxr;*.wdp",

                FilterIndex = 1
            };
            dialog.ShowDialog();

            SelectedPath = dialog.FileName;
            ImageSource = new BitmapImage(new Uri(dialog.FileName));
        }

        #endregion
   ]]>      
</xsl:when>  
<xsl:when test="Table ='Tbl78Names'">      <![CDATA[  

        #region "Private Methods"

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
            set { _languages = value; OnPropertyChanged(); }
        }

        private Language _selectedLanguage;
        public Language SelectedLanguage
        {
            get => _selectedLanguage; 
            set { _selectedLanguage = value; OnPropertyChanged(); }
        }

        public class Language
        {
            public string Name
            {
                get;
                set;
            }
        }

        #endregion "Private Methods"
   ]]>
</xsl:when>          
<xsl:when test="Table ='Tbl81Images'">  
</xsl:when>          
<xsl:when test="Table ='Tbl84Synonyms'">  
</xsl:when>     
<xsl:when test="Table ='Tbl87Geographics'">      <![CDATA[  

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
            set { _continents = value; OnPropertyChanged(); }
        }

        private Continent _selectedContinent;
        public Continent SelectedContinent
        {
            get => _selectedContinent; 
            set { _selectedContinent = value; OnPropertyChanged(); }
        }

        public class Continent
        {
            public string Name
            {
                get;
                set;
            }
        }

        private ObservableCollection<TblCountry> _tblCountriesList;
        public ObservableCollection<TblCountry> TblCountriesList
        {
            get => _tblCountriesList; 
            set { _tblCountriesList = value; OnPropertyChanged(); }
        }

        #endregion "Private Methods"   ]]>
</xsl:when>    
<xsl:when test="Table ='Tbl90References'">  
</xsl:when>  
<xsl:when test="Table ='Tbl90RefAuthors'">  
</xsl:when>                    
<xsl:when test="Table ='Tbl90RefSources'">   
</xsl:when>                    
<xsl:when test="Table ='Tbl90RefExperts'">   
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


</xsl:template>
</xsl:stylesheet>


















