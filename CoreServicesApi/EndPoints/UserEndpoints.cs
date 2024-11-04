using Services;
using Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
namespace EndPoints;

public static class UserEndpoints
{
    private const string _tag = "user";
    private const string _route_createUser = "/api/Signup";
    private const string _route_SignIn = "/api/login";
    private const string _route_Info = "/api/id";
    private const string _routeAlluserInformation = "/api/user";
    private const string _contentType = "application/json";
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost(_route_createUser, CreateNewUserAsync)
            .WithOpenApi()
            .WithSummary("Successfully created a new user")
            .WithTags(_tag);
        app.MapGet(_route_Info, GetUserByTheId)
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
    }
    public record UserResponse(int userId, string userName, string userEmail);
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

    static IResult GetUserByTheId(string name, string password, IUseService userService)
    {
        var user = userService.GetUserById(name);
        return user != null ? Results.Ok(new UserResponse(user.Id, user.Name!, user.Email!)) : Results.NotFound();
    }
    static async Task<IResult> SignInAsync(SignInRequest request, IUseService userService, HttpContext httpContext)
    {
        var user = userService.SignInQuery(request.userEmail, request.userPassword);
        if (user.HasValue)
        {
            var userinfo = userService.GetUserById(request.userEmail);
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userinfo.Name!),
            new Claim(ClaimTypes.NameIdentifier, userinfo.Id.ToString())
        };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await httpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity));
            return Results.Ok(new UserResponse(user.Value, userinfo.Name!, request.userEmail));
        }
        return Results.Unauthorized();
    }
}
