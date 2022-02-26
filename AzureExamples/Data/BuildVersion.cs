using System;
using System.Collections.Generic;

namespace AzureExamples.Data
{
    public partial class BuildVersion
    {
        public byte SystemInformationId { get; set; }
        public string DatabaseVersion { get; set; } = null!;
        public DateTime VersionDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
