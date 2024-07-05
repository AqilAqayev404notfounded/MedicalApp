namespace MedicalApp.Models;

public class Medicine:BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set;}
    public DateTime CreatedDate { get; set; }


    public Medicine()
    {
        CreatedDate= DateTime.Now;
    }








    public override string ToString()
    {
        return $"Id : {Id} - Medicine name : {Name} - Category ID : {CategoryId} - User Id : {UserId} - Date : {CreatedDate} ";
    }



}
