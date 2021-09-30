using UnityEngine;
using System.Collections;
using System.IO;
using SimpleFileBrowser;

public class FileBrowserTest : MonoBehaviour
{
	public static string path = "notyet";

    public void selectSong()
    {
		path = "notyet";
		StartCoroutine(ShowLoadDialogCoroutine());
		ImporterExample.started = false;
		SpeakerController.resetAutoMovement();
	}

	void Start()
	{
		FileBrowser.SetFilters(true, new FileBrowser.Filter("Music", ".mp3", ".wav", ".ogg"));
		FileBrowser.SetDefaultFilter(".mp3");
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

		FileBrowser.AddQuickLink("Storage Music", "/storage/emulated/0/Music", null);
		FileBrowser.AddQuickLink("Downloads", "/storage/emulated/0/Download", null);

		StartCoroutine(ShowLoadDialogCoroutine());
	}

	 IEnumerator ShowLoadDialogCoroutine()
	{
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Select Track", "Load");

		if (FileBrowser.Success)
		{
			path = FileBrowser.Result[0];
		}
	}
}