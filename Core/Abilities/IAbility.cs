namespace AAFramework.Core
{
    public interface IAbility 
    {
        void Execute(IOwner owner, ITarget target);
    }
}