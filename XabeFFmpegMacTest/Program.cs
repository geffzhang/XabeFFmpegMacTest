using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace XabeFFmpegMacTest
{
    class Program
    {
        static async Task Main(string[] args)//https://stackoverflow.com/questions/38114553/are-async-console-applications-supported-in-net-core
        {
            //
            // set FFmpegDir 
            //
            bool isMac = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

            var root = Path.Combine(Path.GetDirectoryName(Directory.GetCurrentDirectory()), "XabeFFmpegMacTest"); 

            Xabe.FFmpeg.FFbase.FFmpegDir = Path.Combine(root, "ffmpeg", (isMac ? "mac64" : "win64"));

            var exist = Directory.Exists(Xabe.FFmpeg.FFbase.FFmpegDir);
            Console.WriteLine(exist + "-" + Xabe.FFmpeg.FFbase.FFmpegDir);

            //
            // mp4 to mp3 test
            //
            var songMp4 = Path.Combine(root, "song.mp4");
            exist = File.Exists(songMp4);
            Console.WriteLine(exist + "-" + songMp4);

            var mp3OutPut = Path.Combine(root, "songAudio.mp3");

            bool result = await ConversionHelper.ExtractAudio(songMp4, mp3OutPut).Start();

            Console.WriteLine($"\nMp3 Extracted\n{ new MediaInfo(mp3OutPut)}\n");
            Console.ReadLine();


/* Windows 10 output
 
True - C:\Users\code\XabeFFmpegMacTest\XabeFFmpegMacTest\ffmpeg\win64
True - C:\Users\code\XabeFFmpegMacTest\XabeFFmpegMacTest\song.mp4

Mp3 Extracted
Video fullName: C: \Users\code\XabeFFmpegMacTest\XabeFFmpegMacTest\songAudio.mp3
Video root: C: \Users\code\XabeFFmpegMacTest\XabeFFmpegMacTest
Video name: songAudio.mp3
Video extension: .mp3
Video duration : 00:00:00
Video format :
Audio format : mp3
Audio duration: 00:04:54
Aspect Ratio :
Framerate: fps
Resolution : 0 x 0
Size: 4706566 b
*/






        }

    }
}
