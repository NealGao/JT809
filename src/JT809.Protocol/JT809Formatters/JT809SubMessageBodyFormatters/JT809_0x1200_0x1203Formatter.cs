﻿using JT809.Protocol.JT809Enums;
using JT809.Protocol.JT809Extensions;
using JT809.Protocol.JT809Properties;
using JT809.Protocol.JT809SubMessageBody;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace JT809.Protocol.JT809Formatters.JT809SubMessageBodyFormatters
{
    public class JT809_0x1200_0x1203Formatter : IJT809Formatter<JT809_0x1200_0x1203>
    {
        public JT809_0x1200_0x1203 Deserialize(ReadOnlySpan<byte> bytes, out int readSize)
        {
            int offset = 0;
            JT809_0x1200_0x1203 jT809_0X1200_0X1203 = new JT809_0x1200_0x1203();
            jT809_0X1200_0X1203.GNSSCount= JT809BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT809_0X1200_0X1203.GNSS = new List<JT809_0x1200_0x1202>();
            if (jT809_0X1200_0X1203.GNSSCount > 0)
            {
                int bufReadSize;
                int tempOffset = 0;
                for (int i = 0; i < jT809_0X1200_0X1203.GNSSCount; i++)
                {
                    try
                    {
                        if (i == 0)
                        {
                            tempOffset = offset + 36;
                            JT809_0x1200_0x1202 jT809_0x1200_0x1202 = JT809FormatterExtensions.GetFormatter<JT809_0x1200_0x1202>()
                                                .Deserialize(bytes.Slice(offset, 36), out bufReadSize);
                            jT809_0X1200_0X1203.GNSS.Add(jT809_0x1200_0x1202);
                        }
                        else
                        {
                            JT809_0x1200_0x1202 jT809_0x1200_0x1202 = JT809FormatterExtensions.GetFormatter<JT809_0x1200_0x1202>()
                                                .Deserialize(bytes.Slice(tempOffset, 36), out bufReadSize);
                            tempOffset += 36;
                            jT809_0X1200_0X1203.GNSS.Add(jT809_0x1200_0x1202);
                        }
                    }
                    catch (Exception)
                    {
                        tempOffset += 36;
                    }
                }
            }   
            readSize = jT809_0X1200_0X1203.GNSSCount * 36 + 1;
            return jT809_0X1200_0X1203;
        }

        public int Serialize(ref byte[] bytes, int offset, JT809_0x1200_0x1203 value)
        {
            offset += JT809BinaryExtensions.WriteByteLittle(bytes, offset, (byte)value.GNSS.Count);
            foreach(var item in value.GNSS)
            {
                try
                {
                    int positionOffset = JT809FormatterExtensions.GetFormatter<JT809_0x1200_0x1202>().Serialize(ref bytes, offset, item);
                    offset = positionOffset;
                }
                catch (Exception ex)
                {

                }
            }
            return offset;
        }
    }
}
