namespace AAFramework.Unity.Identifiers
{
    public interface IUpdatable
    {
        bool IsAlive { get; }
        void UpdateLocal();
    }
}