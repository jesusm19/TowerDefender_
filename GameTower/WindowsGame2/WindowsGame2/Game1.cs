using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame2
{
                        //{    0,        1,      2,      3  }   
    public enum Direccion {IZQUIERDA, DERECHA, ARRIBA, ABAJO};
    // Esta estructura contiene cada una de las rutas de un Mundo
    public struct Ruta
    {
        public Direccion direccion;
        public int  X;
        public int  Y;        
    }; 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D lunch;
        Ruta[] ruta;
        Alien alien1,alien2;
        World world1;
        SpriteFont font;        
        public static int NO_DESAYUNOS = 10;
        public static int dulces = 10;
        Texture2D miDulce;
        // datos torre
        Torre torre;
        Vector2 vectorTorre = new Vector2(720, 0);
        //fin datos torre


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.Window.Title = "Tower Defense - CIMAT";
            // Mostramos el raton
            this.IsMouseVisible = true;

            ruta = Utileria.getRuta("nivel1.txt");           
            // Creamos los aliens, les asignamos una imagen diferente a cada uno
            alien1 = new Alien(new Vector2(10, 126), ruta, "alien1");
            alien2 = new Alien(new Vector2(50, 126), ruta, "alien2");
            // Creamos un mundo
            world1 = new World(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height); 
            //iniciamos el objeto torre, enviamos por parametro la posicion y  graphics
            torre = new Torre(graphics, vectorTorre,dulces);
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("SpriteFont1");
            lunch = Content.Load<Texture2D>("lunch");
            miDulce = Content.Load<Texture2D>("dulces");
            world1.LoadContent(Content);
            alien1.LoadContent(Content);
            alien2.LoadContent(Content);
            torre.cargarTorre(Content);
            dulces = torre.restarDulce();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit. If Escape key was pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            
            // TODO: Add your update logic here
            alien1.Update();
            alien2.Update();
            torre.Update();
            
            base.Update(gameTime);
        }        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            // TODO: Add your drawing code here
            // Vector donde colocaremos el no de vidas que le quedan al jugador
            Vector2 textVector = new Vector2(5, 5);
            spriteBatch.Begin();            
            world1.Draw(spriteBatch);
            alien1.Draw(spriteBatch);
            alien2.Draw(spriteBatch);
            //carca el metodo debujar para crearlo en  la clase principal
            torre.dibujar(spriteBatch);
            spriteBatch.Draw(lunch, new Rectangle(725, 420, 48, 48), Color.White);
            spriteBatch.DrawString(font, "No Almuerzos:"+NO_DESAYUNOS, textVector, Color.Red);
            spriteBatch.Draw(miDulce, new Rectangle(380, 5, miDulce.Width, miDulce.Height), Color.White);
            spriteBatch.DrawString(font, " " + dulces, new Vector2(400,5), Color.Blue);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
        
    }
}