using System.Threading.Tasks;
using Grpc.Net.Client;
using UserCourseGrpcDotnet;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:5000");
var client = new UserService.UserServiceClient(channel);
var reply = await client.GetUserListAsync(new GetUserListRequest { });

foreach (var user in reply.UserList.Users)
{
    var userDetail = await client.GetUserAsync(new GetUserRequest { Id = user.Id });
    Console.WriteLine($"User: {userDetail.User.Name} - {userDetail.User.Email} - {userDetail.User.Age}");

    var courseList = await client.GetCourseListByUserAsync(new GetCourseListByUserRequest { UserId = user.Id });
    foreach (var course in courseList.CourseList.Courses)
    {
        Console.WriteLine($"Course: {course.Name} - {course.Description}");
    }
    Console.WriteLine();
}

Console.WriteLine("---------------------------------------------\n");

var courseClient = new CourseService.CourseServiceClient(channel);
var courseListReply = await courseClient.GetCourseListAsync(new GetCourseListRequest { });

foreach (var course in courseListReply.CourseList.Courses)
{
    var courseDetail = await courseClient.GetCourseAsync(new GetCourseRequest { Id = course.Id });
    Console.WriteLine($"Course: {courseDetail.Course.Name} - {courseDetail.Course.Description}");

    var userList = await courseClient.GetUserListByCourseAsync(new GetUserListByCourseRequest { CourseId = course.Id });
    foreach (var user in userList.UserList.Users)
    {
        Console.WriteLine($"User: {user.Name} - {user.Email} - {user.Age}");
    }
    Console.WriteLine();
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
