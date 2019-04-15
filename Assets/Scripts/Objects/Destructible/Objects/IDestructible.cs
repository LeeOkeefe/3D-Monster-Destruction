namespace Objects.Destructible.Objects
{
    public interface IDestructible
    {
        void Damage(float damage);
        void Destruct();
    }
}