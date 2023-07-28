using System;
using System.Collections.Generic;

namespace VendorBoilerplate.Application.Models.Query
{
  public class PaginationDto : BaseDto
  {
    public PaginationData? Pagination { set; get; }
  }

  public class PaginationData
  {
    public int CurrentPage { set; get; }
    public int PageCount { set; get; }
    public int PageSize { set; get; }
    public int RowCount { set; get; }
    public int FirstRowOnPage { set; get; }
    public int LastRowOnPage { set; get; }
  }
}