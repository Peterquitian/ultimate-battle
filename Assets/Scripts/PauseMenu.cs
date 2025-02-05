using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
   public GameObject Menu;
   public void Pause()
   {
        Menu.SetActive(true);
        Time.timeScale = 0;
   }

   public void Resume()
   {
     Menu.SetActive(false);
     Time.timeScale = 1;
   }

   public void Home()
   {
        SceneManager.LoadSceneAsync(0);
        //Time.timeScale = 1;
   }

   public void Restart()
   {
        
        //Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
