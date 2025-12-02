using Microsoft.EntityFrameworkCore;
using FirstMVC.Data;
using Microsoft.AspNetCore.Identity;
using FirstMVC.Models; // Add this using statement
using FirstMVC.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository Pattern (Data Access Layer) for TaskDB
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Register Identity (uses Identity UI)
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // <-- must be before UseAuthorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Create admin role and user, and mock character
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Create Admin role
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    // Create Admin user
    var adminEmail = "admin@test.com";
    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, "Admin.123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    // Create the 4 core characters if none exist
    if (!await dbContext.Characters.AnyAsync())
    {
        var coreCharacters = new[]
        {
            new Characters
            {
                Name = "Friend 1",
                Role = "ID_FRIEND1",
                Description = "Your first friend in the story",
                Dialog = "",
                ImageUrl = "",
                Translate = ""
            },
            new Characters
            {
                Name = "Friend 2",
                Role = "ID_FRIEND2",
                Description = "Your second friend in the story",
                Dialog = "",
                ImageUrl = "",
                Translate = ""
            },
            new Characters
            {
                Name = "Parent",
                Role = "ID_PARENT",
                Description = "The parent character",
                Dialog = "",
                ImageUrl = "",
                Translate = ""
            },
            new Characters
            {
                Name = "Principal",
                Role = "ID_PRINCIPAL",
                Description = "The school principal",
                Dialog = "",
                ImageUrl = "",
                Translate = ""
            }
        };
        dbContext.Characters.AddRange(coreCharacters);
        await dbContext.SaveChangesAsync();
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // <-- Identity UI pages

app.Run();