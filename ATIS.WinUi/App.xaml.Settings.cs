using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using ATIS.WinUi.Models;
using Microsoft.UI.Xaml;

namespace ATIS.WinUi;
public partial class App : Application
{
    private const string SettingsFile = "settings.xml";

    public Settings? Settings
    {
        get; set;
    }

    internal void EnsureSettings()
    {
        try
        {
            var applicationData = ApplicationData.Current.LocalSettings.Values[SettingsFile];
            if (applicationData == null)
            {
                Settings = null;
            }
            else
            {
                var serializer = new XmlSerializer(typeof(Settings));
                Settings = serializer.Deserialize(new StringReader(applicationData.ToString()!)) as Settings;
            }
        }
#pragma warning disable 0168
        catch (Exception ex)
#pragma warning restore 0168
        {
            // Unable to read the settings (e.g. broken xml)
            Debugger.Break();
            Settings = null;
            ApplicationData.Current.LocalSettings.Values.Remove(SettingsFile);
        }

        Settings ??= new Settings()
        {
            // Default values here ...
        };

        Settings.PropertyChanged += Settings_PropertyChanged;
    }

    private void Settings_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        // Application reaction here.
    }

    public void SaveSettings()
    {
        try
        {
            var serializer = new XmlSerializer(typeof(Settings));
            var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, Settings);
            var applicationData = stringWriter.ToString();
            ApplicationData.Current.LocalSettings.Values[SettingsFile] = applicationData;
        }
        catch 
        {
            Debugger.Break();
        }
    }
}
