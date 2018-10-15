using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    public int turnoID; //0 O 1 X
    public int contadorTurno;
    [SerializeField]public GameObject[] iconesTurno;
    [SerializeField]public Sprite[] playerIcones; //identificar OX
    [SerializeField]public Button[] tictactoeEspaco;
    public bool isCPU;// saber se é o turno da cpu
    public int[] locaisMarcados; //quem marcou o que
    [SerializeField] Text resultText;
    [SerializeField] GameObject[] winningLines; //linhas de vitoria
    bool vencedor = false;
    [SerializeField] GameObject resultPanel;
    public int xPlayerScore;
    public int oPlayerScore;
    [SerializeField] Text xPlayerScoreText;
    [SerializeField] Text oPlayerScoreText;
    [SerializeField] Button oPlayersBotao;
    [SerializeField] Button xPlayerBotao;
    [SerializeField] GameObject deuVelha;
    [SerializeField]public AudioSource soundsFX;


    // Use this for initialization
    void Start () {
        isCPU = false; //inicia turno com player
        GameSetup();
	}
    void GameSetup() {
        isCPU = false;
        turnoID = 0;
        contadorTurno = 0;
        iconesTurno[0].SetActive(true);
        iconesTurno[1].SetActive(false);
        for (int i = 0; i < tictactoeEspaco.Length; i++) {

            tictactoeEspaco[i].interactable = true;
            tictactoeEspaco[i].GetComponent<Image>().sprite = null; //limpa o jogo
                }
        for (int i = 0; i < locaisMarcados.Length; i++) {
            locaisMarcados[i] = -100; 

        }

    }

    // Verificar vitoria em horizontal vertical e diagonal
    bool WinnerCheck()
    {
        int vitoria1 = locaisMarcados[0] + locaisMarcados[1] + locaisMarcados[2];
        int vitoria2 = locaisMarcados[3] + locaisMarcados[4] + locaisMarcados[5];
        int vitoria3 = locaisMarcados[6] + locaisMarcados[7] + locaisMarcados[8];
        int vitoria4 = locaisMarcados[0] + locaisMarcados[3] + locaisMarcados[6];
        int vitoria5 = locaisMarcados[1] + locaisMarcados[4] + locaisMarcados[7];
        int vitoria6 = locaisMarcados[2] + locaisMarcados[5] + locaisMarcados[8];
        int vitoria7 = locaisMarcados[0] + locaisMarcados[4] + locaisMarcados[8];
        int vitoria8 = locaisMarcados[2] + locaisMarcados[4] + locaisMarcados[6];
        var solucoes = new int[] { vitoria1, vitoria2, vitoria3, vitoria4, vitoria5, vitoria6, vitoria7, vitoria8 };
        for (int i = 0; i < solucoes.Length; i++)
        {
            if (solucoes[i] == 3 * (turnoID + 1))
            {
                vencedor = true;
                isCPU = false;
                TelaVitoria(i);
                return true;
            }
            
        }
        return false;

    }

    public void TicTacToeButton( int queNumero) {
        if (!isCPU)
        {
            xPlayerBotao.interactable = false;
            oPlayersBotao.interactable = false;
            tictactoeEspaco[queNumero].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[queNumero].interactable = false;
            locaisMarcados[queNumero] = turnoID + 1;// preenche o array com 1 ou 2
            contadorTurno++;  //passagem de turno

            if (contadorTurno > 4)
            {
                bool isWinner = WinnerCheck();

                if (contadorTurno == 9 && isWinner == false)
                {
                    Velha();
                }
            }

                if (turnoID == 0)
            {
                turnoID = 1;
                iconesTurno[0].SetActive(false);
                iconesTurno[1].SetActive(true);
            }
            else
           {
                turnoID = 0;
                iconesTurno[0].SetActive(true);
                iconesTurno[1].SetActive(false);
           }
            if (!vencedor)
            {
                isCPU = true;
            }
        }

        soundsFX.Play();
    }


    void TelaVitoria(int indexIn)
    {
        resultPanel.gameObject.SetActive(true);
        if(turnoID == 0)
        {
            oPlayerScore++;
            oPlayerScoreText.text = oPlayerScore.ToString();
            resultText.text = "VOCE GANHOU!!!";
            
        }
        else if(turnoID == 1)
        {
            
            xPlayerScore++;
            xPlayerScoreText.text = xPlayerScore.ToString();
            resultText.text = "VOCE PERDEU  :(";
        }

        winningLines[indexIn].SetActive(true);
   

    }


    //AI do CPU


    public void CPUmove()
    {

        turnoID = 1;
        int valorDificuldade = 0;
        if (oPlayerScore <= xPlayerScore)
        {
            valorDificuldade = 20;
        }
        else
        {
            valorDificuldade = 100;
        }
        int fatorhumano = Random.Range(0, valorDificuldade);
        // fator humano irá forçar a cpu a não counterar o jogador com chance de 5% caso esteja perdendo e 1% caso esteja ganhando
        print("Fator humano " + fatorhumano);

        switch (contadorTurno)
        {
            case 1:

                /*  - - -
                    - X -
                    - - -
                */
                if (locaisMarcados[4] == -100)
                {
                    locaisMarcados[4] = 2;
                    tictactoeEspaco[4].image.sprite = playerIcones[turnoID];
                    tictactoeEspaco[4].interactable = false;
                    TrocaTurno();
                }
                else

                /*  - - X
                    - O -
                    - - -
                */
                if (contadorTurno == 1) {
                    MovimentoTaticoAI();
                }


                break;
            case 2:
            
                break;
            case 3:

                //Countera todas as possibilidades de vitoria no caso 4
                CounterAI();
                if (contadorTurno == 3)
                {
                    MovimentoTaticoAI();
                }

                break;
            case 4:
               
                break;
            case 5:


                ProcuraVitoriaAI();
                if (contadorTurno == 5 && fatorhumano != 1) {
                    CounterAI();
                }
                if (contadorTurno == 5)
                {
                    MovimentoTaticoAI();
                }

                break;
            case 6:
               
                break;
            case 7:

                ProcuraVitoriaAI();
                if (contadorTurno == 7 && fatorhumano != 1)
                {
                    CounterAI();
                }
                if (contadorTurno == 7)
                {
                    MovimentoTaticoAI();
                }



                break;
            case 8:
              
                break;


        }
      

    }

    private void TrocaTurno()
    {
        if (contadorTurno > 4)
        {
            bool isWinner = WinnerCheck();

            if (contadorTurno == 9 && isWinner == false)
            {
                Velha();
            }
        }
        contadorTurno++;
        turnoID = 0;
        iconesTurno[0].SetActive(true);
        iconesTurno[1].SetActive(false);
        isCPU = false;
    }



    /*
     * 
     * CASO NÃO COUNTERE OU VENÇA CPU PROCURA MONTAR V
     * 
     * 
     */


    private void MovimentoTaticoAI()
    {
        //Preenche criação de V


        int randomMark = Random.Range(2, 7);

        /* - - X?
           X?X?X?
           X?- -
                 */

        if (locaisMarcados[randomMark] == -100)
        {
            locaisMarcados[randomMark] = 2;
            tictactoeEspaco[randomMark].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[randomMark].interactable = false;
            TrocaTurno();
        }
        else

     



        /* - X -
           - - -
           - - -
           */

        if (locaisMarcados[1] == -100)
        {
            locaisMarcados[1] = 2;
            tictactoeEspaco[1].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[1].interactable = false;
            TrocaTurno();
        }
        else

        /* - - -
           - - -
           - X -
           */

        if (locaisMarcados[7] == -100)
        {
            locaisMarcados[7] = 2;
            tictactoeEspaco[7].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[7].interactable = false;
            TrocaTurno();
        }
        else

        /* - - -
           - - -
           X - -
        */

       if (locaisMarcados[6] == -100)
       {
            locaisMarcados[6] = 2;
            tictactoeEspaco[6].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[6].interactable = false;
            TrocaTurno();
        }
        else



        /* - - X
           - - -
           - - -
   */

        if (locaisMarcados[2] == -100)
        {
            locaisMarcados[2] = 2;
            tictactoeEspaco[2].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[2].interactable = false;
            TrocaTurno();
        }
        else



        /* X - -
           - - -
           - - -
   */

        if (locaisMarcados[0] == -100)
        {
            locaisMarcados[0] = 2;
            tictactoeEspaco[0].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[0].interactable = false;
            TrocaTurno();
        }
        else

        /* - - X
           - - -
           - - -
*/

        if (locaisMarcados[8] == -100)
        {
            locaisMarcados[8] = 2;
            tictactoeEspaco[8].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[8].interactable = false;
            TrocaTurno();
        }


    }


    /*
     * 
     * CPU TENTA VENCER
     * 
     * 
     */


    private void ProcuraVitoriaAI()
    {
        //Metodo de vitoria IA

        /* O - O
           - - -
           - - -  
        */

        if (locaisMarcados[0] == 2 && locaisMarcados[2] == 2 && locaisMarcados[1] == -100)
        {

            locaisMarcados[1] = 2;
            tictactoeEspaco[1].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[1].interactable = false;
            TrocaTurno();
        }
        else

        /* O O -
           - - -
           - - -
        */

        if (locaisMarcados[0] == 2 && locaisMarcados[1] == 2 && locaisMarcados[2] == -100)
        {

            locaisMarcados[2] = 2;
            tictactoeEspaco[2].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[2].interactable = false;
            TrocaTurno();
        }
        else
        /* - O O
           - - -
           - - -
    */

        if (locaisMarcados[1] == 2 && locaisMarcados[2] == 2 && locaisMarcados[0] == -100)
        {

            locaisMarcados[0] = 2;
            tictactoeEspaco[0].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[0].interactable = false;
            TrocaTurno();
        }

        else
        /* O - -
           O - -
           - - -
        */

        if (locaisMarcados[0] == 2 && locaisMarcados[3] == 2 && locaisMarcados[6] == -100)
        {

            locaisMarcados[6] = 2;
            tictactoeEspaco[6].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[6].interactable = false;
            TrocaTurno();
        }

        else

        /* O - -
           - - -
           O - -
        */

        if (locaisMarcados[0] == 2 && locaisMarcados[6] == 2 && locaisMarcados[3] == -100)
        {

            locaisMarcados[3] = 2;
            tictactoeEspaco[3].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[3].interactable = false;
            TrocaTurno();
        }

        else

        /* - - -
           O - -
           O - -
        */

        if (locaisMarcados[3] == 2 && locaisMarcados[6] == 2 && locaisMarcados[0] == -100)
        {

            locaisMarcados[0] = 2;
            tictactoeEspaco[0].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[0].interactable = false;
            TrocaTurno();
        }

        else
        /* O - -
           - O -
           - - -
      */

        if (locaisMarcados[0] == 2 && locaisMarcados[4] == 2 && locaisMarcados[8] == -100)
        {

            locaisMarcados[8] = 2;
            tictactoeEspaco[8].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[8].interactable = false;
            TrocaTurno();
        }

        else

        /* O - -
           - - -
           - - O
    */

        if (locaisMarcados[0] == 2 && locaisMarcados[8] == 2 && locaisMarcados[4] == -100)
        {

            locaisMarcados[4] = 2;
            tictactoeEspaco[4].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[4].interactable = false;
            TrocaTurno();
        }

        else

        /* - - -
           - O -
           - - O
  */
        if (locaisMarcados[4] == 2 && locaisMarcados[8] == 2 && locaisMarcados[0] == -100)
        {

            locaisMarcados[0] = 2;
            tictactoeEspaco[0].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[0].interactable = false;
            TrocaTurno();
        }

        else

        /* - O -
           - O -
           - - -
  */

        if (locaisMarcados[1] == 2 && locaisMarcados[4] == 2 && locaisMarcados[7] == -100)
        {

            locaisMarcados[7] = 2;
            tictactoeEspaco[7].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[7].interactable = false;
            TrocaTurno();
        }

        else

        /*  - O -
            - - -
            - O -
   */

        if (locaisMarcados[1] == 2 && locaisMarcados[7] == 2 && locaisMarcados[4] == -100)
        {

            locaisMarcados[4] = 2;
            tictactoeEspaco[4].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[4].interactable = false;
            TrocaTurno();
        }

        else

        /*  - - -
           - O -
           - O -
  */

        if (locaisMarcados[4] == 2 && locaisMarcados[7] == 2 && locaisMarcados[1] == -100)
        {

            locaisMarcados[1] = 2;
            tictactoeEspaco[1].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[1].interactable = false;
            TrocaTurno();
        }

        else


        /*  - - O
            - O -
            - - -
                   */

        if (locaisMarcados[2] == 2 && locaisMarcados[4] == 2 && locaisMarcados[6] == -100)
        {

            locaisMarcados[6] = 2;
            tictactoeEspaco[6].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[6].interactable = false;
            TrocaTurno();
        }

        else

        /* - - O
           - - -
           O - -
                  */

        if (locaisMarcados[2] == 2 && locaisMarcados[6] == 2 && locaisMarcados[4] == -100)
        {

            locaisMarcados[4] = 2;
            tictactoeEspaco[4].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[4].interactable = false;
            TrocaTurno();
        }
        else

        /* - - -
           - O -
           O - -
               */

        if (locaisMarcados[4] == 2 && locaisMarcados[6] == 2 && locaisMarcados[2] == -100)
        {

            locaisMarcados[2] = 2;
            tictactoeEspaco[2].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[2].interactable = false;
            TrocaTurno();
        }

        else


        /* - - O
           - - O
           - - -
                              */

        if (locaisMarcados[2] == 2 && locaisMarcados[5] == 2 && locaisMarcados[8] == -100)
        {

            locaisMarcados[8] = 2;
            tictactoeEspaco[8].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[8].interactable = false;
            TrocaTurno();
        }

        else

        /* - - O
           - - -
           - - O
                             */

        if (locaisMarcados[2] == 2 && locaisMarcados[8] == 2 && locaisMarcados[5] == -100)
        {

            locaisMarcados[5] = 2;
            tictactoeEspaco[5].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[5].interactable = false;
            TrocaTurno();
        }
        else

        /* - - -
           - - O
           - - O
                             */

        if (locaisMarcados[5] == 2 && locaisMarcados[8] == 2 && locaisMarcados[2] == -100)
        {

            locaisMarcados[2] = 2;
            tictactoeEspaco[2].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[2].interactable = false;
            TrocaTurno();

        }
        else

        /* - - -
           - O O
           - - -
                             */

        if (locaisMarcados[4] == 2 && locaisMarcados[5] == 2 && locaisMarcados[3] == -100)
        {

            locaisMarcados[3] = 2;
            tictactoeEspaco[3].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[3].interactable = false;
            TrocaTurno();
        }
        else

        /* - - -
           O O -
           - - -
                             */

        if (locaisMarcados[4] == 2 && locaisMarcados[3] == 2 && locaisMarcados[5] == -100)
        {

            locaisMarcados[5] = 2;
            tictactoeEspaco[5].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[5].interactable = false;
            TrocaTurno();
        }
        else

        /* - - -
           - - -
           O O -
                            */

        if (locaisMarcados[6] == 2 && locaisMarcados[7] == 2 && locaisMarcados[8] == -100)
        {

            locaisMarcados[8] = 2;
            tictactoeEspaco[8].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[8].interactable = false;
            TrocaTurno();
        }
        else
        /* - - -
           - - -
           O - O
                            */

        if (locaisMarcados[6] == 2 && locaisMarcados[8] == 2 && locaisMarcados[7] == -100)
        {

            locaisMarcados[7] = 2;
            tictactoeEspaco[7].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[7].interactable = false;
            TrocaTurno();
        }
        else
        /* - - -
           - - -
           - O O
                            */

        if (locaisMarcados[7] == 2 && locaisMarcados[8] == 2 && locaisMarcados[6] == -100)
        {

            locaisMarcados[6] = 2;
            tictactoeEspaco[6].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[6].interactable = false;
            TrocaTurno();
        }


        
    }

    /*
     * 
     * COUNTER DA CPU
     * 
     * 
     */




    private void CounterAI()
    {
       /* O X O
          - X -
          - - -
      */

        if (locaisMarcados[0] == 1 && locaisMarcados[2] == 1 && locaisMarcados[1] == -100)
        {

            locaisMarcados[1] = 2;
            tictactoeEspaco[1].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[1].interactable = false;
            TrocaTurno();
        }
        /* O O X
           - X -
           - - -
       */
        else
        if (locaisMarcados[0] == 1 && locaisMarcados[1] == 1 && locaisMarcados[2] == -100)
        {
            locaisMarcados[2] = 2;
            tictactoeEspaco[2].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[2].interactable = false;
            TrocaTurno();
        }
        else
        /*  O - -
            X X -
            O - -
        */
        if (locaisMarcados[0] == 1 && locaisMarcados[6] == 1 && locaisMarcados[3] == -100)
        {
            locaisMarcados[3] = 2;
            tictactoeEspaco[3].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[3].interactable = false;
            TrocaTurno();
        }
        else
        /* O - -
           O X -
           X - -
       */
        if (locaisMarcados[0] == 1 && locaisMarcados[3] == 1 && locaisMarcados[6] == -100)
        {
            locaisMarcados[6] = 2;
            tictactoeEspaco[6].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[6].interactable = false;
            TrocaTurno();
        }
        else

        /*  O - X
            - O -
            - - X
        */

        if (locaisMarcados[0] == 1 && locaisMarcados[4] == 1 && locaisMarcados[8] == -100)
        {
            locaisMarcados[8] = 2;
            tictactoeEspaco[8].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[8].interactable = false;
            TrocaTurno();
        }
        else

         /* X O O
            - X -
            - - -
        */

         if (locaisMarcados[1] == 1 && locaisMarcados[2] == 1 && locaisMarcados[0] == -100)
        {
            locaisMarcados[0] = 2;
            tictactoeEspaco[0].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[0].interactable = false;
            TrocaTurno();
        }

        else

        /*  - O X
            - O -
            - X -
        */
        if (locaisMarcados[1] == 1 && locaisMarcados[4] == 1 && locaisMarcados[7] == -100)
        {
            locaisMarcados[7] = 2;
            tictactoeEspaco[7].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[7].interactable = false;
            TrocaTurno();
        }
        else

        /* X - -
           O X -
           O - -
       */

        if (locaisMarcados[3] == 1 && locaisMarcados[6] == 1 && locaisMarcados[0] == -100)
        {
            locaisMarcados[0] = 2;
            tictactoeEspaco[0].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[0].interactable = false;
            TrocaTurno();
        }
        else

        /* - - -
           - X -
           O O X
       */


        if (locaisMarcados[6] == 1 && locaisMarcados[7] == 1 && locaisMarcados[8] == -100)
        {
            locaisMarcados[8] = 2;
            tictactoeEspaco[8].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[8].interactable = false;
            TrocaTurno();
        }
        else
        /* - - -
           - X -
           X O O
       */

        if (locaisMarcados[7] == 1 && locaisMarcados[8] == 1 && locaisMarcados[6] == -100)
        {
            locaisMarcados[6] = 2;
            tictactoeEspaco[6].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[6].interactable = false;
            TrocaTurno();
        }
        else

        /* - - -
          - X -
          O X O
      */

        if (locaisMarcados[6] == 1 && locaisMarcados[8] == 1 && locaisMarcados[7] == -100)
        {
            locaisMarcados[7] = 2;
            tictactoeEspaco[7].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[7].interactable = false;
            TrocaTurno();
        }
        else

        /* - - O
           - X O
           - - X
       */

        if (locaisMarcados[2] == 1 && locaisMarcados[5] == 1 && locaisMarcados[8] == -100)
        {
            locaisMarcados[8] = 2;
            tictactoeEspaco[8].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[8].interactable = false;
            TrocaTurno();
        }
        else


        /* - - O
           - O -
           X - -
       */

        if (locaisMarcados[2] == 1 && locaisMarcados[4] == 1 && locaisMarcados[6] == -100)
        {
            locaisMarcados[6] = 2;
            tictactoeEspaco[6].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[6].interactable = false;
            TrocaTurno();
        }
        else

       /* - X -
          - O -
          - O -
      */

        if (locaisMarcados[7] == 1 && locaisMarcados[4] == 1 && locaisMarcados[1] == -100)
        {
            locaisMarcados[1] = 2;
            tictactoeEspaco[1].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[1].interactable = false;
            TrocaTurno();
        }
        else


        /* - - O
           - X X
           - - O
     */

        if (locaisMarcados[2] == 1 && locaisMarcados[8] == 1 && locaisMarcados[5] == -100)
        {
            locaisMarcados[5] = 2;
            tictactoeEspaco[5].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[5].interactable = false;
            TrocaTurno();
        }
        else

         /* - - X
            - X O
            - - O
        */

         if (locaisMarcados[5] == 1 && locaisMarcados[8] == 1 && locaisMarcados[2] == -100)
        {
            locaisMarcados[2] = 2;
            tictactoeEspaco[2].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[2].interactable = false;
            TrocaTurno();
        }
        else

         /* X - X
            - O -
            - - O
        */

         if (locaisMarcados[4] == 1 && locaisMarcados[8] == 1 && locaisMarcados[0] == -100)
        {
            locaisMarcados[0] = 2;
            tictactoeEspaco[0].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[0].interactable = false;
            TrocaTurno();
        }


        else

         /* - - -
            X O O
            - - -
        */

         if (locaisMarcados[4] == 1 && locaisMarcados[5] == 1 && locaisMarcados[3] == -100)
        {
            locaisMarcados[3] = 2;
            tictactoeEspaco[3].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[3].interactable = false;
            TrocaTurno();
        }
        else
        /* - - -
          O O X
          - - -
      */

        if (locaisMarcados[4] == 1 && locaisMarcados[3] == 1 && locaisMarcados[5] == -100)
        {
            locaisMarcados[5] = 2;
            tictactoeEspaco[5].image.sprite = playerIcones[turnoID];
            tictactoeEspaco[5].interactable = false;
            TrocaTurno();
        }
    }





    // Update is called once per frame
    void Update () {
		if (isCPU)
        {
           
            CPUmove();
            
        }
	}
    public void Rematch()
    {
        GameSetup();
        for(int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }
        resultPanel.SetActive(false);
        deuVelha.SetActive(false);
        vencedor = false;
        xPlayerBotao.interactable = true;
        oPlayersBotao.interactable = true;
        
    }

    public void Restart()
    {
        Rematch();
        xPlayerScore = 0;
        oPlayerScore = 0;
        xPlayerScoreText.text = "0";
        oPlayerScoreText.text = "0";
        vencedor = false;
        deuVelha.SetActive(false);
    }
    public void TrocaJogador(int qualJogador)
    {
     //   if(qualJogador == 0)
     //   {
     //       turnoID = 0;
      //      iconesTurno[0].SetActive(true);
      //      iconesTurno[1].SetActive(false);
     //   }
      //  else
      //  if (qualJogador ==1)
      //  {
      //      turnoID = 1;
      //      iconesTurno[0].SetActive(false);
      //      iconesTurno[1].SetActive(true);
      //  } 
    }

    public void Velha()
    {
        resultPanel.SetActive(true);
        deuVelha.SetActive(true);
        resultText.text = "DEU VELHA!!!!!!!!!";
    }

}
