
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quanlybenxe123.Models;
using Quanlybenxe123.Repository;

namespace Quanlybenxe123.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
 
    public class BusAdminController : Controller
    {
        private readonly IBusRepository _busRepository;

        public BusAdminController(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        // Hiển thị danh sách xe bus
        public async Task<IActionResult> Index()
        {
            var buses = await _busRepository.GetAllAsync();
            return View(buses);
        }

        // Hiển thị form thêm xe bus mới
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Bus bus)
        {
            if (ModelState.IsValid)
            {
                await _busRepository.AddAsync(bus);
                return RedirectToAction(nameof(Index));
            }
            return View(bus);
        }

        // Hiển thị thông tin chi tiết xe bus
        public async Task<IActionResult> Display(int id)
        {
            var bus = await _busRepository.GetByIdAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            return View(bus);
        }

        // Hiển thị form cập nhật xe bus
        public async Task<IActionResult> Update(int id)
        {
            var bus = await _busRepository.GetByIdAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            return View(bus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Bus bus)
        {
            if (id != bus.BusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _busRepository.UpdateAsync(bus);
                return RedirectToAction(nameof(Index));
            }
            return View(bus);
        }

        // Hiển thị form xác nhận xóa xe bus
        public async Task<IActionResult> Delete(int id)
        {
            var bus = await _busRepository.GetByIdAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            return View(bus);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _busRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}