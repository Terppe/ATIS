using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Styles;
using Mapsui.Tiling;
using NetTopologySuite.Geometries;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Projections;
using Mapsui.Widgets.MouseCoordinatesWidget;
using System.Diagnostics.CodeAnalysis;
using Microsoft.UI.Xaml;

namespace ATIS.WinUi.Maps;
public class MapAfrica : ISample
{
    public string Name => "Africa";
    public string Category => "Continent";

    [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP001:Dispose created")]
    [SuppressMessage("IDisposableAnalyzers.Correctness", "IDISP004:Don't ignore created IDisposable")]
    public Task<Map> CreateMapAsync()
    {
        var map = new Map();
        map.Layers.Add(OpenStreetMap.CreateTileLayer());
        //    new City { CityName = "Kinshasa", Lat = -4.33, Long = 15.31, Population = 6704352, Country = "Congo (Kinshasa)" },

        //  var centerOfAfrica = new MPoint(15.31, -4.33); ok
      //  var centerOfAfrica = new MPoint(-0.50872, 15.79149);
        var centerOfAfrica = new MPoint(15.79149, -0.50872);
        // OSM uses spherical mercator coordinates. So transform the lon lat coordinates to spherical mercator
        var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(centerOfAfrica.X, centerOfAfrica.Y).ToMPoint();
        // Set the center of the viewport to the coordinate. The UI will refresh automatically
        // Additionally you might want to set the resolution, this could depend on your specific purpose
        map.Home = n => n.CenterOnAndZoomTo(sphericalMercatorCoordinate, n.Resolutions[4]);

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
    private void MenuItem_Click_Africa(object sender, RoutedEventArgs e)
    {
        //MyMap.Center = new Location(-0.50872, 15.79149, 4.0);
        //MyMap.ZoomLevel = 4.0;
    }

}
