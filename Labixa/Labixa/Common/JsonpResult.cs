using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Labixa.Common
{
    public class JsonpResult : JsonResult
    {
        public JsonpResult(string callbackName)
        {
            CallbackName = callbackName;
        }

        public JsonpResult() : this("jsoncallback")
        {
        }

        public string CallbackName { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            var jsoncallback = (context.RouteData.Values[CallbackName] as string ?? request[CallbackName]) ?? CallbackName;

            if (!string.IsNullOrEmpty(jsoncallback))
            {
                if (string.IsNullOrEmpty(ContentType))
                {
                    ContentType = "application/x-javascript";
                }
                response.Write($"{jsoncallback}(");
            }

            base.ExecuteResult(context);

            if (!string.IsNullOrEmpty(jsoncallback))
            {
                response.Write(")");
            }
        }
    }

    public static class ControllerExtensions
    {
        public static JsonpResult Jsonp(this Controller controller, object data, string callbackName = "callback")
        {
            return new JsonpResult(callbackName)
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public static T DeserializeObject<T>(this Controller controller, string key) where T : class
        {
            var value = controller.HttpContext.Request.QueryString.Get(key);
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            value = value.Replace("Id\":null", "Id\":\"0");
            value = value.Replace("Id\":\"", "Id\":\"0");
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Deserialize<T>(value);
        }
    }
}