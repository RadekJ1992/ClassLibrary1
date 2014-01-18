using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Packet 
{   
    [Serializable]
    public class SPacket : ISerializable
    {
        private string src;
        private string dest;
        private List<string> parames;

        public SPacket()
        {
            src = "-";
            dest = "-";
            parames = new List<string>();
        }
        public SPacket(string source, string destination, List<string> parames)
        {
            src = (string)source.Clone();
            dest = (string)destination.Clone();
            this.parames = new List<string>(parames);
        }
        public SPacket(string source, string destination, string parames)
        {
            src = source;
            dest = destination;
            this.setParames(parames);
        }
        //konstruktor deserializujący
        public SPacket(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            src = (string)info.GetValue("src", typeof(string));
            dest = (string)info.GetValue("dest", typeof(string));
            parames = (List<string>)info.GetValue("parames", typeof(List<string>));
            
        }
        //metoda serializująca
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("src", src);
            info.AddValue("dest", dest);
            info.AddValue("parames", parames);
            
        }
        public void addParam(string a)
        {
            if (parames == null)
            {
                Console.Out.WriteLine("Wskaźnik na listę parametrów jest null");
                return;
            }
            else
            {
                parames.Add(a);
                Console.Out.WriteLine("Pomyślnie dodało parametr");
            }
        }
		public void setParames(String komenda)//splituje stringa gdzie znakiem rozdzielającym jest string i dodaje kolejne stringi po pdoziale do tablicy, kasuje poprzednią listę!!!
        {
            parames = new List<string>();
            String[] splitowane = komenda.Split(' ');
            for(int i =0;i<splitowane.Length;i++)
            {
                parames.Add(splitowane[i]);
            }
        }


        public override string ToString()
        {
            string result= "from: " + src + " to: " + dest + " params: ";
            for (int i = 0; i < parames.Count; i++ )
            {
                result += parames.ElementAt(i) + " | ";
            }
                return result;
        }
        public string getSrc()
        {
            return this.src;
        }
        public void setSrc(string s)
        {
            this.src = s;
        }
        public string getDest()
        {
            return this.dest;
        }
        public void setDest(string d)
        {
            this.dest = d;
        }
        public List<string> getParames()
        {
            return this.parames;
        }
        public void setParames(List<string> AL)
        {
            this.parames = AL;
        }
    }
}
