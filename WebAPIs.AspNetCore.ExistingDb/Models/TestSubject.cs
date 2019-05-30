using System;
using System.Collections.Generic;

namespace WebAPIs.AspNetCore.ExistingDb.Models
{
    public partial class TestSubject
    {
        public int TestSubjectId { get; set; }
        public int TestId { get; set; }
        public string SubjectCode { get; set; }

        public Subject SubjectNav { get; set; }
        public Test TestNav { get; set; }
    }
}
