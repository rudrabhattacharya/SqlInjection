using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace SqlInjectionActionFilter.Utility
{
    public static class ParameterSynthesizer
    {
        public static object Synthesize(Object param)
        {
            if (param != null)
            {
                if (param.GetType().Equals(typeof(string)))
                {
                    return new SQLEncoderLibrary.Encoder().EncodeForSql(param.ToString());
                }
                else if (param.GetType().IsClass && !param.GetType().IsPrimitive)
                {
                    PropertyInfo[] properties = param.GetType().GetProperties();
                    foreach (PropertyInfo pi in properties)
                    {
                        Type propType = pi.PropertyType;
                        if (pi.PropertyType.Equals(typeof(string)))
                        {
                            pi.SetValue(param, new SQLEncoderLibrary.Encoder().EncodeForSql(pi.GetValue(param, null).ToString()), null);
                        }
                        else if (pi.PropertyType.IsClass && !pi.PropertyType.IsPrimitive)
                        {
                            if (typeof(IEnumerable).IsAssignableFrom(pi.PropertyType))
                            {
                                IEnumerable collection = (IEnumerable)pi.GetValue(param, null);
                                IList list = (IList)Activator.CreateInstance(pi.PropertyType);
                                if (collection != null && list != null)
                                {
                                    foreach (var item in collection)
                                    {
                                        var result = Synthesize(item);
                                        list.Add(result);
                                    }
                                    collection = list;
                                    pi.SetValue(param,collection, null);
                                }
                            }
                            else if (pi.GetValue(param, null) == null)
                            {
                                //do nothing, this condition is for a complex type. ideally this should not be hit.
                            }
                            else
                            {
                                // complex type
                                var result =Synthesize(pi.GetValue(param, null));
                                pi.SetValue(param, result);
                            }
                        }
                    }
                }
            }
            return param;
        }
    }
}
