namespace Backend.Model.Auth
{
    public record AuthResult(bool Success, string Email, string Username, string Token)
    {
        private readonly Dictionary<string, string> _errorMessages = new();

        public void AddError(string errCode, string errDesc)
        {
            _errorMessages.Add(errCode, errDesc);
        }
        public Dictionary<string, string> GetErrors()
        {
            return _errorMessages;
        }
    }
}
