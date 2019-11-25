using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spawner
{
    public class GhostSpawner : TimedSpawner
    {

        Ghost.MonogameGhost ghost;

        public GhostSpawner(Game game) : base(game) {
            this.ghost = new Ghost.MonogameGhost(game);
            this.SetSpawnKey(Microsoft.Xna.Framework.Input.Keys.T);
        }

        public override GameComponent Spawn()
        {
            ghost = new Ghost.MonogameGhost(this.Game);
            ghost.Initialize();
            ghost.Location = this.GetRandLocation(ghost.spriteTexture);
            ghost.Direction = new Vector2(0, 1);
            this.instance = ghost;
            return base.Spawn();
        }

    }
}
