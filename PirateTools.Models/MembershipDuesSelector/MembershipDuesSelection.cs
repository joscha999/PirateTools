using System;

namespace PirateTools.Models.MembershipDuesSelector;

public class MembershipDuesSelection {
    public Guid Id { get; set; }
    public string MemberFirstName { get; set; } = "";
    public string MemberLastName { get; set; } = "";
    public int MemberId { get; set; }
    public string MemberEMail { get; set; } = "";
    public bool EMailVerified { get; set; }
    public decimal MembershipDuesValue { get; set; } = 120;
    public DateTime? SubmitDate { get; set; }
    public DateTime? ConfirmDate { get; set; }
}