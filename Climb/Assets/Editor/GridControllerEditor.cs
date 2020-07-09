using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomEditor(typeof(GridController))]
    public class GridControllerEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            var controller = (GridController) target;

            EditorGUI.BeginChangeCheck();
            controller.size = EditorGUILayout.IntSlider("Size", controller.size, 1, 2048);
            if (EditorGUI.EndChangeCheck()) {
                controller.UpdateShader(PropertyName.GridSize);
                controller.RegenerateValues();
                controller.UpdateShader(PropertyName.Values);
                controller.UpdateShader(PropertyName.ValueLength);
            }

            controller.gridMaterial =
                (Material) EditorGUILayout.ObjectField("Grid Material", controller.gridMaterial, typeof(Material));
            
            EditorGUI.BeginChangeCheck();
            controller.perlinScale = EditorGUILayout.Slider("Perlin Scale", controller.perlinScale, 0.001f, 0.5f);
            controller.offsetChange.x = EditorGUILayout.IntSlider("Horizontal Speed", (int) controller.offsetChange.x, 0, 250);
            controller.offsetChange.y = EditorGUILayout.IntSlider("Horizontal Speed", (int) controller.offsetChange.y, 0, 250);
            if(EditorGUI.EndChangeCheck())
                controller.UpdateShader(PropertyName.Values);
        }
    }
}