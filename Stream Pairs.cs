using System;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.WebSockets;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Stream_Pairs
{
    public partial class StreamPairs : Form
    {
        public string MirrorLink;
        private readonly ClientWebSocket wsClient;
        private bool IsConnected = false;
        private string TwitchChannel;
        private string Deck;
        private int Hits;
        private int Goal;
        private int RecordLevel;
        private int Level;
        private Dictionary<string, List<string>> Matches;
        private List<string> Answers;
        private readonly System.Timers.Timer HeartBeat;

        public StreamPairs()
        {
            InitializeComponent();
            Hits = 0;
            Goal = 0;
            RecordLevel = 0;
            Level = 0;
            MirrorLink = "";
            TwitchChannel = "https://twitch.tv/";
            Matches = new Dictionary<string, List<string>>();
            Answers = new List<string>();
            wsClient = new ClientWebSocket();
            HeartBeat = new System.Timers.Timer(25000);
            HeartBeat.Elapsed += async (sender, e) =>
            {
                if (wsClient.State == WebSocketState.Open)
                    await SendMessage("2");
            };
        }

        private async void ConnectBtn_click(object sender, EventArgs e)
        {
            if (IsConnected)
            {
                await DisconnectFromMirrorLink(false);
                ConnectBtn.Text = "Connect";
                IsConnected = false;
                MirrorLinkTextBox.Enabled = true;
                ChannelName.Text = "None";
                DeckName.Text = "Unknown";
                LevelNumber.Text = "Unknown";
                RecordLabel.Text = "Unknown";
                GoalLabel.Text = "Unknown";
                MatchesBox.Text = "";
                TwitchChannelBtn.Enabled = false;
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

            HeartBeat.Start();

            while (wsClient.State == WebSocketState.Open)
            {
                var buffer = new ArraySegment<byte>(new byte[8196]);
                var result = await wsClient.ReceiveAsync(buffer, CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                    ParseJson(message);
                }
            }
        }

        public async Task DisconnectFromMirrorLink(bool ShouldDispose = false)
        {
            HeartBeat.Stop();
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
            JArray DataPacket = JArray.Parse(json.Substring(2));

            int EventCode = (int)DataPacket[1];

            // Initial data
            if (EventCode == 0)
            {
                JObject data = (JObject)DataPacket[2];
                if (data.ContainsKey("hits")) Hits = JArray.Parse(data["hits"].ToString()).Count / 2;
                else Hits = 0;

                if (data.ContainsKey("goal")) Goal = int.Parse(data["goal"].ToString());
                else Goal = 0;

                if (data.ContainsKey("level")) Level = int.Parse(data["level"].ToString());
                else Level = 1;

                if (data.ContainsKey("deck"))
                {
                    string deck = data["deck"]["name"].ToString();
                    DeckName.Text = char.ToUpper(deck[0]) + deck.Substring(1);
                }

                RecordLevel = int.Parse(data["record"].ToString());
                RecordLabel.Text = RecordLevel.ToString();
                LevelNumber.Text = Level.ToString();
                ChannelName.Text = data["name"].ToString();
                
                GoalLabel.Text = Goal == 0 ? "Unknown" : $"{Hits} / {Goal}";
                TwitchChannel = $"https://twitch.tv/{data["name"]}";
                TwitchChannelBtn.Enabled = true;
            }

            // Round Initiate
            else if (EventCode == 1)
            {
                JObject data = (JObject)DataPacket[2];
                Goal = int.Parse(data["goal"].ToString());
                Hits = 0;
                GoalLabel.Text = $"{Hits} / {Goal}";
                DeckName.Text = Deck;
            }

            // Slot Reveal
            else if (EventCode == 4)
            {
                JArray data = JArray.Parse(DataPacket[2].ToString());

                if (data.Count == 0) return;

                foreach (JObject slot in data.Cast<JObject>())
                {
                    string ind = slot["ind"].ToString();
                    string figure = slot["figure"].ToString()
                        .Replace("-1", "")
                        .Replace("-2", "");

                    if (Matches.ContainsKey(figure))
                    {
                        if (!Matches[figure].Contains(ind)) Matches[figure].Add(ind);
                    }

                    else Matches.Add(figure, new List<string> { ind });
                }

                foreach (var key in Matches.Keys.ToList())
                {
                    List<string> arr = Matches[key];
                    if (arr.Count == 2)
                    {
                        Matches.Remove(key);
                        if (Answers.Contains($"{arr[0]} {arr[1]}") || Answers.Contains($"{arr[1]} {arr[0]}")) continue;
                        Answers.Add($"{arr[0]} {arr[1]}");
                    }
                }

                MatchesBox.Text = string.Join(Environment.NewLine, Answers);
            }            

            // Correct Guess
            else if (EventCode == 5)
            {
                JObject data = (JObject)DataPacket[2];
                JArray cards = JArray.Parse(data["cards"].ToString());

                string ind1 = cards[0]["ind"].ToString();
                string ind2 = cards[1]["ind"].ToString();

                if (Answers.Contains($"{ind1} {ind2}")) Answers.Remove($"{ind1} {ind2}");
                else if (Answers.Contains($"{ind2} {ind1}")) Answers.Remove($"{ind2} {ind1}");

                MatchesBox.Text = string.Join(Environment.NewLine, Answers);
                Hits += 1;
                GoalLabel.Text = $"{Hits} / {Goal}";
            }

            // Level Pass
            else if (EventCode == 6)
            {
                JObject data = (JObject)DataPacket[2];
                Hits = 0;
                Goal = 0;
                GoalLabel.Text = $"Unknown";
                LevelNumber.Text += $" >> {Level + int.Parse(data["stars"].ToString())}";
                DeckName.Text += " >> ??";
                Matches.Clear();
                Answers.Clear();
            }

            // Level Fail
            else if (EventCode == 7)
            {
                Hits = 0;
                Goal = 0;
                GoalLabel.Text = $"Unknown";
                LevelNumber.Text += " >> 1";
                DeckName.Text += " >> ??";
                Matches.Clear();
                Answers.Clear();
            }

            // New Round
            else if (EventCode == 8)
            {
                JObject data = (JObject)DataPacket[2];
                Level = (int)data["level"];
                if (Level > RecordLevel)
                {
                    RecordLevel = Level;
                    RecordLabel.Text = RecordLevel.ToString();
                }

                LevelNumber.Text = Level.ToString();
                string deck = (string)data["deck"]["name"];
                Deck = char.ToUpper(deck[0]) + deck.Substring(1);
                DeckName.Text = DeckName.Text.Replace("??", Deck);
            }
        }

        private void TwitchChannelBtn_Click(object sender, EventArgs e)
        {
            Process.Start(TwitchChannel);
        }
    }
}
