using MedicalApp.Exceptions;
using MedicalApp.Models;
using MedicalApp.Services;
using System.Text.RegularExpressions;

UserService userService = new UserService();
CategoryService categoryService = new CategoryService();
MedicineService medicineService = new MedicineService();
User UserLogin = new User();
User loggedInUser = null;

while (loggedInUser == null)
{
    Console.WriteLine("-------Welcome Aqil's Hospital ....( '-' )------------");
    Console.WriteLine("======================================================");
    Console.WriteLine("[1]-User Registration");
    Console.WriteLine("[2]-User login");
    Console.WriteLine("[0]-Exit");

    string initialSelect = Console.ReadLine();
    Console.Clear();

    switch (initialSelect)
    {
        case "0":
            return;
        case "1":
            Console.WriteLine("Please enter new email:");
            string email = Console.ReadLine();
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            while (!regex.IsMatch(email))
            {
                Console.WriteLine("Olmaz");
                Console.WriteLine("Please enter new email:");
                email = Console.ReadLine();
            }
            Console.WriteLine("Please enter new password:");
            string password = Console.ReadLine();
            Regex regexPasworrd = new Regex(@"^(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$");
            while (!regexPasworrd.IsMatch(password))
            {
                Console.WriteLine("Password : \n1) It must contain at least a number \n2) one upper case letter\n3) 8 characters long");
                Console.WriteLine("Please enter new password:");
                password = Console.ReadLine();
            }

            User user = new User { Email = email, Password = password };
            userService.AddUser(user);
            Console.WriteLine("User registered successfully!");
            continue;



        case "2":
            Console.WriteLine("Please enter email:");
            string loginEmail = Console.ReadLine();
            Regex regexLogin = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            while (!regexLogin.IsMatch(loginEmail))
            {

                Console.WriteLine("You didn't write it correctly");
                Console.WriteLine("Please enter email:");
                loginEmail = Console.ReadLine();

            }
            Console.WriteLine("Please enter password:");
            string loginPassword = Console.ReadLine();
            try
            {
                UserLogin = userService.Login(loginEmail, loginPassword);
                Console.WriteLine($"Welcome, {UserLogin.Email}!");
                Console.WriteLine("======================================");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }
            break;
    }
start:
    Console.WriteLine("[1]-Creat a new catagory");
    Console.WriteLine("[2]-Creat a new Medicine");
    Console.WriteLine("[3]-list all medicine");
    Console.WriteLine("[4]-Uptude a medicine");
    Console.WriteLine("[5]-Find medicine by ID ");
    Console.WriteLine("[6]-Find medicine by Name ");
    Console.WriteLine("[7]-Find medicine by catagory ");
    Console.WriteLine("[8]-Wiew Medicine");
    Console.WriteLine("[0]-Exit");
    Console.WriteLine("[10]-Return to previous menu");

    string select = Console.ReadLine();

    switch (select)
    {
        case "0":
            return;

        case "1":
            Console.WriteLine("Please enter category name:");
            string categoryName = Console.ReadLine();
            Category category = new Category { Name = categoryName };
            categoryService.CreateCategory(category);
            Console.WriteLine("Category created successfully!");
            goto start;
        case "2":
            foreach (var ctgry in DB.Categories)
            {
                Console.WriteLine($" Catogry Name {ctgry.Name}   Catogory Id {ctgry.Id}");
            }
            Console.WriteLine("Please enter medicine name:");
            string medicineName = Console.ReadLine();
            Console.WriteLine("Please enter medicine price:");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Please enter category ID:");
            int categoryId = int.Parse(Console.ReadLine());
            Medicine medicine = new Medicine
            {
                Name = medicineName,
                Price = price,
                CategoryId = categoryId,


            };
            medicineService.CreateMedicine(medicine);
            Console.WriteLine("Medicine created successfully!");
            goto start;

        case "3":
            Medicine[] medicines = medicineService.GetAllMedicines();
            foreach (var med in medicines)
            {
                Console.WriteLine($"ID: {med.Id}, Name: {med.Name}, Price: {med.Price}, Category ID: {med.CategoryId}");
            }
            goto start;
        case "4":
            Console.WriteLine("Please enter the ID of the medicine to update:");
            int updateId = int.Parse(Console.ReadLine());
            try
            {
                Console.WriteLine("Please enter new medicine name:");
                string newName = Console.ReadLine();
                Console.WriteLine("Please enter new medicine price:");
                decimal newPrice = decimal.Parse(Console.ReadLine());
                Console.WriteLine("Please enter new category ID:");
                int newCategoryId = int.Parse(Console.ReadLine());

                Medicine updatedMedicine = new Medicine
                {
                    Name = newName,
                    Price = newPrice,
                    CategoryId = newCategoryId,
                    UserId = UserLogin.Id
                };

                medicineService.UpdateMedicine(updateId, updatedMedicine);
                Console.WriteLine("Medicine updated successfully!");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            goto start;

        case "5":

            Console.WriteLine("Please enter the ID of the medicine:");
           
            int findId = int.Parse(Console.ReadLine());
            try
            {
                Medicine foundMedicine = medicineService.GetMedicineById(findId);
                Console.WriteLine($"ID: {foundMedicine.Id}, Name: {foundMedicine.Name}, Price: {foundMedicine.Price}, Category ID: {foundMedicine.CategoryId}");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            goto start;
        case "6":
            Console.WriteLine("Please enter the name of the medicine:");
            string findName = Console.ReadLine();
            try
            {
                Medicine foundMedicineByName = medicineService.GetMedicineByName(findName);
                Console.WriteLine($"ID: {foundMedicineByName.Id}, Name: {foundMedicineByName.Name}, Price: {foundMedicineByName.Price}, Category ID: {foundMedicineByName.CategoryId}");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            goto start;
        case "7":
            Console.WriteLine("Please enter the category ID:");
            int findCategoryId = int.Parse(Console.ReadLine());
            try
            {
                 medicineService.GetMedicineByCategory(findCategoryId);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            goto start;
        case "8":
            medicines = medicineService.GetAllMedicines();
            foreach (var med in medicines)
            {
                Console.WriteLine($"ID: {med.Id}, Name: {med.Name}, Price: {med.Price}, Category ID: {med.CategoryId}");
            }
            goto start;
        case "10":
            break;


        default:
            Console.WriteLine("Please ,select correct command");
            goto start;
    }
}