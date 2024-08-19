using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PcmGenerator
{

    public class PcmBuilder
    {
        private PcmBuilder() { }

        public float SampleRate { get; set; } = 44100;
        public float Frequency { get; set; } = 4000;
        public float Amplitude { get; set; } = 1;

        public PcmBuilder WithSampleRate(long sampleRate)
        {
            SampleRate = sampleRate;
            return this;
        }

        public PcmBuilder WithFrequency(float frequency)
        {
            Frequency = frequency;
            return this;
        }

        public PcmBuilder WithAmplitude(float amplitude)
        {
            Amplitude = amplitude;
            return this;
        }

        public IEnumerable<float> Build()
        {
            float current = 0;
            while (true)
            {
                yield return MathF.Sin(current / SampleRate * Frequency) * Amplitude;

                current++;
            }
        }

        public static PcmBuilder CreateNew() => new PcmBuilder();
    }
}