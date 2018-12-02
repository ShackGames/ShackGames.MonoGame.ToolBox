using Microsoft.Xna.Framework.Graphics;

namespace ShackGames.MonoGame.Toolbox.Models
{
    public class GameConfiguration
    {
        public string ContentRootDirectory { get; set; } = "Content";
        public int PreferredBackBufferWidth { get; set; }
        public int PreferredBackBufferHeight { get; set; }

        public static GameConfiguration Default()
        {
            return new GameConfiguration
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 600
            };
        }
    }
}
