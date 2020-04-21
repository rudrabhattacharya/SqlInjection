using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;
using WebFormsClient.Utility;

namespace WebFormsClient
{
    public class SqlInjectionHttpModule : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += SanitizeInput;
        }

        private void SanitizeInput(object sender, EventArgs e)
        {
            HttpRequest request = ((HttpApplication)sender).Request;
            if (request.QueryString.Count > 0)
            {
                SanitizeInput(request.QueryString);
            }
            if (request.HttpMethod == "POST")
            {
                if (request.Form.Count > 0)
                {
                    SanitizeInput(request.Form);
                }
            }
        }

        private static void SanitizeInput(NameValueCollection collection)
        {
            // Both the form and query string collections are read-only by 
            // default, so use Reflection to make them writable:
            PropertyInfo readonlyProperty = collection.GetType()
                .GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

            readonlyProperty.SetValue(collection, false, null);

            for (int i = 0; i < collection.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(collection[i]))
                {
                    continue;
                }
                collection[collection.Keys[i]] = ParameterSynthesizer.Synthesize(collection[collection.Keys[i]]);
            }
            readonlyProperty.SetValue(collection, true, null);
        }
    }
}