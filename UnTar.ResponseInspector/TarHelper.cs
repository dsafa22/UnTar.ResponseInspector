using System;
using System.Collections.Generic;
using System.Text;
using ICSharpCode.SharpZipLib.Tar;
using System.Collections;
using System.IO;
using Fiddler;
namespace UnTar.ResponseInspector
{
    public class TarHelper
    {
        public Dictionary<string, string> dic { get; set; }
        public TarHelper()
        {
            dic = new Dictionary<string, string>();
        }

        public void initDicFromTarBytes(byte[] input)
        {
            Stream inputStream = new MemoryStream(input);
            TarInputStream tarStream = new TarInputStream(inputStream);
            TarEntry entry;
            while((entry = tarStream.GetNextEntry()) != null){
                string name = entry.Name;
                MemoryStream ms = new MemoryStream();
                byte[] buff = new byte[2048];
                int size = 0;
                while((size = tarStream.Read(buff, 0, buff.Length)) > 0){
                    ms.Write(buff, 0, size);
                }
                string strContent = Encoding.UTF8.GetString(ms.ToArray());
                dic.Add(name, strContent);
                ms.Close();
            }
        }

        public void Clear()
        {
            dic.Clear();
        }
    }
}
