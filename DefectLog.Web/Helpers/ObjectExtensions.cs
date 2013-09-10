using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DefectLog.Web.Helpers
{
    public static class ObjectExtensions
    {
        public static MvcHtmlString ToJson(this object obj)
        {
            // Property name casing
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;

            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var serializer = JsonSerializer.Create(json.SerializerSettings);
            var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, obj);
            return new MvcHtmlString(stringWriter.ToString());
        }
    }
}