namespace MIP.Interfaces
{
    public interface IConnection
    {
        IHub Via { get; set; }
        IEndpoint Endpoint { get; set; }
        void Register();
    }
}