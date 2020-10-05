using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ATIS.Ui.Views.Log
{
    public class PasswordValidator : FrameworkElement
    {
        static readonly IDictionary<PasswordBox, Brush> PasswordBoxes = new Dictionary<PasswordBox, Brush>();

        public static readonly DependencyProperty Box1Property = DependencyProperty.Register("Box1", typeof(PasswordBox), typeof(PasswordValidator), new PropertyMetadata(Box1Changed));
        public static readonly DependencyProperty Box2Property = DependencyProperty.Register("Box2", typeof(PasswordBox), typeof(PasswordValidator), new PropertyMetadata(Box2Changed));

        public PasswordBox Box1
        {
            get => (PasswordBox)GetValue(Box1Property);
            set => SetValue(Box1Property, value);
        }
        public PasswordBox Box2
        {
            get => (PasswordBox)GetValue(Box2Property);
            set => SetValue(Box2Property, value);
        }

        private static void Box1Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pv = (PasswordValidator)d;
            PasswordBoxes[pv.Box1] = pv.Box1.BorderBrush;

            pv.Box1.LostFocus += (obj, evt) =>
            {
                if (!Regex.IsMatch(pv.Box1.Password,
                @"^[a-zA-Z''!''£''.''*''0-9'\s]{5,20}$"))
                {
                    pv.Box1.BorderBrush = new SolidColorBrush(Colors.Red);
                    //        var msg = new UserMessage { Message = CultRes.StringsRes.RequiredPassword1 };
                    //        Messenger.DefaultMessenger.Send(msg);
                    MessageBox.Show(CultRes.StringsRes.RequiredPassword1);

                }
                else
                {
                    pv.Box1.BorderBrush = PasswordBoxes[pv.Box1];
                    //        var msg = new UserMessage { Message = "" };
                    //        Messenger.DefaultMessenger.Send(msg);
                }
            };

        }
        private static void Box2Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pv = (PasswordValidator)d;
            PasswordBoxes[pv.Box2] = pv.Box2.BorderBrush;

            pv.Box2.LostFocus += (obj, evt) =>
            {
                if (pv.Box1.Password != pv.Box2.Password)
                {
                    pv.Box2.BorderBrush = new SolidColorBrush(Colors.Red);
                    //        var msg = new UserMessage { Message = CultRes.StringsRes.RequiredPassword4 };
                    //        Messenger.DefaultMessenger.Send(msg);
                    MessageBox.Show(CultRes.StringsRes.RequiredPassword4);

                }
                else
                {
                    pv.Box2.BorderBrush = PasswordBoxes[pv.Box2];
                    //        var msg = new UserMessage { Message = "" };
                    //         Messenger.DefaultMessenger.Send(msg);
                }
            };
        }
    }
}
