using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.ASPCore.Clients;
using RetroArcade.ASPCore.Models.RetroArcade;

namespace RetroArcade.ASPCore.Controllers.RetroArcade
{
    public class RoomController : Controller
    {
        private readonly RetroArcadeAPIClient _agency;

        public RoomController(RetroArcadeAPIClient agency)
        {
            _agency = agency;
        }

        // GET: RoomController
        [HttpGet]
        public async Task<ActionResult> Index(Guid buildingId)
        {
            try
            {
                BuildingViewModel building = await _agency.GetBuildingByIdAsync(buildingId);
                if (building == null)
                {
                    return NotFound();
                }

                List<RoomViewModel> rooms = 
                    (List<RoomViewModel>)await _agency.GetAllRoomsByBuildingAsync(buildingId);

                return View(rooms);

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: RoomController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoomController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoomController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
