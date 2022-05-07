using UnityEngine;

/* Thanks to Troy Lamerton and his top down movement script,
 * This is my version of his Boundary struct. */

[System.Serializable]
public struct Boundary
{
    public readonly float xMin;
    public readonly float xMax;
    public readonly float zMin;
    public readonly float zMax;

    public Boundary(float xRange, float zRange) //
    {
        this.xMin = -xRange;
        this.xMax = xRange;
        this.zMin = -zRange;
        this.zMax = zRange;
    }

    public bool IsInBounds(Vector3 position)
    {
        return (
            xMin < position.x && position.x < xMax
            &&
            zMin < position.z && position.z < zMax
            );
    }
}
