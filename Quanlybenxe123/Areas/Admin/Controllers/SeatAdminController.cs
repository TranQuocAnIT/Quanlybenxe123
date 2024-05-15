using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quanlybenxe123.Models;
using Quanlybenxe123.Repository;

namespace Quanlybenxe123.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize]
    [Authorize(Roles = "Admin")]
    [Authorize]
    public class SeatAdminController : Controller
    {
        private readonly ISeatRepository _seatRepository;

        public SeatAdminController(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }

        // Hiển thị danh sách ghế
        public async Task<IActionResult> Index()
        {
            var seats = await _seatRepository.GetAllAsync();
            return View(seats);
        }

        // Hiển thị form thêm ghế mới
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Seat seat)
        {
            if (ModelState.IsValid)
            {
                await _seatRepository.AddAsync(seat);
                return RedirectToAction(nameof(Index));
            }
            return View(seat);
        }

        // Hiển thị thông tin chi tiết ghế
        public async Task<IActionResult> Display(int id)
        {
            var seat = await _seatRepository.GetByIdAsync(id);
            if (seat == null)
            {
                return NotFound();
            }
            return View(seat);
        }

        // Hiển thị form cập nhật ghế
        public async Task<IActionResult> Update(int id)
        {
            var seat = await _seatRepository.GetByIdAsync(id);
            if (seat == null)
            {
                return NotFound();
            }
            return View(seat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Seat seat)
        {
            if (id != seat.SeatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _seatRepository.UpdateAsync(seat);
                return RedirectToAction(nameof(Index));
            }
            return View(seat);
        }

        // Hiển thị form xác nhận xóa ghế
        public async Task<IActionResult> Delete(int id)
        {
            var seat = await _seatRepository.GetByIdAsync(id);
            if (seat == null)
            {
                return NotFound();
            }
            return View(seat);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _seatRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
