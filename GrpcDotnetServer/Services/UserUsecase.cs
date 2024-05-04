using Grpc.Core;
using UserCourseGrpcDotnet;

namespace GrpcDotnetServer.Services;

public class UserUsecase(DataStore.DataStore dataStore, ILogger<UserUsecase> logger) : UserService.UserServiceBase
{
    private readonly DataStore.DataStore _dataStore = dataStore;
    private readonly ILogger<UserUsecase> _logger = logger;

    public override Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext ctx)
    {
        _logger.LogInformation("GetUser called with request: {request}", request);
        var response = new GetUserResponse();
        var users = _dataStore.GetUserModels();
        var user = users.FirstOrDefault(u => u.Id == request.Id);
        if (user == null)
        {
            _logger.LogInformation("User not found with id: {id}", request.Id);
            return Task.FromResult(response);
        }

        response.User = new User
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Age = user.Age
        };
        return Task.FromResult(response);
    }
}