using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.WebSockets;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Stream_Pairs
{
    public partial class StreamPairs : Form
    {
        public string MirrorLink;
        private readonly ClientWebSocket wsClient;
        private bool IsConnected = false;
        private string TwitchChannel;
        private int Hits;
        private int Goal;
        private int RecordLevel;
        private List<string> Matches;
        private List<string> Answers;

        public StreamPairs()
        {
            InitializeComponent();
            Hits = 0;
            Goal = 0;
            RecordLevel = 0;
            MirrorLink = "";
            TwitchChannel = "https://twitch.tv/";
            Matches = Answers = new List<string>();
            wsClient = new ClientWebSocket();
        }

        private async void ConnectBtn_click(object sender, EventArgs e)
        {
            if (IsConnected)
            {
                await DisconnectFromMirrorLink(false);
                ConnectBtn.Text = "Connect";
                IsConnected = false;
                MirrorLinkTextBox.Enabled = true;
            }

            else
            {
                MirrorLink = MirrorLinkTextBox.Text;
                _ = ConnectToMirrorLink($"wss://memory.gartic.es/socket.io/?uid={MirrorLink.Split('#')[1]}&EIO=3&transport=websocket");
                ConnectBtn.Text = "Disconnect";
                IsConnected = true;
                MirrorLinkTextBox.Enabled = false;
            }
        }

        public async Task ConnectToMirrorLink(string url)
        {
            var uri = new Uri(url);
            await wsClient.ConnectAsync(uri, CancellationToken.None);

            while (wsClient.State == WebSocketState.Open)
            {
                var buffer = new ArraySegment<byte>(new byte[4096]);
                var result = await wsClient.ReceiveAsync(buffer, CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    MatchesBox.Text += message + Environment.NewLine;
                    ParseJson(message);
                }
            }
        }

        public async Task DisconnectFromMirrorLink(bool ShouldDispose = false)
        {
            if (wsClient.State == WebSocketState.Open)
                await wsClient.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
            if (ShouldDispose) wsClient.Dispose();
        }

        public async Task SendMessage(string msg)
        {
            var messageBytes = Encoding.UTF8.GetBytes(msg);
            await wsClient.SendAsync(
                new ArraySegment<byte>(messageBytes),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None
            );
        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (wsClient.State == WebSocketState.Open)
                await DisconnectFromMirrorLink(true);
        }

        private void MirrorLinkTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MirrorLinkTextBox.Text.Length == 70) ConnectBtn.Enabled = true;
            else ConnectBtn.Enabled = false;
        }

        private void ParseJson(string json)
        {
            if (!json.StartsWith("42")) return;
            JArray datapacket = JArray.Parse(json.Substring(2));

            // Initial data
            if ((int)datapacket[1] == 0)
            {
                JObject data = (JObject)datapacket[2];
                Hits = JArray.Parse(data["hits"].ToString()).Count;
                Goal = int.Parse(data["goal"].ToString());
                RecordLevel = int.Parse(data["record"].ToString());

                ChannelName.Text = (string)data["name"];
                LevelNumber.Text = data["level"].ToString();
                DeckName.Text = (string)data["deck"]["name"];
                RecordLabel.Text = RecordLevel.ToString();
                GoalLabel.Text = $"{Hits} / {Goal}";
                TwitchChannel = $"https://twitch.tv/{data["name"]}";
                TwitchChannelBtn.Enabled = true;
            }

            // Round Initiate
            //else if (eventCode == 1)
            //{
            //    JObject data = (JObject)datapacket[2];
            //    Goal = (int)data["goal"];
            //    Hits = 0;
            //    GoalLabel.Text = $"{Hits} / {Goal}";
            //}

            // Slot Reveal
            //else if (eventCode == 4)
            //{
            //    JArray data = JArray.Parse(datapacket[2].ToString());
            //    MatchesBox.Text += data[0].Value<string>() + Environment.NewLine;
            //}

            // Correct Guess
            //else if (eventCode == 5)
            //{
            //    Hits++;
            //    GoalLabel.Text = $"{Hits} / {Goal}";
            //}

            // New Round
            //else if (eventCode == 8)
            //{
            //    JObject data = (JObject)datapacket[2];
            //    int currentLevel = (int)data["level"];
            //    if (currentLevel > RecordLevel)
            //    {
            //        RecordLevel = currentLevel;
            //        RecordLabel.Text = RecordLevel.ToString();
            //    }

            //    LevelNumber.Text = currentLevel.ToString();
            //    DeckName.Text = (string)data["deck"]["name"];
            //}
        }

        private void TwitchChannelBtn_Click(object sender, EventArgs e)
        {
            Process.Start(TwitchChannel);
        }
    }
}
