using Services;
using Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EndPoints;

public static class UserEndpoints
{
    private const string _tag = "user";
    private const string _route_createUser = "/api/Signup";
    private const string _route_SignIn = "/api/login";
    private const string _route_Info = "/api/id";
    private const string _routeAlluserInformation = "/api/user";
    private const string _route_signout = "/api/logout";
    private const string _contentType = "application/json";
    private const string _bill_Tag = "bill";
    private const string _route_Bill = "/api/Bill";


    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(_route_createUser, CreateNewUserAsync)
            .WithOpenApi()
            .WithSummary("Successfully created a new user")
            .WithTags(_tag);

        app.MapGet(_route_Info, GetUserInfo)
            .WithOpenApi()
            .WithSummary("Get user by id")
            .WithTags(_tag);

        app.MapPost(_route_SignIn, SignInAsync)
            .WithOpenApi()
            .WithSummary("User sign in")
            .WithTags(_tag);

        app.MapPut("/api/user/update", UpdateUserAsync)
            .WithOpenApi()
            .WithSummary("Update user information")
            .WithTags(_tag);

        app.MapPost(_route_signout, SignOutAsync)
            .WithOpenApi()
            .WithSummary("User sign out")
            .WithTags(_tag);
        app.MapPost(_route_Bill, AddBill)
            .WithOpenApi()
            .WithSummary("Create Bill")
            .WithTags(_bill_Tag);
    }

    public record UserResponse(int userId, string userName, string userEmail);
    public record BillRequest(decimal TotalAmount, DateTime CreatedAt,string ProductName ,string? UserName = null, string? UserEmail = null, string? Address = null);
    public record CreateUserRequest(string userName, string userEmail, string userPassword);
    public record SignInRequest(string userEmail, string userPassword);

    static async Task<IResult> CreateNewUserAsync(CreateUserRequest request, IUseService userService)
    {
        var user = new User { Name = request.userName, Email = request.userEmail, Password = request.userPassword };
        await userService.AddUser(user);
        return Results.Created($"/User/{user.Name}", new UserResponse(user.Id, user.Name, user.Email));
    }

    static async Task<IResult> UpdateUserAsync(CreateUserRequest request, IUseService userService, HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Results.Unauthorized();
        }

        var userId = int.Parse(userIdClaim.Value);
        var userToUpdate = new User
        {
            Id = userId,
            Name = request.userName,
            Email = request.userEmail,
            Password = request.userPassword
        };
        try
        {
            var updatedUser = await userService.UpdateUser(userToUpdate);
            return Results.Ok(new CreateUserRequest(updatedUser.Name!, updatedUser.Email!, updatedUser.Password!));
        }
        catch (ArgumentException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }
    static IResult GetUserInfo(HttpContext httpContext, IUseService userService)
    {
        var userEmail = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userEmail == null)
        {
            return Results.Unauthorized();
        }
        var user = userService.GetUserById(userEmail);
        if (user != null)
        {
            return Results.Ok(new UserResponse(user.Id, user.Name!, user.Email!));
        }
        return Results.NotFound();
    }
    static async Task<IResult> SignInAsync(SignInRequest request, IUseService userService, HttpContext httpContext)
    {
        var user = await userService.SignInQuery(request.userEmail, request.userPassword);
        if (user != null)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email!),
                new Claim(ClaimTypes.Name, user.Name!)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await httpContext.SignInAsync("Cookies", claimsPrincipal);
            return Results.Ok(new { Message = "Inloggning lyckades" });
        }
        return Results.Unauthorized();
    }

    static async Task<IResult> SignOutAsync(HttpContext httpContext)
    {
        await httpContext.SignOutAsync("Cookies");
        return Results.Ok(new { Message = "Utloggning lyckades" });
    }
    static async Task<IResult> AddBill(BillRequest request, IUseService useService, HttpContext httpContext)
    {
        var userEmail = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        User? user = null;
        if (userEmail != null)
        {
            user = useService.GetUserById(userEmail);
        }
        var bill = new Bill
        {
            TotalAmount = request.TotalAmount,
            CreatedAt = request.CreatedAt,
            UserId = user?.Id,
            UserName = user?.Name ?? request.UserName,
            UserEmail = user?.Email ?? request.UserEmail,
            Address = user?.Address ?? request.Address,
            productName = request.ProductName
        };
        await useService.AddBill(bill);
        return Results.Ok(new
        {
            BillId = bill.Id,
            TotalAmount = bill.TotalAmount,
            CreatedAt = bill.CreatedAt,
            UserName = bill.UserName,
            UserEmail = bill.UserEmail,
            Address = bill.Address,
            productName= bill.productName,
        });
    }
}
