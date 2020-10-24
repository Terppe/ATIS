using System;
using System.Windows;
using System.Windows.Controls;

namespace ATIS.Ui.Helper
{
    public class AutoScrollBehavior
    {
        public static bool GetTopMouseScrollPriority(ScrollViewer obj)
        {
            return (bool)obj.GetValue(TopMouseScrollPriorityProperty);
        }

        public static void SetTopMouseScrollPriority(ScrollViewer obj, bool value)
        {
            obj.SetValue(TopMouseScrollPriorityProperty, value);
        }

        public static readonly DependencyProperty TopMouseScrollPriorityProperty =
            DependencyProperty.RegisterAttached("TopMouseScrollPriority", typeof(bool), typeof(AutoScrollBehavior), new PropertyMetadata(false, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = d as ScrollViewer;
            if (scrollViewer == null)
                throw new InvalidOperationException($"{nameof(AutoScrollBehavior)}.{nameof(TopMouseScrollPriorityProperty)} can only be applied to controls of type {nameof(ScrollViewer)}");
            if (e.NewValue == e.OldValue)
                return;
            if ((bool)e.NewValue)
                scrollViewer.PreviewMouseWheel += ScrollViewer_PreviewMouseWheel;
            else
                scrollViewer.PreviewMouseWheel -= ScrollViewer_PreviewMouseWheel;
        }

        private static void ScrollViewer_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var scrollViewer = (ScrollViewer)sender;
            scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
    /*  public static class AutoScrollBehavior
      {
          public static readonly DependencyProperty AutoScrollProperty =
              DependencyProperty.RegisterAttached("AutoScroll", typeof(bool), typeof(AutoScrollBehavior), new PropertyMetadata(false, AutoScrollPropertyChanged));


          public static void AutoScrollPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
          {
              var scrollViewer = obj as ScrollViewer;
              if (scrollViewer != null && (bool)args.NewValue)
              {
                  scrollViewer.ScrollChanged += ScrollViewer_ScrollChanged;
                  scrollViewer.ScrollToEnd();
              }
              else
              {
                  scrollViewer.ScrollChanged -= ScrollViewer_ScrollChanged;
              }
          }

          private static void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
          {
              // Only scroll to bottom when the extent changed. Otherwise you can't scroll up
              if (e.ExtentHeightChange != 0)
              {
                  var scrollViewer = sender as ScrollViewer;
                  scrollViewer?.ScrollToBottom();
              }
          }

          public static bool GetAutoScroll(DependencyObject obj)
          {
              return (bool)obj.GetValue(AutoScrollProperty);
          }

          public static void SetAutoScroll(DependencyObject obj, bool value)
          {
              obj.SetValue(AutoScrollProperty, value);
          }
      }
      */
}
