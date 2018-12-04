using Microsoft.Xna.Framework.Graphics;
using ShackGames.MonoGame.Toolbox.Services.Impl;
using System;

namespace ShackGames.MonoGame.Toolbox.Providers.Impl
{
    public class ExtendedServiceProvider : IServiceProvider
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
}
