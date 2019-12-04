using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using ShotHandler;

namespace Player
{
    public class PlayerShoot : PlayerShip
    {
        public ShotManager SM;

        public PlayerShoot(Game game) : base(game)
        {
            SM = (ShotManager)game.Services.GetService(typeof(IShotHandler));
        }

        public override void Update(GameTime gameTime)
        {
            if (input.KeyboardState.HasReleasedKey(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                Shot s = new Shot(this.Game);
                s.Location = this.Location;
                s.Speed = 600;
                s.Direction = ((this.Direction / 5) + new Vector2(0, -1));
                SM.Shoot(s);
            }
            if (input.KeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.B))
            {
                Shot s = new Shot(this.Game);
                s.Location = this.Location;
                s.Speed = 600;
                s.Direction = (this.Direction / 5 + new Vector2(0, -1));
                SM.Shoot(s);
            }

            base.Update(gameTime);
        }

        public void ResetPlayerShoot()
        {
            SetInitialLocation();
        }
    }
}
