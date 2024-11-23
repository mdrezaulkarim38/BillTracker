using System;

namespace BillTracker.Models;
public class Challan
{
    public int Id { get; set; }
    public string? ChallanNo { get; set; }
    public DateTime ChallanDate { get; set; } 
    public string? Description { get; set; } 
    public int QtyReceived { get; set; } 
}
