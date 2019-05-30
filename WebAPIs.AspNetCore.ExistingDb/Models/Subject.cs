using System;
using System.Collections.Generic;

namespace WebAPIs.AspNetCore.ExistingDb.Models
{
    public partial class Subject
    {
        public Subject()
        {
            TestSubjectNav = new HashSet<TestSubject>();
        }

        public string SubjectCode { get; set; }
        public string Name { get; set; }

        public ICollection<TestSubject> TestSubjectNav { get; set; }
    }

    public class SubjectShort
    {
        public string SubjectCode { get; set; }
        public string Name { get; set; }
    }
}
