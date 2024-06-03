using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class ShareLink : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CopyToClipboardAndShare(string textToCopy);

    public void Share()
    {
        /*var shareText = $"Wordle Clone | {_lineFinished}/6\n";

        foreach (var line in _lines)
        {
            foreach (var answerColor in line.GetAnswerColors())
            {
                if (ColorCollection.IsTheSameColor(ColorCollection.Green, answerColor))
                {
                    shareText += "🟩";
                }
                else if (ColorCollection.IsTheSameColor(ColorCollection.Yellow, answerColor))
                {
                    shareText += "🟨";
                }
                else if (ColorCollection.IsTheSameColor(ColorCollection.Grey, answerColor))
                {
                    shareText += "⬜";
                }
            }

            shareText += "\n";
        }

        _popupModal.ShowPopup("Copied results to clipboard!");*/
        CopyToClipboardAndShare("¡Proba el videojuego de Dexxa y gana increibles premios! https://jeronimocalandro.github.io/Dexxa-game/");
        Debug.Log("Copiado");
    }
}
