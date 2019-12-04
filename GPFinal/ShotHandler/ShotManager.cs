using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameLibrary.GameComponents;
using Microsoft.Xna.Framework;

namespace ShotHandler
{
    public interface IShotHandler
    {
        List<Shot> Shots { get; }
        List<Shot> EnemyShots { get; }

        Shot Shoot();
        Shot Shoot(Shot s);
        Shot Shoot(Vector2 direction, float speed);
        Shot EnemyShoot(Shot shot);

    }

    public class ShotManager : DrawableGameComponent, IShotHandler
    {
        public List<Shot> Shots { get; private set; }
        public List<Shot> EnemyShots { get; private set; }

        List<Shot> shotsToRemove;
        List<Shot> enemyShotsToRemove;

        Shot s;

        public ShotManager(Game game) : base(game)
        {
            if (game.Services.GetService<IShotHandler>() == null)
            {
                game.Services.AddService(typeof(IShotHandler), this);
            }

            Shots = new List<Shot>();
            EnemyShots = new List<Shot>();

            shotsToRemove = new List<Shot>();
            enemyShotsToRemove = new List<Shot>();
        }

        public override void Update(GameTime gameTime)
        {
            shotsToRemove.Clear(); //clear old shots to be removed
            enemyShotsToRemove.Clear();

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

            foreach (var s in EnemyShots)
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

            //Remove shots that are not enalbled anymore
            foreach (Shot s in shotsToRemove)
            {
                this.removeShot(s);
            }

            foreach (Shot s in enemyShotsToRemove)
            {
                this.enemyRemoveShot(s);
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

        public virtual Shot EnemyShoot(Shot shot)
        {
            s = shot;
            //if (!string.isnullorempty(this.shottexture))
            //{
            //    s.shottexture = this.shottexture;
            //}

            s.Initialize();
            this.addEnemyShot(s);
            return s;
        }

        #endregion
        protected virtual void addShot(Shot s)
        {
            this.Shots.Add(s);
        }

        protected virtual void addEnemyShot(Shot s)
        {
            this.EnemyShots.Add(s);
        }

        protected virtual void removeShot(Shot s)
        {
            this.Shots.Remove(s);
        }

        protected virtual void enemyRemoveShot(Shot s)
        {
            this.EnemyShots.Remove(s);
        }

        public void ResetShotManager()
        {
            foreach (Shot s in Shots)
            {
                s.Visible = false;
                s.Enabled = false;
                shotsToRemove.Add(s);
            }

            foreach (Shot es in EnemyShots)
            {
                es.Visible = false;
                es.Enabled = false;
                enemyShotsToRemove.Add(es);
            }

            //Remove shots that are not enalbled anymore
            foreach (Shot s in shotsToRemove)
            {
                this.removeShot(s);
            }

            foreach (Shot s in enemyShotsToRemove)
            {
                this.enemyRemoveShot(s);
            }

            shotsToRemove.Clear(); //clear old shots to be removed
            enemyShotsToRemove.Clear();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (var shot in this.Shots)
            {
                if (shot.Visible)   //respect block visible property
                    shot.Draw(gameTime);
            }
            foreach (var shot in this.EnemyShots)
            {
                if (shot.Visible)   //respect block visible property
                    shot.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
