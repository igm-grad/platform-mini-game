using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

[ExecuteInEditMode]
public class SpriteCover : MonoBehaviour {

    public Sprite sprite;
    public Material material;

    [Range(0, 1)]
    public float topMargin = 0.3f;

    [Range(0, 1)]
    public float bottomMargin = 0.3f;

    [Range(0, 1)]
    public float leftMargin = 0.3f;
    
    [Range(0, 1)]
    public float rightMargin = 0.3f;

    private void CreateSlices()
    {

        while (transform.childCount > 0)
        {
            GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
        }
         
        var meshObject = transform.parent.FindChild("Mesh");

        var mesh = meshObject.GetComponent<MeshFilter>().sharedMesh;

        var bounds = mesh.bounds;
        bounds.size = Vector3.Scale(bounds.size, meshObject.transform.localScale);

        SpriteRenderer sr;
        Rect rect;

        var size = new Vector2(sprite.texture.width, sprite.texture.height);

        // corners
        var tr = new GameObject("tr");
        var br = new GameObject("br");
        var tl = new GameObject("tl");
        var bl = new GameObject("bl");

        tr.transform.parent = transform;
        tr.transform.localPosition = new Vector3(bounds.max.x, bounds.max.y, -1);
        tr.transform.localScale = Vector3.one;
        sr = tr.AddComponent<SpriteRenderer>();
        rect = new Rect(size.x * (1 - rightMargin), size.y * (1 - topMargin), size.x * rightMargin, size.y * topMargin);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(1, 1), 1000);
        sr.material = material;

        br.transform.parent = transform;
        br.transform.localPosition = new Vector3(bounds.max.x, bounds.min.y, -1);
        br.transform.localScale = Vector3.one;
        sr = br.AddComponent<SpriteRenderer>();
        rect = new Rect(size.x * (1 - rightMargin), 0, size.x * rightMargin, size.y * bottomMargin);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(1, 0), 1000);
        sr.material = material;

        tl.transform.parent = transform;
        tl.transform.localPosition = new Vector3(bounds.min.x, bounds.max.y, -1);
        tl.transform.localScale = Vector3.one;
        sr = tl.AddComponent<SpriteRenderer>();
        rect = new Rect(0, size.y * (1 - topMargin), size.x * leftMargin, size.y * topMargin);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(0, 1), 1000);
        sr.material = material;

        bl.transform.parent = transform;
        bl.transform.localPosition = new Vector3(bounds.min.x, bounds.min.y, -1);
        bl.transform.localScale = Vector3.one;
        sr = bl.AddComponent<SpriteRenderer>();
        rect = new Rect(0, 0, size.x * leftMargin, size.y * bottomMargin);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(0, 0), 1000);
        sr.material = material;

        // sides
        var t = new GameObject("t");
        var r = new GameObject("r");
        var b = new GameObject("b");
        var l = new GameObject("l");

        var bodyWidth = 1 - (leftMargin + rightMargin);
        var bodyHeight = 1 - (topMargin + bottomMargin);

        t.transform.parent = transform;
        t.transform.localPosition = new Vector3((leftMargin - rightMargin) / 2, bounds.max.y, -1);
        t.transform.localScale = new Vector3((meshObject.localScale.x - (leftMargin + rightMargin)) * (1 / bodyWidth), 1, 1);
        sr = t.AddComponent<SpriteRenderer>();
        rect = new Rect(size.x * leftMargin, size.y * (1 - topMargin), size.x * bodyWidth, size.y * topMargin);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(.5f, 1), 1000);
        sr.material = material;

        r.transform.parent = transform;
        r.transform.localPosition = new Vector3(bounds.max.x, (bottomMargin - topMargin) / 2, -1);
        r.transform.localScale = new Vector3(1, (meshObject.localScale.y - (topMargin + bottomMargin)) * (1 / bodyHeight), 1);
        sr = r.AddComponent<SpriteRenderer>();
        rect = new Rect(size.x * (1 - rightMargin), size.y * bottomMargin, size.x * rightMargin, size.y * bodyHeight);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(1, .5f), 1000);
        sr.material = material;

        b.transform.parent = transform;
        b.transform.localPosition = new Vector3((leftMargin - rightMargin) / 2, bounds.min.y, -1);
        b.transform.localScale = new Vector3((meshObject.localScale.x - (leftMargin + rightMargin)) * (1 / bodyWidth), 1, 1);
        sr = b.AddComponent<SpriteRenderer>();
        rect = new Rect(size.x * leftMargin, 0, size.x * bodyWidth, size.y * bottomMargin);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(.5f, 0), 1000);
        sr.material = material;

        l.transform.parent = transform;
        l.transform.localPosition = new Vector3(bounds.min.x, (bottomMargin - topMargin) / 2, -1);
        l.transform.localScale = new Vector3(1, (meshObject.localScale.y - (topMargin + bottomMargin)) * (1 / bodyHeight), 1);
        sr = l.AddComponent<SpriteRenderer>();
        rect = new Rect(0, size.y * bottomMargin, size.x * leftMargin, size.y * bodyHeight);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(0, .5f), 1000);
        sr.material = material;

        // center
        var c = new GameObject("c");
        c.transform.parent = transform;
        c.transform.localPosition = new Vector3((leftMargin - rightMargin) / 2, (bottomMargin - topMargin) / 2, -1);
        c.transform.localScale = new Vector3((meshObject.localScale.x - (leftMargin + rightMargin)) * (1 / bodyWidth), (meshObject.localScale.y - (topMargin + bottomMargin)) * (1 / bodyHeight), 1);
        sr = c.AddComponent<SpriteRenderer>();
        rect = new Rect(size.x * leftMargin, size.y * bottomMargin, size.x * bodyWidth, size.y * bodyHeight);
        sr.sprite = Sprite.Create(sprite.texture, rect, new Vector2(.5f, .5f), 1000);
        sr.material = material;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            CreateSlices();
        }
#endif
    }

}
