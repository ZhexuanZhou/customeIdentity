# customeIdentity
Basiclly, in order to use your own user and role model, you need to implement as follow:

  1. create your user, role and userRole

  2. create your userStore and roleStore, and userStore should implement IUserStore, IUserPasswordStore and etc., basing on
     what you want to implement.
     
  3. create your own userManager and roleManager
  4. add code to ConfigureServices in Startup:
     ```C#
      services.AddScoped<UserManager<User>>();
      services.AddScoped<RoleManager<Role>>();
      services.AddIdentity<User, Role>()
        // .AddUserManager<UserManager<User>>()
        .AddUserStore<UserStore>()
        .AddRoleStore<RoleStore>();
     ```
   5. then add code to Configure in startup
      ```C#
      app.UseAuthentication();
      ```
   ref: https://github.com/aspnet/AspNetCore/tree/master/src/Identity/EntityFrameworkCore/src
