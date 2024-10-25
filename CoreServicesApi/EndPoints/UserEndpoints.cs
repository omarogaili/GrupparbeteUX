using Services;
using Models;
namespace EndPoints;

public static class UserEndpoints
{
    private const string _tag = "user";
    private const string _route_createUser = "/api/Signup";
    private const string _route_SignIn = "/api/login";
    private const string _route_Info = "/user/id";
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
    static IResult GetUserByTheId(string name, string password, IUseService userService)
    {
        var user = userService.GetUserById(name);
        return user != null ? Results.Ok(new UserResponse(user.Id, user.Name!, user.Email!)) : Results.NotFound();
    }
    static async Task<IResult> SignInAsync(SignInRequest request, IUseService userService)
    {
        var user = userService.SignInQuery(request.userEmail, request.userPassword);
        Console.WriteLine($"SignInQuery result for {request.userEmail}: {user.HasValue}");
        if (user.HasValue)
        {
            var userinfo = userService.GetUserById(request.userEmail);
            return Results.Ok(new UserResponse(user.Value, userinfo.Name, request.userEmail));
        }
        else
        {
            Console.WriteLine("Unauthorized: Invalid email or password");
            return Results.Unauthorized();
        }
    }
}
