using Grpc.Core;

namespace LabProject.Grpc.Services
{
    public class UserServiceImpl : UserService.UserServiceBase
    {
        public override Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UserResponse
            {
                Id = request.Id,
                Name = "John Doe",
                Email = "john.doe@example.com"
            });
        }
    }
}
