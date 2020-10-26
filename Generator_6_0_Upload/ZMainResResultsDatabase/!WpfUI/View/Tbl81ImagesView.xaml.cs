using System;  

     
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Threading;  
 

      //  Tbl81ImagesView.xaml.cs Skriptdatum:  14.11.2017  10:32     

namespace WPFUI.Views.Database
{  

    /// <summary>
    /// Interactionslogic for Tbl81ImagesView.xaml
    /// </summary>
    public partial class Tbl81ImagesView : UserControl
   {      

    
        private Point _origin;
        private Point _start;

        public Tbl81ImagesView()
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
 

    
        private void ClearImageButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchImage.Text = "";
            TbSearchImage.Focus();
        }  
       
        private void ClearFiSpeciesButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchFiSpecies.Text = "";
            TbSearchFiSpecies.Focus();
        }  
         
        private void ClearPlSpeciesButton_Click(object sender, RoutedEventArgs e)
        {
            TbSearchPlSpecies.Text = "";
            TbSearchPlSpecies.Focus();
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
          
        private void Tbl69FiSpeciessesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl69FiSpeciessesListScroll.VerticalOffset;
            Tbl69FiSpeciessesListScroll.ScrollToVerticalOffset(y - x);
         }   
        
        private void Tbl72PlSpeciessesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl72PlSpeciessesListScroll.VerticalOffset;
            Tbl72PlSpeciessesListScroll.ScrollToVerticalOffset(y - x);
         }   
        

        private void Tbl81ImagesListScroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eargs = e;
            var x = (double)eargs.Delta;
            var y = Tbl81ImagesListScroll.VerticalOffset;
            Tbl81ImagesListScroll.ScrollToVerticalOffset(y - x);
         }   
 

    }
}   

