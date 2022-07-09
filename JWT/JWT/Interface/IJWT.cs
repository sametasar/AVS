namespace JWT.Interface
{
    /// <summary>
    /// Middleware da authentication işlemlerini yönetecek olan classın implement edeceği interface yapısı.
    /// </summary>
    public interface IJWT
    {
        string Authhenticate(string UserName, string Password,ConnectionInfo Connection);
    }
}
