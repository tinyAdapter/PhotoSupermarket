using System;
using System.IO;
using PhotoSupermarket.Core;
using PhotoSupermarket.Core.HistogramEqualization;
using PhotoSupermarket.Core.ModeConverter;
using PhotoSupermarket.Core.Util;
using PhotoSupermarket.Core.Compression;
using System.Drawing;
using System.Security;


namespace PhotoSupermarket.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string goOn;
            System.Console.WriteLine("PhotoSupermarket - A Better Photoshop (?)");
            System.Console.WriteLine("Still under construction. Please wait...");
            do
            {
                PrintWelcomeHint();
                System.Console.WriteLine("继续进行文件操作吗？（Y/N）");
                goOn = System.Console.ReadLine();
            } while (goOn == "Y" || goOn == "y");
        }

        private static void showPhoto(string filePath)
        {
            string filePathName = filePath;//定义图像文件的位置（包括路径及文件名）

            //建立新的系统进程    
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            //设置图片的真实路径和文件名    
            process.StartInfo.FileName = filePathName;

            //设置进程运行参数，这里以最大化窗口方法显示图片。    
            process.StartInfo.Arguments = "rundl132.exe C://WINDOWS//system32//shimgvw.dll,ImageView_Fullscreen";

            //此项为是否使用Shell执行程序，因系统默认为true，此项也可不设，但若设置必须为true    
            process.StartInfo.UseShellExecute = true;

            //此处可以更改进程所打开窗体的显示样式，可以不设    
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            process.Start();
            process.Close();
        }

        private static void PrintWelcomeHint()
        {
            //路径判断
            var image = ImageFile.LoadBmpImage(@CheckInputPath());
            //int num;
            bool isExit = false;
            System.Console.WriteLine("\n***************读取文件中***************");
            System.Console.WriteLine("***************读取成功！！！***************");
            do
            {
                System.Console.WriteLine("\n当前可进行的操作：\n1.模式转换\n2.图像增强\n3.图像压缩\n4.另存为\n0.返回");
                System.Console.Write("选择的操作是：");
                //num = CheckNum();
                try
                {
                    int num = Convert.ToInt32(System.Console.ReadLine());
                    if (num == 0)
                    {
                        isExit = true;
                    }
                    else if (num == 1)
                    {
                        System.Console.WriteLine("\n***************模式转换模块***************");
                        ModuleOne(image);
                    }
                    else if (num == 2)
                    {
                        System.Console.WriteLine("\n***************图像增强模块***************");
                        ModuleTwo(image);
                    }
                    else if (num == 3)
                    {
                        System.Console.WriteLine("\n***************图像压缩模块***************");
                        ModuleThree(image);
                    }
                    else if (num == 4)
                    {
                        int goon = SavePhoto(image);
                        if(goon == 2)
                        {
                            isExit = true;
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Error：输入数字不符！");
                    }
                }
                catch (OverflowException)
                {
                    System.Console.WriteLine("err:转化的不是一个int型数据");
                }
                catch(FormatException)
                {
                    System.Console.WriteLine("err:格式错误");
                }
                catch (ArgumentException)
                {
                    System.Console.WriteLine("err:null");
                }
            } while (isExit == false);
        }

        private static string CheckInputPath()
        {
            bool InputPath = false;
            string FilePath;
            do
            {
                System.Console.WriteLine("输入文件路径：");
                FilePath = System.Console.ReadLine();
                if (!File.Exists(@FilePath))
                {
                    System.Console.WriteLine("ERROR:文件不存在！!");
                }
                else if (@FilePath.Split(".")[@FilePath.Split(".").Length - 1] != "bmp")
                {
                    System.Console.WriteLine("ERROR:文件类型不符！！");
                }
                else
                {
                    InputPath = true;
                    return FilePath;
                    
                }
            } while (InputPath == false);
            return " ";
        }

        private static int CheckPhoto(BmpImage image)
        {

            bool isGray = true;
            // bitCount == 8 
            if (image.Data.ColorMode == BitmapColorMode.TwoFiftySixColors)
            {
                for (int i = 0, len = image.Palette.Length; i < len; i++)
                {
                    if (image.Palette[i].Red != image.Palette[i].Green) { isGray = false; }
                }
                if (isGray)
                {
                    return 1;
                }
            }
            //bitCount ==  32 || bitCount == 24
            else if (image.Data.ColorMode == BitmapColorMode.TrueColor || image.Data.ColorMode == BitmapColorMode.RGBA)
            {
                return 2;
            }
            else
            {
                return 0;
            }
                return 0;
        }

        private static int SavePhoto(BmpImage image)
        {
            System.Console.WriteLine("另存为：");
            string SavePath = System.Console.ReadLine();
            ImageFile.SaveBmpImage(image, @SavePath);
            System.Console.WriteLine("成功保存文件到：{0}", SavePath);
            System.Console.WriteLine("显示图片？(1 or 2）");
            int display = Convert.ToInt32(System.Console.ReadLine());
            if (display == 1)
            {
                showPhoto(SavePath);
            }
            System.Console.WriteLine("是否继续？(1 or 2）");
            int goon = Convert.ToInt32(System.Console.ReadLine());
            return goon;
        }

        private static void ModuleOne(BmpImage image)
        {
            /*
             * 1.检测图像的类型
             *   (1. gray to binary
             *      (1. danyuzhi
             *      (2. dither
             *      (3. ordered dither
             *   (2. color to gray
             *      (1. RGB -> HSI
             *      (2. RGB -> YCbCr
             */

            bool isGray = true;
            int photoFormat = CheckPhoto(image);
            //256 color
            if (photoFormat == 1)
            {
                
                System.Console.Write("检测到图片为灰度图像：");
                System.Console.WriteLine("可进行的操作：\n1.单阈值法\n2.dither\n3.ordered dither\n0.返回");
                System.Console.Write("选择的操作是：");
                bool inputNum = false;
                int argument;
                do
                {
                    try
                    {
                        int num = Convert.ToInt32(System.Console.ReadLine());
                        if (num < 4 && num > -1)
                        {
                            inputNum = true;
                            //1.单阈值法
                            if (num == 1)
                            {
                                System.Console.Write("输入阈值（输入0则为默认值128）：");
                                try
                                {
                                    argument = Convert.ToInt32(System.Console.ReadLine());
                                    image = new SingleThresholdConverter(image, argument).Convert().Image;
                                }
                                catch (OverflowException)
                                {
                                    System.Console.WriteLine("err:转化的不是一个int型数据");
                                }
                                catch (FormatException)
                                {
                                    System.Console.WriteLine("err:格式错误");
                                }
                                catch (ArgumentNullException)
                                {
                                    System.Console.WriteLine("err:null");
                                }
                                System.Console.WriteLine("完成单阈值法处理！");
                            }
                            //2.dither
                            else if (num == 2)
                            {
                                System.Console.Write("输入矩阵参数n（输入0则取默认值N=2）：");
                                try
                                {
                                    argument = Convert.ToInt32(System.Console.ReadLine());
                                    image = new DitherConverter(image, argument).Convert().Image;

                                }
                                catch (OverflowException)
                                {
                                    System.Console.WriteLine("err:转化的不是一个int型数据");
                                }
                                catch (FormatException)
                                {
                                    System.Console.WriteLine("err:格式错误");
                                }
                                catch (ArgumentNullException)
                                {
                                    System.Console.WriteLine("err:null");
                                }
                                System.Console.WriteLine("完成dither矩阵处理！");
                            }
                            //3.ordered dither
                            else if (num == 3)
                            {
                                System.Console.Write("输入矩阵参数n（输入0则取默认值N=2）：");
                                argument = Convert.ToInt32(System.Console.ReadLine());
                                image = new OrderedDitherConverter(image, argument).Convert().Image;
                                System.Console.WriteLine("完成ordered dither矩阵处理！");
                            }
                            else{ ; }
                        }
                        else
                        {
                            System.Console.Write("输入错误请重新输入:");
                        }
                    }
                    catch (OverflowException)
                    {
                        System.Console.WriteLine("err:转化的不是一个int型数据");
                    }
                    catch (FormatException)
                    {
                        System.Console.WriteLine("err:格式错误");
                    }
                    catch (ArgumentNullException)
                    {
                        System.Console.WriteLine("err:null");
                    }
                } while (inputNum == false);
            }
            //True color or RGBA
            else if(photoFormat == 2)
            {
                System.Console.Write("检测到图片为真彩图像：");
                System.Console.WriteLine("可进行的操作：\n1.RGB->HSI\n2.RGB->YCbCr\n0.返回");
                System.Console.Write("选择的操作是：");
                bool inputNum = false;
                do
                {
                    try
                    {
                        int num = Convert.ToInt32(System.Console.ReadLine());
                        if (num < 3)
                        {
                            inputNum = true;
                            //1.
                            if (num == 1)
                            {
                                image = new HSIConverter(image).Convert().Image;
                            }
                            //2.
                            if (num == 2)
                            {
                                image = new YCbCrConverter(image).Convert().Image;
                            }
                            else{ ; }
                        }
                        else
                        {
                            System.Console.Write("输入错误请重新输入:");
                        }
                    }
                    catch (OverflowException)
                    {
                        System.Console.WriteLine("err:转化的不是一个int型数据");
                    }
                    catch (FormatException)
                    {
                        System.Console.WriteLine("err:格式错误");
                    }
                    catch (ArgumentNullException)
                    {
                        System.Console.WriteLine("err:null");
                    }
                } while (inputNum == false);
            }
            else
            {
                System.Console.Write("\n图片形式不支持模块转换哟~\n");
            }
        }

        private static void ModuleTwo(BmpImage image)
        {

            /*
             * 直方图均衡：
             *  1.灰度图像:
             *      进行直方图均衡，将均衡结果在存储至某一目录
             *  2.真彩图像:
             *      1.对其进行彩色变换(RGB->HSV)：
             *      2.对亮度分量做直方图均衡
             *      3.做彩色反变换得到均衡结果，存储至某一目录
             */

            int photoFormat = CheckPhoto(image);
            if(photoFormat == 1)
            {
                System.Console.Write("\n检测到图片为灰度图像");
                System.Console.WriteLine("进行直方图均衡.....");
                image = new GrayEqualization(image).Equalization().Image;
                System.Console.WriteLine("完成直方图均衡！");
                
            }
            else if(photoFormat == 2)
            {
                System.Console.Write("\n检测到图片为彩色图像");
                System.Console.WriteLine("进行直方图均衡(彩色变换为RGB->HSV).....");
                image = new ColorEqualization(image).Equalization().Image;
                System.Console.WriteLine("完成直方图均衡！");
            }
            else
            {
                System.Console.Write("\n图片形式不支持模块转换哟~\n");
            }
        }

        private static void ModuleThree(BmpImage image)
        {
            /*
             * 图像压缩模块
             *  1.无损预测编码+符号编码
             *  2.均匀量化
             *  3.DCT变换及DCT反变换
             */

            int photoFormat = CheckPhoto(image);
            if(photoFormat == 1)
            {
                System.Console.WriteLine("\n检测图片为8bit灰度图像");
                System.Console.WriteLine("可选择的压缩模式：\n1.无损预测编码\n2.均匀量化\n3.DCT变换及DCT反变换\n0.返回");
                bool inputNum = false;
                do
                {
                    try
                    {
                        int num = Convert.ToInt32(System.Console.ReadLine());
                        if (num < 4 && num > -1)
                        {
                            inputNum = true;
                            //1.无损预测编码
                            if (num == 1)
                            {
                                System.Console.WriteLine("\n输如文件保存的路径：");
                                string CompressPath = System.Console.ReadLine();
                                string savedFilePath = Path.GetDirectoryName(CompressPath);
                                string fileName = Path.GetFileNameWithoutExtension(CompressPath);
                                LinearPredictor lp = new LinearPredictor(image, savedFilePath, fileName);
                                lp.predicate();
                                //system.console.writeline("{0},{1}", savedfilepath, filename);
                                System.Console.WriteLine("***************保存中***************");
                                System.Console.WriteLine("压缩文件已保存至{0}\n",CompressPath);
                                System.Console.WriteLine("完成无损预测编码！");
                            }
                            //2.均匀量化
                            else if (num == 2)
                            {
                                System.Console.WriteLine("\n压缩比（输如0为IGS量化）：");
                                double CompressRatio = Convert.ToDouble(System.Console.ReadLine());
                                if(CompressRatio == 0)
                                {
                                    UniformQuantizing igs = new UniformQuantizing(image);
                                    image = igs.InverseUniformQuantizaing();
                                }
                                else
                                {
                                    UniformQuantizing uq = new UniformQuantizing(image, CompressRatio);
                                    image = uq.InverseUniformQuantizaing();
                                }
                                System.Console.WriteLine("完成均匀量化！");
                            }
                            //3.DCT变换及DCT反变换
                            else if (num == 3)
                            {
                                System.Console.WriteLine("\n设置分块大小：");
                                int blockSize = Convert.ToInt32(System.Console.ReadLine());
                                var dct = new DCTCompression(image, blockSize);
                                image = dct.GetDCTImage();
                                //DCT反变换
                                BmpImage reverseImage = dct.GetDCTReverseImage(1.0);

                                //即将50%的高频系数用0代替 DCT反变换
                                BmpImage reverseHalfImage = dct.GetDCTReverseImage(2.0);

                                System.Console.WriteLine("\n把DCT反变换的结果存储至：");
                                string reverseImagePath = System.Console.ReadLine();
                                ImageFile.SaveBmpImage(reverseImage, @reverseImagePath);
                                System.Console.WriteLine("显示图片？(1 or 2）");
                                int display1 = Convert.ToInt32(System.Console.ReadLine());
                                if (display1 == 1)
                                {
                                    showPhoto(reverseImagePath);
                                }

                                System.Console.WriteLine("把DCT反变换(50%)的结果存储至：");
                                string reverseHalfImagePath = System.Console.ReadLine();
                                ImageFile.SaveBmpImage(reverseHalfImage, @reverseHalfImagePath);
                                System.Console.WriteLine("显示图片？(1 or 2）");
                                int display2 = Convert.ToInt32(System.Console.ReadLine());
                                if (display2 == 1)
                                {
                                    showPhoto(reverseHalfImagePath);
                                }
                                System.Console.WriteLine("完成DCT变换及反变换！");
                            }
                            else
                            {
                                ;
                            }
                        }
                        else
                        {
                            System.Console.Write("输入错误请重新输入:");
                        }
                    }
                    catch (OverflowException)
                    {
                        System.Console.WriteLine("err:转化的不是一个int型数据");
                    }
                    catch (FormatException)
                    {
                        System.Console.WriteLine("err:格式错误");
                    }
                    catch (ArgumentNullException)
                    {
                        System.Console.WriteLine("err:null");
                    }
                } while (inputNum == false);
            }
            else
            {
                System.Console.Write("\n图片形式不支持图像压缩哟~\n");
            }
        }

        
    }
}
