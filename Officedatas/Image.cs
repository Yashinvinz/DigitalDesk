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
    
    public partial class Image
    {
        public int OfficeId { get; set; }
        public string ImageURL { get; set; }
    
        public virtual Office Office { get; set; }
    }
}
