namespace App1.Interfaces
{
    public interface IValidatable
    {
        void Validate(string memberName, object value);
    }
}
