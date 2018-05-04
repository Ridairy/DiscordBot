using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FirstDiscordBot
{
    class Program
    {
        static void Main(string[] args) 
            => new Program().MainAsync().GetAwaiter().GetResult();
       
        
        public async Task MainAsync()
        {
            var client = new DiscordSocketClient();
            client.Log += Log;
            //token берем в настройках для розрабів на сайтєцу діскорду
            string token = "NDQxODkzODAxMDcwNjkwMzE0.Dc25Xg.ztsSoxsw4eGaQSaz50yhjqYKow8";
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            client.MessageReceived += MessageRecieved;

           await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg);
            return Task.CompletedTask;
        }
        private async Task MessageRecieved(SocketMessage msg)
        {
            if (ParseMother(msg.Content.ToLower()))
            {
                await msg.Channel.SendMessageAsync("https://www.youtube.com/watch?v=aZFKzn5fBgM");
            }

        }
        private bool ParseMother(string msg) {
            string pattern = @"(тво[a-я]+)?\s?мам[a-я]+\s?(тво[a-я]+)?";
            Regex reg = new Regex(pattern);
            MatchCollection matches = reg.Matches(msg);
            if (matches.Count > 0) {
                return  true;
            }
            return false;
        }

    }
}
