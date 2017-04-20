
using BLL.ViewModels;

namespace MvcUi.Helpers
{
    public static class PagingInfoHelper
    {
        public static PagingInfo PagingInfo(int totalItems, int itemsPerPage, int currentPage)
        {
            PagingInfo pagingInfo = new PagingInfo
            {
                TotalItems = totalItems,
                ItemsPerPage = itemsPerPage,
                CurrentPage = currentPage
            };
            return pagingInfo;
        }
    }
}
