namespace HRSystem.Application.Core.Models
{
    public sealed class TokenResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenResponse"/> class.
        /// </summary>
        /// <param name="token">The token value.</param>
        public TokenResponse(string token, string name, bool isEmployee,string employeeId)
        {
            Token = token; IsEmployee = isEmployee; Name = name; EmployeeId = employeeId;
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        public string Token { get; }
        public bool IsEmployee { get; }
        public string Name { get; }
        public string EmployeeId { get; }
    }
}
