using CustMgmSys.Abstract;
using CustMgmSys.Data;
using CustMgmSys.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CustMgmSys.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly CMSDbContext _context;
        private readonly IWebHostEnvironment _env;

        private readonly IEmailService _emailService;
        private readonly EmailSettings _emailSettings;
        public CustomersController(CMSDbContext context, IWebHostEnvironment env, IEmailService emailService, IOptions<EmailSettings> emailSettings)
        {
            _context = context;
            _env = env;
            _emailService = emailService;
            _emailSettings = emailSettings.Value;
        }
           
        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PhoneNumber,Email,DateOfBirth")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        public IActionResult SendEmail()
        {
            return View(new EmailViewModel());
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendEmail(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var attachment = new EmailAttachment
                    {
                        Data = await GetBytesFromFormFile(model.Attachment),
                        FileName = model.Attachment.FileName,
                        ContentType = model.Attachment.ContentType
                    };

                    await _emailService.SendEmailAsync(_emailSettings.EmailFrom, model.To, model.Subject, model.Body, attachment);

                    return RedirectToAction(nameof(Index), new { message = "Email sent successfully." });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error sending email: {ex.Message}");
                }
            }

            return View(model);
        }

        private async Task<byte[]> GetBytesFromFormFile(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }

}
