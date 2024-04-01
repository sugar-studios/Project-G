# Sunshine Flare

A shader that renders a lens flare and crepuscular rays (sun rays) from the sun over the scene.

![Preview](../../wikis/uploads/60278616baf3c2112265f0b81c911e05/Unity_2021-01-07_12-31-29.jpg)

## Installation

Download the repository. Then place the Shader/ folder with the shader into your Assets/ directory.

## Usage

Provided in this package are:

- The main Sunshine Flare shader.
- An inverted icosphere mesh, used to render the flare.
- A sample prefab and material for a flare.

If not using the prefab, drag the icosphere into your scene and assign it a material with one of the Sunshine Flare shaders. Done! If this doesn't work, you might need to force the depth buffer to be active. [See here for info.](https://github.com/Xiexe/XSVolumetrics)

Note that, like other shaders which depend on the depth buffer, you'll need a directional light or other depth buffer activator (like disabled depth of field) to see the effect properly.

## UI is weird!

Probably will be fixed later.

## The default settings are broken!

Play around and see what works!

## License?

This work is licensed under the MIT license where applicable.
