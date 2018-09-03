using Newtonsoft.Json;
using System;

namespace ASPNETCore.ExceptionHandling.Models
{
    public class ErrorDetail
    {
        public ErrorDetail(Exception exception)
        {
            Target = $"{ exception.TargetSite.ReflectedType.FullName }.{exception.TargetSite.Name}";
            Message = exception.Message;

        }

        public string Target { get; set; }
        public int StatusCode { get; set; }

        public String Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
