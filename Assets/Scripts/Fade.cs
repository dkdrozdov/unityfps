using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    float fadePerTime = 0.1f;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b,
        meshRenderer.material.color.a - fadePerTime * Time.deltaTime);
        if (meshRenderer.material.color.a <= 0f)
        {
            Destroy(transform.root);
        }
    }
}
