using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Data.Models
{
    /// <summary>
    /// This response message for API call
    /// </summary>
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

    }
}
