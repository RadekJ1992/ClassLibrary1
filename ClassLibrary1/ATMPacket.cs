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
        private byte[] payload { get; set; } // 44 bajty payload

        //nagłówki VPI i VCI
        private int VPI { get; set; } // VPI
        private int VCI { get; set; } // VCI

        //nagłówki AAL
        private AALType PacketType { get; set; } // typ pakietu              
        private int AALSeq { get; set; } // 4 bity, do wykrycia straty komórki lub zmiany kolejności
        private int AALMid { get; set; } // 10 bitów identyfikator jednostki danych protokołu 
        //ciąg pakietów zawierających jedną wiadomość ma ten sam identyfikator, simple
    }
}
