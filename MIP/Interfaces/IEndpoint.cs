namespace MIP.Interfaces
{
    public interface IEndpoint
    {
        IEndpointState State { get; set; }
        IConnection ConnectedTo { get; set; }
        string Name { get; set; }
    }
}