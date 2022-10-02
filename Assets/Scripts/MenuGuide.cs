using UnityEngine;

public class MenuGuide : Guide
{

    protected override void OnEnable()
    {
        base.OnEnable();
        PlatesGrid.StartedGame += OnStartedGame;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlatesGrid.StartedGame -= OnStartedGame;
    }


    private void OnStartedGame()
    {
        if (UI != null)
        {
            Destroy(UI.gameObject);
        }       
    }
}
