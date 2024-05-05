using CustomGrpcClient;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var address = "http://localhost:5000";
var client = new GrpcClientUserCourse(address);
var userlist = await client.UserList();
Console.WriteLine("User List:");
foreach (var user in userlist)
{
    Console.WriteLine($"User: {user.User.Name} - {user.User.Email} - {user.User.Age}");
    foreach (var course in user.Courses)
    {
        Console.WriteLine($"Course: {course.Name} - {course.Description}");
    }
    Console.WriteLine();
}

var courselist = await client.CourseList();
Console.WriteLine("Course List:");
foreach (var course in courselist)
{
    Console.WriteLine($"Course: {course.Course.Name} - {course.Course.Description}");
    foreach (var user in course.Users)
    {
        Console.WriteLine($"User: {user.Name} - {user.Email} - {user.Age}");
    }
    Console.WriteLine();
}
