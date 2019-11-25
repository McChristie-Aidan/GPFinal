using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.Sprite;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;

namespace GPFinal
{
    public class PlayerShip : DrawableSprite
    {
        PlayerController playerController;
        protected InputHandler input;

        public PlayerShip(Game1 game) : base(game)
        {
            this.Speed = 300;

            playerController = new PlayerController(game);
            input = (InputHandler)game.Services.GetService(typeof(IInputHandler));

        }

        protected override void LoadContent()
        {
            this.spriteTexture = this.Game.Content.Load<Texture2D>("pacmanSingle");
#if DEBUG   //Show markers if we are in debug mode
            this.ShowMarkers = true;
#endif
            SetInitialLocation();
            base.LoadContent();
        }

        public void SetInitialLocation()
        {
            this.Location = new Vector2(this.GraphicsDevice.Viewport.Width / 2 , this.GraphicsDevice.Viewport.Height - this.spriteTexture.Height); //Shouldn't hard code inital position TODO set to be realtive to windows size

        }

        public override void Update(GameTime gameTime)
        {
            //Movement from controller
            playerController.HandleInput(gameTime);

            this.Direction = playerController.Direction;
            this.Location += this.Direction * (this.Speed * gameTime.ElapsedGameTime.Milliseconds / 1000);

            KeepPlayerOnScreen();
            base.Update(gameTime);
        }

        private void KeepPlayerOnScreen()
        {
            this.Location.X = MathHelper.Clamp(this.Location.X, 0, this.Game.GraphicsDevice.Viewport.Width - this.spriteTexture.Width);
        }
    }
}
