using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FirstDiscordBot
{
    class Program
    {
        readonly string path = AppDomain.CurrentDomain.BaseDirectory+"Img"; 

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
            if (ParseMother(msg.Content.ToLower())&&!(msg.Author.IsBot))
            {
                await msg.Channel.SendMessageAsync("КОД КРАСНЫЙ, КОД КРАСНЫЙ, ЗДЕСЬ ШУТКИ ПРО МАМАШ!!!\n https://www.youtube.com/watch?v=aZFKzn5fBgM");
            }
            if (FindImgByName(msg.Content.ToLower()))
            {
                SocketUser user = msg.Author;
                await msg.Channel.SendFileAsync(path+"\\"+ msg+".png","\n"+user.Username+":");
            }
            if (msg.Content=="!get emoji") {
                string str = String.Join(" ; ", GetAllEmoji());
                await msg.Channel.SendMessageAsync(str);
            }
            if (msg.Content =="!set emoji")
            {
                await msg.Channel.SendMessageAsync("Coming soon lol!");
            }

        }
        private bool ParseMother(string msg) {
            string pattern = @"(тво[a-я]+)?\s?мам[a-я]+\s?(тво[a-я]+)?";
            string ReplacePattern = @"[^а-я0-9]";
            string FormMsg = Regex.Replace(msg, ReplacePattern, "");
            Regex reg = new Regex(pattern);
            MatchCollection matches = reg.Matches(FormMsg);
            if (matches.Count != 0) {
                return  true;
            }
            return false;
        }
        
        private bool FindImgByName(string msg)
        {
            if (!(msg.ElementAt(0) == '('))
                return false;
            var str = GetAllEmoji();
            if (str.Select(m=>m==msg)!=null)
            {
                return true;
            }
            return false;
        }
        private String[] GetAllEmoji()
        {
            var temp = Directory
                .GetFiles(path, "*.png", SearchOption.AllDirectories)
                .Select(Path.GetFileName)
                .ToArray();
            return  temp;
        }

    }
}
