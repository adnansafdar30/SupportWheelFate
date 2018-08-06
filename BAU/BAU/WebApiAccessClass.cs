using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace BAU
{
    public class WebApiAccessClass
    {
        public static HttpClient apiclietn = new HttpClient();
        static WebApiAccessClass()
        {
            apiclietn.BaseAddress = new Uri("http://localhost:58024/api/");//this should be cange to the local host which it is deployed
            apiclietn.DefaultRequestHeaders.Clear();
            apiclietn.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}