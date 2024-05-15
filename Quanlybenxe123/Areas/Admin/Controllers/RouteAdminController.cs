using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quanlybenxe123.Models;
using Quanlybenxe123.Repository;

namespace Quanlybenxe123.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize]
    public class RouteAdminController : Controller
    {
        private readonly IRoutesRepository _routesRepository;

        public RouteAdminController(IRoutesRepository routesRepository)
        {
            _routesRepository = routesRepository;
        }

        // Hiển thị danh sách tuyến đường
        public async Task<IActionResult> Index()
        {
            var routes = await _routesRepository.GetAllAsync();
            return View(routes);
        }

        // Hiển thị form thêm tuyến đường mới
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Routes route)
        {
            if (ModelState.IsValid)
            {
                await _routesRepository.AddAsync(route);
                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }

        // Hiển thị thông tin chi tiết tuyến đường
        public async Task<IActionResult> Display(int id)
        {
            var route = await _routesRepository.GetByIdAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            return View(route);
        }

        // Hiển thị form cập nhật tuyến đường
        public async Task<IActionResult> Update(int id)
        {
            var route = await _routesRepository.GetByIdAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            return View(route);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Routes route)
        {
            if (id != route.RoutesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _routesRepository.UpdateAsync(route);
                return RedirectToAction(nameof(Index));
            }
            return View(route);
        }

        // Hiển thị form xác nhận xóa tuyến đường
        public async Task<IActionResult> Delete(int id)
        {
            var route = await _routesRepository.GetByIdAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            return View(route);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _routesRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        //}
    }
}
