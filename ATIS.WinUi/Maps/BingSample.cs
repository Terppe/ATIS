﻿using BruTile.Cache;
using BruTile.Predefined;
using Mapsui.Styles;
using Mapsui.Tiling.Fetcher;
using Mapsui.Tiling.Layers;
using System.Threading.Tasks;
using Mapsui;

namespace ATIS.WinUi.Maps;
public class BingSample : ISample
{
    public string Name => "3 Virtual Earth";
    public string Category => "Demo";
    public Task<Map> CreateMapAsync()
    {
        return Task.FromResult(CreateMap(BingArial.DefaultCache));
    }

    public static Map CreateMap(IPersistentCache<byte[]>? persistentCache, KnownTileSource source = KnownTileSource.BingAerial)
    {
        var map = new Map();

        var apiKey = "Ai4CQexF-DWddfmN5GMnm8RD19EW_5adcMQAu9Rdzw5LiMxAQ-DeLBKVEG9AcLMg"; // Contact Microsoft about how to use this
        map.Layers.Add(new TileLayer(KnownTileSources.Create(source, apiKey, persistentCache),
                dataFetchStrategy: new DataFetchStrategy()) // DataFetchStrategy prefetches tiles from higher levels
        {
            Name = "Bing Aerial",
        });
        map.Home = n => n.CenterOnAndZoomTo(new MPoint(1059114.80157058, 5179580.75916194), n.Resolutions[14]);
        map.BackColor = Color.FromString("#000613");

        return map;
    }
}
