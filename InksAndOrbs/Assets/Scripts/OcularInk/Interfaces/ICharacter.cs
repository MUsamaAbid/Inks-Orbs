namespace OcularInk.Interfaces
{
    public interface ICharacter
    {
        public void TakeDamage(float damage);
        public void AddStatusEffect(StatusEffect effect);
    }
}
