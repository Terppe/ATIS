using CommunityToolkit.Mvvm.ComponentModel;

namespace ATIS.WinUi.Models;
public partial class Settings : ObservableObject
{
    public bool IsLightTheme
    {
        get; set;
    }

    public Settings()
    {
        // Required for serialization.
    }

}
