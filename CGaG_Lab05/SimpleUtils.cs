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

    }
}
