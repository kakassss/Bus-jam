using UnityEngine.SceneManagement;

public class ResetLevelButtonListener : BaseButtonListener
{
    private void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    protected override void OnClick()
    {
        ReloadCurrentScene();
    }
}
