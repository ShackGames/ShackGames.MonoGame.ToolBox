using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShackGames.MonoGame.Toolbox;
using ShackGames.MonoGame.Toolbox.Models;
using System.Collections.Generic;

namespace Example1
{
    public class MapDef
    {
        public string Name { get; set; }
        public List<string> AssetNames { get; set; }
    }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ExampleGame : GameBase
    {
        SpriteBatch spriteBatch;
        SpriteFont defaultFont;
        string contentStr, otherStr;
        MapDef mapDef;

        public ExampleGame() : base(new GameConfiguration
        {
            PreferredBackBufferWidth = 1080,
            PreferredBackBufferHeight = 720
        })
        {
      
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            defaultFont = Content.Load<SpriteFont>("DefaultFont");
            contentStr = Storage.ReadAllText("Hello.txt");
            mapDef = Storage.DeserializeJson<MapDef>("example-def.json");
            otherStr = Storage.ReadAllText("Other.txt", rootDirectory: "OtherContent");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.DrawString(defaultFont, contentStr, Vector2.Zero, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
