﻿using System.Collections;
using TbsFramework.Units;
using TbsFramework.Units.Highlighters;
using UnityEngine;

public class SpriteRendererGlowHighlighter : UnitHighlighter
{
    public Color Color;
    public float CooloutTime;
    public SpriteRenderer Renderer;

    public override void Apply(Unit unit, Unit otherUnit)
    {
        StartCoroutine(Glow());
    }

    private IEnumerator Glow()
    {
        float startTime = Time.time;
        var baseColor = Renderer.material.color;

        while (startTime + CooloutTime > Time.time)
        {
            Renderer.color = Color.Lerp(baseColor, Color, (startTime + CooloutTime) - Time.time);
            yield return 0;
        }

        Renderer.material.color = baseColor;
    }
}
