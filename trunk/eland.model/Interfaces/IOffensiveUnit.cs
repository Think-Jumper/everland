namespace eland.model.Interfaces
{
    public interface IOffensiveUnit
    {
        void Attack();
        int AttackStrength { get; set; }
    }
}