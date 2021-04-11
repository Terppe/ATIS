using System;


using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;


//  Tbl81ImagesView.xaml.cs Skriptdatum:  29.12.2020  10:32     

namespace ATIS.Ui.Views.Database.D81Image
{

    /// <summary>
    /// Interactionslogic for ImagesView.xaml
    /// </summary>
    public partial class ImagesView : UserControl
    {


        private Point _origin;
        private Point _start;
        //Video
        private bool _mediaPlayerIsPlaying;
        private bool _userIsDraggingSlider;

        public ImagesView()
        {
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
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                Dispatcher.BeginInvoke(
                DispatcherPriority.ContextIdle,
                new Action(delegate
                {
                    TbSearchImage.Focus();
                }));
            }
        }
        private void TbSearchImage_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Tab || e.Key == Key.Enter)
            //{
            //    BtnGet.Focus();
            //    e.Handled = true;
            //}
        }

        private void ShowImage(object sender, RoutedEventArgs e)
        {
            //var path = TbPath.Text;
            ////  var ret = _extSave.SaveImage(CurrentTbl81Image, selectedPath);
            //ImageSource bild = LoadImageData(path);
            //Image1.Source = bild;
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "All images|*.jpg;*.jpeg;*.jpe;*.bmp;*.gif;*.ico;*.png;*.tif;*.tiff;*.hpd;*.jxr;*.wdp|" +
                         "JPEG image|*.jpg;*.jpeg;*.jpe|Windows BMP image|*.bmp|GIF image|*.gif|Microsoft Windows icon|*.ico|" +
                         "PNG image|*.png|TIFF image|*.tif;*.tiff|JPEG XR|*.hpd;*.jxr;*.wdp";

            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                string selectedFileName = dlg.FileName;
                TbPath.Text = selectedFileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                Image1.Source = bitmap;
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

        #region MyPlayer

        private void TabablzControl_Drop(object sender, DragEventArgs e)
        {
            var filename = (string)((DataObject)e.Data).GetFileDropList()[0];
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
            var openFileDialog = new OpenFileDialog
            {
                Filter =
                    "Media files (*.mp4;*.mpg;*.mpeg;*.wmv;*.flv)|*.mp4;*.mpg;*.mpeg;*.wmv;*.flv|All files (*.*)|*.*"
            };
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

