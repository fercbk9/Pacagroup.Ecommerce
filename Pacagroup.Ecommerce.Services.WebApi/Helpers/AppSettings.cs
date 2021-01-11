using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pacagroup.Ecommerce.Services.WebApi.Helpers
{
    /// <summary>
    /// Clase helper para settings de login.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// OriginCors
        /// </summary>
        public string OriginCors { get; set; }
        /// <summary>
        /// Secret
        /// </summary>
        public string Secret { get; set; }
        /// <summary>
        /// Issuer
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; }
    }
}
