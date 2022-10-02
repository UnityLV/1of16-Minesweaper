using UnityEngine;

public class InGameGuide : Guide
{
    protected override void OnEnable()
    {
        base.OnEnable();
        PlatesGrid.StartedGame += OnStartedGame;
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlatesGrid.StartedGame -= OnStartedGame;
    }

    private void OnStartedGame()
    {
        UI.gameObject.SetActive(true);
    }


}
