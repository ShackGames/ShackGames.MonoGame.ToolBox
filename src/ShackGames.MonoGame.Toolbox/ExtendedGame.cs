using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using ShackGames.MonoGame.Toolbox.Content;
using ShackGames.MonoGame.Toolbox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShackGames.MonoGame.Toolbox
{
    public abstract class ExtendedGame : Game
    {
        protected GraphicsDeviceManager graphics;

        public GameConfiguration GameConfiguration { get; set; } = GameConfiguration.Default();
        public new ExtendedContentManager Content { get; }
        public ExtendedContentManager ContentLoader { get { return Content; } }

        public ExtendedGame()
        {
            graphics = new GraphicsDeviceManager(this);

            Content = new ExtendedContentManager(this, GameConfiguration.ContentRootDirectory);
        }

        public ExtendedGame(GameConfiguration configuration)
        {
            GameConfiguration = configuration;

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameConfiguration.PreferredBackBufferWidth;
            graphics.PreferredBackBufferHeight = GameConfiguration.PreferredBackBufferHeight;
            graphics.ApplyChanges();

            Content = new ExtendedContentManager(this, GameConfiguration.ContentRootDirectory);            
        }
    }
}
