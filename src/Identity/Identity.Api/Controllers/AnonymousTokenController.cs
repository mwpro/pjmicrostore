using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    public class AnonymousTokenController : Controller
    {
        private IdentityServerTools _tools;

        public AnonymousTokenController(IdentityServerTools tools)
        {
            _tools = tools;
        }

        public async Task<IActionResult> MyAction()
        {
            var token = await _tools.IssueClientJwtAsync(
                clientId: "client_id",
                lifetime: 3600,
                audiences: new[] { "backend.api" });

            return Ok(token);
        }
    }
}
