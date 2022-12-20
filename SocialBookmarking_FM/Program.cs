using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialBookmarking_FM.Data;
using SocialBookmarking_FM.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "addbookmark",
    pattern: "Bookmark/Create",
    defaults: new {controller = "Bookmark", action="Create"}
    );

app.MapControllerRoute(
    name: "addbookmark",
    pattern: "Bookmark/Index",
    defaults: new { controller = "Bookmark", action = "Index" }
    );

app.MapControllerRoute(
    name: "showBookmark",
    pattern: "Bookmark/Show/{id}",
    defaults: new { controller = "Bookmark", action = "Show" }
    );

app.MapControllerRoute(
    name: "editComment",
    pattern: "Comment/Edit/{id}",
    defaults: new { controller = "Comment", action = "Edit" }
    );

app.MapControllerRoute(
    name: "addcategory",
    pattern: "Category/Create",
    defaults: new { controller = "Category", action = "Create" }
    );

app.MapControllerRoute(
    name: "addcategory",
    pattern: "Category/Index",
    defaults: new { controller = "Category", action = "Index" }
    );

app.MapControllerRoute(
    name: "editcategory",
    pattern: "Category/Edit/{id}",
    defaults: new { controller = "Category", action = "Edit" }
    );

app.MapControllerRoute(
    name: "showcategory",
    pattern: "Category/Details/{id}",
    defaults: new { controller = "Category", action = "Show" }
    );

app.MapControllerRoute(
    name: "userbookmarks",
    pattern: "User/Bookmarks/{uname}",
    defaults: new { controller = "User", action = "Bookmarks" }
    );

app.MapControllerRoute(
    name: "addvote",
    pattern: "Vote/Toggle/{id}",
    defaults: new { controller = "Vote", action = "Toggle" }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
