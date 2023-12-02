using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapsui.Nts.Editing;
using Mapsui.UI;

namespace ATIS.WinUi.Maps;
public class EditingAddPolygonSample : IMapControlSample
{
    public string Name => "Editing Add Polygon";
    public string Category => "Editing";
    public void Setup(IMapControl mapControl)
    {
        EditingSample.InitEditMode(mapControl, EditMode.AddPolygon);
    }
}