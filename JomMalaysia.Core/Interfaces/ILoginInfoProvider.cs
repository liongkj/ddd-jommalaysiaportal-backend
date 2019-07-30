using JomMalaysia.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Interfaces
{
    public interface ILoginInfoProvider
    {
        /// <summary>
        /// Gets the information of the current user.
        /// </summary>
        /// <value>
        /// <see cref="LoginInfo"/> property that stores the basic information of the current authenticated user.
        /// Returns <c>null</c> if the user is not authenticated.
        /// </value>
        LoginInfo GetLoginInfo();
    }
}
