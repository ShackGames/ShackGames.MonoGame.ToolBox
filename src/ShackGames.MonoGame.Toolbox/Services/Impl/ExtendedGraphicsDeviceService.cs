using Microsoft.Xna.Framework.Graphics;
using System;

namespace ShackGames.MonoGame.Toolbox.Services.Impl
{
    public class ExtendedGraphicsDeviceService : IGraphicsDeviceService
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
