using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume (float volume)
    {
        //Time.timeScale = 1;
        audioMixer.SetFloat("volume", volume);
    }

     public void Credits()
   {
        SceneManager.LoadSceneAsync(2);
        //Time.timeScale = 1;
   }
}
