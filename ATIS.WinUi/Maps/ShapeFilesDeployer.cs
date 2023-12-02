using System;
using System.IO;
using System.Reflection;

namespace ATIS.WinUi.Maps;
public static class ShapeFilesDeployer
{
    public static string ShapeFilesLocation { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ATIS.WinUi.Maps");

    public static void CopyEmbeddedResourceToFile(string shapefile)
    {
        var res = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();

        shapefile = Path.GetFileNameWithoutExtension(shapefile);
        var assembly = typeof(ShapefileSample).GetTypeInfo().Assembly;
        //assembly.CopyEmbeddedResourceToFile("ATIS.WinUi.Maps.World.", ShapeFilesLocation, shapefile + ".dbf");
        //assembly.CopyEmbeddedResourceToFile("ATIS.WinUi.Maps.World.", ShapeFilesLocation, shapefile + ".prj");
        //assembly.CopyEmbeddedResourceToFile("ATIS.WinUi.Maps.World.", ShapeFilesLocation, shapefile + ".shp");
        //assembly.CopyEmbeddedResourceToFile("ATIS.WinUi.Maps.World.", ShapeFilesLocation, shapefile + ".shx");
        //assembly.CopyEmbeddedResourceToFile("ATIS.WinUi.Maps.World.", ShapeFilesLocation, shapefile + ".shp.sidx");
    }
}
