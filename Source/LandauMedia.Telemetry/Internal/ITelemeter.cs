namespace LandauMedia.Telemetry.Internal
{
    interface ITelemeter
    {
        void ChangeImplementation(ITelemeterImpl impl);
    }
}