﻿# 需求文档

## 文件处理模块

通过“文件打开” 选项将某一待处理bmp文件读入，允许用户通过浏览方式或键盘键入方式选定待打开文件的路径及名称

通过“文件保存” 选项将某一处理结果以bmp格式保存至指定目录，允许用户通过浏览方式或键盘键入方式选定存储位置及文件名

## 模式转换模块

对当前打开文件，根据其*位深*可允许用户选择其中1项处理
- gray to binary ( 单阈值法， dither， ordered dither）
    若当前待处理图像为gray图像，通过提供多种选项的方式允许用户将该图像转换为黑白图像，并将其在窗口显视或存储至某一目录；对用户选择处理项，允许用户设置进行该处理的具体参数，如阈值，dither 矩阵的大小
- color to gray
    若当前待处理图像为真彩图像，允许用户选择某一种彩色变换方式（如RGB—HSI，  RGB--YCbCr）计算亮度分量，并将计算所得亮度分量在窗口显视或直接存储至某一目录

## 图像增强模块

- 直方图均衡
    - 若当前待处理图像为gray图像， 对其做直方图均衡，并将均衡结果在窗口显视或存储至某一目录
    - 若当前待处理图像为真彩图像，对其进行彩色变换（如RGB—HSI，  RGB--YCbCr），并对亮度分量做直方图均衡，然后做彩色反变换得到均衡结果，最后将均衡结果在窗口显视或存储至某一目录
- 自由发挥
    - 如显示或存储直方图均衡过程中的中间结果
    - 直方图匹配，…, …

## 图像压缩模块

- 无损预测编码 + 符号编码
    - 实现8-bit灰度图像的线性预测编码，允许用户选择预测器阶次并设置预测系数，并将预测结果**以图像的方式**在窗口显示或存储至某一目录
        > -255-255范围处理方案：
        > 可通过线性变换为0-255
        > 用2个8-bit图像来表示，1个用来表示正负号,1个用来表示绝对值
    - 对预测结果进行huffman编码或算术编码
- 均匀量化
    - 实现8-bit灰度图像的均匀量化，允许用户设定压缩比，然后反量化获得恢复图像，并将恢复图像在窗口显示或存储至某一目录
    - 实现8-bit灰度图像的压缩比为2的均匀量化改进版IGS，然后反量化获得恢复图像，并将恢复图像在窗口显示或存储至某一目录
- DCT变换及DCT反变换
    1. 实现8-bit灰度图像的分块DCT变换，允许用户设置分块大小，并将变换结果**以图像的方式**在窗口显示或存储至某一目录，
    2. 对步骤1得到*变换结果*进行DCT反变换，将反变换结果（即恢复图像）在窗口显示或存储至某一目录，并与原始的灰度图像进行对比
    3. 将步骤1得到*变换结果*扔掉50%的数据（即将50%的高频系数用0代替），然后进行DCT反变换，将反变换结果（即恢复图像）在窗口显示或存储至某一目录，并与原始的灰度图像及步骤2的结果进行对比




