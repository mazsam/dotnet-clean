namespace VendorBoilerplate.Application.Models.Query
{
  public abstract class BaseDtoData
  {
    public int Id { set; get; }
    public DateTime? CreatedAt { set; get; }
    public DateTime? UpdatedAt { set; get; }
  }
}