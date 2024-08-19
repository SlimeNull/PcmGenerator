using System.Text;

namespace PcmGenerator
{
    public unsafe struct WaveHeader
    {
        private fixed byte _chunkId[4];
        private uint _chunkSize;
        private fixed byte _format[4];

        private fixed byte _subChunk1Id[4];
        private uint _subChunk1Size;
        private WaveAudioFormat _audioFormat;
        private ushort _numChannels;
        private uint _sampleRate;
        private uint _byteRate;
        private ushort _blockAlign;
        private ushort _bitsPerSample;

        private fixed byte _subChunk2Id[4];
        private uint _subChunk2Size;

        public unsafe string ChunkId
        {
            get
            {
                fixed (byte* ptr = _chunkId)
                {
                    return CreateString(ptr, 4);
                }
            }
            set
            {
                fixed (byte* ptr = _chunkId)
                {
                    FillString(ptr, value, 4);
                }
            }
        }

        public uint ChunkSize
        {
            get => _chunkSize;
            set => _chunkSize = value;
        }

        public string Format
        {
            get
            {
                fixed (byte* ptr = _format)
                {
                    return CreateString(ptr, 4);
                }
            }
            set
            {
                fixed (byte* ptr = _format)
                {
                    FillString(ptr, value, 4);
                }
            }
        }

        public string SubChunk1Id
        {
            get
            {
                fixed (byte* ptr = _subChunk1Id)
                {
                    return CreateString(ptr, 4);
                }
            }
            set
            {
                fixed (byte* ptr = _subChunk1Id)
                {
                    FillString(ptr, value, 4);
                }
            }
        }

        public uint SubChunk1Size
        {
            get => _subChunk1Size;
            set => _subChunk1Size = value;
        }

        public WaveAudioFormat AudioFormat
        {
            get => _audioFormat;
            set => _audioFormat = value;
        }

        public ushort ChannelCount
        {
            get => _numChannels;
            set => _numChannels = value;
        }

        public uint SampleRate
        {
            get => _sampleRate;
            set => _sampleRate = value;
        }

        public uint ByteRate
        {
            get => _byteRate;
            set => _byteRate = value;
        }

        public ushort BlockAlign
        {
            get => _blockAlign;
            set => _blockAlign = value;
        }

        public ushort BitsPerSample
        {
            get => _bitsPerSample;
            set => _bitsPerSample = value;
        }

        public string SubChunk2Id
        {
            get
            {
                fixed (byte* ptr = _subChunk2Id)
                {
                    return CreateString(ptr, 4);
                }
            }
            set
            {
                fixed (byte* ptr = _subChunk2Id)
                {
                    FillString(ptr, value, 4);
                }
            }
        }

        public uint SubChunk2Size
        {
            get => _subChunk2Size;
            set => _subChunk2Size = value;
        }

        public unsafe static WaveHeader Create(WaveAudioFormat audioFormat, ushort channelCount, uint sampleRate, ushort bitsPerSample, uint pcmDataSize)
        {
            return new WaveHeader()
            {
                ChunkId = "RIFF",
                ChunkSize = (uint)(pcmDataSize + (sizeof(WaveHeader) - 8)),
                Format = "WAVE",

                SubChunk1Id = "fmt ",
                SubChunk1Size = 16,
                AudioFormat = audioFormat,
                ChannelCount = channelCount,
                SampleRate = sampleRate,
                ByteRate = sampleRate * channelCount * bitsPerSample / 8,
                BlockAlign = (ushort)(channelCount * bitsPerSample / 8),
                BitsPerSample = bitsPerSample,

                SubChunk2Id = "data",
                SubChunk2Size = pcmDataSize
            };
        }

        private static void FillString(byte* ptr, string value, int maxLength)
        {
            if (value.Length > maxLength)
            {
                throw new ArgumentException(nameof(value));
            }

            fixed (char* textPtr = value)
            {
                for (int i = 0; i < value.Length && i < maxLength; i++)
                {
                    ptr[i] = (byte)textPtr[i];
                }
            }
        }

        private static string CreateString(byte* ptr, int maxLength)
        {
            StringBuilder sb = new StringBuilder(maxLength);
            for (int i = 0; i < maxLength; i++)
            {
                if (ptr[i] == 0)
                {
                    break;
                }

                sb.Append((char)ptr[i]);
            }

            return sb.ToString();
        }


    }
}