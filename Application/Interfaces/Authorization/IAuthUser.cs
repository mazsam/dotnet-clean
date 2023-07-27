namespace VendorBoilerplate.Application.Interfaces.Authorization
{
  public interface IAuthUser
  {
    public int UserId { set; get; }
    public string FullName { set; get; }
    public string Email { set; get; }
    public string? VendorCode { set; get; }
  }
}