using Microsoft.EntityFrameworkCore;
using BillTracker.Models;
public class BillTrackerContext : DbContext
{
    public BillTrackerContext(DbContextOptions<BillTrackerContext> options) : base(options)
    {
    }
    
}