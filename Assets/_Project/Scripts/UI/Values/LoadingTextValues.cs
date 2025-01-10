using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class LoadingTextValues : MonoBehaviour
{
    private LocalizeStringEvent localizeStringEvent;
    public LocalizedString loading = new(tableReference: "UI", entryReference: "loading");
    public LocalizedString loadingComplete = new(tableReference: "UI", entryReference: "loading_complete");

    private void Awake()
    {
        localizeStringEvent = GetComponent<LocalizeStringEvent>();
    }
    public void SetLoadingInProgressText()
    {
        localizeStringEvent.StringReference = loading;
        localizeStringEvent.RefreshString();
    }

    public void SetLoadingCompleteText()
    {
        localizeStringEvent.StringReference = loadingComplete;
        localizeStringEvent.RefreshString();
    }
}
