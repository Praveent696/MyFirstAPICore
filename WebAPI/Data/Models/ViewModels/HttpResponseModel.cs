﻿using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WebAPI.Data.Models.ViewModels
{
    /// <summary>
    /// This response message for API call
    /// </summary>
    /// 
    public class HttpResponseModel
    {
        /// <summary>
        /// Boolean value for API call status , true or false
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Data is dynamic object here you can expect API data
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Count is number of entities returned form API
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// Jwt Token of login user
        /// </summary>
        /// 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string JwtToken { get; set; }

        public string Message { get; set; }

    }
}
