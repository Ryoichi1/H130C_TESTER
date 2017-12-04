using Microsoft.Practices.Prism.Mvvm;
using System.Windows.Media;


namespace H130C_Tester
{
    public class ViewModelTestResult : BindableBase
    {
        //電源電圧
        private string _Vol5v;
        public string Vol5v { get { return _Vol5v; } set { SetProperty(ref _Vol5v, value); } }

        private Brush _ColVol5v;
        public Brush ColVol5v { get { return _ColVol5v; } set { SetProperty(ref _ColVol5v, value); } }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //リレー出力 Ry1
        private string _Ry1OnTime;
        public string Ry1OnTime { get { return _Ry1OnTime; } set { SetProperty(ref _Ry1OnTime, value); } }

        private Brush _ColRy1OnTime;
        public Brush ColRy1OnTime { get { return _ColRy1OnTime; } set { SetProperty(ref _ColRy1OnTime, value); } }

        //リレー出力 Ry2
        private string _Ry2OnTime;
        public string Ry2OnTime { get { return _Ry2OnTime; } set { SetProperty(ref _Ry2OnTime, value); } }

        private Brush _ColRy2OnTime;
        public Brush ColRy2OnTime { get { return _ColRy2OnTime; } set { SetProperty(ref _ColRy2OnTime, value); } }

        //リレー出力 Ry3
        private string _Ry3OnTime;
        public string Ry3OnTime { get { return _Ry3OnTime; } set { SetProperty(ref _Ry3OnTime, value); } }

        private Brush _ColRy3OnTime;
        public Brush ColRy3OnTime { get { return _ColRy3OnTime; } set { SetProperty(ref _ColRy3OnTime, value); } }

        //リレー出力 Ry4
        private string _Ry4OnTime;
        public string Ry4OnTime { get { return _Ry4OnTime; } set { SetProperty(ref _Ry4OnTime, value); } }

        private Brush _ColRy4OnTime;
        public Brush ColRy4OnTime { get { return _ColRy4OnTime; } set { SetProperty(ref _ColRy4OnTime, value); } }

        //リレー出力 Ry5
        private string _Ry5OnTime;
        public string Ry5OnTime { get { return _Ry5OnTime; } set { SetProperty(ref _Ry5OnTime, value); } }

        private Brush _ColRy5OnTime;
        public Brush ColRy5OnTime { get { return _ColRy5OnTime; } set { SetProperty(ref _ColRy5OnTime, value); } }

        //リレー出力 Ry6
        private string _Ry6OnTime;
        public string Ry6OnTime { get { return _Ry6OnTime; } set { SetProperty(ref _Ry6OnTime, value); } }

        private Brush _ColRy6OnTime;
        public Brush ColRy6OnTime { get { return _ColRy6OnTime; } set { SetProperty(ref _ColRy6OnTime, value); } }

        //リレー出力 Ry7
        private string _Ry7OnTime;
        public string Ry7OnTime { get { return _Ry7OnTime; } set { SetProperty(ref _Ry7OnTime, value); } }

        private Brush _ColRy7OnTime;
        public Brush ColRy7OnTime { get { return _ColRy7OnTime; } set { SetProperty(ref _ColRy7OnTime, value); } }

        //リレー出力 Ry8
        private string _Ry8OnTime;
        public string Ry8OnTime { get { return _Ry8OnTime; } set { SetProperty(ref _Ry8OnTime, value); } }

        private Brush _ColRy8OnTime;
        public Brush ColRy8OnTime { get { return _ColRy8OnTime; } set { SetProperty(ref _ColRy8OnTime, value); } }

        /// <summary>
        /// 
        /// </summary>
        private Brush _ColRy1Exp;
        public Brush ColRy1Exp { get { return _ColRy1Exp; } set { SetProperty(ref _ColRy1Exp, value); } }

        private Brush _ColRy2Exp;
        public Brush ColRy2Exp { get { return _ColRy2Exp; } set { SetProperty(ref _ColRy2Exp, value); } }

        private Brush _ColRy3Exp;
        public Brush ColRy3Exp { get { return _ColRy3Exp; } set { SetProperty(ref _ColRy3Exp, value); } }

        private Brush _ColRy4Exp;
        public Brush ColRy4Exp { get { return _ColRy4Exp; } set { SetProperty(ref _ColRy4Exp, value); } }

        private Brush _ColRy5Exp;
        public Brush ColRy5Exp { get { return _ColRy5Exp; } set { SetProperty(ref _ColRy5Exp, value); } }

        private Brush _ColRy6Exp;
        public Brush ColRy6Exp { get { return _ColRy6Exp; } set { SetProperty(ref _ColRy6Exp, value); } }

        private Brush _ColRy7Exp;
        public Brush ColRy7Exp { get { return _ColRy7Exp; } set { SetProperty(ref _ColRy7Exp, value); } }

        private Brush _ColRy8Exp;
        public Brush ColRy8Exp { get { return _ColRy8Exp; } set { SetProperty(ref _ColRy8Exp, value); } }

        private Brush _ColRy1Out;
        public Brush ColRy1Out { get { return _ColRy1Out; } set { SetProperty(ref _ColRy1Out, value); } }

        private Brush _ColRy2Out;
        public Brush ColRy2Out { get { return _ColRy2Out; } set { SetProperty(ref _ColRy2Out, value); } }

        private Brush _ColRy3Out;
        public Brush ColRy3Out { get { return _ColRy3Out; } set { SetProperty(ref _ColRy3Out, value); } }

        private Brush _ColRy4Out;
        public Brush ColRy4Out { get { return _ColRy4Out; } set { SetProperty(ref _ColRy4Out, value); } }

        private Brush _ColRy5Out;
        public Brush ColRy5Out { get { return _ColRy5Out; } set { SetProperty(ref _ColRy5Out, value); } }

        private Brush _ColRy6Out;
        public Brush ColRy6Out { get { return _ColRy6Out; } set { SetProperty(ref _ColRy6Out, value); } }

        private Brush _ColRy7Out;
        public Brush ColRy7Out { get { return _ColRy7Out; } set { SetProperty(ref _ColRy7Out, value); } }

        private Brush _ColRy8Out;
        public Brush ColRy8Out { get { return _ColRy8Out; } set { SetProperty(ref _ColRy8Out, value); } }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //フォトカプラ入力 PH1-1
        private string _Ph1_1InTime;
        public string Ph1_1InTime { get { return _Ph1_1InTime; } set { SetProperty(ref _Ph1_1InTime, value); } }

        private Brush _ColPh1_1InTime;
        public Brush ColPh1_1InTime { get { return _ColPh1_1InTime; } set { SetProperty(ref _ColPh1_1InTime, value); } }

        //フォトカプラ入力 PH1-2
        private string _Ph1_2InTime;
        public string Ph1_2InTime { get { return _Ph1_2InTime; } set { SetProperty(ref _Ph1_2InTime, value); } }

        private Brush _ColPh1_2InTime;
        public Brush ColPh1_2InTime { get { return _ColPh1_2InTime; } set { SetProperty(ref _ColPh1_2InTime, value); } }

        //フォトカプラ入力 PH1-3
        private string _Ph1_3InTime;
        public string Ph1_3InTime { get { return _Ph1_3InTime; } set { SetProperty(ref _Ph1_3InTime, value); } }

        private Brush _ColPh1_3InTime;
        public Brush ColPh1_3InTime { get { return _ColPh1_3InTime; } set { SetProperty(ref _ColPh1_3InTime, value); } }

        //フォトカプラ入力 PH1-4
        private string _Ph1_4InTime;
        public string Ph1_4InTime { get { return _Ph1_4InTime; } set { SetProperty(ref _Ph1_4InTime, value); } }

        private Brush _ColPh1_4InTime;
        public Brush ColPh1_4InTime { get { return _ColPh1_4InTime; } set { SetProperty(ref _ColPh1_4InTime, value); } }

        //フォトカプラ入力 PH2-1
        private string _Ph2_1InTime;
        public string Ph2_1InTime { get { return _Ph2_1InTime; } set { SetProperty(ref _Ph2_1InTime, value); } }

        private Brush _ColPh2_1InTime;
        public Brush ColPh2_1InTime { get { return _ColPh2_1InTime; } set { SetProperty(ref _ColPh2_1InTime, value); } }

        //フォトカプラ入力 PH2-2
        private string _Ph2_2InTime;
        public string Ph2_2InTime { get { return _Ph2_2InTime; } set { SetProperty(ref _Ph2_2InTime, value); } }

        private Brush _ColPh2_2InTime;
        public Brush ColPh2_2InTime { get { return _ColPh2_2InTime; } set { SetProperty(ref _ColPh2_2InTime, value); } }

        //フォトカプラ入力 PH2-3
        private string _Ph2_3InTime;
        public string Ph2_3InTime { get { return _Ph2_3InTime; } set { SetProperty(ref _Ph2_3InTime, value); } }

        private Brush _ColPh2_3InTime;
        public Brush ColPh2_3InTime { get { return _ColPh2_3InTime; } set { SetProperty(ref _ColPh2_3InTime, value); } }

        //フォトカプラ入力 PH2-4
        private string _Ph2_4InTime;
        public string Ph2_4InTime { get { return _Ph2_4InTime; } set { SetProperty(ref _Ph2_4InTime, value); } }

        private Brush _ColPh2_4InTime;
        public Brush ColPh2_4InTime { get { return _ColPh2_4InTime; } set { SetProperty(ref _ColPh2_4InTime, value); } }

        ////
        private Brush _ColPh1_1Exp;
        public Brush ColPh1_1Exp { get { return _ColPh1_1Exp; } set { SetProperty(ref _ColPh1_1Exp, value); } }

        private Brush _ColPh1_2Exp;
        public Brush ColPh1_2Exp { get { return _ColPh1_2Exp; } set { SetProperty(ref _ColPh1_2Exp, value); } }

        private Brush _ColPh1_3Exp;
        public Brush ColPh1_3Exp { get { return _ColPh1_3Exp; } set { SetProperty(ref _ColPh1_3Exp, value); } }

        private Brush _ColPh1_4Exp;
        public Brush ColPh1_4Exp { get { return _ColPh1_4Exp; } set { SetProperty(ref _ColPh1_4Exp, value); } }

        private Brush _ColPh2_1Exp;
        public Brush ColPh2_1Exp { get { return _ColPh2_1Exp; } set { SetProperty(ref _ColPh2_1Exp, value); } }

        private Brush _ColPh2_2Exp;
        public Brush ColPh2_2Exp { get { return _ColPh2_2Exp; } set { SetProperty(ref _ColPh2_2Exp, value); } }

        private Brush _ColPh2_3Exp;
        public Brush ColPh2_3Exp { get { return _ColPh2_3Exp; } set { SetProperty(ref _ColPh2_3Exp, value); } }

        private Brush _ColPh2_4Exp;
        public Brush ColPh2_4Exp { get { return _ColPh2_4Exp; } set { SetProperty(ref _ColPh2_4Exp, value); } }

        private Brush _ColPh1_1In;
        public Brush ColPh1_1In { get { return _ColPh1_1In; } set { SetProperty(ref _ColPh1_1In, value); } }

        private Brush _ColPh1_2In;
        public Brush ColPh1_2In { get { return _ColPh1_2In; } set { SetProperty(ref _ColPh1_2In, value); } }

        private Brush _ColPh1_3In;
        public Brush ColPh1_3In { get { return _ColPh1_3In; } set { SetProperty(ref _ColPh1_3In, value); } }

        private Brush _ColPh1_4In;
        public Brush ColPh1_4In { get { return _ColPh1_4In; } set { SetProperty(ref _ColPh1_4In, value); } }

        private Brush _ColPh2_1In;
        public Brush ColPh2_1In { get { return _ColPh2_1In; } set { SetProperty(ref _ColPh2_1In, value); } }

        private Brush _ColPh2_2In;
        public Brush ColPh2_2In { get { return _ColPh2_2In; } set { SetProperty(ref _ColPh2_2In, value); } }

        private Brush _ColPh2_3In;
        public Brush ColPh2_3In { get { return _ColPh2_3In; } set { SetProperty(ref _ColPh2_3In, value); } }

        private Brush _ColPh2_4In;
        public Brush ColPh2_4In { get { return _ColPh2_4In; } set { SetProperty(ref _ColPh2_4In, value); } }


        /// <summary>
        /// /
        /// </summary>
        //LED1
        private string _HueLed1;
        public string HueLed1 { get { return _HueLed1; } set { SetProperty(ref _HueLed1, value); } }

        private Brush _ColLed1;
        public Brush ColLed1 { get { return _ColLed1; } set { SetProperty(ref _ColLed1, value); } }

        private string _LumLed1;
        public string LumLed1 { get { return _LumLed1; } set { SetProperty(ref _LumLed1, value); } }

        private Brush _ColLumLed1;
        public Brush ColLumLed1 { get { return _ColLumLed1; } set { SetProperty(ref _ColLumLed1, value); } }

        //LED2
        private string _HueLed2;
        public string HueLed2 { get { return _HueLed2; } set { SetProperty(ref _HueLed2, value); } }

        private Brush _ColLed2;
        public Brush ColLed2 { get { return _ColLed2; } set { SetProperty(ref _ColLed2, value); } }

        private string _LumLed2;
        public string LumLed2 { get { return _LumLed2; } set { SetProperty(ref _LumLed2, value); } }

        private Brush _ColLumLed2;
        public Brush ColLumLed2 { get { return _ColLumLed2; } set { SetProperty(ref _ColLumLed2, value); } }

        //LED3
        private string _HueLed3;
        public string HueLed3 { get { return _HueLed3; } set { SetProperty(ref _HueLed3, value); } }

        private Brush _ColLed3;
        public Brush ColLed3 { get { return _ColLed3; } set { SetProperty(ref _ColLed3, value); } }

        private string _LumLed3;
        public string LumLed3 { get { return _LumLed3; } set { SetProperty(ref _LumLed3, value); } }

        private Brush _ColLumLed3;
        public Brush ColLumLed3 { get { return _ColLumLed3; } set { SetProperty(ref _ColLumLed3, value); } }

        //LED4
        private string _HueLed4;
        public string HueLed4 { get { return _HueLed4; } set { SetProperty(ref _HueLed4, value); } }

        private Brush _ColLed4;
        public Brush ColLed4 { get { return _ColLed4; } set { SetProperty(ref _ColLed4, value); } }

        private string _LumLed4;
        public string LumLed4 { get { return _LumLed4; } set { SetProperty(ref _LumLed4, value); } }

        private Brush _ColLumLed4;
        public Brush ColLumLed4 { get { return _ColLumLed4; } set { SetProperty(ref _ColLumLed4, value); } }

        //LED5
        private string _HueLed5;
        public string HueLed5 { get { return _HueLed5; } set { SetProperty(ref _HueLed5, value); } }

        private Brush _ColLed5;
        public Brush ColLed5 { get { return _ColLed5; } set { SetProperty(ref _ColLed5, value); } }

        private string _LumLed5;
        public string LumLed5 { get { return _LumLed5; } set { SetProperty(ref _LumLed5, value); } }

        private Brush _ColLumLed5;
        public Brush ColLumLed5 { get { return _ColLumLed5; } set { SetProperty(ref _ColLumLed5, value); } }

        //LED6
        private string _HueLed6;
        public string HueLed6 { get { return _HueLed6; } set { SetProperty(ref _HueLed6, value); } }

        private Brush _ColLed6;
        public Brush ColLed6 { get { return _ColLed6; } set { SetProperty(ref _ColLed6, value); } }

        private string _LumLed6;
        public string LumLed6 { get { return _LumLed6; } set { SetProperty(ref _LumLed6, value); } }

        private Brush _ColLumLed6;
        public Brush ColLumLed6 { get { return _ColLumLed6; } set { SetProperty(ref _ColLumLed6, value); } }

        //LED7
        private string _HueLed7;
        public string HueLed7 { get { return _HueLed7; } set { SetProperty(ref _HueLed7, value); } }

        private Brush _ColLed7;
        public Brush ColLed7 { get { return _ColLed7; } set { SetProperty(ref _ColLed7, value); } }

        private string _LumLed7;
        public string LumLed7 { get { return _LumLed7; } set { SetProperty(ref _LumLed7, value); } }

        private Brush _ColLumLed7;
        public Brush ColLumLed7 { get { return _ColLumLed7; } set { SetProperty(ref _ColLumLed7, value); } }

        //LED8
        private string _HueLed8;
        public string HueLed8 { get { return _HueLed8; } set { SetProperty(ref _HueLed8, value); } }

        private Brush _ColLed8;
        public Brush ColLed8 { get { return _ColLed8; } set { SetProperty(ref _ColLed8, value); } }

        private string _LumLed8;
        public string LumLed8 { get { return _LumLed8; } set { SetProperty(ref _LumLed8, value); } }

        private Brush _ColLumLed8;
        public Brush ColLumLed8 { get { return _ColLumLed8; } set { SetProperty(ref _ColLumLed8, value); } }


        //LED9
        private string _HueLed9;
        public string HueLed9 { get { return _HueLed9; } set { SetProperty(ref _HueLed9, value); } }

        private Brush _ColLed9;
        public Brush ColLed9 { get { return _ColLed9; } set { SetProperty(ref _ColLed9, value); } }

        private string _LumLed9;
        public string LumLed9 { get { return _LumLed9; } set { SetProperty(ref _LumLed9, value); } }

        private Brush _ColLumLed9;
        public Brush ColLumLed9 { get { return _ColLumLed9; } set { SetProperty(ref _ColLumLed9, value); } }

        //LED10
        private string _HueLed10;
        public string HueLed10 { get { return _HueLed10; } set { SetProperty(ref _HueLed10, value); } }

        private Brush _ColLed10;
        public Brush ColLed10 { get { return _ColLed10; } set { SetProperty(ref _ColLed10, value); } }

        private string _LumLed10;
        public string LumLed10 { get { return _LumLed10; } set { SetProperty(ref _LumLed10, value); } }

        private Brush _ColLumLed10;
        public Brush ColLumLed10 { get { return _ColLumLed10; } set { SetProperty(ref _ColLumLed10, value); } }

        //LED11
        private string _HueLed11;
        public string HueLed11 { get { return _HueLed11; } set { SetProperty(ref _HueLed11, value); } }

        private Brush _ColLed11;
        public Brush ColLed11 { get { return _ColLed11; } set { SetProperty(ref _ColLed11, value); } }

        private string _LumLed11;
        public string LumLed11 { get { return _LumLed11; } set { SetProperty(ref _LumLed11, value); } }

        private Brush _ColLumLed11;
        public Brush ColLumLed11 { get { return _ColLumLed11; } set { SetProperty(ref _ColLumLed11, value); } }

        //LED12
        private string _HueLed12;
        public string HueLed12 { get { return _HueLed12; } set { SetProperty(ref _HueLed12, value); } }

        private Brush _ColLed12;
        public Brush ColLed12 { get { return _ColLed12; } set { SetProperty(ref _ColLed12, value); } }

        private string _LumLed12;
        public string LumLed12 { get { return _LumLed12; } set { SetProperty(ref _LumLed12, value); } }

        private Brush _ColLumLed12;
        public Brush ColLumLed12 { get { return _ColLumLed12; } set { SetProperty(ref _ColLumLed12, value); } }

        //LED13
        private string _HueLed13;
        public string HueLed13 { get { return _HueLed13; } set { SetProperty(ref _HueLed13, value); } }

        private Brush _ColLed13;
        public Brush ColLed13 { get { return _ColLed13; } set { SetProperty(ref _ColLed13, value); } }

        private string _LumLed13;
        public string LumLed13 { get { return _LumLed13; } set { SetProperty(ref _LumLed13, value); } }

        private Brush _ColLumLed13;
        public Brush ColLumLed13 { get { return _ColLumLed13; } set { SetProperty(ref _ColLumLed13, value); } }

        //LED14
        private string _HueLed14;
        public string HueLed14 { get { return _HueLed14; } set { SetProperty(ref _HueLed14, value); } }

        private Brush _ColLed14;
        public Brush ColLed14 { get { return _ColLed14; } set { SetProperty(ref _ColLed14, value); } }

        private string _LumLed14;
        public string LumLed14 { get { return _LumLed14; } set { SetProperty(ref _LumLed14, value); } }

        private Brush _ColLumLed14;
        public Brush ColLumLed14 { get { return _ColLumLed14; } set { SetProperty(ref _ColLumLed14, value); } }

        //LED15
        private string _HueLed15;
        public string HueLed15 { get { return _HueLed15; } set { SetProperty(ref _HueLed15, value); } }

        private Brush _ColLed15;
        public Brush ColLed15 { get { return _ColLed15; } set { SetProperty(ref _ColLed15, value); } }

        private string _LumLed15;
        public string LumLed15 { get { return _LumLed15; } set { SetProperty(ref _LumLed15, value); } }

        private Brush _ColLumLed15;
        public Brush ColLumLed15 { get { return _ColLumLed15; } set { SetProperty(ref _ColLumLed15, value); } }

        //LED16
        private string _HueLed16;
        public string HueLed16 { get { return _HueLed16; } set { SetProperty(ref _HueLed16, value); } }

        private Brush _ColLed16;
        public Brush ColLed16 { get { return _ColLed16; } set { SetProperty(ref _ColLed16, value); } }

        private string _LumLed16;
        public string LumLed16 { get { return _LumLed16; } set { SetProperty(ref _LumLed16, value); } }

        private Brush _ColLumLed16;
        public Brush ColLumLed16 { get { return _ColLumLed16; } set { SetProperty(ref _ColLumLed16, value); } }



        //スイッチ状態
        private Brush _ColSw1_1;
        public Brush ColSw1_1 { get { return _ColSw1_1; } set { SetProperty(ref _ColSw1_1, value); } }

        private Brush _ColSw1_2;
        public Brush ColSw1_2 { get { return _ColSw1_2; } set { SetProperty(ref _ColSw1_2, value); } }

        private Brush _ColSw1_3;
        public Brush ColSw1_3 { get { return _ColSw1_3; } set { SetProperty(ref _ColSw1_3, value); } }

        private Brush _ColSw1_4;
        public Brush ColSw1_4 { get { return _ColSw1_4; } set { SetProperty(ref _ColSw1_4, value); } }

        private Brush _ColSw1_1Exp;
        public Brush ColSw1_1Exp { get { return _ColSw1_1Exp; } set { SetProperty(ref _ColSw1_1Exp, value); } }

        private Brush _ColSw1_2Exp;
        public Brush ColSw1_2Exp { get { return _ColSw1_2Exp; } set { SetProperty(ref _ColSw1_2Exp, value); } }

        private Brush _ColSw1_3Exp;
        public Brush ColSw1_3Exp { get { return _ColSw1_3Exp; } set { SetProperty(ref _ColSw1_3Exp, value); } }

        private Brush _ColSw1_4Exp;
        public Brush ColSw1_4Exp { get { return _ColSw1_4Exp; } set { SetProperty(ref _ColSw1_4Exp, value); } }

        //IOポート（入力）
        private Brush _ColP00In;
        public Brush ColP00In { get { return _ColP00In; } set { SetProperty(ref _ColP00In, value); } }

        private Brush _ColP01In;
        public Brush ColP01In { get { return _ColP01In; } set { SetProperty(ref _ColP01In, value); } }

        private Brush _ColP02In;
        public Brush ColP02In { get { return _ColP02In; } set { SetProperty(ref _ColP02In, value); } }

        private Brush _ColP03In;
        public Brush ColP03In { get { return _ColP03In; } set { SetProperty(ref _ColP03In, value); } }

        private Brush _ColP00InExp;
        public Brush ColP00InExp { get { return _ColP00InExp; } set { SetProperty(ref _ColP00InExp, value); } }

        private Brush _ColP01InExp;
        public Brush ColP01InExp { get { return _ColP01InExp; } set { SetProperty(ref _ColP01InExp, value); } }

        private Brush _ColP02InExp;
        public Brush ColP02InExp { get { return _ColP02InExp; } set { SetProperty(ref _ColP02InExp, value); } }

        private Brush _ColP03InExp;
        public Brush ColP03InExp { get { return _ColP03InExp; } set { SetProperty(ref _ColP03InExp, value); } }

        //IOポート（出力）
        private Brush _ColP00Out;
        public Brush ColP00Out { get { return _ColP00Out; } set { SetProperty(ref _ColP00Out, value); } }

        private Brush _ColP01Out;
        public Brush ColP01Out { get { return _ColP01Out; } set { SetProperty(ref _ColP01Out, value); } }

        private Brush _ColP02Out;
        public Brush ColP02Out { get { return _ColP02Out; } set { SetProperty(ref _ColP02Out, value); } }

        private Brush _ColP03Out;
        public Brush ColP03Out { get { return _ColP03Out; } set { SetProperty(ref _ColP03Out, value); } }

        private Brush _ColP00OutExp;
        public Brush ColP00OutExp { get { return _ColP00OutExp; } set { SetProperty(ref _ColP00OutExp, value); } }

        private Brush _ColP01OutExp;
        public Brush ColP01OutExp { get { return _ColP01OutExp; } set { SetProperty(ref _ColP01OutExp, value); } }

        private Brush _ColP02OutExp;
        public Brush ColP02OutExp { get { return _ColP02OutExp; } set { SetProperty(ref _ColP02OutExp, value); } }

        private Brush _ColP03OutExp;
        public Brush ColP03OutExp { get { return _ColP03OutExp; } set { SetProperty(ref _ColP03OutExp, value); } }

    }

}








