using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.Account.Api.Commands.SignIn;
using Sample.Account.Api.Commands.SignUp;
using Sample.Account.Api.Models;
using Sample.Account.Api.Queries.GetAmountOfRegisteredUsers;
using Sample.Account.Api.ViewModels;

namespace Sample.Account.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Performs Sign Up based on passed credentials
        /// </summary>
        /// <param name="model">Model with credentials</param>
        /// <returns>Created user model</returns>
        [HttpPost("signUp", Name = nameof(SignUp))]
        public async Task<ActionResult> SignUp([FromBody] SignUpModel model)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var isCreated = await _mediator.Send(new SignUpCommand() { Model = model });
            return isCreated ? Created("", model) : BadRequest();
        }

        /// <summary>
        /// Performs Sign In based on passed credentials
        /// </summary>
        /// <param name="model">Model with credentials</param>
        /// <returns>Jwt Token</returns>
        [HttpPost("signIn", Name = nameof(SignIn))]
        public async Task<ActionResult> SignIn([FromBody] SignInModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(new SignInCommand() { Model = model });
            if (result == null)
                return Unauthorized();

            return Ok(result);
        }

        /// <summary>
        /// Get amount of registered users
        /// </summary>
        /// <returns>Amount of registered users</returns>
        [Authorize]
        [HttpGet("amountOfRegisteredUsers", Name = nameof(AmountOfRegisteredUsers))] 
        [Produces(typeof(AmountOfRegisteredUsersViewModel))]
        public async Task<ActionResult<AmountOfRegisteredUsersViewModel>> AmountOfRegisteredUsers()
        {
            var viewModel = await _mediator.Send(new GetAmountOfRegisteredUsersQuery());
            return viewModel;
        }
    }
}