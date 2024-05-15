using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quanlybenxe123.Models;
using Quanlybenxe123.Repository;

namespace Quanlybenxe123.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Authorize]
    public class BusTripAdminController : Controller
    {
        private readonly IBusTripRepository _busTripRepository;
        private readonly IBusRepository _busRepository;
        private readonly IRoutesRepository _routesRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BusTripAdminController(IBusTripRepository busTripRepository, IBusRepository busRepository, IRoutesRepository routesRepository, IWebHostEnvironment webHostEnvironment)
        {
            _busTripRepository = busTripRepository;
            _busRepository = busRepository;
            _routesRepository = routesRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // Hiển thị danh sách chuyến đi
        public async Task<IActionResult> Index()
        {
            var busTrips = await _busTripRepository.GetAllAsync();
            return View(busTrips);
        }

        // Hiển thị form thêm chuyến đi mới
        public async Task<IActionResult> Add()
        {
            var buses = await _busRepository.GetAllAsync();
            var routes = await _routesRepository.GetAllAsync();

            ViewBag.Buses = new SelectList(buses, "BusId", "BusNumber");
            ViewBag.Routes = new SelectList(routes, "RoutesId", "StartLocation", "EndLocation");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(BusTrip busTrip, List<IFormFile> images)
        {
            if (ModelState.IsValid)
            {
                await _busTripRepository.AddAsync(busTrip);

                if (images != null && images.Count > 0)
                {
                    foreach (var image in images)
                    {
                        if (image.Length > 0)
                        {
                            var imagePath = await SaveImage(image);
                            var busTripImage = new BusTripImage
                            {
                                ImageUrl = imagePath,
                                BusTripId = busTrip.BusTripId
                            };
                            busTrip.BusTripImages.Add(busTripImage);
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            var buses = await _busRepository.GetAllAsync();
            var routes = await _routesRepository.GetAllAsync();

            ViewBag.Buses = new SelectList(buses, "BusId", "BusNumber", busTrip.BusId);
            ViewBag.Routes = new SelectList(routes, "RoutesId", $"{nameof(Routes.StartLocation)} - {nameof(Routes.EndLocation)}", busTrip.RouteId);

            return View(busTrip);
        }

        // Lưu hình ảnh vào thư mục wwwroot/images
        private async Task<string> SaveImage(IFormFile image)
        {
            string fileName = $"{Guid.NewGuid()}_{image.FileName}";
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

            using (var fileStream = new FileStream(uploadPath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return $"/images/{fileName}";
        }

        public async Task<IActionResult> Display(int id)
        {
            var busTrip = await _busTripRepository.GetByIdAsync(id);
            if (busTrip == null)
            {
                return NotFound();
            }

            var bus = await _busRepository.GetByIdAsync(busTrip.BusId);
            var route = await _routesRepository.GetByIdAsync(busTrip.RouteId);

            ViewBag.Bus = bus.BusNumber;
            ViewBag.Route = $"{route.StartLocation} - {route.EndLocation}";

            return View(busTrip);
        }

        // Hiển thị form cập nhật chuyến đi
        public async Task<IActionResult> Update(int id)
        {
            var busTrip = await _busTripRepository.GetByIdAsync(id);
            if (busTrip == null)
            {
                return NotFound();
            }

            var buses = await _busRepository.GetAllAsync();
            var routes = await _routesRepository.GetAllAsync();

            ViewBag.Buses = new SelectList(buses, "BusId", "BusNumber", busTrip.BusId);
            ViewBag.Routes = new SelectList(routes, "RoutesId", $"{nameof(Routes.StartLocation)} - {nameof(Routes.EndLocation)}", busTrip.RouteId);

            return View(busTrip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, BusTrip busTrip, List<IFormFile> newImages, List<int> removeImageIds)
        {
            if (id != busTrip.BusTripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingBusTrip = await _busTripRepository.GetByIdAsync(id);

                // Xử lý hình ảnh mới
                if (newImages != null && newImages.Count > 0)
                {
                    foreach (var image in newImages)
                    {
                        if (image.Length > 0)
                        {
                            var imagePath = await SaveImage(image);
                            var busTripImage = new BusTripImage
                            {
                                ImageUrl = imagePath,
                                BusTripId = busTrip.BusTripId
                            };
                            existingBusTrip.BusTripImages.Add(busTripImage);
                        }
                    }
                }

                // Xử lý xóa hình ảnh
                if (removeImageIds != null && removeImageIds.Count > 0)
                {
                    var imagesToRemove = existingBusTrip.BusTripImages.Where(i => removeImageIds.Contains(i.BusTripImageId));
                    _busTripRepository.RemoveImages(imagesToRemove);
                }

                // Cập nhật thông tin chuyến đi
                existingBusTrip.RouteId = busTrip.RouteId;
                existingBusTrip.BusId = busTrip.BusId;
                existingBusTrip.DepartureTime = busTrip.DepartureTime;
                existingBusTrip.DepartureDate = busTrip.DepartureDate;

                await _busTripRepository.UpdateAsync(existingBusTrip);

                return RedirectToAction(nameof(Index));
            }

            var buses = await _busRepository.GetAllAsync();
            var routes = await _routesRepository.GetAllAsync();

            ViewBag.Buses = new SelectList(buses, "BusId", "BusNumber", busTrip.BusId);
            ViewBag.Routes = new SelectList(routes, "RoutesId", $"{nameof(Routes.StartLocation)} - {nameof(Routes.EndLocation)}", busTrip.RouteId);

            return View(busTrip);
        }

        // Hiển thị form xác nhận xóa chuyến đi
        public async Task<IActionResult> Delete(int id)
        {
            var busTrip = await _busTripRepository.GetByIdAsync(id);
            if (busTrip == null)
            {
                return NotFound();
            }

            var bus = await _busRepository.GetByIdAsync(busTrip.BusId);
            var route = await _routesRepository.GetByIdAsync(busTrip.RouteId);

            ViewBag.Bus = bus.BusNumber;
            ViewBag.Route = $"{route.StartLocation} - {route.EndLocation}";

            return View(busTrip);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _busTripRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
