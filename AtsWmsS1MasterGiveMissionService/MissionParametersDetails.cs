using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtsWmsS1MasterGiveMissionService
{
    class MissionParametersDetails
    {
        public int missionId { get; set; }
        public int taskType { get; set; }
        public int startColumn { get; set; }
        public int startFloor { get; set; }
        public int startLine { get; set; }
        public int startDepthOfLine { get; set; }
        public int targetColumn { get; set; }
        public int targetFloor { get; set; }
        public int targetLine { get; set; }
        public int targetDepthOfLine { get; set; }
        public string palletCode { get; set; }
    }
}
