using System;
using UnityEngine;

public static class RadialWheelLayout
{
    public static ((float x, float y) center, (float outer, float inner) radii) GetRadii(float hostW, float hostH, float outerRadiusPix, float innerRadiusPix, float padding)
    {
        if (hostW <= 1f || hostH <= 1f)
        {
            return ((0f, 0f), (0f, 0f));
        }

        float x = hostW * 0.5f;
        float y = hostH * 0.5f;
        float maxR = Mathf.Min(hostW, hostH) * 0.5f - padding;
        float outer = Mathf.Min(outerRadiusPix, maxR);
        float inner = Mathf.Min(innerRadiusPix, outer - 4f);
        return ((x, y), (outer, inner));
    }

    public static float ClampGapAngle(int segmentCount, float gapSize)
    {
        int n = Mathf.Max(1, segmentCount);
        const float minSweep = 1f;
        float maxTotalGap = Mathf.Max(0f, 360f - n * minSweep);
        float maxGapPer = maxTotalGap / n;
        return Mathf.Clamp(Mathf.Max(0f, gapSize), 0f, maxGapPer);
    }


    public static (float startAngle, float endAngle, float midAngle) GetSegmentAngle(int index, int segmentCount, float gapSize)
    {
        int n = Mathf.Max(1, segmentCount);
        gapSize = ClampGapAngle(n, gapSize);
        float sweep = (360f - n * gapSize) / n;
        float startAngle = -90f + gapSize * 0.5f + index * (sweep + gapSize);
        float endAngle = startAngle + sweep;
        float midAngle = startAngle + sweep * 0.5f;
        return (startAngle, endAngle, midAngle);
    }

    public static int GetSegmentFromAngle(float angle, int segmentCount, float gapSize)
    {
        int n = Mathf.Max(1, segmentCount);
        gapSize = ClampGapAngle(n, gapSize);
        float sweep = (360f - n * gapSize) / n;

        for (int i = 0; i < n; i ++)
        {
            float segStart = gapSize * 0.5f + i * (sweep + gapSize);
            float segEnd = segStart + sweep;

            if (angle >= segStart && (i < n - 1 ? angle < segEnd : angle <= segEnd + 0.001f))
            {
                return i;
            }

        }

        return -1;
    }


    public static (float x, float y) GetSegmentCenter(int index, int segmentCount, float centerX, float centerY, float outer, float inner, float gapSize)
    {
        (float startAngle, float endAngle, float midAngle) angles = GetSegmentAngle(index, segmentCount, gapSize);
        float rad = angles.midAngle * Mathf.Deg2Rad;
        float midR = inner <= 1f ? outer * 0.65f : (inner + outer) * 0.5f;
        return (centerX + Mathf.Cos(rad) * midR, centerY + Mathf.Sin(rad) * midR);
    }


}
