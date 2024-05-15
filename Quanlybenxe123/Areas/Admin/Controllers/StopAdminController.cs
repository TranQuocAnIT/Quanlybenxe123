using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quanlybenxe123.Models;
using Quanlybenxe123.Repository;

namespace Quanlybenxe123.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize]
    public class StopAdminController : Controller
    {
        private readonly IStopRepository _stopRepository;
        private readonly IBusTripRepository _busTripRepository;

        public StopAdminController(IStopRepository stopRepository, IBusTripRepository busTripRepository)
        {
            _stopRepository = stopRepository;
            _busTripRepository = busTripRepository;
        }

        // Hiển thị danh sách các điểm dừng
        public async Task<IActionResult> Index()
        {
            var stops = await _stopRepository.GetAllAsync();
            return View(stops);
        }

        // Hiển thị form thêm điểm dừng mới
        public async Task<IActionResult> Add()
        {
            var busTrips = await _busTripRepository.GetAllAsync();
            ViewBag.BusTrips = new SelectList(busTrips, "BusTripId", "DepartureTime");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Stop stop, int busTripId)
        {
            if (ModelState.IsValid)
            {
                var busTrip = await _busTripRepository.GetByIdAsync(busTripId);
                stop.BusTrip = busTrip;

                await _stopRepository.AddAsync(stop);
                return RedirectToAction(nameof(Index));
            }
            return View(stop);
        }

        // Hiển thị thông tin chi tiết điểm dừng
        public async Task<IActionResult> Display(int id)
        {
            var stop = await _stopRepository.GetByIdAsync(id);
            if (stop == null)
            {
                return NotFound();
            }
            return View(stop);
        }

        // Hiển thị form cập nhật điểm dừng
        public async Task<IActionResult> Update(int id)
        {
            var stop = await _stopRepository.GetByIdAsync(id);
            if (stop == null)
            {
                return NotFound();
            }

            var busTrips = await _busTripRepository.GetAllAsync();
            ViewBag.BusTrips = new SelectList(busTrips, "BusTripId", "DepartureTime", stop.BusTripId);

            return View(stop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Stop stop, int busTripId)
        {
            if (id != stop.StopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var busTrip = await _busTripRepository.GetByIdAsync(busTripId);
                stop.BusTrip = busTrip;

                await _stopRepository.UpdateAsync(stop);
                return RedirectToAction(nameof(Index));
            }
            return View(stop);
        }

        // Hiển thị form xác nhận xóa điểm dừng
        public async Task<IActionResult> Delete(int id)
        {
            var stop = await _stopRepository.GetByIdAsync(id);
            if (stop == null)
            {
                return NotFound();
            }
            return View(stop);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _stopRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
