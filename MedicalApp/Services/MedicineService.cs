using MedicalApp.Exceptions;
using MedicalApp.Models;
using System.Data;

namespace MedicalApp.Services;

public class MedicineService
{
    public void CreateMedicine(Medicine medicine)
    {
        Array.Resize(ref DB.Medicines, DB.Medicines.Length + 1);
        DB.Medicines[DB.Medicines.Length - 1] = medicine;
    }
    public void GetAllMedicines(int userid)
    {
        foreach (var item in DB.Medicines)
        {
            if(item.UserId == userid)
            {
                Console.WriteLine(item);
            }
        }
    }

    public Medicine GetMedicineById(int id)
    {
        foreach (var item in DB.Medicines)
        {
            if (item.Id == id)
            {
                return item;
            }



        }
        throw new NotFoundException("this id does not exist");

    }
    public Medicine GetMedicineByName(string name)
    {
        foreach(var item in DB.Medicines)
        {
            if(item.Name == name)
            {
                return item;
            }
        }
        throw new NotFoundException("This Name does not exist");
    }
    public void GetMedicineByCategory(int catagoryId)
    {
        foreach (var item in DB.Medicines)
        {
            if (item.CategoryId == catagoryId)
            {
                //return $"CatagoryId = {item.CategoryId}"//;
                Console.WriteLine($"catagory Id :{item.CategoryId} , Name :{item.Name} Price :{item.Price}$ , Date : {item.CreatedDate} ");

            }
        }

    }
    public void RemoveMedicine(int id)
    {
        for (int i = 0; i < DB.Medicines.Length; i++)
        {
            var item = DB.Medicines[i];

            if (item.Id == id)
            {
                for(int j = i; j < DB.Medicines.Length-1; j++)
                {
                    DB.Medicines[j]= DB.Medicines[j+1];

                }
                Array.Resize(ref DB.Medicines,DB.Medicines.Length-1);
                Console.WriteLine("deleted");
            }
        }


    }
    public void UpdateMedicine(int id,Medicine medicine)
    {
        foreach(var item in DB.Medicines)
        {
            item.Name = medicine.Name;
            item.Price = medicine.Price;
            item.CategoryId = medicine.CategoryId;
            item.CreatedDate = medicine.CreatedDate;
            return;

        }
        throw new NotFoundException("This Id does not exist");
    }



}
