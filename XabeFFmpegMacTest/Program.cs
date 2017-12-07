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

            var mp3OutPut = Path.Combine(root, DateTime.Now.ToString("yyyyMMddHHmmssfff") + "songAudio.mp3");

            bool result = await ConversionHelper.ExtractAudio(songMp4, mp3OutPut).Start();

            Console.WriteLine($"\nMp3 Extracted\n{ new MediaInfo(mp3OutPut)}\n");
            Console.ReadLine();

/*
----------------------------------------------------------------
Windows 10 output
 
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

---------------------------------------------------------------
Mac Output:

True-/Users/code/XabeFFmpegMacTest/XabeFFmpegMacTest/ffmpeg/mac64
True-/Users/code/XabeFFmpegMacTest/XabeFFmpegMacTest/song.mp4

Unhandled Exception: System.ComponentModel.Win32Exception: Permission denied
   at Interop.Sys.ForkAndExecProcess(String filename, String[] argv, String[] envp, String cwd, Boolean redirectStdin, Boolean redirectStdout, Boolean redirectStderr, Int32& lpChildPid, Int32& stdinFd, Int32& stdoutFd, Int32& stderrFd)
   at System.Diagnostics.Process.StartCore(ProcessStartInfo startInfo)
   at System.Diagnostics.Process.Start()
   at Xabe.FFmpeg.FFbase.RunProcess(String args, String processPath, Boolean rStandardInput, Boolean rStandardOutput, Boolean rStandardError)
   at Xabe.FFmpeg.FFmpeg.<>c__DisplayClass9_0.<RunProcess>b__0()
   at System.Threading.Tasks.Task`1.InnerInvoke()
   at System.Threading.ExecutionContext.Run(ExecutionContext executionContext, ContextCallback callback, Object state)
   at System.Threading.Tasks.Task.ExecuteWithThreadLocal(Task& currentTaskSlot)
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Xabe.FFmpeg.FFmpeg.<RunProcess>d__9.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Xabe.FFmpeg.Conversion.<Start>d__37.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Xabe.FFmpeg.Conversion.<Start>d__36.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Xabe.FFmpeg.Conversion.<Start>d__34.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at XabeFFmpegMacTest.Program.<Main>d__0.MoveNext() in /Users/code/XabeFFmpegMacTest/XabeFFmpegMacTest/Program.cs:line 36
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at XabeFFmpegMacTest.Program.<Main>(String[] args)
The program '[6780] XabeFFmpegMacTest.dll' has exited with code 0 (0x0).
 
*/

        }

    }
}
