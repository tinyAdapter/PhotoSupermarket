using System;
using System.IO;
using PhotoSupermarket.Core;
using PhotoSupermarket.Core.ModeConverter;
using PhotoSupermarket.Core.Util;

namespace PhotoSupermarket.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintWelcomeHint();
        }
        private static void PrintWelcomeHint()
        {
            System.Console.WriteLine("PhotoSupermarket - A Better Photoshop (?)");
            System.Console.WriteLine("Still under construction. Please wait...");
            
            //路径判断
            var image = ImageFile.LoadBmpImage(@CheckPath());
            int num;
            bool isExit = false;
            do
            {
                System.Console.WriteLine("\n***************读取文件中***************");
                System.Console.WriteLine("***************读取成功！！！***************");
                System.Console.WriteLine("可进行的操作：\n1.模式转换\n2.图像增强\n3.图像压缩\n4.另存为\n0.退出");
                System.Console.Write("选择的操作是：");
                num = CheckNum();
                if (num == 0)
                {
                    isExit = true;
                }
                else if (num == 1)
                {
                    System.Console.WriteLine("模式转换模块");
                    ModuleOne(image);
                }
                else if (num == 2)
                {
                    System.Console.WriteLine("图像增强模块");
                    ModuleTwo(image);
                }
                else if (num == 3)
                {
                    System.Console.WriteLine("图像压缩模块");
                    ModuleThree(image);
                }
                else
                {
                    System.Console.WriteLine("另存为：");
                    string SavePath = System.Console.ReadLine();
                    ImageFile.SaveBmpImage(image, @SavePath);
                    System.Console.WriteLine("You save to: {0}", SavePath);
                    isExit = true;
                }
            } while (isExit == false);
           
        }

        private static string CheckPath()
        {
            bool InputPath = false;
            string FilePath;
            do
            {
                System.Console.WriteLine("输入文件路径：");
                FilePath = System.Console.ReadLine();
                //if (!File.Exists(@FilePath))
                //{
                //    System.Console.WriteLine("ERROR:文件不存在！!");
                //}
                //else if (FilePath.Split(".")[1] != "bmp")
                //{
                //    System.Console.WriteLine("ERROR:文件类型不符！！");
                //}
                //else
                //{
                    InputPath = true;
                    return FilePath;
                    
               // }
            } while (InputPath == false);
            return " ";
        }

        private static int CheckNum()
        {
            bool InputNum = true;
            string num;
            do
            {
                num = System.Console.ReadLine();
                //num判断
                //System.Console.Write("您选择的操作是：{0}", num);
                switch (num)
                {
                    case "1":
                        return 1;
                    case "2":
                        return 2;
                    case "3":
                        return 3;
                    case "4":
                        return 4;
                    case "0":
                        return 0;
                    default:
                        InputNum = false;
                        System.Console.WriteLine("输入不符请重新输入");
                        break;
                }
            } while (InputNum == false);
            return 0;
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


            //------检测图像类型
            //gray
            bool isGray = true;
            bool isTrueColor = true;
            if(image.InfoHeader.BitCount == 8)
            {
                for (int i = 0, len = image.Palette.Length; i < len; i++)
                {
                    if (image.Palette[i].Red != image.Palette[i].Green) { isGray = false; }
                }
                if (isGray)
                {
                    System.Console.Write("检测到图片为灰度图像：");
                    System.Console.WriteLine("可进行的操作：\n1.单阈值法\n2.dither\n3.ordered dither");
                    System.Console.Write("选择的操作是：");
                    bool inputNum = false;
                    int argument;
                    do
                    {
                        int num = CheckNum();
                        if (num < 3 && num > 0)
                        {
                            inputNum = true;
                            //1.
                            if (num == 1)
                            {
                                System.Console.Write("输入阈值（输入0则为默认值128）：");
                                argument = Convert.ToInt32(System.Console.ReadLine());
                            }
                            //2.
                            else if (num == 2)
                            {
                                System.Console.Write("输入矩阵参数n（输入0则取默认值N=2）：");
                                argument = Convert.ToInt32(System.Console.ReadLine());

                            }
                            else
                            {
                                System.Console.Write("输入矩阵参数n（输入0则取默认值N=2）：");
                                argument = Convert.ToInt32(System.Console.ReadLine());
                            }
                        }
                        else
                        {
                            System.Console.Write("输入错误请重新输入:");
                        }
                    } while (inputNum == false);
                   
                    //2
                    
                    //3
                    
                }
            }
            else if(image.InfoHeader.BitCount == 24 || image.InfoHeader.BitCount == 32)
            {
                System.Console.Write("检测到图片为真彩图像：");
                System.Console.WriteLine("可进行的操作：\n1.RGB->HSI\n2.RGB->YCbCr\n0.返回");
                System.Console.Write("选择的操作是：");
                bool inputNum = false;
                do
                {
                    int num = CheckNum();
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
                        else
                        {
                            ;
                        }
                    }
                    else
                    {
                        System.Console.Write("输入错误请重新输入:");
                    }
                } while (inputNum == false);
                
            }
            else
            {
                System.Console.Write("\n图片格式不支持模块转换哟~\n");
            }
        }
        private static void ModuleTwo(BmpImage image)
        {
            System.Console.Write("检测到图片为灰度图像：");
            System.Console.WriteLine("直方图均衡？");

            System.Console.Write("检测到图片为彩色图像：");
            System.Console.WriteLine("可进行的操作：\n1.RGB->HSI\n2.RGB->YCbCr");
            System.Console.Write("选择的操作是：");
        }
        private static void ModuleThree(BmpImage image)
        {
            System.Console.WriteLine("nihao");
        }
    }
}
