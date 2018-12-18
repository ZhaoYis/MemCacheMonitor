using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.InteropServices;
namespace Commons
{
    public static class JsonFormat
    {
        public static bool CanFormatJson(string jsonStr, out string errMsg)
        {
            errMsg = "";
            bool flag = false;
            try
            {
                JsonSerializer s = new JsonSerializer();
                JsonReader reader = new JsonReader(new StringReader(jsonStr));
                Object jsonObject = s.Deserialize(reader);
                if (jsonObject != null)
                {
                    using (StringWriter sWriter = new StringWriter())
                    {
                        using (JsonWriter writer = new JsonWriter(sWriter))
                        {
                            writer.Formatting = Newtonsoft.Json.Formatting.Indented;
                            writer.Indentation = 4;
                            writer.IndentChar = ' ';
                            s.Serialize(writer, jsonObject);
                        }
                    }
                }
                flag = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                flag = false;
            }
            return flag;
        }

        public static string FormatJson(string text)
        {
            try
            {
                JsonSerializer s = new JsonSerializer();
                JsonReader reader = new JsonReader(new StringReader(text));
                Object jsonObject = s.Deserialize(reader);
                if (jsonObject != null)
                {
                    StringWriter sWriter = new StringWriter();
                    JsonWriter writer = new JsonWriter(sWriter);
                    writer.Formatting = Newtonsoft.Json.Formatting.Indented;
                    writer.Indentation = 4;
                    writer.IndentChar = ' ';
                    s.Serialize(writer, jsonObject);
                    return sWriter.ToString();
                }
                else
                {
                    return text;
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
                throw ex;
            }
        }
    }
}
