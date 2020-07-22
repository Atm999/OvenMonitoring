using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Enums
{
    public class SerialPortParameter
    {
        /// </summary>
        /// 串口数据位列表（5,6,7,8）
        /// </summary>
        public enum SerialPortDatabits : int
        {
            FiveBits = 5,
            SixBits = 6,
            SeventBits = 7,
            EightBits = 8
        }
        /// </summary>
        /// 串口奇偶性校验（0,1,2,3,4）
        /// </summary>
        public enum SerialParity : int
        {
            None = 0,
            Odd = 1,
            Even = 2,
            Mark = 3,
            Space = 4
        }
        /// <summary>
        /// 串口停止位(1:使用一个停止位,3:使用1.5个停止位,2：使用二个停止位)
        /// </summary>
        public enum SerialStopBits : int
        {
            One = 1,
            OnePointFive = 3,
            Two = 2
        }

        /// <summary>
        /// 串口波特率列表。
        /// 75,110,150,300,600,1200,2400,4800,9600,14400,19200,28800,38400,56000,57600,
        /// 115200,128000,230400,256000
        /// </summary>
        public enum SerialPortBaudRates : int
        {
            BaudRate_75 = 75,
            BaudRate_110 = 110,
            BaudRate_150 = 150,
            BaudRate_300 = 300,
            BaudRate_600 = 600,
            BaudRate_1200 = 1200,
            BaudRate_2400 = 2400,
            BaudRate_4800 = 4800,
            BaudRate_9600 = 9600,
            BaudRate_14400 = 14400,
            BaudRate_19200 = 19200,
            BaudRate_28800 = 28800,
            BaudRate_38400 = 38400,
            BaudRate_56000 = 56000,
            BaudRate_57600 = 57600,
            BaudRate_115200 = 115200,
            BaudRate_128000 = 128000,
            BaudRate_230400 = 230400,
            BaudRate_256000 = 256000
        }
    }
}
