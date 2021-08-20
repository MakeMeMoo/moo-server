using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using AutoMapper;
using moo_server.Core.BL.Interfaces;
using moo_server.Core.Entities;
using moo_server.Models;

namespace moo_server.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IUsersBL _usersBL;

        public AccountController(ILogger<AccountController> logger, IMapper mapper, IUsersBL usersBL)
        {
            _logger = logger;
            _mapper = mapper;
            _usersBL = usersBL;
        }

        [HttpPost]
        public UserModel Start(UserModel userModel)
        {
            var user = _usersBL.GetOrCreateUserByTgId(userModel);
            userModel = _mapper.Map<UserModel>(user);
            return userModel;
        }

        [HttpGet]
        public ResponseModel ClickMoo(long tgId)
        {
            var responseModel = new ResponseModel();

            var user = _usersBL.GetUser(tgId);
            if (user != null && (user.LastMooDate == null || user.LastMooDate.Value.AddMinutes(1) < DateTimeOffset.Now))
            {
                

                user.LastMooDate = DateTimeOffset.Now;
                user.MooCount++;
                _usersBL.UpdateUserMooCountAndLastMooDateById(user);
            }

            var message = "М";
            for (var i = 0; i < user.MooCount; i++)
            {
                message += "у";
            }
            responseModel.Message = message;

            return responseModel;
        }
    }
}
