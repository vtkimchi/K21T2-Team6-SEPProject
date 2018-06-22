using System.Collections.Generic;
using BarcodeVer1._0.Models;
using FluentAssertions;

namespace BarcodeVer1._0.UnitTests.Support
{
    public class ReferenceProjectList: Dictionary<string, Member>
    {
        public Member GetById(string memberId)
        {
            return this[memberId.Trim()].Should().NotBeNull()
                                      .And.Subject.Should().BeOfType<Member>().Which;
        }
    }
}
