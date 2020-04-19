namespace WalkerGame.Logic
{
    /// <summary>
    /// This target is fired after all resources have been loaded
    /// </summary>
    public interface ReadyTarget : PartTarget
    {
        void ServiceReady();
    }
}