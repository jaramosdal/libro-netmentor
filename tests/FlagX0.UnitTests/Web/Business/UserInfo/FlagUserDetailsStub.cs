using FlagX0.Web.Business.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlagX0.UnitTests.Web.Business.UserInfo;

internal class FlagUserDetailsStub : IFlagUserDetails
{
    public string UserId => "1";
}
