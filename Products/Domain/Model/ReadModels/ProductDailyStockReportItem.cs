namespace Products.Domain.Model.ReadModels;

public class ProductDailyStockReportItem
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public int InitialStock { get; set; }

    public int Entries { get; set; }

    public int Sold { get; set; }

    public int FinalStock { get; set; }
}