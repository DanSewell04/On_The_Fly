using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetSnapshot(string name)
    {
        AudioMixerSnapshot snapshot = audioMixer.FindSnapshot(name);
        snapshot.TransitionTo(1.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyArea"))
        {
            SetSnapshot("Combat");
        }
    }
}
