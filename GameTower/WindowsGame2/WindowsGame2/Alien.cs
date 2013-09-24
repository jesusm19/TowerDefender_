using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input; 

namespace WindowsGame2
{    
    class Alien
    {        
        // Variable que contendra la imagen del alien 
        Texture2D alienTexture;
        // Variable que servira para indicar la posicion donde se dibujara el alien
        Rectangle rectangle;
        // Esta variable va a guardar la posicion actual del alien
        Vector2 posicionActual;
        // Esta variable va a guardar la posicion inicial del alien
        Vector2 posicionInicio;
        // Variable que sirve para indicar el numero de pixeles que ira avanzando el alien (Velocidad)
        int velocidad = 3;        
        // Esta variable tiene la ruta que seguira el alien
        Ruta[] ruta;
        // Variable que tiene la posicion de la ruta
        int posRuta;        
        //Nombre de la imagen para este alien
        String nombreImagenAlien;

        private ContentManager content;

        public Alien(Vector2 posicionInicial, Ruta[] ruta, String nombreImagenAlien)
        {
            // Cuando se crea el objeto, establecemos la posicion inicial del alien.
            this.posicionActual = posicionInicial;
            this.posicionInicio = posicionInicial;
            // Creamos el rectangulo que contendra el alien, en la posicion inicial
            this.rectangle = new Rectangle((int) posicionInicial.X, (int) posicionInicial.Y, 48, 48);
            this.ruta = ruta;
            this.posRuta = 0;
            this.nombreImagenAlien = nombreImagenAlien;
        }
        
        public void LoadContent(ContentManager content)
        {
            this.content = content;
            alienTexture = content.Load<Texture2D>(this.nombreImagenAlien);
        }
        public void Update()
        {
            // Ya termino su recorrido completo el alien. Desde Inicio hasta el Fin
            if (posRuta == ruta.Length)
            {
                posicionActual.X = posicionInicio.X;
                posicionActual.Y = posicionInicio.Y;
                // Llego a donde esta los desayunos y se robo uno. Es como perder una vida.
                Game1.NO_DESAYUNOS--;
                posRuta = 0;
            }
            else
            {
                this.mover();
            }
        }

        public void mover() 
        {         
            switch (ruta[posRuta].direccion){
                case Direccion.IZQUIERDA:
                    posicionActual.X -= velocidad;
                    if (posicionActual.X <= ruta[posRuta].X) // LLegamos al tope de esta ruta                
                        posRuta++; // Cambiamos a la siguiente ruta.               
                    break;
                case Direccion.DERECHA:            
                    posicionActual.X += velocidad;
                    if (posicionActual.X >= ruta[posRuta].X) // LLegamos al tope de esta ruta                
                        posRuta++; // Cambiamos a la siguiente ruta.               
                    break;
                case  Direccion.ABAJO:
                    posicionActual.Y += velocidad;
                    if (posicionActual.Y >= ruta[posRuta].Y) // LLegamos al tope de esta ruta                
                        posRuta++; // Cambiamos a la siguiente ruta.                
                    break;
                case  Direccion.ARRIBA:
                    posicionActual.Y -= velocidad ;
                    if (posicionActual.Y <= ruta[posRuta].Y) // LLegamos al tope de esta ruta                
                       posRuta++; // Cambiamos a la siguiente ruta.                
                    break;
            }               
        }

        public void Draw(SpriteBatch sprite) {            
            sprite.Draw(alienTexture, new Rectangle((int) posicionActual.X, (int) posicionActual.Y, 48, 48), Color.White);
        }
    }
}
