using RageLib.Resources.GTA5.PC.Bounds;
using System.Numerics;

namespace RageLib.GTA5.Utilities
{
    public class BVHBuilder
    {
        public struct BVHBuilderItem
        {
            public Vector3 Min;
            public Vector3 Max;
            public Vector3 Center;
            public int Id;
        }

        private int depth;
        private BVHBuilderItem[] Items;

        //public int CalculateSplittingAxis(int startIndex, int endIndex)
        //{
        //    Vector3 means = new Vector3(0.0f);
        //    Vector3 variance = new Vector3(0.0f);
        //
        //    int numIndices = endIndex - startIndex;
        //
        //    for (int i = startIndex; i < endIndex; i++)
        //    {
        //        var center = (GetAABBMin(i) + GetAABBMax(i)) * 0.5f;
        //        means += center;
        //    }
        //    means *= 1.0f / numIndices;
        //
        //    for (int i = startIndex; i < endIndex; i++)
        //    {
        //        var center = (GetAABBMin(i) + GetAABBMax(i)) * 0.5f;
        //        var diff2 = center - means;
        //        diff2 = diff2 * diff2;
        //        variance += diff2;
        //    }
        //    variance *= 1.0f / (numIndices - 1);
        //
        //    // Get Max axis
        //    return variance.X < variance.Y ? (variance.Y < variance.Z ? 2 : 1) : (variance.X < variance.Z ? 2 : 0);
        //}

        //public int SortAndCalculateSplittingIndex(int startIndex, int endIndex, int splitAxis)
        //{
        //    Vector3 means = new Vector3(0.0f);
        //    int numIndices = endIndex - startIndex;
        //
        //    for (int i = startIndex; i < endIndex; i++)
        //    {
        //        var center = (GetAABBMin(i) + GetAABBMin(i)) * 0.5f;
        //        means += center;
        //    }
        //    means *= 1.0f / numIndices;
        //
        //    float splitValue = splitAxis == 0 ? means.X : splitAxis == 1 ? means.Y : means.Z;
        //}

        public BVHBuilder(BoundBVH boundBVH, int maxPerNode)
        {
            depth = 4;
            BVH bvh = new BVH();
            Items = new BVHBuilderItem[boundBVH.PrimitivesCount];

            bvh.BoundingBoxMax = new Vector4(boundBVH.BoundingBoxMax, float.NaN);
            bvh.BoundingBoxMin = new Vector4(boundBVH.BoundingBoxMin, float.NaN);
            var margin = new Vector3(boundBVH.Margin);

            var primitivesSpan = boundBVH.Primitives.AsSpan();

            for (int i = 0; i < boundBVH.PrimitivesCount; i++)
            {
                Vector3 Min = new Vector3(float.MaxValue);
                Vector3 Max = new Vector3(float.MinValue);
                Vector3 Center = Vector3.Zero;

                if (primitivesSpan[i].PrimitiveType == BoundPrimitiveType.Triangle)
                {
                    ref BoundPrimitiveTriangle triangle = ref primitivesSpan[i].AsTriangle();

                    var vertex1 = boundBVH.GetVertexOffset(triangle.VertexIndex1);
                    var vertex2 = boundBVH.GetVertexOffset(triangle.VertexIndex2);
                    var vertex3 = boundBVH.GetVertexOffset(triangle.VertexIndex3);

                    Min = Vector3.Min(Vector3.Min(vertex1, vertex2), vertex3);
                    Max = Vector3.Max(Vector3.Max(vertex1, vertex2), vertex3);
                    Center = (vertex1 + vertex2 + vertex3) / 3.0f;
                }
                else if (primitivesSpan[i].PrimitiveType == BoundPrimitiveType.Sphere)
                {
                    ref BoundPrimitiveSphere sphere = ref primitivesSpan[i].AsSphere();

                    Center = boundBVH.GetVertexOffset(sphere.VertexIndex);
                    var radius = new Vector3(sphere.Radius);
                    Min = Center - radius;
                    Max = Center + radius;
                }
                else if (primitivesSpan[i].PrimitiveType == BoundPrimitiveType.Capsule)
                {
                    ref BoundPrimitiveCapsule capsule = ref primitivesSpan[i].AsCapsule();

                    var vertex1 = boundBVH.GetVertexOffset(capsule.VertexIndex1);
                    var vertex2 = boundBVH.GetVertexOffset(capsule.VertexIndex2);
                    var radius = new Vector3(capsule.Radius);
                    Center = (vertex1 + vertex2) * 0.5f;
                    Min = Vector3.Min(vertex1, vertex2) - radius;
                    Max = Vector3.Min(vertex1, vertex2) - radius;
                }
                else if (primitivesSpan[i].PrimitiveType == BoundPrimitiveType.Box)
                {
                    ref BoundPrimitiveBox box = ref primitivesSpan[i].AsBox();

                    var vertex1 = boundBVH.GetVertexOffset(box.VertexIndex1);
                    var vertex2 = boundBVH.GetVertexOffset(box.VertexIndex2);
                    var vertex3 = boundBVH.GetVertexOffset(box.VertexIndex3);
                    var vertex4 = boundBVH.GetVertexOffset(box.VertexIndex4);
                    Center = (vertex1 + vertex2 + vertex3 + vertex4) * 0.25f;
                    Min = Vector3.Min(Vector3.Min(Vector3.Min(vertex1, vertex2), vertex3), vertex4);
                    Max = Vector3.Max(Vector3.Max(Vector3.Max(vertex1, vertex2), vertex3), vertex4);
                }
                else if (primitivesSpan[i].PrimitiveType == BoundPrimitiveType.Cylinder)
                {
                    ref BoundPrimitiveCylinder cylinder = ref primitivesSpan[i].AsCylinder();
                    var vertex1 = boundBVH.GetVertexOffset(cylinder.VertexIndex1);
                    var vertex2 = boundBVH.GetVertexOffset(cylinder.VertexIndex2);
                    var radius = new Vector3(cylinder.Radius);
                    Center = (vertex1 + vertex2) * 0.5f;
                    Min = Vector3.Min(vertex1, vertex2) - radius;
                    Max = Vector3.Min(vertex1, vertex2) - radius;
                }
                else continue;

                Min -= margin;
                Max += margin;

                Items[i] = new BVHBuilderItem()
                {
                    Min = Min,
                    Max = Max,
                    Center = Center,
                    Id = i
                };
            }

            ComputeBVH(bvh, Items, maxPerNode);
        }

        public BVHBuilder(BoundComposite boundComposite, int maxPerNode)
        {
            depth = 1;
            BVH bvh = new BVH();
            Items = new BVHBuilderItem[boundComposite.NumBounds];

            bvh.BoundingBoxMax = new Vector4(boundComposite.BoundingBoxMax, float.NaN);
            bvh.BoundingBoxMin = new Vector4(boundComposite.BoundingBoxMin, float.NaN);
            var margin = new Vector3(boundComposite.Margin);

            for (int i = 0; i < boundComposite.NumBounds; i++)
            {
                var child = boundComposite.Bounds[i];
                var transform = boundComposite.CurrentMatrices[i];

                var Min = child.BoundingBoxMin;
                Min = Vector3.Transform(Min, transform);
                
                var Max = child.BoundingBoxMax;
                Max = Vector3.Transform(Max, transform);

                var Center = (Max + Min) * 0.5f;

                Min -= margin;
                Max += margin;

                Items[i] = new BVHBuilderItem()
                {
                    Min = Min,
                    Max = Max,
                    Center = Center,
                    Id = i
                };
            }

            ComputeBVH(bvh, Items, maxPerNode);
        }

        public void ComputeBVH(BVH bvh, BVHBuilderItem[] items, int maxPerNode)
        {

        }
    }
}
