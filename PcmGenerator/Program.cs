using PcmGenerator;

var pcm1Builder = PcmBuilder.CreateNew()
    .WithSampleRate(44100)
    .WithFrequency(8000);

var pcm2Builder = PcmBuilder.CreateNew()
    .WithSampleRate(44100)
    .WithFrequency(3456);

IEnumerable<float> CombinePcm(IEnumerable<float> numbers1, IEnumerable<float> numbers2)
{
    var enumerator1 = numbers1.GetEnumerator();
    var enumerator2 = numbers2.GetEnumerator();

    while (enumerator1.MoveNext() && enumerator2.MoveNext())
    {
        yield return enumerator1.Current / 2 + enumerator2.Current / 2;
    }
}

using var output1 = File.Create("output1.wav");
using var output2 = File.Create("output2.wav");
using var output3 = File.Create("output3.wav");

using var writer1 = new BinaryWriter(output1);
using var writer2 = new BinaryWriter(output2);
using var writer3 = new BinaryWriter(output3);

var header = WaveHeader.Create(WaveAudioFormat.PCM, 1, 44100, 32, 44100 * 32);
unsafe
{
    var headerByteSpan = new Span<byte>((byte*)&header, sizeof(WaveHeader));
    writer1.Write(headerByteSpan);
    writer2.Write(headerByteSpan);
    writer3.Write(headerByteSpan);
}

var pcm1 = pcm1Builder.Build();
var pcm2 = pcm2Builder.Build();
var pcm3 = CombinePcm(pcm1Builder.Build(), pcm2Builder.Build());

var pcm1Enumerator = pcm1.GetEnumerator();
var pcm2Enumerator = pcm2.GetEnumerator();
var pcm3Enumerator = pcm3.GetEnumerator();

for (int i = 0; i < 44100 * 30; i++)
{
    pcm1Enumerator.MoveNext();
    pcm2Enumerator.MoveNext();
    pcm3Enumerator.MoveNext();

    writer1.Write(pcm1Enumerator.Current);
    writer2.Write(pcm2Enumerator.Current);
    writer3.Write(pcm3Enumerator.Current);
}
