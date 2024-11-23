using System;

namespace BillTracker.Models;
public class PurchaseOrder
{
    public int Id { get; set; }
    public string? PoNo { get; set; }
    public string? ItemKey { get; set; }
    public string? Description { get; set; } 
    public decimal QtyReceived { get; set; }
    public decimal PoAmount { get; set; }
    public bool IsFullyBilled { get; set; }
}
