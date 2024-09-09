using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webintroBE.Data;
using webintroBE.DTOs;

namespace webintroBE.Controllers
{
    [Route("api/")]
    [ApiController]
    public class BaiVietController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BaiVietController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("bai-viet/getdata")]
        public async Task<ApiResponse> GetBaiViets()
        {
            var baiviets = await _context.BaiViets
                                         .OrderByDescending(bv => bv.CreatedAt)
                                         .ToListAsync();
            return new ApiResponse(200, "Day la message", baiviets);
        }

        [HttpGet("bai-viet/getdata/{id}")]
        public async Task<ApiResponse> GetByBaiVietSelectId(int id)
        {
            var baiviet = await _context.BaiViets.FindAsync(id);
            if (baiviet == null)
            {
                return new ApiResponse(404, "Khong thay bai viet", baiviet);

            }
            return new ApiResponse(200, "Day la message", baiviet);

        }

        [HttpPost]
        [Route("bai-viet/createdata")]
        public async Task<ApiResponse> CreateBaiViet(BaiViet baiviet)
        {
            _context.BaiViets.Add(baiviet);
            await _context.SaveChangesAsync();

            return new ApiResponse(200, "Day la message", baiviet);
        }

        [HttpPut("bai-viet/updatedata/{id}")]
        public async Task<ApiResponse> UpdateBaiViet(int id, BaiViet baiviet)
        {
            if (id != baiviet.Id)
            {
                return new ApiResponse(400, "Loi");
            }

            _context.Entry(baiviet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new ApiResponse(200, "cap nhat thanh cong", baiviet);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BaiVietExists(id))
                {
                    return new ApiResponse(404, "Loi");
                }
                else
                {
                    return new ApiResponse(404, ex.Message);
                }
            }
        }

        [HttpDelete("bai-viet/deletedata/{id}")]

        public async Task<ApiResponse> DeleteBaiViet(int id)
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
