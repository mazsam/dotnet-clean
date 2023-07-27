using System;
using System.Collections.Generic;

namespace VendorBoilerplate.Application.Models.Query
{
  public class PaginationQuery
  {
    public int PagingPage { set; get; }
    public int PagingLimit { set; get; }
    public string SortColumn { set; get; }
    public string SortType { set; get; }
    public string QuerySearch { set; get; } = string.Empty;
  }
}