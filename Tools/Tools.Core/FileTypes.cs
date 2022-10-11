// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

namespace Tools.Core;

public static class FileTypes
{
    public sealed class FileType
    {
        public string Name { get; }
        public string Extension { get; }

        internal FileType(string title, string extension)
        {
            Name = title;
            Extension = extension;
        }
    }

    public static class Rage
    {
        public static readonly FileType RagePackFile = new("Rage Pack File", ".rpf");

        public static class Resources
        {
            public static readonly FileType Bound = new("Bound", ".ybn");
            public static readonly FileType BoundDictionary = new("Bound Dictionary", ".ybd");
            public static readonly FileType ClipDictionary = new("Clip Dictionary", ".ycd");
            public static readonly FileType ClothDictionary = new("Cloth Dictionary", ".yld");
            public static readonly FileType Drawable = new("Drawable", ".ydr");
            public static readonly FileType DrawableDictionary = new("Drawable Dictionary", ".ydd");
            public static readonly FileType ExpressionDictionary = new("Expression Dictionary", ".yed");
            public static readonly FileType FilterDictionary = new("Filter Dictionary", ".yfd");
            public static readonly FileType Fragment = new("Fragment", ".yft");
            public static readonly FileType Manifest = new("Manifest", ".ymf");
            public static readonly FileType Map = new("Maps", ".ymap");
            public static readonly FileType Meta = new("Meta", ".ymt");
            public static readonly FileType Navigations = new("Navigations", ".ynv");
            public static readonly FileType Nodes = new("Nodes", ".ynd");
            public static readonly FileType Particles = new("Particles", ".ypt");
            public static readonly FileType Script = new("Script", ".ysc");
            public static readonly FileType TextureDictionary = new("Texture Dictionary", ".ytd");
            public static readonly FileType Types = new("Types", ".ytyp");
            public static readonly FileType VehicleRecords = new("Vehicle Records", ".yvr");
            public static readonly FileType WaypointRecords = new("Waypoint Records", ".ywr");
        }
    }
    
    public static readonly IReadOnlyDictionary<string, FileType> AllTypes = new Dictionary<string, FileType>()
    {
        { Rage.RagePackFile.Extension,                      Rage.RagePackFile },

        { Rage.Resources.Bound.Extension,                   Rage.Resources.Bound },
        { Rage.Resources.BoundDictionary.Extension,         Rage.Resources.BoundDictionary },
        { Rage.Resources.ClipDictionary.Extension,          Rage.Resources.ClipDictionary },
        { Rage.Resources.ClothDictionary.Extension,         Rage.Resources.ClothDictionary },
        { Rage.Resources.DrawableDictionary.Extension,      Rage.Resources.DrawableDictionary },
        { Rage.Resources.ExpressionDictionary.Extension,    Rage.Resources.ExpressionDictionary },
        { Rage.Resources.FilterDictionary.Extension,        Rage.Resources.FilterDictionary },
        { Rage.Resources.Fragment.Extension,                Rage.Resources.Fragment },
        { Rage.Resources.Manifest.Extension,                Rage.Resources.Manifest },
        { Rage.Resources.Map.Extension,                     Rage.Resources.Map },
        { Rage.Resources.Meta.Extension,                    Rage.Resources.Meta },
        { Rage.Resources.Nodes.Extension,                   Rage.Resources.Nodes },
        { Rage.Resources.Particles.Extension,               Rage.Resources.Particles },
        { Rage.Resources.Script.Extension,                  Rage.Resources.Script },
        { Rage.Resources.TextureDictionary.Extension,       Rage.Resources.TextureDictionary },
        { Rage.Resources.Types.Extension,                   Rage.Resources.Types },
        { Rage.Resources.VehicleRecords.Extension,          Rage.Resources.VehicleRecords },
        { Rage.Resources.WaypointRecords.Extension,         Rage.Resources.WaypointRecords },
    };
}