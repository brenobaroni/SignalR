using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Core;
using LinkGamer.Controllers.Base;
using LinkGamer.Domain.Entities;
using LinkGamer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LinkGamer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : LinkGamerControllerBase
    {
        private readonly IUserCore _core;

        public UserController(
                    IUserCore core,
                    IConfiguration configuration) : base(configuration)
        {
            _core = core;
        }

        internal override void CreateCoreConnections()
        {
            _core.CreateConnection(_Server);
        }

        // GET: api/<UserController>
        [HttpGet]
        [Authorize]
        [Route("GetLogin")]
        public User Get(string email)
        {
            User user = _core.Get(email);

            return user;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public LinkGamerResult Login([FromBody] UserModel user)
        {
            if (user == null) return new LinkGamerResult(System.Net.HttpStatusCode.BadRequest, false, "Usuário ou senha inválidos");

            return _core.Login(user);
        }


    }
}
