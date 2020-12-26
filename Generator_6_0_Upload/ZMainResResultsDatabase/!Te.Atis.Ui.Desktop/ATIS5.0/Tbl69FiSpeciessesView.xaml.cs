using System;  

     
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using Te.Atis.BusinessLayer;
using Te.Atis.DomainModel;
using Te.Atis.Ui.Desktop.Domain.Helper;
using Te.Atis.Ui.Desktop.Properties; 
 

      //  Tbl69FiSpeciessesView.xaml.cs Skriptdatum:  10.11.2018  10:32     

namespace Te.Atis.Ui.Desktop.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl69FiSpeciessesView.xaml
    /// </summary>
    public partial class Tbl69FiSpeciessesView : UserControl
   {      

    
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;

        private Point _origin;
        private Point _start;
        //Video
        private bool _mediaPlayerIsPlaying;
        private bool _userIsDraggingSlider;
        //Geographic
        private Location _loc1;
        private Location _loc2;
        private Location _loc3;
        private Location _loc4;
        private int _z;

        public Tbl69FiSpeciessesView()
        {       
            _businessLayer = new BusinessLayer.BusinessLayer();
            _entityException = new DbEntityException();
  
            InitializeComponent();   
            IsVisibleChanged += UserControl_IsVisibleChanged;
            //Video
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += timer_Tick;
            timer.Start();

            //Image
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
                    TbSearchFiSpecies.Focus();
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
                var geographic = _businessLayer.SingleListTbl87GeographicsByGeographicId(Convert.ToInt32(TbGeographicId.Text));
                if (Convert.ToInt32(TbGeographicId.Text) != 0)  //update
                {
                    geographic.FiSpeciesID = Convert.ToInt32(FiSpeciesId.Text);
                    geographic.PlSpeciesID = 2;
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
                    geographic = new Tbl87Geographic   //add new
                    {
                        FiSpeciesID = Convert.ToInt32(FiSpeciesId.Text),
                        PlSpeciesID = 2,
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
                    };
                }
                _businessLayer.UpdateGeographic(geographic);
                MapWithPolygon?.Focus();
            }
        }

        private void Button_Click_Point(object sender, RoutedEventArgs e)
        {
            var geographic = _businessLayer.SingleListTbl87GeographicsByGeographicId(Convert.ToInt32(TbGeographicId.Text));
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
           
        #region MyPlayer

        private void TabablzControl_Drop(object sender, DragEventArgs e)
        {
            string filename = (string)((DataObject)e.Data).GetFileDropList()[0];
            MyPlayer.Source = new Uri(filename);
            MyPlayer.LoadedBehavior = MediaState.Manual;
            MyPlayer.UnloadedBehavior = MediaState.Manual;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((MyPlayer.Source != null) && (MyPlayer.NaturalDuration.HasTimeSpan) && (!_userIsDraggingSlider))
            {
                SliProgress.Minimum = 0;
                SliProgress.Maximum = MyPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                SliProgress.Value = MyPlayer.Position.TotalSeconds;
            }
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp4;*.mpg;*.mpeg;*.wmv)|*.mp4;*.mpg;*.mpeg;*.wmv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                MyPlayer.Source = new Uri(openFileDialog.FileName);
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (MyPlayer != null) && (MyPlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MyPlayer.Play();
            _mediaPlayerIsPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MyPlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MyPlayer.Stop();
            _mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            _userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            _userIsDraggingSlider = false;
            MyPlayer.Position = TimeSpan.FromSeconds(SliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LblProgressStatus.Text = TimeSpan.FromSeconds(SliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            MyPlayer.Volume += e.Delta > 0 ? 0.1 : -0.1;
        }
        private void SlVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MyPlayer != null) MyPlayer.Volume = (double)SlVolume.Value;
        }

        #endregion    
 

    }
}   

