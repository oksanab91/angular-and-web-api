using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIs.AspNetCore.ExistingDb.Models
{
    public partial class TraineeTest
    {
        public int TraineeTestId { get; set; }
        public int TestId { get; set; }
        public int TraineeId { get; set; }
        public string TestStatus { get; set; }

        public Test TestNav { get; set; }
        public Trainee TraineeNav { get; set; }
    }

    public class TraineeTestSubject
    {
        public int TraineeTestId { get; set; }
        public string TestDescription { get; set; }
        public string TestStatus { get; set; }
        public string SubjectName { get; set; }
    }

    public class TraineeTestCreateFill
    {
        public TraineeTestCreateFill(IEnumerable<TestShort> tests, IEnumerable<SubjectShort> subjects, string[] statuses)
        {
            Tests = tests;
            Statuses = statuses;
            Subjects = subjects;
        }

        [NotMapped]
        public string[] Statuses { get; set; }
        public IEnumerable<TestShort> Tests { get; set; }
        public IEnumerable<SubjectShort> Subjects { get; set; }
    }

    public class TraineeTestCreate
    {
        public int TraineeTestId { get; set; }
        public int TestId { get; set; }
        public int TraineeId { get; set; }
        public string TestStatus { get; set; }
        public string SubjectCode { get; set; }
    }
    
}
