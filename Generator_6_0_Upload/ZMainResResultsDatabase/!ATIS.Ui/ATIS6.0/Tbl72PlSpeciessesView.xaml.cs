using System;  

     
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Maps.MapControl.WPF;
using Te.Atis.Ui.Desktop.BusinessLayer;
using Te.Atis.Ui.Desktop.Maps;
using Te.Atis.Ui.Desktop.Properties;
using DataObject = System.Windows.DataObject;
using DragEventArgs = System.Windows.DragEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using UserControl = System.Windows.Controls.UserControl; 
 

      //  Tbl72PlSpeciessesView.xaml.cs Skriptdatum:  13.12.2019  12:32     

namespace ATIS.Ui.Views.Database.D72PlSpecies
{  

    /// <summary>
    /// Interactionslogic for PlSpeciessesView.xaml
    /// </summary>
    public partial class PlSpeciessesView : UserControl
   {      

    
        private static IBusinessLayer _businessLayer;
        private static DbEntityException _entityException;

        private Point _origin;
        private Point _start;
        //Video
        private bool _mediaPlayerIsPlaying;
        private bool _userIsDraggingSlider;
        //Geographic
        private bool _isDrawing = false;
        private Location _center;
        private MapPolygon _currentShape;
 
        public Tbl72PlSpeciessesView()
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
            //Key
            MyMap.CredentialsProvider = new ApplicationIdCredentialsProvider(Settings.Default.BingMapKey);
            //Set focus to map
            MyMap.Focus();
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
        private void TbSearchPlSpecies_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                BtnGet.Focus();
                e.Handled = true;
            }
        }   

        #region Geographic      

        //-------------------- Geographic Draw Rectangle on Map---------------------------------------------------
        private void StartDrawing(object sender, RoutedEventArgs e)
        {
            ((ToggleButton)sender).Content = "Stop Drawing";

            //Capture the current center of the map. We will use this to lock the map view.
            _center = MyMap.Center;

            //Add map events
            MyMap.MouseLeftButtonDown += MouseTouchStartHandler;
            MyMap.TouchDown += MouseTouchStartHandler;
            MyMap.MouseMove += MouseTouchMoveHandler;
            MyMap.TouchMove += MouseTouchMoveHandler;
            MyMap.MouseLeftButtonUp += MouseTouchEndHandler;
            MyMap.TouchUp += MouseTouchEndHandler;
            MyMap.ViewChangeOnFrame += MyMap_ViewChangeOnFrame;
        }

        private void StopDrawing(object sender, RoutedEventArgs e)
        {
            ((ToggleButton)sender).Content = "Start Drawing";

            //Remove map events
            MyMap.MouseLeftButtonDown -= MouseTouchStartHandler;
            MyMap.TouchDown += MouseTouchStartHandler;
            MyMap.MouseMove -= MouseTouchMoveHandler;
            MyMap.TouchMove -= MouseTouchMoveHandler;
            MyMap.MouseLeftButtonUp -= MouseTouchEndHandler;
            MyMap.TouchUp -= MouseTouchEndHandler;
            MyMap.ViewChangeOnFrame -= MyMap_ViewChangeOnFrame;
        }

        private void MouseTouchStartHandler(object sender, object e)
        {
            //Optional: Remove any already drawn polygons.
            MyMap.Children.Clear();

            Location startLoc = GetMouseTouchLocation(e);

            //Get the initial location where the user pressed the mouse down.
            if (startLoc != null)
            {

                //Create a polygon that has four corners, all of which are the starting location.
                _currentShape = new MapPolygon()
                {
                    Locations = new LocationCollection()
                    {
                        startLoc,
                        startLoc,
                        startLoc,
                        startLoc
                    },
                    Fill = new SolidColorBrush(Colors.Transparent),
                    Stroke = new SolidColorBrush(Colors.Red),
                    StrokeThickness = 2
                };

                MyMap.Children.Add(_currentShape);

                _isDrawing = true;
            }
        }

        private void MyMap_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            //If drawing keep reseting the center to the original center value when we entered drawing mode. 
            //This will disable panning of the map when we click and drag. 
            MyMap.Center = _center;

            //Optional: Disable rotation of map, useful when using touch.
            MyMap.Heading = 0;
        }

        private void MouseTouchMoveHandler(object sender, object e)
        {
            if (_isDrawing)
            {
                Location currentLoc = GetMouseTouchLocation(e);

                //Get the location where mouse is.
                if (currentLoc != null)
                {
                    var firstLoc = _currentShape.Locations[0];

                    //Update locations 1 - 3 of polygon so as to create a rectangle.
                    _currentShape.Locations[1] = new Location(firstLoc.Latitude, currentLoc.Longitude);
                    _currentShape.Locations[2] = currentLoc;
                    _currentShape.Locations[3] = new Location(currentLoc.Latitude, firstLoc.Longitude);
                }
            }
        }

        private void MouseTouchEndHandler(object sender, object e)
        {
            string specifier = "#000.00000000000000";
            //Update drawing flag so that polygon isn't updated when mouse is moved.
            _isDrawing = false;

            //The rectangle is drawn, grab it's locations and do something with them.
            TbLatitude.Text  = _currentShape.Locations[0].Latitude.ToString(specifier);
            TbLongitude.Text = _currentShape.Locations[0].Longitude.ToString(specifier);

            TbLatitude1.Text = _currentShape.Locations[1].Latitude.ToString(specifier);
            TbLongitude1.Text = _currentShape.Locations[1].Longitude.ToString(specifier);

            TbLatitude2.Text = _currentShape.Locations[2].Latitude.ToString(specifier);
            TbLongitude2.Text = _currentShape.Locations[2].Longitude.ToString(specifier);

            TbLatitude3.Text = _currentShape.Locations[3].Latitude.ToString(specifier);
            TbLongitude3.Text = _currentShape.Locations[3].Longitude.ToString(specifier);

            TbZoomLevel.Text = MyMap.ZoomLevel.ToString(specifier);


        _currentShape = null;

            MyMap.Focus();
        }

        private Location GetMouseTouchLocation(object e)
    {
        Location loc = null;

        if (e is MouseEventArgs)
        {
            MyMap.TryViewportPointToLocation((e as MouseEventArgs).GetPosition(MyMap), out loc);
        }
        else if (e is TouchEventArgs)
        {
            MyMap.TryViewportPointToLocation((e as TouchEventArgs).GetTouchPoint(MyMap).Position, out loc);
        }

        return loc;
    }

        private void MyMap_OnMouseMove(object sender, MouseEventArgs e)
        {
            var p0 = e.GetPosition(MyMap);
            var loc = MyMap.ViewportPointToLocation(p0);

            var pointX = Convert.ToInt16(p0.X);
            var pointY = Convert.ToInt16(p0.Y);

            //   TbMouseX6.Text = pointX.ToString(CultureInfo.InvariantCulture);
            //  TbMouseY6.Text = pointY.ToString(CultureInfo.InvariantCulture);

            //   TbLatitude6.Text = loc.Latitude.ToString(CultureInfo.InvariantCulture);
            //   TbLongitude6.Text = loc.Longitude.ToString(CultureInfo.InvariantCulture);

            //  TbZoom6.Text = MyMap.ZoomLevel.ToString(CultureInfo.InvariantCulture);
        }

        private void CbContinent_DropDownClosed(object sender, EventArgs e)
        {
            var sel = new SelectMaps();
            sel.SelectContinent(MyMap, CbContinent.Text);
        }

        private void CbCountry_DropDownClosed(object sender, EventArgs e)
        {
            var sel = new SelectMaps();
            sel.SelectCountry(MyMap, CbCountry.Text);
        }

        private void MenuItem_Click_DrawSavedArea(object sender, RoutedEventArgs e)
        {
            MyMap.Children.Clear();

            var geographic = _businessLayer.SingleListTbl87GeographicsByGeographicId(Convert.ToInt16(TbGeographicId.Text));
            var startLoc = new Location(geographic.Latitude, geographic.Longitude, geographic.ZoomLevel);
            var startLoc1 = new Location(geographic.Latitude1, geographic.Longitude1, geographic.ZoomLevel);
            var startLoc2 = new Location(geographic.Latitude2, geographic.Longitude2, geographic.ZoomLevel);
            var startLoc3 = new Location(geographic.Latitude3, geographic.Longitude3, geographic.ZoomLevel);

            //Create a polygon that has four corners, all of which are the starting location.
            _currentShape = new MapPolygon()
            {
                Locations = new LocationCollection()
                {
                    startLoc,
                    startLoc1,
                    startLoc2,
                    startLoc3
                },
                Fill = new SolidColorBrush(Colors.Transparent),
                Stroke = new SolidColorBrush(Colors.Red),
                StrokeThickness = 2
            };
            MyMap.Children.Add(_currentShape);
        }

        private void MenuItem_Click_StartDraw(object sender, RoutedEventArgs e)
        {
            StartStopDraw.IsChecked = true;
        }

        private void MenuItem_Click_StopDraw(object sender, RoutedEventArgs e)
        {
            StartStopDraw.IsChecked = false;
        }

        private void MenuItem_Click_ClearStrokeArea(object sender, RoutedEventArgs e)
        {
            MyMap.Children.Clear();

            TbLatitude.Text = "";
            TbLongitude.Text = "";
            TbLatitude1.Text = "";
            TbLongitude1.Text = "";
            TbLatitude2.Text = "";
            TbLongitude2.Text = "";
            TbLatitude3.Text = "";
            TbLongitude3.Text = "";
            TbZoomLevel.Text = "";
        }
        private void MenuItem_Click_Africa(object sender, RoutedEventArgs e)
        {
            MyMap.Center = new Location(-0.50872, 15.79149, 4.0);
            MyMap.ZoomLevel = 4.0;
        }
        private void MenuItem_Click_Antarctica(object sender, RoutedEventArgs e)
        {
            MyMap.Center = new Location(-79.84140, 4.29547, 4.0);
            MyMap.ZoomLevel = 4.0;
        }
        private void MenuItem_Click_Asia(object sender, RoutedEventArgs e)
        {
            MyMap.Center = new Location(57.17668, 95.15212, 4.0);
            MyMap.ZoomLevel = 4.0;
        }
        private void MenuItem_Click_Australia(object sender, RoutedEventArgs e)
        {
            MyMap.Center = new Location(-25.95802, 132.27543, 4.0);
            MyMap.ZoomLevel = 4.0;
        }
        private void MenuItem_Click_SouthAmerica(object sender, RoutedEventArgs e)
        {
            MyMap.Center = new Location(-17.39255, -63.63278, 4.0);
            MyMap.ZoomLevel = 4.0;
        }
        private void MenuItem_Click_Europe(object sender, RoutedEventArgs e)
        {
            MyMap.Center = new Location(55.17888, 19.51176, 4.0);
            MyMap.ZoomLevel = 4.0;
        }
        private void MenuItem_Click_NorthAmerica(object sender, RoutedEventArgs e)
        {
            MyMap.Center = new Location(33.35808, -99.49215, 4.0);
            MyMap.ZoomLevel = 4.0;
        }

        //------------------------------------------------------
        #endregion

        #region Image

        //-----------------------Image----------------------------------------
       
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
            _origin = new Point((int)tt.X, (int)tt.Y);
        }

        private void image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var transformGroup = (TransformGroup)Image1.RenderTransform;
            var transform = (ScaleTransform)transformGroup.Children[0];

            var zoom = e.Delta > 0 ? .2 : -.2;
            transform.ScaleX += zoom;
            transform.ScaleY += zoom;
        }

        #endregion  
          
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

