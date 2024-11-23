using System;

namespace BillTracker.Models;
public class Bill
{
    public int Id { get; set; }
    public string? UniqueBillId { get; set; }
    public string? PoNo { get; set; } 
    public string? BillNo { get; set; }
    public DateTime BillDate { get; set; }
    public string? ChallanNo { get; set; }
    public decimal BillAmount { get; set; }
    public DateTime SubmitDate { get; set; } = DateTime.UtcNow;
    public string? CurrentStatus { get; set; }
    public string? QRCodeData { get; set; }
    public int UserId { get; set; }
}

