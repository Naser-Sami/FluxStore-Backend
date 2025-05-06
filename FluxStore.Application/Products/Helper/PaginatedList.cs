namespace FluxStore.Application.Products.Helper
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }

        public PaginatedList(List<T> items, int count)
        {
            Items = items;
            TotalCount = count;
        }
    }
}

