using Grpc.Core;
using UserCourseGrpcDotnet;

namespace GrpcDotnetServer.Services;

public class CourseUsecase(DataStore.DataStore dataStore, ILogger<CourseUsecase> logger) : CourseService.CourseServiceBase
{
    private readonly DataStore.DataStore _dataStore = dataStore;
    private readonly ILogger<CourseUsecase> _logger = logger;

    public override Task<GetCourseResponse> GetCourse(GetCourseRequest request, ServerCallContext ctx)
    {
        _logger.LogInformation("GetCourse called with request: {request}", request);
        var response = new GetCourseResponse();
        var courses = _dataStore.GetCourseModels();
        var course = courses.FirstOrDefault(c => c.Id == request.Id);
        if (course == null)
        {
            _logger.LogInformation("Course not found with id: {id}", request.Id);
            return Task.FromResult(response);
        }

        response.Course = new Course
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
        };
        return Task.FromResult(response);
    }

    public override Task<GetCourseListResponse> GetCourseList(GetCourseListRequest request, ServerCallContext ctx)
    {
        _logger.LogInformation("GetCourseList called with request: {request}", request);
        var response = new GetCourseListResponse();
        var courses = _dataStore.GetCourseModels();
        if (courses == null)
        {
            _logger.LogInformation("No courses found");
            return Task.FromResult(response);
        }
        response.CourseList.Courses.AddRange(courses.Select(c => new Course
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
        }));
        return Task.FromResult(response);
    }

    public override Task<GetUserListByCourseResponse> GetUserListByCourse(GetUserListByCourseRequest request, ServerCallContext ctx)
    {
        _logger.LogInformation("GetUserListByCourse called with request: {request}", request);
        var response = new GetUserListByCourseResponse();
        var userCourses = _dataStore.GetUserCourseModels();
        if (userCourses == null)
        {
            _logger.LogInformation("No user courses found");
            return Task.FromResult(response);
        }
        var users = _dataStore.GetUserModels();
        var userCourseList = userCourses.Where(uc => uc.CourseId == request.CourseId);
        response.UserList.Users.AddRange(userCourseList.Select(uc => users.FirstOrDefault(u => u.Id == uc.UserId)).Select(u => new User
        {
            Id = u?.Id,
            Name = u?.Name,
            Email = u?.Email,
            Age = u == null ? -1 : u.Age
        }));
        return Task.FromResult(response);
    }
}