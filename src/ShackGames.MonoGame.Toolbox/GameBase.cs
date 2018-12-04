using Microsoft.Xna.Framework;
using ShackGames.MonoGame.Toolbox.Content;
using ShackGames.MonoGame.Toolbox.Models;

namespace ShackGames.MonoGame.Toolbox
{
    public abstract class GameBase : Game
    {
        protected GraphicsDeviceManager graphics;

        public GameConfiguration GameConfiguration { get; set; } = GameConfiguration.Default();
        public new ExtendedContentManager Content { get; }
        public ExtendedContentManager Storage { get { return Content; } }

        public GameBase()
        {
            graphics = new GraphicsDeviceManager(this);

            Content = new ExtendedContentManager(this, GameConfiguration.ContentRootDirectory);
        }

        public GameBase(GameConfiguration configuration)
        {
            GameConfiguration = configuration;

            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = GameConfiguration.PreferredBackBufferWidth,
                PreferredBackBufferHeight = GameConfiguration.PreferredBackBufferHeight
            };
            graphics.ApplyChanges();

            Content = new ExtendedContentManager(this, GameConfiguration.ContentRootDirectory);            
        }
    }
}
