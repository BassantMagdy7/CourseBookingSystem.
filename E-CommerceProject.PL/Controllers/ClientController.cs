using AutoMapper;
using CourseBookingSystem.BLL.Interfaces;
using CourseBookingSystem.BLL.Repositories;
using CourseBookingSystem.BLL.UnitOfWork;
using CourseBookingSystem.DAL.Models;
using CourseBookingSystem.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseBookingSystem.PL.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public ClientController(IUnitOfWork unitOfWork, ICourseRepository courseRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        // Handle GET requests to show the form
        [HttpGet] 
        public async Task<IActionResult> Create()
        {
            // Retrieve the list of services from the database asynchronously
            var services = await _courseRepository.GetAllServices();
            // Convert the list of services to SelectListItems
            var serviceItems = services.Select(s => new SelectListItem
            {
                Value = s.ServiceId.ToString(),
                Text = s.ServiceName
            }).ToList();
            // Pass the list of service items to the view
            ViewBag.Services = serviceItems;
            return View();
        }

        // Handle POST requests to add a client
        [HttpPost]
        public async Task<IActionResult> Create(ClientFormViewModel formData)
        {
            if (ModelState.IsValid)
            {
                // Map ClientFormViewModel to Client
                var client = _mapper.Map<Client>(formData);
                // Add the client to the database
                _unitOfWork.ClientRepository.Add(client);
                await _unitOfWork.SaveAsync(); // Await the SaveAsync method
                // Map ServiceId and SomeServiceDetails to ClientRequest
                var clientRequest = _mapper.Map<ClientRequest>(formData);
                clientRequest.ClientId = client.ClientId;
                // Add the client request to the database
                await _unitOfWork.ClientRequests.AddAsync(clientRequest);
                await _unitOfWork.SaveAsync(); 
                return RedirectToAction("ClientRequests", new { clientId = client.ClientId });
            }
            // Return to the form with validation errors if ModelState is not valid
            return View(formData);
        }

        //Get Client Request
        [HttpGet]
        public async Task<IActionResult> ClientRequests(int clientId)
        {
            // Retrieve client requests from the database for the specified client ID
            var clientRequests = await _unitOfWork.ClientRequests.GetByClientIdAsync(clientId);
            // Pass the client requests to the view
            return View(clientRequests);
        }
        //Get Edit Form
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve the client request from the database based on the provided ID
            var clientRequest = await _unitOfWork.ClientRequests.GetByIdUpdateAsync(id);
            if (clientRequest == null)
            {
                return NotFound();
            }
            // Ensure that the Client and Service properties are loaded
            await _unitOfWork.ClientRequests.LoadClientAsync(clientRequest);
            await _unitOfWork.ClientRequests.LoadServiceAsync(clientRequest);
            // Check for null references before mapping
            if (clientRequest.Client == null || clientRequest.Service == null)
            {
                return NotFound();
            }
            // Map the ClientRequest data to a ClientFormViewModel
            var clientFormViewModel = _mapper.Map<ClientFormViewModel>(clientRequest);
            // Retrieve the list of services from the database asynchronously
            var services = await _courseRepository.GetAllServices();
            // Convert the list of services to SelectListItems
            var serviceItems = services.Select(s => new SelectListItem
            {
                Value = s.ServiceId.ToString(),
                Text = s.ServiceName,
                Selected = s.ServiceId == clientRequest.ServiceId // Select the current service
            }).ToList();
            // Pass the list of service items to the view
            ViewBag.Services = serviceItems;
            // Pass the clientFormViewModel to the view for editing
            return View(clientFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientFormViewModel clientFormViewModel)
        {
            if (id != clientFormViewModel.Id)
            {
                return BadRequest(); // Return 400 Bad Request if IDs don't match
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var client = _mapper.Map<Client>(clientFormViewModel);
                    var clientRequest = _mapper.Map<ClientRequest>(clientFormViewModel);
                    // Retrieve the existing client request from the database
                    var existingClientRequest = await _unitOfWork.ClientRequests.GetByIdAsync(id);
                    if (existingClientRequest == null)
                    {
                        return NotFound(); //if the client request is not found
                    }
                    // Update the existing client request with mapped properties
                    existingClientRequest.Client = client;
                    existingClientRequest.ServiceId = clientRequest.ServiceId;
                    existingClientRequest.SomeServiceDetails = clientRequest.SomeServiceDetails;
                    // Update the client request in the database
                    await _unitOfWork.ClientRequests.UpdateAsync(existingClientRequest);
                    clientRequest.ClientId = existingClientRequest.ClientId;
                    await _unitOfWork.SaveAsync();
                    return RedirectToAction(nameof(ClientRequests), new { clientId = clientRequest.ClientId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientRequestExists(id))
                    {
                        return NotFound(); // Return 404 Not Found if the client request is not found
                    }
                    else
                    {
                        throw; 
                    }
                }
            }
            // if the model is not valid Pass the clientFormViewModel back to the view
            return View(clientFormViewModel);
        }

       // Check if a client request with the given ID exists in the database
        private bool ClientRequestExists(int id)
        {
            return _unitOfWork.ClientRequests.GetByIdAsync(id) != null;
        }

        //Delete Client Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // Retrieve the client request from the database
            var clientRequest = await _unitOfWork.ClientRequests.GetByIdAsync(id);
            if (clientRequest == null)
            {
                return NotFound(); // Return 404 Not Found if the client request is not found
            }
            try
            {
                // Delete the client request from the database
                await _unitOfWork.ClientRequests.RemoveAsync(clientRequest);
                await _unitOfWork.SaveAsync(); // Save changes
                return RedirectToAction(nameof(ClientRequests));
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during deletion
                // Log the exception, display an error message
                return RedirectToAction(nameof(Error));
            }
        }

        //Return Course Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var (service, serviceDetails) = await _courseRepository.GetDetailsById(id);
            if (service == null)
            {
                return NotFound();
            }
            var model = (service, serviceDetails);
            return View(model);
        }
    }
}
