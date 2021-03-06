﻿using ControlzEx.Theming;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ATIS.Ui.Views.Main
{
    /// <summary>
    /// Interaktionslogik für SettingView.xaml
    /// </summary>
    public partial class SettingView : UserControl
    {
        public static readonly DependencyProperty ColorsProperty
            = DependencyProperty.Register("Colors",
                typeof(List<KeyValuePair<string, Color>>),
                typeof(SettingView),
                new PropertyMetadata(default(List<KeyValuePair<string, Color>>)));

        public List<KeyValuePair<string, Color>> Colors
        {
            get => (List<KeyValuePair<string, Color>>)GetValue(ColorsProperty);
            set => SetValue(ColorsProperty, value);
        }

        public SettingView()
        {
            //   DataContext = new SettingViewModel();

            InitializeComponent();
            this.DataContext = this;

            Colors = typeof(Colors)
                .GetProperties()
                .Where(prop => typeof(Color).IsAssignableFrom(prop.PropertyType))
                .Select(prop => new KeyValuePair<String, Color>(prop.Name, (Color)prop.GetValue(null)))
                .ToList();

            var appTheme = ThemeManager.Current.DetectTheme(Application.Current);
            ThemeManager.Current.ChangeTheme(this, appTheme!);
        }

        //--------------------- AppStyle-------------
        private void ChangeAppThemeButtonClick(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeThemeBaseColor(Application.Current, ((Button)sender).Content.ToString()!);
            Application.Current?.MainWindow?.Activate();
        }

        private void ChangeAppAccentButtonClick(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ChangeThemeColorScheme(Application.Current, ((Button)sender).Content.ToString()!);
            Application.Current?.MainWindow?.Activate();
        }
        private void AccentSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTheme = e.AddedItems.OfType<Theme>().FirstOrDefault();
            if (selectedTheme != null)
            {
                ThemeManager.Current.ChangeTheme(Application.Current, selectedTheme);
                Application.Current?.MainWindow?.Activate();
            }
        }

        private void ColorsSelectorOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedColor = e.AddedItems.OfType<KeyValuePair<string, Color>?>().FirstOrDefault();
            if (selectedColor != null)
            {
                var col = selectedColor.Value;
                var brush = new SolidColorBrush(col.Value);
                //local Background
                Background = brush;
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings["BackgroundBrush"].Value = brush.ToString();

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("AppSettings");
                Application.Current?.MainWindow?.Activate();
            }
        }


        //-----------------------------------------------------------

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rdButton)
            {
                switch (rdButton.Tag.ToString())
                {
                    case "Dark":
                        //                AppSettings.CurrentTheme = ElementTheme.Dark;
                        break;
                    case "light":
                        //                 AppSettings.CurrentTheme = ElementTheme.Light;
                        break;
                }

                //         AppSettings.ThemeUpdate();
            }
        }
    }
}
