using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(GridController))]
public class GridControllerEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
        var controller = (GridController) target;

        controller.size = EditorGUILayout.IntSlider("Size", controller.size, 1, 2048);
        controller.perlinScale = EditorGUILayout.Slider("Perlin Scale", controller.perlinScale, 0.001f, 0.5f);
        controller.offsetChange.x = EditorGUILayout.IntSlider("Horizontal Speed", (int) controller.offsetChange.x, 0, 250);
        controller.offsetChange.y = EditorGUILayout.IntSlider("Horizontal Speed", (int) controller.offsetChange.y, 0, 250);
    }
}