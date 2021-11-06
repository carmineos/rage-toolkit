using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Clips
{
    // crAnimTrack ?
    public struct AnimTrack : IResourceStruct<AnimTrack>
    {
        // structure data
        public ushort BoneId;
        public byte Unknown_02h;
        public byte TrackId;

        public AnimTrack ReverseEndianness()
        {
            return new AnimTrack()
            {
                BoneId = EndiannessExtensions.ReverseEndianness(BoneId),
                Unknown_02h = EndiannessExtensions.ReverseEndianness(Unknown_02h),
                TrackId = EndiannessExtensions.ReverseEndianness(TrackId),
            };
        }
    }
}
