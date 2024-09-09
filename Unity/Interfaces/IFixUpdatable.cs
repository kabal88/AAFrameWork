namespace AAFramework.Unity.Identifiers
{
    public interface IFixUpdatable
    {
        bool IsAlive { get; }
        void FixedUpdateLocal();
    }
}