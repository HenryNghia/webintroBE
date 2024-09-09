using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webintroBE.Data;
using webintroBE.DTOs;
using System.Threading.Tasks;

namespace webintroBE.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmailController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả email
        [HttpGet]
        [Route("email/getdata")]
        public async Task<IActionResult> GetEmails()
        {
            var emails = await _context.Emails
                                       .OrderByDescending(e => e.CreatedAt)
                                       .ToListAsync();
            return Ok(new ApiResponse(200, "Danh sách email", emails));
        }

        // Lấy email theo ID
        [HttpGet("email/getdata/{id}")]
        public async Task<IActionResult> GetEmailById(int id)
        {
            var email = await _context.Emails.FindAsync(id);
            if (email == null)
            {
                return NotFound(new ApiResponse(404, "Không tìm thấy email", null));
            }
            return Ok(new ApiResponse(200, "Thông tin email", email));
        }

        // Tạo email mới
        [HttpPost]
        [Route("email/createdata")]
        public async Task<IActionResult> CreateEmail([FromBody] AddEmailDTO addEmailDTO)
        {
            var email = new Email
            {
                Name = addEmailDTO.Name,
                Surname = addEmailDTO.Surname,
                Content = addEmailDTO.Content,
                Emailuser = addEmailDTO.Emailuser,
                CreatedAt = DateTime.Now
            };

            _context.Emails.Add(email);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse(200, "Tạo email thành công", email));
        }

        // Xóa email
        [HttpDelete("email/deletedata/{id}")]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            var email = await _context.Emails.FindAsync(id);
            if (email == null)
            {
                return NotFound(new ApiResponse(404, "Không tìm thấy email"));
            }

            _context.Emails.Remove(email);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse(200, "Xóa email thành công"));
        }
    }
}