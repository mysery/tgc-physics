using System;
using System.Collections.Generic;
using System.Text;
using TgcViewer.Example;
using TgcViewer;
using Microsoft.DirectX.Direct3D;
using System.Drawing;
using Microsoft.DirectX;
using TgcViewer.Utils.Modifiers;
using TgcViewer.Utils.TgcGeometry;
using TgcViewer.Utils.TgcSceneLoader;
using TgcViewer.Utils.TgcSkeletalAnimation;
using TgcViewer.Utils.Terrain;
using TgcViewer.Utils.Fog;

namespace AlumnoEjemplos.Piguyis
{
    /// <summary>
    /// Ejemplo del alumno
    /// </summary>
    public class EjemploAlumno : TgcExample
    {
        /// <summary>
        /// Categoría a la que pertenece el ejemplo.
        /// Influye en donde se va a haber en el árbol de la derecha de la pantalla.
        /// </summary>
        public override string getCategory()
        {
            return "Alumno";
        }

        /// <summary>
        /// Completar nombre del grupo en formato Grupo NN
        /// </summary>
        public override string getName()
        {
            return "Grupo Piguyis - Escape del Infierno";
        }

        /// <summary>
        /// Completar con la descripción del TP
        /// </summary>
        public override string getDescription()
        {
            return "Tienes que escapar esquivando los obstaculos: Controles de movimiento:w a s d, Salto: BarraEspaciadora, Agacharse: Control-Izq";
        }


        //Velocidad de desplazamiento
        const float VELOCIDAD_DESPLAZAMIENTO = 200f;

        TgcSimpleTerrain terrain;
        string currentHeightmap;
        string currentTexture;
        float currentScaleXZ;
        float currentScaleY;
        TgcSkyBox skyBox;

        List<TgcMesh> obstaculos;
        Vector3 move = new Vector3();

        /// <summary>
        /// Método que se llama una sola vez,  al principio cuando se ejecuta el ejemplo.
        /// Escribir aquí todo el código de inicialización: cargar modelos, texturas, modifiers, uservars, etc.
        /// Borrar todo lo que no haga falta
        /// </summary>
        public override void init()
        {
            //GuiController.Instance: acceso principal a todas las herramientas del Framework

            //Device de DirectX para crear primitivas
            //Device d3dDevice = GuiController.Instance.D3dDevice;

            //Carpeta de archivos Media del alumno
            //string alumnoMediaFolder = GuiController.Instance.AlumnoEjemplosMediaDir;

            this.generateScene();

            ///////////////USER VARS//////////////////

            //Crear una UserVar
            //GuiController.Instance.UserVars.addVar("variablePrueba");
            

            //Cargar valor en UserVar
            //GuiController.Instance.UserVars.setValue("variablePrueba", 5451);



            ///////////////MODIFIERS//////////////////
            //Modifier para habilitar o no el renderizado del BoundingBox del personaje
            GuiController.Instance.Modifiers.addBoolean("showBoundingBox", "Bouding Box", false);

            //Crear un modifier para un valor FLOAT
            //GuiController.Instance.Modifiers.addFloat("valorFloat", -50f, 200f, 0f);
            
            //Crear un modifier para un ComboBox con opciones
            //string[] opciones = new string[]{"opcion1", "opcion2", "opcion3"};
            //GuiController.Instance.Modifiers.addInterval("valorIntervalo", opciones, 0);

            //Crear un modifier para modificar un vértice
            //GuiController.Instance.Modifiers.addVertex3f("valorVertice", new Vector3(-100, -100, -100), new Vector3(50, 50, 50), new Vector3(0, 0, 0));



            ///////////////CONFIGURAR CAMARA ROTACIONAL//////////////////
            //Es la camara que viene por default, asi que no hace falta hacerlo siempre
            //GuiController.Instance.ThirdPersonCamera.Enable = true;
            //Configurar centro al que se mira y distancia desde la que se mira
            //GuiController.Instance.RotCamera.setCamera(new Vector3(0, 0, 0), 100);


            ///////////////CONFIGURAR CAMARA PRIMERA PERSONA//////////////////
            //Camara en primera persona, tipo videojuego FPS
            //Solo puede haber una camara habilitada a la vez. Al habilitar la camara FPS se deshabilita la camara rotacional
            GuiController.Instance.FpsCamera.Enable = true;
            GuiController.Instance.FpsCamera.MovementSpeed = 100f;
            GuiController.Instance.FpsCamera.JumpSpeed = 100f;
            GuiController.Instance.FpsCamera.setCamera(new Vector3(-722.6171f, 495.0046f, -31.2611f), new Vector3(164.9481f, 35.3185f, -61.5394f));

            //Loggear por consola del Framework
            //GuiController.Instance.Logger.log(elemento);
       }

        private void generateScene()
        {
            //Cargar obstaculos y posicionarlos
            TgcSceneLoader loader = new TgcSceneLoader();
            obstaculos = new List<TgcMesh>();
            TgcScene scene;
            TgcMesh obstaculo;

            //Esenario.
            //Path de Heightmap default del terreno y Modifier para cambiarla
            currentHeightmap = GuiController.Instance.AlumnoEjemplosMediaDir + "Heighmaps\\" + "lavaHimap2-64x64.jpg";
            //            GuiController.Instance.Modifiers.addTexture("heightmap", currentHeightmap);

            //Modifiers para variar escala del mapa
            currentScaleXZ = 100f;
            GuiController.Instance.Modifiers.addFloat("scaleXZ", 10f, 250f, currentScaleXZ);
            currentScaleY = 1f;
            GuiController.Instance.Modifiers.addFloat("scaleY", 0.5f, 3f, currentScaleY);

            //Path de Textura default del terreno y Modifier para cambiarla
            currentTexture = GuiController.Instance.AlumnoEjemplosMediaDir + "Heighmaps\\" + "lava.jpg";
            //            GuiController.Instance.Modifiers.addTexture("texture", currentTexture);


            //Cargar terreno: cargar heightmap y textura de color
            terrain = new TgcSimpleTerrain();
            terrain.loadHeightmap(currentHeightmap, currentScaleXZ, currentScaleY, new Vector3(0, 0, 0));
            terrain.loadTexture(currentTexture);

            string texturesPath = GuiController.Instance.AlumnoEjemplosMediaDir + "Texturas\\CubeMaps\\Inferno\\";

            //Crear SkyBox 
            skyBox = new TgcSkyBox();
            skyBox.Center = new Vector3(0, 0, 0);
            skyBox.Size = new Vector3(6000, 6000, 6000);

            //Configurar color
            //skyBox.Color = Color.OrangeRed;

            //Configurar las texturas para cada una de las 6 caras
            skyBox.setFaceTexture(TgcSkyBox.SkyFaces.Up, texturesPath + "posy.jpg");
            skyBox.setFaceTexture(TgcSkyBox.SkyFaces.Down, texturesPath + "negy.jpg");
            skyBox.setFaceTexture(TgcSkyBox.SkyFaces.Left, texturesPath + "negz.jpg");
            skyBox.setFaceTexture(TgcSkyBox.SkyFaces.Right, texturesPath + "posz.jpg");
            skyBox.setFaceTexture(TgcSkyBox.SkyFaces.Front, texturesPath + "negx.jpg");
            skyBox.setFaceTexture(TgcSkyBox.SkyFaces.Back, texturesPath + "posx.jpg");

            //Actualizar todos los valores para crear el SkyBox
            skyBox.updateValues();

            //Obstaculo 1: Malla estatática de Box de formato TGC
            scene = loader.loadSceneFromFile(
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Box\\" + "Box-TgcScene.xml",
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Box\\");
            //Escalarlo, posicionarlo y agregar a array de obstáculos
            obstaculo = scene.Meshes[0];
            obstaculo.Scale = new Vector3(1, 2, 1);
            obstaculo.move(100, 100, 0);
            obstaculos.Add(obstaculo);

            //Obstaculo 2: Malla estatática de Box de formato TGC
            scene = loader.loadSceneFromFile(
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Box\\" + "Box-TgcScene.xml",
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Box\\");
            //Escalarlo, posicionarlo y agregar a array de obstáculos
            obstaculo = scene.Meshes[0];
            obstaculo.Scale = new Vector3(1, 2, 1);
            obstaculo.move(0, 100, 100);
            //Le cambiamos la textura a este modelo particular
            obstaculo.changeDiffuseMaps(new TgcTexture[] { TgcTexture.createTexture(GuiController.Instance.D3dDevice, GuiController.Instance.ExamplesMediaDir + "Texturas\\madera.jpg") });
            obstaculos.Add(obstaculo);

            //Obstaculo 2: Malla estatática de Box de formato TGC
            scene = loader.loadSceneFromFile(
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Box\\" + "Box-TgcScene.xml",
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Box\\");
            //Escalarlo, posicionarlo y agregar a array de obstáculos
            obstaculo = scene.Meshes[0];
            obstaculo.Scale = new Vector3(1, 2, 1);
            obstaculo.move(100, 100, 100);
            obstaculos.Add(obstaculo);
        }

        /// <summary>
        /// Método que se llama cada vez que hay que refrescar la pantalla.
        /// Escribir aquí todo el código referido al renderizado.
        /// Borrar todo lo que no haga falta
        /// </summary>
        /// <param name="elapsedTime">Tiempo en segundos transcurridos desde el último frame</param>
        public override void render(float elapsedTime)
        {
            //Device de DirectX para renderizar
            //Device d3dDevice = GuiController.Instance.D3dDevice;

            //Ver si hay que mostrar el BoundingBox
            bool showBB = (bool)GuiController.Instance.Modifiers.getValue("showBoundingBox");

            ///////////////INPUT//////////////////
            //conviene deshabilitar ambas camaras para que no haya interferencia
            
            //////////////RENDERS/////////////////
            //Renderizar piso
            //Ver si cambio alguno de los valores de escala
            float selectedScaleXZ = (float)GuiController.Instance.Modifiers["scaleXZ"];
            float selectedScaleY = (float)GuiController.Instance.Modifiers["scaleY"];
            if (currentScaleXZ != selectedScaleXZ || currentScaleY != selectedScaleY)
            {
                //Volver a cargar el Heightmap
                currentScaleXZ = selectedScaleXZ;
                currentScaleY = selectedScaleY;
                terrain.loadHeightmap(currentHeightmap, currentScaleXZ, currentScaleY, new Vector3(0, 0, 0));
            }
            terrain.render();            
            skyBox.render();

            //Renderizar obstaculos
            foreach (TgcMesh obstaculo in obstaculos)
            {

                Vector3 posMin = obstaculo.BoundingBox.PMin;
//                obstaculo.moveOrientedY(terrain.HeightmapData[(int)((posMin.X / currentScaleXZ) + terrain.HeightmapData.GetLength(0)),
//                                                              (int)((posMin.Z / currentScaleXZ) + terrain.HeightmapData.GetLength(0))]);
                obstaculo.render();                
                //Renderizar BoundingBox si asi lo pidieron
                if (showBB)
                {
                    obstaculo.BoundingBox.render();
                }
            }
            

        }

        /// <summary>
        /// Método que se llama cuando termina la ejecución del ejemplo.
        /// Hacer dispose() de todos los objetos creados.
        /// </summary>
        public override void close()
        {
            terrain.dispose();
            foreach (TgcMesh obstaculo in obstaculos)
            {
                obstaculo.dispose();
            }
        }
    }
}
