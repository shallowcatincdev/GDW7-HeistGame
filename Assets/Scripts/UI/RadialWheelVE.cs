using UnityEngine;
using UnityEngine.UIElements;

public class RadialWheelVE : VisualElement
{

    #region Feilds

    int segmentCount;
    int highlightIndex;
    float outerRadiusPixels;
    float innerRadiusPixels;
    float wheelPadding;
    float segmentGapAngle;

    Color normalFillInner;
    Color normalFillOuter;
    Color highlightFillInner;
    Color highlightFillOuter;
    Color edgeInner;
    Color edgeOuter;

    readonly Gradient runtimeStrokeGradient = new Gradient();

    #endregion

    public RadialWheelVE()
    {
        AddToClassList("radial-menu");
        pickingMode = PickingMode.Position;
        generateVisualContent += OnGenerateVisualContent;


    }

    public void SetColours(Color normalFillInner, Color normalFillOuter, Color highlightFillInner, Color highlightFillOuter, Color edgeInner, Color edgeOuter)
    {
        this.normalFillInner = normalFillInner;
        this.normalFillOuter = normalFillOuter;
        this.highlightFillInner = highlightFillInner;
        this.highlightFillOuter = highlightFillOuter;
        this.edgeInner = edgeInner;
        this.edgeOuter = edgeOuter;
        MarkDirtyRepaint();
    }

    public void SetHighlight(int index)
    {
        highlightIndex = index;
        MarkDirtyRepaint();
    }


    public void Configure(int segmentCount, int highlightIndex, float outerRadiusPixels, float innerRadiusPixels, float wheelPadding, float segmentGapAngle)
    {
        this.segmentCount = Mathf.Max(1, segmentCount);
        this.outerRadiusPixels = Mathf.Max(8f, outerRadiusPixels);
        this.innerRadiusPixels = Mathf.Max(0f, innerRadiusPixels);
        this.wheelPadding = Mathf.Max(0f, wheelPadding);
        this.segmentGapAngle = RadialWheelLayout.ClampGapAngle(segmentCount, segmentGapAngle);
        this.highlightIndex = highlightIndex;
        MarkDirtyRepaint();

    }

    static void StrokeRadialDivider(Painter2D painter, float x, float y, Vector2 center, float inner, float outer, float angle)
    {
        float a = angle * Mathf.Deg2Rad;
        painter.BeginPath();

        if (inner <= 1f) painter.MoveTo(center);
        else painter.MoveTo(new Vector2(x + Mathf.Cos(a) * inner, y + Mathf.Sin(a) * inner));

        painter.LineTo(new Vector2(x + Mathf.Cos(a) + outer, y + Mathf.Sin(a) * outer));
        painter.Stroke();
    }




    void OnGenerateVisualContent(MeshGenerationContext context)
    {
        Painter2D painter = context.painter2D;

        float w = contentRect.width;
        float h = contentRect.height;

        ((float x, float y) center, (float outer, float inner) radii) radial = RadialWheelLayout.GetRadii(w, h, outerRadiusPixels, innerRadiusPixels, wheelPadding);
        (float x, float y) = radial.center;
        (float outer, float inner) = radial.radii;
        Vector2 center = new Vector2(x, y);
        int n = segmentCount;
        if (n <= 0) return;

        float gap = RadialWheelLayout.ClampGapAngle(n, segmentGapAngle);
        runtimeStrokeGradient.SetKeys(
            new[] {new GradientColorKey(edgeInner, 0f), new GradientColorKey(edgeOuter, 1f)},
            new[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f)}
        );

        for (int i = 0; i < n; i++)
        {
            (float startAngle, float endAngle, float midAngle) = RadialWheelLayout.GetSegmentAngle(i, n, gap);
            float mid = midAngle * Mathf.Deg2Rad;
            Vector2 dir = new Vector2(Mathf.Cos(mid), Mathf.Sin(mid));
            Vector2 innerPt = inner <= 1f ? center : center + dir * inner;
            Vector2 outerPt = center + dir * outer;

            bool isHighlighted = i == highlightIndex;
            Color fillInner = isHighlighted ? highlightFillInner : normalFillInner;
            Color fillOuter = isHighlighted ? highlightFillOuter : normalFillOuter;

            painter.fillGradient = FillGradient.MakeLinearGradient(fillInner, fillOuter, innerPt, outerPt, AddressMode.Clamp);
            painter.BeginPath();

            if (inner <= 1f)
            {
                painter.MoveTo(center);
                float rs = startAngle * Mathf.Deg2Rad;
                painter.LineTo(new Vector2(x + Mathf.Cos(rs) * outer, y + Mathf.Sin(rs) * outer));
                painter.Arc(center, outer, Angle.Degrees(startAngle), Angle.Degrees(endAngle), ArcDirection.Clockwise);
                painter.LineTo(center);
            }
            else
            {
                float rs = startAngle * Mathf.Deg2Rad;
                float re = endAngle * Mathf.Deg2Rad;

                painter.MoveTo(new Vector2(x + Mathf.Cos(rs) * outer, y + Mathf.Sin(rs) * outer));
                painter.Arc(center, outer, Angle.Degrees(startAngle), Angle.Degrees(endAngle), ArcDirection.Clockwise);
                painter.LineTo(new Vector2(x + Mathf.Cos(re) * inner, y + Mathf.Sin(re) * inner));
                painter.Arc(center, inner, Angle.Degrees(endAngle), Angle.Degrees(startAngle), ArcDirection.CounterClockwise);
                painter.ClosePath();

            }

            painter.Fill();

        }

        painter.fillGradient = default;
        painter.lineWidth = 2f;
        painter.strokeGradient = runtimeStrokeGradient;


        for (int i = 0; i < n; i++)
        {
            (float startAngle, float endAngle, float midAngle) = RadialWheelLayout.GetSegmentAngle(i, n, gap);

            painter.BeginPath();
            painter.Arc(center, outer, Angle.Degrees(startAngle), Angle.Degrees(endAngle), ArcDirection.Clockwise);
            painter.Stroke();
            StrokeRadialDivider(painter, x, y, center, inner, outer, startAngle);
            if (gap > 1e-4f) StrokeRadialDivider(painter, x, y, center, inner, outer, endAngle);


        }
    }


}
