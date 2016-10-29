namespace Assets.Infrastructure.CoreTools.Instanced.JsonFormatter
{
    public interface IJsonFormatter
    {
        string ToJsonString<T>(T data);
        T FromString<T>(string data);
    }
}