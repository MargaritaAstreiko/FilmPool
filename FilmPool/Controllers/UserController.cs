using FileStorage.FileStorage;
using FilmPool.DbModels;
using FilmPool.RequestModels;
using FilmPool.ResponseModels;
using FilmPool.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmPool.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.Get();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int Id)
        {
            var user = await _userService.Get(Id);
            return Ok(user);
        }

        [HttpPost("Picture")]
        public async Task<IActionResult> PostPicture(IFormFile file)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            var picture = new Picture();
            var res = picture.UploadImage(bytes, 1, "dbo.Users");
            return Ok(res);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequestModel user)
        {
            var res = await _userService.Update(user);
            return Ok(res);
        }


    }
}
