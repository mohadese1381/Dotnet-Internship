using Microsoft.AspNetCore.Mvc;
using WebApplication2.Contract.V1;
using WebApplication2.Contract.V1.Requests;
using WebApplication2.Contract.V1.Responses;
using WebApplication2.Domain;
using WebApplication2.Services;

namespace WebApplication2.Controllers.V1;

public class TestController : Controller
{

    private readonly IPostService _postService;

   public TestController(IPostService postService) { _postService = postService; }
   
//---------------------------------------
   [HttpGet(ApiRoutes.Posts.Get)]
    public async Task<IActionResult> GetById([FromRoute] string postId)
    {
        var post = await  _postService.GetPostByIdAsync(postId);

        if (post== null)
            return NotFound();

        return Ok(post);

    }
    
    //----------------------
    [HttpPut(ApiRoutes.Posts.Update)]
    public async Task<IActionResult> Update([FromRoute] string postId,[FromBody] UpdatePostRequest request )
    {
        var post = new Post{Id = postId , Name = request.Name};

        var updated = await _postService.UpdatePostAsync(post);

        if (updated)
            return Ok(post);

        return NotFound();
    }
    
//------------------------------------------

    [HttpGet(ApiRoutes.Posts.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        return Ok(_postService.GetPostsAsync());
    }
    
    //-----------------------------------------
    [HttpPost(ApiRoutes.Posts.Create)]
    public async Task<IActionResult> CreatResult([FromBody] CreatePostRequest postRequest)
    {
        if (string.IsNullOrEmpty(postRequest.Name))
            postRequest.Name = Guid.NewGuid().ToString();

        var post = new Post{Id = postRequest.Id, Name = postRequest.Name};
        
        await _postService.CreatPostAsync(post);
        
        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}",post.Id);
        var response = new PostResponse {Id = post.Id };
        return Created(locationUri,response);
    }

    [HttpDelete(ApiRoutes.Posts.Delete)]

    public async Task<IActionResult> DeletePost([FromRoute] string postId)
    {
        var deleted = await _postService.DeletePostAsync(postId);

        if (deleted)
            return NoContent();
        return NotFound();
    }
}