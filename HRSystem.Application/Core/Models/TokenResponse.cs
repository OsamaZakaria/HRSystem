namespace HRSystem.Application.Core.Models
{
    public sealed class TokenResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenResponse"/> class.
        /// </summary>
        /// <param name="token">The token value.</param>
        public TokenResponse(string token, string name, bool isEmployee)
        {
            Token = token; IsEmployee = isEmployee; Name = name;
        }

        /// <summary>
        /// Gets the token.
        /// </summary>
        public string Token { get; }
        public bool IsEmployee { get; }
        public string Name { get; }
    }
}
