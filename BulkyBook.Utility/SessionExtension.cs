using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Utility
{
    // This is for a generic implementation of session where we can get convert it into an object and store it
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
