using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;
using Microsoft.DirectX.Direct3D;
using TgcViewer;
using TgcViewer.Utils;
using Microsoft.DirectX;
using System.Drawing;
using AlumnoEjemplos.PiguYis.Matematica;

namespace AlumnoEjemplos.Piguyis.TGCView
{
    /// <summary>
    /// Herramienta para crear una flecha 3D y renderizarla con color.
    /// </summary>
    public class TgcArrow : IRenderObject
    {

        #region Creacion

        /// <summary>
        /// Crea una línea en base a sus puntos extremos
        /// </summary>
        /// <param name="start">Punto de inicio</param>
        /// <param name="end">Punto de fin</param>
        /// <returns>Línea creada</returns>
        public static TgcArrow fromExtremes(Vector3 start, Vector3 end)
        {
            TgcArrow line = new TgcArrow();
            line.pStart = start;
            line.pEnd = end;
            return line;
        }

        /// <summary>
        /// Crea una línea en base a sus puntos extremos, con el color especificado
        /// </summary>
        /// <param name="start">Punto de inicio</param>
        /// <param name="end">Punto de fin</param>
        /// <param name="color">Color de la línea</param>
        /// <returns>Línea creada</returns>
        public static TgcArrow fromExtremes(Vector3 start, Vector3 end, Color color)
        {
            TgcArrow line = TgcArrow.fromExtremes(start, end);
            line.color = color;
            return line;
        }

        public TgcArrow()
        {
            int verticesConeCount = ((CONE_MESH_RESOLUTION * 2 + 2) * MAX_RINGS);
            this.verticesCone = new CustomVertex.PositionColored[verticesConeCount];
            this.vertices = new CustomVertex.PositionColored[2];
            
            this.color = Color.Green;
            this.dirtyValues = true;                
        }

        #endregion

        #region  Variables
        
        CustomVertex.PositionColored[] vertices;        
        CustomVertex.PositionColored[] verticesCone;        
        Vector3 pStart;
        Vector3 pEnd;
        public const int CONE_MESH_RESOLUTION = 20;
        public const float MAX_RADIUS = 2f;
        public const int MAX_RINGS = 8;
        //Mesh cone;
        Color color;
        private bool enabled;
        bool dirtyValues;
        
        #endregion

        #region  Getters y Setters

        /// <summary>
        /// Punto de inicio de la linea
        /// </summary>
        public Vector3 PStart
        {
            get { return pStart; }
            set {   pStart = value;
                    dirtyValues = true;
                }
        }

        /// <summary>
        /// Punto final de la linea
        /// </summary>
        public Vector3 PEnd
        {
            get { return pEnd; }
            set {   pEnd = value;
                    dirtyValues = true;
                }
        }

        /// <summary>
        /// Color de los vértices de la caja
        /// </summary>
        public Color Color
        {
            get { return color; }
            set {   color = value; 
                    dirtyValues = true;
                }
        }

        /// <summary>
        /// Indica si la caja esta habilitada para ser renderizada
        /// </summary>
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        #endregion

        /// <summary>
        /// Actualizar parámetros de la línea en base a los valores configurados
        /// </summary>
        private void updateValues()
        {
            int index = 0;
            int c = color.ToArgb();
            vertices[0] = new CustomVertex.PositionColored(pStart, c);
            vertices[1] = new CustomVertex.PositionColored(pEnd, c);
            
            float step = FastMath.PI2 / (float)CONE_MESH_RESOLUTION;
            Vector3 position;
            float radius;            
            for (int ring = 0; ring < MAX_RINGS; ring++)
            {
                position = pEnd + new Vector3(0f, 0f, ((float)ring * 2 / (float)MAX_RINGS));
                radius = MAX_RADIUS - ((float)ring * 2 / (float)MAX_RINGS);
                
                for (float a = 0f; a < FastMath.PI2; a += step)
                {
                    verticesCone[index++] = new CustomVertex.PositionColored(new Vector3(FastMath.Cos(a) * radius, FastMath.Sin(a) * radius, 0f) + position, c);
                    verticesCone[index++] = new CustomVertex.PositionColored(new Vector3(FastMath.Cos(a + step) * radius, FastMath.Sin(a + step) * radius, 0f) + position, c);
                }                
            }
        }

        /// <summary>
        /// Renderizar la línea
        /// </summary>
        public void render()
        {
            Device d3dDevice = GuiController.Instance.D3dDevice;
            TgcTexture.Manager texturesManager = GuiController.Instance.TexturesManager;
            texturesManager.clear(0);
            d3dDevice.Material = TgcD3dDevice.DEFAULT_MATERIAL;

            //d3dDevice.Transform.World = Matrix.
            //Actualizar solo si hubo una modificación
            if (dirtyValues)
            {
                updateValues();
                dirtyValues = false;
            }

            d3dDevice.VertexFormat = CustomVertex.PositionColored.Format;
            d3dDevice.DrawUserPrimitives(PrimitiveType.LineList, 1, vertices);
            d3dDevice.DrawUserPrimitives(PrimitiveType.LineList, verticesCone.Length / 2, verticesCone);            
        }

        /// <summary>
        /// Liberar recursos de la línea
        /// </summary>
        public void dispose()
        {
            verticesCone = null;
            vertices = null;
        }
    }
}

