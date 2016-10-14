using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kendo.Mvc.UI;

namespace mhg_internalnet2.ViewModel
{
    public class DependencyViewModel : IGanttDependency
    {
        public int DependencyId { get; set; }

        public int PredecessorId { get; set; }
        public int SuccessorId { get; set; }
        public DependencyType Type { get; set; }
    }
}