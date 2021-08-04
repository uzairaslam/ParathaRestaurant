using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Paratha.Web.SD;

namespace Paratha.Web.Models
{
    public class ApiRequest
    {
        public APIType ApiType { get; set; } = APIType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
