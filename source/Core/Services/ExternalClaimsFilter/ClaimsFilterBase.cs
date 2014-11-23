﻿/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Thinktecture.IdentityServer.Core.Configuration.Hosting;
using Thinktecture.IdentityServer.Core.Models;

namespace Thinktecture.IdentityServer.Core.Services.Default
{
    public abstract class ClaimsFilterBase : IExternalClaimsFilter
    {
        readonly string provider;

        public ClaimsFilterBase(string provider)
        {
            this.provider = provider;
        }

        public IEnumerable<Claim> Filter(string provider, IEnumerable<Claim> claims)
        {
            if (this.provider == provider)
            {
                claims = TransformClaims(claims);
            }

            return claims;
        }

        protected abstract IEnumerable<Claim> TransformClaims(IEnumerable<Claim> claims);
    }
}
