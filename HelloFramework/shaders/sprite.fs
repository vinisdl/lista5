#version 450 core
in vec4 ourColor;
in vec2 TexCoord;
out vec4 color;

// pixels da textura
uniform sampler2D tex1;
//uniform sampler2D tex2;



uniform bool onlyRed;
uniform bool isGray;
uniform bool isColorization;
uniform bool inversion;
uniform bool binary;
uniform bool isMix;

uniform float channel;

uniform vec4 colorization;



vec4 toGray(vec4 colorVec)
{
    float gray = dot(colorVec.rgb, vec3(0.299, 0.587, 0.114));
    return vec4(vec3(gray), 1.0);
}

vec4 Binarizacao()
{
    color = toGray(color);

    if(color.r < 0.5)
    {
        return vec4(0.0,0.0,0.0,1.0);
    }
        
    return vec4(1.0,1.0,1.0,1.0);
}

vec4 mixColor()
{
    if(channel == 0 )
    {
        color = pow(color, vec4(10,10,10,10));
    }
     else if( channel == 1 )
    {
        color = pow(color, vec4(2,2,2,2));
        // color.r = log2(color.r +1.0);
        // color.g = log2(color.g+1.0);
        // color.b = log2(color.b +1.0);
    } 

    return color ;
}



void main()
{
    vec4 colorVec = texture2D(tex1, TexCoord);
    color = colorVec;
    
    if(onlyRed)
    {
        color.g = color.r;
        color.g = color.b;
    }

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
    
    if(isColorization)
    {
        color = color * colorization;
    }
      
    if(binary)
    {        
       color = Binarizacao();
    }

    if(isMix)
    {
        color = mixColor();
    }

}

