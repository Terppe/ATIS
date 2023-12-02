using Mapsui;

namespace ATIS.WinUi.Maps;
public interface ISample : ISampleBase
{
    Task<Map> CreateMapAsync();
}