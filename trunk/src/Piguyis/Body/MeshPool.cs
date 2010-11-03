using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer;
using Microsoft.DirectX;

namespace AlumnoEjemplos.Piguyis.Body
{
    class MeshPool
    {
        private static MeshPool instance;
        private static Dictionary<string, TgcMesh> meshMap;
        private const float MESH_SHPERE_SIZE = 0.1375f;
        public const string SHPERE_TYPE = "shpere";

        private MeshPool()
        {
            meshMap = new Dictionary<string, TgcMesh>(1);
            TgcScene scene = new TgcSceneLoader().loadSceneFromFile(
                GuiController.Instance.AlumnoEjemplosMediaDir + "ModelosTgc\\Sphere\\" + "Sphere-TgcScene.xml",
                GuiController.Instance.AlumnoEjemplosMediaDir + "ModelosTgc\\Sphere\\");
            meshMap.Add(SHPERE_TYPE, scene.Meshes[0]);
        }

        public static MeshPool Instance {
            get {
                    if (instance == null)
                        instance = new MeshPool();
                    return instance;
                }
        }

        public TgcMesh getMeshToRender(string type, Vector3 pos, float scale)
        {
            TgcMesh s = meshMap[type];
            s.Position = pos;
            s.Scale = new Vector3(MESH_SHPERE_SIZE * scale, MESH_SHPERE_SIZE * scale, MESH_SHPERE_SIZE * scale);
            return s;
        }

    }
}
