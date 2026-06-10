using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private Image _fadePanel;

    private void Awake()
    {
        _fadePanel.color = new Color(0, 0, 0, 1);
        _fadePanel.DOFade(0, 1);
    }

    public void Load(string sceneName)
    {
        if(SceneManager.GetSceneByName(sceneName).name != "") 
        {
            _fadePanel.DOFade(1, 1).OnComplete(() => SceneManager.LoadScene(sceneName));
        }
    }
}
