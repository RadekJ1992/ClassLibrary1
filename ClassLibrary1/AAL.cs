using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Packet {

    /*
     * Ta klasa ma za zadanie przekształcać string wpisany przez klienta 
     * do kilku pakietów ATM zgodnych ze standardem AAL 3/4, ORAZ przekształcenie
     * kilku pakietów ATM na jeden string
     * 
     * WAŻNE: pakiety nie mają ustawionych wartości VPI, VCI i port
     */
    public class AAL {

        public static byte[] GetBytesFromString(string str) {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetStringFromBytes(byte[] bytes) {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        // kolejka FIFO pakietów ATM, wysyłamy od początkowego elementu kolejki
        public static Queue<ATMPacket> getATMPackets(String text) {
            byte[] bufBytes;
            int numberOfPackets;
            int numberOfBytes;
            //tworzymy zmienną AALMid, losowa liczba z przedziału <0, 2^10)
            Random r = new Random();
            int AALMid = r.Next(1023);

            Queue<ATMPacket> q = new Queue<ATMPacket>();
            bufBytes = GetBytesFromString(text);
            numberOfBytes = bufBytes.Length;
            //ile pakietów ATM z 44bajtowym payloadem musimy stworzyć?
            if (numberOfBytes % 44 == 0) numberOfPackets = numberOfBytes / 44;
            else numberOfPackets = (numberOfBytes / 44) + 1;

            for (int i = 0; i < numberOfPackets; i++) {
                //jeśli wystarczy wysłać jeden pakiet
                if (numberOfPackets == 1) {
                    q.Enqueue(new ATMPacket(ATMPacket.AALType.SSM,
                        bufBytes, i, AALMid));
                }
                else if (i == 0) {
                    byte[] tempBytes = bufBytes.Skip(i * 44).Take(44).ToArray();
                    q.Enqueue(new ATMPacket(
                    Packet.ATMPacket.AALType.BOM,
                    tempBytes, i, AALMid));
                }
                else if (i == (numberOfPackets - 1)) {
                    byte[] tempBytes = bufBytes.Skip(i * 44).Take(numberOfBytes % 44).ToArray();
                    q.Enqueue(new ATMPacket(
                    Packet.ATMPacket.AALType.EOM,
                    tempBytes, i, AALMid));
                }
                else {
                    byte[] tempBytes = bufBytes.Skip(i * 44).Take(44).ToArray();
                    q.Enqueue(new ATMPacket(
                    Packet.ATMPacket.AALType.COM,
                    tempBytes, i, AALMid));
                }
            }
            return q;
        }

        //Podajemy kolejkę FIFO pakietów, metoda przekształca je na String
        //NA RAZIE nie ma sprawdzania AALMid i AALSeq, jak będzie działać jako tako to się to doda
        public static String getStringFromPackets(Queue<ATMPacket> queue) {
            String bufString = "";
            foreach (ATMPacket p in queue) {
                bufString += GetStringFromBytes(p.payload);
            }
            return bufString;
        }

        public static String getStringFromPacket(ATMPacket packet) {
            String bufString = "";
            //foreach (ATMPacket p in queue) {
            bufString = GetStringFromBytes(packet.payload);
            //}
            return bufString;
        }
    }
}
