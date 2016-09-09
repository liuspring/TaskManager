using System.Web;

namespace TaskManager.Logs.Dto
{
    public class LogListInput:DataTablesRequest
    {
        public LogListInput(HttpRequestBase request) : base(request)
        {
        }

        public LogListInput(HttpRequest httpRequest) : base(httpRequest)
        {
        }
    }
}
