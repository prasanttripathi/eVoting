//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eVoting.OtherDBContextModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class EvotingAdmin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EvotingAdmin()
        {
            this.SuperAreaDetails = new HashSet<SuperAreaDetail>();
            this.SuperAreaDetails1 = new HashSet<SuperAreaDetail>();
        }
    
        public string VoterID { get; set; }
        public int AdminTypeID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuperAreaDetail> SuperAreaDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuperAreaDetail> SuperAreaDetails1 { get; set; }
    }
}