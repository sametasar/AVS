using JWT.Class.Models.GlobalModels;

namespace JWT.Interface
{
    /// <summary>
    /// Middleware da authentication işlemlerini yönetecek olan classın implement edeceği interface yapısı.
    /// </summary>
    public interface IJWT
    {
        Mdl_User Authhenticate(string UserName, string Password,ConnectionInfo Connection);
    }
}
