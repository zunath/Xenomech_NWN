void main()
{
    object oPC = GetLastUsedBy();

    AssignCommand(oPC, ActionPlayAnimation(ANIMATION_LOOPING_CUSTOM16));
}
