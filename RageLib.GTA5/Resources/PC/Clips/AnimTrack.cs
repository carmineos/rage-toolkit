// Copyright © Neodymium, carmineos and contributors. See LICENSE.md in the repository root for more information.

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Clips
{
    // crAnimTrack ?
    public struct AnimTrack : IResourceStruct<AnimTrack>
    {
        // structure data
        public ushort BoneId;
        public AnimTrackType TrackType;
        public byte TrackId;

        public AnimTrack ReverseEndianness()
        {
            return new AnimTrack()
            {
                BoneId = EndiannessExtensions.ReverseEndianness(BoneId),
                TrackType = (AnimTrackType)EndiannessExtensions.ReverseEndianness((byte)TrackType),
                TrackId = EndiannessExtensions.ReverseEndianness(TrackId),
            };
        }
    }

    public enum AnimTrackType : byte
    {
        Position = 0,
        Rotation = 1,
        Scale = 2,
    }
}
