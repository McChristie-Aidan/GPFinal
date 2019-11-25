using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.GameComponents;
using Microsoft.Xna.Framework;

namespace GPFinal
{
    class ShotManager : DrawableGameComponent
    {
        public List<Shot> Shots { get; private set; }

        List<Shot> shotsToRemove;

        Shot s;

        public ShotManager(Game1 game) : base(game)
        {
            Shots = new List<Shot>();

            shotsToRemove = new List<Shot>();

        }

        public override void Update(GameTime gameTime)
        {
            shotsToRemove.Clear(); //clear old shots to be removed

            //Update each shot in the Shots Collection
            foreach (var s in Shots)
            {
                if (s.Enabled)
                {
                    s.Update(gameTime); //Only update enabled shots
                }
                else //If the shot is not enabled 
                {
                    shotsToRemove.Add(s);
                }
            }

           

            foreach (Shot s in Shots)
            {

            }

            //Remove shots that are not enalbled anymore
            foreach (Shot s in shotsToRemove)
            {
                this.removeShot(s);
            }

            base.Update(gameTime);
        }

        #region Shoot
        public virtual Shot Shoot()
        {
            return Shoot(new Shot(this.Game));
        }

        public virtual Shot Shoot(Vector2 direction, float speed)
        {
            s.Direction = direction;
            s.Speed = speed;
            return this.Shoot(s);
        }

        public virtual Shot Shoot(Shot shot)
        {
            s = shot;
            //if (!string.isnullorempty(this.shottexture))
            //{
            //    s.shottexture = this.shottexture;
            //}

            s.Initialize();
            this.addShot(s);
            return s;
        }

        #endregion
        protected virtual void addShot(Shot s)
        {
            this.Shots.Add(s);
        }

        protected virtual void removeShot(Shot s)
        {
            this.Shots.Remove(s);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var shot in this.Shots)
            {
                if (shot.Visible)   //respect block visible property
                    shot.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
