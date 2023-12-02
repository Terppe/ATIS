using Mapsui.Nts.Editing;
using Mapsui.UI;

namespace ATIS.WinUi.Maps;
public class EditingAddPointSample : IMapControlSample
{
    public string Name => "Editing Add Point";
    public string Category => "Editing";
    public void Setup(IMapControl mapControl)
    {
        EditingSample.InitEditMode(mapControl, EditMode.AddPoint);
    }
}
