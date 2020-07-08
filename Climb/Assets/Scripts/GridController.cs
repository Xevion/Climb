using System;
using UnityEngine;

public enum PropertyName {
    GridSize,
    ValueLength,
    Values
}

/// <summary>
/// A simple Grid Rendering Controller using MeshRenderer.
/// </summary>
public class GridController : MonoBehaviour {
    public Material gridMaterial;
    public int size = 32;
    public float perlinScale = 16;
    public Vector2 offsetChange = new Vector2(1, 0);
    
    private Vector2 _offset;
    private float[] _values;
    private ComputeBuffer _buffer;
    
    // Get all property IDs
    private static readonly int ValueLength = Shader.PropertyToID("_valueLength");
    private static readonly int Values = Shader.PropertyToID("_values");
    private static readonly int GridSize = Shader.PropertyToID("_GridSize");

    public void RegenerateValues() {
        _values = new float[size * size];
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                SetValue(x, y, Mathf.PerlinNoise((x + _offset.x) * perlinScale, (y + _offset.y) * perlinScale));
            }
        }
    }

    private void Start() {
        _offset = new Vector2(0, 0);
        _buffer = new ComputeBuffer((int) Mathf.Pow(2048, 2), 4);
        RegenerateValues();
        
        // Update all Shader properties
        foreach(PropertyName property in Enum.GetValues(typeof(PropertyName)))
            UpdateShader(property);
    }

    /// <summary>
    /// Updates Shader material properties.
    /// </summary>
    /// <param name="property">PropertyName item representing Shader property to be updated</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void UpdateShader(PropertyName property) {
        switch (property) {
            case PropertyName.GridSize:
                gridMaterial.SetFloat(GridSize, size);
                break;
            case PropertyName.Values:
                _buffer.SetData(_values);
                gridMaterial.SetBuffer(Values, _buffer);
                break;
            case PropertyName.ValueLength:
                gridMaterial.SetFloat(ValueLength, _values.Length);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(property), property, null);
        }
    }

    private void Update() {
        // Regenerate new position then send values to Shader
        RegenerateValues();
        UpdateShader(PropertyName.Values);
        
        // Move offset
        _offset += offsetChange * Time.deltaTime;
    }

    private void OnApplicationQuit() {
        // Release ComputeBuffer memory
        _buffer.Release();
    }

    public void SetValue(int x, int y, float value) {
        _values[size * y + x] = value;
    }

    public float GetValue(int x, int y) {
        return _values[size * y + x];
    }
}