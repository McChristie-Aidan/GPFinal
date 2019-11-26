using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GPFinal
{
    class PlayerShoot : PlayerShip
    {
        public ShotManager SM;

        public PlayerShoot(Game1 game) : base(game)
        {
            SM = new ShotManager(game);
            this.Game.Components.Add(SM);
        }

        public override void Update(GameTime gameTime)
        {
            if (input.KeyboardState.HasReleasedKey(Keys.Space))
            {
                Shot s = new Shot(this.Game);
                s.Location = this.Location;
                s.Speed = 600;
                s.Direction = ((this.Direction / 5) + new Vector2(0, -1));
                SM.Shoot(s);
            }
            // maybe put a different kind of shot here
            if (input.KeyboardState.IsKeyDown(Keys.B))
            {
                Shot s = new Shot(this.Game);
                s.Location = this.Location;
                s.Speed = 600;
                s.Direction = ((this.Direction / 5) + new Vector2(0, -1));
                SM.Shoot(s);
            }

            base.Update(gameTime);
        }
    }
}
