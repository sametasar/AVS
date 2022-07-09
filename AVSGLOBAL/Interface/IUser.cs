using AVSGLOBAL.Class.Models;

namespace AVSGLOBAL.Interface
{
    public interface IUser
    {
        Task<Mdl_User> GetUser(Mdl_User userModel);
    }
}