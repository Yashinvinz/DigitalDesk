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
    
    public partial class ManagerCabin
    {
        public int CabinId { get; set; }
        public string CabinName { get; set; }
        public string ManagerId { get; set; }
        public bool IsAllocated { get; set; }
        public int OfficeId { get; set; }
        public int FloorId { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual FloorInfo FloorInfo { get; set; }
        public virtual Office Office { get; set; }
    }
}
