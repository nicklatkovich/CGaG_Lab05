using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGaG_Lab05 {
    public static class SimpleUtils {

        public static void DrawLineTriangle(this Game thread, Vector3 pos1, Vector3 pos2, Vector3 pos3, Color color) {
            VertexPositionColor[ ] points = new VertexPositionColor[3];
            points[0] = new VertexPositionColor(pos1, color);
            points[1] = new VertexPositionColor(pos2, color);
            points[2] = new VertexPositionColor(pos3, color);
            thread.DrawLineStrip(points, true);
        }

        public static void DrawTriangle(this Game thread, Vector3 pos1, Vector3 pos2, Vector3 pos3, Color color) {
            VertexPositionColor[ ] vertexList = new VertexPositionColor[ ] {
                new VertexPositionColor(pos1, color),
                new VertexPositionColor(pos2, color),
                new VertexPositionColor(pos3, color),
            };
            short[ ] indices = new short[3];
            for (short i = 0; i < 3; i++) {
                indices[i] = i;
            }

            VertexBuffer vertexBuffer = new VertexBuffer(thread.GraphicsDevice, typeof(VertexPositionColor), vertexList.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertexList);
            thread.GraphicsDevice.SetVertexBuffer(vertexBuffer);

            thread.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, vertexList.Length / 2);
        }

        public static void DrawLineList(this Game thread, VertexPositionColor[ ] vertexList) {
            short[ ] indices = new short[vertexList.Length];
            for (short i = 0; i < vertexList.Length; i++) {
                indices[i] = i;
            }

            VertexBuffer vertexBuffer = new VertexBuffer(thread.GraphicsDevice, typeof(VertexPositionColor), vertexList.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertexList);
            thread.GraphicsDevice.SetVertexBuffer(vertexBuffer);

            thread.GraphicsDevice.DrawPrimitives(PrimitiveType.LineList, 0, vertexList.Length / 2);
        }
        /*
                public static void DrawPrimitive(this Game thread, PrimitiveType type, IVertexType[ ] vertexList, short[ ] indices) {
                    VertexBuffer vertexBuffer = new VertexBuffer(thread.GraphicsDevice, typeof(VertexPositionColor), vertexList.Length, BufferUsage.WriteOnly);
                    vertexBuffer.SetData(vertexList);
                    thread.GraphicsDevice.SetVertexBuffer(vertexBuffer);

                    thread.GraphicsDevice.DrawUserIndexedPrimitives(type, vertexList, 0, vertexList.Length, indices, 0, indices.Length - 1);
                }
                */
        public static void DrawLineList(this Game thread, VertexPositionColor[ ] vertexList, short[ ] indices) {

            VertexBuffer vertexBuffer = new VertexBuffer(thread.GraphicsDevice, typeof(VertexPositionColor), vertexList.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertexList);
            thread.GraphicsDevice.SetVertexBuffer(vertexBuffer);

            thread.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.LineList, vertexList, 0, vertexList.Length, indices, 0, indices.Length / 2);
        }

        public static void DrawLineList(this Game thread, VertexPositionColor[ ] vertexList, short[ ] indices, bool[ ] visibleLines) {
            uint resultSize = 0;
            for (uint i = 0; i < visibleLines.Length; i++) {
                if (visibleLines[i]) {
                    resultSize++;
                }
            }
            short[ ] result = new short[resultSize * 2];
            uint pos = 0;
            for (uint i = 0; i < visibleLines.Length; i++) {
                if (visibleLines[i]) {
                    result[pos] = indices[2 * i];
                    result[pos + 1] = indices[2 * i + 1];
                    pos += 2;
                }
            }
            thread.DrawLineList(vertexList, result);
        }

        public static void DrawLineStrip(this Game thread, VertexPositionColor[ ] vertexList, bool isLooped = false) {
            short[ ] indices = new short[vertexList.Length + (isLooped ? 1 : 0)];
            for (short i = 0; i < vertexList.Length; i++) {
                indices[i] = i;
            }
            if (isLooped) {
                indices[vertexList.Length] = 0;
            }

            thread.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.LineStrip, vertexList, 0, vertexList.Length, indices, 0, indices.Length - 1);
        }

        public static Vector3 SphereToCart(this Vector3 v) {
            float cos = (float)Math.Cos(MathHelper.ToRadians(v.Z));
            return v.X * new Vector3(
                (float)Math.Cos(MathHelper.ToRadians(v.Y)) * cos,
                (float)Math.Sin(MathHelper.ToRadians(v.Z)),
                (float)Math.Sin(MathHelper.ToRadians(v.Y)) * cos);
        }

        public static void Median(ref float value, float min, float max) {
            if (min > max) {
                throw new Exception( );
            }
            if (value < min) {
                value = min;
            }
            if (value > max) {
                value = max;
            }
        }

    }
}
