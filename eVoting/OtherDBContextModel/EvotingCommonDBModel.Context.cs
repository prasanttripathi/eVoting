﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EvotingCommonDBContext : DbContext
    {
        public EvotingCommonDBContext()
            : base("name=EvotingCommonDBContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<UserComplaints> UserComplaints { get; set; }
        public virtual DbSet<Idea> Ideas { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<CandidateMaster> CandidateMasters { get; set; }
        public virtual DbSet<ElectionCandidate> ElectionCandidates { get; set; }
        public virtual DbSet<ElectionParty> ElectionParties { get; set; }
        public virtual DbSet<EvotingAdmin> EvotingAdmins { get; set; }
        public virtual DbSet<SuperAreaDetail> SuperAreaDetails { get; set; }
        public virtual DbSet<VoteCount> VoteCounts { get; set; }
    }
}
