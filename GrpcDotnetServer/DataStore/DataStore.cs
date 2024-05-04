using GrpcDotnetServer.Models;

namespace GrpcDotnetServer.DataStore;

public class DataStore
{
    public List<UserModel> GetUserModels()
    {
        return
        [
            new UserModel
            {
                Id = "1",
                Name = "John Doe",
                Email = "john.doe@gmail.com",
                Age = 30,
            },
            new UserModel
            {
                Id = "2",
                Name = "Jane Doe",
                Email = "jane.doe@gmail.com",
                Age = 25,
            },
            new UserModel
            {
                Id = "3",
                Name = "Alice",
                Email = "alicewonderland@gmail.com",
                Age = 22,
            },
            new UserModel
            {
                Id = "4",
                Name = "Bob",
                Email = "bobmaster@gmail.com",
                Age = 35,
            },
            new UserModel
            {
                Id = "5",
                Name = "Charlie",
                Email = "charliechaplin@gmail.com",
                Age = 40,
            },
        ];
    }

    public List<CourseModel> GetCourseModels()
    {
        return
        [
            new CourseModel
            {
                Id = "1",
                Name = "C# Programming",
                Description = "Learn C# Programming",
            },
            new CourseModel
            {
                Id = "2",
                Name = "Java Programming",
                Description = "Learn Java Programming",
            },
            new CourseModel
            {
                Id = "3",
                Name = "Python Programming",
                Description = "Learn Python Programming",
            },
            new CourseModel
            {
                Id = "4",
                Name = "JavaScript Programming",
                Description = "Learn JavaScript Programming",
            },
            new CourseModel
            {
                Id = "5",
                Name = "C++ Programming",
                Description = "Learn C++ Programming",
            },
        ];
    }

    public List<UserCourseModel> GetUserCourseModels()
    {
        return
        [
            new UserCourseModel
            {
                UserId = "1",
                CourseId = "1",
            },
            new UserCourseModel
            {
                UserId = "1",
                CourseId = "2",
            },
            new UserCourseModel
            {
                UserId = "1",
                CourseId = "3",
            },
            new UserCourseModel
            {
                UserId = "2",
                CourseId = "2",
            },
            new UserCourseModel
            {
                UserId = "2",
                CourseId = "5",
            },
            new UserCourseModel
            {
                UserId = "3",
                CourseId = "1",
            },
            new UserCourseModel
            {
                UserId = "3",
                CourseId = "2",
            },
            new UserCourseModel
            {
                UserId = "3",
                CourseId = "4",
            },
            new UserCourseModel
            {
                UserId = "4",
                CourseId = "2",
            },
            new UserCourseModel
            {
                UserId = "4",
                CourseId = "3",
            },
            new UserCourseModel
            {
                UserId = "4",
                CourseId = "4",
            },
            new UserCourseModel
            {
                UserId = "5",
                CourseId = "3",
            },
            new UserCourseModel
            {
                UserId = "5",
                CourseId = "4",
            },
            new UserCourseModel
            {
                UserId = "5",
                CourseId = "5",
            },
        ];
    }
}