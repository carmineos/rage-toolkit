// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using System.Collections.Generic;

namespace RageLib.GTA5.Resources.PC
{
    public class ResourceFileType
    {
        public string Extension { get; private set; }
        public int Version { get; private set; }

        public ResourceFileType(string extension, int version)
        {
            this.Extension = extension;
            this.Version = version;
        }
    }

    public class ResourceFileTypes_GTA5_pc
    {
        public static readonly ResourceFileType Bound = new ResourceFileType("ybn", 43);
        public static readonly ResourceFileType BoundDictionary = new ResourceFileType("ybd", 43);
        public static readonly ResourceFileType ClipDictionary = new ResourceFileType("ycd", 46);
        public static readonly ResourceFileType ClothDictionary = new ResourceFileType("yld", 8);
        public static readonly ResourceFileType Drawable = new ResourceFileType("ydr", 165);
        public static readonly ResourceFileType DrawableDictionary = new ResourceFileType("ydd", 165);
        public static readonly ResourceFileType ExpressionDictionary = new ResourceFileType("yed", 25);
        public static readonly ResourceFileType FilterDictionary = new ResourceFileType("yfd", 4);
        public static readonly ResourceFileType Fragment = new ResourceFileType("yft", 162);
        public static readonly ResourceFileType Maps = new ResourceFileType("ymap", 2);
        public static readonly ResourceFileType Meta = new ResourceFileType("ymt", 2);
        public static readonly ResourceFileType Navigations = new ResourceFileType("ynv", 2);
        public static readonly ResourceFileType Nodes = new ResourceFileType("ynd", 1);
        public static readonly ResourceFileType Particles = new ResourceFileType("ypt", 68);
        public static readonly ResourceFileType Script = new ResourceFileType("ysc", 10);
        public static readonly ResourceFileType TextureDictionary = new ResourceFileType("ytd", 13);
        public static readonly ResourceFileType Types = new ResourceFileType("ytyp", 2);
        public static readonly ResourceFileType VehicleRecords = new ResourceFileType("yvr", 1);
        public static readonly ResourceFileType WaypointRecords = new ResourceFileType("ywr", 1);

        public static readonly List<ResourceFileType> AllTypes = new List<ResourceFileType>
        {
            Bound,
            BoundDictionary,
            ClipDictionary,
            ClothDictionary,
            Drawable,
            DrawableDictionary,
            ExpressionDictionary,
            FilterDictionary,
            Fragment,
            Maps,
            Meta,
            Navigations,
            Nodes,
            Particles,
            Script,
            TextureDictionary,
            Types,
            VehicleRecords,
            WaypointRecords
        };
    }
}
