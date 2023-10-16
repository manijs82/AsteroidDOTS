using UnityEngine;

[System.Serializable]
public struct Bound
{
    [HideInInspector] public float x;
    [HideInInspector] public float y;
    public float w;
    public float h;

    public Vector2 Extents => new Vector2(w / 2f, h / 2f);
    public Vector2 Start => new Vector2(x, y);
    public Vector2 Center => Start + Extents;
    public Vector2 BottomLeft => new Vector2(x, y);
    public Vector2 BottomRight => new Vector2(XPW, y);
    public Vector2 TopLeft => new Vector2(x, YPH);
    public Vector2 TopRight => new Vector2(XPW, YPH);
    public float XPW => x + w;
    public float YPH => y + h;

    public Bound(float x, float y, float w, float h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }
    
    public Vector2 GetRandomVectorInside()
    {
        float xPos = Random.Range(x, XPW);
        float yPos = Random.Range(y, YPH);

        return new Vector2(xPos, yPos);
    }

    public static bool Collide(Bound rect1, Bound rect2)
    {
        return rect1.x < rect2.x + rect2.w &&
               rect1.x + rect1.w > rect2.x &&
               rect1.y < rect2.y + rect2.h &&
               rect1.h + rect1.y > rect2.y;
    }

    public static bool Inside(Bound rect1, Bound rect2)
    {
        return rect1.x + rect1.w <= rect2.w &&
               rect1.y + rect1.h <= rect2.h &&
               rect1.x >= rect2.x &&
               rect1.y >= rect2.y;
    }
    
    public static bool Inside(Vector2 point, Bound bound)
    {
        return point.x > bound.x && point.x < bound.XPW &&
               point.y > bound.y && point.y < bound.YPH;
    }
}