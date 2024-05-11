public class SoundTrack : SaiMonoBehaviour
{
    protected override void Start()
    {
        SoundController.Instance.PlaySoundTrackEffect();
    }

    public virtual void StopSoundTrack()
    {
        SoundController.Instance.StopSoundTrackEffect();
    }
}
