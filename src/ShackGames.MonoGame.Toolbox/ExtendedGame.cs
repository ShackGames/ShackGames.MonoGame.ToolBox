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

        public new ExtendedContentManager Content { get; }

        public ExtendedGame() : this(GameConfiguration.Default())
        {

        }

        public ExtendedGame(GameConfiguration configuration)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = configuration.PreferredBackBufferWidth;
            graphics.PreferredBackBufferHeight = configuration.PreferredBackBufferHeight;
            graphics.ApplyChanges();

            Content = new ExtendedContentManager(this, configuration.ContentRootDirectory);            
        }
    }
}
