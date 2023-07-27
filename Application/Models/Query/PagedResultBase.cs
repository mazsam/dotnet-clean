using System;
using System.Collections.Generic;

namespace VendorBoilerplate.Application.Models.Query
{
  public abstract class PagedResultBase
  {
    public int CurrentPage { set; get; }
    public int PageCount { set; get; }
    public int PageSize { set; get; }
    public int RowCount { set; get; }

    public int FirstRowOnPage
    {
      get { return (CurrentPage - 1) * PageSize + 1; }
    }

    public int LastRowOnPage
    {
      get { return Math.Min(CurrentPage * PageSize, RowCount); }
    }

    // public PaginationData Pagination
    // {
    //   get {
    //     return new PaginationData
    //     {
    //       CurrentPage = this.CurrentPage,
    //       FirstRowOnPage = this.FirstRowOnPage,
    //       LastRowOnPage = this.LastRowOnPage,
    //       PageCount = this.PageCount,
    //       PageSize = this.PageSize,
    //       RowCount = this.RowCount
    //     };
    //   }
    // }
  }
  public class PagedResult<T> : PagedResultBase where T : class
    {
      public IList<T> Data { get; set; }
      public PagedResult()
      {
        Data = new List<T>();
      }
    }
}
