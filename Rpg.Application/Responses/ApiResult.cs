using FluentValidation.Results;

namespace Rpg.Application.Responses
{
    public class ApiResult<TResponse>
    {
        public bool IsSuccessfulRequest { get; }
        public TResponse Response { get; }
        public IEnumerable<string> ErrorMessages { get; }

        public ApiResult(TResponse response)
        {
            IsSuccessfulRequest = true;
            Response = response;
            ErrorMessages = default;
        }

        public ApiResult(IEnumerable<ValidationFailure> validationFailures)
        {
            IsSuccessfulRequest = default;
            Response = default;
            ErrorMessages = validationFailures.Select(validationFailure => validationFailure.ErrorMessage);
        }

        public ApiResult(params string[] errorMessages)
        {
            IsSuccessfulRequest = default;
            Response = default;
            ErrorMessages = errorMessages;
        }
    }
}
