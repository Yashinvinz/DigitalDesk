//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OfficeDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class SeatDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SeatDetail()
        {
            this.ManagerToSeatPoolTemps = new HashSet<ManagerToSeatPoolTemp>();
            this.SeatAllocations = new HashSet<SeatAllocation>();
        }
    
        public string SeatNo { get; set; }
        public bool IsAllocated { get; set; }
        public int OfficeId { get; set; }
        public int FloorId { get; set; }
    
        public virtual FloorInfo FloorInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ManagerToSeatPoolTemp> ManagerToSeatPoolTemps { get; set; }
        public virtual Office Office { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SeatAllocation> SeatAllocations { get; set; }
    }
}
