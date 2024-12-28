namespace BillTracker.Models;

public class Product
{
    public int Id { get; set; }
    public string? UniqueBillId { get; set; }
    public string? PoNo { get; set; }
    public string? BillNo { get; set; }
    public string? BillDate { get; set; }
    public decimal BillAmount { get; set; }
    public string? Challan { get; set; }
    public string? ChallanDate { get; set; }
    public bool Status { get; set; }
    public int QuantityReceived { get; set; }
    public string? Description { get; set; }
    public int ItemKey { get; set; }
    public string? SubmitDate { get; set; }
    public bool RequestForDeletion { get; set; }
    public string? QrCode { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
}