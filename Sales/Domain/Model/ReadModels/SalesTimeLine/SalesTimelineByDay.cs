namespace Sales.Domain.Model.ReadModels.SalesTimeLine;

public class SalesTimelineByDay
{
    public DateOnly Date { get; set; }

    public List<SellerBlock> Blocks { get; set; } = [];
}
