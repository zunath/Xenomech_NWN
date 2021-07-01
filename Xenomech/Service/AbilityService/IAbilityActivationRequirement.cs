namespace Xenomech.Service.AbilityService
{
    public interface IAbilityActivationRequirement
    {
        string CheckRequirements(uint player);
        void AfterActivationAction(uint player);
    }
}
