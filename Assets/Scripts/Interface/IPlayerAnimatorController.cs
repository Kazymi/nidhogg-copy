public interface IPlayerAnimatorController
{
    public void SetAnimationBool(AnimationNameType animationNameType, bool value);
    public void SetTrigger(string animationNameType, bool isInteractable);
    public void UpdateAnimation();
}