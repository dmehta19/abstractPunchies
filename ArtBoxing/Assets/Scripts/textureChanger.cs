using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textureChanger : MonoBehaviour {

    public List<Texture2D> textureLists;
    public GameObject testCube;

    private Renderer mainMat;
    private int textureIndex = 0;

	// Use this for initialization
	void Start () {
        mainMat = testCube.GetComponent<Renderer>();
        mainMat.material.mainTexture = textureLists[textureIndex];
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A) == true)
        {
            incrementTexture(-1);
            mainMat.material.mainTexture = textureLists[textureIndex];
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            incrementTexture(1);
            mainMat.material.mainTexture = textureLists[textureIndex];
        }
        Debug.Log(textureIndex);
	}

    public void incrementTexture(int amount)
    {
        if(textureIndex + amount > textureLists.Count - 1)
        {
            textureIndex = 0;
        } else if(textureIndex + amount < 0)
        {
            textureIndex = textureLists.Count - 1;
        }
        else
        {
            textureIndex += amount;
        }
    }
}