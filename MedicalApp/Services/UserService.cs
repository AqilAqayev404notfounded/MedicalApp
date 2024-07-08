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
            throw new NotFoundException("User not found");
    } 
    public void AddUser(User user)
    {
        Array.Resize(ref DB.Users, DB.Users.Length+1);
        DB.Users[DB.Users.Length-1] = user;
    }

    public void RemoveUser(string email, string password)
    {
        for (int i = 0; i < DB.Users.Length; i++)
        {
            if (DB.Users[i].Email == email && DB.Users[i].Password == password)
            {
                for (int j = i; j < DB.Users.Length - 1; j++)
                {
                    DB.Users[j] = DB.Users[j + 1];
                }
                Array.Resize(ref DB.Users, DB.Users.Length - 1);
                Console.WriteLine("User deleted successfully!");
                return;
            }
        }
        throw new NotFoundException("User not found or incorrect password");
    } public void AdminRemoveUser(string email)
    {
        for (int i = 0; i < DB.Users.Length; i++)
        {
            if (DB.Users[i].Email == email)
            {
                for (int j = i; j < DB.Users.Length - 1; j++)
                {
                    DB.Users[j] = DB.Users[j + 1];
                }
                Array.Resize(ref DB.Users, DB.Users.Length - 1);
                Console.WriteLine("User deleted successfully!");
                return;
            }
        }
        throw new NotFoundException("User not found or incorrect password");
    }
    public bool NoSpace(string a) 
    {
        return string.IsNullOrWhiteSpace(a);

    }

}
