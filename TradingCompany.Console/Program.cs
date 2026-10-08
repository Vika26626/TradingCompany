using TradingCompany.DAL.DAL;
using TradingCompany.DAL.Models;

const string connectionString =
    "Server=localhost;Database=TradingCompany;Integrated Security=True;TrustServerCertificate=True;";

var roleDal = new RoleDAL(connectionString);
var categoryDal = new CategoryDAL(connectionString);
var userDal = new UserDAL(connectionString);
var productDal = new ProductDAL(connectionString);
var newsDal = new NewsDAL(connectionString);
var newsProductDal = new NewsProductDAL(connectionString);

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Trading Company ===");
    Console.WriteLine("1. Roles");
    Console.WriteLine("2. Categories");
    Console.WriteLine("3. Users");
    Console.WriteLine("4. Products");
    Console.WriteLine("5. News");
    Console.WriteLine("6. NewsProducts");
    Console.WriteLine("0. Exit");
    Console.Write("Choose: ");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            RoleMenu(roleDal);
            break;

        case "2":
            CategoryMenu(categoryDal);
            break;

        case "3":
            UserMenu(userDal);
            break;

        case "4":
            ProductMenu(productDal);
            break;

        case "5":
            NewsMenu(newsDal);
            break;

        case "6":
            NewsProductMenu(newsProductDal);
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Invalid choice.");
            Pause();
            break;
    }
}


void RoleMenu(RoleDAL dal)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Roles ===");
        Console.WriteLine("1. Add");
        Console.WriteLine("2. Get by ID");
        Console.WriteLine("3. Get all");
        Console.WriteLine("4. Update");
        Console.WriteLine("5. Delete");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");

        string? choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.Write("Role name: ");
                    string roleName = Console.ReadLine() ?? "";

                    dal.Add(new Role
                    {
                        RoleName = roleName
                    });

                    Console.WriteLine("Role added.");
                    Pause();
                    break;

                case "2":
                    Console.Write("Role ID: ");
                    int id = int.Parse(Console.ReadLine()!);

                    var role = dal.GetById(id);

                    if (role == null)
                        Console.WriteLine("Role not found.");
                    else
                        Console.WriteLine($"{role.RoleId}: {role.RoleName}");

                    Pause();
                    break;

                case "3":
                    var roles = dal.GetAll();

                    foreach (var item in roles)
                        Console.WriteLine($"{item.RoleId}: {item.RoleName}");

                    Pause();
                    break;

                case "4":
                    Console.Write("Role ID: ");
                    int updateId = int.Parse(Console.ReadLine()!);

                    Console.Write("New role name: ");
                    string newName = Console.ReadLine() ?? "";

                    dal.Update(new Role
                    {
                        RoleId = updateId,
                        RoleName = newName
                    });

                    Console.WriteLine("Role updated.");
                    Pause();
                    break;

                case "5":
                    Console.Write("Role ID: ");
                    int deleteId = int.Parse(Console.ReadLine()!);

                    dal.Delete(deleteId);

                    Console.WriteLine("Role deleted.");
                    Pause();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Pause();
        }
    }
}


void CategoryMenu(CategoryDAL dal)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Categories ===");
        Console.WriteLine("1. Add");
        Console.WriteLine("2. Get by ID");
        Console.WriteLine("3. Get all");
        Console.WriteLine("4. Update");
        Console.WriteLine("5. Delete");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");

        string? choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.Write("Category name: ");
                    string name = Console.ReadLine() ?? "";

                    dal.Add(new Category
                    {
                        CategoryName = name
                    });

                    Console.WriteLine("Category added.");
                    Pause();
                    break;

                case "2":
                    Console.Write("Category ID: ");
                    int id = int.Parse(Console.ReadLine()!);

                    var category = dal.GetById(id);

                    if (category == null)
                        Console.WriteLine("Category not found.");
                    else
                        Console.WriteLine($"{category.CategoryId}: {category.CategoryName}");

                    Pause();
                    break;

                case "3":
                    var categories = dal.GetAll();

                    foreach (var item in categories)
                        Console.WriteLine($"{item.CategoryId}: {item.CategoryName}");

                    Pause();
                    break;

                case "4":
                    Console.Write("Category ID: ");
                    int updateId = int.Parse(Console.ReadLine()!);

                    Console.Write("New category name: ");
                    string newName = Console.ReadLine() ?? "";

                    dal.Update(new Category
                    {
                        CategoryId = updateId,
                        CategoryName = newName
                    });

                    Console.WriteLine("Category updated.");
                    Pause();
                    break;

                case "5":
                    Console.Write("Category ID: ");
                    int deleteId = int.Parse(Console.ReadLine()!);

                    dal.Delete(deleteId);

                    Console.WriteLine("Category deleted.");
                    Pause();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Pause();
        }
    }
}


void UserMenu(UserDAL dal)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Users ===");
        Console.WriteLine("1. Add");
        Console.WriteLine("2. Get by ID");
        Console.WriteLine("3. Get all");
        Console.WriteLine("4. Update");
        Console.WriteLine("5. Delete");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");

        string? choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.Write("Login: ");
                    string login = Console.ReadLine() ?? "";

                    Console.Write("Password: ");
                    string password = Console.ReadLine() ?? "";

                    Console.Write("First name: ");
                    string firstName = Console.ReadLine() ?? "";

                    Console.Write("Last name: ");
                    string lastName = Console.ReadLine() ?? "";

                    Console.Write("Is active (true/false): ");
                    bool isActive = bool.Parse(Console.ReadLine()!);

                    Console.Write("Role ID: ");
                    int roleId = int.Parse(Console.ReadLine()!);

                    dal.Add(new User
                    {
                        Login = login,
                        Password = password,
                        FirstName = firstName,
                        LastName = lastName,
                        IsActive = isActive,
                        RoleId = roleId
                    });

                    Console.WriteLine("User added.");
                    Pause();
                    break;

                case "2":
                    Console.Write("User ID: ");
                    int id = int.Parse(Console.ReadLine()!);

                    var user = dal.GetById(id);

                    if (user == null)
                        Console.WriteLine("User not found.");
                    else
                        Console.WriteLine(
                            $"{user.UserId}: {user.Login}, {user.FirstName} {user.LastName}, Active: {user.IsActive}, RoleId: {user.RoleId}");

                    Pause();
                    break;

                case "3":
                    var users = dal.GetAll();

                    foreach (var item in users)
                    {
                        Console.WriteLine(
                            $"{item.UserId}: {item.Login}, {item.FirstName} {item.LastName}, Active: {item.IsActive}, RoleId: {item.RoleId}");
                    }

                    Pause();
                    break;

                case "4":
                    Console.Write("User ID: ");
                    int updateId = int.Parse(Console.ReadLine()!);

                    Console.Write("Login: ");
                    string newLogin = Console.ReadLine() ?? "";

                    Console.Write("Password: ");
                    string newPassword = Console.ReadLine() ?? "";

                    Console.Write("First name: ");
                    string newFirstName = Console.ReadLine() ?? "";

                    Console.Write("Last name: ");
                    string newLastName = Console.ReadLine() ?? "";

                    Console.Write("Is active (true/false): ");
                    bool newIsActive = bool.Parse(Console.ReadLine()!);

                    Console.Write("Role ID: ");
                    int newRoleId = int.Parse(Console.ReadLine()!);

                    dal.Update(new User
                    {
                        UserId = updateId,
                        Login = newLogin,
                        Password = newPassword,
                        FirstName = newFirstName,
                        LastName = newLastName,
                        IsActive = newIsActive,
                        RoleId = newRoleId
                    });

                    Console.WriteLine("User updated.");
                    Pause();
                    break;

                case "5":
                    Console.Write("User ID: ");
                    int deleteId = int.Parse(Console.ReadLine()!);

                    dal.Delete(deleteId);

                    Console.WriteLine("User deleted.");
                    Pause();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Pause();
        }
    }
}


void ProductMenu(ProductDAL dal)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Products ===");
        Console.WriteLine("1. Add");
        Console.WriteLine("2. Get by ID");
        Console.WriteLine("3. Get all");
        Console.WriteLine("4. Update");
        Console.WriteLine("5. Delete");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");

        string? choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.Write("Product name: ");
                    string productName = Console.ReadLine() ?? "";

                    Console.Write("Price: ");
                    decimal price = decimal.Parse(Console.ReadLine()!);

                    Console.Write("Quantity: ");
                    int quantity = int.Parse(Console.ReadLine()!);

                    Console.Write("Category ID: ");
                    int categoryId = int.Parse(Console.ReadLine()!);

                    dal.Add(new Product
                    {
                        ProductName = productName,
                        Price = price,
                        Quantity = quantity,
                        CategoryId = categoryId
                    });

                    Console.WriteLine("Product added.");
                    Pause();
                    break;

                case "2":
                    Console.Write("Product ID: ");
                    int id = int.Parse(Console.ReadLine()!);

                    var product = dal.GetById(id);

                    if (product == null)
                        Console.WriteLine("Product not found.");
                    else
                        Console.WriteLine(
                            $"{product.ProductId}: {product.ProductName}, Price: {product.Price}, Quantity: {product.Quantity}, CategoryId: {product.CategoryId}");

                    Pause();
                    break;

                case "3":
                    var products = dal.GetAll();

                    foreach (var item in products)
                    {
                        Console.WriteLine(
                            $"{item.ProductId}: {item.ProductName}, Price: {item.Price}, Quantity: {item.Quantity}, CategoryId: {item.CategoryId}");
                    }

                    Pause();
                    break;

                case "4":
                    Console.Write("Product ID: ");
                    int updateId = int.Parse(Console.ReadLine()!);

                    Console.Write("Product name: ");
                    string newProductName = Console.ReadLine() ?? "";

                    Console.Write("Price: ");
                    decimal newPrice = decimal.Parse(Console.ReadLine()!);

                    Console.Write("Quantity: ");
                    int newQuantity = int.Parse(Console.ReadLine()!);

                    Console.Write("Category ID: ");
                    int newCategoryId = int.Parse(Console.ReadLine()!);

                    dal.Update(new Product
                    {
                        ProductId = updateId,
                        ProductName = newProductName,
                        Price = newPrice,
                        Quantity = newQuantity,
                        CategoryId = newCategoryId
                    });

                    Console.WriteLine("Product updated.");
                    Pause();
                    break;

                case "5":
                    Console.Write("Product ID: ");
                    int deleteId = int.Parse(Console.ReadLine()!);

                    dal.Delete(deleteId);

                    Console.WriteLine("Product deleted.");
                    Pause();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Pause();
        }
    }
}


void NewsMenu(NewsDAL dal)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== News ===");
        Console.WriteLine("1. Add");
        Console.WriteLine("2. Get by ID");
        Console.WriteLine("3. Get all");
        Console.WriteLine("4. Update");
        Console.WriteLine("5. Delete");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");

        string? choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.Write("Title: ");
                    string title = Console.ReadLine() ?? "";

                    Console.Write("Content: ");
                    string content = Console.ReadLine() ?? "";

                    Console.Write("Is active (true/false): ");
                    bool isActive = bool.Parse(Console.ReadLine()!);

                    Console.Write("Created by User ID: ");
                    int createdByUserId = int.Parse(Console.ReadLine()!);

                    dal.Add(new News
                    {
                        Title = title,
                        Content = content,
                        CreatedAt = DateTime.Now,
                        IsActive = isActive,
                        CreatedByUserId = createdByUserId
                    });

                    Console.WriteLine("News added.");
                    Pause();
                    break;

                case "2":
                    Console.Write("News ID: ");
                    int id = int.Parse(Console.ReadLine()!);

                    var news = dal.GetById(id);

                    if (news == null)
                        Console.WriteLine("News not found.");
                    else
                        Console.WriteLine(
                            $"{news.NewsId}: {news.Title}, Active: {news.IsActive}, Created: {news.CreatedAt}");

                    Pause();
                    break;

                case "3":
                    var newsList = dal.GetAll();

                    foreach (var item in newsList)
                    {
                        Console.WriteLine(
                            $"{item.NewsId}: {item.Title}, Active: {item.IsActive}, Created: {item.CreatedAt}");
                    }

                    Pause();
                    break;

                case "4":
                    Console.Write("News ID: ");
                    int updateId = int.Parse(Console.ReadLine()!);

                    Console.Write("Title: ");
                    string newTitle = Console.ReadLine() ?? "";

                    Console.Write("Content: ");
                    string newContent = Console.ReadLine() ?? "";

                    Console.Write("Is active (true/false): ");
                    bool newIsActive = bool.Parse(Console.ReadLine()!);

                    Console.Write("Created by User ID: ");
                    int newCreatedByUserId = int.Parse(Console.ReadLine()!);

                    dal.Update(new News
                    {
                        NewsId = updateId,
                        Title = newTitle,
                        Content = newContent,
                        CreatedAt = DateTime.Now,
                        IsActive = newIsActive,
                        CreatedByUserId = newCreatedByUserId
                    });

                    Console.WriteLine("News updated.");
                    Pause();
                    break;

                case "5":
                    Console.Write("News ID: ");
                    int deleteId = int.Parse(Console.ReadLine()!);

                    dal.Delete(deleteId);

                    Console.WriteLine("News deleted.");
                    Pause();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Pause();
        }
    }
}


void NewsProductMenu(NewsProductDAL dal)
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== NewsProducts ===");
        Console.WriteLine("1. Add");
        Console.WriteLine("2. Get by ID");
        Console.WriteLine("3. Get all");
        Console.WriteLine("4. Update");
        Console.WriteLine("5. Delete");
        Console.WriteLine("0. Back");
        Console.Write("Choose: ");

        string? choice = Console.ReadLine();

        try
        {
            switch (choice)
            {
                case "1":
                    Console.Write("News ID: ");
                    int newsId = int.Parse(Console.ReadLine()!);

                    Console.Write("Product ID: ");
                    int productId = int.Parse(Console.ReadLine()!);

                    dal.Add(new NewsProduct
                    {
                        NewsId = newsId,
                        ProductId = productId
                    });

                    Console.WriteLine("NewsProduct added.");
                    Pause();
                    break;

                case "2":
                    Console.Write("News ID: ");
                    int getNewsId = int.Parse(Console.ReadLine()!);

                    Console.Write("Product ID: ");
                    int getProductId = int.Parse(Console.ReadLine()!);

                    var newsProduct = dal.GetById(getNewsId, getProductId);

                    if (newsProduct == null)
                        Console.WriteLine("NewsProduct not found.");
                    else
                        Console.WriteLine(
                            $"NewsId: {newsProduct.NewsId}, ProductId: {newsProduct.ProductId}");

                    Pause();
                    break;

                case "3":
                    var newsProducts = dal.GetAll();

                    foreach (var item in newsProducts)
                        Console.WriteLine(
                            $"NewsId: {item.NewsId}, ProductId: {item.ProductId}");

                    Pause();
                    break;

                case "4":
                    Console.Write("Old News ID: ");
                    int oldNewsId = int.Parse(Console.ReadLine()!);

                    Console.Write("Old Product ID: ");
                    int oldProductId = int.Parse(Console.ReadLine()!);

                    Console.Write("New News ID: ");
                    int newNewsId = int.Parse(Console.ReadLine()!);

                    Console.Write("New Product ID: ");
                    int newProductId = int.Parse(Console.ReadLine()!);

                    dal.Update(
                        new NewsProduct
                        {
                            NewsId = newNewsId,
                            ProductId = newProductId
                        },
                        oldNewsId,
                        oldProductId);

                    Console.WriteLine("NewsProduct updated.");
                    Pause();
                    break;

                case "5":
                    Console.Write("News ID: ");
                    int deleteNewsId = int.Parse(Console.ReadLine()!);

                    Console.Write("Product ID: ");
                    int deleteProductId = int.Parse(Console.ReadLine()!);

                    dal.Delete(deleteNewsId, deleteProductId);

                    Console.WriteLine("NewsProduct deleted.");
                    Pause();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    Pause();
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Pause();
        }
    }
}


void Pause()
{
    Console.WriteLine();
    Console.WriteLine("Press Enter to continue...");
    Console.ReadLine();
}