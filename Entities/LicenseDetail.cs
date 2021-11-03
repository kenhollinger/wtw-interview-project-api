using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wtw_interview_project_api.Entities
{
    public class LicenseDetail
    {
        public int LicenseId { get; set; }
        public string LicenseName { get; set; }
        public bool IsRequired { get; set; }
        public bool Selected { get; set; }
    }
}
