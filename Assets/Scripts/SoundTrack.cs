public class SoundTrack : SaiMonoBehaviour
{
    protected override void Start()
    {
        SoundManager.Instance.PlaySoundTrackEffect();
    }

    public virtual void StopSoundTrack()
    {
        SoundManager.Instance.StopSoundTrackEffect();
    }
}
