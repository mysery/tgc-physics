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
using AlumnoEjemplos.Piguyis.Colisiones;
using AlumnoEjemplos.Piguyis.Body;
using AlumnoEjemplos.PiguYis.Matematica;
using AlumnoEjemplos.Piguyis.Fisica;

namespace AlumnoEjemplos.Piguyis
{
    /// <summary>
    /// Ejemplo del alumno
    /// </summary>
    public class EjemploAlumnoCollision : TgcExample
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
            return "Grupo Piguyis - Collision test";
        }

        /// <summary>
        /// Completar con la descripción del TP
        /// </summary>
        public override string getDescription()
        {
            return "Test de particulas (pequeños circulos cayendo sobre superficies)";
        }


        TgcBox piso;
        List<TgcMesh> obstaculos = new List<TgcMesh>();

        /// <summary>
        /// Método que se llama una sola vez,  al principio cuando se ejecuta el ejemplo.
        /// Escribir aquí todo el código de inicialización: cargar modelos, texturas, modifiers, uservars, etc.
        /// Borrar todo lo que no haga falta
        /// </summary>
        public override void init()
        {
            //GuiController.Instance: acceso principal a todas las herramientas del Framework

            //Device de DirectX para crear primitivas
            Device d3dDevice = GuiController.Instance.D3dDevice;

            //Carpeta de archivos Media del alumno
            //string alumnoMediaFolder = GuiController.Instance.AlumnoEjemplosMediaDir;

            ///////////////USER VARS//////////////////

            //Crear una UserVar
            /*GuiController.Instance.UserVars.addVar("variablePrueba1");
            GuiController.Instance.UserVars.addVar("variablePrueba2");
            GuiController.Instance.UserVars.addVar("variablePrueba3");
            GuiController.Instance.UserVars.addVar("variablePrueba4");
            GuiController.Instance.UserVars.addVar("variablePrueba5");
            GuiController.Instance.UserVars.addVar("variablePrueba6");
            */

            //Cargar valor en UserVar
            //GuiController.Instance.UserVars.setValue("variablePrueba", 5451);



            ///////////////MODIFIERS//////////////////
            //Modifier para habilitar o no el renderizado del BoundingBox del personaje
            GuiController.Instance.Modifiers.addBoolean("showBoundingBox", "Bouding Box", false);

            //Crear un modifier para un valor FLOAT
            //GuiController.Instance.Modifiers.addFloat("valorFloat", -50f, 50f, 0f);
            
            //Crear un modifier para un ComboBox con opciones
            //string[] opciones = new string[]{"opcion1", "opcion2", "opcion3"};
            //GuiController.Instance.Modifiers.addInterval("valorIntervalo", opciones, 0);

            //Crear un modifier para modificar un vértice
            //GuiController.Instance.Modifiers.addVertex3f("valorVertice", new Vector3(-100, -100, -100), new Vector3(50, 50, 50), new Vector3(0, 0, 0));
            
            ///////////////CONFIGURAR CAMARA PRIMERA PERSONA//////////////////
            //Camara en primera persona, tipo videojuego FPS
            //Solo puede haber una camara habilitada a la vez. Al habilitar la camara FPS se deshabilita la camara rotacional
            GuiController.Instance.FpsCamera.Enable = true;
            GuiController.Instance.FpsCamera.MovementSpeed = 500f;
            //GuiController.Instance.FpsCamera.JumpSpeed = 500f;
            GuiController.Instance.FpsCamera.setCamera(new Vector3(75, 75, 250), new Vector3(50, -200, -600));

            //Loggear por consola del Framework
            //GuiController.Instance.Logger.log(elemento);
       }

        private void generateScene()
        {
            //Device de DirectX para crear primitivas
            Device d3dDevice = GuiController.Instance.D3dDevice;

            //Piso.
            TgcTexture pisoTexture = TgcTexture.createTexture(d3dDevice, GuiController.Instance.ExamplesMediaDir + "Texturas\\pasto.jpg");
            piso = TgcBox.fromSize(new Vector3(1000, 1, 1000), pisoTexture);

            //Cargar obstaculos y posicionarlos
            TgcSceneLoader loader = new TgcSceneLoader();
            TgcScene scene;
            TgcMesh obstaculo;

            //Obstaculo 1: Malla estatática de Box de formato TGC
            scene = loader.loadSceneFromFile(
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Box\\" + "Box-TgcScene.xml",
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Box\\");
            //Escalarlo, posicionarlo y agregar a array de obstáculos
            obstaculo = scene.Meshes[0];
            obstaculo.Scale = new Vector3(1, 2, 3);
            obstaculo.move(75, 26, 0);
            obstaculos.Add(obstaculo);

            //Obstaculo 2: Malla estatática de Box de formato TGC
            scene = loader.loadSceneFromFile(
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Box\\" + "Box-TgcScene.xml",
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Box\\");
            //Escalarlo, posicionarlo y agregar a array de obstáculos
            obstaculo = scene.Meshes[0];
            obstaculo.Scale = new Vector3(3, 2, 1);
            obstaculo.move(0, 26, 75);
            //Le cambiamos la textura a este modelo particular
            obstaculo.changeDiffuseMaps(new TgcTexture[] { TgcTexture.createTexture(GuiController.Instance.D3dDevice, GuiController.Instance.ExamplesMediaDir + "Texturas\\madera.jpg") });
            obstaculos.Add(obstaculo);

            //Obstaculo 3: Malla estatática de Box de formato TGC
            scene = loader.loadSceneFromFile(
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Box\\" + "Box-TgcScene.xml",
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Box\\");
            //Escalarlo, posicionarlo y agregar a array de obstáculos
            obstaculo = scene.Meshes[0];
            obstaculo.Scale = new Vector3(1, 2, 1);
            obstaculo.move(75, 26, 75);
            obstaculos.Add(obstaculo);

            //Obstaculo 4: Malla estatática de Roca de formato TGC
            scene = loader.loadSceneFromFile(
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Roca\\" + "Roca-TgcScene.xml",
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\Roca\\");
            //Escalarlo, posicionarlo y agregar a array de obstáculos
            obstaculo = scene.Meshes[0];
            obstaculo.Scale = new Vector3(3, 3, 3);
            obstaculo.move(75, 21, 110);
            obstaculos.Add(obstaculo);

            //Obstaculo 5: Malla estatática de CopaMadera de formato TGC
            scene = loader.loadSceneFromFile(
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\CopaMadera\\" + "CopaMadera-TgcScene.xml",
                GuiController.Instance.ExamplesMediaDir + "ModelosTgc\\CopaMadera\\");
            //Escalarlo, posicionarlo y agregar a array de obstáculos
            obstaculo = scene.Meshes[0];
            obstaculo.Scale = new Vector3(0.5f, 0.5f, 0.5f);
            obstaculo.move(150, 1, 75);
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

            //float moveY = (float)GuiController.Instance.Modifiers.getValue("valorFloat");

            ///////////////INPUT//////////////////
            //conviene deshabilitar ambas camaras para que no haya interferencia
            
            //////////////RENDERS/////////////////
            //Renderizar piso
            piso.render();
            //Renderizar obstaculos           
            foreach (TgcMesh obstaculo in obstaculos)
            {
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
            piso.dispose();
            foreach (TgcMesh obstaculo in obstaculos)
            {
                obstaculo.dispose();
            }            
        }
    }
}
