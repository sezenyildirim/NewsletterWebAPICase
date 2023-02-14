using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsletterWebAPI.Context;
using Microsoft.EntityFrameworkCore;
using NewsletterWebAPI.Models;

namespace NewsletterWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly NewsletterDB _context;

        public NewsController(NewsletterDB context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _context.Newsletters.ToListAsync();
            return Ok(result);  
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int ID)
        {
            var result = await _context.Newsletters.FindAsync(ID);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Newsletter newsLetter)
        {
            newsLetter.CreatedDate = DateTime.Now;
            await _context.Newsletters.AddAsync(newsLetter);
            await _context.SaveChangesAsync();
            return Ok("Haber kaydı başarılı");
        }

        [HttpPut]
        public async Task<IActionResult> Put(Newsletter newsLetter)
        {
             _context.Newsletters.Update(newsLetter);
            await _context.SaveChangesAsync();
            return Ok("Haber güncelleme işlemi başarılı");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int ID)
        {
            var result = await _context.Newsletters.FindAsync(ID);
            _context.Newsletters.Remove(result);
            await _context.SaveChangesAsync();
            return Ok("Haber silme işlemi başarılı");
        }
    }
}
