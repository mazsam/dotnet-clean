using System;

namespace VendorBoilerplate.Domain.Infrastructures
{
  public class BaseEntity
  {
    public int Id { set; get; }
    public string CreatedBy { set; get; }
    public DateTime CreatedAt { set; get; }
    public string UpdatedBy { set; get; }
    public DateTime? UpdatedAt { set; get; }
    public int RowStatus { set; get; }
  }
}