using System;

namespace VendorBoilerplate.Application.Exceptions
{
  public class InterfaceException : Exception
  {
    public InterfaceException() {}
    public InterfaceException(string message, Exception ex) : base(message, ex) {}
  }
}