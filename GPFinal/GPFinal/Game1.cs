using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;
using Spawner;

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
        
        Shot s;


        GameState gameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            StartGame();
        }

        public void StartGame()
        {          
            gameState = GameState.Playing;

            input = new InputHandler(this);
            this.Components.Add(input);

            spawner = new Spawner.GhostSpawner(this);
            this.Components.Add(spawner);

            PS = new PlayerShoot(this);
            this.Components.Add(PS);

            score = new ScoreManager(this);
            this.Components.Add(score);
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

            //colision checking. technical debt
            foreach (GameComponent gc in Components)
            {
                if (gc is Ghost.MonogameGhost)
                {
                    if (((Ghost.MonogameGhost)gc).Enabled == true)
                    {
                        foreach (Shot s in PS.SM.Shots)
                        {
                            if (((Ghost.MonogameGhost)gc).Intersects(s))
                            {
                                if (((Ghost.MonogameGhost)gc).PerPixelCollision(s))
                                {
                                    gc.Enabled = false;
                                    s.Visible = false;
                                    s.Enabled = false;
                                }
                            }
                        }

                        if (((Ghost.MonogameGhost)gc).Intersects(PS));
                        {
                            if (((Ghost.MonogameGhost)gc).PerPixelCollision(PS))
                            {
                                ScoreManager.Lives -= 1;
                                gc.Enabled = false;
                            }
                        }
                    }
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
