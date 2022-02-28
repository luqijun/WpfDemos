using Newtonsoft.Json;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SerializeCompare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Person> lstPerson = new List<Person>();
            for (int i = 0; i < 100000; i++)
            {
                lstPerson.Add(new Person("Person" + i, 23));
            }

            Serialize_ProtoBuf(lstPerson);
            Serialize_Json(lstPerson);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            byte[] bytes_protobuf = File.ReadAllBytes("Person-ProtoBuf.txt");
            List<Person>? lstResults1 = ProtoBuf.Serializer.Deserialize<List<Person>>(new MemoryStream(bytes_protobuf));
            sw.Stop();
            Debug.WriteLine("ProtoBuf耗时：" + sw.Elapsed);


            sw.Restart();
            string json = File.ReadAllText("Person-Json.txt");
            List<Person>? lstResults2 = JsonConvert.DeserializeObject<List<Person>>(json);
            sw.Stop();
            Debug.WriteLine("Json耗时：" + sw.Elapsed);

        }

        private byte[] Serialize_ProtoBuf(object model)
        {
            try
            {
                //涉及格式转换，需要用到流，将二进制序列化到流中
                using (FileStream fs = new FileStream("Person-ProtoBuf.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    //使用ProtoBuf工具的序列化方法
                    ProtoBuf.Serializer.Serialize<object>(fs, model);
                    //定义二级制数组，保存序列化后的结果
                    byte[] result = new byte[fs.Length];
                    //将流的位置设为0，起始点
                    fs.Position = 0;
                    //将流中的内容读取到二进制数组中
                    fs.Read(result, 0, result.Length);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return new byte[3];
            }

        }

        private void Serialize_Json(object model)
        {
            string json = JsonConvert.SerializeObject(model);
            File.WriteAllText("Person-Json.txt", json);
        }


    }

    [ProtoContract]
    public class Person
    {

        [ProtoMember(1)]
        public string? Name { get; set; }


        [ProtoMember(2)]
        public int Age { get; set; }

        public Person()
        {

        }

        public Person(string? name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}
