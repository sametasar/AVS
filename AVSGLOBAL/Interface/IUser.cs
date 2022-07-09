using AVSGLOBAL.Class.Models;

namespace AVSGLOBAL.Interface
{
    public interface IUser
    {
        Mdl_User GetUser(Mdl_User userModel);
    }
}