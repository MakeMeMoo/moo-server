using Microsoft.AspNetCore.Mvc;
using System;
using moo_server.Core.BL.Interfaces;
using moo_server.Models;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace moo_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MooController : ControllerBase
    {
        private readonly IUsersBL _usersBL;

        public MooController(IUsersBL usersBL)
        {
            _usersBL = usersBL;
        }

        [SwaggerOperation(Summary = "Сказать \"Муу\"")]
        [HttpGet("user")]
        [Authorize]
        public ResponseModel SayMoo()
        {
            var responseModel = new ResponseModel();

            //TODO: move this logic to method SayMoo in UserBL that will return message 'moo...'
            var user = _usersBL.GetUserByTgUsername(User.Identity?.Name);
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
