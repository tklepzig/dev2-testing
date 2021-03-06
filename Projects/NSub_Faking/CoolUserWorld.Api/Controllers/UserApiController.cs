﻿namespace CoolUserWorld.Api.Controllers
{
    using System;
    using System.Net;
    using System.Web.Http;

    public class UserApiController : ApiController
    {
        private readonly ICredentialService _credentialService;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly INotifier _notifier;

        public UserApiController(ICredentialService credentialService, IUserRepository userRepository, ILogger logger, INotifier notifier)
        {
            _credentialService = credentialService;
            _userRepository = userRepository;
            _logger = logger;
            _notifier = notifier;
        }

        [HttpPost]
        public ApiResult CreateUser(User user)
        {
            try
            {

                if (_credentialService.UserExists(user))
                    throw new HttpResponseException(HttpStatusCode.Conflict);

                if (!_credentialService.CheckUserCredentials(user))
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                _userRepository.CreateUser(user);

                if (user.HasActivatedNotification)
                    _notifier.Notify(user);

                return new ApiResult { Success = true };
            }

            catch (Exception exc)
            {
                _logger.Error("irgendwas halt");
                throw exc;
            }
        }
    }

    public class ApiResult
    {
        public bool Success { get; set; }
    }
}

