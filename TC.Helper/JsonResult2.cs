using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TC.Helper
{
    // Source: https://code.msdn.microsoft.com/Build-truly-RESTful-API-194a6253
    public class JsonResult2 : ActionResult
    {
        public JsonResult2() { }
        public JsonResult2(object data) { this.Data = data; }

        public string ContentType { get; set; }
        public Encoding ContentEncoding { get; set; }
        public object Data { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpResponseBase response = context.HttpContext.Response;
            if (!string.IsNullOrEmpty(this.ContentType))
                response.ContentType = this.ContentType;
            else
                response.ContentType = "application/json";

            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(this.Data.GetType());
            serializer.WriteObject(response.OutputStream, this.Data);
        }
    }
}
