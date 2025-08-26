using ToDoList.Core.Models.ToDoItem;

namespace ToDoList.Core.Models.Pages
{
    public class PagedList(IQueryable<ToDoItems> queryItems)
    {
        public PageInfo PageInfo { get; private set; } = new PageInfo(queryItems.Count());

        public List<ToDoItems> Items { get; private set; }

        public IEnumerator<ToDoItems> GetEnumerator() => Items.GetEnumerator();

        private List<ToDoItems>? GetPage(IQueryable<ToDoItems> items)
            => PageInfo != null
                ? items.Skip((PageInfo.PageNumber - 1) * PageInfo.PageSize).Take(PageInfo.PageSize).ToList()
                : null;

    }
}
