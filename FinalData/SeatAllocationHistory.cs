//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalData
{
    using System;
    using System.Collections.Generic;
    
    public partial class SeatAllocationHistory
    {
        public string Reason { get; set; }
        public System.DateTime SDate { get; set; }
        public System.DateTime EDate { get; set; }
        public int SeatId { get; set; }
        public int FloorId { get; set; }
        public int OfficeId { get; set; }
        public string AllocatedEmpId { get; set; }
        public string AllocatedManagerId { get; set; }
        public int HistoryId { get; set; }
    }
}
