﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.Sprite;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GPFinal
{
    class Shot : DrawableSprite
    {

        public Shot(Game game): base(game)
        {

#if DEBUG
            this.ShowMarkers = true;
#endif
        }

        protected override void LoadContent()
        {
            this.spriteTexture = this.Game.Content.Load<Texture2D>("shot");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            this.Location += this.Direction * (this.Speed * gameTime.ElapsedGameTime.Milliseconds / 1000);

            if (this.IsOffScreen())
            {
                this.Enabled = false;
            }

            base.Update(gameTime);
        }
    }
}
