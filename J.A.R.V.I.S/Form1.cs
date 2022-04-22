using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Globalization;
namespace J.A.R.V.I.S
{
    public partial class Zendd : Form
    {



        private SpeechRecognitionEngine engine;
        private CultureInfo ci;


        public Zendd()
        {
            InitializeComponent();
            Init();
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Init()
        {
            try
            {
                CultureInfo ci = new CultureInfo("pt-BR");
 

                 engine = new SpeechRecognitionEngine(ci);

                SpeechRec();
             
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro em init()");
            }
        }

        private List<Grammar> Load_Grammar()
        {

            #region 
            List<Grammar> GramaticasParaFala = new List<Grammar>();

            #endregion

            #region Choices
            Choices comandosDosSistema = new Choices();
            comandosDosSistema.Add(Gramaticas.QueHorasSao.ToArray());

            #endregion


            #region GrammarBuilder
            GrammarBuilder comandosDosSistema_gb = new GrammarBuilder();
            comandosDosSistema_gb.Append(comandosDosSistema);



            #endregion



            #region Grammar

            Grammar gramaticaSistema = new Grammar(comandosDosSistema_gb);
            gramaticaSistema.Name = "system";


            #endregion
            GramaticasParaFala.Add(gramaticaSistema);

            /* return null;*/ // apenas pra não dar erro

            return GramaticasParaFala;
        
        }


        private void SpeechRec()

        {
            try
            {

               List<Grammar> g = Load_Grammar();

                #region Speech Recognition (Eventos)

                engine.SetInputToDefaultAudioDevice();

                foreach(Grammar gr in g)
                {
                    engine.LoadGrammar(gr);
                }


                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Rec);
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(AudioLevel);
                engine.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(Rejected);
                #endregion

                engine.RecognizeAsync(RecognizeMode.Multiple); //Inicia o reconhecimento 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro em SpeechRec ()");
            }
        }

        #region Eventos do Reconhecimento 
        private void Rec(object s, SpeechRecognizedEventArgs e) // Aqui é onde fica os dados que foram reconhecidos
        {

        }

        private void AudioLevel(object s, AudioLevelUpdatedEventArgs e) // Barra de Progresso - nivel do Audio
        {
            if (e.AudioLevel > barraDeAudio.Maximum)
            {
                barraDeAudio.Value = barraDeAudio.Maximum;
            }
            else if (e.AudioLevel < barraDeAudio.Minimum)
            {
                barraDeAudio.Value = e.AudioLevel;
            }
            else
            {
                this.barraDeAudio.Value = e.AudioLevel;
            }

        }

        private void Rejected(object s, SpeechRecognitionRejectedEventArgs e) // avisos de que a IA não entendeu

        {

        }
        #endregion



    }
}
