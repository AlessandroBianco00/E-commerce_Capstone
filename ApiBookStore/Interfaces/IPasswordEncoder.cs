namespace ApiBookStore.Interfaces
{
    public interface IPasswordEncoder
    {
        string Encode(string password);
        bool IsSame(string plainText, string codedText);
    }
}
