namespace MIP.Interfaces
{
    public interface IEndpoint
    {
        IEndpointState State { get; set; }
        IConnection ConnectedTo { get; set; }
        void Trigger();
        string Name { get; set; }
    }
}