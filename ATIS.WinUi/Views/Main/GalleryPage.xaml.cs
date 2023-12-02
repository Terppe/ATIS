using ATIS.WinUi.Maps;
using Mapsui.Extensions;
using Mapsui;
using Mapsui.Tiling;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using ATIS.WinUi.ViewModels.Main;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Views.Main;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class GalleryPage : Page
{
    public GeosViewModel ViewModel
    {
        get;
    }

    private readonly DispatcherTimer _timer = new()
    {
        Interval = TimeSpan.FromSeconds(3)
    };
    public GalleryPage()
    {
        ViewModel = App.GetService<GeosViewModel>();
        InitializeComponent();

        MapControl.Map.Layers.Add(OpenStreetMap.CreateTileLayer());
        MapControl.Map.Navigator.RotationLock = false;
        MapControl.UnSnapRotationDegrees = 30;
        MapControl.ReSnapRotationDegrees = 5;
        MapControl.Renderer.WidgetRenders[typeof(CustomWidget)] = new CustomWidgetSkiaRenderer();

        ContinentComboBox.SelectionChanged += ContinentComboBoxSelectionChanged;

        FillComboBoxWithContinents();
        FillListWithSamples();

        _timer.Tick += (s, a) =>
        {
            FeatureInfo.Text = "";
            _timer.Stop();
        };
    }

    private void FillComboBoxWithContinents()
    {
        var categories = AllSamples.GetSamples()?.Select(s => s.Category).Distinct().OrderBy(c => c);
        if (categories == null)
            return;

        foreach (var category in categories)
        {
            ContinentComboBox.Items?.Add(category);
        }
        ContinentComboBox.SelectedIndex = 0;
    }

    private void MapOnInfo(object? sender, MapInfoEventArgs args)
    {
        if (args.MapInfo?.Feature != null)
        {
            FeatureInfo.Text = $"Click Info: '{args.MapInfo.Feature.ToDisplayText()}'";
            _timer.Start();
        }
    }

    private void FillListWithSamples()
    {
        //var selectedCategory = ContinentComboBox.SelectedValue?.ToString() ?? "";
        //SampleList.Children.Clear();
        //var samples = AllSamples.GetSamples()?.Where(s => s.Category == selectedCategory);
        //if (samples == null)
        //{
        //    return;
        //}

        //foreach (var sample in samples)
        //{
        //    SampleList.Children.Add(CreateRadioButton(sample));
        //}
    }

    private void ContinentComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        FillListWithSamples();
    }

    private UIElement CreateRadioButton(ISampleBase sample)
    {
        var radioButton = new RadioButton
        {
            //FontSize = 16,
            Content = sample.Name,
            Margin = new Thickness(4)
        };

        radioButton.Click += (s, a) =>
        {
            Catch.Exceptions(async () =>
            {
                MapControl.Map!.Layers.Clear();
                MapControl.Info -= MapOnInfo;
                await sample.SetupAsync(MapControl);
                MapControl.Info += MapOnInfo;
                MapControl.Refresh();
            });
        };

        return radioButton;
    }

    private void RotationSlider_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        var percent = RotationSlider.Value / (RotationSlider.Maximum - RotationSlider.Minimum);
        MapControl.Map.Navigator.RotateTo(percent * 360);
    }

}
