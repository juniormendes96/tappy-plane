using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScroller : MonoBehaviour {
    
    public float scrollSpeed;
    
    private Renderer renderer;
    private Vector2 savedOffset;

    void Start() {
        savedOffset = renderer.sharedMaterial.GetTextureOffset("_MainTex");
    }

    void Update() {
       float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
       Vector2 offset = new Vector2(x, savedOffset.y);

       renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    void Awake() {
        renderer = GetComponent<Renderer>();
    }

    void OnDisable() {
        renderer.sharedMaterial.SetTextureOffset("_MainTex", savedOffset);
    }
}
