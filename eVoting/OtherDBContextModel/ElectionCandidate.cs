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
    
    public partial class ElectionCandidate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ElectionCandidate()
        {
            this.VoteCounts = new HashSet<VoteCount>();
        }
    
        public int CandidateNo { get; set; }
        public Nullable<int> CandidateId { get; set; }
        public Nullable<int> PartyId { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public byte[] Symbol { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual CandidateMaster CandidateMaster { get; set; }
        public virtual ElectionParty ElectionParty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VoteCount> VoteCounts { get; set; }
    }
}
