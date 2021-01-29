using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JAD.Heroku.SDK.Extensions
{
    public class HerokuExtensions
    {
        public static StringContent CreateCamelCaseStringContent<T>(T entity)
        {
            return new StringContent(
                JsonSerializer.Serialize(entity, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }),
                Encoding.UTF8,
                "application/json"
            );
        }
        public static StringContent CreateStringContent<T>(T entity)
        {
            return new StringContent(
                JsonSerializer.Serialize(entity),
                Encoding.UTF8,
                "application/json"
            );
        }
    }
}
