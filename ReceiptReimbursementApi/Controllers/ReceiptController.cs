using Microsoft.AspNetCore.Mvc;
using ReceiptReimbursementApi.Data;
using ReceiptReimbursementApi.Models;

namespace ReceiptReimbursementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        
        public ReceiptsController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
/*
        [HttpPost]
        public IActionResult PostReceipt([FromBody] object payload)
        {
            Console.WriteLine("Received payload: ");
            Console.WriteLine(payload?.ToString());
            return Ok();
        }
        */
        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromForm] ReceiptFormModel form)
        {
            if (form.ReceiptFile == null || form.ReceiptFile.Length == 0)
                return BadRequest("Receipt file is required.");
            
            var uploadsDir = Path.Combine(_environment.ContentRootPath, "uploads");
            Directory.CreateDirectory(uploadsDir);

            var fileName = Guid.NewGuid() + Path.GetExtension(form.ReceiptFile.FileName);
            var filePath = Path.Combine(uploadsDir, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await form.ReceiptFile.CopyToAsync(stream);
            }

            var receipt = new ReceiptSubmission
            {
                Date = form.Date,
                Amount = form.Amount,
                Description = form.Description,
                FilePath = fileName
            };

            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();

            return Ok("Receipt submitted Successfully.");
        }
    }
    public class ReceiptFormModel
    {
        public DateTime Date {get; set;}
        public decimal Amount {get; set;}
        public string Description {get; set;}
        public IFormFile ReceiptFile {get; set;}
    }
}