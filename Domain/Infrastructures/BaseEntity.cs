using System;

namespace VendorBoilerplate.Domain.Infrastructures
{
  public class BaseEntity
  {
    public int Id { set; get; }
    public string CreatedBy { set; get; } = string.Empty;
    public DateTime CreatedAt { set; get; }
    public string UpdatedBy { set; get; }  = string.Empty;
    public DateTime UpdatedAt { set; get; }
    public int RowStatus { set; get; }
  }
}