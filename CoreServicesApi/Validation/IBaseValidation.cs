namespace Validation
{
    public interface IBaseValidation
    {
        public bool IsValidEmail(string email);
        public bool IsPasswordValid(string password);
        public bool IsValidName(string name);
        public Task<bool> IsTheNameUsed(string name);
        
    }
}