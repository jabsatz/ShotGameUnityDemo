using UnityEngine;
using System.Collections;

// [ExecuteInEditMode]
public class SimpleBlit : MonoBehaviour {
    public Material TransitionMaterial;
    public int transitionTime = 100;
    public bool opening = true;
    public float cutoffPosition = 1;
    public Color transitionColor = Color.black;
    public Texture2D transitionTexture;
    public float fade = 1;

    private void Update() {
        if(opening && cutoffPosition > 0) {
            cutoffPosition -= 1f/transitionTime;
        }
        if(!opening && cutoffPosition < 1) {
            cutoffPosition += 1f/transitionTime;
        }
        cutoffPosition = Mathf.Clamp(cutoffPosition, 0, 1);
        if(cutoffPosition != 0 && cutoffPosition != 1) {
            TransitionMaterial.SetFloat("_Cutoff", EaseInOut(cutoffPosition));
            TransitionMaterial.SetTexture("_TransitionTex", transitionTexture);
            TransitionMaterial.SetColor("_Color", transitionColor);
            TransitionMaterial.SetFloat("_Fade", fade);
        }
    }

    private float EaseInOut(float position) {
        return position*(2-position);
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst) {
        if(TransitionMaterial != null)
            Graphics.Blit(src, dst, TransitionMaterial);
    }
}
