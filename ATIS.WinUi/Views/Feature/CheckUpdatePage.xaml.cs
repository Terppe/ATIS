﻿using Microsoft.UI.Xaml.Controls;
using ATIS.WinUi.Helpers.Update;
using ATIS.WinUi.Helpers;
using ATIS.WinUi.ViewModels.Main;
using ATIS.WinUi.ViewModels.Feature;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Views.Feature;
/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class CheckUpdatePage : Page
{
    public CheckUpdatePageViewModel ViewModel
    {
        get; set;
    }
    private readonly AllDialogs _allDialogs = new();

    public CheckUpdatePage()
    {
        ViewModel = App.GetService<CheckUpdatePageViewModel>();
        InitializeComponent();
    }

    private async void Button_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(TxtUser.Text) || string.IsNullOrEmpty(TxtRepo.Text))
        {
            TxtUser.Text = "Terppe";
            TxtRepo.Text = "ATIS";
        }
        var ver = await UpdateHelper.CheckUpdateAsync(TxtUser.Text, TxtRepo.Text);
        if (ver.IsExistNewVersion)
        {
            TxtReleaseUrl.Text = $"Release Url: {ver.HtmlUrl}";
            TxtCreatedAt.Text = $"Created At: {ver.CreatedAt}";
            TxtPublishedAt.Text = $"Published At {ver.PublishedAt}";
            TxtIsPreRelease.Text = $"Is PreRelease: {ver.IsPreRelease}";
            TxtTagName.Text = $"Tag Name: {ver.TagName}";
            TxtChangelog.Text = $"Changelog: {ver.Changelog}";
            foreach (var item in ver.Assets)
            {
                ListView.Items.Add($"{item.Url}{Environment.NewLine}Size: {item.Size}");
            }
        }
    }
}
