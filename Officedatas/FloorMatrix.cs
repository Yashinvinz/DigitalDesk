//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Officedatas
{
    using System;
    using System.Collections.Generic;
    
    public partial class FloorMatrix
    {
        public int SectionId { get; set; }
        public int FloorId { get; set; }
        public int OfficeId { get; set; }
        public string SectionName { get; set; }
        public int RangeFrom { get; set; }
        public int RangeEnd { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Order { get; set; }
        public int Count { get; set; }
    
        public virtual FloorInfo FloorInfo { get; set; }
        public virtual Office Office { get; set; }
    }
}
