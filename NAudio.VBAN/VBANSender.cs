using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using NAudio.Wave;

namespace NAudio.VBAN
{
    public class VBANSender : IDisposable, ISampleProvider
    {
        private string DestHost { get; }
        private readonly ISampleProvider _source;
        private readonly string _streamName;
        private readonly UdpClient _udpClient;
        private uint _framecount;

        public WaveFormat WaveFormat => this._source.WaveFormat;

        public VBANSender(ISampleProvider source, string destHost, int destPort, string streamName)
        {
            DestHost = destHost;
            _source = source;
            _streamName = streamName;
            _udpClient = new UdpClient(DestHost, destPort);
        }

        public void Dispose()
        {
            _udpClient.Close();
        }

        public int Read(float[] buffer, int offset, int count)
        {
            var readed = _source.Read(buffer, offset, count);

            if (readed > 0)
                Sent(buffer, offset, readed);
            return readed;
        }

        private void Sent(IEnumerable<float> buffer, int offset, int readed)
        {
            var samples = buffer.Skip(offset).Take(readed).ToArray();
            var _count = 256;
            for (int i = 0; i < samples.Length / _count + (samples.Length % _count == 0 ? 0 : 1); i++)
            {
                var take = samples.Skip(_count * i).Count() > _count;
                SentUdp(samples.Skip(_count * i).Take(take ? _count : samples.Skip(_count * i).Count()).ToArray());
            }
        }

        private void SentUdp(IReadOnlyCollection<float> samples)
        {
            IList<byte> sendBytes = new List<byte>();
            sendBytes.Add((byte)'V');//F
            sendBytes.Add((byte)'B');//O
            sendBytes.Add((byte)'A');//U
            sendBytes.Add((byte)'N');//R
            sendBytes.Add((byte)((int)VBanProtocol.VBAN_PROTOCOL_AUDIO << 5 | Array.IndexOf(VBANConsts.VBAN_SRList, WaveFormat.SampleRate)));//SR+Protocol
            sendBytes.Add((byte)(samples.Count / WaveFormat.Channels - 1));//Number of samples 
            sendBytes.Add((byte)(WaveFormat.Channels - 1));//Number of channels
            sendBytes.Add((int)VBanCodec.VBAN_CODEC_PCM << 5 | 0 << 4 | (byte)VBanBitResolution.VBAN_BITFMT_16_INT);//DataFormat+1bit pad+CODEC
            for (var i = 0; i < 16; i++)//StreamName char x 16
            {
                if (_streamName.ToCharArray().Length > i)
                {
                    sendBytes.Add((byte)_streamName.ToCharArray()[i]);
                }
                else
                {
                    sendBytes.Add((byte)0);//Number of samples 
                }
            }
            var fc = BitConverter.GetBytes(_framecount);
            for (var i = 0; i < 4; i++)//FrameCounter 32bits
            {
                sendBytes.Add(fc[i]);//temp padding
            }

            /* // for 32bit float out
            var byteArray = new byte[samples.Length * 4];
            Buffer.BlockCopy(samples, 0, byteArray, 0, byteArray.Length);
            foreach (var b in byteArray)
            {
                sendBytes.Add(b);
            }
            */

            foreach (var s in samples)// 16bit int out
            {
                var f = s;
                f = f * 32768;
                if (f > 32767) f = 32767;
                if (f < -32768) f = -32768;
                var i = (short)f;

                var bita = BitConverter.GetBytes(i);
                foreach (var b in Reverse(bita, Endian.Little))
                {
                    sendBytes.Add(b);
                }
            }
            _udpClient.Send(sendBytes.ToArray(), sendBytes.Count);
            _framecount++;
        }

        private static IEnumerable<byte> Reverse(IEnumerable<byte> bytes, Endian endian)
        {
            if (BitConverter.IsLittleEndian ^ endian == Endian.Little)
                return bytes.Reverse().ToArray();
            return bytes;
        }
    }

    internal enum Endian
    {
        Little, Big
    }
}
