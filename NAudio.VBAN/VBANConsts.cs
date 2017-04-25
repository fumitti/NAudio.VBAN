using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudio.VBAN
{
    public static class VBANConsts
    {
        public static readonly int[] VBAN_SRList = { 6000, 12000, 24000, 48000, 96000, 192000, 384000, 8000, 16000, 32000, 64000, 128000, 256000, 512000, 11025, 22050, 44100, 88200, 176400, 352800, 705600 };

    }

    public enum VBanBitResolution
    {
        VBAN_BITFMT_8_INT = 0,
        VBAN_BITFMT_16_INT,
        VBAN_BITFMT_24_INT,
        VBAN_BITFMT_32_INT,
        VBAN_BITFMT_32_FLOAT,
        VBAN_BITFMT_64_FLOAT,
        VBAN_BITFMT_12_INT,
        VBAN_BITFMT_10_INT,
    }
    public enum VBanCodec
    {
        VBAN_CODEC_PCM = 0x00,
        VBAN_CODEC_VBCA = 0x10,
        VBAN_CODEC_VBCV = 0x20,
        VBAN_CODEC_UNDEFINED_3 = 0x30,
        VBAN_CODEC_UNDEFINED_4 = 0x40,
        VBAN_CODEC_UNDEFINED_5 = 0x50,
        VBAN_CODEC_UNDEFINED_6 = 0x60,
        VBAN_CODEC_UNDEFINED_7 = 0x70,
        VBAN_CODEC_UNDEFINED_8 = 0x80,
        VBAN_CODEC_UNDEFINED_9 = 0x90,
        VBAN_CODEC_UNDEFINED_10 = 0xA0,
        VBAN_CODEC_UNDEFINED_11 = 0xB0,
        VBAN_CODEC_UNDEFINED_12 = 0xC0,
        VBAN_CODEC_UNDEFINED_13 = 0xD0,
        VBAN_CODEC_UNDEFINED_14 = 0xE0,
        VBAN_CODEC_USER = 0xF0
    }
    public enum VBanProtocol
    {
        VBAN_PROTOCOL_AUDIO = 0x00,
        VBAN_PROTOCOL_SERIAL = 0x20,
        VBAN_PROTOCOL_TXT = 0x40,
        VBAN_PROTOCOL_UNDEFINED_1 = 0x80,
        VBAN_PROTOCOL_UNDEFINED_2 = 0xA0,
        VBAN_PROTOCOL_UNDEFINED_3 = 0xC0,
        VBAN_PROTOCOL_UNDEFINED_4 = 0xE0
    }

    public enum VBanQuality
    {
        VBAN_QUALITY_OPTIMAL = 512,
        VBAN_QUALITY_FAST = 1024,
        VBAN_QUALITY_MEDIUM = 2048,
        VBAN_QUALITY_SLOW = 4096,
        VBAN_QUALITY_VERYSLOW = 8192,
    }
}
