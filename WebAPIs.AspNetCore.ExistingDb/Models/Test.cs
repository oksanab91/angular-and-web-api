using System;
using System.Collections.Generic;

namespace WebAPIs.AspNetCore.ExistingDb.Models
{
    public partial class Test
    {
        public Test()
        {
            TestSubjectNav = new HashSet<TestSubject>();
            TraineeTestNav = new HashSet<TraineeTest>();
        }

        public int TestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<TestSubject> TestSubjectNav { get; set; }
        public ICollection<TraineeTest> TraineeTestNav { get; set; }
    }

    public class TestShort
    {
        public int TestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
