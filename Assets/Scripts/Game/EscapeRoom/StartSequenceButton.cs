using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequenceButton : CubeButton
{
    private void OnMouseDown()
    {
        Highlight();
        simonManager.PlaySequence();
    }
}
