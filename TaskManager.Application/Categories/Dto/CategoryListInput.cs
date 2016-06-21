using System.Web;

namespace TaskManager.Categories.Dto
{
    public class CategoryListInput : DataTablesRequest
    {
        public CategoryListInput(HttpRequestBase request) : base(request)
        {
        }

        public CategoryListInput(HttpRequest httpRequest) : base(httpRequest)
        {
        }

        public string CategoryName { get; set; }
    }
}
