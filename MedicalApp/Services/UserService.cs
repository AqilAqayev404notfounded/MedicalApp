using MedicalApp.Exceptions;
using MedicalApp.Models;

namespace MedicalApp.Services;

public class UserService
{

    public User Login(string email, string password)
    {
        foreach(var user in DB.Users)
        {
            if(user.Password == password && user.Email==email)
            {
               return user;
            }

        }
            throw new NotFoundException("User yoxdu");
    } 
    public void AddUser(User user)
    {
        Array.Resize(ref DB.Users, DB.Users.Length+1);
        DB.Users[DB.Users.Length-1] = user;
    }
   

}
