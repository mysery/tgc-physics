using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using Microsoft.DirectX;
using TgcViewer.Utils.TgcSceneLoader;
using AlumnoEjemplos.PiguYis.Matematica;

namespace TgcViewer.Utils.TgcGeometry
{
    /// <summary>
    /// Herramienta para dibujar una flecha 3D.
    /// </summary>
    public class TgcArrow : IRenderObject
    {

        #region Creacion

        /// <summary>
        /// Crea una flecha en base a sus puntos extremos
        /// </summary>
        /// <param name="start">Punto de inicio</param>
        /// <param name="end">Punto de fin</param>
        /// <returns>Flecha creada</returns>
        public static TgcArrow fromExtremes(Vector3 start, Vector3 end)
        {
            TgcArrow arrow = new TgcArrow();
            arrow.pStart = start;
            arrow.pEnd = end;
            //arrow.updateValues();
            return arrow;
        }

        /// <summary>
        /// Crea una flecha en base a sus puntos extremos, con el color y el grosor especificado
        /// </summary>
        /// <param name="start">Punto de inicio</param>
        /// <param name="end">Punto de fin</param>
        /// <param name="bodyColor">Color del cuerpo de la flecha</param>
        /// <param name="headColor">Color de la punta de la flecha</param>
        /// <param name="thickness">Grosor del cuerpo de la flecha</param>
        /// <param name="headSize">Tamaño de la punta de la flecha</param>
        /// <returns>Flecha creada</returns>
        public static TgcArrow fromExtremes(Vector3 start, Vector3 end, Color bodyColor, Color headColor, float thickness, Vector2 headSize)
        {
            TgcArrow arrow = new TgcArrow();
            arrow.pStart = start;
            arrow.pEnd = end;
            arrow.bodyColor = bodyColor;
            arrow.headColor = headColor;
            arrow.thickness = thickness;
            arrow.headSize = headSize;
            //arrow.updateValues();
            return arrow;
        }

        /// <summary>
        /// Crea una flecha en base a su punto de inicio y dirección
        /// </summary>
        /// <param name="start">Punto de inicio</param>
        /// <param name="direction">Dirección de la flecha</param>
        /// <returns>Flecha creada</returns>
        public static TgcArrow fromDirection(Vector3 start, Vector3 direction)
        {
            TgcArrow arrow = new TgcArrow();
            arrow.pStart = start;
            arrow.pEnd = start + direction;
            //arrow.updateValues();
            return arrow;
        }

        /// <summary>
        /// Crea una flecha en base a su punto de inicio y dirección, con el color y el grosor especificado
        /// </summary>
        /// <param name="start">Punto de inicio</param>
        /// <param name="direction">Dirección de la flecha</param>
        /// <param name="bodyColor">Color del cuerpo de la flecha</param>
        /// <param name="headColor">Color de la punta de la flecha</param>
        /// <param name="thickness">Grosor del cuerpo de la flecha</param>
        /// <param name="headSize">Tamaño de la punta de la flecha</param>
        /// <returns>Flecha creada</returns>
        public static TgcArrow fromDirection(Vector3 start, Vector3 direction, Color bodyColor, Color headColor, float thickness, Vector2 headSize)
        {
            TgcArrow arrow = new TgcArrow();
            arrow.pStart = start;
            arrow.pEnd = start + direction;
            arrow.bodyColor = bodyColor;
            arrow.headColor = headColor;
            arrow.thickness = thickness;
            arrow.headSize = headSize;
            //arrow.updateValues();
            return arrow;
        }

        #endregion

        #region Atributes
        private bool dirtyValues;
        VertexBuffer vertexBuffer;

        Vector3 pStart;
        /// <summary>
        /// Punto de inicio de la linea
        /// </summary>
        public Vector3 PStart
        {
            get { return pStart; }
            set
            {
                pStart = value;
                dirtyValues = true;
            }
        }

        Vector3 pEnd;
        /// <summary>
        /// Punto final de la linea
        /// </summary>
        public Vector3 PEnd
        {
            get { return pEnd; }
            set
            {
                pEnd = value;
                dirtyValues = true;
            }
        }

        Color bodyColor;
        /// <summary>
        /// Color del cuerpo de la flecha
        /// </summary>
        public Color BodyColor
        {
            get { return bodyColor; }
            set
            {
                bodyColor = value;
                dirtyValues = true;
            }
        }

        Color headColor;
        /// <summary>
        /// Color de la cabeza de la flecha
        /// </summary>
        public Color HeadColor
        {
            get { return headColor; }
            set
            {
                headColor = value;
                dirtyValues = true;
            }
        }

        private bool enabled;
        /// <summary>
        /// Indica si la flecha esta habilitada para ser renderizada
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        private float thickness;
        /// <summary>
        /// Grosor del cuerpo de la flecha. Debe ser mayor a cero.
        /// </summary>
        public float Thickness
        {
            get { return thickness; }
            set { thickness = value; dirtyValues = true; }
        }

        private Vector2 headSize;
        /// <summary>
        /// Tamaño de la cabeza de la flecha. Debe ser mayor a cero.
        /// </summary>
        public Vector2 HeadSize
        {
            get { return headSize; }
            set { headSize = value; dirtyValues = true; }
        }
        #endregion


        public TgcArrow()
        {
            Device d3dDevice = GuiController.Instance.D3dDevice;

            vertexBuffer = new VertexBuffer(typeof(CustomVertex.PositionColored), 54, d3dDevice,
                Usage.Dynamic | Usage.WriteOnly, CustomVertex.PositionColored.Format, Pool.Default);

            this.thickness = 0.06f;
            this.headSize = new Vector2(0.3f, 0.6f);
            this.enabled = true;
            this.bodyColor = Color.Blue;
            this.headColor = Color.LightBlue;
            this.dirtyValues = true;
        }

        /// <summary>
        /// Actualizar parámetros de la flecha en base a los valores configurados
        /// </summary>
        public void updateValues()
        {
            CustomVertex.PositionColored[] vertices = new CustomVertex.PositionColored[54];

            //Crear caja en vertical en Y con longitud igual al módulo de la recta.
            Vector3 lineVec = Vector3.Subtract(pEnd, pStart);
            float lineLength = lineVec.Length();
            Vector3 min = new Vector3(-thickness, 0, -thickness);
            Vector3 max = new Vector3(thickness, lineLength, thickness);

            //Vertices del cuerpo de la flecha
            int bc = bodyColor.ToArgb();
            // Front face
            vertices[0] = new CustomVertex.PositionColored(min.X, max.Y, max.Z, bc);
            vertices[1] = new CustomVertex.PositionColored(min.X, min.Y, max.Z, bc);
            vertices[2] = new CustomVertex.PositionColored(max.X, max.Y, max.Z, bc);
            vertices[3] = new CustomVertex.PositionColored(min.X, min.Y, max.Z, bc);
            vertices[4] = new CustomVertex.PositionColored(max.X, min.Y, max.Z, bc);
            vertices[5] = new CustomVertex.PositionColored(max.X, max.Y, max.Z, bc);

            // Back face (remember this is facing *away* from the camera, so vertices should be clockwise order)
            vertices[6] = new CustomVertex.PositionColored(min.X, max.Y, min.Z, bc);
            vertices[7] = new CustomVertex.PositionColored(max.X, max.Y, min.Z, bc);
            vertices[8] = new CustomVertex.PositionColored(min.X, min.Y, min.Z, bc);
            vertices[9] = new CustomVertex.PositionColored(min.X, min.Y, min.Z, bc);
            vertices[10] = new CustomVertex.PositionColored(max.X, max.Y, min.Z, bc);
            vertices[11] = new CustomVertex.PositionColored(max.X, min.Y, min.Z, bc);

            // Top face
            vertices[12] = new CustomVertex.PositionColored(min.X, max.Y, max.Z, bc);
            vertices[13] = new CustomVertex.PositionColored(max.X, max.Y, min.Z, bc);
            vertices[14] = new CustomVertex.PositionColored(min.X, max.Y, min.Z, bc);
            vertices[15] = new CustomVertex.PositionColored(min.X, max.Y, max.Z, bc);
            vertices[16] = new CustomVertex.PositionColored(max.X, max.Y, max.Z, bc);
            vertices[17] = new CustomVertex.PositionColored(max.X, max.Y, min.Z, bc);

            // Bottom face (remember this is facing *away* from the camera, so vertices should be clockwise order)
            vertices[18] = new CustomVertex.PositionColored(min.X, min.Y, max.Z, bc);
            vertices[19] = new CustomVertex.PositionColored(min.X, min.Y, min.Z, bc);
            vertices[20] = new CustomVertex.PositionColored(max.X, min.Y, min.Z, bc);
            vertices[21] = new CustomVertex.PositionColored(min.X, min.Y, max.Z, bc);
            vertices[22] = new CustomVertex.PositionColored(max.X, min.Y, min.Z, bc);
            vertices[23] = new CustomVertex.PositionColored(max.X, min.Y, max.Z, bc);

            // Left face
            vertices[24] = new CustomVertex.PositionColored(min.X, max.Y, max.Z, bc);
            vertices[25] = new CustomVertex.PositionColored(min.X, min.Y, min.Z, bc);
            vertices[26] = new CustomVertex.PositionColored(min.X, min.Y, max.Z, bc);
            vertices[27] = new CustomVertex.PositionColored(min.X, max.Y, min.Z, bc);
            vertices[28] = new CustomVertex.PositionColored(min.X, min.Y, min.Z, bc);
            vertices[29] = new CustomVertex.PositionColored(min.X, max.Y, max.Z, bc);

            // Right face (remember this is facing *away* from the camera, so vertices should be clockwise order)
            vertices[30] = new CustomVertex.PositionColored(max.X, max.Y, max.Z, bc);
            vertices[31] = new CustomVertex.PositionColored(max.X, min.Y, max.Z, bc);
            vertices[32] = new CustomVertex.PositionColored(max.X, min.Y, min.Z, bc);
            vertices[33] = new CustomVertex.PositionColored(max.X, max.Y, min.Z, bc);
            vertices[34] = new CustomVertex.PositionColored(max.X, max.Y, max.Z, bc);
            vertices[35] = new CustomVertex.PositionColored(max.X, min.Y, min.Z, bc);


            //Vertices del cuerpo de la flecha
            int hc = headColor.ToArgb();
            Vector3 hMin = new Vector3(-headSize.X, lineLength, -headSize.X);
            Vector3 hMax = new Vector3(headSize.X, lineLength + headSize.Y, headSize.X);

            //Bottom face
            vertices[36] = new CustomVertex.PositionColored(hMin.X, hMin.Y, hMax.Z, hc);
            vertices[37] = new CustomVertex.PositionColored(hMin.X, hMin.Y, hMin.Z, hc);
            vertices[38] = new CustomVertex.PositionColored(hMax.X, hMin.Y, hMin.Z, hc);
            vertices[39] = new CustomVertex.PositionColored(hMin.X, hMin.Y, hMax.Z, hc);
            vertices[40] = new CustomVertex.PositionColored(hMax.X, hMin.Y, hMin.Z, hc);
            vertices[41] = new CustomVertex.PositionColored(hMax.X, hMin.Y, hMax.Z, hc);

            //Left face
            vertices[42] = new CustomVertex.PositionColored(hMin.X, hMin.Y, hMin.Z, hc);
            vertices[43] = new CustomVertex.PositionColored(0, hMax.Y, 0, hc);
            vertices[44] = new CustomVertex.PositionColored(hMin.X, hMin.Y, hMax.Z, hc);

            //Right face
            vertices[45] = new CustomVertex.PositionColored(hMax.X, hMin.Y, hMin.Z, hc);
            vertices[46] = new CustomVertex.PositionColored(0, hMax.Y, 0, hc);
            vertices[47] = new CustomVertex.PositionColored(hMax.X, hMin.Y, hMax.Z, hc);

            //Back face
            vertices[48] = new CustomVertex.PositionColored(hMin.X, hMin.Y, hMin.Z, hc);
            vertices[49] = new CustomVertex.PositionColored(0, hMax.Y, 0, hc);
            vertices[50] = new CustomVertex.PositionColored(hMax.X, hMin.Y, hMin.Z, hc);

            //Front face
            vertices[51] = new CustomVertex.PositionColored(hMin.X, hMin.Y, hMax.Z, hc);
            vertices[52] = new CustomVertex.PositionColored(0, hMax.Y, 0, hc);
            vertices[53] = new CustomVertex.PositionColored(hMax.X, hMin.Y, hMax.Z, hc);


            Matrix t = getTransform(ref lineVec);
            //Matrix t = getTransform();

            //Transformar todos los puntos
            for (int i = 0; i < vertices.Length; i++)
			{
                vertices[i].Position = Vector3.TransformCoordinate(vertices[i].Position, t);
			}

            //Cargar vertexBuffer
            vertexBuffer.SetData(vertices, 0, LockFlags.None);
        }

        private Matrix getTransform(ref Vector3 lineVec)
        {
            //Obtener matriz de rotacion respecto del vector de la linea
            lineVec.Normalize();
            Vector3 originalDir = new Vector3(0, 1, 0);
            float angle = FastMath.Acos(Vector3.Dot(originalDir, lineVec));
            Vector3 axisRotation = Vector3.Cross(originalDir, lineVec);
            axisRotation.Normalize();
            return Matrix.RotationAxis(axisRotation, angle) * Matrix.Translation(pStart);            
        }
/*
        private Vector3 IDENTITYZ = new Vector3(0f, 0f, 1f);
        private Vector3 IDENTITYY = new Vector3(0f, 1f, 0f);
        private Vector3 IDENTITYX = new Vector3(1f, 0f, 0f);
        /// <summary>
        /// Mi version de transformada. no me esta andando.
        /// </summary>
        /// <returns></returns>
        private Matrix getTransform()
        {
            Vector3 v1 = IDENTITYX;
            Vector3 v2 = IDENTITYY;
            Vector3 v3 = Vector3.Subtract(pEnd, pStart);
            v3.Normalize();

            if (Vector3.Dot(v3, IDENTITYZ) != 0f)
            {
                v1 = Vector3.Cross(v3, IDENTITYZ);
                v1.Normalize();
                v2 = Vector3.Cross(v3, v1);
                v2.Normalize();
            }

            Matrix mx = new Matrix();
            mx.M11 = v1.X; mx.M12 = v1.Y; mx.M13 = v1.Z;
            mx.M21 = v2.X; mx.M22 = v2.Y; mx.M23 = v2.Z;
            mx.M31 = v3.X; mx.M32 = v3.Y; mx.M33 = v3.Z;
            mx.M44 = 1f;

            return mx * Matrix.Translation(pStart);
        }
*/
        /// <summary>
        /// Renderizar la flecha
        /// </summary>
        public void render()
        {
            if (dirtyValues)
            {
                updateValues();
                dirtyValues = false;
            }

            Device d3dDevice = GuiController.Instance.D3dDevice;
            TgcTexture.Manager texturesManager = GuiController.Instance.TexturesManager;

            texturesManager.clear(0);
            d3dDevice.Material = TgcD3dDevice.DEFAULT_MATERIAL;
            d3dDevice.Transform.World = Matrix.Identity;

            d3dDevice.VertexFormat = CustomVertex.PositionColored.Format;
            d3dDevice.SetStreamSource(0, vertexBuffer, 0);
            d3dDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 18);
        }

        /// <summary>
        /// Liberar recursos de la flecha
        /// </summary>
        public void dispose()
        {
            if (vertexBuffer != null && !vertexBuffer.Disposed)
            {
                vertexBuffer.Dispose();
            }
        }


    }
}
