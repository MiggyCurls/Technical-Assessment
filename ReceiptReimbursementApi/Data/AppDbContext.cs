using Microsoft.EntityFrameworkCore;
using ReceiptReimbursementApi.Models;

namespace ReceiptReimbursementApi.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<ReceiptSubmission> Receipts {get; set;}
    }
}