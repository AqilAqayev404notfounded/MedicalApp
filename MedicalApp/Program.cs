using MedicalApp.Exceptions;
using MedicalApp.Models;
using MedicalApp.Services;
using System.Text.RegularExpressions;

UserService userService = new UserService();
CategoryService categoryService = new CategoryService();
MedicineService medicineService = new MedicineService();
User UserLogin = new User();
User loggedInUser=null;

while (loggedInUser==null)
{
    SoundPlayer player = new SoundPlayer("welcome.wav");
    player.PlaySync();
restart:
    Console.WriteLine("-------Welcome Aqil's Hospital ....( '-' )------------");
    Console.WriteLine("======================================================");
    Console.WriteLine("[1]-User Registration");
    Console.WriteLine("[2]-User login");
    Console.WriteLine("[3]-Admin Panel");
    Console.WriteLine("[0]-Exit");

    string initialSelect = Console.ReadLine();
    Console.Clear();

    switch (initialSelect)
    {
        case "0":
            return;
        case "1":
            Console.WriteLine("Please enter new name:");
            string name = Console.ReadLine();
            Regex nameRegex = new Regex(@"^[A-zA-Z]+$");
            while (!nameRegex.IsMatch(name))
            {
                Console.WriteLine("Please enter correct new name!");
                Console.WriteLine("Please enter new name:");
                name = Console.ReadLine();
            }



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

            foreach (var item in DB.Users)
            {
                if (item.Email == email)
                {
                    Console.Write("User already Registration");
                    goto restart;
                }

            }


            User user = new User { Email = email, Password = password, Fullname = name };
            userService.AddUser(user);
            Console.WriteLine("User registered successfully!");
            Console.Clear();

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
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }
            Console.Clear();
            Console.WriteLine($"Welcome, {UserLogin.Fullname}!");
            Console.WriteLine("======================================");

            break;


        case "3":
            Console.WriteLine("Please enter Admin email:");
            string adminEmail = Console.ReadLine();
            Console.WriteLine("Please enter Admin password:");

            string adminPassword = Console.ReadLine();
            if (adminEmail != "Admin@gmail.com" && adminPassword != "Admin1234")
            {
                Console.WriteLine("Please ,wrinte correct Admin email and Admin password");
                continue;
            }


            Console.WriteLine("welcome Admin Panel");
            Console.WriteLine("=======================");
            foreach (var logins in DB.Users)
            {
                Console.WriteLine($"Name : {logins.Fullname} ,Email : {logins.Email} ,Id :{logins.Id} ,Password : {logins.Password}");
            }
            foreach (var medcn in DB.Medicines)
            {
                if (medcn == null)
                {
                    Console.WriteLine("medicine is not added");
                }
                else
                {
                    Console.WriteLine($"Medicine name : {medcn.Name} Medicine price : {medcn.Price}$ Medicine id : {medcn.Id}");
                }

            }
            continue;
        default:
            Console.Clear();
            Console.WriteLine("Please ,select correct command");
            continue;



    }
start:
    Console.WriteLine("[1]-Create a new catagory");
    Console.WriteLine("[2]-Create a new Medicine");
    Console.WriteLine("[3]-List all medicine");
    Console.WriteLine("[4]-Update a medicine");
    Console.WriteLine("[5]-Find medicine by ID ");
    Console.WriteLine("[6]-Find medicine by Name ");
    Console.WriteLine("[7]-Find medicine by catagory ");
    Console.WriteLine("[8]-Wiew Medicine");
    Console.WriteLine("[0]-Exit");
    Console.WriteLine("[10]-Return to previous menu");

    string select = Console.ReadLine();
    Console.Clear();


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
            Console.Clear();

            goto start;
        case "2":
            foreach (var ctgry in DB.Categories)
            {
                Console.WriteLine($" Catogry Name {ctgry.Name}   Catogory Id {ctgry.Id}");
            }
            Console.WriteLine("Please enter medicine name:");
            string medicineName = Console.ReadLine();

            decimal price;
            Console.WriteLine("Please enter medicine price:");
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Invalid input. Please enter a valid price:");
            }

            int categoryId;
            Console.WriteLine("Please enter category ID:");
            while (!int.TryParse(Console.ReadLine(), out categoryId))
            {
                Console.WriteLine("Invalid input. Please enter a valid category ID:");
            }
            Medicine medicine = new Medicine
            {
                Name = medicineName,
                Price = price,
                CategoryId = categoryId,
                UserId = UserLogin.Id

            };
            medicineService.CreateMedicine(medicine);
            Console.WriteLine("Medicine created successfully!");
            Console.Clear();

            goto start;

        case "3":
            medicineService.GetAllMedicines(UserLogin.Id);
            

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
                Console.WriteLine($"ID: {foundMedicine.Id}, Name: {foundMedicine.Name}, Price: {foundMedicine.Price}$, Category ID: {foundMedicine.CategoryId}");
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
                Console.WriteLine($"ID: {foundMedicineByName.Id}, Name: {foundMedicineByName.Name}, Price: {foundMedicineByName.Price}$, Category ID: {foundMedicineByName.CategoryId}");
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }


            goto start;
        case "7":
            foreach (var c in DB.Categories)
            {
                Console.WriteLine($"Category Name :{c.Name} Category Id : {c.Id}");
            }
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
            medicineService.GetAllMedicines(UserLogin.Id);


            goto start;
        case "10":


            break;


        default:
            Console.WriteLine("Please ,select correct command");
            Console.Clear();

            goto start;
    }
}