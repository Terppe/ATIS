using System;  

     
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using DAL;
using DAL.Helper;
using DAL.Models;
using Microsoft.Maps.MapControl.WPF;
using WPFUI.Properties;  
 

      //  Tbl72PlSpeciessesView.xaml.cs Skriptdatum:  13.12.2019  12:32     

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl72PlSpeciessesView.xaml
    /// </summary>
    public partial class Tbl72PlSpeciessesView : UserControl
   {      

    
        private Point _origin;
        private Point _start;

        //Geographic
        private Location _loc1;
        private Location _loc2;
        private Location _loc3;
        private Location _loc4;
        private int _z;
        private readonly Repository<Tbl87Geographic, int> _tbl87GeographicsRepository = new Repository<Tbl87Geographic, int>();

        public Tbl72PlSpeciessesView()
        {         
            InitializeComponent();   
            IsVisibleChanged += UserControl_IsVisibleChanged;


            var group = new TransformGroup();
            var xform = new ScaleTransform();
            group.Children.Add(xform);

            var tt = new TranslateTransform();
            group.Children.Add(tt);

            Image1.RenderTransform = group;

            Image1.MouseWheel += image_MouseWheel;
            Image1.MouseLeftButtonDown += image_MouseLeftButtonDown;
            Image1.MouseLeftButtonUp += image_MouseLeftButtonUp;
            Image1.MouseMove += image_MouseMove;

            //geographic
            MapWithPolygon.MouseDoubleClick += Map_MouseDoubleClick;

            //Key
            //MapWithPolygon.CredentialsProvider = new ApplicationIdCredentialsProvider("Ai4CQexF-DWddfmN5GMnm8RD19EW_5adcMQAu9Rdzw5LiMxAQ-DeLBKVEG9AcLMg");
            MapWithPolygon.CredentialsProvider = new ApplicationIdCredentialsProvider(Settings.Default.BingMapKey);
            //Set focus to map
            MapWithPolygon.Focus();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                Dispatcher.BeginInvoke(
                DispatcherPriority.ContextIdle,
                new Action(delegate
                {
                    TbSearchPlSpecies.Focus();
                }));
            }
        }

        private void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image1.ReleaseMouseCapture();
        }

        private void image_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Image1.IsMouseCaptured) return;

            var tt = (TranslateTransform)((TransformGroup)Image1.RenderTransform).Children.First(tr => tr is TranslateTransform);
            var v = _start - e.GetPosition(Border1);
            tt.X = _origin.X - v.X;
            tt.Y = _origin.Y - v.Y;
        }

        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image1.CaptureMouse();
            var tt = (TranslateTransform)((TransformGroup)Image1.RenderTransform).Children.First(tr => tr is TranslateTransform);
            _start = e.GetPosition(Border1);
            _origin = new Point((int) tt.X, (int) tt.Y);
        }

        private void image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var transformGroup = (TransformGroup)Image1.RenderTransform;
            var transform = (ScaleTransform)transformGroup.Children[0];

            var zoom = e.Delta > 0 ? .2 : -.2;
            transform.ScaleX += zoom;
            transform.ScaleY += zoom;
        } 

        private void MenuItem_Click_CreateArea(object sender, RoutedEventArgs e)
        {
            if (_loc1 != null)
            {
                if (_loc2 != null)
                {
                    DrawLine(_loc1, _loc2);
                    DrawLine(_loc2, _loc3);
                }
                if (_loc3 != null) if (_loc4 != null) DrawLine(_loc3, _loc4);
                if (_loc4 != null) if (_loc1 != null) DrawLine(_loc4, _loc1);
            }

            // Defines the polygon fill details
            //Set focus back to the map so that +/- work for zoom in/out
            MapWithPolygon.Focus();

        }
        private void MenuItem_Click_ClearArea(object sender, RoutedEventArgs e)
        {
            //  var ct = MapWithPolygon.Children.Count;
            MapWithPolygon.Children.Clear();
            TbLatitude.Text = "";
            TbLongitude.Text = "";
            TbLatitude1.Text = "";
            TbLongitude1.Text = "";
            TbLatitude2.Text = "";
            TbLongitude2.Text = "";
            TbLatitude3.Text = "";
            TbLongitude3.Text = "";
            TbZoomLevel.Text = "";
            _loc1 = null;
            _loc2 = null;
            _loc3 = null;
            _loc4 = null;
            _z = 0;
            MapWithPolygon.Focus();
        }
        private void MenuItem_Click_Africa(object sender, RoutedEventArgs e)
        {
            MapWithPolygon.Center = new Location(-0.50872, 15.79149);
            MapWithPolygon.ZoomLevel = 4.0;
        }
        private void MenuItem_Click_Antarctica(object sender, RoutedEventArgs e)
        {
            MapWithPolygon.Center = new Location(-79.84140, 4.29547);
            MapWithPolygon.ZoomLevel = 4.0;
        }
        private void MenuItem_Click_Asia(object sender, RoutedEventArgs e)
        {
            MapWithPolygon.Center = new Location(57.17668, 95.15212);
            MapWithPolygon.ZoomLevel = 4.0;
        }
        private void MenuItem_Click_Australia(object sender, RoutedEventArgs e)
        {
            MapWithPolygon.Center = new Location(-25.95802, 132.27543);
            MapWithPolygon.ZoomLevel = 4.0;
        }
        private void MenuItem_Click_SouthAmerica(object sender, RoutedEventArgs e)
        {
            MapWithPolygon.Center = new Location(-17.39255, -63.63278);
            MapWithPolygon.ZoomLevel = 4.0;
        }
        private void MenuItem_Click_Europe(object sender, RoutedEventArgs e)
        {
            MapWithPolygon.Center = new Location(55.17888, 19.51176);
            MapWithPolygon.ZoomLevel = 4.0;
        }
        private void MenuItem_Click_NorthAmerica(object sender, RoutedEventArgs e)
        {
            MapWithPolygon.Center = new Location(33.35808, -99.49215);
            MapWithPolygon.ZoomLevel = 4.0;
        }
        private void DrawLine(Location start, Location finish)
        {
            var coll = new LocationCollection { start, finish };
            var line = new MapPolyline
            {
                Stroke = new SolidColorBrush(Colors.Orange),
                StrokeThickness = 2.0,
                Locations = coll
            };
            MapWithPolygon.Children.Add(line);
        }
        private void PlaceDot(Location location, Color color)
        {
            var dot = new Ellipse { Fill = new SolidColorBrush(color) };
            const double radius = 6.0;
            dot.Width = radius * 2;
            dot.Height = radius * 2;
            var tt = new ToolTip { Content = "Location = " + location };
            dot.ToolTip = tt;
            var p0 = MapWithPolygon.LocationToViewportPoint(location);
            var p1 = new Point(p0.X - radius, p0.Y - radius);
            var loc = MapWithPolygon.ViewportPointToLocation(p1);
            MapLayer.SetPosition(dot, loc);
            MapWithPolygon.Children.Add(dot);
        }
        private void Map_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            // Creates a location for a single polygon point and adds it to
            // the polygon's point location list.

            // A visual representation of a polygon point.
            var r = new Rectangle
            {
                Fill = new SolidColorBrush(Colors.Red),
                Stroke = new SolidColorBrush(Colors.Yellow),
                StrokeThickness = 1,
                Width = 8,
                Height = 8
            };


            if (_z == 0)
            {
                var p0 = e.GetPosition(this);
                var p1 = new Point(p0.X - 335, p0.Y - 355);
                _loc1 = MapWithPolygon.ViewportPointToLocation(p1);
                TbLatitude.Text = _loc1.Latitude.ToString(CultureInfo.InvariantCulture);
                TbLongitude.Text = _loc1.Longitude.ToString(CultureInfo.InvariantCulture);
                TbZoomLevel.Text = MapWithPolygon.ZoomLevel.ToString(CultureInfo.InvariantCulture);

                PlaceDot(_loc1, Colors.Red);
                _z++;
                return;
            }

            if (_z == 1)
            {

                var p0 = e.GetPosition(this);
                var p1 = new Point(p0.X - 335, p0.Y - 355);
                _loc2 = MapWithPolygon.ViewportPointToLocation(p1);
                TbLatitude1.Text = _loc2.Latitude.ToString(CultureInfo.InvariantCulture);
                TbLongitude1.Text = _loc2.Longitude.ToString(CultureInfo.InvariantCulture);

                PlaceDot(_loc2, Colors.Red);
                _z++;
                return;
            }
            if (_z == 2)
            {

                var p0 = e.GetPosition(this);
                var p1 = new Point(p0.X - 335, p0.Y - 355);
                _loc3 = MapWithPolygon.ViewportPointToLocation(p1);
                TbLatitude2.Text = _loc3.Latitude.ToString(CultureInfo.InvariantCulture);
                TbLongitude2.Text = _loc3.Longitude.ToString(CultureInfo.InvariantCulture);

                PlaceDot(_loc3, Colors.Red);
                _z++;
                return;
            }
            if (_z == 3)
            {

                var p0 = e.GetPosition(this);
                var p1 = new Point(p0.X - 335, p0.Y - 355);
                _loc4 = MapWithPolygon.ViewportPointToLocation(p1);
                TbLatitude3.Text = _loc4.Latitude.ToString(CultureInfo.InvariantCulture);
                TbLongitude3.Text = _loc4.Longitude.ToString(CultureInfo.InvariantCulture);

                PlaceDot(_loc4, Colors.Red);

                //Nummer in Geographic suchen
                var geographic = _tbl87GeographicsRepository.Get(Convert.ToInt32(TbGeographicId.Text));
                if (Convert.ToInt32(TbGeographicId.Text) != 0)  //update
                {
                    geographic.PlSpeciesID = Convert.ToInt32(PlSpeciesId.Text);
                    geographic.FiSpeciesID = 3;
                    geographic.Address = TbAddress.Text;
                    geographic.Continent = CbContinent.Text;
                    geographic.Country = CbCountry.SelectedValue.ToString();
                    geographic.Http = TbHttp.Text;
                    geographic.Valid = CbValid.IsChecked;
                    geographic.ValidYear = TbValidYear.Text;
                    geographic.Author = TbAuthor.Text;
                    geographic.AuthorYear = TbAuthorYear.Text;
                    geographic.Info = TbInfo.Text;
                    geographic.Memo = TbMemo.Text;
                    geographic.Latitude = _loc1.Latitude;
                    geographic.Longitude = _loc1.Longitude;
                    geographic.Latitude1 = _loc2.Latitude;
                    geographic.Longitude1 = _loc2.Longitude;
                    geographic.Latitude2 = _loc4.Latitude;
                    geographic.Longitude2 = _loc3.Longitude;
                    geographic.Latitude3 = _loc4.Latitude;
                    geographic.Longitude3 = _loc4.Longitude;
                    geographic.ZoomLevel = MapWithPolygon.ZoomLevel;
                    geographic.Updater = Environment.UserName;
                    geographic.UpdaterDate = DateTime.Now;
                }
                else
                {
                    _tbl87GeographicsRepository.Add(new Tbl87Geographic //add new
                    {
                        PlSpeciesID = Convert.ToInt32(PlSpeciesId.Text),
                        FiSpeciesID = 3,
                        CountID = RandomHelper.Randomnumber(),
                        Address = TbAddress.Text,
                        Continent = CbContinent.Text,
                        Country = CbCountry.SelectedValue.ToString(),
                        Http = TbHttp.Text,
                        Valid = CbValid.IsChecked,
                        ValidYear = TbValidYear.Text,
                        Author = TbAuthor.Text,
                        AuthorYear = TbAuthorYear.Text,
                        Info = TbInfo.Text,
                        Memo = TbMemo.Text,
                        Latitude = _loc1.Latitude,
                        Longitude = _loc1.Longitude,
                        Latitude1 = _loc2.Latitude,
                        Longitude1 = _loc2.Longitude,
                        Latitude2 = _loc4.Latitude,
                        Longitude2 = _loc3.Longitude,
                        Latitude3 = _loc4.Latitude,
                        Longitude3 = _loc4.Longitude,
                        ZoomLevel = MapWithPolygon.ZoomLevel,
                        Writer = Environment.UserName,
                        WriterDate = DateTime.Now,
                        Updater = Environment.UserName,
                        UpdaterDate = DateTime.Now
                    });
                }
                _tbl87GeographicsRepository.Save();
                MapWithPolygon?.Focus();
            }
        }

        private void Button_Click_Point(object sender, RoutedEventArgs e)
        {
            var geographic = _tbl87GeographicsRepository.Get(Convert.ToInt32(TbGeographicId.Text));
            MapWithPolygon.Focus();

            //Clear
            MapWithPolygon.Children.Clear();
            //Show Saved points

            //     if (geographic.Latitude > 0 || geographic.Latitude < 0)
            if (geographic.Latitude == 0)
            {
                switch (geographic.Continent)
                {
                    case "Africa":
                        MenuItem_Click_Africa(sender, e);
                        break;
                    case "Antarctica":
                        MenuItem_Click_Antarctica(sender, e);
                        break;
                    case "Asia":
                        MenuItem_Click_Asia(sender, e);
                        break;
                    case "Australia":
                        MenuItem_Click_Australia(sender, e);
                        break;
                    case "Central/South America":
                        MenuItem_Click_SouthAmerica(sender, e);
                        break;
                    case "Europe":
                        MenuItem_Click_Europe(sender, e);
                        break;
                    case "North America/Caribbean":
                        MenuItem_Click_NorthAmerica(sender, e);
                        break;
                }
            }
            if (geographic.Latitude > 0 || geographic.Latitude < 0)
                MapWithPolygon.Center = new Location(geographic.Latitude, geographic.Longitude);
            MapWithPolygon.ZoomLevel = geographic.ZoomLevel;

            PlaceDot(new Location(geographic.Latitude, geographic.Longitude), Colors.Red);

            if (geographic.Latitude1 > 0 || geographic.Latitude1 < 0)
                PlaceDot(new Location(geographic.Latitude1, geographic.Longitude1), Colors.Red);

            if (geographic.Latitude2 > 0 || geographic.Latitude2 < 0)
                PlaceDot(new Location(geographic.Latitude2, geographic.Longitude2), Colors.Red);

            if (geographic.Latitude3 > 0 || geographic.Latitude3 < 0)
                PlaceDot(new Location(geographic.Latitude3, geographic.Longitude3), Colors.Red);

            //Line  evtl. not ok
            if ((geographic.Latitude > 0 || geographic.Latitude < 0) && (geographic.Latitude1 > 0 || geographic.Latitude1 < 0))
                DrawLine(new Location(geographic.Latitude, geographic.Longitude), new Location(geographic.Latitude1, geographic.Longitude1));
            if ((geographic.Latitude1 > 0 || geographic.Latitude1 < 0) && (geographic.Latitude2 > 0 || geographic.Latitude2 < 0))
                DrawLine(new Location(geographic.Latitude1, geographic.Longitude1), new Location(geographic.Latitude2, geographic.Longitude2));
            if ((geographic.Latitude2 > 0 || geographic.Latitude2 < 0) && (geographic.Latitude3 > 0 || geographic.Latitude3 < 0))
                DrawLine(new Location(geographic.Latitude2, geographic.Longitude2), new Location(geographic.Latitude3, geographic.Longitude3));
            if ((geographic.Latitude2 > 0 || geographic.Latitude2 < 0) && geographic.Latitude3 == 0.0)
                DrawLine(new Location(geographic.Latitude2, geographic.Longitude2), new Location(geographic.Latitude, geographic.Longitude));
            if (geographic.Latitude3 > 0 || geographic.Latitude3 < 0)
                DrawLine(new Location(geographic.Latitude3, geographic.Longitude3), new Location(geographic.Latitude, geographic.Longitude));

            MapWithPolygon.Focus();
        }  
 

    
        private void ClearPlSpeciesButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchPlSpecies.Text = "";
            TbSearchPlSpecies.Focus();
        }  
       
        private void ClearGenusButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchGenus.Text = "";
            TbSearchGenus.Focus();
        }  
         
        private void ClearSpeciesgroupButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchSpeciesgroup.Text = "";
            TbSearchSpeciesgroup.Focus();
        }  
         
        private void ClearNameButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchName.Text = "";
            TbSearchName.Focus();
        }  
         
        private void ClearImageButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchImage.Text = "";
            TbSearchImage.Focus();
        }  
         
        private void ClearSynonymButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchSynonym.Text = "";
            TbSearchSynonym.Focus();
        }  
         
        private void ClearGeographicButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchGeographic.Text = "";
            TbSearchGeographic.Focus();
        }  
     
        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            MePlayer.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            MePlayer.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            MePlayer.Stop();
        }      
        
        private void ClearRefExpertButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchRefExpert.Text = "";
            TbSearchRefExpert.Focus();
        }

        private void ClearRefSourceButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchRefSource.Text = "";
            TbSearchRefSource.Focus();
        }

        private void ClearRefAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchRefAuthor.Text = "";
            TbSearchRefAuthor.Focus();
        }

        private void ClearCommentButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchComment.Text = "";
            TbSearchComment.Focus();
        }           
        
        private void Tbl66GenussesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl66GenussesListScroll.VerticalOffset;
            Tbl66GenussesListScroll.ScrollToVerticalOffset(y - x);
         }   
        
        private void Tbl68SpeciesgroupsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl68SpeciesgroupsListScroll.VerticalOffset;
            Tbl68SpeciesgroupsListScroll.ScrollToVerticalOffset(y - x);
         }   
        

        private void Tbl72PlSpeciessesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl72PlSpeciessesListScroll.VerticalOffset;
            Tbl72PlSpeciessesListScroll.ScrollToVerticalOffset(y - x);
         }   
        

        private void Tbl78NamesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl78NamesListScroll.VerticalOffset;
            Tbl78NamesListScroll.ScrollToVerticalOffset(y - x);
         }   
        
        private void Tbl81ImagesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl81ImagesListScroll.VerticalOffset;
            Tbl81ImagesListScroll.ScrollToVerticalOffset(y - x);
         } 

        private void Tbl84SynonymsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl84SynonymsListScroll.VerticalOffset;
            Tbl84SynonymsListScroll.ScrollToVerticalOffset(y - x);
         } 

        private void Tbl87GeographicsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl87GeographicsListScroll.VerticalOffset;
            Tbl87GeographicsListScroll.ScrollToVerticalOffset(y - x);
         }   
        
        private void Tbl90RefExpertsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl90RefExpertsListScroll.VerticalOffset;
            Tbl90RefExpertsListScroll.ScrollToVerticalOffset(y - x);
        }
        private void Tbl90RefSourcesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl90RefSourcesListScroll.VerticalOffset;
            Tbl90RefSourcesListScroll.ScrollToVerticalOffset(y - x);
        }
        private void Tbl90RefAuthorsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl90RefAuthorsListScroll.VerticalOffset;
            Tbl90RefAuthorsListScroll.ScrollToVerticalOffset(y - x);
        }
        private void Tbl93CommentsListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl93CommentsListScroll.VerticalOffset;
            Tbl93CommentsListScroll.ScrollToVerticalOffset(y - x); 
         }   
 

    }
}   

