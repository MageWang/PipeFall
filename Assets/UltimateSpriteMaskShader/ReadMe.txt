Product:
Ultimate Sprite Mask Shader

Description:
This asset  is a complex sprite shader that overlays the main texture from SpriteRenderer component with custom color or alpha texture.
Additionally, you can adjust opacity and tint of overlaying texture with slider.
Alpha channel of the custom texture is being added to alpha channel of main texture.

Alpha Mask Mode:
Custom texture can acts as the alpha mask masking the main texture. 
In this mode alpha channel of the custom texture is being calculated like in normal mode, but RGB channels are not overlaying main texture.
If you want to use classic black&white mask textures you need to choose "From Gray Scale" as Alpha Source in texture import settings.

Static Texture Mode:
Custom texture can be projected in the screen space instead of the object space.
This function can be used to achieve static texture/pattern effect.
(Added texture will retain the same orientation regardless of the positioning of the character)

Usage:
Simply set shader (Sprites/SpriteMask) to material and add desired texture.
Important: If you want to use static texture mode make sure that custom texture has Wrap Mode set to "Repeat".
Note: Sprites from sprite atlases can't be used as textures in this shader. 
 
Video: https://youtu.be/xAW7lWkRRnE

Please review this asset on asset store - http://u3d.as/KZr

