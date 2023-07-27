using System;

namespace VendorBoilerplate.Domain.Infrastructures
{
  public class BaseEntity
  {
    public int Id { set; get; }
    public string CreatedBy { set; get; }
    public DateTime CreateAt { set; get; }
    public string UpdateBy { set; get; }
    public DateTime? UpdateAt { set; get; }
  }
}