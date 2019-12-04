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
        public List<Ghost.MonogameGhost> Ghosts { get; private set; }
        List<Ghost.MonogameGhost> ghostsToRemove;
        

        Ghost.MonogameGhost ghost;

        public GhostSpawner(Game game) : base(game)
        {
            Ghosts = new List<Ghost.MonogameGhost>();
            ghostsToRemove = new List<Ghost.MonogameGhost>();

            this.ghost = new Ghost.MonogameGhost(game);
            this.SetSpawnKey(Microsoft.Xna.Framework.Input.Keys.T);
        }

        public override GameComponent Spawn()
        {
            ghost = new Ghost.MonogameGhost(this.Game);
            ghost.Initialize();
            Ghosts.Add(ghost);
            ghost.Location = this.GetRandLocation(ghost.spriteTexture);
            ghost.Direction = new Vector2(0, 1);
            this.instance = ghost;
            return base.Spawn();
        }

        public override void Update(GameTime gameTime)
        {
            ghostsToRemove.Clear(); //clear old shots to be removed

            //Update each shot in the Shots Collection
            foreach (var g in Ghosts)
            {
                if (g.Enabled)
                {

                }
                else //If the shot is not enabled 
                {
                    ghostsToRemove.Add(g);
                }
            }

            //Remove shots that are not enalbled anymore
            foreach (Ghost.MonogameGhost g in ghostsToRemove)
            {
                this.Ghosts.Remove(g);
            }
            base.Update(gameTime);
        }

        public void ResetGhostSpawner()
        {
            foreach(Ghost.MonogameGhost g in Ghosts)
            {
                g.Visible = false;
                g.Enabled = false;
            }
        }
    }
}
