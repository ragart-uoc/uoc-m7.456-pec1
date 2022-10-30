namespace PEC1.Entities
{
    public class Character
    {
        public int Health;

        public Character(int health)
        {
            Health = health;
        }

        public bool TakeDamage(int damage)
        {
            Health -= damage;
            return Health <= 0;
        }
    }
}