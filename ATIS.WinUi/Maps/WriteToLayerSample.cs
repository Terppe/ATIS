using System.Collections;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Styles;
using Mapsui.Tiling;
using NetTopologySuite.Geometries;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Projections;
using Mapsui.Widgets.MouseCoordinatesWidget;
using Mapsui.Providers;
using System.Diagnostics.CodeAnalysis;
using Mapsui.UI.WinUI;

namespace ATIS.WinUi.Maps;
public class WriteToLayerSample : ISample
{
    public string Name => "4 Add Pins";
    public string Category => "Demo";

    [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP001:Dispose created")]
    [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP004:Don't ignore created IDisposable")]
    public Task<Map> CreateMapAsync()
    {
        var map = new Map();

        map.Layers.Add(OpenStreetMap.CreateTileLayer());

        // Mouse Position Widget
        map.Widgets.Add(new MouseCoordinatesWidget(map));

        var layer = new GenericCollectionLayer<List<IFeature>>
        {
            Style = SymbolStyles.CreatePinStyle()
        };
        map.Layers.Add(layer);

        map.Info += (s, e) =>
        {
            if (e.MapInfo?.WorldPosition == null)
            {
                return;
            }

            // Add a point to the layer using the Info position
            layer?.Features.Add(new GeometryFeature
            {
                Geometry = new Point(e.MapInfo.WorldPosition.X, e.MapInfo.WorldPosition.Y)
            });
            // To notify the map that a redraw is needed.
            layer?.DataHasChanged();
        };

        return Task.FromResult(map);
    }
}
