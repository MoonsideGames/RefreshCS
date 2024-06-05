/* RefreshCS - C# bindings for the Refresh graphics Library
 *
 * Copyright (c) 2020-2024 Evan Hemsley
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 * Evan "cosmonaut" Hemsley <evan@moonside.games>
 *
 */

using System;
using System.Runtime.InteropServices;

namespace RefreshCS;

public unsafe static class Refresh
{
    private const string nativeLibName = "Refresh";

    /* Enums */

    public enum PrimitiveType
    {
        PointList,
        LineList,
        LineStrip,
        TriangleList,
        TriangleStrip
    }

    public enum LoadOp
    {
        Load,
        Clear,
        DontCare
    }

    public enum StoreOp
    {
        Store,
        DontCare
    }

    public enum IndexElementSize
    {
        Sixteen,
        ThirtyTwo
    }

    public enum TextureFormat
    {
	/* Unsigned Normalized Float Color Formats */
	R8G8B8A8,
	B8G8R8A8,
	R5G6B5,
	A1R5G5B5,
	B4G4R4A4,
	A2R10G10B10,
    A2B10G10R10,
	R16G16,
	R16G16B16A16,
	R8,
	A8,
	/* Compressed Unsigned Normalized Float Color Formats */
	BC1,
	BC2,
	BC3,
	BC7,
	/* Signed Normalized Float Color Formats  */
	R8G8_SNORM,
	R8G8B8A8_SNORM,
	/* Signed Float Color Formats */
	R16_SFLOAT,
	R16G16_SFLOAT,
	R16G16B16A16_SFLOAT,
	R32_SFLOAT,
	R32G32_SFLOAT,
	R32G32B32A32_SFLOAT,
	/* Unsigned Integer Color Formats */
	R8_UINT,
	R8G8_UINT,
	R8G8B8A8_UINT,
	R16_UINT,
	R16G16_UINT,
	R16G16B16A16_UINT,
	/* SRGB Color Formats */
	R8G8B8A8_SRGB,
	B8G8R8A8_SRGB,
	/* Compressed SRGB Color Formats */
	BC3_SRGB,
	BC7_SRGB,
	/* Depth Formats */
	D16_UNORM,
	D24_UNORM,
	D32_SFLOAT,
	D24_UNORM_S8_UINT,
	D32_SFLOAT_S8_UINT
    }

    [Flags]
    public enum TextureUsageFlags
    {
        Sampler = 0x1,
        ColorTarget = 0x2,
        DepthStencil = 0x4,
        GraphicsStorage = 0x8,
        ComputeStorageRead = 0x20,
        ComputeStorageWrite = 0x40
    }

    public enum TextureType
    {
        TwoD,
        ThreeD,
        Cube
    }

    public enum SampleCount
    {
        One,
        Two,
        Four,
        Eight
    }

    public enum CubeMapFace
    {
        PositiveX,
        NegativeX,
        PositiveY,
        NegativeY,
        PositiveZ,
        NegativeZ
    }

    [Flags]
    public enum BufferUsageFlags
    {
        Vertex = 0x1,
        Index = 0x2,
        Indirect = 0x4,
        GraphicsStorage = 0x8,
        ComputeStorageRead = 0x20,
        ComputeStorageWrite = 0x40
    }

    [Flags]
    public enum TransferBufferMapFlags
    {
        Read = 0x1,
        Write = 0x2
    }

    public enum ShaderStage
    {
        Vertex,
        Fragment,
        Compute
    }

    public enum ShaderFormat
    {
        Invalid,
        SPIRV,
        DXBC,
        DXIL,
        MSL,
        METALLIB,
        SECRET
    }

    public enum VertexElementFormat
    {
        Uint,
        Float,
        Vector2,
        Vector3,
        Vector4,
        Color,
        Byte4,
        Short2,
        Short4,
        NormalizedShort2,
        NormalizedShort4,
        HalfVector2,
        HalfVector4
    }

    public enum VertexInputRate
    {
        Vertex,
        Instance
    }

    public enum FillMode
    {
        Fill,
        Line
    }

    public enum CullMode
    {
        None,
        Front,
        Back
    }

    public enum FrontFace
    {
        CounterClockwise,
        Clockwise
    }

    public enum CompareOp
    {
        Never,
        Less,
        Equal,
        LessOrEqual,
        Greater,
        NotEqual,
        GreaterOrEqual,
        Always
    }

    public enum StencilOp
    {
        Keep,
        Zero,
        Replace,
        IncrementAndClamp,
        DecrementAndClamp,
        Invert,
        IncrementAndWrap,
        DecrementAndWrap
    }

    public enum BlendOp
    {
        Add,
        Subtract,
        ReverseSubtract,
        Min,
        Max
    }

    public enum BlendFactor
    {
        Zero,
        One,
        SourceColor,
        OneMinusSourceColor,
        DestinationColor,
        OneMinusDestinationColor,
        SourceAlpha,
        OneMinusSourceAlpha,
        DestinationAlpha,
        OneMinusDestinationAlpha,
        ConstantColor,
        OneMinusConstantColor,
        SourceAlphaSaturate
    }

    [Flags]
    public enum ColorComponentFlags
    {
        R = 0x1,
        G = 0x2,
        B = 0x4,
        A = 0x8
    }

    public enum Filter
    {
        Nearest,
        Linear
    }

    public enum SamplerMipmapMode
    {
        Nearest,
        Linear
    }

    public enum SamplerAddressMode
    {
        Repeat,
        MirroredRepeat,
        ClampToEdge,
        ClampToBorder
    }

    public enum BorderColor
    {
        FloatTransparentBlack,
        IntTransparentBlack,
        FloatOpaqueBlack,
        IntOpaqueBlack,
        FloatOpaqueWhite,
        IntOpaqueWhite
    }

    public enum TransferUsage
    {
        Buffer,
        Texture
    }

    public enum PresentMode
    {
        VSync,
        Immediate,
        Mailbox
    }

    public enum SwapchainComposition
    {
        SDR,
        SDRLinear,
        HDRExtendedLinear,
        HDR10_ST2084
    }

    [Flags]
    public enum BackendFlags
    {
        Invalid = 0x0,
        Vulkan = 0x1,
        D3D11 = 0x2,
        Metal = 0x4,
        All = Vulkan | D3D11 | Metal
    }

    /* Structures */

    public struct DepthStencilValue
    {
        public float Depth;
        public uint Stencil;
    }

    public struct Rect
    {
        public int X;
        public int Y;
        public int W;
        public int H;
    }

    public struct Color
    {
        public float R;
        public float G;
        public float B;
        public float A;
    }

    public struct Viewport
    {
        public float X;
        public float Y;
        public float W;
        public float H;
        public float MinDepth;
        public float MaxDepth;
    }

    public struct TextureSlice
    {
        public nint Texture;
        public uint MipLevel;
        public uint Layer;
    }

    public struct TextureRegion
    {
        public TextureSlice TextureSlice;
        public uint X;
        public uint Y;
        public uint Z;
        public uint W;
        public uint H;
        public uint D;
    }

    public struct BufferImageCopy
    {
        public uint BufferOffset;
        public uint BufferStride;
        public uint BufferImageHeight;
    }

    public struct BufferCopy
    {
        public uint SourceOffset;
        public uint DestinationOffset;
        public uint Size;
    }

    public struct IndirectDrawCommand
    {
        public uint VertexCount;
        public uint InstanceCount;
        public uint FirstVertex;
        public uint FirstInstance;
    }

    public struct IndexedIndirectDrawCommand
    {
        public uint IndexCount;
        public uint InstanceCount;
        public uint FirstIndex;
        public uint VertexOffset;
        public uint FirstInstance;
    }

    public struct SamplerCreateInfo
    {
        public Filter MinFilter;
        public Filter MagFilter;
        public SamplerMipmapMode MipmapMode;
        public SamplerAddressMode AddressModeU;
        public SamplerAddressMode AddressModeV;
        public SamplerAddressMode AddressModeW;
        public float MipLodBias;
        public int AnisotropyEnable; /* SDL_bool */
        public float MaxAnisotropy;
        public int CompareEnable; /* SDL_bool */
		public CompareOp CompareOp;
        public float MinLod;
        public float MaxLod;
        public BorderColor BorderColor;
    }

    public struct VertexBinding
    {
        public uint Binding;
        public uint Stride;
        public VertexInputRate InputRate;
        public uint StepRate;
    }

    public struct VertexAttribute
    {
        public uint Location;
        public uint Binding;
        public VertexElementFormat Format;
        public uint Offset;
    }

    public struct VertexInputState
    {
        public VertexBinding* VertexBindings;
        public uint VertexBindingCount;
        public VertexAttribute* VertexAttributes;
        public uint VertexAttributeCount;
    }

    public struct StencilOpState
    {
        public StencilOp FailOp;
        public StencilOp PassOp;
        public StencilOp DepthFailOp;
        public CompareOp CompareOp;
    }

    public struct ColorAttachmentBlendState
    {
        public int BlendEnable; /* SDL_bool */
        public BlendFactor SourceColorBlendFactor;
        public BlendFactor DestinationColorBlendFactor;
        public BlendOp ColorBlendOp;
        public BlendFactor SourceAlphaBlendFactor;
        public BlendFactor DestinationAlphaBlendFactor;
        public BlendOp AlphaBlendOp;
        public ColorComponentFlags ColorWriteMask;
    }

    public struct ShaderCreateInfo
    {
        public nuint CodeSize;
        public byte* Code;
        [MarshalAs(UnmanagedType.LPUTF8Str)]
        public string EntryPointName;
        public ShaderStage Stage;
        public ShaderFormat Format;
    }

    public struct TextureCreateInfo
    {
        public uint Width;
        public uint Height;
        public uint Depth;
        public int IsCube; /* SDL_bool */
        public uint LayerCount;
        public uint LevelCount;
        public SampleCount SampleCount;
        public TextureFormat Format;
        public TextureUsageFlags UsageFlags;
    }

    public struct RasterizerState
    {
        public FillMode FillMode;
        public CullMode CullMode;
        public FrontFace FrontFace;
        public int DepthBiasEnable; /* SDL_bool */
        public float DepthBiasConstantFactor;
        public float DepthBiasClamp;
        public float DepthBiasSlopeFactor;
    }

    public struct MultisampleState
    {
        public SampleCount MultisampleCount;
        public uint SampleMask;
    }

    public struct DepthStencilState
    {
        public int DepthTestEnable; /* SDL_bool */
        public int DepthWriteEnable; /* SDL_bool */
        public CompareOp CompareOp;
        public int DepthBoundsTestEnable; /* SDL_bool */
        public int StencilTestEnable; /* SDL_bool */
        public StencilOpState BackStencilState;
        public StencilOpState FrontStencilState;
        public uint CompareMask;
        public uint WriteMask;
        public uint Reference;
        public float MinDepthBounds;
        public float MaxDepthBounds;
    }

    public struct ColorAttachmentDescription
    {
        public TextureFormat Format;
        public ColorAttachmentBlendState BlendState;
    }

    public struct GraphicsPipelineAttachmentInfo
    {
        public ColorAttachmentDescription* ColorAttachmentDescriptions;
        public uint ColorAttachmentCount;
        public int HasDepthStencilAttachment; /* SDL_bool */
        public TextureFormat DepthStencilFormat;
    }

    public struct GraphicsPipelineResourceInfo
    {
        public uint SamplerCount;
        public uint StorageBufferCount;
        public uint StorageTextureCount;
        public uint UniformBufferCount;
    }

    public struct GraphicsPipelineCreateInfo
    {
        public nint VertexShader;
        public nint FragmentShader;
        public VertexInputState VertexInputState;
        public PrimitiveType PrimitiveType;
        public RasterizerState RasterizerState;
        public MultisampleState MultisampleState;
        public DepthStencilState DepthStencilState;
        public GraphicsPipelineAttachmentInfo AttachmentInfo;
        public GraphicsPipelineResourceInfo VertexResourceInfo;
        public GraphicsPipelineResourceInfo FragmentResourceInfo;
        public fixed float BlendConstants[4];
    }

    public struct ComputePipelineResourceInfo
    {
        public uint ReadOnlyStorageTextureCount;
        public uint ReadOnlyStorageBufferCount;
        public uint ReadWriteStorageTextureCount;
        public uint ReadWriteStorageBufferCount;
        public uint UniformBufferCount;
    }

    public struct ComputePipelineCreateInfo
    {
        public nint ComputeShader;
        public ComputePipelineResourceInfo PipelineResourceInfo;
    }

    public struct ColorAttachmentInfo
    {
        public TextureSlice TextureSlice;
        public Color ClearColor;
        public LoadOp LoadOp;
        public StoreOp StoreOp;
        public int Cycle; /* SDL_bool */
    }

    public struct DepthStencilAttachmentInfo
    {
        public TextureSlice TextureSlice;
        public DepthStencilValue DepthStencilClearValue;
        public LoadOp LoadOp;
        public StoreOp StoreOp;
        public LoadOp StencilLoadOp;
        public StoreOp StencilStoreOp;
        public int Cycle; /* SDL_bool */
    }

    public struct BufferBinding
    {
        public nint Buffer;
        public uint Offset;
    }

    public struct TextureSamplerBinding
    {
        public nint Texture;
        public nint Sampler;
    }

    public struct StorageBufferReadWriteBinding
    {
        public nint Buffer;
        public int Cycle; /* SDL_bool */
    }

    public struct StorageTextureReadWriteBinding
    {
        public TextureSlice TextureSlice;
        public int Cycle; /* SDL_bool */
    }

    /* Functions */

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_CreateDevice(
        BackendFlags preferredBackends,
        int debugMode /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_DestroyDevice(
        nint device
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern BackendFlags Refresh_GetBackend(
        nint device
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_CreateComputePipeline(
        nint device,
        in ComputePipelineCreateInfo computePipelineCreateInfo
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_CreateGraphicsPipeline(
        nint device,
        in GraphicsPipelineCreateInfo graphicsPipelineCreateInfo
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_CreateSampler(
        nint device,
        in SamplerCreateInfo samplerCreateInfo
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_CreateShader(
        nint device,
        in ShaderCreateInfo shaderCreateInfo
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_CreateTexture(
        nint device,
        in TextureCreateInfo textureCreateInfo
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_CreateBuffer(
        nint device,
        BufferUsageFlags usageFlags,
        uint sizeInBytes
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_CreateTransferBuffer(
        nint device,
        TransferUsage usage,
        TransferBufferMapFlags mapFlags,
        uint sizeInBytes
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_CreateOcclusionQuery(
        nint device
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_SetBufferName(
        nint device,
        nint buffer,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string text
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_SetTextureName(
        nint device,
        nint texture,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string text
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_ReleaseTexture(
        nint device,
        nint texture
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_ReleaseSampler(
        nint device,
        nint sampler
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_ReleaseBuffer(
        nint device,
        nint buffer
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_ReleaseTransferBuffer(
        nint device,
        nint transferBuffer
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_ReleaseShader(
        nint device,
        nint shader
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_ReleaseGraphicsPipeline(
        nint device,
        nint graphicsPipeline
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_ReleaseComputePipeline(
        nint device,
        nint computePipeline
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_ReleaseOcclusionQuery(
        nint device,
        nint query
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_BeginRenderPass(
        nint commandBuffer,
        ColorAttachmentInfo* colorAttachmentInfos,
        uint colorAttachmentCount,
        DepthStencilAttachmentInfo* depthStencilAttachmentInfo
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_BindGraphicsPipeline(
        nint renderPass,
        nint graphicsPipeline
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_SetViewport(
        nint renderPass,
        in Viewport viewport
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_SetScissor(
        nint renderPass,
        in Rect scissor
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_BindVertexBuffers(
        nint renderPass,
        uint firstBinding,
        BufferBinding *bindings,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_BindIndexBuffer(
        nint renderPass,
        in BufferBinding binding,
        IndexElementSize indexElementSize
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_BindVertexSamplers(
        nint renderPass,
        uint firstSlot,
        TextureSamplerBinding* textureSamplerBindings,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_BindVertexStorageTextures(
        nint renderPass,
        uint firstSlot,
        TextureSlice *storageTextureSlices,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_BindVertexStorageBuffers(
        nint renderPass,
        uint firstSlot,
        nint* storageBuffers,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_BindFragmentSamplers(
        nint renderPass,
        uint firstSlot,
        TextureSamplerBinding* textureSamplerBindings,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_BindFragmentStorageTextures(
        nint renderPass,
        uint firstSlot,
        TextureSlice *storageTextureSlices,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_BindFragmentStorageBuffers(
        nint renderPass,
        uint firstSlot,
        nint* storageBuffers,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_PushVertexUniformData(
        nint renderPass,
        uint slotIndex,
        nint data,
        uint dataLengthInBytes
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_PushFragmentUniformData(
        nint renderPass,
        uint slotIndex,
        nint data,
        uint dataLengthInBytes
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_DrawIndexedPrimitives(
        nint renderPass,
        uint baseVertex,
        uint startIndex,
        uint primitiveCount,
        uint instanceCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_DrawPrimitives(
        nint renderPass,
        uint vertexStart,
        uint primitiveCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_DrawPrimitivesIndirect(
        nint renderPass,
        nint buffer,
        uint offsetInBytes,
        uint drawCount,
        uint stride
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_DrawIndexedPrimitivesIndirect(
        nint renderPass,
        nint buffer,
        uint offsetInBytes,
        uint drawCount,
        uint stride
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_EndRenderPass(
        nint renderPass
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_BeginComputePass(
        nint commandBuffer,
        StorageTextureReadWriteBinding* storageTextureBindings,
        uint storageTextureBindingCount,
        StorageBufferReadWriteBinding* storageBufferBindings,
        uint storageBufferBindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_BindComputePipeline(
        nint computePass,
        nint computePipeline
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_BindComputeStorageTextures(
        nint computePass,
        uint firstSlot,
        TextureSlice* storageTextureSlices,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_BindComputeStorageBuffers(
        nint computePass,
        uint firstSlot,
        nint* storageBuffers,
        uint bindingCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_PushComputeUniformData(
        nint computePass,
        uint slotIndex,
        nint data,
        uint dataLengthInBytes
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_DispatchCompute(
        nint computePass,
        uint groupCountX,
        uint groupCountY,
        uint groupCountZ
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_EndComputePass(
        nint computePass
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_MapTransferBuffer(
        nint device,
        nint transferBuffer,
        int cycle, /* SDL_bool */
        out nint ppData
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_UnmapTransferBuffer(
        nint device,
        nint transferBuffer
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_SetTransferData(
        nint device,
        nint data,
        nint transferBuffer,
        in BufferCopy copyParams,
        int cycle /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_GetTransferData(
        nint device,
        nint transferBuffer,
        nint data,
        in BufferCopy copyParams
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_BeginCopyPass(
        nint commandBuffer
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_UploadToTexture(
        nint copyPass,
        nint transferBuffer,
        in TextureRegion textureRegion,
        in BufferImageCopy copyParams,
        int cycle /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_UploadToBuffer(
        nint copyPass,
        nint transferBuffer,
        nint buffer,
        in BufferCopy copyParams,
        int cycle /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_CopyTextureToTexture(
        nint copyPass,
        in TextureRegion source,
        in TextureRegion destination,
        int cycle /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_CopyBufferToBuffer(
        nint copyPass,
        nint source,
        nint destination,
        in BufferCopy copyParams,
        int cycle /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void SDL_GenerateMipmaps(
        nint copyPass,
        nint texture
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_DownloadFromTexture(
        nint copyPass,
        in TextureRegion textureRegion,
        nint transferBuffer,
        in BufferImageCopy copyParams
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_DownloadFromBuffer(
        nint copyPass,
        nint buffer,
        nint transferBuffer,
        in BufferCopy copyParams
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_EndCopyPass(
        nint copyPass
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_Blit(
        nint commandBuffer,
        in TextureRegion source,
        in TextureRegion destination,
        Filter filterMode,
        int cycle /* SDL_bool */
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Refresh_SupportsSwapchainComposition(
        nint device,
        nint window,
        SwapchainComposition swapchainComposition
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Refresh_SupportsPresentMode(
        nint device,
        nint window,
        PresentMode presentMode
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Refresh_ClaimWindow(
        nint device,
        nint window,
        SwapchainComposition swapchainComposition,
        PresentMode presentMode
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_UnclaimWindow(
        nint device,
        nint window
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_SetSwapchainParameters(
        nint device,
        nint window,
        SwapchainComposition swapchainComposition,
        PresentMode presentMode
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern TextureFormat Refresh_GetSwapchainTextureFormat(
        nint device,
        nint window
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_AcquireCommandBuffer(
        nint device
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_AcquireSwapchainTexture(
        nint commandBuffer,
        nint window,
        out uint width,
        out uint height
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_Submit(
        nint commandBuffer
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern nint Refresh_SubmitAndAcquireFence(
        nint commandBuffer
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_Wait(
        nint device
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_WaitForFences(
        nint device,
        int waitAll, /* SDL_bool */
        uint fenceCount,
        nint* fences
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Refresh_QueryFence(
        nint device,
        nint fence
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_ReleaseFence(
        nint device,
        nint fence
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern uint Refresh_TextureFormatTexelBlockSize(
        TextureFormat textureFormat
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Refresh_IsTextureFormatSupported(
        nint device,
        TextureFormat format,
        TextureType type,
        TextureUsageFlags usage
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern SampleCount Refresh_GetBestSampleCount(
        nint device,
        TextureFormat format,
        SampleCount desiredSampleCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_OcclusionQueryBegin(
        nint commandBuffer,
        nint query
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_OcclusionQueryEnd(
        nint commandBuffer,
        nint query
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Refresh_OcclusionQueryPixelCount(
        nint device,
        nint query,
        out uint pixelCount
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern byte* Refresh_Image_Load(
        byte* bufferPtr,
        int bufferLength,
        out int w,
        out int h,
        out int len
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int Refresh_Image_Info(
        byte* bufferPtr,
        int bufferLength,
        out int w,
        out int h,
        out int len
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_Image_Free(
        byte* bufferPtr
    );

    [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
    public static extern void Refresh_Image_SavePNG(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string filename,
        byte* dataPtr,
        int width,
        int height
    );
}
