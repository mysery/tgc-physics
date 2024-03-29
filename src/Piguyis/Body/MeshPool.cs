using System.Collections.Generic;
using System.Drawing;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer;
using Microsoft.DirectX;

namespace AlumnoEjemplos.Piguyis.Body
{
    class MeshPool
    {
        private static MeshPool _instance;
        private static Dictionary<string, TgcMesh> _meshMap;
        
        private const float MeshShpereSize = 0.1375f;
        public const string ShpereType = "shpere";
        public const string ShpereHeavyType = "shpereHeavy";
        public const string ShpereHighType = "shpereHigh";
        public const string ShpereMediumType = "shpereMedium";
        public const string ShpereLowType = "shpereLow";
        public const string PlaneXYType = "planeXY";
        public const string PlaneXZType = "planeXZ";
        public const string PlaneYZType = "planeYZ";

        private MeshPool()
        {
            _meshMap = new Dictionary<string, TgcMesh>(8);
            TgcScene scene = new TgcSceneLoader().loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosMediaDir + "ModelosTgc\\Sphere\\" + "Sphere-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosMediaDir + "ModelosTgc\\Sphere\\");
            TgcMesh mesh = scene.Meshes[0];
            _meshMap.Add(ShpereType, mesh);
            
            //Reutilizo la mesh para crear otra con otro color.
            scene = new TgcSceneLoader().loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosMediaDir + "ModelosTgc\\Sphere\\" + "SphereHeavy-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosMediaDir + "ModelosTgc\\Sphere\\");
            mesh = scene.Meshes[0];            
            _meshMap.Add(ShpereHeavyType, mesh);

            //Reutilizo la mesh para crear otra con otro color.
            scene = new TgcSceneLoader().loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosMediaDir + "ModelosTgc\\Sphere\\" + "SphereHigh-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosMediaDir + "ModelosTgc\\Sphere\\");
            mesh = scene.Meshes[0];
            _meshMap.Add(ShpereHighType, mesh);

            //Reutilizo la mesh para crear otra con otro color.
            scene = new TgcSceneLoader().loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosMediaDir + "ModelosTgc\\Sphere\\" + "SphereMedium-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosMediaDir + "ModelosTgc\\Sphere\\");
            mesh = scene.Meshes[0];
            _meshMap.Add(ShpereMediumType, mesh);

            //Reutilizo la mesh para crear otra con otro color.
            scene = new TgcSceneLoader().loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosMediaDir + "ModelosTgc\\Sphere\\" + "SphereLow-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosMediaDir + "ModelosTgc\\Sphere\\");
            mesh = scene.Meshes[0];
            //Color.DimGray;
            _meshMap.Add(ShpereLowType, mesh);
            
            //Modifier de textura
            string textureWallPath = GuiController.Instance.ExamplesMediaDir + "Texturas\\Quake\\TexturePack2\\brick1_1.jpg";
            TgcTexture currentTexture = TgcTexture.createTexture(GuiController.Instance.D3dDevice, textureWallPath);
            //Crear pared
            TgcPlaneWall wallXY = new TgcPlaneWall(
                new Vector3(-5.15f, -5.15f, 0f), new Vector3(10.3f, 10.3f, 10.3f), TgcPlaneWall.Orientations.XYplane, currentTexture);
            wallXY.AutoAdjustUv = true;
            _meshMap.Add(PlaneXYType, wallXY.toMesh(PlaneXYType));
            
            //Nueva textura
            currentTexture = TgcTexture.createTexture(GuiController.Instance.D3dDevice, textureWallPath);
            //Crear pared
            TgcPlaneWall wallXZ = new TgcPlaneWall(
                new Vector3(-5.15f, 0f, -5.15f), new Vector3(10.3f, 10.3f, 10.3f), TgcPlaneWall.Orientations.XZplane, currentTexture);
            wallXZ.AutoAdjustUv = false;
            _meshMap.Add(PlaneXZType, wallXZ.toMesh(PlaneXZType));

            //Nueva textura
            currentTexture = TgcTexture.createTexture(GuiController.Instance.D3dDevice, textureWallPath);
            //Crear pared
            TgcPlaneWall wallYZ = new TgcPlaneWall(
                new Vector3(0f, -5.15f, -5.15f), new Vector3(10.3f, 10.3f, 10.3f), TgcPlaneWall.Orientations.YZplane, currentTexture);
            wallYZ.AutoAdjustUv = false;
            _meshMap.Add(PlaneYZType, wallYZ.toMesh(PlaneYZType));
        }

        public static MeshPool Instance {
            get { return _instance ?? (_instance = new MeshPool()); }
        }

        public TgcMesh GetMeshToRender(string type, Vector3 pos, float scale)
        {
            TgcMesh s = _meshMap[type];
            s.Position = pos;
            s.Scale = new Vector3(MeshShpereSize * scale, MeshShpereSize * scale, MeshShpereSize * scale);
            return s;
        }
    }
}
