using BackendProj.Hubs;
using BackendProj.Types;
using Microsoft.AspNetCore.SignalR;

namespace BackendProj
{
    public class AudioService
    {
        private IHubContext<MyHub> _hubContext;
        private System.Timers.Timer _timer;
        public AudioService(IHubContext<MyHub> hub) => _hubContext = hub;

        public async Task StartSending()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += async (s, e) => await SendAudio(@"D:/sound/01Label.wav");
            _timer.Start();
        }
        public async Task SendAudio(string path)
        {

            byte[]? audio = null;
            try
            {
                audio = File.ReadAllBytes(path);
            }
            catch
            {
                return;
            }
            await _hubContext.Clients.All.SendAsync("AudioReceived",
                new AudioReceivedEventArgs() { Audio = audio });
            _timer.Stop();
        }
    }
}
