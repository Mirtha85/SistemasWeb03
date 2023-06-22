using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemasWeb01.Enums;
using SistemasWeb01.Helpers;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Implementations;
using SistemasWeb01.Repository.Interfaces;
using SistemasWeb01.ViewModels;
using System.Data;

namespace SistemasWeb01.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICombosHelper _combosHelper;
        private readonly IFormFileHelper _formFileHelper;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;

        public UserController(IUserRepository userRepository, ICombosHelper combosHelper, IFormFileHelper formFileHelper, ICountryRepository countryRepository, IStateRepository stateRepository)
        {
            _userRepository = userRepository;
            _combosHelper = combosHelper;
            _formFileHelper = formFileHelper;
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<User> users = _userRepository.AllUsers;
            return View(users);
        }

        public async Task<IActionResult> Create()
        {
            AddUserViewModel model = new AddUserViewModel
            {
                Id = Guid.Empty.ToString(),
                Countries = await _combosHelper.GetComboCountriesAsync(),
                States = await _combosHelper.GetComboStatesAsync(0),
                Cities = await _combosHelper.GetComboCitiesAsync(0),
                UserType = UserType.Admin
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string ImageName = string.Empty;

                if (model.ImageFile != null)
                {
                    ImageName = await _formFileHelper.UploadFile(model.ImageFile);
                }
                model.ImageName = ImageName;
                User user = await _userRepository.AddUserAsync(model);
                await _userRepository.AddUserToRole(user, "Admin");
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");
                    return View(model);
                }
                return RedirectToAction("Index", "User");
            }

            return View(model);
        }

        public JsonResult GetStates(int countryId)
        {
            Country? country = _countryRepository.GetCountryById(countryId);
            if (country == null)
            {
                return null;
            }

            return Json(country.States.OrderBy(d => d.Name));
        }

        public JsonResult GetCities(int stateId)
        {
            State? state = _stateRepository.GetStateById(stateId);
            if (state == null)
            {
                return null;
            }

            return Json(state.Cities.OrderBy(c => c.Name));
        }
    }
}
