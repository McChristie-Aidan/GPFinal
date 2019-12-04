using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;
using Spawner;
using Player;
using ShotHandler;

namespace GPFinal
{
    enum GameState
    {
        Playing, GameOver, Win
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputHandler input;
        PlayerShoot PS;
        GhostSpawner spawner;
        ScoreManager score;
        ShotManager SM;
        
        Shot s;


        GameState gameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            StartGame();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //collision checking. Technical debt
            CheckCollision();

            //Shoot of the ghosts. its here because the shots need access to the players location. Technical debt
            GhostsShoot();

            //Checks for when to reset game.
            CheckForDeath();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        private void CheckForDeath()
        {
            if (ScoreManager.Lives <= 0 || gameState == GameState.GameOver || input.WasKeyPressed(Keys.P))
            {
                PS.ResetPlayerShoot();
                SM.ResetShotManager();
                spawner.ResetGhostSpawner();
                score.ResetScoreManager();
            }
        }

        private void GhostsShoot()
        {
            foreach (Ghost.MonogameGhost g in spawner.Ghosts)
            {
                if (g.hasShot == false && g.Location.Y >= this.GraphicsDevice.Viewport.Height / 7)
                {
                    g.hasShot = true;
                    ShotHandler.Shot s = new ShotHandler.Shot(this);
                    s.Location = g.Location;
                    s.Speed = 250;
                    s.Direction = PS.Location - g.Location;
                    s.Direction.Normalize();
                    SM.EnemyShoot(s);
                }
            }
        }

        public void StartGame()
        {
            gameState = GameState.Playing;

            SM = new ShotManager(this);
            this.Components.Add(SM);

            input = new InputHandler(this);
            this.Components.Add(input);

            spawner = new GhostSpawner(this);
            this.Components.Add(spawner);

            PS = new PlayerShoot(this);
            this.Components.Add(PS);

            score = new ScoreManager(this);
            this.Components.Add(score);
        }

        private void CheckCollision()
        {
            //colision checking. technical debt

            //Ghost collision
            foreach (Ghost.MonogameGhost gc in spawner.Ghosts)
            {
                if (gc.Enabled == true)
                {
                    //enemy on player bullet
                    foreach (ShotHandler.Shot s in SM.Shots)
                    {
                        if (gc.Intersects(s))
                        {
                            if (gc.PerPixelCollision(s))
                            {
                                gc.Visible = false;
                                gc.Enabled = false;
                                ScoreManager.Score += 10;
                                ScoreManager.EnemiesKilled += 1;
                                s.Visible = false;
                                s.Enabled = false;
                            }
                        }
                    }

                    //enemy on player
                    if (gc.Intersects(PS))
                    {
                        if (gc.PerPixelCollision(PS))
                        {
                            ScoreManager.Lives -= 1;
                            gc.Visible = false;
                            gc.Enabled = false;
                        }
                    }
                }
            }

            //enemy bullet collision
            foreach (ShotHandler.Shot shot in SM.EnemyShots)
            {
                if(shot.Enabled == true)
                {
                    //enemy bullet on player bullet
                    foreach (ShotHandler.Shot s in SM.Shots)
                    {
                        if (shot.Intersects(s))
                        {
                            if (shot.PerPixelCollision(s))
                            {
                                shot.Visible = false;
                                shot.Enabled = false;
                                ScoreManager.Score += 5;
                                s.Visible = false;
                                s.Enabled = false;
                            }
                        }
                    }

                    //enemy bullet on player
                    if (shot.Intersects(PS))
                    {
                        if (shot.PerPixelCollision(PS))
                        {
                            ScoreManager.Lives -= 1;
                            shot.Visible = false;
                            shot.Enabled = false;
                        }
                    }
                }
            }
        }
    }
}
