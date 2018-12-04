namespace ShackGames.MonoGame.Toolbox.Providers
{
    public interface IJsonSerializationProvider
    {
        string Serialize<T>(T obj);
        T Deserialize<T>(string json) where T : class;
    }
}
