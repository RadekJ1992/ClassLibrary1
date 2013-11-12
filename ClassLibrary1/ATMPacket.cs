using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Packet {

    [Serializable()]
    public class ATMPacket : ISerializable {

        public enum AALType {COM, BOM, EOM, SSM}; /*  2 bity
                                 *  01 BOM początek komunikatu
                                 *  00 COM kontynuacja komunikatu
                                 *  10 EOM koniec komunikatu
                                 *  11 SSM komunikat jednosegmentowy   */


        /*
         * WAŻNE - port z którego pakiet został WYSŁANY z poprzedniego węzła
         * każdy węzeł po zmianie VCI i VPI pakietu dopisuje tutaj numer portu, z którego wychodzi pakiet, 
         * dla przykładu 1. Chmura po otrzymaniu pakietu sprawdza to pole i wie że 
         * "pakiet wyszedł z węzła x portem 1, który jest połączony do portu 2 węzła y. 
         * Chmura zmienia wartosć tego pola na 2 i wysyła do węzła y. Węzeł y widzi że przyszedł pakiet, czyta 
         * wartość tego pola i wie że "ten pakiet przyszedł na ten port, więc zrobię to i to
         */
        public int port { get; set; }

        //nagłówki VPI i VCI
        public int VPI { get; set; } // VPI
        public int VCI { get; set; } // VCI

        //nagłówki AAL
        public AALType PacketType { get; private set; } // typ pakietu              
        public int AALSeq { get; private set; } // 4 bity, do wykrycia straty komórki lub zmiany kolejności
        public int AALMid { get; private set; } // 10 bitów identyfikator jednostki danych protokołu 
        //ciąg pakietów zawierających jedną wiadomość ma ten sam identyfikator, simple

        //Dane
        public byte[] payload { get; private set; } // 44 bajty payload

        public ATMPacket(AALType type, byte[] payload, int AALSeq, int AALMid) {
            this.PacketType = type;
            this.payload = payload;
            this.AALMid = AALMid;
            this.AALSeq = AALSeq;
        }

        //konstruktor deserializujący
        public ATMPacket(SerializationInfo info, StreamingContext ctxt) {
            //Get the values from info and assign them to the appropriate properties
            port = (int)info.GetValue("port", typeof(int));
            VPI = (int)info.GetValue("VPI", typeof(int));
            VCI = (int)info.GetValue("VCI", typeof(int));
            PacketType = (Packet.ATMPacket.AALType)info.GetValue("PacketType", typeof(Packet.ATMPacket.AALType));
            AALSeq = (int)info.GetValue("AALSeq", typeof(int));
            AALMid = (int)info.GetValue("AALMid", typeof(int));
            payload = (byte[])info.GetValue("payload", typeof(byte[]));
        }
        //metoda serializująca
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt) {
            info.AddValue("port", port);
            info.AddValue("VPI", VPI);
            info.AddValue("VCI", VCI);
            info.AddValue("PacketType", PacketType);
            info.AddValue("AALSeq", AALSeq);
            info.AddValue("AALMid", AALMid);
            info.AddValue("payload", payload);
        }
    }
}
