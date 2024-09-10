using ToDoList.Core.Models.ToDoItem;

namespace ToDoList.Core.Models.Pages
{
    public class PagedList
    {
        public PagedList(IQueryable<ToDoItems> queryItems)
            => PageInfo = new PageInfo(queryItems.Count());

        public PageInfo PageInfo { get; private set; }

        public List<ToDoItems> Items { get; private set; }

        public IEnumerator<ToDoItems> GetEnumerator() => Items.GetEnumerator();

        private List<ToDoItems>? GetPage(IQueryable<ToDoItems> items)
            => PageInfo != null
                ? items.Skip((PageInfo.PageNumber - 1) * PageInfo.PageSize).Take(PageInfo.PageSize).ToList()
                : null;

    }
}
