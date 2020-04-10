namespace Heirloom.OpenGLES
{
    public enum ActiveUniformType : uint
    {
        Float = 0x1406,
        Integer = 0x1404,
        UnsignedInteger = 0x1405,
        Bool = 0x8B56,

        FloatVec2 = 0x8B50,
        Matrix2 = 0x8B5A,
        Matrix2x3 = 0x8B65,
        Matrix2x4 = 0x8B66,

        FloatVec3 = 0x8B51,
        Matrix3x2 = 0x8B67,
        Matrix3 = 0x8B5B,
        Matrix3x4 = 0x8B68,

        FloatVec4 = 0x8B52,
        Matrix4x2 = 0x8B69,
        Matrix4x3 = 0x8B6A,
        Matrix4 = 0x8B5C,

        IntVec2 = 0x8B53,
        IntVec3 = 0x8B54,
        IntVec4 = 0x8B55,

        UnsignedIntVec2 = 0x8DC6,
        UnsignedIntVec3 = 0x8DC7,
        UnsignedIntVec4 = 0x8DC8,

        BoolVec2 = 0x8B57,
        BoolVec3 = 0x8B58,
        BoolVec4 = 0x8B59,

        Sampler2D = 0x8B5E,
        Sampler2DArray = 0x8DC1,
        SamplerCube = 0x8B60,
        Sampler3D = 0x8B5F,
    }
}
