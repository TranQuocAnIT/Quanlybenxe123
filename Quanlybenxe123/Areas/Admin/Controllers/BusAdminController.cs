using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quanlybenxe123.Models;
using Quanlybenxe123.Repository;
using System.Threading.Tasks;

namespace Quanlybenxe123.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BusAdminController : Controller
    {
        private readonly IBusRepository _busRepository;

        public BusAdminController(IBusRepository busRepository)
        {
            _busRepository = busRepository;
        }

        public async Task<IActionResult> Index()
        {
            var buses = await _busRepository.GetAllAsync();
            return View(buses);
        }

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

        public async Task<IActionResult> Display(int id)
        {
            var bus = await _busRepository.GetByIdAsync(id);
            if (bus == null)
            {
                return NotFound();
            }
            return View(bus);
        }

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
