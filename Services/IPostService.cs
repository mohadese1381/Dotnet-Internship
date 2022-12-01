using WebApplication2.Contract.V1;
using WebApplication2.Domain;

namespace WebApplication2.Services;

public interface IPostService
{
      
    public Task<List<Post>> GetPostsAsync();

    public Task<Post> GetPostByIdAsync(string postId);

    public Task<bool> UpdatePostAsync(Post postToUpdate);
    
    public Task<bool> CreatPostAsync(Post post);
    public Task<bool> DeletePostAsync(string postId);
}