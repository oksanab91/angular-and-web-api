using System;
using System.Collections.Generic;

namespace WebAPIs.AspNetCore.ExistingDb.Models
{
    public partial class Trainee
    {
        public Trainee(int? traineeId = 0, string traineeName = "")
        {
            TraineeTestNav = new HashSet<TraineeTest>();
            TraineeId = traineeId == null ? 0: (int)traineeId;
            TraineeName = traineeName;
        }

        public Trainee()
        {
            TraineeTestNav = new HashSet<TraineeTest>();
            TraineeId = 0;
            TraineeName = "";
        }

        public int TraineeId { get; set; }
        public string TraineeName { get; set; }

        public ICollection<TraineeTest> TraineeTestNav { get; set; }
    }

    public class TraineeShort
    {
        public int TraineeId { get; set; }
        public string TraineeName { get; set; }
    }
}
