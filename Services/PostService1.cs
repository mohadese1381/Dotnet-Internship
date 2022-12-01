using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Domain;

namespace WebApplication2.Services;

public class PostService : IPostService
{
    private readonly DataContext _dataContext;
    public PostService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<Post>> GetPostsAsync()
    {
        return await _dataContext.Posts.ToListAsync();
    }

    public async Task<Post> GetPostByIdAsync(string postId)
    {
        return await _dataContext.Posts.SingleOrDefaultAsync(x => x.Id == postId);;
    }

    public async Task<bool> UpdatePostAsync(Post postToUpdate)
    {
        _dataContext.Posts.Update(postToUpdate);
        var updated = await _dataContext.SaveChangesAsync();

        return updated > 0;

    }

    public async Task<bool> CreatPostAsync(Post post)
    {
        await _dataContext.Posts.AddAsync(post);
        var created = await _dataContext.SaveChangesAsync();
        return created>0;
    }

    public async Task<bool> DeletePostAsync(string postId)
    {
        var post = await GetPostByIdAsync(postId);

        if (post==null)
        {
            return false;
        }
        
        _dataContext.Posts.Remove(post);
        var deleted = await _dataContext.SaveChangesAsync();
        return deleted > 0;
    }
}