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

    public override Task<GetUserListResponse> GetUserList(GetUserListRequest request, ServerCallContext ctx)
    {
        _logger.LogInformation("GetUserList called with request: {request}", request);
        var response = new GetUserListResponse();
        var users = _dataStore.GetUserModels();
        if (users == null)
        {
            _logger.LogInformation("No users found");
            return Task.FromResult(response);
        }
        response.UserList.Users.AddRange(users.Select(u => new User
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Age = u.Age
        }));
        return Task.FromResult(response);
    }
    public override Task<GetCourseListByUserResponse> GetCourseListByUser(GetCourseListByUserRequest request, ServerCallContext ctx)
    {
        _logger.LogInformation("GetCourseListByUser called with request: {request}", request);
        var response = new GetCourseListByUserResponse();
        var userCourses = _dataStore.GetUserCourseModels();
        if (userCourses == null)
        {
            _logger.LogInformation("No user courses found");
            return Task.FromResult(response);
        }
        var userCourseList = userCourses.Where(uc => uc.UserId == request.UserId).ToList();
        if (userCourseList == null)
        {
            _logger.LogInformation("No user courses found for user id: {userId}", request.UserId);
            return Task.FromResult(response);
        }

        var courses = _dataStore.GetCourseModels();
        if (courses == null)
        {
            _logger.LogInformation("No courses found");
            return Task.FromResult(response);
        }

        var coursesDetail = userCourseList.Select(uc => courses.FirstOrDefault(c => c.Id == uc.CourseId));
        if (coursesDetail == null)
        {
            _logger.LogInformation("No courses found for user id: {userId}", request.UserId);
            return Task.FromResult(response);
        }

        response.CourseList.Courses.AddRange(coursesDetail.Select(c => new Course
        {
            Id = c?.Id,
            Name = c?.Name,
            Description = c?.Description
        }));
        return Task.FromResult(response);
    }
}