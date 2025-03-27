public class BloemetjeGevenQuestStep : QuestStep
{
    private bool _flowerGet = false;

    private void OnEnable()
    {
        GameManager.instance.miscEvents.OnFlowersCollected += FlowerCollect;
    }
    private void OnDisable()
    {
        GameManager.instance.miscEvents.OnFlowersCollected -= FlowerCollect;
    }

    private void FlowerCollect()
    {
        //Condition Check, did you do the thing, yes? finish quest, no? return*
        if (_flowerGet) return;

        UpdateState();

        FinishedQuestStep();

        _flowerGet = true;
    }

    private void UpdateState()
    {
        string state = _flowerGet.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this._flowerGet = System.Boolean.Parse(state);
        UpdateState();
    }

}
