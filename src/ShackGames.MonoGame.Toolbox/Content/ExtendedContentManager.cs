using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PCLStorage;
using ShackGames.MonoGame.Toolbox.Providers;
using ShackGames.MonoGame.Toolbox.Providers.Impl;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace ShackGames.MonoGame.Toolbox.Content
{
    public sealed class ExtendedContentManager : ContentManager
    {
        private readonly GameBase _game;
        private readonly IFileSystem _fileSystem = FileSystem.Current;
        private IJsonSerializationProvider _jsonProvider;

        public GraphicsDevice GraphicsDevice
        {
            get { return _game.GraphicsDevice; }
        }

        public IJsonSerializationProvider JsonSerializationProvider
        {
            get
            {
                if (_jsonProvider == null) _jsonProvider = new JsonSerializationProviderDefault();
                return _jsonProvider;
            }
            set { _jsonProvider = value; }
        }

        public ExtendedContentManager(GameBase game, string rootDirectory) : base(new ExtendedServiceProvider(game.GraphicsDevice), rootDirectory)
        {
            _game = game;
        }

        public string ReadAllText(string assetName, bool failOnMissing = false, string rootDirectory = null)
        {
            if (rootDirectory == null) rootDirectory = RootDirectory;

            var fullAssetName = Path.Combine(rootDirectory, assetName).ToLower();
            var file = _fileSystem.GetFileFromPathAsync(fullAssetName, CancellationToken.None).Result;

            if (file == null && failOnMissing) throw new ArgumentNullException($"Unable to locate asset: {assetName}"); 
            
            return file.ReadAllTextAsync()
                .GetAwaiter()
                .GetResult();
        }

        public T DeserializeJson<T>(string assetName, bool failOnMissing = false, string rootDirectory = null) where T : class
        {
            var json = ReadAllText(assetName, failOnMissing, rootDirectory);

            return JsonSerializationProvider.Deserialize<T>(json);
        }

        public Stream GetContentStream(string assetName, bool failOnMissing = false, string rootDirectory = null)
        {
            if (rootDirectory == null) rootDirectory = RootDirectory;

            var fullAssetName = Path.Combine(rootDirectory, assetName).ToLower();
            var file = _fileSystem.GetFileFromPathAsync(fullAssetName, CancellationToken.None).Result;

            if (file == null && failOnMissing) throw new ArgumentNullException($"Unable to locate asset: {assetName}");

            return file.OpenAsync(PCLStorage.FileAccess.Read, CancellationToken.None).Result;
        }

        public new T Load<T>(string assetName) where T: class
        {
            if (typeof(T) == typeof(string))
                return ReadAllText(assetName) as T;
            
            return base.Load<T>(assetName);
        }

        protected override Stream OpenStream(string assetName)
        {
            return base.OpenStream(assetName);
        }
    }
}
