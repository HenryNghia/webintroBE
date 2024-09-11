using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webintroBE.Data;
using webintroBE.DTOs;
using webintroBE.DTOs.BaiViet;
using webintroBE.Models;

namespace webintroBE.Controllers
{
    [Route("api/bai-viets/")]
    [ApiController]
    public class BaiVietController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BaiVietController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public async Task<ApiResponse> GetBaiViets()
        {
            var baiviets = await _context.BaiViets
                                         .OrderByDescending(bv => bv.CreatedAt)
                                         .ToListAsync();
            return new ApiResponse(200, "Day la message", baiviets);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse> GetByBaiVietSelectId([FromRoute] int id)
        {
            var baiviet = await _context.BaiViets.FindAsync(id);
            if (baiviet == null)
            {
                return new ApiResponse(404, "Khong thay bai viet", baiviet);

            }
            return new ApiResponse(200, "Day la message", baiviet);

        }

        [HttpPost]
        [Route("")]
        public async Task<ApiResponse> CreateBaiViet([FromBody] CreateBaiVietDTO createBaiVietDTO)

        {
            var baiviet = new BaiViet
            {
                Title = createBaiVietDTO.Title,
                Image = createBaiVietDTO.Image,
                Content = createBaiVietDTO.Content,
            };
            _context.BaiViets.Add(baiviet);
            await _context.SaveChangesAsync();

            return new ApiResponse(200, "Day la message", baiviet);
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdateBaiViet([FromRoute] int id, [FromBody] UpdateBaiVietDTO updateBaiVietDTO)
        {
            var baiviet = _context.BaiViets.FirstOrDefault(x => x.Id == id);
            if (baiviet == null)
            {
                return new ApiResponse(400, "Loi");
            }
            baiviet.Title = updateBaiVietDTO.Title;
            baiviet.Image = updateBaiVietDTO.Image;
            baiviet.Content = updateBaiVietDTO.Content;

            await _context.SaveChangesAsync();
            return new ApiResponse(200, "cap nhat thanh cong", baiviet);
        }

        [HttpDelete("{id}")]

        public async Task<ApiResponse> DeleteBaiViet([FromRoute] int id)
        {
            var product = await _context.BaiViets.FindAsync(id);
            if (product == null)
            {
                return new ApiResponse(404, "Khong tim thay");
            }

            _context.BaiViets.Remove(product);
            await _context.SaveChangesAsync();

            return new ApiResponse(200, "Xoa thanh cong");
        }

        private bool BaiVietExists(int id)
        {
            return _context.BaiViets.Any(e => e.Id == id);
        }
    }
}
