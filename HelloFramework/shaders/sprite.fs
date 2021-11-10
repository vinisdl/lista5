#version 450 core
in vec4 ourColor;
in vec2 TexCoord;
out vec4 color;

// pixels da textura
uniform sampler2D tex1;
//uniform sampler2D tex2;

uniform bool isGray;
uniform float colorization;
uniform bool inversion;
uniform bool binary;

float BinaryCheck(float color){
    if(color > 85)
        return 0;
    return 1;
}

vec4 toGray(vec4 colorVec)
{
    float gray = dot(colorVec.rgb, vec3(0.299, 0.587, 0.114));
    return vec4(gray,gray,gray, 1.0);
}

void main()
{
    vec4 colorVec = texture2D(tex1, TexCoord);
    color = colorVec;
    
    if(isGray)
    {
        color = toGray(color);
    }

    
    if(inversion) 
    {
        vec3 invert = vec3(0.0, 0.1215686275, 0.2039215686);
        color.rgb = 1.0 - color.rgb;
        color.rgb = color.rgb + invert;       
    }
    

    //if(binary)
    //{        
    //    color.r = BinaryCheck(color.r);
    //    color.g = BinaryCheck(color.g);
    //    color.b = BinaryCheck(color.b);
    //}

}

