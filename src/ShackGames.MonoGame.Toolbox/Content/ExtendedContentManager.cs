using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShackGames.MonoGame.Toolbox.Content
{
    public sealed class ExtendedContentManager : ContentManager
    {
        private readonly ExtendedGame _game;
        private readonly IFileSystem _fileSystem = FileSystem.Current;

        public GraphicsDevice GraphicsDevice { get { return _game.GraphicsDevice; } }

        public ExtendedContentManager(ExtendedGame game, string rootDirectory) : base(new ExtendedServiceProvider(game.GraphicsDevice), rootDirectory)
        {
            _game = game;
        }

        public string ReadAllText(string assetName, bool failOnMissing = false)
        {
            var fullAssetName = Path.Combine(RootDirectory, assetName).ToLower();
            var file = _fileSystem.GetFileFromPathAsync(fullAssetName, CancellationToken.None).Result;

            if (file == null && failOnMissing) throw new ArgumentNullException($"Unable to locate asset: {assetName}"); 
            
            return file.ReadAllTextAsync()
                .GetAwaiter()
                .GetResult();
        }

        public T DeserializeJson<T>(string assetName, bool failOnMissing = false) where T : class
        {
            var json = ReadAllText(assetName, failOnMissing);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return serializer.ReadObject(stream) as T;
            }
        }

        public new T Load<T>(string assetName) where T: class
        {
            if (typeof(T) == typeof(string))
                return ReadAllText(assetName) as T;

            //string originalAssetName = assetName;
            //object result = null;

            //// Check for windows-style directory separator character
            ////Lowercase assetName (monodroid specification all assests are lowercase)
            //assetName = Path.Combine(RootDirectory, assetName.Replace('\\', Path.DirectorySeparatorChar)).ToLower();

            //// Get the real file name
            //if ((typeof(T) == typeof(Texture2D)))
            //{
            //    assetName = Texture2DReader.Normalize(assetName);
            //}
            //else if ((typeof(T) == typeof(SpriteFont)))
            //{
            //    assetName = SpriteFontReader.Normalize(assetName);
            //}
            //else if ((typeof(T) == typeof(Effect)))
            //{
            //    assetName = Effect.Normalize(assetName);
            //}
            //else if ((typeof(T) == typeof(Song)))
            //{
            //    assetName = SongReader.Normalize(assetName);
            //}
            //else if ((typeof(T) == typeof(SoundEffect)))
            //{
            //    assetName = SoundEffectReader.Normalize(assetName);
            //}
            //else if ((typeof(T) == typeof(Video)))
            //{
            //    assetName = Video.Normalize(assetName);
            //}
            //else
            //{
            //    throw new NotSupportedException("Format not supported");
            //}

            //if (string.IsNullOrEmpty(assetName))
            //{
            //    throw new ContentLoadException("Could not load " + originalAssetName + " asset!");
            //}

            //if (Path.GetExtension(assetName).ToUpper() != ".XNB")
            //{
            //    if ((typeof(T) == typeof(Texture2D)))
            //    {
            //        //Basically the same as Texture2D.FromFile but loading from the assets instead of a filePath
            //        Stream assetStream = Game.contextInstance.Assets.Open(assetName);
            //        Bitmap image = BitmapFactory.DecodeStream(assetStream);
            //        ESImage theTexture = new ESImage(image, graphicsDeviceService.GraphicsDevice.PreferedFilter);
            //        result = new Texture2D(theTexture) { Name = Path.GetFileNameWithoutExtension(assetName) };
            //    }
            //    if ((typeof(T) == typeof(SpriteFont)))
            //    {
            //        //result = new SpriteFont(Texture2D.FromFile(graphicsDeviceService.GraphicsDevice,assetName), null, null, null, 0, 0.0f, null, null);
            //        throw new NotImplementedException();
            //    }

            //    if ((typeof(T) == typeof(Song)))
            //        result = new Song(assetName);
            //    if ((typeof(T) == typeof(SoundEffect)))
            //        result = new SoundEffect(assetName);
            //    if ((typeof(T) == typeof(Video)))
            //        result = new Video(assetName);

            //}
            //else
            //{
            //    // Load a XNB file
            //    //Loads from Assets directory + /assetName
            //    Stream assetStream = Game.contextInstance.Assets.Open(assetName);

            //    ContentReader reader = new ContentReader(this, assetStream, this.graphicsDeviceService.GraphicsDevice);
            //    ContentTypeReaderManager typeManager = new ContentTypeReaderManager(reader);
            //    reader.TypeReaders = typeManager.LoadAssetReaders(reader);
            //    foreach (ContentTypeReader r in reader.TypeReaders)
            //    {
            //        r.Initialize(typeManager);
            //    }
            //    // we need to read a byte here for things to work out, not sure why
            //    reader.ReadByte();

            //    // Get the 1-based index of the typereader we should use to start decoding with
            //    int index = reader.ReadByte();
            //    ContentTypeReader contentReader = reader.TypeReaders[index - 1];
            //    result = reader.ReadObject<T>(contentReader);

            //    reader.Close();
            //    assetStream.Close();
            //}

            //if (result == null)
            //{
            //    throw new ContentLoadException("Could not load " + originalAssetName + " asset!");
            //}

            //return (T)result;


            return base.Load<T>(assetName);
        }

        protected override Stream OpenStream(string assetName)
        {
            return base.OpenStream(assetName);
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
