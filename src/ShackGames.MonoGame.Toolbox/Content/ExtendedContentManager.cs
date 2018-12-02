using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShackGames.MonoGame.Toolbox.Content
{
    public sealed class ExtendedContentManager : ContentManager
    {
        public ExtendedContentManager(ExtendedGame game, string rootDirectory) : base(new ExtendedServiceProvider(game.GraphicsDevice), rootDirectory)
        {
            
        }

        class ExtendedServiceProvider : IServiceProvider
        {
            private readonly GraphicsDevice _graphicsDevice;

            public ExtendedServiceProvider(GraphicsDevice graphicsDevice)
            {
                _graphicsDevice = graphicsDevice;
            }

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof(IGraphicsDeviceService))
                    return new ExtendedGraphicsDeviceService(_graphicsDevice);

                throw new NotImplementedException();
            }
        }

        class ExtendedGraphicsDeviceService : IGraphicsDeviceService
        {
            public GraphicsDevice GraphicsDevice { get; }

            public ExtendedGraphicsDeviceService(GraphicsDevice graphicsDevice)
            {
                GraphicsDevice = graphicsDevice;
            }

#pragma warning disable 67
            public event EventHandler<EventArgs> DeviceCreated;
            public event EventHandler<EventArgs> DeviceDisposing;
            public event EventHandler<EventArgs> DeviceReset;
            public event EventHandler<EventArgs> DeviceResetting;
#pragma warning restore 67
        }
    }
}
