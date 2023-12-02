using BruTile.Cache;
using BruTile.Predefined;
using Mapsui.Styles;
using Mapsui.Tiling.Fetcher;
using Mapsui.Tiling.Layers;
using Mapsui.Layers;
using Mapsui.Nts;
using NetTopologySuite.Geometries;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Widgets.MouseCoordinatesWidget;
using static ATIS.WinUi.Maps.WorldCitiesFeaturesBuilder;
using Microsoft.UI.Xaml;
using Mapsui.Projections;
using Mapsui.Tiling;

namespace ATIS.WinUi.Maps;
public class MapEurope : ISample
{
    public string Name => "Europe";
    public string Category => "Continent";

    //public Task<Map> CreateMapAsync()
    //{
    //    return Task.FromResult(CreateMap(BingArial.DefaultCache));
    //}
    public Task<Map> CreateMapAsync()
    {
        var map = new Map();
        map.Layers.Add(OpenStreetMap.CreateTileLayer());
        //new City { CityName = "London", Lat = 51.5, Long = -0.12, Population = 7994105, Country = "United Kingdom" },

        //   var centerOfEurope = new MPoint(15.79149, -0.50872);
        //  var centerOfEurope = new MPoint(-0.12, 51.5);ok
        //MyMap.Center = new Location(55.17888, 19.51176, 4.0);
        var centerOfEurope = new MPoint(19.51176, 55.17888);
        // OSM uses spherical mercator coordinates. So transform the lon lat coordinates to spherical mercator
        var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(centerOfEurope.X, centerOfEurope.Y).ToMPoint();
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

    public static Map CreateMap(IPersistentCache<byte[]>? persistentCache, KnownTileSource source = KnownTileSource.BingRoads)
    {
        var map = new Map();

        // Mouse Position Widget
        map.Widgets.Add(new MouseCoordinatesWidget(map));

        var apiKey = "Ai4CQexF-DWddfmN5GMnm8RD19EW_5adcMQAu9Rdzw5LiMxAQ-DeLBKVEG9AcLMg"; // Contact Microsoft about how to use this
        map.Layers.Add(new TileLayer(KnownTileSources.Create(source, apiKey, persistentCache),
                dataFetchStrategy: new DataFetchStrategy()) // DataFetchStrategy prefetches tiles from higher levels
            {
                Name = "Bing Aerial",
            });

        //new City { CityName = "London", Lat = 51.5, Long = -0.12, Population = 7994105, Country = "United Kingdom" },

        //   map.Home = n => n.CenterOnAndZoomTo(new MPoint(1270000.0, 5880000.0), n.Resolutions[3]);ok
        //  map.Home = n => n.CenterOnAndZoomTo(new MPoint(55.17888, 19.51176), n.Resolutions[3]);
        //  map.Home = n => n.CenterOnAndZoomTo(new MPoint(51.5, -0.12), n.Resolutions[3]);
          map.Home = n => n.CenterOnAndZoomTo(new MPoint(19.51176, 55.17888), n.Resolutions[3]);
        map.BackColor = Color.FromString("#000613");

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

        return map;
    }
    private void MenuItem_Click_Africa(object sender, RoutedEventArgs e)
    {
        //MyMap.Center = new Location(-0.50872, 15.79149, 4.0);
        //MyMap.ZoomLevel = 4.0;
    }
    private void MenuItem_Click_Antarctica(object sender, RoutedEventArgs e)
    {
        //MyMap.Center = new Location(-79.84140, 4.29547, 4.0);
        //MyMap.ZoomLevel = 4.0;
    }
    private void MenuItem_Click_Asia(object sender, RoutedEventArgs e)
    {
        //MyMap.Center = new Location(57.17668, 95.15212, 4.0);
        //MyMap.ZoomLevel = 4.0;
    }
    private void MenuItem_Click_Australia(object sender, RoutedEventArgs e)
    {
        //MyMap.Center = new Location(-25.95802, 132.27543, 4.0);
        //MyMap.ZoomLevel = 4.0;
    }
    private void MenuItem_Click_SouthAmerica(object sender, RoutedEventArgs e)
    {
        //MyMap.Center = new Location(-17.39255, -63.63278, 4.0);
        //MyMap.ZoomLevel = 4.0;
    }
    private void MenuItem_Click_Europe(object sender, RoutedEventArgs e)
    {
        //MyMap.Center = new Location(55.17888, 19.51176, 4.0);
        //MyMap.ZoomLevel = 4.0;
    }
    private void MenuItem_Click_NorthAmerica(object sender, RoutedEventArgs e)
    {
        //MyMap.Center = new Location(33.35808, -99.49215, 4.0);
        //MyMap.ZoomLevel = 4.0;
    }

}
