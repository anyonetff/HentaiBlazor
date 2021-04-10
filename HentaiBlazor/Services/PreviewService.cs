using HentaiBlazor.Data.Anime;
using LibVLCSharp.Shared;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace HentaiBlazor.Services
{
    // 获取视频封面服务.
    public class PreviewService
    {
        private static byte[] none = new byte[0];

        private static uint Width = 320;

        private static uint Height = 180;

        /// <summary>
        /// RGBA is used, so 4 byte per pixel, or 32 bits.
        /// </summary>
        private const uint BytePerPixel = 4;

        /// <summary>
        /// the number of bytes per "line"
        /// For performance reasons inside the core of VLC, it must be aligned to multiples of 32.
        /// </summary>
        private static readonly uint Pitch = Width * BytePerPixel;

        /// <summary>
        /// The number of lines in the buffer.
        /// For performance reasons inside the core of VLC, it must be aligned to multiples of 32.
        /// </summary>
        private static readonly uint Lines = Height;


        // 封面缓存
        // private Dictionary<string, string> cache;

        private Dictionary<string, byte[]> cache;

        public PreviewService()
        {
            cache = new Dictionary<string, byte[]>();
            Core.Initialize();
        }

        public byte[] GetCached(string id)
        {
            if (cache.ContainsKey(id))
            {
                return cache[id];
            }

            return null;
        }

        public async Task<byte[]> GetAsync(AVideoEntity video)
        {
            //lock(this);

            if (cache.ContainsKey(video.Id))
            {
                return cache[video.Id];
            }
            //string cover = "";

            byte[] c = none;

            lock (this)
            {
                c = ReadAsync(Path.Combine(video.Path, video.Name));
                cache.Add(video.Id, c);
            }
            
            return c;
        }

        private byte[] ReadAsync(string file)
        {

            FileInfo f = new FileInfo(file);

            if (!f.Exists)
            {
                Console.WriteLine("文件不存在...");
                return none;
            }

            lock (this)
            {

            }

            

            using (var libvlc = new LibVLC())
            using (var mediaPlayer = new MediaPlayer(libvlc))
            {
                Console.WriteLine("创建VLC播放器成功.");

                var processingCancellationTokenSource = new CancellationTokenSource();
                mediaPlayer.Stopped += (s, e) => processingCancellationTokenSource.CancelAfter(1);

                //Stream s = new FileStream(file, FileMode.Open);

                // Create new media
                using var media = new Media(libvlc, new Uri(file));

                media.AddOption(":no-audio");
                // Set the size and format of the video here.
                mediaPlayer.SetVideoFormat("RV32", Width, Height, Pitch);
                mediaPlayer.SetVideoCallbacks(Lock, null, Display);

                // Start recording
                mediaPlayer.Play(media);

                //try
                
                var data = ProcessThumbnailsAsync("E:\\", processingCancellationTokenSource.Token);
                

                mediaPlayer.Stop();
                //catch (OperationCanceledException)
                //{ }

                return data;

            }

            return none;

        }

        private MemoryMappedFile CurrentMappedFile;
        private MemoryMappedViewAccessor CurrentMappedViewAccessor;
        private readonly ConcurrentQueue<(MemoryMappedFile file, MemoryMappedViewAccessor accessor)> FilesToProcess = new ConcurrentQueue<(MemoryMappedFile file, MemoryMappedViewAccessor accessor)>();
        private long FrameCounter = 0;

        private byte [] ProcessThumbnailsAsync(string destination, CancellationToken token)
        {
            var frameNumber = 0;
            while (!token.IsCancellationRequested)
            {
                if (FilesToProcess.TryDequeue(out var file))
                {
                    using (var image = new Image<SixLabors.ImageSharp.PixelFormats.Bgra32>((int)(Pitch / BytePerPixel), (int)Lines))
                    using (var sourceStream = file.file.CreateViewStream())
                    {
                        sourceStream.Read(MemoryMarshal.AsBytes(image.GetPixelMemoryGroup().Single().Span));

                        Console.WriteLine($"Writing {frameNumber:0000}.jpg");
                        var fileName = Path.Combine(destination, $"{frameNumber:0000}.jpg");
                        using (var output = new MemoryStream())
                        {
                            image.Mutate(ctx => ctx.Crop((int)Width, (int)Height));
                            image.SaveAsPng(output);

                            return output.ToArray();
                        }
                    }
                    file.accessor.Dispose();
                    file.file.Dispose();
                    frameNumber++;
                }
                else
                {
                    Task.Delay(TimeSpan.FromSeconds(1), token);
                }
            }

            return none;
        }

        private IntPtr Lock(IntPtr opaque, IntPtr planes)
        {
            CurrentMappedFile = MemoryMappedFile.CreateNew(null, Pitch * Lines);
            CurrentMappedViewAccessor = CurrentMappedFile.CreateViewAccessor();
            Marshal.WriteIntPtr(planes, CurrentMappedViewAccessor.SafeMemoryMappedViewHandle.DangerousGetHandle());
            return IntPtr.Zero;
        }

        private void Display(IntPtr opaque, IntPtr picture)
        {
            if (FrameCounter % 100 == 0)
            {
                FilesToProcess.Enqueue((CurrentMappedFile, CurrentMappedViewAccessor));
                CurrentMappedFile = null;
                CurrentMappedViewAccessor = null;
            }
            else
            {
                CurrentMappedViewAccessor.Dispose();
                CurrentMappedFile.Dispose();
                CurrentMappedFile = null;
                CurrentMappedViewAccessor = null;
            }
            FrameCounter++;
        }



    }
}
