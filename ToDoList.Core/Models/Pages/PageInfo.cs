namespace ToDoList.Core.Models.Pages
{
    public class PageInfo
    {
        public const int NumberStartPage = 1;

        public const int StartPageSize = 15;

        public PageInfo(int totalItems)
            : this(totalItems, NumberStartPage, StartPageSize)
        {
        }

        public PageInfo(int totalItems, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
        }

        public int PageNumber { get; private set; } = NumberStartPage;

        public int PageSize { get; private set; } = StartPageSize;

        public int TotalItems { get; private set; }

        public int TotalPage
            => (int)Math.Ceiling((decimal)(TotalItems / PageSize));
    }
}
