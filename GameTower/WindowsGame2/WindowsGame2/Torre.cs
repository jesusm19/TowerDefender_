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
    class Torre
    {
        public Color color { get; set; }
        protected GraphicsDeviceManager misGraficos;
        protected ContentManager micontenido;
        public Boolean vida = false; // saber si se mueve la imagen
        public Vector2 velocidad = Vector2.Zero;
        public Vector2 ventanaTamaño = Vector2.Zero;
        protected float escala = 1.0f;
        protected SpriteEffects efectos = SpriteEffects.None;
        protected SpriteBatch batch;
        public Texture2D imagen { get; set; }
        public Vector2 posicion;
        public float Escala { get; set; }
        public Vector2 origen { get; set; }

        int mouseX, mouseY;
        Boolean presionado = false;
        Boolean click, clickAnterior = false;
        Vector2 clickTorre = Vector2.Zero;

        public bool hayDulces = false;
        public bool resta = false;
        public Torre(GraphicsDeviceManager graficos, Vector2 posicion,int dulces)
        {
            this.misGraficos = graficos;
            this.posicion = posicion;
            batch = new SpriteBatch(misGraficos.GraphicsDevice);
            color = Color.White;
            if (((double)dulces / 5) >= 1)
            {
                hayDulces = true;
            }
        }
        public void cargarTorre(ContentManager contenido)
        {
            this.micontenido = contenido;
            this.imagen = micontenido.Load<Texture2D>("Torre");
        }
        /*public void mover()
        {
            posicion.X += velocidad.X;
            posicion.Y += velocidad.Y;
        }
        public void setPosicion(float x, float y)
        {
            posicion.X = x;
            posicion.Y = y;
        }*/
        public void dibujar(SpriteBatch sprite)
        {
            sprite.Draw(imagen, posicion, null, color, 0.0f, origen, new Vector2(escala, escala), efectos, 0.0f);
            if (posicion.X != 720 || posicion.Y != 0) { resta = true; }
        }
        public void Update()
        {
            //movimiento de la torre
            MouseState mouse = Mouse.GetState();
            mouseX = mouse.X;
            mouseY = mouse.Y;
            clickAnterior = click;
            click = mouse.LeftButton == ButtonState.Pressed;
            if (hayDulces)
            {
                restarDulce();
                if (clickItem(new Rectangle((int)posicion.X, (int)posicion.Y,
                                    imagen.Width, imagen.Height), mouseX, mouseY))
                {
                    if (click)
                    {
                        presionado = true;
                        clickTorre = new Vector2(mouseX - posicion.X, mouseY - posicion.Y);
                    }
                    else
                    {
                        presionado = false;
                    }
                }
                if (presionado)
                {
                    color = new Color(255, 255, 255, 100);
                    posicion = new Vector2(mouseX - clickTorre.X, mouseY - clickTorre.Y);
                   // resta = true;
                }
                else
                {
                    color = Color.White;
                }
            }
        }
        public Boolean clickItem(Rectangle rec, int x, int y)
        {
            return (x >= rec.X &&
                        x <= rec.X + rec.Width &&
                        y >= rec.Y &&
                        y <= rec.Y + rec.Height);
        }
        public int restarDulce()
        {
            Vector2 posicionInicial = Vector2.Zero;
            if (resta)
            {
                return Game1.dulces -= 5;               
            }
            else 
            { 
                return Game1.dulces; 
            }
        }
    }
}
