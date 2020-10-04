using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RangeDeltaAttribute : PropertyAttribute
{
    public float min;
    public float max;
    public float delta;

    public RangeDeltaAttribute(float min = 0.0f, float max = 1.0f, float delta = 1.0f)
    {
        this.min = min;
        this.max = max;
        this.delta = delta;
    }
}

[CustomPropertyDrawer(typeof(RangeDeltaAttribute))]
public class RangeDeltaDrawer : PropertyDrawer
{
    private readonly float buttonWidth = 70.0f;
    private readonly float buttonHeight = 20.0f;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        RangeDeltaAttribute RangeDeltaAttribute = (RangeDeltaAttribute)attribute;

        if(property.propertyType == SerializedPropertyType.Float)
        {
            float current = position.x;
            var rect = new Rect(current, position.y, buttonWidth, buttonHeight);
            EditorGUI.LabelField(rect, property.name);
            rect.x += buttonWidth;
            if (GUI.Button(rect, "-" + RangeDeltaAttribute.delta.ToString()))
            {
                property.floatValue -= RangeDeltaAttribute.delta;
            }
            rect.x += buttonWidth;
            property.floatValue = EditorGUI.FloatField(rect, property.floatValue);
            rect.x += buttonWidth;
            if (GUI.Button(rect, "+" + RangeDeltaAttribute.delta.ToString()))
            {
                property.floatValue += RangeDeltaAttribute.delta;
            }
            property.floatValue = Mathf.Clamp(property.floatValue, RangeDeltaAttribute.min, RangeDeltaAttribute.max);
        }

        if(property.propertyType == SerializedPropertyType.Integer)
        {
            float current = position.x;
            var rect = new Rect(current, position.y, position.width, buttonHeight);
            EditorGUI.LabelField(rect, property.name);
            rect.x += buttonWidth;
            if (GUI.Button(rect, "-" + RangeDeltaAttribute.delta.ToString()))
            {
                property.intValue -= (int)RangeDeltaAttribute.delta;
            }
            rect.x += buttonWidth;
            property.intValue = (int)EditorGUI.IntField(rect, property.intValue);
            rect.x += buttonWidth;
            if (GUI.Button(rect, "+" + RangeDeltaAttribute.delta.ToString()))
            {
                property.intValue += (int)RangeDeltaAttribute.delta;
            }
            property.intValue = Mathf.Clamp((int)property.intValue, (int)RangeDeltaAttribute.min, (int)RangeDeltaAttribute.max);
        }
    }
}
