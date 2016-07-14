using System.IO;
using Newtonsoft.Json.Linq;

namespace TaskManager.HubService.Model
{
    public class ClientResult
    {
        private string _resString = string.Empty;

        public int Code { get; set; }

        public string Msg { get; set; }

        public JObject RepObject { get; set; }

        public string ResponseContentType { get; set; }

        public Stream ResponseStream { get; set; }

        public string ResString
        {
            get
            {
                if (string.IsNullOrEmpty(this._resString) && !string.IsNullOrEmpty(this.Msg))
                {
                    return this.Msg;
                }
                return this._resString;
            }
            set
            {
                this._resString = value;
            }
        }

        public int Statuscode { get; set; }

        public bool Success
        {
            get
            {
                return (this.Code > 0);
            }
        }

        public long Total { get; set; }
    }
}
