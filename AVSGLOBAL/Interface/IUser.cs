using AVSGLOBAL.Class.Models;

namespace AVSGLOBAL.Interface
{
    public interface IUser
    {
        Task<Mdl_User> GetUser(string UserID);

        Task<Mdl_User> Authenticate(Mdl_User userModel);
    }
}