using System.Web;

namespace TaskManager.Errors.Dto
{
     public class ErrorListInput : DataTablesRequest
    {
         public ErrorListInput(HttpRequestBase request) : base(request)
         {
         }

         public ErrorListInput(HttpRequest httpRequest) : base(httpRequest)
         {
         }
    }
}
