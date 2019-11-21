using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LandauMedia.Telemetry.Internal
{
    class UdpTelemeterImpl : ITelemeterImpl
    {
        readonly UdpClient _client;

        public UdpTelemeterImpl(string host, int port)
        {
            _client = new UdpClient();
            var entry = Dns.GetHostEntry(host);
            _client.Connect(entry.AddressList[0], port);
        }

        public Action<Lazy<string>> GetCounterIncrementByOne()
        {
            return lazyName =>
            {
                var name = lazyName.Value + ":1|c";
                var data = Encoding.UTF8.GetBytes(name);
                _client.Send(data, data.Length);
            };
        }

        public Action<Lazy<string>> GetCounterDecrementByOne()
        {
            return lazyName =>
            {
                var name = lazyName.Value + ":-1|c";
                var data = Encoding.UTF8.GetBytes(name);
                _client.Send(data, data.Length);
            };
        }

        public Action<Lazy<string>, int> GetCounterIncrement()
        {
            return (lazyName, value) =>
            {
                var name = lazyName.Value + ":" + value + "|c";
                var bytes = Encoding.UTF8.GetBytes(name);
                _client.Send(bytes, bytes.Length);
            };
        }

        public Action<Lazy<string>, int> GetCounterDecrement()
        {
            return (lazyName, value) =>
            {
                var name = lazyName.Value + ":" + -value + "|c";
                var bytes = Encoding.UTF8.GetBytes(name);
                _client.Send(bytes, bytes.Length);
            };
        }

        public Action<Lazy<string>, long> GetTiming()
        {
            return (lazyName, ms) =>
            {
                var name = lazyName.Value + ":" + ms + "|ms";
                var bytes = Encoding.UTF8.GetBytes(name);
                _client.Send(bytes, bytes.Length);
            };
        }

        public Action<Lazy<string>, long> GetGauge()
        {
            return (lazyName, value) =>
            {
                var name = lazyName.Value + ":" + value + "|g";
                var bytes = Encoding.UTF8.GetBytes(name);
                _client.Send(bytes, bytes.Length);
            };
        }

    }
}