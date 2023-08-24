namespace Domin.Repository
{
    public interface ICypherServices
    {
        string EncryptText(string text, string key);
        string DecryptText(string text, string key);

    }
}
