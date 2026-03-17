namespace ImgurWPF.Components.Search
{
    public class SearchParamsDTO
    {
        public string Sort { get; set; }

        public string Window { get; set; }

        public int Page { get; set; }

        public string Query { get; set; }

        public SearchParamsDTO(string sort, string window, int page, string query)
        {
            Sort = sort;
            Window = window;
            Page = page;
            Query = query;
        }
    }
}