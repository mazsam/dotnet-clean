using System;
namespace VendorBoilerplate.Application.Exceptions
{
  public class NotFoundException : Exception
  {
    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) wa not found.")
    {

    }
  }
}