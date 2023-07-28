namespace VendorBoilerplate.Application.Models.Query
{
  public class BaseQueryCommand
  {
    public int UserId { set; get; }
    public string FullName { set; get; } = string.Empty;
    public string Email { set; get; } = string.Empty;
    public string VendorCode { set; get; } = string.Empty;
  }
}