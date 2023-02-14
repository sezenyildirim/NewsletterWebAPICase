using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsletterWebAPI.Context;
using NewsletterWebAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using NewsletterWebAPI.Models;

namespace NewsletterWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly NewsletterDB _context;

        public AuthController(NewsletterDB context)
        {
            _context = context;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var isNameExist = await _context.Users.Where(p => p.UserName == registerDTO.UserName).FirstOrDefaultAsync();
            if(isNameExist != null)
            {
                return BadRequest("Bu kullanıcı adı daha önce alınmış!");
            }
            var isEmailExist = await _context.Users.Where(p => p.Email == registerDTO.Email).FirstOrDefaultAsync();
            if(isEmailExist != null)
            {
                return BadRequest("Bu mail adresi daha önce alınmış!");
            }

            User user = new()
            {
                Email = registerDTO.Email,
                Name = registerDTO.Name,
                Password = registerDTO.Password,
                UserName = registerDTO.UserName
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok("Kayıt işlemi başarılı");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var user = await _context.Users.Where(p=>p.UserName == loginDTO.UserName).FirstOrDefaultAsync();
            if(user == null)
            {
                user = await _context.Users.Where(p => p.Email == loginDTO.UserName).FirstOrDefaultAsync();
            }
            if(user == null)
            {
                return BadRequest("Kayıtlı kullanıcı bulunamadı!");
            }
            if(user.Password == loginDTO.Password)
            {
                return Ok("Kullanıcı giriş başarılı");
            }
            else { return BadRequest("Kullanıcı şifresi yanlış"); }

            return Ok();
        }
    }
}
