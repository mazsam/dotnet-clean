using System;
namespace VendorBoilerplate.Application.Exceptions
{
  public class DeleteFailureException : Exception
  {
    public DeleteFailureException(string name, object key, string message) : base($"Delete of entity \"{name}\" ({key}) failed. {message}")
    {

    }
  }
}