using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packet {
    public class ATMPacket {

        public enum AALType {COM, BOM, EOM, SSM}; /*  2 bity
                                 *  01 BOM początek komunikatu
                                 *  00 COM kontynuacja komunikatu
                                 *  10 EOM koniec komunikatu
                                 *  11 SSM komunikat jednosegmentowy   */

        //Dane
        public byte[] payload { get; private set; } // 44 bajty payload

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

        public ATMPacket(AALType type, byte[] payload, int AALSeq, int AALMid) {
            this.PacketType = type;
            this.payload = payload;
            this.AALMid = AALMid;
            this.AALSeq = AALSeq;
        }
    
    }
}
