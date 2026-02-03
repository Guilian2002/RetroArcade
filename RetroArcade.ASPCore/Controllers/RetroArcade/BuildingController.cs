using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroArcade.ASPCore.Clients;
using RetroArcade.ASPCore.Models.RetroArcade;

namespace RetroArcade.ASPCore.Controllers.RetroArcade
{
    public class BuildingController : Controller
    {
        private readonly RetroArcadeAPIClient _agency;

        public BuildingController(RetroArcadeAPIClient agency)
        {
            _agency = agency;
        }

        // GET: BuildingController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            try
            {
                List<BuildingViewModel> films = (List<BuildingViewModel>)await _agency.GetAllBuildingsAsync();
                return View(films);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: BuildingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BuildingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BuildingController/Create
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

        // GET: BuildingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BuildingController/Edit/5
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

        // GET: BuildingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BuildingController/Delete/5
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
