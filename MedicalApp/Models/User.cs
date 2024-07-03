using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace MedicalApp.Models;

public class User : BaseEntity
{

    public string Fullname { get; set; }
    string _email;
    public string Email {  get; set; }
    public string Password { get; set; }
    //public User(string fulname ,string email,string pasword)
    //{
    //   Fullname = fulname;
    //    Email = email;
    //    Password = pasword;

    //}
}
