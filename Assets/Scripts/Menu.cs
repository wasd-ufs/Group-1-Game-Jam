using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public GameObject Opcoes;
    public void Jogo()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void MenuOpcoes()
    {
        Opcoes.SetActive(true);
    }
    public void FecharMenu()
    {
        Opcoes.SetActive(false);
    }
    public void Sair()
    {
        Application.Quit();
    }
    public void MenuBotaoFim()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
