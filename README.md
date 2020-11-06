# Climb

A simple project for testing out how to use Shaders in Unity.

This project mostly revolves around rendering of a grid of squares, each able to accept a color. Currently it is programmed to accept a single floating point describing it's position on a value gradient.

It is also programmed to use values from a Perlin Noise map which slowly scrolls over. The value gradient's points were generated from a image I found online, and the RGB values generated from a simple Python script.

## Screenshot

![Scrolling Perlin Noise Heatmap](./perlin_heatmap.gif)

## Important Files

- [GridShader.shader](./Climb/Assets/GridShader.shader) - Controls rendering of a grid. Values are sent through the GPU (StructuredBuffer)!

- [GridController.cs](./Climb/Assets/Scripts/GridController.cs) - Controls value generation, scrolling and actually sending values to the shader mat.

- [hsv/main.py](./hsv/main.py) - Generates an array literal from [gradient.png](./hsv/gradient.png) for use in the Shader as a gradient for a heatmap.