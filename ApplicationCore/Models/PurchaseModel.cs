namespace ApplicationCore.Models;

public class PurchaseModel
{
    public System.Guid PurchaseNumber { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PurchaseDateTime { get; set; }
    public MovieCardModel Movie { get; set; }
    
}