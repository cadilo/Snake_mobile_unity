using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject play_btn;
    public GameObject multiplayer_btn;

    public void play_load_sc()
    {
        SceneManager.LoadScene("SingleGame");
    }

    public void mlt_btn_sc()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
