using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertWebPToJPeg
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                string target = Path.ChangeExtension(arg, ".jpg");

                var bytes = File.ReadAllBytes(arg);

                using (var image = new Imazen.WebP.SimpleDecoder().DecodeFromBytes(bytes, bytes.Length))
                {
                    var encoder = ImageCodecInfo.GetImageEncoders().Single(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    var encoderParameters = new EncoderParameters
                    {
                        Param = new[] { new EncoderParameter(Encoder.Quality, 90L) }
                    };

                    image.Save(target, encoder, encoderParameters);
                }
            }
        }
    }
}
