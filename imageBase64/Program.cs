using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace imageBase64
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(ImageToBase64());
            mattBase64();
            //Console.ReadLine();
        }

        static string base64String = null;
        static byte[] fileConverted = null;
        static string path = "c:\\users\\matth\\Desktop\\SampleImage.jpg";
        static string path2 = "c:\\users\\matth\\Desktop\\SampleImage2.jpg";
        static string writeToPath = "c:\\users\\matth\\Desktop\\";

        public static void mattBase64()
        {
            var memoryStream = new MemoryStream(File.ReadAllBytes(path));

            base64String = Convert.ToBase64String(memoryStream.ToArray());
            string[] strArr = Chop(base64String, 102400);

            Console.WriteLine(base64String);

            

            for (int count = 0; count < strArr.Length; count++)
            {
                File.WriteAllText(writeToPath + "splitFile" + count + ".txt", strArr[count]);
            }

            fileConverted = Convert.FromBase64String(base64String);

            File.WriteAllBytes(path2, memoryStream.ToArray());
        }

        public static string[] Chop(string value, int length)
        {
            int strLength = value.Length;
            int strCount = (strLength + length - 1) / length;
            string[] result = new string[strCount];
            for (int i = 0; i < strCount; ++i)
            {
                result[i] = value.Substring(i * length, Math.Min(length, strLength));
                strLength -= length;
            }
            return result;
        }

        public static string ImageToBase64()
        {
            
            byte[] fileBytes = File.ReadAllBytes(path);

            using (MemoryStream ms = new MemoryStream())
            {
                base64String = Convert.ToBase64String(fileBytes);
                return base64String;
            }

            
            //using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            //{
            //    using (MemoryStream m = new MemoryStream())
            //    {
            //        image.Save(m, image.RawFormat);
            //        byte[] imageBytes = m.ToArray();
            //        base64String = Convert.ToBase64String(imageBytes);
            //        return base64String;
            //    }
            //}
        }
        public System.Drawing.Image Base64ToImage()
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            return image;
        }
    }
}
