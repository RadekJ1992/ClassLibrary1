using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packet {
    public class ATMPacket {

        public enum AALType { COM, BOM, EOM, SSM }; /*  2 bity
                                 *  01 BOM początek komunikatu
                                 *  00 COM kontynuacja komunikatu
                                 *  10 EOM koniec komunikatu
                                 *  11 SSM komunikat jednosegmentowy   */

        //Dane
        private byte[] payload; // 44 bajty payload

        //nagłówki VPI i VCI
        private int VPI; // VPI
        private int VCI; // VCI

        //nagłówki AAL
        private AALType PacketType; // typ pakietu              
        private int AALSeq; // 4 bity, do wykrycia straty komórki lub zmiany kolejności
        private int AALMid; // 10 bitów identyfikator jednostki danych protokołu 
        //ciąg pakietów zawierających jedną wiadomość ma ten sam identyfikator, simple

        public void setAALType(AALType type) {
            PacketType = type;
        }

        public AALType getAALType() {
            return PacketType;
        }

        public void setAALSeq(int seq) {
            AALSeq = seq;
        }

        public int getAALSeq() {
            return AALSeq;
        }

        public void setAALMid(int mid) {
            AALMid = mid;
        }

        public int getAALMid() {
            return AALMid;
        }

        public int getVPI() {
            return VPI;
        }

        public int getVCI() {
            return VPI;
        }

        public void setVPI(int VPI) {
            this.VPI = VPI;
        }

        public void setVCI(int VCI) {
            this.VCI = VCI;
        }

    }
}
