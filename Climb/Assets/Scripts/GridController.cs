using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// A simple Grid Rendering Controller using MeshRenderer.
/// </summary>
public class GridController : MonoBehaviour {
    public int size = 32;

    public Material gridMaterial;
    private float[] _values;
    private ComputeBuffer buffer;


    private void regenerateValues() {
        _values = new float[size * size];
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                SetValue(x, y, Random.value);
            }
        }
        
        Debug.Log($"{_values.Length} values regenerated.");
        // Debug.Log(String.Join(", ", _values.ToList().ConvertAll(i => i.ToString()).ToArray()));
    }

    private void Start() {
        buffer = new ComputeBuffer(size * size, 4);
        regenerateValues();

        UpdateShader();
    }

    private void UpdateShader() {
        gridMaterial.SetFloat("_GridSize", size);
        gridMaterial.SetFloat("_valueLength", _values.Length);
        buffer.SetData(_values);
        gridMaterial.SetBuffer("_values", buffer);
    }

    private void Update() {
        if (Input.GetKey("space")) {
            Debug.Log("Reloading values...");
            regenerateValues();
            Debug.Log("Values generated. Updating Shader...");
            UpdateShader();
            Debug.Log("Done.");
        }
    }

    private void OnApplicationQuit() {
        buffer.Release();
    }

    public void SetValue(int x, int y, float value) {
        _values[size * y + x] = value;
    }

    public float GetValue(int x, int y) {
        return _values[size * y + x];
    }
}