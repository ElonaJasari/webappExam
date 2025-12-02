using Microsoft.EntityFrameworkCore;
using FirstMVC.Data;
using Microsoft.AspNetCore.Identity;
using FirstMVC.Repositories;
using FirstMVC.Models; // Add this using statement

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repository Pattern (Data Access Layer)
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();

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

    // Create admin role and user, and ensure core characters exist
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

    // Ensure the 4 core characters exist
    var coreCharacterRoles = new[] { "ID_FRIEND1", "ID_FRIEND2", "ID_PARENT", "ID_PRINCIPAL" };
    var existingRoles = await dbContext.Characters
        .Where(c => coreCharacterRoles.Contains(c.Role))
        .Select(c => c.Role)
        .ToListAsync();

    var charactersToAdd = new List<Characters>();

    if (!existingRoles.Contains("ID_FRIEND1"))
    {
        charactersToAdd.Add(new Characters
        {
            Name = "Friend 1",
            Role = "ID_FRIEND1",
            Description = "Your first friend in the story",
            Dialog = "",
            ImageUrl = "",
            Translate = ""
        });
    }

    if (!existingRoles.Contains("ID_FRIEND2"))
    {
        charactersToAdd.Add(new Characters
        {
            Name = "Friend 2",
            Role = "ID_FRIEND2",
            Description = "Your second friend in the story",
            Dialog = "",
            ImageUrl = "",
            Translate = ""
        });
    }

    if (!existingRoles.Contains("ID_PARENT"))
    {
        charactersToAdd.Add(new Characters
        {
            Name = "Parent",
            Role = "ID_PARENT",
            Description = "The parent character",
            Dialog = "",
            ImageUrl = "",
            Translate = ""
        });
    }

    if (!existingRoles.Contains("ID_PRINCIPAL"))
    {
        charactersToAdd.Add(new Characters
        {
            Name = "Principal",
            Role = "ID_PRINCIPAL",
            Description = "The school principal",
            Dialog = "",
            ImageUrl = "",
            Translate = ""
        });
    }

    if (charactersToAdd.Any())
    {
        dbContext.Characters.AddRange(charactersToAdd);
        await dbContext.SaveChangesAsync();
    }
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // <-- Identity UI pages

app.Run();