using MemberDataEntryForm.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MemberDataEntryForm.Models
{
    public class MemberViewModel
    {
        public MemberData memberData { get; set; }
        public MemberImageModel memberImageModel { get; set; }
        public MembersFamilyData FamilyData { get; set; }
        public MemberBusinessData BusinessData { get; set; }
        public MemberAddressData AddressData { get; set; }
        public MemberProposedData ProposedData { get; set; }

        public MemberViewModel() 
        {
            memberData = new MemberData();
            memberImageModel = new MemberImageModel();
            FamilyData = new MembersFamilyData();
            BusinessData = new MemberBusinessData();
            AddressData = new MemberAddressData();
            ProposedData = new MemberProposedData();
        }

    }
}
