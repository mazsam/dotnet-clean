namespace VendorBoilerplate.Application.Models.Query
{
  public class BaseQueryCommand
  {
    public int UserId { set; get; }
    public string FullName { set; get; }
    public string Email { set; get; }
    public string? VendorCode { set; get; }
  }
}