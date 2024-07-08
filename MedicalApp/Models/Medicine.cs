namespace MedicalApp.Models;

public class Medicine:BaseEntity
{
    private decimal price;
    public string Name { get; set; }
    public decimal Price 
    {
        get => price;
        set
        {
            if (value<0)
            {
                throw new Exception("Price is negative");
            }
            price = value;
        }
    }
    public int CategoryId { get; set; }
    public int UserId { get; set;}
    public DateTime CreatedDate { get; set; }


    public Medicine()
    {
        CreatedDate= DateTime.Now;
    }








    public override string ToString()
    {
        return $"Id : {Id} - Medicine name : {Name} - Price : {Price}$ - Category ID : {CategoryId} - User Id : {UserId} - Date : {CreatedDate} ";
    }



}
