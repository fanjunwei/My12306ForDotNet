using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;
using AForge.Imaging;
using AForge.Imaging.Filters;
using ImageMagickNET;

namespace ImageCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(getLoginImgCode));
            t.Start();
        }
        private void getLoginImgCode()
        {
            for (int i = 0; i < 100; i++)
            {
                Bitmap map = getImage("https://dynamic.12306.cn/otsweb/passCodeAction.do?rand=sjrand");
                map.Save("c:\\code\\00" + i.ToString() + ".jpg");
            }
        }
        private Bitmap getImage(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(uri);
                http.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
                //http.Headers.Set(HttpRequestHeader.UserAgent, "Mozilla/4.0");

                http.Referer = url;
                HttpWebResponse response = (HttpWebResponse)http.GetResponse();

                Stream stream = response.GetResponseStream();
                Bitmap map = new Bitmap(stream);
                return map;


            }

            catch (UriFormatException ex)
            {

                Console.WriteLine("无效的URL");

            }

            catch (IOException ex)
            {

                Console.WriteLine("连接失败");

            }
            return null;
        }

        unsafe private void to2(Bitmap map)
        {
            BitmapData data = map.LockBits(new Rectangle(0, 0, map.Width, map.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte* p = (byte*)data.Scan0;
            for (int i = 0; i < data.Height; i++)
            {
                for (int j = 0; j < data.Width; j++)
                {
                    byte b = p[i * data.Stride + j * 4];
                    byte g = p[i * data.Stride + j * 4 + 1];
                    byte r = p[i * data.Stride + j * 4 + 2];
                    byte a = p[i * data.Stride + j * 4 + 3];
                    int average = (r + g + b) / 3;
                    if (average < 115)
                    {
                        p[i * data.Stride + j * 4] = 0;
                        p[i * data.Stride + j * 4 + 1] = 0;
                        p[i * data.Stride + j * 4 + 2] = 0;

                    }
                    else
                    {
                        p[i * data.Stride + j * 4] = 255;
                        p[i * data.Stride + j * 4 + 1] = 255;
                        p[i * data.Stride + j * 4 + 2] = 255;

                    }
                }
            }
            map.UnlockBits(data);


        }
        FiltersSequence preprocessFilters;
        Bitmap denoise(Bitmap bmp, Size size)
        {
            Bitmap ivbmp = bmp.Clone() as Bitmap;
            Invert iv = new Invert();
            iv.ApplyInPlace(ivbmp);

            BlobCounter bc = new BlobCounter();
            bc.FilterBlobs = true;
            bc.MinWidth = bc.MinHeight = 0;
            bc.MaxWidth = size.Width;
            bc.MaxHeight = size.Height;
            bc.ProcessImage(ivbmp);

            Rectangle[] rects = bc.GetObjectsRectangles();

            Bitmap tmp = new Bitmap(bmp);

            using (Graphics g = Graphics.FromImage(tmp))
                //g.DrawRectangles(Pens.Red, rects);
                if (rects.Length > 0)
                    g.FillRectangles(Brushes.White, rects);

            //iv.ApplyInPlace(tmp);

            return tmp;
        }
        Bitmap preprocess(Bitmap bmp, FiltersSequence fs)
        {
            Bitmap tmp = bmp.Clone() as Bitmap;
            tmp = fs.Apply(bmp);
            return tmp;
        }
        private void doAllImage()
        {
            DirectoryInfo dir = new DirectoryInfo("c:\\code");
            FileInfo[] files = dir.GetFiles("*.jpg");
            preprocessFilters = new FiltersSequence();
            preprocessFilters.Add(Grayscale.CommonAlgorithms.BT709);
            //preprocessFilters.Add(new BradleyLocalThresholding());
            preprocessFilters.Add(new OtsuThreshold());
            foreach (FileInfo file in files)
            {

                Bitmap map = new Bitmap(file.FullName);
                int noiseWidth = 5;
                Bitmap preImg = denoise(preprocess(map, preprocessFilters), new Size(noiseWidth, noiseWidth));
                preImg = denoise(preprocess(preImg, preprocessFilters), new Size(noiseWidth, 3));
                preImg = denoise(preprocess(preImg, preprocessFilters), new Size(3, noiseWidth));
                quganrao(preImg);
                //
                //to2(map);
                preImg.Save("c:\\code\\1\\" + file.Name);
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(doAllImage));
            t.Start();
            //Bitmap map = new Bitmap("c:\\code\\3.jpg");
            //to2(map);
            //pictureBox2.Image = map;
            //Bitmap map1 = new Bitmap(map.Width / 4, map.Height);
            //using (Graphics g = Graphics.FromImage(map1))
            //{
            //    g.DrawImage(map, new Rectangle(0, 0, map1.Width, map1.Height), new Rectangle(0, 0, map1.Width, map1.Height), GraphicsUnit.Pixel);
            //}

            //Bitmap map2 = new Bitmap(map.Width / 4, map.Height);
            //using (Graphics g = Graphics.FromImage(map2))
            //{
            //    g.DrawImage(map, new Rectangle(0, 0, map2.Width, map2.Height), new Rectangle(map2.Width, 0, map2.Width, map2.Height), GraphicsUnit.Pixel);
            //}

            //Bitmap map3 = new Bitmap(map.Width / 4, map.Height);
            //using (Graphics g = Graphics.FromImage(map3))
            //{
            //    g.DrawImage(map, new Rectangle(0, 0, map1.Width, map1.Height), new Rectangle(map2.Width * 2, 0, map2.Width, map2.Height), GraphicsUnit.Pixel);
            //}
            //pictureBox1.Image = map1;
            //pictureBox2.Image = map2;
            //pictureBox3.Image = map3;
        }
        unsafe private void quganrao(Bitmap map)
        {


            BitmapData data = map.LockBits(new Rectangle(0, 0, map.Width, map.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte* p = (byte*)data.Scan0;
            bool find = false;
            int begin = 0;
            for (int i = 0; i < data.Height; i++)
            {
                int colum = 0;
                for (int j = 0; j < data.Width; j++)
                {


                    byte b = p[i * data.Stride + j * 4];
                    byte g = p[i * data.Stride + j * 4 + 1];
                    byte r = p[i * data.Stride + j * 4 + 2];
                    byte a = p[i * data.Stride + j * 4 + 3];

                    if (r == 0)
                    {
                        if (find)
                        {
                            colum += 1;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine(colum);
                            begin = j;
                            colum = 1;

                        }
                        find = true;
                    }
                    else
                    {
                        if (find)
                        {
                            for (int k = begin; k < j; k++)
                            {
                                if (colum > 5)
                                {
                                    p[i * data.Stride + k * 4] = 255;
                                    p[i * data.Stride + k * 4 + 1] = 255;
                                    p[i * data.Stride + k * 4 + 2] = 255;
                                }
                            }
                        }
                        find = false;
                    }
                }



            }

            map.UnlockBits(data);

        }
        unsafe private List<SplitItem> split(Bitmap map)
        {
            List<SplitItem> recs = new List<SplitItem>();
            int width = (map.Width - 7) / 4;
            for (int i = 0; i < 4; i++)
            {
                recs.Add(new SplitItem(7 + i * width, 0, width, map.Height));
            }
            //BitmapData data = map.LockBits(new Rectangle(0, 0, map.Width, map.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            //byte* p = (byte*)data.Scan0;
            //bool find = false;
            //int begin = 0;
            //for (int j = 0; j < data.Width; j++)
            //{
            //    int colum = 0;
            //    for (int i = 0; i < data.Height; i++)
            //    {
            //        byte b = p[i * data.Stride + j * 4];
            //        byte g = p[i * data.Stride + j * 4 + 1];
            //        byte r = p[i * data.Stride + j * 4 + 2];
            //        byte a = p[i * data.Stride + j * 4 + 3];
            //        if (r == 0)
            //        {
            //            colum += 1;
            //        }
            //    }
            //    if (!find)
            //    {
            //        if (colum > 2)
            //        {
            //            find = true;
            //            begin = j;
            //        }
            //    }
            //    else
            //    {
            //        if (colum <= 2)
            //        {
            //            find = false;
            //            SplitItem rec = new SplitItem(begin, 0, j - begin, data.Height);
            //            recs.Add(rec);
            //        }
            //    }

            //    //System.Diagnostics.Debug.WriteLine(colum);
            //}
            //if (find)
            //{
            //    SplitItem rec = new SplitItem(begin, 0, data.Width - begin, data.Height);
            //    recs.Add(rec);
            //}

            //while (recs.Count < 4)
            //{
            //    recs.Sort();
            //    SplitItem max = recs[recs.Count - 1];
            //    recs.Remove(max);
            //    SplitItem rec1 = new SplitItem(max.X, max.Y, max.Width / 2, data.Height);
            //    SplitItem rec2 = new SplitItem(max.X + max.Width / 2, max.Y, max.Width / 2, data.Height);
            //    recs.Add(rec1);
            //    recs.Add(rec2);

            //}
            //map.UnlockBits(data);



            return recs;
        }
        private void treatAllImage()
        {
            DirectoryInfo dir = new DirectoryInfo("c:\\code\\1");
            FileInfo[] files = dir.GetFiles("*.jpg");
            foreach (FileInfo file in files)
            {
                Bitmap map = new Bitmap(file.FullName);
                to2(map);
                List<SplitItem> splits = split(map);
                for (int i = 0; i < splits.Count; i++)
                {
                    Bitmap map1 = new Bitmap(splits[0].Width, splits[0].Height);
                    using (Graphics g = Graphics.FromImage(map1))
                    {
                        g.DrawImage(map, new Rectangle(0, 0, map1.Width, map1.Height), splits[0].toRectangle(), GraphicsUnit.Pixel);
                    }
                    map1.Save("c:\\code\\2\\" + file.Name + "_" + i.ToString() + ".jpg");
                }

            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(treatAllImage));
            t.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MagickNet.InitializeMagick(Application.ExecutablePath);
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ImageMagickNET.Image im = new ImageMagickNET.Image("");
        }
    }
}
