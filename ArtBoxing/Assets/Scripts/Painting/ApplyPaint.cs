using UnityEngine;

public class ApplyPaint : MonoBehaviour
{
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();

        // duplicate the original texture and assign to the material
        Texture2D texture = Instantiate(rend.material.mainTexture) as Texture2D;
        rend.material.mainTexture = texture;

        // colors used to tint the first 3 mip levels
        Color[] colors = new Color[3];
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;
        int mipCount = Mathf.Min(3, texture.mipmapCount);

        // tint each mip level
        for (int mip = 0; mip < mipCount; ++mip)
        {
            Color[] cols = texture.GetPixels(mip);
            for (int i = 0; i < cols.Length; ++i)
            {
                //cols[i] = Color.Lerp(cols[i], colors[mip], 0.33f);
                cols[i] = Color.green;
            }
            texture.SetPixels(cols, mip);
        }

        // actually apply all SetPixels, don't recalculate mip levels
        texture.Apply();

        rend.material.mainTexture = texture;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
        }
    }

    bool HitTestUVPosition(ref Vector3 uvWorldPosition)
    {
        RaycastHit hit;
        Vector3 mousePos = Input.mousePosition;
        Vector3 cursorPos = new Vector3(mousePos.x, mousePos.y, 0.0f);
        Ray cursorRay = Camera.main.ScreenPointToRay(cursorPos);
        if (Physics.Raycast(cursorRay, out hit, 200))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return false;
            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            uvWorldPosition.x = pixelUV.x;
            uvWorldPosition.y = pixelUV.y;
            uvWorldPosition.z = 0.0f;
            return true;
        }
        else
        {
            return false;
        }
    }
}
